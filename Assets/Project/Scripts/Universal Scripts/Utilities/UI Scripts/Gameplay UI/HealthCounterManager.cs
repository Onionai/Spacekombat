using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

namespace Onion_AI
{
    public class HealthCounterManager : MonoBehaviour
    {
        private DateTime apiTime;
        private DateTime currentTime;
        private DateTime lastDamageTime;
        private WaitForSeconds waitForSeconds;

        private Coroutine RegenerationCoroutine;
        public static HealthCounterManager Instance;
        private HealthCounterPanel healthCounterPanel;
        
        [Header("Parameters")]
        public int currentLifeCount;
        private bool hasSetLastTime;
        [SerializeField] private int maxLifeCount;
        [SerializeField] private int regenerationInterval;

        private const string currentHealthKey = "CurrentHealth";
        private const string lastDamageTimeKey = "LastDamageTime";
        private const string hasSetLastDamageKey = "SetLastDamageKey";
        private const string worldTimeAPI = "https://timeapi.io/api/Time/current/zone?timeZone=UTC";

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }

            LoadHealthState();
            waitForSeconds = new WaitForSeconds(1f);
        }

        private void Update()
        {
            if(currentLifeCount < maxLifeCount && RegenerationCoroutine == null)
            {
                RegenerationCoroutine = StartCoroutine(CheckForRegeneration());
            }
        }

        private IEnumerator CheckForRegeneration()
        {
            while(currentLifeCount < maxLifeCount)
            {
                yield return StartCoroutine(GetWorldTime(worldTime => {currentTime = worldTime;}));
                TimeSpan timeSinceDamage = currentTime - lastDamageTime;
                print($"current ({currentTime }- {lastDamageTime}) = {timeSinceDamage}");

                if(Math.Abs(timeSinceDamage.TotalSeconds) >= regenerationInterval)
                {
                    currentLifeCount++;
                    lastDamageTime = currentTime;
                    SaveHealthState();
                }

                TimeSpan remainingTime = TimeSpan.FromSeconds(regenerationInterval) - (currentTime - lastDamageTime);
                print($"{TimeSpan.FromSeconds(regenerationInterval)} - ({currentTime }- {lastDamageTime}) = {remainingTime}");
                healthCounterPanel.timeSinceDamage = (remainingTime > TimeSpan.Zero) ? remainingTime : TimeSpan.Zero;
                healthCounterPanel.UpdateHealthUI(healthCounterPanel.timeSinceDamage);
                yield return waitForSeconds;
            }
            RegenerationCoroutine = null;
        }

        private IEnumerator GetWorldTime(Action<DateTime>onComplete)
        {
            yield return StartCoroutine(FetchWorldTimeFromAPI());
            onComplete?.Invoke(apiTime);
        }

        public void ReduceHealthCounter()
        {
            currentLifeCount--;
            if(RegenerationCoroutine == null)
            {
                hasSetLastTime = false;
                StartCoroutine(FetchWorldTimeFromAPI());
            }
        }

        public void SetHealthCounterPanel(HealthCounterPanel hcp)
        {
            healthCounterPanel = hcp;
        }

        public void LoadHealthState()
        {
            currentLifeCount = PlayerPrefs.GetInt(currentHealthKey, maxLifeCount);

            if(PlayerPrefs.HasKey(lastDamageTimeKey) && PlayerPrefs.HasKey(hasSetLastDamageKey))
            {
                int hasSetKey = PlayerPrefs.GetInt(hasSetLastDamageKey);
                hasSetLastTime = (hasSetKey == 1);

                string savedTime = PlayerPrefs.GetString(lastDamageTimeKey);
                lastDamageTime = DateTime.Parse(savedTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
                return;
            }
            hasSetLastTime = false;
            StartCoroutine(FetchWorldTimeFromAPI());
        }

        private void SaveHealthState()
        {
            int hasSetKey = hasSetLastTime ? 1 : 0;
            PlayerPrefs.SetInt(hasSetLastDamageKey, hasSetKey);
            PlayerPrefs.SetInt(currentHealthKey, currentLifeCount);
            PlayerPrefs.SetString(lastDamageTimeKey, lastDamageTime.ToString("o"));

            PlayerPrefs.Save();
        }

        private IEnumerator FetchWorldTimeFromAPI()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(worldTimeAPI);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var timeData = webRequest.downloadHandler.text;
                apiTime = ParseTime(timeData);

                if(hasSetLastTime != true)
                {
                    hasSetLastTime = true;
                    lastDamageTime = ParseTime(timeData);
                    SaveHealthState();
                }
            }
            else
            {
                Debug.LogError("Error: " + webRequest.error);
            }
        }

        private DateTime ParseTime(string dateTime)
        {
            string time = Regex.Match(dateTime, @"\d{2}:\d{2}:\d{2}").Value;
            string date = Regex.Match(dateTime, @"^\d{4}-\d{2}-\d{2}").Value;

            return DateTime.Parse(string.Format("{0} {1}", date, time));
        }
    }
}

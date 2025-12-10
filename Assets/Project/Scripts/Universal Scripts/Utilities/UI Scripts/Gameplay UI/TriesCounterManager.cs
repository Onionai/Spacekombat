using System;
using UnityEngine;

public class TriesCounterManager : MonoBehaviour
{
    private TriesCounterPanel counterPanel;
    public static TriesCounterManager Instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private int maxBars = 5;
    [SerializeField] private int regenerationMinutes = 20;

    private float progressSeconds;
    private long lastSavedUtcSeconds;

    public int CurrentTriesNumber { get; private set; }
    private int RegenerationSeconds => regenerationMinutes * 60;

    public event Action OnTriesChanged;
    public event Action OnTriesFullyRefilled;

    private const string PrefsKey_LastUtc = "Tries_LastUtc_v1_fixed";
    private const string PrefsKey_Units = "Tries_CurrentUnits_v1_fixed";
    private const string PrefsKey_Progress = "Tries_Progress_v1_fixed";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadAndApplyOfflineRegen();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(counterPanel != null && counterPanel.gameObject.activeSelf)
        {
            counterPanel.UpdateCounterUI();
        }
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            SaveState();
        }
    }

    private void OnApplicationQuit()
    {
        SaveState();
    }

    public void SetCounterPanel(TriesCounterPanel counter)
    {
        counterPanel = counter;
    }

    public bool SpendTry()
    {
        RecalculateFromNow();

        if (CurrentTriesNumber <= 0)
        {
            return false;
        }
        CurrentTriesNumber--;
        lastSavedUtcSeconds = GetUtcNowSeconds();
        OnTriesChanged?.Invoke();
        SaveState();
        return true;
    }

    public void LoseTry()
    {
        SpendTry();
    }

    public float GetSecondsUntilNextBarFull()
    {
        RecalculateFromNowWithoutMutating(out double computedProgress, out bool reachedMax);

        if (reachedMax)
        {
            return 0f;
        }
        double remaining = RegenerationSeconds - (computedProgress % RegenerationSeconds);
        return (float)Math.Ceiling(remaining);
    }

    public float GetProgressFraction()
    {
        RecalculateFromNowWithoutMutating(out double computedProgress, out bool reachedMax);
        if (reachedMax)
        {
            return 0f;
        }
        return (float)((computedProgress % RegenerationSeconds) / (double)RegenerationSeconds);
    }

    public int GetMaxBars() => maxBars;

    private void RecalculateFromNow()
    {
        long now = GetUtcNowSeconds();

        if (lastSavedUtcSeconds <= 0)
        {
            lastSavedUtcSeconds = now;
            SaveState();
            return;
        }
        double delta = Math.Max(0.0, (now - lastSavedUtcSeconds));
        double totalSecondsWorth = (double)CurrentTriesNumber * RegenerationSeconds + progressSeconds + delta;

        int newTries = (int)Math.Floor(totalSecondsWorth / RegenerationSeconds);
        newTries = Mathf.Clamp(newTries, 0, maxBars);

        double leftover = totalSecondsWorth - (newTries * RegenerationSeconds);
        bool wasFull = CurrentTriesNumber >= maxBars;

        CurrentTriesNumber = newTries;
        progressSeconds = (float)leftover;
        lastSavedUtcSeconds = now;

        if (CurrentTriesNumber >= maxBars)
        {
            progressSeconds = 0f;
            if (!wasFull)
            {
                OnTriesFullyRefilled?.Invoke();
            }
            OnTriesChanged?.Invoke();
        }
        else
        {
            OnTriesChanged?.Invoke();
        }
        SaveState();
    }

    private void RecalculateFromNowWithoutMutating(out double computedProgressSeconds, out bool reachedMax)
    {
        long now = GetUtcNowSeconds();
        if (lastSavedUtcSeconds <= 0)
        {
            computedProgressSeconds = progressSeconds;
            reachedMax = CurrentTriesNumber >= maxBars;
            return;
        }

        double delta = Math.Max(0.0, (now - lastSavedUtcSeconds));
        double totalSecondsWorth = (double)CurrentTriesNumber * RegenerationSeconds + progressSeconds + delta;
        int newTries = (int)Math.Floor(totalSecondsWorth / RegenerationSeconds);
        newTries = Mathf.Clamp(newTries, 0, maxBars);
        double leftover = totalSecondsWorth - (newTries * RegenerationSeconds);

        computedProgressSeconds = leftover;
        reachedMax = newTries >= maxBars;
    }

    private void LoadAndApplyOfflineRegen()
    {
        int savedUnits = PlayerPrefs.GetInt(PrefsKey_Units, maxBars);
        float savedProgress = PlayerPrefs.GetFloat(PrefsKey_Progress, 0f);
        string lastStr = PlayerPrefs.GetString(PrefsKey_LastUtc, null);

        long savedUtc = 0;
        if (!string.IsNullOrEmpty(lastStr))
        {
            long.TryParse(lastStr, out savedUtc);
        }

        long now = GetUtcNowSeconds();

        if (savedUtc <= 0)
        {
            CurrentTriesNumber = Mathf.Clamp(savedUnits, 0, maxBars);
            progressSeconds = Mathf.Clamp(savedProgress, 0f, RegenerationSeconds);
            lastSavedUtcSeconds = now;
            SaveState();
            return;
        }

        double delta = Math.Max(0.0, (now - savedUtc));
        double totalSecondsWorth = (double)Mathf.Clamp(savedUnits, 0, maxBars) * RegenerationSeconds
            + Mathf.Clamp(savedProgress, 0f, RegenerationSeconds) + delta;

        int newTries = (int)Math.Floor(totalSecondsWorth / RegenerationSeconds);
        newTries = Mathf.Clamp(newTries, 0, maxBars);
        double leftover = totalSecondsWorth - (newTries * RegenerationSeconds);

        CurrentTriesNumber = newTries;
        lastSavedUtcSeconds = now;
        progressSeconds = (float)leftover;

        if (CurrentTriesNumber >= maxBars)
        {
            CurrentTriesNumber = maxBars;
            progressSeconds = 0f;
            OnTriesFullyRefilled?.Invoke();
        }
        SaveState();
    }

    private void SaveState()
    {
        PlayerPrefs.SetInt(PrefsKey_Units, Mathf.Clamp(CurrentTriesNumber, 0, maxBars));
        PlayerPrefs.SetFloat(PrefsKey_Progress, Mathf.Clamp(progressSeconds, 0f, RegenerationSeconds));
        PlayerPrefs.SetString(PrefsKey_LastUtc, GetUtcNowSeconds().ToString());
        PlayerPrefs.Save();
    }

    private long GetUtcNowSeconds() => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}

using System;
using System.Text;
using UnityEngine;
using Unity.Services.Core;
using UnityEngine.Networking;
using Unity.Services.CloudSave;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.CloudSave.Models;

namespace Onion_AI
{
    [Serializable]
    public class TelegramUserData
    {
        public string id;
        public string username;
    }

    public class UserManagerController : MonoBehaviour
    {
        private string telegramUserID;
        private string telegramUsername;
        private const string telegramApiUrl = "https://api.telegram.org/bot7455844015:AAEV6KnOxK4AVgNuH9Q0-Z86rVKl9AVASOA/getMe";

        public static UserManagerController Instance { get; private set; }
        [field: SerializeField] public UserProfile CurrentUser { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private async void Start()
        {
            try
            {
                await UnityServices.InitializeAsync();
                if (!AuthenticationService.Instance.IsSignedIn)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error Initializing Unity Service: {exception.Message}");
                return;
            }

            await FetchTelegramData();

            if (string.IsNullOrEmpty(telegramUserID) || string.IsNullOrEmpty(telegramUsername))
            {
                Debug.LogError("Telegram User data is missing, cannot proceed.");
                return;
            }
            await HandleUserAuthentication();
        }

        private async System.Threading.Tasks.Task HandleUserAuthentication()
        {
            try
            {
                var userDataKey = $"{telegramUserID}_UserData";
                var userDataKeys = new HashSet<string> { userDataKey };
                var savedData = await CloudSaveService.Instance.Data.Player.LoadAsync(userDataKeys);

                if (savedData.ContainsKey(userDataKey))
                {
                    var userDataItem = savedData[userDataKey];
                    var userDataJson = userDataItem.Value.GetAsString();
                    CurrentUser = JsonUtility.FromJson<UserProfile>(userDataJson);
                }
                else
                {
                    CurrentUser = new UserProfile(telegramUserID, telegramUsername);

                    var userDataJson = JsonUtility.ToJson(CurrentUser);
                    var savedItem = new SaveItem(userDataJson, "UserWriteLock");
                    await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, SaveItem> { { userDataKey, savedItem } });
                }
                string password = Encoding.UTF8.GetString(CurrentUser.UserPassword);
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(CurrentUser.UserName, password);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error Handling Authentication: {exception.Message}");
            }
        }

        private async System.Threading.Tasks.Task FetchTelegramData()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(telegramApiUrl);
            try
            {
                var operation = webRequest.SendWebRequest();
                while (!operation.isDone)
                {
                    await System.Threading.Tasks.Task.Yield();
                }

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    var jsonResponse = webRequest.downloadHandler.text;
                    ParseTelegramData(jsonResponse);
                    Debug.Log("Successfully fetched Telegram data.");
                }
                else
                {
                    Debug.LogError($"Error fetching Telegram data: {webRequest.error}");
                }
            }
            catch (Exception exception)
            {
                Debug.LogError($"Exception fetching Telegram data: {exception.Message}");
            }
        }

        private void ParseTelegramData(string jsonResponse)
        {
            telegramUserID = ParseJsonField(jsonResponse, "id");
            telegramUsername = ParseJsonField(jsonResponse, "username");
        }


        private string ParseJsonField(string json, string field)
        {
            string pattern = $"\"{field}\":\"(.*?)\"";
            var match = System.Text.RegularExpressions.Regex.Match(json, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }
    }
}
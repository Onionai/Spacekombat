using UnityEngine;
using System.Text;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

namespace Onion_AI
{
    public class AeonPaymentSystem : MonoBehaviour
    {
        [System.Serializable]
        private class ResponseData
        {
            public ResponseDataDetail model;

            [System.Serializable]
            public class ResponseDataDetail
            {
                public string webUrl;
            }
        }

        private float amount;
        private bool hasInitialized;
        private string currency = "USD";
        private string telegramUserID = "6265640740";

        private const string appId = "CPM202412040924";
        private const string secretKey = "FKEA2mnyCJlpwi9ejegt7LIp3y2ExM";
        private const string aeonApiUrl = "https://sbx-crypto-payment-api.aeon.xyz/open/api/tg/payment/V2";

        [Header("UI Elements")]
        [SerializeField] private Button aeonButton;

        private void OnEnable()
        {
            if (hasInitialized)
            {
                return;
            }
            UIEventsStaticClass.AddButtonListener(aeonButton, OnPayButtonClick);
            hasInitialized = true;
        }

        private void OnPayButtonClick()
        {
            amount = 10.0f;
            StartCoroutine(CreateOrder(amount, currency, telegramUserID));
        }

        private IEnumerator CreateOrder(float amount, string currency, string telegramUserID)
        {
            var requestData = new OrderRequestData
            {
                appId = appId,
                sign = secretKey,
                merchantOrderNo = System.Guid.NewGuid().ToString(),
                orderAmount = amount.ToString("F2"),
                payCurrency = currency,
                userId = telegramUserID,
                redirectURL = "https://yourgame.com/payment-success",
                callbackURL = "https://yourserver.com/payment-webhook",
                tgModel = "BOT",
                customParam = "{\"botName\":\"YourBotName\",\"orderDetail\":\"Purchase Details\",\"chatId\":\"" + telegramUserID + "\"}"
            };

            string jsonData = JsonUtility.ToJson(requestData);

            using (UnityWebRequest request = new UnityWebRequest(aeonApiUrl, "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData));
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log(request.downloadHandler.text);
                    var response = JsonUtility.FromJson<ResponseData>(request.downloadHandler.text);
                    if (response?.model?.webUrl != null)
                    {
                        Application.OpenURL(response.model.webUrl);
                    }
                    else
                    {
                        Debug.LogError("Error: Pay URL is missing in the response.");
                    }
                }
                else
                {
                    Debug.LogError($"Error creating order: {request.error} \nResponse: {request.downloadHandler.text}");
                }
            }
        }
    }

    [System.Serializable]
    public class OrderRequestData
    {
        public string appId;
        public string sign;
        public string merchantOrderNo;
        public string orderAmount;
        public string payCurrency;
        public string userId;
        public string paymentTokens;
        public string redirectURL;
        public string callbackURL;
        public string customParam;
        public string expiredTime;
        public string payType;
        public string paymentNetworks;
        public string orderModel = "ORDER";
        public string tgModel;
    }
}

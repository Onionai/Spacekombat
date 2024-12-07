using System;
using UnityEngine;
using System.Text;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Onion_AI
{
    public class AeonPaymentSystem : MonoBehaviour
    {
        [Serializable]
        private class ResponseData
        {
            public ResponseDataDetail model;

            [Serializable]
            public class ResponseDataDetail
            {
                public string webUrl;
            }
        }

        private float amount;
        private bool hasInitialized;
        private string currency = "USD";
        private string telegramUserID = "6265640740";

        private const string appId = "fTNv0tLTW9xtfSO8PSJJOFyu";
        private const string secretKey = "FKEA2mnyCJlpwi9ejegt7LIp3y2ExM";
        private const string aeonApiUrl = "https://sbx-crypto-payment-api.aeon.xyz/open/api/tg/payment/V2";

        [Header("UI Elements")]
        [SerializeField] private Button aeonButton;

        private void OnEnable()
        {
            if (hasInitialized)
                return;

            UIEventsStaticClass.AddButtonListener(aeonButton, OnPayButtonClick);
            hasInitialized = true;
        }

        private void OnPayButtonClick()
        {
            amount = 10.0f;
            StartCoroutine(CreateOrder(amount, currency, telegramUserID));
        }

        /// <summary>
        /// Generates the signature required for the API request using only the necessary parameters.
        /// </summary>
        private string GenerateSignatureParameter(Dictionary<string, string> parameters, string secretKey)
        {
            if (parameters.ContainsKey("sign"))
            {
                parameters.Remove("sign");
            }

            var requiredParameters = new Dictionary<string, string>
            {
                { "appId", parameters["appId"] },
                { "merchantOrderNo", parameters["merchantOrderNo"] },
                { "orderAmount", parameters["orderAmount"] },
                { "payCurrency", parameters["payCurrency"] },
                { "userId", parameters["userId"] },
                { "paymentTokens", parameters["paymentTokens"] }
            };

            // Sort parameters alphabetically by key
            var sortedParameters = requiredParameters.OrderBy(p => p.Key).ToList();
            StringBuilder signBuilder = new StringBuilder();

            // Append each parameter to the sign string
            foreach (var param in sortedParameters)
            {
                signBuilder.Append($"{param.Key}={param.Value}&");
            }
            signBuilder.Append($"key={secretKey}");

            string signString = signBuilder.ToString();
            Debug.Log($"Signature string: {signString}");

            // Compute SHA-512 hash of the sign string
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(signString));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
            }
        }

        /// <summary>
        /// Coroutine to create an order and process the response.
        /// </summary>
        private IEnumerator CreateOrder(float amount, string currency, string telegramUserID)
        {
            OrderRequestData orderRequestData = new OrderRequestData
            {
                appId = appId,
                callbackURL = "https://cp-bba-sbx.alchemypay.org",
                customParam = $"{{\"botName\":\"YourBotName\",\"orderDetail\":\"Special Upgrade Pack - Cyber Edition\",\"chatId\":\"{telegramUserID}\"}}",
                expiredTime = "999999",
                merchantOrderNo = Guid.NewGuid().ToString(),
                orderAmount = amount.ToString(),
                payCurrency = currency,
                tgModel = "MINIAPP",
                userId = telegramUserID,
                paymentTokens = "USDT,ETH,USDC,BTC,PCI"
            };

            // Generate signature for the order
            string signature = GenerateSignatureParameter(ToDictionary(orderRequestData), secretKey);
            orderRequestData.sign = signature;

            // Convert the order data to JSON format
            string jsonData = JsonUtility.ToJson(orderRequestData);

            using (UnityWebRequest request = new UnityWebRequest(aeonApiUrl, "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData));
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                // Handle the response
                if (request.result == UnityWebRequest.Result.Success)
                {
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

        [Serializable]
        public class OrderRequestData
        {
            public string appId;
            public string callbackURL;
            public string customParam;
            public string expiredTime;

            public string merchantOrderNo;
            public string orderAmount;
            public string orderModel = "ORDER";
            public string payCurrency;
            public string sign;
            public string tgModel;
            public string userId;
            public string paymentTokens;
        }

        /// <summary>
        /// Convert OrderRequestData to Dictionary for signature generation with only required parameters.
        /// </summary>
        private Dictionary<string, string> ToDictionary(OrderRequestData data)
        {
            var dict = new Dictionary<string, string>
            {
                { "appId", data.appId },
                { "merchantOrderNo", data.merchantOrderNo },
                { "orderAmount", data.orderAmount },
                { "payCurrency", data.payCurrency },
                { "userId", data.userId },
                { "paymentTokens", data.paymentTokens }
            };
            return dict;
        }
    }
}

using Google.Apis.AndroidPublisher.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;


namespace Server
{
    public class IapValidation : MonoBehaviourSingleton<IapValidation>
    {
        private const string ACCESS_TOKEN_PATH = "C:\\Server\\accessToken.json"; //pc-api json file


        public delegate void OnSuccessValidatedHandler(int clientId, string productId);

        private GoogleCredential accessTokenCredential;
        private List<string> usedTokens;


        private void Start()
        {
            Load();

            accessTokenCredential = GoogleCredential.FromFile(ACCESS_TOKEN_PATH);
        }
        private void OnApplicationQuit()
        {
            Save();
        }


        public void Validate(int clientId, string productId, string purchaseToken, OnSuccessValidatedHandler OnSuccessValidated)
        {
            if (usedTokens.Contains(purchaseToken))
            {
                return;
            }

            if (IsValidated(productId, purchaseToken))
            {
                usedTokens.Add(purchaseToken);

                //give product:
                OnSuccessValidated?.Invoke(clientId, productId);
            }
        }

        private bool IsValidated(string productId, string purchaseToken)
        {
            var credential = accessTokenCredential.CreateScoped(AndroidPublisherService.Scope.Androidpublisher);
            var service = new AndroidPublisherService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                }
            );

            string packageName = Application.identifier;
            var productRequest = service.Purchases.Products.Get(packageName, productId, purchaseToken);

            try
            {
                var productPurchase = productRequest.Execute();
            }
            catch
            {
                return false;
            }

            return true;
        }
        private void Save()
        {

        }
        private void Load()
        {

        }
    }
}

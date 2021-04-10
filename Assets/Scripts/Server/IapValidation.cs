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


        [SerializeField] private GoldAmountView goldAmountView;


        private GoogleCredential accessTokenCredential;
        private Dictionary<int, string> clientTokenDictionary = new Dictionary<int, string>();


        private void Start()
        {
            accessTokenCredential = GoogleCredential.FromFile(ACCESS_TOKEN_PATH);
        }


        public void GiveProduct(int clientId, string productId, string purchaseToken)
        {
            if (clientTokenDictionary.ContainsKey(clientId))
            {
                if (clientTokenDictionary[clientId] == purchaseToken)
                {
                    return;
                }
            }

            if (IsValidated(productId, purchaseToken))
            {
                clientTokenDictionary.Add(clientId, purchaseToken);

                //give product:

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
    }
}

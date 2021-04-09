using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Purchasing.Security;

namespace Networking.Server
{
    public class IAPValidation : MonoBehaviour
    {
        private void Start1()
        {
            string packageName = Application.identifier;
            string productId = packageName + "." + "gold10";
            string token = "";
            string requestUrlString = $"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/products/{productId}/tokens/{token}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrlString);
            var response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Debug.Log(responseFromServer);
            }

            response.Close();
        }


        public static void Validate(string receipt)
        {
            //string packageName = Application.identifier;
            //string productId = packageName + "." + "gold10";
            //string token = "";
            //string requestUrlString = $"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/products/{productId}/tokens/{token}";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrlString);

            //string receipt = "";//e.purchasedProduct.receipt;

            //CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
            //FileStream fileStream = File.Create(ServerStorage.SAVE_PATH + "\\receipt" + Random.Range(0, int.MaxValue));
            //string receiptText = "";

            //try
            //{
            //    // On Google Play, result has a single product ID.
            //    // On Apple stores, receipts contain multiple products.
            //    var result = validator.Validate(receipt);
            //    // For informational purposes, we list the receipt(s)
            //    Debug.Log("Receipt is valid. Contents:");
            //    receiptText += "Receipt is valid. Contents:" + "\n";
            //    foreach (IPurchaseReceipt productReceipt in result)
            //    {
            //        Debug.Log(productReceipt.productID);
            //        Debug.Log(productReceipt.purchaseDate);
            //        Debug.Log(productReceipt.transactionID);

            //        receiptText += "productReceipt.productID: " + productReceipt.productID + '\n';
            //        receiptText += "productReceipt.purchaseDate: " + productReceipt.purchaseDate + "\n";
            //        receiptText += "productReceipt.transactionID: " + productReceipt.transactionID + '\n';

            //        GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
            //        if (null != google)
            //        {
            //            // This is Google's Order ID.
            //            // Note that it is null when testing in the sandbox
            //            // because Google's sandbox does not provide Order IDs.
            //            Debug.Log(google.orderID);
            //            Debug.Log(google.purchaseState);
            //            Debug.Log(google.purchaseToken);

            //            receiptText += "google.orderID: " + google.orderID + "\n";
            //            receiptText += "google.purchaseState: " + google.purchaseState + "\n";
            //            receiptText += "google.purchaseToken: " + google.purchaseToken + "\n";
            //        }

            //        AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
            //        if (null != apple)
            //        {
            //            Debug.Log(apple.originalTransactionIdentifier);
            //            Debug.Log(apple.subscriptionExpirationDate);
            //            Debug.Log(apple.cancellationDate);
            //            Debug.Log(apple.quantity);

            //            receiptText += "apple.originalTransactionIdentifier: " + apple.originalTransactionIdentifier + "\n";
            //            receiptText += "apple.subscriptionExpirationDate: " + apple.subscriptionExpirationDate + "\n";
            //            receiptText += "apple.cancellationDate: " + apple.cancellationDate + "\n";
            //            receiptText += "apple.quantity: " + apple.quantity + "\n";
            //        }
            //    }
            //}
            //catch (IAPSecurityException)
            //{
            //    Debug.Log("Invalid receipt, not unlocking content");

            //    receiptText += "Invalid receipt, not unlocking content" + "\n";
            //}
            //finally
            //{
            //    var bytes = Encoding.UTF8.GetBytes(receiptText);
            //    fileStream.Write(bytes, 0, bytes.Length);
            //    fileStream.Close();
            //}
        }
    }

}

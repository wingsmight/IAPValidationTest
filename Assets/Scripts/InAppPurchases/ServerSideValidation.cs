using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
//using Google.Apis;
//using Google.Apis.Discovery;
//using Google.Apis.Services;
//using Google.Apis.Discovery.v1;
//using Google.Apis.Discovery.v1.Data;

public class ServerSideValidation : MonoBehaviour
{
    private void Start()
    {
        string token = "nckdelcmomkejldchdakoapp.AO-J1OwC5XrR2urL0_YYBLQ_VTqNwSYJC1kGXxbJzP87VcMvSN7d7bswxcrArnu-XdlHoFeKrdtJQgeVExXOW7b5y-hNyTz0L1ao9_vPjJiAJtTNGp9ZklI";
        //Validate(token);

       //DoMagicAsync().Wait();
    }


    public static void Validate(string token)
    {
        string packageName = Application.identifier;
        string productId = "gold11";
        //string token = "ojppckbnkpfkbjjbdbnaaopk.AO-J1OywYo6BLe-DlTjT1Hr5oNn0tod7GsEuVa-2c1b-ibKcfQ6VTCgKVRRbRHUfngeYycWYb-gXrH46dT4Mqwf09Ar28yAAJ1-V9eXqbNeDVzQhcZAQmMs";
        TextAsset tokenAccessTextAsset = Resources.Load("accessToken") as TextAsset;
        string accessToken = tokenAccessTextAsset.text;
        string requestUrlString = $"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/products/{productId}/tokens/{token}";
        //string requestUrlString = $"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/products/{productId}/tokens/{token}?access_token={accessToken}";
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

            File.WriteAllText(Application.persistentDataPath + "/responseFromServer.json", responseFromServer);
        }

        response.Close();
    }
    //public async System.Threading.Tasks.Task DoMagicAsync()
    //{
    //    TextAsset tokenAccessTextAsset = Resources.Load("accessToken") as TextAsset;
    //    string accessToken = tokenAccessTextAsset.text;

    //    // Create the service.
    //    var service = new DiscoveryService(new BaseClientService.Initializer
    //    {
    //        ApplicationName = "IAPValidationTest",
    //        ApiKey = accessToken,
    //    });

    //    // Run the request.
    //    Debug.Log("Executing a list request...");
    //    var result = await service.Apis.List().ExecuteAsync();

    //    // Display the results.
    //    if (result.Items != null)
    //    {
    //        foreach (DirectoryList.ItemsData api in result.Items)
    //        {
    //            Debug.Log(api.Id + " - " + api.Title);
    //        }
    //    }
    //}
}

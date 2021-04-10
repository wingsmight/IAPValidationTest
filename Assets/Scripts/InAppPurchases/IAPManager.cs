using System;
using System.IO;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using Server;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private const int CLIENT_ID = 0;


    public static IAPManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public delegate void OnPurchasedHandler(PurchaseEventArgs args);
    public event OnPurchasedHandler OnPurchased;


    //Step 1 create your products
    private string[] golds = new string[5]
    {
        "gold11",
        "gold12",
        "gold13",
        "gold14",
        "gold15",
    };


    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        foreach (var gold in golds)
        {
            builder.AddProduct(gold, ProductType.Consumable);
        }

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods
    public void BuyGold(int amount)
    {
        BuyProductID("gold" + amount);
    }



    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_TVOS
        var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);

        try
        {
            var result = validator.Validate(args.purchasedProduct.receipt);
            Debug.Log("Receipt is valid. Contents:");
            string productId = args.purchasedProduct.definition.id;
            foreach (IPurchaseReceipt productReceipt in result)
            {
                foreach (var gold in golds)
                {
                    if (string.Equals(productId, gold, StringComparison.Ordinal))
                    {
                        int goldCount = int.Parse(gold.Replace("gold", ""));
                        string receipt = args.purchasedProduct.receipt;

                        File.WriteAllText(Application.persistentDataPath + "/receipt.txt", receipt);

                        string purchaseToken = GetPurchaseTokenFromReceipt(receipt);

                        File.WriteAllText(Application.persistentDataPath + "/purchaseToken.txt", purchaseToken);

                        IapValidation.Instance.GiveProduct(CLIENT_ID, productId, purchaseToken);

                        OnPurchased?.Invoke(args);

                        return PurchaseProcessingResult.Complete;
                    }
                }
            }
        }
        catch (IAPSecurityException)
        {
            Debug.Log("Purchase Failed");
        }
#endif

        return PurchaseProcessingResult.Complete;
    }










    //**************************** Dont worry about these methods ***********************************
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    private string GetPurchaseTokenFromReceipt(string receipt)
    {
        string formatedReceipt = receipt.Replace("\\\\\\", "");
        string startPhrase = "purchaseToken\":\"";
        int startIndex = formatedReceipt.IndexOf(startPhrase);
        int finishIndex = formatedReceipt.IndexOf("\",\"", startIndex + startPhrase.Length);
        string purchaseToken = formatedReceipt.Substring(startPhrase.Length + startIndex, finishIndex - (startPhrase.Length + startIndex));

        return purchaseToken;
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGoldButton : UIButton
{
    private const string PRODUCT_NAME = "gold";


    [SerializeField] private int amount;
    [SerializeField] private GoldAmountView goldAmountView;


    protected override void OnClick()
    {
        IAPManager.instance.BuyProduct(PRODUCT_NAME, amount);
        IAPManager.instance.OnPurchased += OnPurchased;
    }
    protected override void OnDestroy()
    {
        IAPManager.instance.OnPurchased -= OnPurchased;
    }


    private void OnPurchased(string productId)
    {
        if (productId.Contains(PRODUCT_NAME))
        {
            int goldAmount = int.Parse(productId.Replace(PRODUCT_NAME, ""));

            if (goldAmount == amount)
            {
                goldAmountView.AddAmount(goldAmount);
                goldAmountView.Show();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGoldButton : UIButton
{
    [SerializeField] private int amount;
    [SerializeField] private GoldAmountView goldAmountView;


    protected override void OnClick()
    {
        IAPManager.instance.BuyGold(amount);
        IAPManager.instance.OnPurchased += OnPurchased;
    }
    protected override void OnDestroy()
    {
        IAPManager.instance.OnPurchased -= OnPurchased;
    }


    private void OnPurchased(UnityEngine.Purchasing.PurchaseEventArgs args)
    {
        int goldAmount = int.Parse(args.purchasedProduct.definition.id.Replace("gold", ""));

        if (goldAmount == amount)
        {
            goldAmountView.AddAmount(goldAmount);
            goldAmountView.Show();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldAmountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountText;


    private int amount;


    public void Show()
    {
        amountText.text = amount.ToString();
    }
    public void AddAmount(int additiveAmount)
    {
        amount += additiveAmount;
    }
}

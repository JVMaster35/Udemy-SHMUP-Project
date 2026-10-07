using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCurrency : MonoBehaviour
{
    public static UpdateCurrency instance;

    public Text currencyDisplay, scoreDisplay;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DisplayCurrency(StatsManager.instance.money);
    }

    public void DisplayCurrency(int value)
    {
        if(currencyDisplay)
        {
           currencyDisplay.text = "£ " + value.ToString();
        }
    }

    public void UpdateCurrencyDisplay()
    {
        DisplayCurrency(StatsManager.instance.money);
    }

    public void DisplayScore(int value)
    {
        if(scoreDisplay)
        {
           scoreDisplay.text = value.ToString("000000");
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeItem : MonoBehaviour
{
    [Header("Upgrade Menu Objects")]
    public string statName;
    public string itemName;
    public Text itemNameTxt, buyTxt;
    public Slider itemLevelBar;
    public Button buyBtn;


    [Header("Item Prices Setup")]
    public int[] pricesLevel;

    private StatsUpgradeInfo stat;
    private bool isUpgrading;

    // Start is called before the first frame update
    void Start()
    {
        stat = StatsManager.instance.GetStats(statName);

        itemNameTxt.text = itemName;

        buyTxt.text = pricesLevel[stat.level].ToString();

        itemLevelBar.value = stat.level;

        buyBtn.onClick.AddListener(BuyUpgrades);

        UpdateItemDisplay();
    }

    public void BuyUpgrades()
    {
        if(isUpgrading)
        {
            DialogueManager.instance.ShowMessage(statName + " is Currently Upgrading.");
            return;
        }

        if(StatsManager.instance.money >= pricesLevel[stat.level])
        {

            DialogueManager.instance.ShowDialogue("Confirm " + statName + " Upgrade?", ()=>
            {
            //Upgrade
            StatsManager.instance.AddMoney(-pricesLevel[stat.level]);

            StatsManager.instance.statsTimer.Add(statName, DateTime.Now.AddMinutes(StatsManager.instance.GetUpgradeTime(statName)[stat.level]));

            //Start Coroutine
            StartCoroutine(DoUpgrade());
            });

            Debug.Log("Upgrading " + statName);
        }
        else
        {
            //Message: Insufficient Funds!
            //Debug.Log("Insufficient Funds!");
            DialogueManager.instance.ShowMessage("Insufficient Funds to Upgrade " + statName + "!");
        }
    }

    public void UpdateItemDisplay()
    {
        UpdateCurrency.instance.UpdateCurrencyDisplay();
        
        stat = StatsManager.instance.GetStats(statName);

        itemLevelBar.value = stat.level;

        if(stat.level == pricesLevel.Length)
        {
            buyTxt.text = "MAX!";
            return;
        }

        buyTxt.text = pricesLevel[stat.level].ToString();
        CheckForUpgrades();
    }

    public void CheckForUpgrades()
    {
        if(StatsManager.instance.statsTimer.ContainsKey(statName))
        {
            if(DateTime.Now < StatsManager.instance.statsTimer[statName])
            {
                StartCoroutine(DoUpgrade());
            }
            else 
            {
                IncreaseStat();
            }
        }
    }

    IEnumerator DoUpgrade()
    {
        isUpgrading = true;

        TimeSpan timeRemaining = StatsManager.instance.statsTimer[statName] - DateTime.Now;

        while(timeRemaining.TotalSeconds > 0f)
        {
            timeRemaining = StatsManager.instance.statsTimer[statName] - DateTime.Now;
            buyTxt.text = string.Format("{0:00}:{1:00}", timeRemaining.Minutes, timeRemaining.Seconds);
            yield return null;
        }

        //Do Upgrade
        isUpgrading = false;

        IncreaseStat();
    }

    public void IncreaseStat()
    {
        stat.level++;

        if(isUpgrading)
        {
            StopAllCoroutines();
            isUpgrading = false;
        }

        buyTxt.text = pricesLevel[stat.level].ToString();
        itemLevelBar.value = stat.level;
        StatsManager.instance.statsTimer.Remove(statName);

        DialogueManager.instance.ShowMessage(statName + " Upgrade Finished!");
    }
}

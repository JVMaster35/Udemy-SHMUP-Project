using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    public int lives = 5;
    public int money;

    [Header("Upgrade Options")]
    public List<ShootProfiles> blasterUpgradeList = new List<ShootProfiles>();
    public List<ShootProfiles> missileUpgradeList = new List<ShootProfiles>();
    public List<float> healthUpgradeList = new List<float>();
    public List<MegaBombData> megaBombUpgradelist = new List<MegaBombData>();
    public List<ShieldData> shieldUpgradelist = new List<ShieldData>();
    public List<LaserData> laserUpgradelist = new List<LaserData>();


    public Dictionary<string, Medals> achievementList = new Dictionary<string, Medals>();
    public Dictionary<string, bool> levelCompleted = new Dictionary<string, bool>();
    public Dictionary<string, DateTime> statsTimer = new Dictionary<string, DateTime>();

    [Header("Upgrade Timer Data")]
    public List<StatsUpgradeInfo> stats = new List<StatsUpgradeInfo>();

    //Single Pattern Initialization
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use for Initialization
    void Start()
    {
        LoadProgress();
    }

    public void AddMoney(int value)
    {
        money += value;

        //To-Do: Update Money UI
        UpdateCurrency.instance.DisplayCurrency(money);
    }

    //Method for Retrieving Stats Values, i.e. Weapons & PowerUps.
    public T GetStatsValue<T>(string statsName, List<T> statsList)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].name == statsName)
            {
                return statsList[stats[i].level - 1];
            }
        }

        return default(T);
    }

    //Method for Retrieving Upgrade Time
    public float[] GetUpgradeTime(string statsName)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].name == statsName)
            {
                return stats[i].upgradeTime;
            }
        }

        return null;
    }

    public StatsUpgradeInfo GetStats(string statsName)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].name == statsName)
            {
                return stats[i];
            }
        }

        return null;
    }

    public void SaveProgress()
    {
        SaveData saveData = new SaveData();

        saveData.lives = lives;
        saveData.money = money;

        saveData.achievementList = achievementList;
        saveData.levelCompleted = levelCompleted;
        saveData.statsTimer = statsTimer;

        saveData.stats = stats;

        SaveSystem.Save(saveData);
    }

    public void LoadProgress()
    {
        SaveData loadData = SaveSystem.Load<SaveData>();

        lives = loadData.lives;
        money = loadData.money;

        achievementList = loadData.achievementList;
        levelCompleted = loadData.levelCompleted;
        statsTimer = loadData.statsTimer;

        stats = loadData.stats;

        UpdateItemDisplay();
    }

    public void UpdateItemDisplay()
    {
        UpgradeItem[] items = UnityEngine.Object.FindObjectsByType<UpgradeItem>(FindObjectsSortMode.None);
        LevelMenu[] levelMenus = UnityEngine.Object.FindObjectsByType<LevelMenu>(FindObjectsSortMode.None);

        for (int i = 0; i < items.Length; i++)
        {
            items[i].UpdateItemDisplay();
        }

        for (int i = 0; i < levelMenus.Length; i++)
        {
            levelMenus[i].UpdateMenu();
        }
    }

    public void AddMedals(string levelName, Medals medal)
    {
        Medals newMedals = new Medals();

        if(achievementList.ContainsKey(levelName))
        {
            newMedals.kill = medal.kill ? true : achievementList[levelName].kill;

            newMedals.rescue = medal.rescue ? true : achievementList[levelName].rescue;

            newMedals.untouched = medal.untouched ? true : achievementList[levelName].untouched;

            achievementList[levelName] = newMedals;
        }
        else
        {
            achievementList.Add(levelName, medal);   
        }
    }

    public void AddLevelCompleted(string levelName, bool value)
    {
        if(levelCompleted.ContainsKey(levelName))
        {
            levelCompleted[levelName] = value;
        }
        else
        {
            levelCompleted.Add(levelName, value);
        }
    }
}

[System.Serializable]
public class StatsUpgradeInfo
{
    public string name;
    public int level;
    public float[] upgradeTime;
}

[System.Serializable]
public class MegaBombData
{
    public float radius;
    public float damage;
}

[System.Serializable]
public class ShieldData
{
    public float shieldDuration;
}

[System.Serializable]
public class LaserData
{
    public float lazerDuration;
}

[System.Serializable]
public class SaveData
{
    public int lives;
    public int money;
    public Dictionary<string, Medals> achievementList = new Dictionary<string, Medals>();
    public Dictionary<string, bool> levelCompleted = new Dictionary<string, bool>();
    public Dictionary<string, DateTime> statsTimer = new Dictionary<string, DateTime>();
    public List<StatsUpgradeInfo> stats = new List<StatsUpgradeInfo>();
}



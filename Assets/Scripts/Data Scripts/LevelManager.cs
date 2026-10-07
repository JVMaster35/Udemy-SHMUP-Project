using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Medals medals = new Medals();
    public int totalEnemy, enemyKilled, totalRescue, humansRescued, score;

    public UnityEvent onGameEnd;

    private string levelName;

    private void Awake()
    {
        instance = this;
        medals.untouched = true;

        levelName = SceneManager.GetActiveScene().name;
    }

    public void RegisterEnemy()
    {
        totalEnemy++;
    }

    public void RegisterRescue()
    {
        totalRescue++;
    }

    public void AddEnemyKill(int scoreValue)
    {
        enemyKilled++;
        score += scoreValue;
        UpdateCurrency.instance.DisplayScore(score);
    }

    public void AddRescue(int scoreValue)
    {
        humansRescued++;
        score += scoreValue;
        UpdateCurrency.instance.DisplayScore(score);
    }

    public void PlayerHit()
    {
        medals.untouched = false;
    }

    public void GameEnd(bool win)
    {
        StartCoroutine(CountDelay(win));
    }

    IEnumerator CountDelay(bool win)
    {
        yield return new WaitForSeconds(0.25f);

        if (enemyKilled >= totalEnemy)
        {
            medals.kill = true;
        }

        if(humansRescued >= totalRescue) 
        { 
            medals.rescue = true;
        }

        StatsManager.instance.AddMedals(levelName, medals);
        StatsManager.instance.AddLevelCompleted(levelName, win);
        StatsManager.instance.SaveProgress();

        onGameEnd.Invoke();
    }
}

[System.Serializable]
public class Medals
{
    public bool rescue, kill, untouched;

}

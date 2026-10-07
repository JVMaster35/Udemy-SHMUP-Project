using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public string sceneTarget, requiredLevel;

    public Button playButton;
    public Text killTxt, rescueTxt, undamagedTxt;

    public Color disableColor, enableColor;

    private Medals sceneMedal;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMenu();

        playButton.onClick.AddListener(GoToLevel);
        CheckIfUnlocked();
        playButton.GetComponentInChildren<Text>().text = (transform.GetSiblingIndex() + 1).ToString("00");
    }

    public void UpdateMenu()
    {
        if(StatsManager.instance.achievementList.ContainsKey(sceneTarget))
        {
           sceneMedal = StatsManager.instance.achievementList[sceneTarget];
        }

        if(sceneMedal != null)
        {
            killTxt.color = sceneMedal.kill ? enableColor : disableColor;
            rescueTxt.color = sceneMedal.rescue ? enableColor : disableColor;
            undamagedTxt.color = sceneMedal.untouched ? enableColor : disableColor;
        }
        else
        {
            killTxt.color = disableColor;
            rescueTxt.color = disableColor;
            undamagedTxt.color = disableColor;
        }

        CheckIfUnlocked();
    }

    void CheckIfUnlocked()
    {
        bool unlockedCondition = StatsManager.instance.levelCompleted.ContainsKey(requiredLevel) && StatsManager.instance.levelCompleted[requiredLevel];
        playButton.interactable = unlockedCondition || string.IsNullOrEmpty(requiredLevel) ? true : false;
    }

    void GoToLevel()
    {
        SceneLoader.instance.ChangeScene(sceneTarget);
    }
}

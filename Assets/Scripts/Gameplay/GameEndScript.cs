using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndScript : MonoBehaviour
{

    public Button exitButton;
    public Text killTxt, rescueTxt, undamagedTxt;
    public Color disableColor, enableColor;

    private WaitForSeconds interval = new WaitForSeconds(0.5f);

    void OnEnable()
    {
        exitButton.onClick.AddListener(BackToMenu);
        exitButton.interactable = false;

        StartCoroutine(ShowAchievements());
    }

    IEnumerator ShowAchievements()
    {
        yield return interval; 

        killTxt.color = LevelManager.instance.medals.kill ? enableColor : disableColor;

        yield return interval; 

        rescueTxt.color = LevelManager.instance.medals.rescue ? enableColor : disableColor;

        yield return interval; 

        undamagedTxt.color = LevelManager.instance.medals.untouched ? enableColor : disableColor;

        yield return interval; 

        exitButton.interactable = true;
    }

    void BackToMenu()
    {
        SceneLoader.instance.ChangeScene("Menu");
    }
}

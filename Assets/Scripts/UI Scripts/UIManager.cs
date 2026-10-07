using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIScreen[] screenArray;

    private UIScreen currentScreen, previousScreen;

    private bool isChanging;

    // Start is called before the first frame update
    void Start()
    {
        screenArray = GetComponentsInChildren<UIScreen>(true);

        Init(0);
    }

    void Init(int defaultUI)
    {
        for (int i = 0; i < screenArray.Length; i++)
        {
            if(i == defaultUI)
            {
                //activate the screen
                screenArray[i].Init(true);
                currentScreen = screenArray[i];
            }
            else
            {
                //disable the screen
                screenArray[i].Init(false);
            }
        }
    }

    // Update is called once per frame
    public void ChangeScreen(UIScreen newScreen)
    {
        if(isChanging || currentScreen == newScreen)
        {
            return;
        }

        if(newScreen)
        {
            isChanging = true;

            newScreen.DoneChange -= DoneSwitching;
            newScreen.DoneChange += DoneSwitching;

            if(currentScreen)
            {
                previousScreen = currentScreen;
                //disable the previous screen
                previousScreen.Hide();
            }

            currentScreen = newScreen;
            //show our current screen
            currentScreen.Show();
        }
    }

    void DoneSwitching()
    {
        isChanging = false;
    }
}

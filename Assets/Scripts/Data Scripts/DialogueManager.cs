using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject panel;
    public Text dialogueMessage, confirmTxt, abortTxt;
    public Button confirmBtn, abortBtn;
    
    private void Awake()
    {
        instance = this;
        panel.SetActive(false);
    }

    public void ShowDialogue(string message, UnityAction confirmAct, UnityAction abortAct = null, 
    string confirmText = "Confirm", string abortText = "Abort")
    {
        abortBtn.gameObject.SetActive(true);

        dialogueMessage.text = message;

        confirmTxt.text = confirmText;
        abortTxt.text = abortText;

        confirmBtn.onClick.RemoveAllListeners();
        abortBtn.onClick.RemoveAllListeners();

        if(abortAct != null)
        {
            abortBtn.onClick.AddListener(abortAct);
        }

        abortBtn.onClick.AddListener(DisablePanel);

        confirmBtn.onClick.AddListener(confirmAct);
        confirmBtn.onClick.AddListener(DisablePanel);

        panel.SetActive(true);
    }

    public void ShowMessage(string message)
    {
        panel.SetActive(true);
        
        abortBtn.gameObject.SetActive(false);

        dialogueMessage.text = message;

        confirmTxt.text = "Confirm";

        confirmBtn.onClick.RemoveAllListeners();

        confirmBtn.onClick.AddListener(DisablePanel);
    }

    void DisablePanel()
    {
        panel.SetActive(false);
    }
}

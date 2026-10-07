using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerUpMenu : MonoBehaviour
{
    public Button shieldBtn, lazerBtn, mbBtn;
    public Text shieldTxt, lazerTxt, mbTxt;
    public int shieldAmount = 3, lazerAmount = 3, mBAmount = 1;

    public ShieldScript shield;
    public LazerScript lazer;
    public MegaBombScript mBomb;

    private float defaultPhysicsSteps;


    // Start is called before the first frame update
    void Start()
    {
        shieldTxt.text = "S " + shieldAmount;
        lazerTxt.text = "L " + lazerAmount;
        mbTxt.text = "M " + mBAmount;

        shieldBtn.onClick.AddListener(EnableShield);
        lazerBtn.onClick.AddListener(EnableLazer);
        mbBtn.onClick.AddListener(EnableMegaBomb);

        defaultPhysicsSteps = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        BulletTime(Input.GetMouseButton(1));
        #elif UNITY_ANDROID
        BulletTime(Input.touchCount == 0 || EventSystem.current.IsPointerOverGameObject(0));
        #endif    
    }

    void BulletTime(bool slow)
    {
        if(slow)
        {
            Time.timeScale = 0.25f; 
            Time.fixedDeltaTime = defaultPhysicsSteps * Time.timeScale;
        }
        else 
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = defaultPhysicsSteps;
        }

        shieldBtn.gameObject.SetActive(slow);
        lazerBtn.gameObject.SetActive(slow);
        mbBtn.gameObject.SetActive(slow);
    }

    void EnableShield()
    {
        if(shieldAmount == 0)
        {
            return;
        }

        shield.ShieldUp();

        shieldAmount --;

        shieldTxt.text = "S " + shieldAmount;

        if(shieldAmount == 0)
        {
            shieldBtn.interactable = false;
        }
    }

        void EnableLazer()
    {
        if(lazerAmount == 0)
        {
            return;
        }

        lazer.ShootLazer();

        lazerAmount --;

        lazerTxt.text = "L " + lazerAmount;

        if(lazerAmount == 0)
        {
            lazerBtn.interactable = false;
        }
    }

        void EnableMegaBomb()
    {
        if(mBAmount == 0)
        {
            return;
        }

        mBomb.DeployBomb();

        mBAmount --;

        mbTxt.text = "M " + mBAmount;

        if(mBAmount == 0)
        {
            mbBtn.interactable = false;
        }
    }
}

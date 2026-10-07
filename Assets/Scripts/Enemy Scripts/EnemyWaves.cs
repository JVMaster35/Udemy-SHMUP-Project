using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyWaves : MonoBehaviour
{

    public int numWaves = 1;
    public float intervalBetweenEnemy = 0.5f, removeAfter = 2f, checkKillCombo = 5f;
    public List<GameObject> childs = new List<GameObject>();

    private GameObject mainChild;
    private WaitForSeconds interval, disableAfter;

    // Start is called before the first frame update
    void Start()
    {
        interval = new WaitForSeconds(intervalBetweenEnemy);
        disableAfter = new WaitForSeconds(removeAfter);
        Init();

        StartCoroutine(StartWaves());
        StartCoroutine(CheckCombo());
    }

    void Init()
    {
        mainChild = transform.GetChild(0).gameObject;
        Vector3 position = mainChild.transform.position;
        mainChild.SetActive(false);
        childs.Add(mainChild);

        for (int i = 1; i < numWaves; i++)
        {
            GameObject temp = Instantiate(mainChild, position, mainChild.transform.rotation);
            childs.Add(temp);
            childs[i].transform.SetParent(transform);
            childs[i].SetActive(false);
        }
    }

    IEnumerator StartWaves()
    {
        int i = 0;
        while (i < numWaves)
        {
            childs[i].SetActive(true);
            //Disable Child After Certain Time (Seconds)
            StartCoroutine(DisableChild(childs[i]));
            i++;
            yield return interval;
        }
    }

    IEnumerator DisableChild(GameObject gojt)
    {
        yield return disableAfter;

        if(gojt != null)
        {
            gojt.SetActive(false);
        }
    }

    IEnumerator CheckCombo()
    {
        yield return disableAfter;

        if (transform.childCount == 0)
        {
            //All Enemies Killed
            print("Enemy Combo Killed!");
        }
        else
        {
            print("Combo Chain Lost!");
        }
    }
}

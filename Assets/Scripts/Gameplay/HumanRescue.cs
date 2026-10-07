using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HumanRescue : MonoBehaviour
{
    public float rescueTime = 5f;
    public Image timerUI;
    public UnityEvent onRescued;
    public int minScore = 50, maxScore = 100;

    private GameObject player;

    private void Start()
    {
        LevelManager.instance.RegisterRescue();

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    IEnumerator Rescuing(float time)
    {
        while (time > 0)
        {
            //Rescue Code
            time -= Time.deltaTime;
            UpdateUI(time);

            if(player == null)
            {
                StopAllCoroutines();
                timerUI.fillAmount = 0f;
            }

            yield return null;
        }

        LevelManager.instance.AddRescue(Random.Range(minScore, maxScore));
        onRescued.Invoke();
        Destroy(gameObject, 1.5f);
    }

    void UpdateUI(float time)
    {
        if (timerUI != null)
        {
            float value = 1f - (time / rescueTime);
            timerUI.fillAmount = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Rescuing(rescueTime));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            timerUI.fillAmount = 0f;
        }
    }
}

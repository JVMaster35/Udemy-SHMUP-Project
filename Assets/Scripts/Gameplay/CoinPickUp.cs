using UnityEngine;
using UnityEngine.Events;

public class CoinPickUp : MonoBehaviour
{
    public UnityEvent onPickUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            PoolingManager.instance.ReturnObject(other.gameObject);
            onPickUp.Invoke();

            //To-Do: Add Coin Counter
            StatsManager.instance.AddMoney(1);
        }
    }

}

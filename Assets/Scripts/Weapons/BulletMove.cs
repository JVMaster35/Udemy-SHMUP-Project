using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    protected Rigidbody rgdby;

    // Start is called before the first frame update
    void Start()
    {
        rgdby = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rgdby.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Activator") || other.CompareTag("Deactivator"))
        {
            PoolingManager.instance.ReturnObject(this.gameObject);
        }
    }
}

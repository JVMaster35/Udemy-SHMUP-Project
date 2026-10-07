using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f, bankingValue =90f;
    public Transform visualChild;
    private Camera cam;
    private Rigidbody rgdby;
    private float distance;
    private Vector3 velocity, lastPosition, rotation, touchPos, screenToWorld;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rgdby = GetComponent<Rigidbody>();

        distance = (cam.transform.position - transform.position).y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = transform.position - lastPosition;

        Move();

        lastPosition = transform.position;
    }

    void Move()
    {
        #if UNITY_EDITOR
        touchPos = Input.mousePosition;
        #elif UNITY_ANDROID
        touchPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)touchPos;
        #endif
        touchPos.z = distance;

        screenToWorld = cam.ScreenToWorldPoint(touchPos);

        Vector3 movement = Vector3.Lerp(rgdby.position, screenToWorld, moveSpeed * Time.fixedDeltaTime);

        rgdby.MovePosition(movement);

        rotation.z = -velocity.x * bankingValue;
        rgdby.MoveRotation(Quaternion.Euler(rotation));
    }
}

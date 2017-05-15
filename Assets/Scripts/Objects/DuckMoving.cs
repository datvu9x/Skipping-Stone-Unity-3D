using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMoving : MonoBehaviour
{

    Rigidbody rb;
    float speedX = 0f;
    float speedY = 0f;
    float speedZ = 0f;
    float speed = 5f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedZ = speed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speedX, speedY, speedZ);
    }

    void OnTriggerEnter(Collider other)
    {
        print("Collisder: " + other.name);

        if (other.gameObject.name == "In Front Of Lake")
        {
            speedY = 0;
            speedZ = -speed;
            speedX = 0;
            transform.rotation = Quaternion.Euler(-90, 0, 180);
        }

        if (other.gameObject.name == "Side Lake")
        {
            speedY = 0;
            speedZ = speed;
            speedX = 0;
            transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
    }

}

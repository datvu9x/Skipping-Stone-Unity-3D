using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMoving : MonoBehaviour
{

    private Rigidbody rb;
    private const float GRAVITY = 9.82f;
    private const float PI = 3.14f;
    private bool isFalling;
    private float speedZ;
    private float speedY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Angle: " + PlayerControl.angle);
        Debug.Log("Dis: " + NetworkMenu.dis);
        Debug.Log("Ang: " + NetworkMenu.ang);
        move(NetworkMenu.dis, NetworkMenu.ang, PlayerControl.angle);
    }


    void Update()
    {
    
        if (isFalling == false)
        {
            isFalling = true;
            Vector3 v = rb.velocity;
            v.y = speedY;
            v.z = speedZ;
            rb.velocity = v;
        }

    }

    public void move(float dis, float angle, float angleThrown)
    {
        float stoneNewVelocity = dis / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / GRAVITY);

        float Vx = 0;
        float Vz = Mathf.Sqrt(stoneNewVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(stoneNewVelocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        //rb.velocity = new Vector3(Vz * Mathf.Tan(3.14f / 180f), Vy, Vz);

        transform.Rotate(0, angleThrown, 0);

        Debug.Log("Goc theo Y: " + angleThrown);
       
        if (((angleThrown >= 0) && (angleThrown <= 90)) || ((angleThrown >= 270) && (angleThrown <= 360)))
        {
            rb.velocity = new Vector3(Vz * Mathf.Tan(angleThrown * 3.14f / 180f), Vy, Vz);
        }
        else
        {
            rb.velocity = new Vector3(-Vz * Mathf.Tan(angleThrown * 3.14f / 180f), Vy, -Vz);
        }

    }

    public bool conditionToJump(float Vz, float firingAngle)
    {
        float TS = Mathf.Sqrt((16 * (rb.mass / 1000) * GRAVITY) / (PI * 1000));
        float y = 8 * (rb.mass / 1000) * Mathf.Tan(firingAngle * Mathf.Deg2Rad) * Mathf.Tan(firingAngle * Mathf.Deg2Rad);
        float z = PI * 1000 * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float MS = Mathf.Sqrt(1 - y / z);

        float result = 1000 * TS / MS;
        //Debug.Log("Vmin = " + result + " - Vz =  " + Vz);
        if (Mathf.Abs(Vz) > result)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float speedZDown(float speedPre)
    {
        float downConstant = -1.4f * 0.13f * GRAVITY * 2f * 10;
        float speedAfter = Mathf.Sqrt(downConstant + speedPre * speedPre);
        return speedAfter;
    }

    public float speedYDown(float speedZAfter, float firingAngle)
    {
        return speedZAfter * Mathf.Tan(firingAngle * Mathf.Deg2Rad);
    }

    void OnTriggerEnter(Collider other)
    {
        print("Collider: " + other.gameObject.name);

        if (other.name == "Terrain")
        {
            Destroy(gameObject);
        }

        if (conditionToJump(rb.velocity.z, 10f) == true)
        {
            if (rb.velocity.z < 0)
            {
                speedZ = -(speedZDown(Mathf.Abs(rb.velocity.z)));
                speedY = speedYDown(-speedZ, 10f);
            }
            else
            {
                speedZ = speedZDown(rb.velocity.z);
                speedY = speedYDown(speedZ, 10f);
            }

            isFalling = false;
        }
        else
        {
            isFalling = true;
        }

        if (
            other.gameObject.name == "In Front Of Lake"
            || other.gameObject.name == "Left Lake"
            || other.gameObject.name == "Right Lake"
            )
        {
            isFalling = false;
            speedZ = -speedZDown(rb.velocity.z);
            speedY = speedYDown(speedZ, 10f);
        }

    }

}

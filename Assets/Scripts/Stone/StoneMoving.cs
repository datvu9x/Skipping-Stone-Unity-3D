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

    public float angleStuff;
    public float stoneSpeed;
    public Transform stone;

    public static float angle = NetworkMenu.ang, distance = NetworkMenu.dis, angPlay = PlayerControl.angle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("WaitTimeScore");
    }

    IEnumerator WaitTimeScore()
    {
        yield return new WaitForSeconds(0.5f);

        stone.SetParent(null);
        stone.GetComponent<Rigidbody>().useGravity = true;

        move(distance, angle, angPlay);
    }

    void Update()
    {
        print("ANGLE: " + angle + "- DIS: " + distance + " -PLAYER:" + angPlay);


        if (isFalling == false)
        {
            isFalling = true;
            Vector3 v = rb.velocity;
            //v.y = speedY;
            //v.z = speedZ;
            // Xét các góc ném khác nhau d? thay d?i giá tr? lúc Update...
            if (((angleStuff >= 0) && (angleStuff <= 45)) || ((angleStuff >= 315) && (angleStuff <= 360)) || ((angleStuff >= 135) && (angleStuff <= 225)))
            {
                v.y = speedY;
                v.z = speedZ;
            }
            else
            {
                v.y = speedY;
                v.x = speedZ;
            }

            stoneSpeed = Mathf.Sqrt(speedZ * speedZ + speedY * speedY);
            rb.velocity = v;
        }

    }

    public void move(float dis, float angle, float angleThrown)
    {
        float stoneNewVelocity = dis / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / GRAVITY);

        float Vx = 0;
        float Vz = Mathf.Sqrt(stoneNewVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(stoneNewVelocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        Debug.LogWarning("V = " + stoneNewVelocity);

        transform.Rotate(0, angleThrown, 0);
        angleStuff = angleThrown;

        //if (((angleThrown >= 0) && (angleThrown <= 90)) || ((angleThrown >= 270) && (angleThrown <= 360)))
        //{
        //    rb.velocity = new Vector3(Vz * Mathf.Tan(angleThrown * 3.14f / 180f), Vy, Vz);
        //}
        //else
        //{
        //    rb.velocity = new Vector3(-Vz * Mathf.Tan(angleThrown * 3.14f / 180f), Vy, -Vz);
        //}

        // Xét các góc 45 d?c bi?t d? t?o v?n t?c c?a ḥn dá theo phuong h?p lư khi quay góc ném
        if (((angleThrown >= 0) && (angleThrown <= 45)) || ((angleThrown >= 315) && (angleThrown <= 360)))
        {
            rb.velocity = new Vector3(Vz * Mathf.Tan(angleThrown * 3.14f / 180f), Vy, Vz);
        }
        else if ((angleThrown >= 135) && (angleThrown <= 225))
        {
            rb.velocity = new Vector3(-Vz * Mathf.Tan(angleThrown * 3.14f / 180f), Vy, -Vz);
        }
        else if ((angleThrown > 45) && (angleThrown < 135))
        {
            if ((angleThrown > 45) && (angleThrown <= 90))
            {
                rb.velocity = new Vector3(Vz, Vy, Vz * Mathf.Tan((90f - angleThrown) * 3.14f / 180f));
            }
            else
            {
                rb.velocity = new Vector3(Vz, Vy, Vz * Mathf.Tan((90f - angleThrown) * 3.14f / 180f));
            }
        }
        else
        {
            if ((angleThrown > 225) && (angleThrown <= 270))
            {
                rb.velocity = new Vector3(-Vz, Vy, -Vz * Mathf.Tan((270f - angleThrown) * 3.14f / 180f));
            }
            else
            {
                rb.velocity = new Vector3(-Vz, Vy, -Vz * Mathf.Tan((270f - angleThrown) * 3.14f / 180f));
            }
        }
        Debug.LogWarning("V_1 = " + rb.velocity);

    }

    public bool conditionToJump(float Vz, float firingAngle)
    {
        float TS = Mathf.Sqrt((16 * (rb.mass / 1000) * GRAVITY) / (PI * 1000));
        float y = 8 * (rb.mass / 1000) * Mathf.Tan(firingAngle * Mathf.Deg2Rad) * Mathf.Tan(firingAngle * Mathf.Deg2Rad);
        float z = PI * 1000 * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float MS = Mathf.Sqrt(1 - y / z);

        float result = 1000 * TS / MS;

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

        if (other.gameObject.name == "Terrain")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.name == "DynamicWaterPlaneCollider")
        {
            if (PlayerControl.checkScore)
            {
                Score.ScorePlayer++;
            }
        }

        if (other.name == "Collider" || other.name == "Tube(Clone)" || other.name == "Torus(Clone)" || other.name == "Duck(Clone)")
        {
            rb.velocity = new Vector3(-(Mathf.Abs(rb.velocity.z)) * Mathf.Tan(angleStuff * 3.14f / 180f), speedY, -(Mathf.Abs(rb.velocity.z)));
        }

        //if (other.gameObject.name == "Water")
        //{

            // Xet dieu kien nay voi cac goc nem khac nhau
            

        //}
        //if (conditionToJump(rb.velocity.z, 10f) == true)
        //{
        //    if (rb.velocity.z < 0)
        //    {
        //        speedZ = -(speedZDown(Mathf.Abs(rb.velocity.z)));
        //        speedY = speedYDown(-speedZ, 10f);
        //    }
        //    else
        //    {
        //        speedZ = speedZDown(rb.velocity.z);
        //        speedY = speedYDown(speedZ, 10f);
        //    }

        //    isFalling = false;
        //}
        //else
        //{
        //    isFalling = true;
        //}

        if (((angleStuff >= 0) && (angleStuff <= 45)) || ((angleStuff >= 315) && (angleStuff <= 360)) || ((angleStuff >= 135) && (angleStuff <= 225)))
        {
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
        }
        else
        {
            if (conditionToJump(rb.velocity.x, 10f) == true)
            {
                if (rb.velocity.x < 0)
                {
                    speedZ = -(speedZDown(Mathf.Abs(rb.velocity.x)));
                    speedY = speedYDown(-speedZ, 10f);
                }
                else
                {
                    speedZ = speedZDown(rb.velocity.x);
                    speedY = speedYDown(speedZ, 10f);
                }

                isFalling = false;
            }
            else
            {
                isFalling = true;
            }
        }

        if (
            other.gameObject.name == "In Front Of Lake"
             || other.gameObject.name == "Left Lake"
            || other.gameObject.name == "Right Lake"
            || other.gameObject.name == "Left Lake 1"
            || other.gameObject.name == "Right Lake 1"
            || other.gameObject.name == "Left Lake 2"
            || other.gameObject.name == "Right Lake 3"
             || other.gameObject.name == "Side Lake"
            )
        {
            //isFalling = false;
            //speedZ = -speedZDown(rb.velocity.z);
            //speedY = speedYDown(speedZ, 10f);
            isFalling = false;
            if (((angleStuff >= 0) && (angleStuff <= 45)) || ((angleStuff >= 315) && (angleStuff <= 360)) || ((angleStuff >= 135) && (angleStuff <= 225)))
            {
                speedZ = -speedZDown(rb.velocity.z);
                speedY = speedYDown(speedZ, 10f);
            }
            else
            {
                speedZ = -speedZDown(rb.velocity.x);
                speedY = speedYDown(speedZ, 10f);
            }
        }

        

    }
}

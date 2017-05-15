using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffMoving : MonoBehaviour
{
    //public GameObject infrontofWall = null;
    public GameObject behindWall = null;
    //public GameObject leftWall = null;
    //public GameObject rightWall = null;
    private StoneMoving stoneMoving;
    public GameObject panelAddObject;

    public GameObject torus = null;
    public GameObject tube = null;
    public GameObject boat = null;
    public GameObject duck = null;

    public GameObject behindWall25 = null;
    public GameObject behindWall36 = null;
    public GameObject behindWall14 = null;
    public GameObject behindWall710 = null;
    public GameObject behindWall811 = null;
    public GameObject behindWall912 = null;

    float minHeight;
    float maxHeight;
    float minWidth;
    float maxWidth;

    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Tinh thong so de random vat the

        //if (infrontofWall != null && behindWall != null && leftWall != null && rightWall != null)
        //{
        //    if (behindWall.transform.position.z < 0)
        //    {
        //        minHeight = behindWall.transform.position.z + behindWall.transform.localScale.x / 2;
        //    }
        //    else
        //    {
        //        minHeight = behindWall.transform.position.z - behindWall.transform.localScale.x / 2;
        //    }

        //    if (infrontofWall.transform.position.z < 0)
        //    {
        //        maxHeight = infrontofWall.transform.position.z + infrontofWall.transform.localScale.x / 2;
        //    }
        //    else
        //    {
        //        maxHeight = infrontofWall.transform.position.z - infrontofWall.transform.localScale.x / 2;
        //    }

        //    if (leftWall.transform.position.x < 0)
        //    {
        //        minWidth = leftWall.transform.position.x + leftWall.transform.localScale.z / 2;
        //    }
        //    else
        //    {
        //        minWidth = leftWall.transform.position.x - leftWall.transform.localScale.z / 2;
        //    }

        //    if (rightWall.transform.position.x < 0)
        //    {
        //        maxWidth = rightWall.transform.position.x + rightWall.transform.localScale.z / 2;
        //    }
        //    else
        //    {
        //        maxWidth = rightWall.transform.position.x - rightWall.transform.localScale.z / 2;
        //    }

        Debug.Log("Chieu dai " + minHeight + "-" + maxHeight);
        Debug.Log("Chieu ngang" + minWidth + "-" + maxWidth);
        //}
    }

    public void InstantiateTube()
    {
        CountSizeToBornStuff(NetworkMenu.lakePosition);

        if (tube != null)
        {
            Network.Instantiate(tube, new Vector3(Random.Range(minWidth, maxWidth), 0, Random.Range(minHeight, maxHeight)), Quaternion.identity, 0);
            panelAddObject.gameObject.SetActive(false);
        }
    }

    public void InstantiateTorus()
    {
        CountSizeToBornStuff(NetworkMenu.lakePosition);

        if (torus != null)
        {
            Network.Instantiate(torus, new Vector3(Random.Range(minWidth, maxWidth), 0, Random.Range(minHeight, maxHeight)), Quaternion.identity, 0);
            panelAddObject.gameObject.SetActive(false);
        }
    }

    public void InstantiateBoat()
    {
        CountSizeToBornStuff(NetworkMenu.lakePosition);

        if (boat != null)
        {
            Network.Instantiate(boat, new Vector3(Random.Range(minWidth, maxWidth), 0, Random.Range(minHeight, maxHeight)), Quaternion.identity, 0);
            panelAddObject.gameObject.SetActive(false);
        }

    }

    public void InstantiateDuck()
    {
        CountSizeToBornStuff(NetworkMenu.lakePosition);

        if (duck != null)
        {
            Network.Instantiate(duck, new Vector3(Random.Range(minWidth + 10, maxWidth - 10), 0, Random.Range(minHeight + 10, maxHeight - 10)), Quaternion.Euler(-90f, 0f, 0f), 0);
            panelAddObject.gameObject.SetActive(false);
        }

    }

    //Ham tinh van toc luc sau cua vat duoc va cham 
    float countSpeedAfterTouching(float speedStuffComing, float massStuffComing, float speedThisStuff, float massThisStuff)
    {
        float speedThisStuffAfterTouching = ((2 * speedStuffComing * massStuffComing) - (massStuffComing - massThisStuff) * speedThisStuff) / (massStuffComing + massThisStuff);
        return speedThisStuffAfterTouching;
    }


    void OnTriggerEnter(Collider other)
    {
        float speedStuffAfterTouching;
        Debug.Log("Khoi hop Va cham voi " + other.gameObject.name);
        if (other.gameObject.name == "Stone(Clone)")
        {
            Debug.Log("VA_CHAM " + this.gameObject.name);
            stoneMoving = other.GetComponent<StoneMoving>();
            float angleThrown = stoneMoving.angleStuff;
            float speedStuffComing = stoneMoving.stoneSpeed;
            if (this.gameObject.name == "Tube(Clone)" || this.gameObject.name == "Torus(Clone)")
            {
                // Goi ham countSpeedAfterTouching tinh van toc cua Khuc go sau khi va cham
                //0.001f la khoi luong cua vien da
                // 0.01f la khoi luong cua khuc go
                //0f la van toc khuc go luc dau 
                speedStuffAfterTouching = countSpeedAfterTouching(speedStuffComing, 0.001f, 0f, 0.01f);

            }
            else
            {
                // Goi ham countSpeedAfterTouching tinh van toc cua Thuyen sau khi va cham
                //0.001f la khoi luong cua vien da
                // 50f la khoi luong cua Thuyen
                //0f la van toc khuc go luc dau 
                speedStuffAfterTouching = countSpeedAfterTouching(speedStuffComing, 0.001f, 0f, 50f);
            }

            // Xet chuyen dong cua cac van sau khi co van toc . Cach tinh dựa theo goc nem
            if (((angleThrown >= 0) && (angleThrown <= 90)) || ((angleThrown >= 270) && (angleThrown <= 360)))
            {
                rb.velocity = new Vector3(Mathf.Abs(speedStuffAfterTouching) * Mathf.Tan(angleThrown * 3.14f / 180f), 0, Mathf.Abs(speedStuffAfterTouching));
            }
            else
            {
                rb.velocity = new Vector3(-Mathf.Abs(speedStuffAfterTouching) * Mathf.Tan(angleThrown * 3.14f / 180f), 0, -Mathf.Abs(speedStuffAfterTouching));
            }
        }
    }

    void CountSizeToBornStuff(string lakePosition)
    {

        if (lakePosition == "2-5")
        {
            behindWall = behindWall25;
        }
        else if (lakePosition == "3-6")
        {
            behindWall = behindWall36;
        }
        else if (lakePosition == "1-4")
        {
            behindWall = behindWall14;
        }
        else if (lakePosition == "8-11")
        {
            behindWall = behindWall811;
        }
        else if (lakePosition == "7-10")
        {
            behindWall = behindWall710;
        }
        else if (lakePosition == "9-12")
        {
            behindWall = behindWall912;
        }


        if (behindWall != null)
        {
            if (behindWall.transform.position.z < 0)
            {
                minHeight = behindWall.transform.position.z + behindWall.transform.localScale.x / 2;
                maxHeight = minHeight + 80f;
                minWidth = behindWall.transform.position.x - 13f;
                maxWidth = behindWall.transform.position.x + 13f;

            }
            else
            {
                minHeight = behindWall.transform.position.z - behindWall.transform.localScale.x / 2;
                maxHeight = minHeight - 80f;
                minWidth = behindWall.transform.position.x - 13f;
                maxWidth = behindWall.transform.position.x + 13f;
            }
        }

    }

}

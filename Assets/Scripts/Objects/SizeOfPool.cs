using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeOfPool : MonoBehaviour {

    public GameObject infrontofWall = null;
    public GameObject behindWall = null;
    public GameObject leftWall = null;
    public GameObject rightWall = null;

    public float minHeight;
    public float maxHeight;
    public float minWidth;
    public float maxWidth;

    // Use this for initialization
    void Start () {
        countSizeofPool();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void countSizeofPool()
    {
        //Tinh thong so de random vat the

        if (infrontofWall != null && behindWall != null && leftWall != null && rightWall != null)
        {
            if (behindWall.transform.position.z < 0)
            {
                minHeight = behindWall.transform.position.z + behindWall.transform.localScale.x / 2;
            }
            else
            {
                minHeight = behindWall.transform.position.z - behindWall.transform.localScale.x / 2;
            }

            if (infrontofWall.transform.position.z < 0)
            {
                maxHeight = infrontofWall.transform.position.z + infrontofWall.transform.localScale.x / 2;
            }
            else
            {
                maxHeight = infrontofWall.transform.position.z - infrontofWall.transform.localScale.x / 2;
            }

            if (leftWall.transform.position.x < 0)
            {
                minWidth = leftWall.transform.position.x + leftWall.transform.localScale.z / 2;
            }
            else
            {
                minWidth = leftWall.transform.position.x - leftWall.transform.localScale.z / 2;
            }

            if (rightWall.transform.position.x < 0)
            {
                maxWidth = rightWall.transform.position.x + rightWall.transform.localScale.z / 2;
            }
            else
            {
                maxWidth = rightWall.transform.position.x - rightWall.transform.localScale.z / 2;
            }

            Debug.Log("Chieu dai " + minHeight + "-" + maxHeight);
            Debug.Log("Chieu ngang" + minWidth + "-" + maxWidth);
        }
    }
}

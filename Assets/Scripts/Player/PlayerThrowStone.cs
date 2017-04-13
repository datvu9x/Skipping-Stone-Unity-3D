using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThrowStone : MonoBehaviour
{
    private float firingAngle;
    private float distance;
    private const float gravity = 9.82f;

    private StoneJumping BallScript;

    public Transform Projectile;

    public GameObject myTransform;
    public GameObject myTransformClient1;
    public GameObject myTransformClient2;
    public GameObject myTransformClient3;
    public GameObject myTransformClient4;
    public GameObject myTransformClient5;

    void Update()
    {

        Debug.Log("MIME: " + GetComponent<NetworkView>().isMine);
        Debug.Log("POS: " + NetworkMenu.posLake);

        distance = NetworkMenu.dis;
        firingAngle = NetworkMenu.ang;

        //if (GetComponent<NetworkView>().isMine)
        //{

        if (Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log("CLIENT: F");
            GetComponent<NetworkView>().RPC("ThrowBallClient", RPCMode.All, distance, firingAngle);
        }
        //}

    }

    public void ThrowStone()
    {
        GetComponent<NetworkView>().RPC("ThrowBallClient", RPCMode.All, distance, firingAngle);
    }


    IEnumerator SimulateProjectileServer(float dis, float angle)
    {
        yield return new WaitForSeconds(1.5f);

        BallScript = Projectile.GetComponent<StoneJumping>();

        Projectile.position = new Vector3(myTransform.transform.position.x, myTransform.transform.position.y, myTransform.transform.position.z);

        Debug.Log("Position Server = " + Projectile.position);

        float stoneNewVelocity = dis / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);

        float Vz = Mathf.Sqrt(stoneNewVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(stoneNewVelocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        Debug.Log("Goc nhin cua nguoi : " + myTransform.transform.rotation.eulerAngles.y);

        BallScript.move(0, Vy, Vz, myTransform.transform.rotation.eulerAngles.y);

    }

    public IEnumerator SimulateProjectileClient(float dis, float angle)
    {
        yield return new WaitForSeconds(1.5f);

        BallScript = Projectile.GetComponent<StoneJumping>();

        float stoneNewVelocity = dis / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);

        float Vy = Mathf.Sqrt(stoneNewVelocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        if (NetworkMenu.posLake == 0)
        {
            Projectile.position = new Vector3(myTransform.transform.position.x, myTransform.transform.position.y, myTransform.transform.position.z);

            Debug.Log("Position Projectile = " + Projectile.position);

            Debug.Log("Goc nhin cua nguoi : " + myTransform.transform.rotation.eulerAngles.y);

            float Vz = (Mathf.Sqrt(stoneNewVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad));

            BallScript.move(0, Vy, Vz, myTransform.transform.rotation.eulerAngles.y);
        }
        else if (NetworkMenu.posLake == 3)
        {
            Projectile.position = new Vector3(myTransformClient3.transform.position.x, myTransformClient3.transform.position.y, myTransformClient3.transform.position.z);

            Debug.Log("Position Projectile = " + Projectile.position);

            Debug.Log("Goc nhin cua nguoi : " + myTransformClient3.transform.rotation.eulerAngles.y);

            float Vz = -(Mathf.Sqrt(stoneNewVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad));

            BallScript.move(0, Vy, Vz, myTransformClient3.transform.rotation.eulerAngles.y);
        }
        else if (NetworkMenu.posLake == 1)
        {
            Projectile.position = new Vector3(myTransformClient1.transform.position.x, myTransformClient1.transform.position.y, myTransformClient1.transform.position.z);

            Debug.Log("Position Projectile = " + Projectile.position);

            Debug.Log("Goc nhin cua nguoi : " + myTransformClient1.transform.rotation.eulerAngles.y);

            float Vz = (Mathf.Sqrt(stoneNewVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad));

            BallScript.move(0, Vy, Vz, myTransformClient1.transform.rotation.eulerAngles.y);
        }
        else if (NetworkMenu.posLake == 2)
        {
            Projectile.position = new Vector3(myTransformClient2.transform.position.x, myTransformClient2.transform.position.y, myTransformClient2.transform.position.z);

            Debug.Log("Position Projectile = " + Projectile.position);

            Debug.Log("Goc nhin cua nguoi : " + myTransformClient2.transform.rotation.eulerAngles.y);

            float Vz = -(Mathf.Sqrt(stoneNewVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad));

            BallScript.move(0, Vy, Vz, myTransformClient2.transform.rotation.eulerAngles.y);
        }
        else if (NetworkMenu.posLake == 4)
        {
            Projectile.position = new Vector3(myTransformClient4.transform.position.x, myTransformClient4.transform.position.y, myTransformClient4.transform.position.z);

            Debug.Log("Position Projectile = " + Projectile.position);

            Debug.Log("Goc nhin cua nguoi : " + myTransformClient4.transform.rotation.eulerAngles.y);

            float Vz = -(Mathf.Sqrt(stoneNewVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad));

            BallScript.move(0, Vy, Vz, myTransformClient4.transform.rotation.eulerAngles.y);
        }
        else if (NetworkMenu.posLake == 5)
        {
            Projectile.position = new Vector3(myTransformClient5.transform.position.x, myTransformClient5.transform.position.y, myTransformClient5.transform.position.z);

            Debug.Log("Position Projectile = " + Projectile.position);

            Debug.Log("Goc nhin cua nguoi : " + myTransformClient5.transform.rotation.eulerAngles.y);

            float Vz = (Mathf.Sqrt(stoneNewVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad));

            BallScript.move(0, Vy, Vz, myTransformClient5.transform.rotation.eulerAngles.y);
        }

    }

    [RPC]
    public void ThrowBallServer(float distance, float angle)
    {
        StartCoroutine(SimulateProjectileServer(distance, angle));
    }

    [RPC]
    public void ThrowBallClient(float distance, float angle)
    {
        StartCoroutine(SimulateProjectileClient(distance, angle));
    }

}

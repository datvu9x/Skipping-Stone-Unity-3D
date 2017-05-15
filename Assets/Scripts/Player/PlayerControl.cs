using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour
{

    public GameObject stone;

    public Transform tempHand;
    private Transform model;
    private Transform cam;
    private Animator anim;

    public static float angle;
    public static bool isSendCount = false;
    public static bool checkScore = false;

    public static ArrayList arrayScore = new ArrayList();

    void OnEnable()
    {
        EasyJoystick.On_JoystickMove += On_JoystickMove;
        EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
    }

    void OnDisable()
    {
        EasyJoystick.On_JoystickMove -= On_JoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
    }

    void OnDestroy()
    {
        EasyJoystick.On_JoystickMove -= On_JoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
    }

    void Start()
    {
        model = transform.FindChild("Model").transform;
        cam = transform.FindChild("CameraAxis").transform;
        anim = model.GetComponent<Animator>();

        if (GetComponent<NetworkView>().isMine)
        {
            cam.gameObject.SetActive(true);
        }
        else
        {
            cam.gameObject.SetActive(false);
        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GetComponent<NetworkView>().isMine)
            {
                angle = transform.rotation.eulerAngles.y;
                
                Debug.Log("Goc nhin cua nguoi: " + angle);

                GetComponent<AudioSource>().Play();
                GetComponent<NetworkView>().RPC("ThrowStone", RPCMode.All, NetworkMenu.dis, NetworkMenu.ang, angle);
                checkScore = true;
                StartCoroutine("WaitTimeScore");
            }
        }
    }

    void Throw()
    {
        if (GetComponent<NetworkView>().isMine)
        {
            angle = transform.rotation.eulerAngles.y;

            Debug.Log("Goc nhin cua nguoi: " + angle);

            GetComponent<AudioSource>().Play();
            GetComponent<NetworkView>().RPC("ThrowStone", RPCMode.All, NetworkMenu.dis, NetworkMenu.ang, angle);
            checkScore = true;
            StartCoroutine("WaitTimeScore");
        }
    }

    IEnumerator WaitTimeScore()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);

        GetComponent<NetworkView>().RPC("SendCount", RPCMode.All, Score.CountPlay, NetworkMenu.name, Score.ScorePlayer);
        isSendCount = true;
    }


    [RPC]
    public void SendCount(int count, string name, int score)
    {
        Score.CountPlay++;
        count = Score.CountPlay;
        Score.CountPlay = 0;
        Score.ScorePlayer = 0;

        print("CountPlay: " + count);
        print("UserName: " + name);
        print("ScorePlayer: " + score);

        bool a = false;
        foreach (HighScore tmpScore in arrayScore)
        {
            print("Name: " + tmpScore.Name);
            if (tmpScore.Name == name)
            {
                tmpScore.Count = tmpScore.Count + count;
                tmpScore.Score = tmpScore.Score + score;
                a = true;
            }

        }
        if (a == false)
        {
            arrayScore.Add(new HighScore(name, count, score, DateTime.Today));
        }
        print("SIZE_MAP: " + arrayScore.Count);
    }


    void On_JoystickMove(MovingJoystick move)
    {
        if (GetComponent<NetworkView>().isMine)
        {
            if (move.joystickName == "MoveTurnJoystick")
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
        }

    }

    void On_JoystickMoveEnd(MovingJoystick move)
    {
        if (GetComponent<NetworkView>().isMine)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
        }

    }

    [RPC]
    public void ThrowStone(float dis, float ang, float anglePlayer)
    {
        anim.SetTrigger("isThrow");

        GameObject tmpObject = Instantiate(stone, tempHand.position, Quaternion.Euler(45f,0f, 35f));
        tmpObject.transform.SetParent(tempHand);

        StoneMoving.angle = ang;
        StoneMoving.distance = dis;
        StoneMoving.angPlay = anglePlayer;

        print("ANGLE1: " + ang + "- DIS1: " + dis + " -PLAYER1:" + anglePlayer + "-" + angle);

        anim.SetBool("isIdle", true);
    }
}

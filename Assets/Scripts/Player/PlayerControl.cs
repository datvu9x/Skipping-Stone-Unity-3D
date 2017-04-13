using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    public GameObject stone;

    private Transform model;
    private Transform point;
    private Animator anim;

    public static float angle;

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
        point = transform.FindChild("Point").transform;
        anim = model.GetComponent<Animator>();
    }

    void Throw()
    {
        print("isMIme: " + GetComponent<NetworkView>().isMine);
        GetComponent<NetworkView>().RPC("ThrowStone", RPCMode.All, null);
    }


    void On_JoystickMove(MovingJoystick move)
    {
        if (move.joystickName == "MoveTurnJoystick")
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
        }
    }

    void On_JoystickMoveEnd(MovingJoystick move)
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isIdle", true);
    }

    [RPC]
    public void ThrowStone()
    {
        anim.SetTrigger("isThrow");
        angle = transform.rotation.eulerAngles.y;

        Debug.Log("Goc nhin cua nguoi: " + angle);

        Instantiate(stone, point.transform.position, point.rotation);
    }

}

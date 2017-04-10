using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class User
{
    public string ip;
}

public class NetworkMenu : MonoBehaviour
{
    public InputField IPInput;
    public InputField PortInput;
    public Text Angle;
    public Text Distance;
    public Text ErrorText;
    public Text NameUser;
    public Button BtnServer;
    public Button BtnClient1;
    public Button BtnClient2;
    public Button BtnClient3;
    public Button BtnClient4;
    public Button BtnClient5;
    public GameObject panelListUser;

    public List<User> userList;
    public GameObject userObj;
    public Transform panelList;

    public Canvas canvasConnection;
    public Canvas canvasMain;
    public Canvas canvasSelectLake;

    public Transform objectClient5;
    public Transform objectClient1;
    public Transform objectClient2;
    public Transform objectClient3;
    public Transform objectClient4;
    public Transform objectClient;

    public Transform character;
    public Transform characterClient1;
    public Transform characterClient2;
    public Transform characterClient3;
    public Transform characterClient4;
    public Transform characterClient5;

    public Transform terrain;

    public Camera camServer;
    public Camera camClient;
    public Camera camConnection;

    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;

    public GameObject lake0;
    public GameObject lake1;
    public GameObject lake2;
    public GameObject lake3;
    public GameObject lake4;
    public GameObject lake5;

    public GameObject inFrontOf0;
    public GameObject inFrontOf1;
    public GameObject inFrontOf2;
    public GameObject inFrontOf3;
    public GameObject inFrontOf4;
    public GameObject inFrontOf5;

    public GameObject objGround0;
    public GameObject objGround1;
    public GameObject objGround2;
    public GameObject objGround3;
    public GameObject objGround4;
    public GameObject objGround5;

    private string ip;
    private int port;
    public static int numConnections = 10;
    private bool connected = false;
    private bool mode = false;

    public static int posLake = -1;
    public static float dis = 0;
    public static float ang = 0;

    private const float MAX_ANGLE = 90.0f;
    private const float MIN_ANGLE = 1.0f;
    private const float MAX_DISTANCE = 1000.0f;
    private const float MIN_DISTANCE = 5.0f;
    private const float DELTA = 1.0f;

    private void Start()
    {
        IPInput.text = "127.0.0.1";
        PortInput.text = "8080";
        Distance.text = "30";
        Angle.text = "15";
    }

    public void ClientConnect_ConnectPressed()
    {
        if (!IPInput.text.Trim().Equals(""))
        {
            if (!PortInput.text.Trim().Equals(""))
            {
                ip = IPInput.text;
                int.TryParse(PortInput.text, out port);
                Network.Connect(ip, port);
                ErrorText.gameObject.SetActive(false);
            }
            else
            {
                ErrorText.gameObject.SetActive(true);
                ErrorText.text = "You have not entered port";
            }
        }
        else
        {
            ErrorText.gameObject.SetActive(true);
            ErrorText.text = "You have not entered ip";
        }
    }

    public void ServerConnect_ServerPressed()
    {
        if (!IPInput.text.Trim().Equals(""))
        {
            if (!PortInput.text.Trim().Equals(""))
            {
                ip = IPInput.text;
                int.TryParse(PortInput.text, out port);
                Network.InitializeServer(numConnections, port, true);
                ErrorText.gameObject.SetActive(false);
            }
            else
            {
                ErrorText.gameObject.SetActive(true);
                ErrorText.text = "You have not entered port";
            }
        }
        else
        {
            ErrorText.gameObject.SetActive(true);
            ErrorText.text = "You have not entered ip";
        }
    }

    public void Logout()
    {
        Debug.Log("Logout");

        connected = false;

        GetComponent<NetworkView>().RPC("RemoveUser", RPCMode.All, Network.player.ipAddress);
        GetComponent<NetworkView>().RPC("DestroyObject", RPCMode.All, posLake);

        camConnection.gameObject.SetActive(true);
        camClient.gameObject.SetActive(false);
        canvasMain.gameObject.SetActive(false);
        canvasConnection.gameObject.SetActive(true);

        Network.Disconnect();
    }

    public void ListUserClick()
    {
        panelListUser.SetActive(true);

        foreach (Transform child in panelList)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (User user in userList)
        {
            Debug.Log("USER: " + user.ip);
            GameObject newUser = Instantiate(userObj) as GameObject;
            ButtonScript buttonScript = newUser.GetComponent<ButtonScript>();
            buttonScript.nameLabel.text = user.ip;

            newUser.transform.SetParent(panelList);
        }
    }

    public void CloseListUser()
    {
        panelListUser.SetActive(false);
    }

    public void ClearError()
    {
        ErrorText.text = "";
        ErrorText.gameObject.SetActive(false);
    }

    public void Lake0Click()
    {
        //BtnServer.image.color = new Color(230, 255, 0);
        //BtnClient4.image.color = new Color(255, 255, 255);
        //BtnClient5.image.color = new Color(255, 255, 255);
        //BtnClient3.image.color = new Color(255, 255, 255);
        //BtnClient2.image.color = new Color(255, 255, 255);
        //BtnClient1.image.color = new Color(255, 255, 255);
        posLake = 0;
        Debug.Log("POS_LAKE = " + posLake);
        InitMap();
    }

    public void Lake1Click()
    {
        posLake = 1;
        Debug.Log("POS_LAKE = " + posLake);
        InitMap();
    }

    public void Lake2Click()
    {
        posLake = 2;
        Debug.Log("POS_LAKE = " + posLake);
        InitMap();
    }

    public void Lake3Click()
    {
        posLake = 3;
        Debug.Log("POS_LAKE = " + posLake);
        InitMap();
    }

    public void Lake4Click()
    {
        posLake = 4;
        Debug.Log("POS_LAKE = " + posLake);
        InitMap();
    }

    public void Lake5Click()
    {
        posLake = 5;
        Debug.Log("POS_LAKE = " + posLake);
        InitMap();
    }

    private void InitMap()
    {
        camServer.gameObject.SetActive(false);
        canvasSelectLake.gameObject.SetActive(false);
        camClient.gameObject.SetActive(true);
        canvasMain.gameObject.SetActive(true);

        GetComponent<NetworkView>().RPC("AddUser", RPCMode.All, null);

        NameUser.text = "User: " + Network.player.ipAddress;

        connected = true;

        GetComponent<NetworkView>().RPC("CreateObject", RPCMode.All, posLake);
        GetComponent<NetworkView>().RPC("SpawnBallClient", RPCMode.All, posLake);

        if (posLake == 0)
        {
            camClient.transform.position = new Vector3(19.7f, 5.68f, -35.72f);
            camClient.transform.rotation = Quaternion.Euler(new Vector3(17, 0, 0));

            //Network.Instantiate(Resources.Load("Prefabs/Lake 1"), objectClient3.position, Quaternion.Euler(0f, 180f, 0f), 0);
            //Network.Instantiate(Resources.Load("Prefabs/Lake 1"), objectClient.position, Quaternion.identity, 0);

            //SpawnBallRPCClient(posLake);
        }
        else if (posLake == 3)
        {
            camClient.transform.position = new Vector3(20.2f, 5.9f, 169.5f);
            camClient.transform.rotation = Quaternion.Euler(new Vector3(17, 180, 0));

            //Network.Instantiate(Resources.Load("Prefabs/Lake 1"), objectClient3.position, Quaternion.Euler(0f, 180f, 0f), 0);
            //Network.Instantiate(Resources.Load("Prefabs/Lake 1"), objectClient.position, Quaternion.identity, 0);

            //SpawnBallRPCClient(posLake);
        }
        else if (posLake == 1)
        {

            camClient.transform.position = new Vector3(-13.2f, 5.68f, -35.72f);
            camClient.transform.rotation = Quaternion.Euler(new Vector3(17, 0, 0));

            //Network.Instantiate(Resources.Load("Prefabs/Lake"), objectClient1.position, Quaternion.identity, 0);
            //SpawnBallRPCClient(posLake);
        }
        else if (posLake == 2)
        {
            camClient.transform.position = new Vector3(-13f, 5.9f, 169.5f);
            camClient.transform.rotation = Quaternion.Euler(new Vector3(17, 180, 0));
            //Network.Instantiate(Resources.Load("Prefabs/Lake"), objectClient2.position, Quaternion.identity, 0);
            //SpawnBallRPCClient(posLake);
        }
        else if (posLake == 5)
        {
            camClient.transform.position = new Vector3(55.8f, 5.68f, -35.72f);
            camClient.transform.rotation = Quaternion.Euler(new Vector3(17, 0, 0));
            //Network.Instantiate(Resources.Load("Prefabs/Lake 1"), objectClient3.position, Quaternion.Euler(0f, 180f, 0f), 0);
            //SpawnBallRPCClient(posLake);
        }
        else if (posLake == 4)
        {
            camClient.transform.position = new Vector3(54.2f, 5.9f, 169.5f);
            camClient.transform.rotation = Quaternion.Euler(new Vector3(17, 180, 0));
            //Network.Instantiate(Resources.Load("Prefabs/Lake 1"), objectClient4.position, Quaternion.Euler(0f, 180f, 0f), 0);
            //SpawnBallRPCClient(posLake);
        }
    }

    private void OnConnectedToServer()
    {
        camConnection.gameObject.SetActive(false);
        camServer.gameObject.SetActive(true);
        canvasSelectLake.gameObject.SetActive(true);
        canvasConnection.gameObject.SetActive(false);
    }

    private void OnServerInitialized()
    {
        camConnection.gameObject.SetActive(false);
        camServer.gameObject.SetActive(true);
        canvasSelectLake.gameObject.SetActive(true);
        canvasConnection.gameObject.SetActive(false);
    }

    public void ModeView()
    {
        Debug.Log("ModeView");
        if (mode == false)
        {
            camClient.gameObject.SetActive(false);
            camServer.gameObject.SetActive(true);
            mode = true;
        }
        else
        {
            camClient.gameObject.SetActive(true);
            camServer.gameObject.SetActive(false);
            mode = false;
        }

    }

    private void OnPlayerDisconnected(NetworkPlayer player)
    {
    }

    private void OnDisconnectedFromServer()
    {
        
    }

    private void OnGUI()
    {

        if (connected)
        {
            float.TryParse(Angle.text.ToString(), out ang);
            float.TryParse(Distance.text.ToString(), out dis);

            if (Input.GetKeyDown(KeyCode.L))
            {
                dis += DELTA;
                if (dis >= MAX_DISTANCE)
                    dis = MAX_DISTANCE;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                dis -= DELTA;
                if (dis <= MIN_DISTANCE)
                    dis = MIN_DISTANCE;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                ang += DELTA;
                if (ang >= MAX_ANGLE)
                    ang = MAX_ANGLE;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                ang -= DELTA;
                if (ang <= MIN_ANGLE)
                    ang = MIN_ANGLE;
            }

            Angle.text = ang + "";
            Distance.text = dis + "";
        }

    }

    public void SpawnBallRPCServer()
    {
        GetComponent<NetworkView>().RPC("SpawnBallServer", RPCMode.All);
    }

    public void SpawnBallRPCClient(int position)
    {
    }

    [RPC]
    public void AddUser()
    {
        User user = new User();
        user.ip = Network.player.ipAddress;
        userList.Add(user);
    }

    [RPC]
    public void RemoveUser(string ipUser)
    {
        foreach (User user in userList)
        {
            if (user.ip.Equals(ipUser))
            {
                userList.Remove(user);
            }
        }

    }

    [RPC]
    public void DestroyObject(int position)
    {
        if (position == 0)
        {
            objGround0.SetActive(true);
            lake0.SetActive(false);
            player0.SetActive(false);
        }
        else if (position == 1)
        {
            objGround1.SetActive(true);
            lake1.SetActive(false);
            player1.SetActive(false);
        }
        else if (position == 2)
        {
            objGround2.SetActive(true);
            lake2.SetActive(false);
            player2.SetActive(false);
        }
        else if (position == 3)
        {
            objGround3.SetActive(true);
            lake3.SetActive(false);
            player3.SetActive(false);
        }
        else if (position == 4)
        {
            objGround4.SetActive(true);
            lake4.SetActive(false);
            player4.SetActive(false);
        }
        else if (position == 5)
        {
            objGround5.SetActive(true);
            lake5.SetActive(false);
            player5.SetActive(false);
        }
    }

    [RPC]
    public void CreateObject(int position)
    {
        if (position == 0)
        {
            objGround0.SetActive(false);
            lake0.SetActive(true);
        }
        else if (position == 1)
        {
            objGround1.SetActive(false);
            lake1.SetActive(true);
        }
        else if (position == 2)
        {
            objGround2.SetActive(false);
            lake2.SetActive(true);
            inFrontOf1.SetActive(false);
            inFrontOf2.SetActive(false);
        }
        else if (position == 3)
        {
            objGround3.SetActive(false);
            lake3.SetActive(true);
            objGround0.SetActive(false);
            lake0.SetActive(true);
            inFrontOf0.SetActive(false);
            inFrontOf3.SetActive(false);
        }
        else if (position == 4)
        {
            objGround4.SetActive(false);
            lake4.SetActive(true);
        }
        else if (position == 5)
        {
            objGround5.SetActive(false);
            lake5.SetActive(true);
            inFrontOf4.SetActive(false);
            inFrontOf5.SetActive(false);
        }
    }


    [RPC]
    void SpawnBallClient(int index)
    {

        if (index == 0)
        {
            player0.transform.position = new Vector3(20f, 1f, -20f);
            player0.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            player0.SetActive(true);
            Network.Instantiate(Resources.Load("Prefabs/Sphere"), character.position + new Vector3(10, 0, 0), Quaternion.identity, 0);
        }
        else if (index == 1)
        {
            player1.transform.position = new Vector3(-13f, 1f, -20f);
            player1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            player1.SetActive(true);
            Network.Instantiate(Resources.Load("Prefabs/Sphere"), characterClient1.position + new Vector3(10, 0, 0), Quaternion.identity, 0);
        }
        else if (index == 2)
        {
            player2.transform.position = new Vector3(-13f, 1f, 156f);
            player2.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            player2.SetActive(true);
            Network.Instantiate(Resources.Load("Prefabs/Sphere"), characterClient2.position, Quaternion.identity, 0);
        }
        else if (index == 3)
        {
            player3.transform.position = new Vector3(20f, 1f, 156f);
            player3.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            player3.SetActive(true);
            Network.Instantiate(Resources.Load("Prefabs/Sphere"), characterClient3.position - new Vector3(10, 0, 0), Quaternion.identity, 0);
        }
        else if (index == 4)
        {
            player4.transform.position = new Vector3(54f, 1f, 156f);
            player4.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            player4.SetActive(true);
            Network.Instantiate(Resources.Load("Prefabs/Sphere"), characterClient4.position, Quaternion.identity, 0);
        }
        else if (index == 5)
        {
            player5.transform.position = new Vector3(56f, 1f, -21f);
            player4.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            player5.SetActive(true);
            Network.Instantiate(Resources.Load("Prefabs/Sphere"), characterClient5.position, Quaternion.identity, 0);
        }
    }
}

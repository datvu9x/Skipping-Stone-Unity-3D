using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class User
{
    public string name;
    public string ip;
}

public class NetworkMenu : MonoBehaviour
{
    public InputField UserInput;
    public InputField IPInput;
    public InputField PortInput;
    public Text Angle;
    public Text Distance;
    public Text ErrorText;
    public Text NameUser;
    public Text lakeSelected;
    public Text device;
    public Slider SliderAngle;
    public Slider SliderDistance;
    public Button btnListUser;

    public GameObject panelMobile;
    public GameObject panelPC;
    public GameObject dragPC;
    public GameObject dragMobile;

    public GameObject panelListUser;
    public GameObject userObj;
    public Transform panelList;
    public List<User> userList;

    public Canvas canvasConnection;
    public Canvas canvasMain;
    public Canvas canvasSelectLake;

    public Camera camServer;
    //public Camera camClient;
    public Camera camConnection;

    public Transform terrain;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject player6;
    public GameObject player7;
    public GameObject player8;
    public GameObject player9;
    public GameObject player10;
    public GameObject player11;
    public GameObject player12;

    public GameObject lakePC1;
    public GameObject lakePC2;
    public GameObject lakePC3;
    public GameObject lakePC4;
    public GameObject lakePC5;
    public GameObject lakePC6;

    public GameObject lakeMobile1;
    public GameObject lakeMobile2;
    public GameObject lakeMobile3;
    public GameObject lakeMobile4;
    public GameObject lakeMobile5;
    public GameObject lakeMobile6;
    public GameObject lakeMobile7;
    public GameObject lakeMobile8;
    public GameObject lakeMobile9;
    public GameObject lakeMobile10;
    public GameObject lakeMobile11;
    public GameObject lakeMobile12;

    public GameObject inFrontOf0;
    public GameObject inFrontOf1;
    public GameObject inFrontOf2;
    public GameObject inFrontOf3;
    public GameObject inFrontOf4;
    public GameObject inFrontOf5;

    public GameObject objGround1;
    public GameObject objGround2;
    public GameObject objGround3;
    public GameObject objGround4;
    public GameObject objGround5;
    public GameObject objGround6;
    public GameObject objGround7;
    public GameObject objGround8;
    public GameObject objGround9;
    public GameObject objGround10;
    public GameObject objGround11;
    public GameObject objGround12;

    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;
    public Image slot5;
    public Image slot6;
    public Image slot7;
    public Image slot8;
    public Image slot9;
    public Image slot10;
    public Image slot11;
    public Image slot12;

    public Image slot14;
    public Image slot25;
    public Image slot36;
    public Image slot710;
    public Image slot811;
    public Image slot912;

    public string[] arrLakeSelected;
    public string[] arrLakeSelectedPC;

    private string ip;
    private int port;
    private string name;
    private static int numConnections = 10;
    private bool connected = false;
    private bool mode = false;
    private float screenSize;
    private int curElement;
    private int curElementPC;

    private string typeDevice;
    private string lakePosition;

    public static int posLake = -1;
    public static float dis = 0;
    public static float ang = 0;

    private const float MAX_ANGLE = 90.0f;
    private const float MIN_ANGLE = 1.0f;
    private const float MAX_DISTANCE = 100.0f;
    private const float MIN_DISTANCE = 1.0f;
    private const float DELTA = 1.0f;

    private void Start()
    {
        UserInput.text = "DatVIT";
        IPInput.text = "127.0.0.1";
        PortInput.text = "8080";
        Distance.text = "30";
        Angle.text = "15";
        SliderAngle.maxValue = MAX_ANGLE;
        SliderDistance.maxValue = MAX_DISTANCE;
        SliderDistance.minValue = MIN_DISTANCE;
        SliderAngle.minValue = MIN_ANGLE;

        float.TryParse(Angle.text.ToString(), out ang);
        float.TryParse(Distance.text.ToString(), out dis);

        SliderDistance.value = dis;
        SliderAngle.value = ang;

        print("ScreenSize: " + Screen.width + "x" + Screen.height);

        screenSize = Mathf.Max(Screen.width, Screen.height);

        arrLakeSelected = new string[12];
        arrLakeSelectedPC = new string[12];

        curElement = 0;
    }

    private void ConnectGame(int type)
    {
        if (!UserInput.text.Trim().Equals(""))
        {
            if (!IPInput.text.Trim().Equals(""))
            {
                if (!PortInput.text.Trim().Equals(""))
                {
                    ip = IPInput.text;
                    int.TryParse(PortInput.text, out port);
                    name = UserInput.text;
                    if (type == 1)
                    {
                        Network.Connect(ip, port);
                        btnListUser.gameObject.SetActive(false);

                    }
                    else if (type == 0)
                    {
                        Network.InitializeServer(numConnections, port, true);
                    }
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
                ErrorText.text = "You have not entered username";
            }
        }
        else
        {
            ErrorText.gameObject.SetActive(true);
            ErrorText.text = "You have not entered ip";
        }
    }

    public void ClientConnect_ConnectPressed()
    {
        ConnectGame(1);
    }

    public void ServerConnect_ServerPressed()
    {
        ConnectGame(0);
    }

    public void Logout()
    {
        Debug.Log("Logout");

        connected = false;

        GetComponent<NetworkView>().RPC("RemoveUser", RPCMode.All, Network.player.ipAddress, name);
        GetComponent<NetworkView>().RPC("DestroyObject", RPCMode.All, lakePosition, typeDevice);

        camConnection.gameObject.SetActive(true);
        //camClient.gameObject.SetActive(false);
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
            Debug.Log("USER: " + user.name + "-" + user.ip);
            GameObject newUser = Instantiate(userObj) as GameObject;
            ButtonScript buttonScript = newUser.GetComponent<ButtonScript>();
            buttonScript.nameLabel.text = user.name;
            buttonScript.ipLabel.text = user.ip;

            buttonScript.btnDel.onClick.AddListener(delegate
            {
                DisconnectPlayer(user.ip, user.name);
            });

            newUser.transform.SetParent(panelList);
        }
    }

    public void DisconnectPlayer(string ip, string name)
    {
        if (Network.player.ipAddress.Equals(ip))
        {
            Network.CloseConnection(Network.player, true);
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

    private void InitMap()
    {
        connected = true;

        camConnection.gameObject.SetActive(false);
        canvasSelectLake.gameObject.SetActive(false);
        //camClient.gameObject.SetActive(true);
        canvasMain.gameObject.SetActive(true);

        NameUser.text = "User: " + name + " - " + Network.player.ipAddress;

        print("Lake selected: " + lakeSelected.text);

        typeDevice = lakeSelected.text.Substring(0, device.text.Length);
        lakePosition = lakeSelected.text.Substring(lakeSelected.text.IndexOf("@") + 1)
            .Replace("Slot", "").Trim();
        string[] listPos = lakePosition.Split(new string[] { "-" }, 2, 0);

        GetComponent<NetworkView>().RPC("AddUser", RPCMode.All, Network.player.ipAddress, name);
        GetComponent<NetworkView>().RPC("AddLake", RPCMode.All, typeDevice, listPos);
        GetComponent<NetworkView>().RPC("CreateObject", RPCMode.All, typeDevice);
        GetComponent<NetworkView>().RPC("SpawnBallClient", RPCMode.All, lakePosition);
    }

    private void OnConnectedToServer()
    {
        Connect();
    }

    private void OnServerInitialized()
    {
        Connect();
    }

    private void Connect()
    {
        canvasSelectLake.gameObject.SetActive(true);
        canvasConnection.gameObject.SetActive(false);

        if (screenSize > 1000)
        {
            panelPC.SetActive(true);
            dragPC.SetActive(true);
            panelMobile.SetActive(false);
            dragMobile.SetActive(false);
            device.text = "PC";

            foreach (string i in arrLakeSelectedPC)
            {
                print("SelectedPC_Connect: " + i);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "1") > -1)
            {
                slot14.color = new Color(255, 0, 0);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "2") > -1)
            {
                slot25.color = new Color(255, 0, 0);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "3") > -1)
            {
                slot36.color = new Color(255, 0, 0);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "7") > -1)
            {
                slot710.color = new Color(255, 0, 0);
            }
            if (Array.IndexOf(arrLakeSelectedPC, "8") > -1)
            {
                slot811.color = new Color(255, 0, 0);
            }
            if (Array.IndexOf(arrLakeSelectedPC, "9") > -1)
            {
                slot912.color = new Color(255, 0, 0);
            }
        }
        else
        {
            panelMobile.SetActive(true);
            dragMobile.SetActive(true);
            panelPC.SetActive(false);
            dragPC.SetActive(false);
            device.text = "Mobile";

            foreach (string i in arrLakeSelected)
            {
                print("Selected_Connect: " + i);
            }

            if (Array.IndexOf(arrLakeSelected, "1") > -1)
            {
                slot1.color = new Color(144, 255, 0);
            }

            if (Array.IndexOf(arrLakeSelected, "2") > -1)
            {
                slot2.color = new Color(144, 255, 0);
            }

            if (Array.IndexOf(arrLakeSelected, "3") > -1)
            {
                slot3.color = new Color(144, 255, 0);
            }
            if (Array.IndexOf(arrLakeSelected, "4") > -1)
            {
                slot4.color = new Color(144, 255, 0);
            }
            if (Array.IndexOf(arrLakeSelected, "5") > -1)
            {
                slot5.color = new Color(144, 255, 0);
            }
            if (Array.IndexOf(arrLakeSelected, "6") > -1)
            {
                slot6.color = new Color(144, 255, 0);
            }
            if (Array.IndexOf(arrLakeSelected, "7") > -1)
            {
                slot7.color = new Color(144, 255, 0);
            }
            if (Array.IndexOf(arrLakeSelected, "8") > -1)
            {
                slot8.color = new Color(144, 255, 0);
            }
            if (Array.IndexOf(arrLakeSelected, "9") > -1)
            {
                slot9.color = new Color(144, 255, 0);
            }
            if (Array.IndexOf(arrLakeSelected, "10") > -1)
            {
                slot10.color = new Color(144, 255, 0);
            }
            if (Array.IndexOf(arrLakeSelected, "11") > -1)
            {
                slot12.color = new Color(144, 255, 0);
            }
            if (Array.IndexOf(arrLakeSelected, "12") > -1)
            {
                slot12.color = new Color(144, 255, 0);
            }

        }

        lakeSelected.text = "";
    }

    public void SelectLake()
    {
        InitMap();
    }

    public void ModeView()
    {
        Debug.Log("ModeView");
        if (mode == false)
        {
            //camClient.gameObject.SetActive(false);
            camServer.gameObject.SetActive(true);
            mode = true;
        }
        else
        {
            //camClient.gameObject.SetActive(true);
            camServer.gameObject.SetActive(false);
            mode = false;
        }

    }

    private void OnPlayerDisconnected(NetworkPlayer player)
    {

    }

    private void OnPlayerConnected(NetworkPlayer player)
    {

    }

    private void OnDisconnectedFromServer()
    {

    }

    public void SliderDistance_Changed()
    {
        dis = SliderDistance.value;
    }

    public void SliderAngle_Changed()
    {
        ang = SliderAngle.value;
    }

    private void Update()
    {

        Distance.text = dis + "";
        Angle.text = ang + "";

        print("ScreenSize: " + Screen.width + "x" + Screen.height);
    }

    private void OnGUI()
    {
        if (connected)
        {
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
        }

        if (Network.isServer)
        {
            Debug.Log("Server Send: " + curElement + "-" + curElementPC);
            GetComponent<NetworkView>().RPC("SendListLake", RPCMode.All, arrLakeSelected, arrLakeSelectedPC, curElement, curElementPC);
        }

    }

    [RPC]
    public void SendListLake(string[] arr, string[] arrPC, int cur, int curPC)
    {
        this.arrLakeSelectedPC = arrPC;
        this.arrLakeSelected = arr;
        this.curElement = cur;
        this.curElementPC = curPC;
    }

    [RPC]
    public void AddUser(string ip, string nameUser)
    {
        Debug.Log("AddUser: " + nameUser);
        User user = new User();
        user.ip = ip;
        user.name = nameUser;
        userList.Add(user);
    }

    [RPC]
    public void RemoveUser(string ipUser, string name)
    {
        Debug.Log("RemoveUser: " + name);
        foreach (User user in userList)
        {
            if (user.ip.Equals(ipUser) && user.name.Equals(name))
            {
                userList.Remove(user);
            }
        }

    }

    [RPC]
    public void AddLake(string type, string[] list)
    {
        if (type.Equals("PC"))
        {
            for (int i = 0; i < list.Length; i++)
            {
                curElementPC++;
                arrLakeSelectedPC[curElementPC] = list[i];
            }

            foreach (string i in arrLakeSelectedPC)
            {
                print("SelectedPC_AddLake: " + i);
            }
        }
        else
        {
            for (int i = 0; i < list.Length; i++)
            {
                curElement++;
                arrLakeSelected[curElement] = list[i];
            }

            foreach (string i in arrLakeSelected)
            {
                print("Selected_AddLake: " + i);
            }
        }
    }

    [RPC]
    public void CreateObject(string type)
    {
        if (type.Equals("PC"))
        {
            if (Array.IndexOf(arrLakeSelectedPC, "1") > -1)
            {
                print("Lake 1");
                objGround1.SetActive(false);
                objGround4.SetActive(false);
                lakePC1.SetActive(true);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "2") > -1)
            {
                print("Lake 2");
                objGround2.SetActive(false);
                objGround5.SetActive(false);
                lakePC2.SetActive(true);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "3") > -1)
            {
                print("Lake 3");
                objGround3.SetActive(false);
                objGround6.SetActive(false);
                lakePC3.SetActive(true);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "7") > -1)
            {
                print("Lake 4");
                objGround7.SetActive(false);
                objGround10.SetActive(false);
                lakePC4.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelectedPC, "8") > -1)
            {
                print("Lake 5");
                objGround8.SetActive(false);
                objGround11.SetActive(false);
                lakePC5.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelectedPC, "9") > -1)
            {
                print("Lake 6");
                objGround9.SetActive(false);
                objGround12.SetActive(false);
                lakePC6.SetActive(true);
            }
        }
        else
        {
            if (Array.IndexOf(arrLakeSelected, "1") > -1)
            {
                objGround1.SetActive(false);
                lakeMobile1.SetActive(true);
            }

            if (Array.IndexOf(arrLakeSelected, "2") > -1)
            {
                objGround2.SetActive(false);
                lakeMobile2.SetActive(true);
            }

            if (Array.IndexOf(arrLakeSelected, "3") > -1)
            {
                objGround3.SetActive(false);
                lakeMobile3.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelected, "4") > -1)
            {
                objGround4.SetActive(false);
                lakeMobile4.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelected, "5") > -1)
            {
                objGround5.SetActive(false);
                lakeMobile5.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelected, "6") > -1)
            {
                objGround6.SetActive(false);
                lakeMobile6.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelected, "7") > -1)
            {
                objGround7.SetActive(false);
                lakeMobile7.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelected, "8") > -1)
            {
                objGround8.SetActive(false);
                lakeMobile8.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelected, "9") > -1)
            {
                objGround9.SetActive(false);
                lakeMobile9.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelected, "10") > -1)
            {
                objGround10.SetActive(false);
                lakeMobile10.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelected, "11") > -1)
            {
                objGround11.SetActive(false);
                lakeMobile11.SetActive(true);
            }
            if (Array.IndexOf(arrLakeSelected, "12") > -1)
            {
                objGround12.SetActive(false);
                lakeMobile12.SetActive(true);
            }
        }

    }

    [RPC]
    public void DestroyObject(string index, string type)
    {
        if (type.Equals("PC"))
        {
            if (Array.IndexOf(arrLakeSelectedPC, "1") > -1)
            {
                print("Lake 1");
                objGround1.SetActive(true);
                objGround4.SetActive(true);
                lakePC1.SetActive(false);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "2") > -1)
            {
                print("Lake 2");
                objGround2.SetActive(true);
                objGround5.SetActive(true);
                lakePC2.SetActive(false);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "3") > -1)
            {
                print("Lake 3");
                objGround3.SetActive(true);
                objGround6.SetActive(true);
                lakePC3.SetActive(false);
            }

            if (Array.IndexOf(arrLakeSelectedPC, "7") > -1)
            {
                print("Lake 4");
                objGround7.SetActive(true);
                objGround10.SetActive(true);
                lakePC4.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelectedPC, "8") > -1)
            {
                print("Lake 5");
                objGround8.SetActive(true);
                objGround11.SetActive(true);
                lakePC5.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelectedPC, "9") > -1)
            {
                print("Lake 6");
                objGround9.SetActive(true);
                objGround12.SetActive(true);
                lakePC6.SetActive(false);
            }
        }
        else
        {
            if (Array.IndexOf(arrLakeSelected, "1") > -1)
            {
                objGround1.SetActive(true);
                lakeMobile1.SetActive(false);
            }

            if (Array.IndexOf(arrLakeSelected, "2") > -1)
            {
                objGround2.SetActive(true);
                lakeMobile2.SetActive(false);
            }

            if (Array.IndexOf(arrLakeSelected, "3") > -1)
            {
                objGround3.SetActive(true);
                lakeMobile3.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelected, "4") > -1)
            {
                objGround4.SetActive(true);
                lakeMobile4.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelected, "5") > -1)
            {
                objGround5.SetActive(true);
                lakeMobile5.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelected, "6") > -1)
            {
                objGround6.SetActive(true);
                lakeMobile6.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelected, "7") > -1)
            {
                objGround7.SetActive(true);
                lakeMobile7.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelected, "8") > -1)
            {
                objGround8.SetActive(true);
                lakeMobile8.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelected, "9") > -1)
            {
                objGround9.SetActive(true);
                lakeMobile9.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelected, "10") > -1)
            {
                objGround10.SetActive(true);
                lakeMobile10.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelected, "11") > -1)
            {
                objGround11.SetActive(true);
                lakeMobile11.SetActive(false);
            }
            if (Array.IndexOf(arrLakeSelected, "12") > -1)
            {
                objGround12.SetActive(true);
                lakeMobile12.SetActive(false);
            }
        }

        if (index.Equals("1") || index.Equals("1-4"))
        {
            player1.SetActive(false);
        }
        if (index.Equals("2") || index.Equals("2-5"))
        {
            player2.SetActive(false);
        }
        if (index.Equals("3") || index.Equals("3-6"))
        {
            player3.SetActive(false);
        }
        if (index.Equals("4"))
        {
            player4.SetActive(false);
        }
        if (index.Equals("5"))
        {
            player5.SetActive(false);
        }
        if (index.Equals("6"))
        {
            player6.SetActive(false);
        }
        if (index.Equals("7"))
        {
            player7.SetActive(false);
        }
        if (index.Equals("8"))
        {
            player8.SetActive(false);
        }
        if (index.Equals("9"))
        {
            player9.SetActive(false);
        }
        if (index.Equals("10") || index.Equals("7-10"))
        {
            player10.SetActive(false);
        }
        if (index.Equals("11") || index.Equals("8-11"))
        {
            player11.SetActive(false);
        }
        if (index.Equals("12") || index.Equals("9-12"))
        {
            player12.SetActive(false);
        }

    }

    [RPC]
    public void SpawnBallClient(string index)
    {

        if (index.Equals("1") || index.Equals("1-4"))
        {
            player1.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 1"), player1.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("2") || index.Equals("2-5"))
        {
            player2.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 2"), player2.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("3") || index.Equals("3-6"))
        {
            player3.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 3"), player3.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("4"))
        {
            player4.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 4"), player4.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("5"))
        {
            player5.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 5"), player5.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("6"))
        {
            player6.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 6"), player6.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("7"))
        {
            player7.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 7"), player7.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("8"))
        {
            player8.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 8"), player8.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("9"))
        {
            player9.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 9"), player9.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("10") || index.Equals("7-10"))
        {
            player10.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 10"), player10.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("11") || index.Equals("8-11"))
        {
            player11.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 11"), player11.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("12") || index.Equals("9-12"))
        {
            player12.SetActive(true);
            //Network.Instantiate(Resources.Load("Prefabs/Player 12"), player12.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
    }

}

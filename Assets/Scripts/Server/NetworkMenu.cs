using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class User
{
    public string name;
    public string ip;
    public string lakePos;
    public string device;
}

public class NetworkMenu : MonoBehaviour
{
    #region Khai bao

    // Create Server 
    public InputField IpInputServer;
    public InputField PortInputServer;
    public Text ErrorTextServer;

    // Single Player
    public InputField UserInputSingle;

    // Connection
    public InputField IPInput;
    public InputField PortInput;
    public Text ErrorTextConnection;

    public Text Angle;
    public Text Distance;
    public Text NameUser;
    public Text TypeConnect;
    public Text lakeSelected;
    public Text errorlakeSelected;
    public Text device;
    public Slider SliderAngle;
    public Slider SliderDistance;
    public Button btnListUser;

    public GameObject panelMobile;
    public GameObject panelPC;
    public GameObject dragPC;
    public GameObject dragMobile;

    public GameObject map;
    public GameObject panelSelectLake;
    public GameObject panelSelectModeStart;
    public GameObject panelOption;
    public GameObject panelAddObject;

    public GameObject panelRegister;
    public GameObject panelLogin;
    public GameObject panelConnection;
    public GameObject panelSinglePlayer;
    public GameObject panelServer;

    public GameObject tabScore;

    public GameObject panelListUser;
    public GameObject userObj;
    public Transform panelList;
    public List<User> userList;

    public Canvas canvasConnection;
    public Canvas canvasMain;
    public Canvas canvasSelectLake;
    public Canvas canvasSelectModeGame;

    public Camera camServer;
    public Camera camConnection;

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
    public GameObject player13;
    public GameObject player14;
    public GameObject player15;
    public GameObject player16;

    public GameObject lakePC1;
    public GameObject lakePC2;
    public GameObject lakePC3;
    public GameObject lakePC4;
    public GameObject lakePC5;
    public GameObject lakePC6;

    public GameObject lakeMobile1;
    public GameObject lakeMobile3;
    public GameObject lakeMobile4;
    public GameObject lakeMobile6;
    public GameObject lakeMobile7;
    public GameObject lakeMobile8;
    public GameObject lakeMobile9;
    public GameObject lakeMobile10;
    public GameObject lakeMobile11;
    public GameObject lakeMobile12;
    public GameObject lakeMobile13;
    public GameObject lakeMobile14;

    public GameObject inFrontOf1;
    public GameObject inFrontOf3;
    public GameObject inFrontOf4;
    public GameObject inFrontOf6;
    public GameObject inFrontOf7;
    public GameObject inFrontOf8;
    public GameObject inFrontOf9;
    public GameObject inFrontOf10;
    public GameObject inFrontOf11;
    public GameObject inFrontOf12;
    public GameObject inFrontOf13;
    public GameObject inFrontOf14;

    public GameObject inFrontOfPC1;
    public GameObject inFrontOfPC2;
    public GameObject inFrontOfPC3;
    public GameObject inFrontOfPC4;
    public GameObject inFrontOfPC5;
    public GameObject inFrontOfPC6;

    public GameObject left1;
    public GameObject left2;
    public GameObject left3;
    public GameObject left4;
    public GameObject left5;
    public GameObject left6;
    public GameObject left7;
    public GameObject left8;

    public GameObject right1;
    public GameObject right2;
    public GameObject right3;
    public GameObject right4;
    public GameObject right5;
    public GameObject right6;
    public GameObject right7;
    public GameObject right8;

    public GameObject leftMobile1;
    public GameObject leftMobile3;
    public GameObject leftMobile4;
    public GameObject leftMobile6;
    public GameObject leftMobile7;
    public GameObject leftMobile8;
    public GameObject leftMobile9;
    public GameObject leftMobile10;
    public GameObject leftMobile11;
    public GameObject leftMobile12;
    public GameObject leftMobile13;
    public GameObject leftMobile14;


    public GameObject rightMobile1;
    public GameObject rightMobile3;
    public GameObject rightMobile4;
    public GameObject rightMobile6;
    public GameObject rightMobile7;
    public GameObject rightMobile8;
    public GameObject rightMobile9;
    public GameObject rightMobile10;
    public GameObject rightMobile11;
    public GameObject rightMobile12;
    public GameObject rightMobile13;
    public GameObject rightMobile14;

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

    public GameObject ground1;
    public GameObject ground1b;
    public GameObject ground4;
    public GameObject ground3;
    public GameObject ground3b;
    public GameObject ground6;
    public GameObject ground7;
    public GameObject ground10;
    public GameObject ground10b;
    public GameObject ground9;
    public GameObject ground12;
    public GameObject ground12b;
    public GameObject ground8;
    public GameObject ground11;
    public GameObject ground11b;
    public GameObject ground13;
    public GameObject ground14;
    public GameObject ground14b;

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

    public string arrLakeSelected;
    public string arrLakeSelectedPC;
    public string arrNotSelectLake;

    public static string name;
    private string remoteIp;
    private int remotePort;
    private int listenPort;
    private bool useNAT;
    private const int NUMBER_CONNECTIONS = 12;

    private bool mode = false;
    private bool isTabScore = false;
    private bool isDisconnect = false;
    private bool isOption = false;
    private float screenSize;


    private string typeDevice;
    public static string lakePosition;

    public static float dis = 0;
    public static float ang = 0;

    private const float MAX_ANGLE = 90.0f;
    private const float MIN_ANGLE = 1.0f;
    private const float MAX_DISTANCE = 100.0f;
    private const float MIN_DISTANCE = 1.0f;
    private const float DELTA = 1.0f;

    private int count = 0;

    private SQLiteDB db = null;

    IEnumerator Download(WWW www)
    {
        yield return www;
    }

    #endregion

    #region START SERVER

    void Start()
    {
        IpInputServer.text = "127.0.0.1";
        PortInputServer.text = "8080";
        UserInputSingle.text = "DatVIT";
        IPInput.text = "127.0.0.1";
        PortInput.text = "8080";
        listenPort = 8080;

        Distance.text = "30";
        Angle.text = "15";
        TypeConnect.text = "";
        SliderAngle.maxValue = MAX_ANGLE;
        SliderDistance.maxValue = MAX_DISTANCE;
        SliderDistance.minValue = MIN_DISTANCE;
        SliderAngle.minValue = MIN_ANGLE;

        useNAT = false;
        float.TryParse(Angle.text.ToString(), out ang);
        float.TryParse(Distance.text.ToString(), out dis);

        SliderDistance.value = dis;
        SliderAngle.value = ang;

        screenSize = Mathf.Max(Screen.width, Screen.height);
        Debug.Log("DENSITY: " + DisplayMetricsAndroid.Density);

    }

    void OnGUI()
    {
        if (Network.peerType != NetworkPeerType.Disconnected)
        {
            Distance.text = (int)dis + "";
            Angle.text = (int)ang + "";

            if (Network.isServer)
            {
                Debug.Log("userList.Count : " + userList.Count);
                GetComponent<NetworkView>().RPC("SendListLakeSelected", RPCMode.All, arrLakeSelected, arrLakeSelectedPC);
            }

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabScore.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            tabScore.gameObject.SetActive(false);
        }
    }

    void OnConnectedToServer()
    {
        Debug.Log("ConnectionServerGame");
        panelConnection.gameObject.SetActive(false);
        panelServer.gameObject.SetActive(false);
        panelRegister.gameObject.SetActive(false);
        panelSinglePlayer.gameObject.SetActive(false);
        panelLogin.gameObject.SetActive(true);
        btnListUser.gameObject.SetActive(false);
    }

    void OnServerInitialized()
    {
        Debug.Log("OnServerInitialized");
        panelConnection.gameObject.SetActive(false);
        panelLogin.gameObject.SetActive(true);
        panelServer.gameObject.SetActive(false);
        panelRegister.gameObject.SetActive(false);
        panelSinglePlayer.gameObject.SetActive(false);
    }

    void OnPlayerDisconnected()
    {
        Debug.Log("Player Disconnected");
    }

    void OnPlayerConnected()
    {
        Debug.Log("Player connected");
    }

    void OnDisconnectedFromServer()
    {
        Debug.Log("Disconnected From Server: " + isDisconnect);

        if (isDisconnect == false)
        {
            GetComponent<NetworkView>().RPC("DisconnectionServer", RPCMode.All);
        }
    }

    #endregion

    #region INITIALIZED MAP

    public void SelectModeStart()
    {
        if (Network.isServer)
        {
            InitMap();
        }
        else
        {
            panelSelectModeStart.gameObject.SetActive(false);
            panelSelectLake.gameObject.SetActive(true);

            SelectLake();
        }
    }

    private void SelectLake()
    {
        arrNotSelectLake = "1-2-3-4-5-6-7-8-9-10-11*12*";

        Debug.Log("SpawnLakeSelected_Connect " + arrLakeSelected);
        Debug.Log("SpawnLakeSelectedPC_Connect: " + arrLakeSelectedPC);

        string[] arrMobile = arrLakeSelected.Trim().Split('-');
        string[] arrPC = arrLakeSelectedPC.Trim().Split('-');


        ArrayList listMobile = new ArrayList();
        ArrayList listPC = new ArrayList();

        for (int i = 0; i < arrMobile.Length; i++)
        {
            if (!arrMobile[i].Equals(""))
            {
                listMobile.Add(arrMobile[i]);
            }
        }

        for (int i = 0; i < arrPC.Length; i++)
        {
            if (!arrPC[i].Equals(""))
            {
                listPC.Add(arrPC[i]);
            }
        }

        if (screenSize > 1000 && DisplayMetricsAndroid.Density == 0)
        {
            panelPC.SetActive(true);
            dragPC.SetActive(true);
            panelMobile.SetActive(false);
            dragMobile.SetActive(false);
            device.text = "PC";

            if (listPC.Contains("1"))
            {
                slot14.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("1-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("4-", "");

                if (!listPC.Contains("7") && !listMobile.Contains("7")
                    && !listMobile.Contains("10") && !listMobile.Contains("13") && !listMobile.Contains("14"))
                {
                    slot710.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("10-", "");
                }
            }

            if (listPC.Contains("2"))
            {
                slot25.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("2-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("5-", "");

                if (!listPC.Contains("1") && !listMobile.Contains("1") && !listMobile.Contains("4"))
                {
                    slot14.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("1-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("4-", "");
                }

                if (!listPC.Contains("3") && !listMobile.Contains("3") && !listMobile.Contains("6"))
                {
                    slot36.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("3-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("6-", "");
                }

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("11")
                    && !listMobile.Contains("13") && !listMobile.Contains("14"))
                {
                    slot811.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }
            }

            if (listPC.Contains("3"))
            {
                slot36.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("3-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("6-", "");

                if (!listPC.Contains("9") && !listMobile.Contains("9") && !listMobile.Contains("12")
                    && !listMobile.Contains("8") && !listMobile.Contains("11"))
                {
                    slot912.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("12*", "");
                }
            }

            if (listPC.Contains("7"))
            {
                slot710.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("10-", "");

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("11")
                    && !listMobile.Contains("13") && !listMobile.Contains("14"))
                {
                    slot811.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }

                if (!listPC.Contains("1") && !listMobile.Contains("1") && !listMobile.Contains("4"))
                {
                    slot14.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("1-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("4-", "");
                }
            }

            if (listPC.Contains("8"))
            {
                slot811.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("11*", "");

                if (!listPC.Contains("7") && !listMobile.Contains("7") && !listMobile.Contains("10"))
                {
                    slot710.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("10-", "");
                }

                if (!listPC.Contains("9") && !listMobile.Contains("9") && !listMobile.Contains("12"))
                {
                    slot912.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("12*", "");
                }

            }

            if (listPC.Contains("9"))
            {
                slot912.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("12*", "");

                if (!listPC.Contains("3") && !listMobile.Contains("3") && !listMobile.Contains("6"))
                {
                    slot36.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("3-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("6-", "");
                }

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("11")
                   && !listMobile.Contains("13") && !listMobile.Contains("14"))
                {
                    slot811.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }
            }

            if (listMobile.Contains("1") || listMobile.Contains("4"))
            {
                slot14.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("1-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("4-", "");

                if (!listPC.Contains("7") && !listMobile.Contains("7") && !listMobile.Contains("10")
                    && !listMobile.Contains("13") && !listMobile.Contains("14"))
                {
                    slot710.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("10-", "");
                }
            }

            if (listMobile.Contains("2") || listMobile.Contains("5"))
            {
                slot25.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("2-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("5-", "");
            }

            if (listMobile.Contains("3") || listMobile.Contains("6"))
            {
                slot36.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("3-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("6-", "");

                if (!listPC.Contains("9") && !listMobile.Contains("9") && !listMobile.Contains("12")
                    && !listMobile.Contains("8") && !listMobile.Contains("11"))
                {
                    slot912.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("12*", "");
                }
            }
            if (listMobile.Contains("7") || listMobile.Contains("10"))
            {
                slot710.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("10-", "");

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("11")
                    && !listMobile.Contains("13") && !listMobile.Contains("14"))
                {
                    slot811.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }
            }
            if (listMobile.Contains("8") || listMobile.Contains("11"))
            {
                slot811.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("11*", "");

                if (!listPC.Contains("7") && !listMobile.Contains("7") && !listMobile.Contains("10"))
                {
                    slot710.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("10-", "");
                }
            }

            if (listMobile.Contains("13") || listMobile.Contains("14"))
            {
                slot811.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("11*", "");

                if (!listPC.Contains("9") && !listMobile.Contains("9") && !listMobile.Contains("12"))
                {
                    slot912.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("12*", "");
                }
            }
            if (listMobile.Contains("9") || listMobile.Contains("12"))
            {
                slot912.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("12*", "");

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("11")
                    && !listMobile.Contains("13") && !listMobile.Contains("14"))
                {
                    slot811.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }
            }
        }
        else
        {
            panelMobile.SetActive(true);
            dragMobile.SetActive(true);
            panelPC.SetActive(false);
            dragPC.SetActive(false);
            device.text = "Mobile";

            if (listMobile.Contains("1"))
            {
                slot1.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("1-", "");

                if (!listPC.Contains("2") && !listMobile.Contains("2"))
                {
                    slot2.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("2-", "");
                }

                if (!listPC.Contains("5") && !listMobile.Contains("5"))
                {
                    slot5.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("5-", "");
                }

                if (!listPC.Contains("4") && !listMobile.Contains("4"))
                {
                    slot4.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("4-", "");
                }
            }

            if (listMobile.Contains("2"))
            {
                slot2.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("2-", "");

                if (!listPC.Contains("1") && !listMobile.Contains("1"))
                {
                    slot1.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("1-", "");
                }

                if (!listPC.Contains("5") && !listMobile.Contains("5"))
                {
                    slot5.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("5-", "");
                }

                if (!listPC.Contains("3") && !listMobile.Contains("3"))
                {
                    slot3.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("3-", "");
                }
            }

            if (listMobile.Contains("3"))
            {
                slot3.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("3-", "");

                if (!listPC.Contains("2") && !listMobile.Contains("2"))
                {
                    slot2.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("2-", "");
                }

                if (!listPC.Contains("6") && !listMobile.Contains("6"))
                {
                    slot6.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("6-", "");
                }

            }
            if (listMobile.Contains("4"))
            {
                slot4.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("4-", "");

                if (!listPC.Contains("5") && !listMobile.Contains("5"))
                {
                    slot5.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("5-", "");
                }

                if (!listPC.Contains("7") && !listMobile.Contains("7") && !listMobile.Contains("13"))
                {
                    slot7.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                }

                if (!listPC.Contains("1") && !listMobile.Contains("1"))
                {
                    slot1.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("1-", "");
                }
            }
            if (listMobile.Contains("5"))
            {
                slot5.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("5-", "");

                if (!listPC.Contains("2") && !listMobile.Contains("2"))
                {
                    slot2.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("2-", "");
                }

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("13"))
                {
                    slot8.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                }

                if (!listPC.Contains("4") && !listMobile.Contains("4"))
                {
                    slot4.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("4-", "");
                }

                if (!listPC.Contains("6") && !listMobile.Contains("6"))
                {
                    slot6.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("6-", "");
                }
            }
            if (listMobile.Contains("6"))
            {
                slot6.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("6-", "");

                if (!listPC.Contains("9") && !listMobile.Contains("9") && !listMobile.Contains("8"))
                {
                    slot9.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                }

                if (!listPC.Contains("3") && !listMobile.Contains("3"))
                {
                    slot3.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("3-", "");
                }

                if (!listPC.Contains("5") && !listMobile.Contains("5"))
                {
                    slot5.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("5-", "");
                }
            }
            if (listMobile.Contains("7"))
            {
                slot7.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("7-", "");

                if (!listPC.Contains("10") && !listMobile.Contains("10"))
                {
                    slot10.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("10-", "");
                }

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("13"))
                {
                    slot8.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                }

                if (!listPC.Contains("4") && !listMobile.Contains("4"))
                {
                    slot4.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("4-", "");
                }
            }
            if (listMobile.Contains("8"))
            {
                slot8.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("8-", "");

                if (!listPC.Contains("11") && !listMobile.Contains("11") && !listMobile.Contains("14"))
                {
                    slot11.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }

                if (!listPC.Contains("5") && !listMobile.Contains("5"))
                {
                    slot5.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("5-", "");
                }

                if (!listPC.Contains("7") && !listMobile.Contains("7"))
                {
                    slot7.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                }

                if (!listPC.Contains("9") && !listMobile.Contains("9"))
                {
                    slot9.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                }
            }
            if (listMobile.Contains("9"))
            {
                slot9.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("9-", "");

                if (!listPC.Contains("12") && !listMobile.Contains("12"))
                {
                    slot12.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("12*", "");
                }

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("13"))
                {
                    slot8.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                }

                if (!listPC.Contains("6") && !listMobile.Contains("6"))
                {
                    slot6.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("6-", "");
                }
            }
            if (listMobile.Contains("10"))
            {
                slot10.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("10-", "");

                if (!listPC.Contains("7") && !listMobile.Contains("7"))
                {
                    slot7.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                }

                if (!listPC.Contains("11") && !listMobile.Contains("11") && !listMobile.Contains("14"))
                {
                    slot11.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }
            }
            if (listMobile.Contains("11"))
            {
                slot12.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("12*", "");

                if (!listPC.Contains("10") && !listMobile.Contains("10"))
                {
                    slot7.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                }

                if (!listPC.Contains("12") && !listMobile.Contains("12"))
                {
                    slot12.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("12*", "");
                }

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("13"))
                {
                    slot8.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                }
            }
            if (listMobile.Contains("12"))
            {
                slot12.color = Color.green;

                arrNotSelectLake = arrNotSelectLake.Replace("12*", "");

                if (!listPC.Contains("11") && !listMobile.Contains("11") && !listMobile.Contains("14"))
                {
                    slot11.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }

                if (!listPC.Contains("9") && !listMobile.Contains("9"))
                {
                    slot9.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                }
            }

            if (listPC.Contains("1"))
            {
                slot1.color = Color.red;
                slot4.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("1-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("4-", "");

                if (!listPC.Contains("7") && !listMobile.Contains("7") && !listMobile.Contains("13"))
                {
                    slot7.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                }

            }

            if (listPC.Contains("2"))
            {
                slot2.color = Color.red;
                slot5.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("2-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("5-", "");

                if (!listPC.Contains("1") && !listMobile.Contains("1"))
                {
                    slot1.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("1-", "");
                }

                if (!listPC.Contains("4") && !listMobile.Contains("4"))
                {
                    slot4.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("4-", "");
                }

                if (!listPC.Contains("3") && !listMobile.Contains("3"))
                {
                    slot3.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("3-", "");
                }

                if (!listPC.Contains("6") && !listMobile.Contains("6"))
                {
                    slot6.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("6-", "");
                }

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("13"))
                {
                    slot8.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                }
            }

            if (listPC.Contains("3"))
            {
                slot3.color = Color.red;
                slot6.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("3-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("6-", "");

                if (!listPC.Contains("9") && !listMobile.Contains("9") && !listMobile.Contains("8"))
                {
                    slot9.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                }
            }

            if (listPC.Contains("7"))
            {
                slot7.color = Color.red;
                slot10.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("10-", "");

                if (!listPC.Contains("4") && !listMobile.Contains("4"))
                {
                    slot4.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("4-", "");
                }

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("13"))
                {
                    slot8.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                }

                if (!listPC.Contains("11") && !listMobile.Contains("11") && !listMobile.Contains("14"))
                {
                    slot11.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }
            }
            if (listPC.Contains("8"))
            {
                slot8.color = Color.red;
                slot11.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("11*", "");

                if (!listPC.Contains("7") && !listMobile.Contains("7") && !listMobile.Contains("13"))
                {
                    slot7.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("7-", "");
                }

                if (!listPC.Contains("10") && !listMobile.Contains("10"))
                {
                    slot10.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("10-", "");
                }

                if (!listPC.Contains("9") && !listMobile.Contains("9") && !listMobile.Contains("8"))
                {
                    slot9.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                }

                if (!listPC.Contains("12") && !listMobile.Contains("12"))
                {
                    slot12.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("12*", "");
                }
            }
            if (listPC.Contains("9"))
            {
                slot9.color = Color.red;
                slot12.color = Color.red;

                arrNotSelectLake = arrNotSelectLake.Replace("9-", "");
                arrNotSelectLake = arrNotSelectLake.Replace("12*", "");

                if (!listPC.Contains("6") && !listMobile.Contains("6"))
                {
                    slot6.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("6-", "");
                }

                if (!listPC.Contains("8") && !listMobile.Contains("8") && !listMobile.Contains("13"))
                {
                    slot8.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("8-", "");
                }

                if (!listPC.Contains("11") && !listMobile.Contains("11") && !listMobile.Contains("14"))
                {
                    slot11.color = Color.white;

                    arrNotSelectLake = arrNotSelectLake.Replace("11*", "");
                }
            }
        }

        lakeSelected.text = "";
        errorlakeSelected.gameObject.SetActive(false);
    }

    public void InitMap()
    {
        Debug.Log("NotLakeSelected: " + arrNotSelectLake);
        string[] arrLake = arrNotSelectLake.Trim().Replace("*", "-").Split('-');

        ArrayList listLake = new ArrayList();

        for (int i = 0; i < arrLake.Length; i++)
        {
            if (!arrLake[i].Equals(""))
            {
                listLake.Add(arrLake[i]);
            }
        }

        lakePosition = lakeSelected.text.Substring(lakeSelected.text.IndexOf("@") + 1)
                .Replace("Slot", "").Trim();

        bool isCheckLake = false;

        if (lakePosition.Contains("-"))
        {
            string[] temp = temp = lakePosition.Split('-');

            for (int i = 0; i < temp.Length; i++)
            {
                if (listLake.Contains(temp[i]))
                {
                    isCheckLake = true;
                    break;
                }
            }
        }
        else
        {
            if (listLake.Contains(lakePosition))
            {
                isCheckLake = true;
            }
        }

        if (isCheckLake == false)
        {
            errorlakeSelected.gameObject.SetActive(false);

            map.gameObject.SetActive(true);
            panelConnection.gameObject.SetActive(false);
            panelLogin.gameObject.SetActive(false);
            panelServer.gameObject.SetActive(false);
            panelRegister.gameObject.SetActive(false);
            panelSinglePlayer.gameObject.SetActive(false);
            camConnection.gameObject.SetActive(false);
            canvasConnection.gameObject.SetActive(false);
            canvasSelectLake.gameObject.SetActive(false);
            canvasMain.gameObject.SetActive(true);

            if (Network.isServer)
            {
                typeDevice = "PC";
                lakePosition = "2-5";
                TypeConnect.text = "Server";
            }
            else
            {
                typeDevice = lakeSelected.text.Substring(0, device.text.Length);
                lakePosition = lakeSelected.text.Substring(lakeSelected.text.IndexOf("@") + 1)
                    .Replace("Slot", "").Trim();
                TypeConnect.text = "Client";
            }

            print("LIST_POS: " + lakePosition);
            NameUser.text = "User: " + name;

            if (Network.isClient)
            {
                NameUser.text = "User: " + name + "\nIP: " + Network.player.ipAddress;
                GetComponent<NetworkView>().RPC("AddUser", RPCMode.Server, Network.player.ipAddress, name, lakePosition, typeDevice);
            }

            GetComponent<NetworkView>().RPC("AddLakeToList", RPCMode.All, typeDevice, lakePosition);
            GetComponent<NetworkView>().RPC("SpawnLakePC", RPCMode.All, null);
            GetComponent<NetworkView>().RPC("SpawnLakeMobile", RPCMode.All, null);

            SpawnPlayer(lakePosition);

        }
        else
        {
            errorlakeSelected.gameObject.SetActive(true);
        }
    }

    public void SpawnPlayer(string index)
    {

        if (index.Equals("1-4"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 1"), player1.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("2-5"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 2"), player2.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("3-6"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 3"), player3.transform.position, Quaternion.Euler(0f, 0f, 0f), 0);
        }
        if (index.Equals("7-10"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 10"), player10.transform.position, Quaternion.Euler(0f, 180f, 0f), 0);
        }
        if (index.Equals("8-11"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 11"), player11.transform.position, Quaternion.Euler(0f, 180f, 0f), 0);
        }
        if (index.Equals("9-12"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 12"), player12.transform.position, Quaternion.Euler(0f, 180f, 0f), 0);
        }

        ///

        if (index.Equals("1"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 4"), player4.transform.position, Quaternion.Euler(0f, 90f, 0f), 0);
        }
        if (index.Equals("3"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 5"), player5.transform.position, Quaternion.Euler(0f, -90f, 0f), 0);
        }
        if (index.Equals("4"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 7"), player7.transform.position, Quaternion.Euler(0f, 90f, 0f), 0);
        }
        if (index.Equals("6"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 6"), player6.transform.position, Quaternion.Euler(0f, -90f, 0f), 0);
        }
        if (index.Equals("7"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 8"), player8.transform.position, Quaternion.Euler(0f, 90f, 0f), 0);
        }
        if (index.Equals("8"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 15"), player15.transform.position, Quaternion.Euler(0f, -90f, 0f), 0);

        }
        if (index.Equals("9"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 9"), player9.transform.position, Quaternion.Euler(0f, -90f, 0f), 0);
        }
        if (index.Equals("10"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 13"), player13.transform.position, Quaternion.Euler(0f, 90f, 0f), 0);
        }
        if (index.Equals("11"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 16"), player16.transform.position, Quaternion.Euler(0f, -90f, 0f), 0);
        }
        if (index.Equals("12"))
        {
            Network.Instantiate(Resources.Load("Prefabs/Player 14"), player14.transform.position, Quaternion.Euler(0f, -90f, 0f), 0);
        }

    }

    #endregion

    #region DESTROY MAP

    public void Logout()
    {
        isDisconnect = true;

        map.gameObject.SetActive(false);
        camConnection.gameObject.SetActive(true);
        canvasMain.gameObject.SetActive(false);
        canvasConnection.gameObject.SetActive(true);
        canvasSelectLake.gameObject.SetActive(false);

        GetComponent<NetworkView>().RPC("RemoveUser", RPCMode.Server, Network.player.ipAddress, name);
        DestroyPlayer(lakePosition);
        GetComponent<NetworkView>().RPC("DestroyLake", RPCMode.All, lakePosition, typeDevice);
        GetComponent<NetworkView>().RPC("RemoveLakeFromList", RPCMode.All, typeDevice, lakePosition);

        GetComponent<NetworkView>().RPC("DisconnectToServer", RPCMode.All, Player.UserName);

        if (Network.isServer)
        {
            GetComponent<NetworkView>().RPC("DisconnectionServer", RPCMode.All);
        }

        Network.Disconnect(200);

        Application.LoadLevel(0);
    }

    public void DestroyPlayer(string position)
    {

        if (position.Equals("1-4"))
        {
            position = "1";
        }
        if (position.Equals("2-5"))
        {
            position = "2";
        }
        if (position.Equals("3-6"))
        {
            position = "3";
        }
        if (position.Equals("7-10"))
        {
            position = "10";
        }
        if (position.Equals("8-11"))
        {
            position = "11";
        }
        if (position.Equals("9-12"))
        {
            position = "12";
        }

        ///

        if (position.Equals("1"))
        {
            position = "4";
        }
        if (position.Equals("3"))
        {
            position = "5";
        }
        if (position.Equals("4"))
        {
            position = "7";
        }
        if (position.Equals("6"))
        {
            position = "6";
        }
        if (position.Equals("7"))
        {
            position = "8";
        }
        if (position.Equals("8"))
        {
            position = "15";

        }
        if (position.Equals("9"))
        {
            position = "9";
        }
        if (position.Equals("10"))
        {
            position = "13";
        }
        if (position.Equals("11"))
        {
            position = "16";
        }
        if (position.Equals("12"))
        {
            position = "14";
        }
        if (position.Equals("13"))
        {
            position = "17";
        }
        if (position.Equals("14"))
        {
            position = "18";
        }

        Debug.Log("Destroy Player: " + "Player " + position + "(Clone)");

        Network.Destroy(GameObject.Find("Player " + position + "(Clone)"));
    }

    public void DestroyLakeForPC(string index)
    {
        Debug.Log("SpawnLakeMobile: " + arrLakeSelected);
        Debug.Log("SpawnLakeMobile_PC: " + arrLakeSelectedPC);

        string[] arrMobile = arrLakeSelected.Trim().Split('-');
        string[] arrPC = arrLakeSelectedPC.Trim().Split('-');


        ArrayList listMobile = new ArrayList();
        ArrayList listPC = new ArrayList();

        for (int i = 0; i < arrMobile.Length; i++)
        {
            if (!arrMobile[i].Equals(""))
            {
                listMobile.Add(arrMobile[i]);
            }
        }

        for (int i = 0; i < arrPC.Length; i++)
        {
            if (!arrPC[i].Equals(""))
            {
                listPC.Add(arrPC[i]);
            }
        }

        Debug.Log("listPC: " + listPC.Count);
        Debug.Log("listMobile: " + listMobile.Count);

        if (index.Equals("1-4"))
        {
            objGround1.SetActive(true);
            objGround4.SetActive(true);
            lakePC1.SetActive(false);
            ground1b.SetActive(true);

            right1.SetActive(true);
            right2.SetActive(true);
            left1.SetActive(true);
            left2.SetActive(true);

            if (listPC.Contains("7"))
            {
                inFrontOfPC1.SetActive(true);
                inFrontOfPC4.SetActive(true);
            }

            if (listMobile.Contains("7"))
            {
                inFrontOfPC1.SetActive(true);
                rightMobile7.SetActive(true);
            }
        }

        if (index.Equals("2-5"))
        {
            objGround2.SetActive(true);
            objGround5.SetActive(true);
            lakePC2.SetActive(false);
        }

        if (index.Equals("3-6"))
        {
            objGround3.SetActive(true);
            objGround6.SetActive(true);
            lakePC3.SetActive(false);
            ground3b.SetActive(true);

            right3.SetActive(true);
            right4.SetActive(true);
            left3.SetActive(true);
            left4.SetActive(true);

            if (listPC.Contains("9"))
            {
                inFrontOfPC3.SetActive(true);
                inFrontOfPC6.SetActive(true);
            }


            if (listMobile.Contains("9"))
            {
                inFrontOfPC3.SetActive(true);
                leftMobile9.SetActive(true);
            }
        }

        if (index.Equals("7-10"))
        {
            objGround7.SetActive(true);
            objGround10.SetActive(true);
            lakePC4.SetActive(false);
            ground10b.SetActive(true);

            if (listPC.Contains("8"))
            {
                right5.SetActive(true);
                right6.SetActive(true);
                left5.SetActive(true);
                left6.SetActive(true);
            }

            if (listMobile.Contains("4"))
            {
                inFrontOfPC4.SetActive(true);
                leftMobile4.SetActive(true);
            }

            if (listMobile.Contains("8"))
            {
                inFrontOf8.SetActive(true);
                left6.SetActive(true);
            }

            if (listMobile.Contains("11"))
            {
                inFrontOf11.SetActive(true);
                left5.SetActive(true);
            }
        }

        if (index.Equals("8-11"))
        {
            objGround8.SetActive(true);
            objGround11.SetActive(true);
            lakePC5.SetActive(false);
            ground11b.SetActive(true);
            ground14b.SetActive(true);

            inFrontOfPC5.SetActive(true);
            inFrontOfPC2.SetActive(true);

            if (listPC.Contains("9"))
            {
                right7.SetActive(true);
                right8.SetActive(true);
                left7.SetActive(true);
                left8.SetActive(true);
            }

            if (listMobile.Contains("9"))
            {
                left8.SetActive(true);
                inFrontOf9.SetActive(true);
            }

            if (listMobile.Contains("12"))
            {
                left8.SetActive(true);
                inFrontOf12.SetActive(true);
            }
        }

        if (index.Equals("9-12"))
        {
            objGround9.SetActive(true);
            objGround12.SetActive(true);
            lakePC6.SetActive(false);
            ground12b.SetActive(true);

            if (listMobile.Contains("6"))
            {
                rightMobile6.SetActive(true);
                inFrontOfPC6.SetActive(true);
            }

            if (listMobile.Contains("13"))
            {
                right8.SetActive(true);
                inFrontOf13.SetActive(true);
            }

            if (listMobile.Contains("14"))
            {
                right7.SetActive(true);
                inFrontOf14.SetActive(true);
            }
        }
    }

    public void DestroyLakeForMobile(string index)
    {
        Debug.Log("SpawnLakeMobile: " + arrLakeSelected);
        Debug.Log("SpawnLakeMobile_PC: " + arrLakeSelectedPC);

        string[] arrMobile = arrLakeSelected.Trim().Split('-');
        string[] arrPC = arrLakeSelectedPC.Trim().Split('-');


        ArrayList listMobile = new ArrayList();
        ArrayList listPC = new ArrayList();

        for (int i = 0; i < arrMobile.Length; i++)
        {
            if (!arrMobile[i].Equals(""))
            {
                listMobile.Add(arrMobile[i]);
            }
        }

        for (int i = 0; i < arrPC.Length; i++)
        {
            if (!arrPC[i].Equals(""))
            {
                listPC.Add(arrPC[i]);
            }
        }

        Debug.Log("listPC: " + listPC.Count);
        Debug.Log("listMobile: " + listMobile.Count);

        if (index.Equals("1"))
        {
            objGround1.SetActive(true);
            lakeMobile1.SetActive(false);
            inFrontOf1.SetActive(true);
            left1.SetActive(true);
            ground1.SetActive(true);

            if (listMobile.Contains("4"))
            {
                leftMobile1.SetActive(true);
                rightMobile4.SetActive(true);
            }
        }

        if (index.Equals("3"))
        {
            objGround3.SetActive(true);
            lakeMobile3.SetActive(false);
            inFrontOf3.SetActive(true);
            right3.SetActive(true);
            ground3.SetActive(true);

            if (listMobile.Contains("6"))
            {
                rightMobile3.SetActive(true);
                leftMobile6.SetActive(true);
            }
        }

        if (index.Equals("4"))
        {
            objGround4.SetActive(true);
            lakeMobile4.SetActive(false);
            inFrontOf4.SetActive(true);
            left2.SetActive(true);
            ground4.SetActive(true);

            if (listPC.Contains("7"))
            {
                leftMobile4.SetActive(true);
                inFrontOfPC4.SetActive(true);
            }

            if (listMobile.Contains("7"))
            {
                rightMobile7.SetActive(true);
                leftMobile4.SetActive(true);
            }
        }

        if (index.Equals("6"))
        {
            objGround6.SetActive(true);
            lakeMobile6.SetActive(false);
            inFrontOf6.SetActive(true);
            right4.SetActive(true);
            ground6.SetActive(true);

            if (listPC.Contains("9"))
            {
                rightMobile6.SetActive(true);
                inFrontOfPC6.SetActive(true);
            }

            if (listMobile.Contains("9"))
            {
                rightMobile6.SetActive(true);
                leftMobile9.SetActive(true);
            }

        }
        if (index.Equals("7"))
        {
            objGround7.SetActive(true);
            lakeMobile7.SetActive(false);
            ground7.SetActive(true);

            if (listPC.Contains("8"))
            {
                inFrontOf7.SetActive(true);
                right6.SetActive(true);
            }

            if (listPC.Contains("1"))
            {
                inFrontOfPC1.SetActive(true);
                rightMobile7.SetActive(true);
            }

            if (listMobile.Contains("10"))
            {
                rightMobile10.SetActive(true);
                leftMobile7.SetActive(true);
            }

        }
        if (index.Equals("8"))
        {
            objGround8.SetActive(true);
            objGround9.SetActive(true);
            lakeMobile8.SetActive(false);
            ground8.SetActive(false);

            leftMobile8.SetActive(true);
            inFrontOfPC2.SetActive(true);

            if (listMobile.Contains("11"))
            {
                rightMobile8.SetActive(true);
                leftMobile11.SetActive(true);
            }

            if (listMobile.Contains("7"))
            {
                inFrontOf7.SetActive(true);
                inFrontOf8.SetActive(true);
            }

            if (listPC.Contains("7"))
            {
                left6.SetActive(true);
                inFrontOf8.SetActive(true);
            }
        }

        if (index.Equals("9"))
        {
            objGround9.SetActive(true);
            lakeMobile9.SetActive(false);
            ground9.SetActive(true);

            if (listMobile.Contains("12"))
            {
                rightMobile9.SetActive(true);
                leftMobile12.SetActive(true);
            }

            if (listMobile.Contains("6"))
            {
                rightMobile6.SetActive(true);
                leftMobile9.SetActive(true);
            }

            if (listMobile.Contains("13"))
            {
                inFrontOf13.SetActive(true);
                inFrontOf9.SetActive(true);
            }

            if (listPC.Contains("8"))
            {
                left8.SetActive(true);
                inFrontOf9.SetActive(true);
            }

            if (listPC.Contains("3"))
            {
                inFrontOfPC3.SetActive(true);
                leftMobile9.SetActive(true);
            }
        }

        if (index.Equals("10"))
        {
            objGround10.SetActive(true);
            lakeMobile10.SetActive(false);
            ground10.SetActive(true);

            if (listMobile.Contains("7"))
            {
                rightMobile10.SetActive(true);
                leftMobile7.SetActive(true);
            }

            if (listPC.Contains("8"))
            {
                right5.SetActive(true);
                inFrontOf10.SetActive(true);
            }

            if (listMobile.Contains("11"))
            {
                inFrontOf11.SetActive(true);
                inFrontOf10.SetActive(true);
            }
        }

        if (index.Equals("11"))
        {
            objGround11.SetActive(true);
            objGround12.SetActive(true);
            ground11.SetActive(false);
            lakeMobile11.SetActive(false);

            if (listPC.Contains("7"))
            {
                inFrontOf11.SetActive(true);
                left5.SetActive(true);
            }
        }

        if (index.Equals("12"))
        {
            objGround12.SetActive(true);
            lakeMobile12.SetActive(false);
            ground12.SetActive(true);

            if (listPC.Contains("8"))
            {
                inFrontOf12.SetActive(true);
                left7.SetActive(true);
            }

            if (listMobile.Contains("14"))
            {
                inFrontOf12.SetActive(true);
                inFrontOf14.SetActive(true);
            }
        }

        if (index.Equals("13"))
        {
            objGround8.SetActive(true);
            objGround7.SetActive(true);
            ground13.SetActive(false);

            rightMobile13.SetActive(true);
            inFrontOfPC2.SetActive(true);

            if (listPC.Contains("9"))
            {
                inFrontOf13.SetActive(true);
                right8.SetActive(true);
            }

            if (listMobile.Contains("14"))
            {
                leftMobile13.SetActive(true);
                rightMobile14.SetActive(true);
            }

        }

        if (index.Equals("14"))
        {
            objGround11.SetActive(true);
            objGround10.SetActive(true);
            ground14.SetActive(false);

            if (listPC.Contains("9"))
            {
                inFrontOf14.SetActive(true);
                right7.SetActive(true);
            }
        }
    }

    #endregion

    #region MAIN GAME

    public void ModeView()
    {
        if (mode == false)
        {
            camServer.gameObject.SetActive(true);
            mode = true;
        }
        else
        {
            camServer.gameObject.SetActive(false);
            mode = false;
        }

    }

    public void Option()
    {
        if (!isOption)
        {
            panelOption.gameObject.SetActive(true);
            isOption = true;
        }
        else
        {
            panelOption.gameObject.SetActive(false);
            isOption = false;
        }
    }

    public void AddObjectForLake()
    {
        panelOption.gameObject.SetActive(false);
        panelAddObject.gameObject.SetActive(true);
    }

    public void InitantiateObject()
    {
        panelAddObject.gameObject.SetActive(false);
    }

    public void SliderDistance_Changed()
    {
        dis = SliderDistance.value;
    }

    public void SliderAngle_Changed()
    {
        ang = SliderAngle.value;
    }

    public void ListUserClick()
    {
        panelListUser.SetActive(true);
        panelOption.SetActive(false);

        foreach (Transform child in panelList)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (User user in userList)
        {
            GameObject newUser = Instantiate(userObj) as GameObject;
            ButtonScript buttonScript = newUser.GetComponent<ButtonScript>();
            buttonScript.nameLabel.text = user.name;
            buttonScript.ipLabel.text = user.ip;

            buttonScript.btnDel.onClick.AddListener(delegate
            {
                Destroy(newUser);
                GetComponent<NetworkView>().RPC("DisconnectClient", RPCMode.All, user.ip, user.name, user.lakePos, user.device);
            });

            newUser.transform.SetParent(panelList);
        }
    }

    public void CloseListUser()
    {
        panelListUser.SetActive(false);
    }

    public void OpenTabSoce()
    {
        panelOption.SetActive(false);
        tabScore.gameObject.SetActive(true);
    }

    public void CloseTabScore()
    {
        tabScore.gameObject.SetActive(false);
    }

    #endregion

    #region LOGIN GAME

    public void SinglePlayer()
    {
        canvasConnection.gameObject.SetActive(true);
        canvasSelectModeGame.gameObject.SetActive(false);
        panelSinglePlayer.gameObject.SetActive(true);
        panelLogin.gameObject.SetActive(false);
        panelRegister.gameObject.SetActive(false);
        panelServer.gameObject.SetActive(false);
        panelConnection.gameObject.SetActive(false);
    }

    public void MultiplePlayer()
    {
        canvasConnection.gameObject.SetActive(true);
        canvasSelectModeGame.gameObject.SetActive(false);
        panelSinglePlayer.gameObject.SetActive(false);
        panelLogin.gameObject.SetActive(false);
        panelRegister.gameObject.SetActive(false);
        panelServer.gameObject.SetActive(false);
        panelConnection.gameObject.SetActive(true);
    }

    public void ConnectionServerGame()
    {
        if (!IPInput.text.Trim().Equals(""))
        {
            if (!PortInput.text.Trim().Equals(""))
            {
                remoteIp = IPInput.text;
                int.TryParse(PortInput.text, out remotePort);

                NetworkConnectionError err = Network.Connect(remoteIp, remotePort);

                if (err != null && !err.ToString().Equals(""))
                {
                    ErrorTextConnection.text = "Not found Server. You must create Server for connection";
                }
                else
                {
                    ErrorTextConnection.text = "";
                }
            }
            else
            {
                ErrorTextConnection.text = "You have not entered port";
            }
        }
        else
        {
            ErrorTextConnection.text = "You have not entered ip";
        }

    }

    public void CreateServer()
    {
        if (!IpInputServer.text.Trim().Equals(""))
        {
            if (!PortInputServer.text.Trim().Equals(""))
            {
                int.TryParse(PortInputServer.text, out listenPort);

                NetworkConnectionError err = Network.InitializeServer(NUMBER_CONNECTIONS, listenPort, useNAT);

                if (err != null && !err.ToString().Equals(""))
                {
                    ErrorTextServer.text = "This port used. Please select port other";
                }
                else
                {
                    ErrorTextConnection.text = "";
                }
            }
            else
            {
                ErrorTextServer.text = "You have not entered port";
            }
        }
        else
        {
            ErrorTextServer.text = "You have not entered ip";
        }

    }

    public void StartSinglePlayer()
    {
        if (!UserInputSingle.text.Trim().Equals(""))
        {
            name = UserInputSingle.text.Trim();
            Network.InitializeServer(1, listenPort, useNAT);
            InitMap();
        }
        else
        {
            ErrorTextServer.text = "You have not entered username";
        }
    }

    public void SelectServer()
    {
        panelServer.gameObject.SetActive(true);
        panelConnection.gameObject.SetActive(false);
        panelSinglePlayer.gameObject.SetActive(false);
        panelLogin.gameObject.SetActive(false);
        panelRegister.gameObject.SetActive(false);
    }

    public void BackToConnection()
    {
        panelServer.gameObject.SetActive(false);
        panelLogin.gameObject.SetActive(false);
        panelRegister.gameObject.SetActive(false);
        panelConnection.gameObject.SetActive(true);
        panelSinglePlayer.gameObject.SetActive(false);
    }

    public void BackToSelectMode()
    {
        panelServer.gameObject.SetActive(false);
        panelLogin.gameObject.SetActive(false);
        panelRegister.gameObject.SetActive(false);
        panelConnection.gameObject.SetActive(false);
        panelSinglePlayer.gameObject.SetActive(false);
        canvasConnection.gameObject.SetActive(false);
        canvasSelectModeGame.gameObject.SetActive(true);
    }

    #endregion

    #region RPC

    [RPC]
    public void DisconnectToServer(string ten)
    {
        // connect DB
        db = new SQLiteDB();
        string filename = Application.persistentDataPath + "/HighScore.sqlite";
        if (!File.Exists(filename))
        {
            string dbfilename = "HighScore.sqlite";
            byte[] bytes = null;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename;
            WWW www = new WWW(dbpath);
            Download(www);
            bytes = www.bytes;
#elif UNITY_WEBPLAYER
					string dbpath = "StreamingAssets/" + dbfilename;								
					WWW www = new WWW(dbpath);
					Download(www);
					bytes = www.bytes;
#elif UNITY_IPHONE
					string dbpath = Application.dataPath + "/Raw/" + dbfilename;								
					try{	
						using ( FileStream fs = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
							bytes = new byte[fs.Length];
							fs.Read(bytes,0,(int)fs.Length);
						}			
					} catch (Exception e){
						
					}
#elif UNITY_ANDROID
					string dbpath = Application.streamingAssetsPath + "/" + dbfilename;	           
					WWW www = new WWW(dbpath);
						while (!www.isDone) {;}
					//Download(www);
					bytes = www.bytes;
#endif
            if (bytes != null)
            {
                try
                {
                    System.IO.File.WriteAllBytes(filename, bytes);
                    db.Open(filename);
                }
                catch (Exception e)
                {
                    print(e.StackTrace);
                }
            }
        }
        else
        {
            try
            {
                db.Open(filename);
            }
            catch (Exception e)
            {
                print(e.ToString());
            }
        }
        //		
        print("USERNAME DISCONNECT: " + ten);

        foreach (HighScore tmpScore in PlayerControl.arrayScore)
        {
            print(tmpScore.Name);
            if (tmpScore.Name == ten)
            {
                SQLiteQuery qr;
                string sqlQuery = "INSERT INTO HighScore(Name, Score, Count) VALUES(?,?,?)";
                qr = new SQLiteQuery(db, sqlQuery);
                qr.Bind(tmpScore.Name);
                qr.Bind(tmpScore.Score);
                qr.Bind(tmpScore.Count);
                qr.Step();
                qr.Release();
                db.Close();
            }
        }
    }

    [RPC]
    public void DisconnectionServer()
    {
        isDisconnect = true;

        if (Network.isClient)
        {
            map.gameObject.SetActive(false);
            camConnection.gameObject.SetActive(true);
            canvasMain.gameObject.SetActive(false);
            canvasConnection.gameObject.SetActive(true);
            canvasSelectLake.gameObject.SetActive(false);

            RemoveUser(Network.player.ipAddress, name);
            DestroyPlayer(lakePosition);
            DestroyLake(lakePosition, typeDevice);
            RemoveLakeFromList(typeDevice, lakePosition);
            DisconnectToServer(Player.UserName);

            Network.Disconnect(200);

            Application.LoadLevel(0);
        }

    }

    [RPC]
    public void SendListLakeSelected(string arr, string arrPC)
    {
        this.arrLakeSelected = arr;
        this.arrLakeSelectedPC = arrPC;
    }

    [RPC]
    public void AddUser(string ip, string nameUser, string pos, string device)
    {
        User user = new User();
        user.ip = ip;
        user.name = nameUser;
        user.lakePos = pos;
        user.device = device;
        userList.Add(user);
    }

    [RPC]
    public void RemoveUser(string ipUser, string name)
    {
        Debug.Log("SIZE_LIST: " + userList.Count);
        foreach (User user in userList)
        {
            if (user.ip.Equals(ipUser) && user.name.Equals(name))
            {
                userList.Remove(user);
            }
        }
        Debug.Log("SIZE_LIST_REMOVE: " + userList.Count);
    }

    [RPC]
    public void AddLakeToList(string type, string list)
    {
        Debug.Log("AddLakeToList: " + list);
        if (type.Equals("PC"))
        {
            this.arrLakeSelectedPC += list + "-";
        }
        else
        {
            this.arrLakeSelected += list + "-";
        }
    }

    [RPC]
    public void RemoveLakeFromList(string type, string list)
    {
        Debug.Log("RemoveLakeFromList: " + list.Trim());
        if (type.Equals("PC"))
        {
            this.arrLakeSelectedPC = this.arrLakeSelectedPC.Replace(list + "-", " ");
        }
        else
        {
            this.arrLakeSelected = this.arrLakeSelected.Replace(list + "-", "");
        }
        Debug.Log("Remove: " + arrLakeSelected + "#" + arrLakeSelectedPC);
    }

    [RPC]
    public void SpawnLakePC()
    {
        Debug.Log("SpawnLakeMobile: " + arrLakeSelected);
        Debug.Log("SpawnLakeMobile_PC: " + arrLakeSelectedPC);

        string[] arrMobile = arrLakeSelected.Trim().Split('-');
        string[] arrPC = arrLakeSelectedPC.Trim().Split('-');


        ArrayList listMobile = new ArrayList();
        ArrayList listPC = new ArrayList();

        for (int i = 0; i < arrMobile.Length; i++)
        {
            if (!arrMobile[i].Equals(""))
            {
                listMobile.Add(arrMobile[i]);
            }
        }

        for (int i = 0; i < arrPC.Length; i++)
        {
            if (!arrPC[i].Equals(""))
            {
                listPC.Add(arrPC[i]);
            }
        }

        Debug.Log("listPC: " + listPC.Count);
        Debug.Log("listMobile: " + listMobile.Count);

        if (listPC.Contains("1"))
        {
            objGround1.SetActive(false);
            objGround4.SetActive(false);
            lakePC1.SetActive(true);
            ground1b.SetActive(false);

            right1.SetActive(false);
            right2.SetActive(false);
            left1.SetActive(false);
            left2.SetActive(false);

            if (listPC.Contains("7"))
            {
                inFrontOfPC1.SetActive(false);
                inFrontOfPC4.SetActive(false);
            }

            if (listMobile.Contains("7"))
            {
                inFrontOfPC1.SetActive(false);
                rightMobile7.SetActive(false);
            }
        }

        if (listPC.Contains("2"))
        {
            objGround2.SetActive(false);
            objGround5.SetActive(false);
            lakePC2.SetActive(true);
        }

        if (listPC.Contains("3"))
        {
            objGround3.SetActive(false);
            objGround6.SetActive(false);
            lakePC3.SetActive(true);
            ground3b.SetActive(false);

            right3.SetActive(false);
            right4.SetActive(false);
            left3.SetActive(false);
            left4.SetActive(false);

            if (listPC.Contains("9"))
            {
                inFrontOfPC3.SetActive(false);
                inFrontOfPC6.SetActive(false);
            }


            if (listMobile.Contains("9"))
            {
                inFrontOfPC3.SetActive(false);
                leftMobile9.SetActive(false);
            }
        }

        if (listPC.Contains("7"))
        {
            objGround7.SetActive(false);
            objGround10.SetActive(false);
            lakePC4.SetActive(true);
            ground10b.SetActive(false);

            if (listPC.Contains("8"))
            {
                right5.SetActive(false);
                right6.SetActive(false);
                left5.SetActive(false);
                left6.SetActive(false);
            }

            if (listMobile.Contains("4"))
            {
                inFrontOfPC4.SetActive(false);
                leftMobile4.SetActive(false);
            }

            if (listMobile.Contains("8"))
            {
                inFrontOf8.SetActive(false);
                left6.SetActive(false);
            }

            if (listMobile.Contains("11"))
            {
                inFrontOf11.SetActive(false);
                left5.SetActive(false);
            }
        }

        if (listPC.Contains("8"))
        {
            objGround8.SetActive(false);
            objGround11.SetActive(false);
            lakePC5.SetActive(true);
            ground11b.SetActive(false);
            ground14b.SetActive(false);

            inFrontOfPC5.SetActive(false);
            inFrontOfPC2.SetActive(false);

            if (listPC.Contains("9"))
            {
                right7.SetActive(false);
                right8.SetActive(false);
                left7.SetActive(false);
                left8.SetActive(false);
            }

            if (listMobile.Contains("9"))
            {
                left8.SetActive(false);
                inFrontOf9.SetActive(false);
            }

            if (listMobile.Contains("12"))
            {
                left8.SetActive(false);
                inFrontOf12.SetActive(false);
            }
        }

        if (listPC.Contains("9"))
        {
            objGround9.SetActive(false);
            objGround12.SetActive(false);
            lakePC6.SetActive(true);
            ground12b.SetActive(false);

            if (listMobile.Contains("6"))
            {
                rightMobile6.SetActive(false);
                inFrontOfPC6.SetActive(false);
            }

            if (listMobile.Contains("13"))
            {
                right8.SetActive(false);
                inFrontOf13.SetActive(false);
            }

            if (listMobile.Contains("14"))
            {
                right7.SetActive(false);
                inFrontOf14.SetActive(false);
            }
        }
    }

    [RPC]
    public void SpawnLakeMobile()
    {
        Debug.Log("SpawnLakeMobile: " + arrLakeSelected);
        Debug.Log("SpawnLakeMobile_PC: " + arrLakeSelectedPC);

        string[] arrMobile = arrLakeSelected.Trim().Split('-');
        string[] arrPC = arrLakeSelectedPC.Trim().Split('-');


        ArrayList listMobile = new ArrayList();
        ArrayList listPC = new ArrayList();

        for (int i = 0; i < arrMobile.Length; i++)
        {
            if (!arrMobile[i].Equals(""))
            {
                listMobile.Add(arrMobile[i]);
            }
        }

        for (int i = 0; i < arrPC.Length; i++)
        {
            if (!arrPC[i].Equals(""))
            {
                listPC.Add(arrPC[i]);
            }
        }

        Debug.Log("listPC: " + listPC.Count);
        Debug.Log("listMobile: " + listMobile.Count);

        if (listMobile.Contains("1"))
        {
            objGround1.SetActive(false);
            lakeMobile1.SetActive(true);
            inFrontOf1.SetActive(false);
            left1.SetActive(false);
            ground1.SetActive(false);

            if (listMobile.Contains("4"))
            {
                leftMobile1.SetActive(false);
                rightMobile4.SetActive(false);
            }
        }

        if (listMobile.Contains("3"))
        {
            objGround3.SetActive(false);
            lakeMobile3.SetActive(true);
            inFrontOf3.SetActive(false);
            right3.SetActive(false);
            ground3.SetActive(false);

            if (listMobile.Contains("6"))
            {
                rightMobile3.SetActive(false);
                leftMobile6.SetActive(false);
            }
        }

        if (listMobile.Contains("4"))
        {
            objGround4.SetActive(false);
            lakeMobile4.SetActive(true);
            inFrontOf4.SetActive(false);
            left2.SetActive(false);
            ground4.SetActive(false);

            if (listPC.Contains("7"))
            {
                leftMobile4.SetActive(false);
                inFrontOfPC4.SetActive(false);
            }

            if (listMobile.Contains("7"))
            {
                rightMobile7.SetActive(false);
                leftMobile4.SetActive(false);
            }
        }

        if (listMobile.Contains("6"))
        {
            objGround6.SetActive(false);
            lakeMobile6.SetActive(true);
            inFrontOf6.SetActive(false);
            right4.SetActive(false);
            ground6.SetActive(false);

            if (listPC.Contains("9"))
            {
                rightMobile6.SetActive(false);
                inFrontOfPC6.SetActive(false);
            }

            if (listMobile.Contains("9"))
            {
                rightMobile6.SetActive(false);
                leftMobile9.SetActive(false);
            }

        }
        if (listMobile.Contains("7"))
        {
            objGround7.SetActive(false);
            lakeMobile7.SetActive(true);
            ground7.SetActive(false);

            if (listPC.Contains("8"))
            {
                inFrontOf7.SetActive(false);
                right6.SetActive(false);
            }

            if (listPC.Contains("1"))
            {
                inFrontOfPC1.SetActive(false);
                rightMobile7.SetActive(false);
            }

            if (listMobile.Contains("10"))
            {
                rightMobile10.SetActive(false);
                leftMobile7.SetActive(false);
            }

        }
        if (listMobile.Contains("8"))
        {
            objGround8.SetActive(false);
            objGround9.SetActive(false);
            lakeMobile8.SetActive(true);
            ground8.SetActive(true);

            leftMobile8.SetActive(false);
            inFrontOfPC2.SetActive(false);

            if (listMobile.Contains("11"))
            {
                rightMobile8.SetActive(false);
                leftMobile11.SetActive(false);
            }

            if (listMobile.Contains("7"))
            {
                inFrontOf7.SetActive(false);
                inFrontOf8.SetActive(false);
            }

            if (listPC.Contains("7"))
            {
                left6.SetActive(false);
                inFrontOf8.SetActive(false);
            }
        }

        if (listMobile.Contains("9"))
        {
            objGround9.SetActive(false);
            lakeMobile9.SetActive(true);
            ground9.SetActive(false);

            if (listMobile.Contains("12"))
            {
                rightMobile9.SetActive(false);
                leftMobile12.SetActive(false);
            }

            if (listMobile.Contains("6"))
            {
                rightMobile6.SetActive(false);
                leftMobile9.SetActive(false);
            }

            if (listMobile.Contains("13"))
            {
                inFrontOf13.SetActive(false);
                inFrontOf9.SetActive(false);
            }

            if (listPC.Contains("8"))
            {
                left8.SetActive(false);
                inFrontOf9.SetActive(false);
            }

            if (listPC.Contains("3"))
            {
                inFrontOfPC3.SetActive(false);
                leftMobile9.SetActive(false);
            }
        }

        if (listMobile.Contains("10"))
        {
            objGround10.SetActive(false);
            lakeMobile10.SetActive(true);
            ground10.SetActive(false);

            if (listMobile.Contains("7"))
            {
                rightMobile10.SetActive(false);
                leftMobile7.SetActive(false);
            }

            if (listPC.Contains("8"))
            {
                right5.SetActive(false);
                inFrontOf10.SetActive(false);
            }

            if (listMobile.Contains("11"))
            {
                inFrontOf11.SetActive(false);
                inFrontOf10.SetActive(false);
            }
        }

        if (listMobile.Contains("11"))
        {
            objGround11.SetActive(false);
            objGround12.SetActive(false);
            ground11.SetActive(true);
            lakeMobile11.SetActive(true);

            if (listPC.Contains("7"))
            {
                inFrontOf11.SetActive(false);
                left5.SetActive(false);
            }
        }

        if (listMobile.Contains("12"))
        {
            objGround12.SetActive(false);
            lakeMobile12.SetActive(true);
            ground12.SetActive(false);

            if (listPC.Contains("8"))
            {
                inFrontOf12.SetActive(false);
                left7.SetActive(false);
            }

            if (listMobile.Contains("14"))
            {
                inFrontOf12.SetActive(false);
                inFrontOf14.SetActive(false);
            }
        }

        if (listMobile.Contains("13"))
        {
            objGround8.SetActive(false);
            objGround7.SetActive(false);
            ground13.SetActive(true);

            rightMobile13.SetActive(false);
            inFrontOfPC2.SetActive(false);

            if (listPC.Contains("9"))
            {
                inFrontOf13.SetActive(false);
                right8.SetActive(false);
            }

            if (listMobile.Contains("14"))
            {
                leftMobile13.SetActive(false);
                rightMobile14.SetActive(false);
            }

        }

        if (listMobile.Contains("14"))
        {
            objGround11.SetActive(false);
            objGround10.SetActive(false);
            ground14.SetActive(true);

            if (listPC.Contains("9"))
            {
                inFrontOf14.SetActive(false);
                right7.SetActive(false);
            }
        }
    }

    [RPC]
    public void DestroyLake(string index, string type)
    {
        if (type.Equals("PC"))
        {
            DestroyLakeForPC(index);
        }
        else
        {
            DestroyLakeForMobile(index);
        }
    }

    [RPC]
    public void DisconnectClient(string ip, string name, string pos, string device)
    {
        Debug.Log("DisconnectClient: " + ip);

        if (ip.Equals(Network.player.ipAddress) &&
            name.Equals(NetworkMenu.name) &&
            pos.Equals(lakePosition) &&
            device.Equals(typeDevice))
        {
            isDisconnect = true;

            map.gameObject.SetActive(false);
            camConnection.gameObject.SetActive(true);
            canvasMain.gameObject.SetActive(false);
            canvasConnection.gameObject.SetActive(true);
            canvasSelectLake.gameObject.SetActive(false);

            GetComponent<NetworkView>().RPC("RemoveUser", RPCMode.Server, ip, name);
            DestroyPlayer(pos);
            GetComponent<NetworkView>().RPC("DestroyLake", RPCMode.All, pos, device);
            GetComponent<NetworkView>().RPC("RemoveLakeFromList", RPCMode.All, device, pos);

            Network.Disconnect(200);

            Application.LoadLevel(0);
        }
    }

    #endregion
}

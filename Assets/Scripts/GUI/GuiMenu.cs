using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiMenu : MonoBehaviour
{

    public Rect ButtonSize = new Rect(0, 0, 200, 70);

    public float GapSize = 10;
    public string[] Buttons = new[] { "Start", "Option", "About", "Quit" };
    public string[] ButtonPosition = new[] { "1", "2", "3", "4", "5", "6" };
    public string nameGame = "Skipping Stone";

    public GUISkin SkinMenu;
    public GUISkin SkinPos;

    private Rect _menuBounds;

    private bool isPlay = false;

    void OnGUI()
    {
      
        int count = Buttons.Length;

        float width = ButtonSize.width;
        float height = ButtonSize.height * count + GapSize * Mathf.Max(count - 1, 0); 
        float x = (Screen.width - width) * 0.5f;
        float y = (Screen.height - height) * 0.5f;

        _menuBounds = new Rect(x, y, width, height + 10);

        if (null != SkinMenu)
            GUI.skin = SkinMenu;

        GUI.Label(new Rect(Screen.width / 3 - 50 , 0, 600, 150), "Skipping Stone");
       

        if (!isPlay)
        {
            GUI.BeginGroup(_menuBounds);

            int index = 0;
            foreach (string s in Buttons)
            {
                if (GUI.Button(new Rect(0, (ButtonSize.height + GapSize) * index, ButtonSize.width, ButtonSize.height), s))
                {
                    ClickHandler(index);
                }
                index++;
            }
        }
        else
        {
            if (null != SkinPos)
                GUI.skin = SkinPos;

            GUI.BeginGroup(new Rect(Screen.width / 3, Screen.height / 3 + 10, 1000, 1000));

            if (GUI.Button(new Rect(0, 0, 150, 150), ButtonPosition[0]))
            {
                ChoosePositionMap(0);
            }

            if (GUI.Button(new Rect(160, 0, 150, 150), ButtonPosition[1]))
            {
                ChoosePositionMap(1);
            }

            if (GUI.Button(new Rect(320, 0, 150, 150), ButtonPosition[2]))
            {
                ChoosePositionMap(2);
            }

            if (GUI.Button(new Rect(0, 160, 150, 150), ButtonPosition[3]))
            {
                ChoosePositionMap(3);
            }

            if (GUI.Button(new Rect(160, 160, 150, 150), ButtonPosition[4]))
            {
                ChoosePositionMap(4);
            }

            if (GUI.Button(new Rect(320, 160, 150, 150), ButtonPosition[5]))
            {
                ChoosePositionMap(5);
            }

            if (GUI.Button(new Rect(160, 330, 150, 40), "Back"))
            {
                ChoosePositionMap(6);
            }
       
        }


        GUI.EndGroup(); 
    }

    void ClickHandler(int index)
    {
        switch (index)
        {
            case 0:
                isPlay = true;
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                Application.Quit();
                break;
        }
    }

    void ChoosePositionMap(int index)
    {
        switch (index)
        {

            case 0:
                Application.LoadLevel(1);
                break;
            case 1:
                Application.LoadLevel(1);
                break;
            case 2:
                Application.LoadLevel(1);
                break;
            case 3:
                break;
            case 4:
                Application.LoadLevel(1);
                break;
            case 5:
                Application.LoadLevel(1);
                break;
            case 6:
                isPlay = false;
                break;
        }
    }
}

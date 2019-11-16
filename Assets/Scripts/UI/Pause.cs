using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public GUISkin menuSkin;
    public GUIStyle title;
    public Texture black, purple, white;
    public bool isPlaying = true, usingMouse = true, usingKeyboard = false;

    private Vector3 tmpMousePos;
    private Vector2 scrollPos = Vector2.zero;

    private string focusItem;
    private string[] pauseButtons = new string[] { "", "Play", "ExitToMenu" };

    public int selectionUpDown, selectionAcross;

    public ScreenState currentScreen = ScreenState.isPlaying;
    public ScreenState preivousScreen;
    public enum ScreenState
    {
        isPaused,
        isPlaying
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        KeyInputManager();
    }

    private void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        float i_H = 0, i_W = 0, i_scale = 0;

        GUI.skin = menuSkin;
        if (currentScreen != ScreenState.isPlaying)
        {
            GUI.Box(new Rect(scrW * 0, scrH * 0, Screen.width, Screen.height), "");
        }

        switch (currentScreen)
        {
            case ScreenState.isPaused:
                #region Paused
                i_H = 2;
                GUI.SetNextControlName(pauseButtons[1]);
                if (GUI.Button(new Rect(scrW * 4, scrH * i_H, scrW * 8, scrH), "Play"))
                {
                    Play();
                }
                i_H++;
                GUI.SetNextControlName(pauseButtons[2]);
                if (GUI.Button(new Rect(scrW * 4, scrH * i_H, scrW * 8, scrH), "Exit To Menu"))
                {
                    ExitToMenu();
                }

                if (usingKeyboard)
                {
                    selectionUpDown = Mathf.Clamp(selectionUpDown, 1, 3);
                    GUI.FocusControl(pauseButtons[selectionUpDown]);
                    focusItem = GUI.GetNameOfFocusedControl();
                }
                else
                {
                    selectionUpDown = Mathf.Clamp(selectionUpDown, 0, 3);
                    GUI.FocusControl(pauseButtons[0]);
                    focusItem = GUI.GetNameOfFocusedControl();
                }

                #endregion
                break;
            case ScreenState.isPlaying:
                break;
        }

        SwitchInputMethod();
    }

    private void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPlaying)
        {
            currentScreen = ScreenState.isPaused;
            isPlaying = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPlaying)
        {
            currentScreen = ScreenState.isPlaying;
            isPlaying = true;
        }
    }

    private void Play()
    {

        currentScreen = ScreenState.isPlaying;
        selectionAcross = 0;
        selectionUpDown = 0;
        isPlaying = true;
    }

    private void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void KeyInputManager()
    {
        if (currentScreen == ScreenState.isPlaying || currentScreen == ScreenState.isPaused)
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Invoke(focusItem, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectionUpDown--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectionUpDown++;
        }
    }

    private void SwitchInputMethod()
    {
        if (usingMouse)
        {
            Event e = Event.current;
            if (e.isKey && e.keyCode != KeyCode.None && e != null)
            {
                selectionAcross = 0;
                selectionUpDown = 0;
                usingMouse = false;
                usingKeyboard = true;
            }
        }

        if (tmpMousePos != Input.mousePosition)
        {
            Debug.Log("Mouse moved");
            selectionAcross = 0;
            selectionUpDown = 0;
            usingMouse = true;
            usingKeyboard = false;
            tmpMousePos = Input.mousePosition;
        }
    }
}

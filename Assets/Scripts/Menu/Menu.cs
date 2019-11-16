using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GUISkin menuSkin;
    public GUIStyle title, toggleButton;
    public Texture black, purple, white;
    public AudioSource music, sfx;
    public Light dirLight;
    public bool isPlaying = false, usingMouse = true, usingKeyboard = false, bindingKey = false, showResScroll = false;

    private Vector3 tmpMousePos;
    private Vector2 scrollPos = Vector2.zero;
    private float musicVol, brightness;
    private bool mute;

    private string focusItem;
    private string[] menuButtons = new string[] { "", "Play", "Options", "Controls", "Quit" };
    private string[,] optionButtons = new string[,] { { "", "Resolutions", "Fullscreen", "MuteToggle", "Music", "SFX", "Brightness", "Back" }, {"", "1", "2", "3", "4", "5", "6", "Apply"} };
    private string[] resolutionButton = new string[] { "", "Intial", "1920", "1600", "1366", "1280", "1152", "1024" };
    private string[,] controlButtons = new string[,] {{ "","Left", "Right", "Up", "Down", "LightAttack", "HeavyAttack", "Fire", "Back" },{ "","Interact", "Dash", "Sneak", "Ultimate", "Inventory", "QuickPotion", "Craft", "" }};


    public int selectionUpDown, selectionAcross, keysIndexToBind;

    public KeyCode /*0*/left, /*1*/right, /*2*/up, /*3*/down, /*4*/lightAttack, /*5*/heavyAttack, /*6*/fire, /*7*/interact,
                   /*8*/dash, /*9*/sneak, /*10*/ultimate, /*11*/inventory, /*12*/quickPotion, /*13*/craft;
    public List<KeyCode> keys;
    public KeyCode KeyBeingChanged;

    private int resWidth, resHeight, currentResIndex;
    private static int intialWidth, intialHeight;
    private bool fullscreen = true;
    private string[] resolutions; 
    private Resolution currentResolution;
    private enum Resolution
    {
        resIntial,
        res1920x1080,
        res1600x900,
        res1366x768,
        res1280x720,
        res1152x648,
        res1024x576
    }

    private ScreenState currentScreen;
    private ScreenState preivousScreen;
    private enum ScreenState
    {
        isMenu,
        isOptions,
        isControlOptions,
        isPaused,
        isPlaying
    }

    #region Code Comment / Issues
    // THINGS TO FIX
    // Keyboard selection only active if mouse is not used [DONE]
    // Switch clamp limits so when mouse is used selected button can be null (index of 0 or blank) [DONE]
    // when keyboard is used a button has to be selected [DONE]

    // mouse still selects buttons if mouse is on top of button when switching into usingKeyboard
    // Set buttons to labels when using keyboard
    // Can't Do this cause i then can't select the 'Buttons' using keys as the no longer exsist
    // Idea (Disable mouse selection when usingKeyboard)

    // Invoke function reset ketboard selection back to 0 or null / blank [DONE]
    // currently when changing from menu's the preivous selection index's stay the same [DONE]

    // Add Keybindings [DONE]
    // Add a function Onclick() wait for key press, switch keys[DONE]
    // [Ideas] 

    // Add Drop down for resolutions [DONE]
    // Add toggle for fullscreen/windowed [DONE]
    // Sliders [DONE]
    // Volume [DONE]
    // Brightness [DONE]

    // KeyBinding to A,S,D,W or Up,Down,Left,Right the selection will move

    // Adding key controls for mute and slider

    // Data Saving
    // Player Prefs
    // - keybindings
    // - Resolution
    // - Volume
    // - Windowed/Fullscreen
    // - Mute
    // XML (ask Manny)

    // Only Set intial resolution if current resolution != any of already availble resolution

    // BE A BOSS
    #endregion

    // Use this for initialization
    void Start()
    {
        intialHeight = Screen.height;
        intialWidth = Screen.width;

        resolutions = new string[] { "" + intialWidth + "x" + intialHeight, "1920x1080", "1600x900", "1366x768", "1280x720", "1152x648", "1024x576" };

        tmpMousePos = Input.mousePosition;

        if (PlayerPrefs.HasKey("Save"))
        {
            if (PlayerPrefs.GetInt("Mute") == 0)
            {
                // Music Volume
                music.volume = PlayerPrefs.GetFloat("MusicVolume");
                // SFX Volume
                sfx.volume = PlayerPrefs.GetFloat("SFXVolume");

                mute = false;
            }
            else
            {
                mute = true;
                music.volume = 0;
                sfx.volume = 0;
            }
            

            // Brightness
            dirLight.intensity = PlayerPrefs.GetFloat("Brightness");

            SetKeyBindings();
        }
        else
        {
            SetKeyBindings();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeysUsingList();
        KeyInputManager();

        if (!mute)
        {

        }
        else
        {
            music.volume = 0;
            sfx.volume = 0;
        }
    }

    private void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        float i_H, i_W, i_scale;

        GUI.skin = menuSkin;
        if (currentScreen != ScreenState.isPlaying)
        {
            GUI.Box(new Rect(scrW * 0, scrH * 0, Screen.width, Screen.height), "");
        }

        switch (currentScreen)
        {
            case ScreenState.isMenu: //======================================================================================================//
                #region Menu Buttons
                i_H = 2;
                i_W = 0;

                GUI.DrawTexture(new Rect(scrW * 3.9f, scrH * (i_H - 0.1f), scrW * 8.2f, scrH * 4.2f), purple);
                GUI.DrawTexture(new Rect(scrW * 4f, scrH * i_H, scrW * 8, scrH * 4), black);

                GUI.SetNextControlName(menuButtons[1]);
                if (GUI.Button(new Rect(scrW * 4, scrH * i_H, scrW * 8, scrH), "Play"))
                {
                    Play();
                }
                i_H++;
                GUI.SetNextControlName(menuButtons[2]);
                if (GUI.Button(new Rect(scrW * 4, scrH * i_H, scrW * 8, scrH), "Options"))
                {
                    Options();
                }
                i_H++;
                GUI.SetNextControlName(menuButtons[3]);
                if (GUI.Button(new Rect(scrW * 4, scrH * i_H, scrW * 8, scrH), "Controls"))
                {
                    Controls();
                }
                i_H++;
                GUI.SetNextControlName(menuButtons[4]);
                if (GUI.Button(new Rect(scrW * 4, scrH * i_H, scrW * 8, scrH), "Quit"))
                {
                    Quit();
                }

                // IF keyboard selection active
                // THEN
                if (usingKeyboard)
                {
                    selectionUpDown = Mathf.Clamp(selectionUpDown, 1, 4);
                    GUI.FocusControl(menuButtons[selectionUpDown]);
                    focusItem = GUI.GetNameOfFocusedControl();
                }
                else
                {
                    selectionUpDown = Mathf.Clamp(selectionUpDown, 0, 4);
                    GUI.FocusControl(menuButtons[0]);
                    focusItem = GUI.GetNameOfFocusedControl();
                }

                GUI.skin = null;
                #endregion 
                break;
            case ScreenState.isOptions: //===================================================================================================//
                #region Options

                GUI.Label(new Rect(scrW * 6, scrH, scrW * 4, scrH * 0.8f), "Options", title);

                GUI.DrawTexture(new Rect(scrW * (1 - 0.1f), scrH * (2- 0.1f), scrW * 14.2f, scrH * 6.1f), purple);
                GUI.DrawTexture(new Rect(scrW * 1f, scrH * 2, scrW * 14, scrH * 5.9f), black);

                i_W = 4;

                // Add Drop down for resolutions
                GUI.Label(new Rect(scrW * 4.5f, scrH * 2.1f, scrW * 4, scrH * 0.8f), "Resolution");
                GUI.SetNextControlName(optionButtons[0, 1]);
                if (GUI.Button(new Rect(scrW * 8f, scrH * 2.1f, scrW * i_W, scrH * 0.8f), "" + resolutions[currentResIndex]))
                {
                    scrollPos = Vector3.zero;
                    showResScroll = !showResScroll;
                }

                #region Resolution Drop Down
                if (showResScroll)
                {


                    scrollPos = GUI.BeginScrollView(new Rect(scrW * 8, scrH * 2.9f, scrW * i_W, scrH * 3), scrollPos, new Rect(0, 0, 0, scrH * ((System.Enum.GetValues(typeof(Resolution)).Length) * 0.8f)), false, true);
                    // If type do thing
                    i_H = 0;
                    GUI.SetNextControlName(resolutionButton[0]);
                    if (GUI.Button(new Rect(0, 0 * scrH + i_H * (scrH * 0.8f), scrW * i_W, scrH * 0.8f), "" + resolutions[0]))
                    {
                        currentResIndex = 0;
                        currentResolution = Resolution.resIntial;
                        showResScroll = false;
                    }
                    i_H++;
                    GUI.SetNextControlName(resolutionButton[1]);
                    if (GUI.Button(new Rect(0, 0 * scrH + i_H * (scrH * 0.8f), scrW * i_W, scrH * 0.8f), "" + resolutions[1]))
                    {
                        currentResIndex = 1;
                        currentResolution = Resolution.res1920x1080;
                        showResScroll = false;
                    }
                    i_H++;
                    GUI.SetNextControlName(resolutionButton[2]);
                    if (GUI.Button(new Rect(0, 0 * scrH + i_H * (scrH * 0.8f), scrW * i_W, scrH * 0.8f), "" + resolutions[2]))
                    {
                        currentResIndex = 2;
                        currentResolution = Resolution.res1600x900;
                        showResScroll = false;
                    }
                    i_H++;
                    GUI.SetNextControlName(resolutionButton[3]);
                    if (GUI.Button(new Rect(0, 0 * scrH + i_H * (scrH * 0.8f), scrW * i_W, scrH * 0.8f), "" + resolutions[3]))
                    {
                        currentResIndex = 3;
                        currentResolution = Resolution.res1366x768;
                        showResScroll = false;
                    }
                    i_H++;
                    GUI.SetNextControlName(resolutionButton[4]);
                    if (GUI.Button(new Rect(0, 0 * scrH + i_H * (scrH * 0.8f), scrW * i_W, scrH * 0.8f), "" + resolutions[4]))
                    {
                        currentResIndex = 4;
                        currentResolution = Resolution.res1280x720;
                        showResScroll = false;
                    }
                    i_H++;
                    GUI.SetNextControlName(resolutionButton[5]);
                    if (GUI.Button(new Rect(0, 0 * scrH + i_H * (scrH * 0.8f), scrW * i_W, scrH * 0.8f), "" + resolutions[5]))
                    {
                        currentResIndex = 5;
                        currentResolution = Resolution.res1152x648;
                        showResScroll = false;
                    }
                    i_H++;
                    GUI.SetNextControlName(resolutionButton[6]);
                    if (GUI.Button(new Rect(0, 0 * scrH + i_H * (scrH * 0.8f), scrW * i_W, scrH * 0.8f), "" + resolutions[6]))
                    {
                        currentResIndex = 6;
                        currentResolution = Resolution.res1024x576;
                        showResScroll = false;
                    }
                    GUI.EndScrollView();                    
                }
                #endregion

                i_H = 3f;
                GUI.Label(new Rect(scrW * 4.5f, scrH * (i_H - 0.1f), scrW * 4, scrH * 0.8f), "Fullscreen");
                i_H++;
                GUI.Label(new Rect(scrW * 4.5f, scrH * (i_H - 0.2f), scrW * 4, scrH * 0.8f), "Mute");
                i_H++;
                GUI.Label(new Rect(scrW * 4.5f, scrH * (i_H - 0.27f), scrW * 4, scrH * 0.8f), "Music Vol");
                i_H++;
                GUI.Label(new Rect(scrW * 4.5f, scrH * (i_H - 0.3f), scrW * 4, scrH * 0.8f), "SFX Vol");
                i_H++;
                GUI.Label(new Rect(scrW * 4.5f, scrH * (i_H - 0.4f), scrW * 4, scrH * 0.8f), "Brightness");

                if (!showResScroll)
                {
                    GUI.SetNextControlName(optionButtons[0, 2]);
                    // Fullscreen Toggle
                    if (GUI.Button(new Rect(scrW * 9.75f, scrH * 3, scrW * 0.5f, scrH * 0.5f), "", toggleButton))
                    {
                        FullScreen();
                    }

                    if (Screen.fullScreen == true)
                    {
                        GUI.DrawTexture(new Rect(scrW * 9.87f, scrH * 3.125f, scrW * 0.25f, scrH * 0.25f), white);
                    }

                    // Mute toggle
                    GUI.SetNextControlName(optionButtons[0, 3]);
                    if (GUI.Button(new Rect(scrW * 9.75f, scrH * 4, scrW * 0.5f, scrH * 0.5f), "", toggleButton))
                    {
                        mute = MuteToggle();
                    }

                    if (mute)
                    {
                        GUI.DrawTexture(new Rect(scrW * 9.87f, scrH * 4.125f, scrW * 0.25f, scrH * 0.25f), white);
                    }

                    // Music Volume
                    music.volume = GUI.HorizontalSlider(new Rect(scrW * 8, scrH * 5, scrW * 4, scrH), music.volume, 0.0f, 1.0f);
                }

                // SFX Volume
                sfx.volume = GUI.HorizontalSlider(new Rect(scrW * 8, scrH * 6, scrW * 4, scrH), sfx.volume, 0.0f, 1.0f);

                // Brightness
                dirLight.intensity = GUI.HorizontalSlider(new Rect(scrW * 8, scrH * 7, scrW * 4, scrH), dirLight.intensity, 0.0f, 1.0f);

                GUI.SetNextControlName(optionButtons[0,7]);
                if (GUI.Button(new Rect(scrW * 6, scrH * 8, scrW * 4, scrH * 0.8f), "Back"))
                {
                    Back();
                }

                GUI.SetNextControlName(optionButtons[1,7]);
                if (GUI.Button(new Rect(scrW * 11.1f, scrH * 8, scrW * 4, scrH * 0.8f), "Apply"))
                {
                    Apply();
                }

                if (selectionUpDown != 7)
                {
                    selectionAcross = Mathf.Clamp(selectionAcross, 0, 0);
                }
                else
                {
                    selectionAcross = Mathf.Clamp(selectionAcross, 0, 1);
                }

                if (usingKeyboard)
                {
                    selectionUpDown = Mathf.Clamp(selectionUpDown, 1, 7);
                    GUI.FocusControl(optionButtons[selectionAcross, selectionUpDown]);
                    focusItem = GUI.GetNameOfFocusedControl();
                }
                else
                {
                    selectionUpDown = Mathf.Clamp(selectionUpDown, 0, 7);
                    GUI.FocusControl(optionButtons[0, 0]);
                    focusItem = GUI.GetNameOfFocusedControl();
                }

                #endregion
                break;
            case ScreenState.isControlOptions: //===================================================================================================//
                #region Controls
                i_H = 0.5f;
                i_W = 1;
                i_scale = 0.2f;

                GUI.skin = null;
                GUI.Label(new Rect(scrW * 6, scrH * i_H, scrW * 4, scrH * 0.8f), "Key Bindings", title);
                GUI.skin = menuSkin;

                //GUI.DrawTexture(new Rect(scrW * (1 - 0.1f), scrH * (2 - 0.1f), scrW * 14.2f, scrH * 6.1f), purple);
                //GUI.DrawTexture(new Rect(scrW * 1f, scrH * 2, scrW * 14, scrH * 5.9f), black);

                #region KeyBinding Buttons
                i_H++;
                GUI.SetNextControlName(controlButtons[0, 1]);
                GUI.Label(new Rect(scrW, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Left");
                if (GUI.Button(new Rect(scrW * (i_W + 4), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + left))
                {
                    Left();
                }

                i_H++;
                GUI.SetNextControlName(controlButtons[0, 2]);
                GUI.Label(new Rect(scrW, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Right");
                if (GUI.Button(new Rect(scrW * (i_W + 4), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + right))
                {
                    Right();
                }

                i_H++;
                GUI.SetNextControlName(controlButtons[0, 3]);
                GUI.Label(new Rect(scrW, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Up");
                if (GUI.Button(new Rect(scrW * (i_W + 4), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + up))
                {
                    Up();
                }

                i_H++;
                GUI.SetNextControlName(controlButtons[0, 4]);
                GUI.Label(new Rect(scrW, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Down");
                if (GUI.Button(new Rect(scrW * (i_W + 4), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + down))
                {
                    Down();
                }

                i_H++;
                GUI.SetNextControlName(controlButtons[0, 5]);
                GUI.Label(new Rect(scrW, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Light Attack");
                if (GUI.Button(new Rect(scrW * (i_W + 4), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + lightAttack))
                {
                    LightAttack();
                }
                i_H++;
                GUI.SetNextControlName(controlButtons[0, 6]);
                GUI.Label(new Rect(scrW, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Heavy Attack");
                if (GUI.Button(new Rect(scrW * (i_W + 4), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + heavyAttack))
                {
                    HeavyAttack();
                }
                i_H++;
                GUI.SetNextControlName(controlButtons[0, 7]);
                GUI.Label(new Rect(scrW, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Fire");
                if (GUI.Button(new Rect(scrW * (i_W + 4), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + fire))
                {
                    Fire();
                }

                i_W = 9;
                i_H = 0.5f;
                i_H++;
                GUI.SetNextControlName(controlButtons[1, 1]);
                GUI.Label(new Rect(scrW * i_W, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Interact");
                if (GUI.Button(new Rect(scrW * (i_W + 3), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + interact))
                {
                    Interact();
                }
                i_H++;
                GUI.SetNextControlName(controlButtons[1, 2]);
                GUI.Label(new Rect(scrW * i_W, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Dash");
                if (GUI.Button(new Rect(scrW * (i_W + 3), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + dash))
                {
                    Dash();
                }
                i_H++;
                GUI.SetNextControlName(controlButtons[1, 3]);
                GUI.Label(new Rect(scrW * i_W, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Sneak");
                if (GUI.Button(new Rect(scrW * (i_W + 3), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + sneak))
                {
                    Sneak();
                }
                i_H++;
                GUI.SetNextControlName(controlButtons[1, 4]);
                GUI.Label(new Rect(scrW * i_W, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Ultimate");
                if (GUI.Button(new Rect(scrW * (i_W + 3), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + ultimate))
                {
                    Ultimate();
                }
                i_H++;
                GUI.SetNextControlName(controlButtons[1, 5]);
                GUI.Label(new Rect(scrW * i_W, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Inventory");
                if (GUI.Button(new Rect(scrW * (i_W + 3), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + inventory))
                {
                    Inventory();
                }
                i_H++;
                GUI.SetNextControlName(controlButtons[1, 6]);
                GUI.Label(new Rect(scrW * i_W, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Quick Potion");
                if (GUI.Button(new Rect(scrW * (i_W + 3), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + quickPotion))
                {
                    QuickPotion();
                }
                i_H++;
                GUI.SetNextControlName(controlButtons[1, 7]);
                GUI.Label(new Rect(scrW * i_W, scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "Craft");
                if (GUI.Button(new Rect(scrW * (i_W + 3), scrH * (i_H - i_scale), scrW * 4, scrH * 0.8f), "" + craft))
                {
                    Craft();
                }
                i_H++;
                GUI.SetNextControlName(controlButtons[0, 8]);
                if (GUI.Button(new Rect(scrW * 6, scrH * (i_H - 0.4f), scrW * 4, scrH * 0.8f), "Back"))
                {
                    Back();
                }
                #endregion

                selectionAcross = Mathf.Clamp(selectionAcross, 0, 1);

                if (selectionUpDown != 8)
                {
                    selectionAcross = Mathf.Clamp(selectionAcross, 0, 1);
                }
                else
                {
                    selectionAcross = Mathf.Clamp(selectionAcross, 0, 0);
                }

                if (usingKeyboard)
                {
                    selectionUpDown = Mathf.Clamp(selectionUpDown, 1, 8);
                    GUI.FocusControl(controlButtons[selectionAcross, selectionUpDown]);
                    focusItem = GUI.GetNameOfFocusedControl();
                }
                else
                {
                    selectionUpDown = Mathf.Clamp(selectionUpDown, 0, 8);
                    GUI.FocusControl(controlButtons[0,0]);
                    focusItem = GUI.GetNameOfFocusedControl();
                }

                #endregion
                break;
            case ScreenState.isPaused: //====================================================================================================//
                break;
            case ScreenState.isPlaying: //===================================================================================================//
                #region Playing               

                #endregion
                break;
        }

        SwitchInputMethod();

        if (bindingKey)
        {
            GetKey();
        }
    }

    #region Menu Button Invoke Functions
    private void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void Options()
    {
        selectionAcross = 0;
        selectionUpDown = 0;
        preivousScreen = currentScreen;
        currentScreen = ScreenState.isOptions;
        isPlaying = false;
    }

    private void Controls()
    {
        currentScreen = ScreenState.isControlOptions;
        selectionAcross = 0;
        selectionUpDown = 0;
        isPlaying = false;
    }

    private void Quit()
    { Application.Quit(); }

    private void Back()
    {
        currentScreen = preivousScreen;
        selectionAcross = 0;
        selectionUpDown = 0;
        isPlaying = false;
    }

    private void ExitToMenu()
    {
        currentScreen = ScreenState.isMenu;
        selectionAcross = 0;
        selectionUpDown = 0;
        isPlaying = false;
    }
    #endregion

    #region Option Buttons
    #endregion 

    #region Controls/KeyBinding

    private void Left()
    {
        keysIndexToBind = 0;
        bindingKey = true;
    }
    private void Right()
    {
        keysIndexToBind = 1;
        bindingKey = true;
    }
    private void Up()
    {
        keysIndexToBind = 2;
        bindingKey = true;
    }
    private void Down()
    {
        keysIndexToBind = 3;
        bindingKey = true;
    }
    private void LightAttack()
    {
        keysIndexToBind = 4;
        bindingKey = true;
    }
    private void HeavyAttack()
    {
        keysIndexToBind = 5;
        bindingKey = true;
    }
    private void Fire()
    {
        keysIndexToBind = 6;
        bindingKey = true;
    }
    private void Interact()
    {
        keysIndexToBind = 7;
        bindingKey = true;
    }
    private void Dash()
    {
        keysIndexToBind = 8;
        bindingKey = true;
    }
    private void Sneak()
    {
        keysIndexToBind = 9;
        bindingKey = true;
    }
    private void Ultimate()
    {
        keysIndexToBind = 10;
        bindingKey = true;
    }
    private void Inventory()
    {
        keysIndexToBind = 11;
        bindingKey = true;
    }
    private void QuickPotion()
    {
        keysIndexToBind = 12;
        bindingKey = true;
    }
    private void Craft()
    {
        keysIndexToBind = 13;
        bindingKey = true;
    }
    #endregion

    private void Apply()
    {
        switch (currentResolution)
        {
            case Resolution.resIntial:
                Screen.SetResolution(intialWidth, intialHeight, fullscreen);
                Screen.fullScreen = fullscreen;
                break;
            case Resolution.res1920x1080:
                Screen.SetResolution(1920,1080, fullscreen);
                Screen.fullScreen = fullscreen;
                break;
            case Resolution.res1600x900:
                Screen.SetResolution(1600, 900, fullscreen);
                Screen.fullScreen = fullscreen;
                break;
            case Resolution.res1366x768:
                Screen.SetResolution(1366, 768, fullscreen);
                Screen.fullScreen = fullscreen;
                break;
            case Resolution.res1280x720:
                Screen.SetResolution(1280, 720, fullscreen);
                Screen.fullScreen = fullscreen;
                break;
            case Resolution.res1152x648:
                Screen.SetResolution(1152, 648, fullscreen);
                Screen.fullScreen = fullscreen;
                break;
            case Resolution.res1024x576:
                Screen.SetResolution(1024, 576, fullscreen);
                Screen.fullScreen = fullscreen;
                break;
            default:
                break;
        }
        PlayerPrefs.SetString("Save", "");
        PlayerPrefs.SetInt("Mute", 0);

        if (!mute)
        {
            PlayerPrefs.SetFloat("MusicVolume", music.volume);
            PlayerPrefs.SetFloat("SFXVolume", sfx.volume);
            PlayerPrefs.SetInt("Mute", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 1);
        }

        PlayerPrefs.SetFloat("Brightness", dirLight.intensity);
        PlayerPrefs.SetInt("Width", Screen.width);
        PlayerPrefs.SetInt("Height", Screen.height);
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

    private void SetKeyBindings()
    {
        left = KeyCode.A;
        right = KeyCode.D;
        up = KeyCode.W;
        down = KeyCode.S;
        lightAttack = KeyCode.Mouse0;
        heavyAttack = KeyCode.Mouse1;
        fire = KeyCode.Space;
        interact = KeyCode.E;
        dash = KeyCode.LeftShift;
        sneak = KeyCode.LeftControl;
        ultimate = KeyCode.Q;
        inventory = KeyCode.Tab;
        quickPotion = KeyCode.R;
        craft = KeyCode.C;

        keys.Add(left);
        keys.Add(right);
        keys.Add(up);
        keys.Add(down);
        keys.Add(lightAttack);
        keys.Add(heavyAttack);
        keys.Add(fire);
        keys.Add(interact);
        keys.Add(dash);
        keys.Add(sneak);
        keys.Add(ultimate);
        keys.Add(inventory);
        keys.Add(quickPotion);
        keys.Add(craft);
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

    private void GetKey()
    {
        Event e = Event.current;

        for (int i = 0; i < keys.Count; i++)
        {
            if (e.keyCode == keys[i])
            {
                Debug.Log("Key Already Used");
                bindingKey = false;
                return;
            }
        }

        if (e.isKey && e.keyCode != KeyCode.None && e.keyCode != KeyCode.Return)
        {
            Debug.Log("Detected key code: " + e.keyCode);
            keys[keysIndexToBind] = e.keyCode;
            bindingKey = false;
        }
        else if (e.isMouse && e.keyCode != KeyCode.None)
        {
            Debug.Log("Detected key code: " + e.button);
            keys[keysIndexToBind] = e.keyCode;
            bindingKey = false;
        }
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

        if (currentScreen == ScreenState.isControlOptions || currentScreen == ScreenState.isOptions)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                selectionAcross--;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                selectionAcross++;
            }
        }
    }

    private void UpdateKeysUsingList()
    {
        left = keys[0];
        right = keys[1];
        up = keys[2];
        down = keys[3];
        lightAttack = keys[4];
        heavyAttack = keys[5];
        fire = keys[6];
        interact = keys[7];
        dash = keys[8];
        sneak = keys[9];
        ultimate = keys[10];
        inventory = keys[11];
        quickPotion = keys[12];
        craft = keys[13];
    }

    bool FullScreen()
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;
            return false;
        }
        else
        {
            Screen.fullScreen = true;
            return true;
        }

    }

    bool MuteToggle()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        if (mute)
        {
            music.volume = PlayerPrefs.GetFloat("MusicVolume", music.volume);
            sfx.volume = PlayerPrefs.GetFloat("SFXVolume", sfx.volume);           
            mute = false;
            return false;
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", music.volume);
            PlayerPrefs.SetFloat("SFXVolume", sfx.volume);            
            music.volume = 0;
            sfx.volume = 0;
            mute = true;            
            return true;
        }
    }
}

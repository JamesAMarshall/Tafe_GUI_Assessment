using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public Texture2D purple, line;
    public GUISkin skin, stats;
    public GUIStyle title, text;

    public Sprite[] helmet, head, body, weapon, shield, robot, icon;
    public bool showHelmet, showWeapon, showShield;
    [Range(0, 5)]
    public int i_Helmet;
    [Range(0, 3)]
    public int i_Head;
    [Range(0, 3)]
    public int i_Body;
    [Range(0, 6)]
    public int i_Weapon;
    [Range(0, 2)]
    public int i_Shield;
    [Range(0, 9)]
    public int i_Robot;
    [Range(0, 4)]
    public int i_Class;

    private GameObject player;
    private SpriteRenderer[] playerSprites;

    private float[] weaponDamge = new float[] { 5, 15, 15, 10, 20, 30, 25};
    private float[] shieldDefense = new float[] { 20,10, 0};
    private float health, damage, defence, stamina, agility;

    private string[] weapons = new string[] { "Knuckle \n Blade", "Broad \n Sword" , "Spear", "Short \n Sword", "Cleaver", "Battle \n Axe", "Long \n Sword"};
    private string[] shields = new string[] { "Round \n shieild", "Square \n Shield", "None" };
    private string[] descriptionTitles = new string[] { "Brawler", "Fighter", "Brute", "Rogue", "Lancer"};
    private string[] descriptions = new string[]{
        "A Brawler is well skilled in most combat feilds",
        "A fighter is able to carry a shield for extra defense",
        "A Brute utilises two handed weapons such as the axe, longsword or speer",
        "A Rogue is quick and agile, well versed with small weapons, but this gives the advantage of speed",
        "A Lancer has range at their side, they keep distance while waiting for the right times to strike"
    };

    private Class currentClass;
    private enum Class
    {
        Brawler,
        Fighter,
        Brute,
        Rogue,
        Lancer
    }

    // Use this for initialization
    void Start()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/Icons");
        helmet = Resources.LoadAll<Sprite>("Sprites/Helmets");
        head = Resources.LoadAll<Sprite>("Sprites/Heads");
        body = Resources.LoadAll<Sprite>("Sprites/Person");
        weapon = Resources.LoadAll<Sprite>("Sprites/Weapons");
        shield = Resources.LoadAll<Sprite>("Sprites/Shields");
        robot = Resources.LoadAll<Sprite>("Sprites/Robots");

        player = GameObject.Find("Player");
        playerSprites = player.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ClampIndexValues();
        SetCharacterSprites();
        SetClassStats();    
    }

    private void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        
        float scaleH = .5f;
        float scaleW = .5f;

        float i = 0;

        // new Rect(scrW, scrH, scrW, scrH)

        GUI.skin = skin;

        #region [OutLines]
        GUI.Box(new Rect(scrW * 0, scrH * 1.4f, scrW * 17,scrH * 0.1f),"");
        GUI.Box(new Rect(scrW * 10.5f, scrH * 1.4f, scrW * 0.12f, scrH * 5.6f), "");
        GUI.Box(new Rect(scrW * 0, scrH * 7f, scrW * 17, scrH * 0.1f), "");
        #endregion

        #region [Title]
        GUI.Box(new Rect(scrW * .5f, scrH * .5f , scrW * 10, scrH),"Customization", title);
        #endregion
        i = 1.5f;
        #region [Helmet]
        i++; // [Helmet] -------------------------------------------------
        if (GUI.Button(new Rect(scrW * .5f, scrH * i, scrW * scaleW, scrH * scaleH), "<"))
        {
            i_Helmet--;
        }

        GUI.Label(new Rect(scrW * .8f, scrH * i, scrW * 2.5f, scrH * scaleH), "Helmet");

        if (GUI.Button(new Rect(scrW * 3, scrH * i, scrW * scaleW, scrH * scaleH), ">"))
        {
            i_Helmet++;
        }
        #endregion
        #region [Head]
        i++; // [Head] -------------------------------------------------
        if (GUI.Button(new Rect(scrW * .5f, scrH * i, scrW * scaleW, scrH * scaleH), "<"))
        {
            i_Head--;
        }

        GUI.Label(new Rect(scrW * .8f, scrH * i, scrW * 2.5f, scrH * scaleH), "Head");

        if (GUI.Button(new Rect(scrW * 3, scrH * i, scrW * scaleW, scrH * scaleH), ">"))
        {
            i_Head++;
        }
        #endregion
        #region [Body]
        i++; // [Body] -------------------------------------------------
        if (GUI.Button(new Rect(scrW * .5f, scrH * i, scrW * scaleW, scrH * scaleH), "<"))
        {
            i_Body--;
        }

        GUI.Label(new Rect(scrW * .8f, scrH * i, scrW * 2.5f, scrH * scaleH), "Shirt");

        if (GUI.Button(new Rect(scrW * 3, scrH * i, scrW * scaleW, scrH * scaleH), ">"))
        {
            i_Body++;
        }
        #endregion
        #region [Robot]
        i++; // [Robot] -------------------------------------------------
        if (GUI.Button(new Rect(scrW * .5f, scrH * i, scrW * scaleW, scrH * scaleH), "<"))
        {
            i_Robot--;
        }

        GUI.Label(new Rect(scrW * .8f, scrH * i, scrW * 2.5f, scrH * scaleH), "Robot");

        if (GUI.Button(new Rect(scrW * 3, scrH * i, scrW * scaleW, scrH * scaleH), ">"))
        {
            i_Robot++;
        }
        #endregion    
        i = 0;
        i = 2f;
        #region [Toggles]
        GUI.skin = stats;
        GUI.DrawTexture(new Rect(scrW * 5.75f, scrH * (i - .05f), scrW * 1.75f, scrH * 0.65f), line);
        showHelmet = GUI.Toggle(new Rect(scrW * 7.25f, scrH * i, scrW * .2f, scrH * .22f), showHelmet, "");
        if (showHelmet)
            GUI.Label(new Rect(scrW * 7.5f, scrH * (i + .035f), scrW * 1f, scrH * .5f), "Helmet [On]");
        else
            GUI.Label(new Rect(scrW * 7.5f, scrH * (i + .035f), scrW * 1f, scrH * .5f), "Helmet [Off]");
        i = 5.35f;
        GUI.DrawTexture(new Rect(scrW * 5.1f, scrH * (i + .35f), scrW * 1.75f, scrH * -0.65f), line);
        showShield = GUI.Toggle(new Rect(scrW * 6.5f, scrH * i, scrW * .2f, scrH * .22f), showShield, "");
        if (showShield)
            GUI.Label(new Rect(scrW * 6.75f, scrH * (i + .035f), scrW * 1f, scrH * .5f), "Shield [On]");
        else
            GUI.Label(new Rect(scrW * 6.75f, scrH * (i + .035f), scrW * 1f, scrH * .5f), "Shield [Off]");
        i = 5f;
        GUI.DrawTexture(new Rect(scrW * 7.5f, scrH * (i + .3f), scrW * 1.75f, scrH * -0.65f), line);
        showWeapon = GUI.Toggle(new Rect(scrW * 9f, scrH * i, scrW * .2f, scrH * .22f), showWeapon, "");
        if (showWeapon)
            GUI.Label(new Rect(scrW * 9.25f, scrH * (i + .035f), scrW * 1f, scrH * .5f), "Weapon [On]");
        else
            GUI.Label(new Rect(scrW * 9.25f, scrH * (i + .035f), scrW * 1f, scrH * .5f), "Weapon [Off]");
        GUI.skin = skin;
        #endregion
        i = 0;
        
        i = 1.5f;
        #region [Stats]
        GUI.Label(new Rect(scrW * 12.7f, scrH * (i + .1f), scrW * 2, scrH), "STATS", title);
        i = 2;
        GUI.Label(new Rect(scrW * 10.5f, scrH * i, scrW * 2, scrH), "Class");
        GUI.skin = stats;
        i = 2.2f;
        GUI.Label(new Rect(scrW * 12.35f, scrH * i, scrW * 2.5f, scrH * scaleH), descriptionTitles[i_Class]);

        if (GUI.Button(new Rect(scrW * 12.3f, scrH * i, scrW * scaleW, scrH * scaleH), "<"))
        {
            i_Class--;
        }

        if (GUI.Button(new Rect(scrW * 14.4f, scrH * i, scrW * scaleW, scrH * scaleH), ">"))
        {
            i_Class++;
        }

        #region Bars
        GUI.skin = stats;
        i = 2.95f;
        GUI.Label(new Rect(scrW * 10.1f, scrH * (i), scrW * 2.5f, scrH * scaleH), "Health");
        GUI.DrawTexture(new Rect(scrW * 12.2f, scrH * (i + .1f), scrW * (health * 0.035f),scrH *  0.25f), purple);
        i += .4f;
        GUI.Label(new Rect(scrW * 10.1f, scrH * (i), scrW * 2.5f, scrH * scaleH), "Damage");
        GUI.DrawTexture(new Rect(scrW * 12.2f, scrH * (i + .1f), scrW * (damage * 0.035f), scrH * 0.25f), purple);
        i += .4f;
        GUI.Label(new Rect(scrW * 10.1f, scrH * (i), scrW * 2.5f, scrH * scaleH), "Defence");
        GUI.DrawTexture(new Rect(scrW * 12.2f, scrH * (i + .1f), scrW * (defence * 0.035f), scrH * 0.25f), purple);
        i += .4f;
        GUI.Label(new Rect(scrW * 10.1f, scrH * (i), scrW * 2.5f, scrH * scaleH), "Stamina");
        GUI.DrawTexture(new Rect(scrW * 12.2f, scrH * (i + .1f), scrW * (stamina * 0.035f), scrH * 0.25f), purple);
        i += .4f;
        GUI.Label(new Rect(scrW * 10.1f, scrH * (i), scrW * 2.5f, scrH * scaleH), "Agility");
        GUI.DrawTexture(new Rect(scrW * 12.2f, scrH * (i + .1f), scrW * (agility * 0.035f), scrH * 0.25f), purple);
        GUI.skin = skin;
        #endregion

        #endregion
        i = 0;
        GUI.skin = skin;

        #region [Weapon]
        i = 5f; // [Weapon] -------------------------------------------------
        GUI.Label(new Rect(scrW * 10.7f, scrH * (i), scrW * 2.5f, scrH * scaleH), "Weapon");
        i += .5f;
        GUI.skin = stats;
        GUI.Label(new Rect(scrW * 10.7f, scrH * (i), scrW * 2.5f, scrH * scaleH), weapons[i_Weapon]);
        GUI.skin = skin;
        if (GUI.Button(new Rect(scrW * 10.7f, scrH * i, scrW * scaleW, scrH * scaleH), "<"))
        {
            i_Weapon--;
        }

        if (GUI.Button(new Rect(scrW * 12.7f, scrH * i, scrW * scaleW, scrH * scaleH), ">"))
        {
            i_Weapon++;
        }
        #endregion
        #region [Shield]
        i = 5f; // [Shield] -------------------------------------------------
        GUI.Label(new Rect(scrW * 13.4f, scrH * (i), scrW * 2.5f, scrH * scaleH), "Shield");
        i += .5f;
        GUI.skin = stats;
        GUI.Label(new Rect(scrW * 13.4f, scrH * (i), scrW * 2.5f, scrH * scaleH), shields[i_Shield]);
        GUI.skin = skin;
        if (GUI.Button(new Rect(scrW * 13.3f, scrH * i, scrW * scaleW, scrH * scaleH), "<"))
        {
            i_Shield--;
        }

        if (GUI.Button(new Rect(scrW * 15.5f, scrH * i, scrW * scaleW, scrH * scaleH), ">"))
        {
            i_Shield++;
        }
        #endregion
        i = 0;
        i = 7.25f;
        #region [Description]
        GUI.Label(new Rect(scrW * 0f, scrH * i, scrW * 2, scrH * .5f), descriptionTitles[Mathf.Clamp(i_Class, 0, 4)]);
        i = 7.65f;
        GUI.Label(new Rect(scrW * 0.25f, scrH * i, scrW * 8, scrH * 3f), "-" + descriptions[Mathf.Clamp(i_Class, 0, 4)], text);
        #endregion
        i = 7.75f;
        #region [Confirm]
        if (GUI.Button(new Rect(scrW * 13.15f, scrH * i, scrW * 2, scrH * 0.75f), "Confirm"))
        {
            PlayerPrefs.SetInt("Helmet", i_Helmet);
            PlayerPrefs.SetInt("Head", i_Head);
            PlayerPrefs.SetInt("Body", i_Body);
            PlayerPrefs.SetInt("Weapon", i_Weapon);
            PlayerPrefs.SetInt("Shield", i_Shield);
            PlayerPrefs.SetInt("Robot", i_Robot);
            PlayerPrefs.SetInt("Class", i_Class);

            PlayerPrefs.SetFloat("Health", health);
            PlayerPrefs.SetFloat("Damage", damage);
            PlayerPrefs.SetFloat("Defense", defence);
            PlayerPrefs.SetFloat("Stamina", stamina);
            PlayerPrefs.SetFloat("Agility", agility);

            if(showHelmet)
                PlayerPrefs.SetInt("ShowHelmet", 1);
            else
                PlayerPrefs.SetInt("ShowHelmet", 0);

            if (showShield)
                PlayerPrefs.SetInt("ShowShield", 1);
            else
                PlayerPrefs.SetInt("ShowShield", 0);

            if (showWeapon)
                PlayerPrefs.SetInt("ShowWeapon", 1);
            else
                PlayerPrefs.SetInt("ShowWeapon", 0);

            SceneManager.LoadScene(2);
        }
        
        #endregion
        GUI.skin = null;
    }

    void SetCharacterSprites()
    {    
        playerSprites[0].sprite = helmet[Mathf.Clamp(i_Helmet, 0, 5)]; // Set Helmet
        playerSprites[1].sprite = head[Mathf.Clamp(i_Head, 0, 3)]; // Set Head
        playerSprites[2].sprite = body[Mathf.Clamp(i_Body, 0, 3)]; // Set Body
        playerSprites[3].sprite = weapon[Mathf.Clamp(i_Weapon, 0, 6)]; // Set Weapon
        playerSprites[4].sprite = shield[Mathf.Clamp(i_Shield, 0, 2)]; // Set Shield
        playerSprites[5].sprite = robot[Mathf.Clamp(i_Robot, 0, 9)]; // Set Robot
        playerSprites[6].sprite = weapon[Mathf.Clamp(i_Weapon, 0, 6)]; // Set Weapon
        playerSprites[7].sprite = shield[Mathf.Clamp(i_Shield, 0, 2)]; // Set Shield
        playerSprites[8].sprite = icon[Mathf.Clamp(i_Class, 0, 5)]; // Set Icon

        playerSprites[0].enabled = showHelmet ? true : false; // Toggle the Helmet Render on/off
        playerSprites[3].enabled = showWeapon ? true : false; // Toggle the Weapon Render on/off
        playerSprites[4].enabled = showShield ? true : false; // Toggle the Shield Render on/off
    }

    void ClampIndexValues()
    {
        i_Helmet = Mathf.Clamp(i_Helmet, 0, 5); // Clamp Helmet
        i_Head = Mathf.Clamp(i_Head, 0, 3); // Clamp Head
        i_Body = Mathf.Clamp(i_Body, 0, 3); // Clamp Body       
        i_Weapon = Mathf.Clamp(i_Weapon, 0, 6); // Clamp Weapon       
        i_Shield = Mathf.Clamp(i_Shield, 0, 2); // Clamp Shield
        i_Robot = Mathf.Clamp(i_Robot, 0, 9); // Clamp Shield
        i_Class = Mathf.Clamp(i_Class, 0, 4);// Clamp Icon
    }

    void SetClassStats()
    {
        switch (i_Class)
        {
            case 0:
                currentClass = Class.Brawler;
                break;
            case 1:
                currentClass = Class.Fighter;
                break;
            case 2:
                currentClass = Class.Brute;
                break;
            case 3:
                currentClass = Class.Rogue;
                break;
            case 4:
                currentClass = Class.Lancer;
                break;
        }

        switch (currentClass)
        {
            case Class.Brawler:
                health = 100;
                damage = 50 + weaponDamge[i_Weapon];
                defence = 50 + shieldDefense[i_Shield];
                stamina = 70;
                agility = 50 - ((shieldDefense[i_Shield] + weaponDamge[i_Weapon]) * 0.5f);
                break;
            case Class.Fighter:
                health = 100;
                damage = 40 + weaponDamge[i_Weapon];
                defence = 50 + shieldDefense[i_Shield];
                stamina = 50;
                agility = 50 - ((shieldDefense[i_Shield] + weaponDamge[i_Weapon]) * 0.5f);
                break;
            case Class.Brute:
                health = 100;
                damage = 20 + weaponDamge[i_Weapon];
                defence = 70 + shieldDefense[i_Shield];
                stamina = 75;
                agility = 20 - ((shieldDefense[i_Shield] + weaponDamge[i_Weapon]) * 0.5f);
                break;
            case Class.Rogue:
                health = 100;
                damage = 60 + weaponDamge[i_Weapon];
                defence = 20 + shieldDefense[i_Shield];
                stamina = 100;
                agility = 80 - ((shieldDefense[i_Shield] + weaponDamge[i_Weapon]) * 0.5f);
                break;
            case Class.Lancer:
                health = 100;
                damage = 50 + weaponDamge[i_Weapon];
                defence = 30 + shieldDefense[i_Shield];
                stamina = 70;
                agility = 60 - ((shieldDefense[i_Shield] + weaponDamge[i_Weapon]) * 0.5f);
                break;
        }
    }
}

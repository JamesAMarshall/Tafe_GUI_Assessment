using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Item selectedItem;
    public GUISkin invSkin;
    public GUIStyle textSize;
    public Vector2 scrollPos = Vector2.zero;
    public Inventory inv;


    private Texture2D[][] itemIcons = new Texture2D[2][];
    private Texture2D[] on, off;
    private Texture2D black, purple;

    private float scrW = Screen.width / 16;
    private float scrH = Screen.height / 9;
    private string[] itemTypeCategory = new string[] { "Shop: Weapons", "Shop: Armour", "Shop: Craftables", "Shop: Valuable", "Shop: Potion", "Shop: Consumable", "Shop: Quests" };
    
    public int i_itemType;

    private Item[] BlankShop, itemShop, questShop, consumableShop, potionShop, valuablesShop, craftablesShop, armourShop, weaponShop;

    private ItemType currentType = ItemType.Weapon;

    // THINGS
    /*
     I could have an array of items, to dicate what items are sold at the shop depending on what the catergory is
     Reference the item sold by the index of the respective array
        eg. Greatsword is 2 in the array
            weapon array.name = 
            .texture for the icon
            .description
            .price

    // Add an amount to the shop so you can buy multiple in one click
        - Have a textfeild that sets an int
        - when buy it checks the amount to give
    */

        

    // Use this for initialization
    void Start()
    {
        weaponShop = new Item[] { ItemDatabase.createItem(100), ItemDatabase.createItem(101), ItemDatabase.createItem(102), ItemDatabase.createItem(103), ItemDatabase.createItem(104), ItemDatabase.createItem(105) };
        armourShop = new Item[] { ItemDatabase.createItem(200), ItemDatabase.createItem(201) };
        craftablesShop = new Item[] { ItemDatabase.createItem(300), ItemDatabase.createItem(301), ItemDatabase.createItem(302), ItemDatabase.createItem(303) };
        valuablesShop = new Item[] { ItemDatabase.createItem(400), ItemDatabase.createItem(401), ItemDatabase.createItem(402), ItemDatabase.createItem(403) };
        potionShop = new Item[] { ItemDatabase.createItem(500), ItemDatabase.createItem(501), ItemDatabase.createItem(502), ItemDatabase.createItem(503) };
        consumableShop = new Item[] { ItemDatabase.createItem(600), ItemDatabase.createItem(601) };
        questShop = new Item[] { ItemDatabase.createItem(700), ItemDatabase.createItem(701), ItemDatabase.createItem(702) };

        inv = FindObjectOfType<Inventory>();

        on = Resources.LoadAll<Texture2D>("Textures/Icons/ON");
        off = Resources.LoadAll<Texture2D>("Textures/Icons/OFF");

        itemIcons[0] = on;
        itemIcons[1] = off;

        black = Resources.Load("Textures/UI/Black") as Texture2D;
        purple = Resources.Load("Textures/UI/Hover") as Texture2D;

        itemShop = weaponShop;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShopWindow(int windowID)
    {
        float i = 0;
        float iH = 0.8f;
        float igapH = .035f;
        GUI.skin = invSkin;

        GUI.DrawTexture(new Rect(scrW * 0f, scrH * 0f, scrW * 8f, scrH * 6f), purple);
        GUI.DrawTexture(new Rect(scrW * .6f, scrH * .1f, scrW * 7.3f, scrH * 5.8f), black);

        #region [Item Types]
        i = .1f;
        if (currentType == ItemType.Weapon)
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[0][0]);
        else
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[1][0]);
        i += iH + igapH;
        if (currentType == ItemType.Armour)
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[0][1]);
        else
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[1][1]);
        i += iH + igapH;
        if (currentType == ItemType.Craftable)
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[0][2]);
        else
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[1][2]);
        i += iH + igapH;
        if (currentType == ItemType.Valuables)
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[0][3]);
        else
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[1][3]);
        i += iH + igapH;
        if (currentType == ItemType.Potion)
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[0][4]);
        else
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[1][4]);
        i += iH + igapH;
        if (currentType == ItemType.Consumable)
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[0][5]);
        else
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[1][5]);
        i += iH + igapH;
        if (currentType == ItemType.Quest)
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[0][6]);
        else
            GUI.DrawTexture(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), itemIcons[1][6]);

        i = .1f;
        GUI.color = new Color(1, 1, 1, 0.75f);
        if (currentType != ItemType.Weapon)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
            { 
                currentType = ItemType.Weapon;
                i_itemType = 0;
                itemShop = weaponShop;
                }
        i += iH + igapH;

        if (currentType != ItemType.Armour)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
            {
                currentType = ItemType.Armour;
                i_itemType = 1;
                itemShop = armourShop;
            }
        i += iH + igapH;

        if (currentType != ItemType.Craftable)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
            {
                currentType = ItemType.Craftable;
                i_itemType = 2;
                itemShop = craftablesShop;
            }
        i += iH + igapH;

        if (currentType != ItemType.Valuables)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
            {
                currentType = ItemType.Valuables;
                i_itemType = 3;
                itemShop = valuablesShop;
            }
        i += iH + igapH;

        if (currentType != ItemType.Potion)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
            {
                currentType = ItemType.Potion;
                i_itemType = 4;
                itemShop = potionShop;
            }
        i += iH + igapH;

        if (currentType != ItemType.Consumable)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
            {
                currentType = ItemType.Consumable;
                itemShop = consumableShop;
                i_itemType = 5;
            }
        i += iH + igapH;

        if (currentType != ItemType.Quest)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
            {
                currentType = ItemType.Quest;
                i_itemType = 6;
                itemShop = questShop;
            }
        GUI.color = new Color(1, 1, 1, 1f);
        #endregion

        #region [Shop List]
        // Background

        
        scrollPos = GUI.BeginScrollView(new Rect(scrW * .625f, scrH * 3f, scrW * 7.25f, scrH * 2.9f), scrollPos, new Rect(0, 0, 0, scrH * (itemShop.Length * 1f)), false, true);
        for (int s = 0; s < itemShop.Length; s++)
        {
            if (GUI.Button(new Rect(scrW * .1f, scrH * (1f * (s /*+ offset*/)), scrW * 5.9f, scrH * 1f), itemShop[s].Name))
            {
                selectedItem = itemShop[s];
            }
            GUI.DrawTexture(new Rect(scrW * 6, scrH * (1f * (s)), scrW * 1f, scrH * 1f), itemShop[s].Icon);
        }
        GUI.EndScrollView();
        // List of Items
        #endregion

        #region [Selected Item Display]
        // GUI.DrawTexture(new Rect(scrW * .5f,scrH * 0f, scrW * 7.5f, scrH * 6f),purple);

        GUI.Label(new Rect(scrW * 2.25f, scrH * 0.1f, scrW * 4, scrH * .75f), itemTypeCategory[i_itemType]);

        if (selectedItem != null)
        {
            // Selected item
            GUI.DrawTexture(new Rect(scrW * .725f, scrH * .75f, scrW * 2.2f, scrH * 2.2f), purple);
            GUI.DrawTexture(new Rect(scrW * .825f, scrH * .85f, scrW * 2, scrH * 2), black);
            GUI.DrawTexture(new Rect(scrW * .825f, scrH * .85f, scrW * 2, scrH * 2), selectedItem.Icon);

            GUI.Label(new Rect(scrW * 3.1f, scrH * .5f, scrW, scrH * 1), "Name: " + selectedItem.Name, textSize);
            GUI.Label(new Rect(scrW * 3.1f, scrH * 1f, scrW, scrH * 1), "Value: " + selectedItem.Value, textSize);
            GUI.Label(new Rect(scrW * 3.1f, scrH * 1.5f, scrW, scrH * 1), "Price: " + selectedItem.Price, textSize);


            if (GUI.Button(new Rect(scrW * 6.5f, scrH * 1.5f, scrW * 1, scrH * 1), "Buy"))
                inv.inv.Add(selectedItem);
        }
        else
        {
            // Selected item
            GUI.DrawTexture(new Rect(scrW * .725f, scrH * .75f, scrW * 2.2f, scrH * 2.2f), purple);
            GUI.DrawTexture(new Rect(scrW * .825f, scrH * .85f, scrW * 2, scrH * 2), black);

            GUI.Label(new Rect(scrW * 3.1f, scrH * .5f, scrW, scrH * 1), "Name: ", textSize);
            GUI.Label(new Rect(scrW * 3.1f, scrH * 1f, scrW, scrH * 1), "Value: ", textSize);
            GUI.Label(new Rect(scrW * 3.1f, scrH * 1.5f, scrW, scrH * 1), "Price: ", textSize);

            //GUI.Button(new Rect(scrW * 6.5f, scrH * 1.5f, scrW * 1, scrH * 1), "Buy");

        }
        #endregion

        GUI.DragWindow();
    }
}


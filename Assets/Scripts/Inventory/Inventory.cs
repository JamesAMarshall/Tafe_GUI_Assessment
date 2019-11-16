using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<Item> inv = new List<Item>();
    public List<Item> viewedItems = new List<Item>();

    public Item selectedItem;
    public GUISkin invSkin;
    public GUIStyle textSize;
    public Vector2 scrollPos = Vector2.zero;

    public string idNum;
    public int money, slots;

    private Chest chest;

    private Texture2D[][] itemIcons = new Texture2D[2][];
    private Texture2D[] on, off;
    private Texture2D black, purple;
    private Rect invRect;

    private float scrW = Screen.width / 16;
    private float scrH = Screen.height / 9;
    private int selectedIndex, selectedCategory;

    private ItemType currentType = ItemType.Weapon;

    // Use this for initialization
    void Start()
    {
        chest = FindObjectOfType<Chest>();

        inv.Add(ItemDatabase.createItem(100));
        invRect = new Rect(scrW * .5f, scrH * 0f, scrW * 7.5f, scrH * 6f);

        on = Resources.LoadAll<Texture2D>("Textures/Icons/ON");
        off = Resources.LoadAll<Texture2D>("Textures/Icons/OFF");

        itemIcons[0] = on;
        itemIcons[1] = off;

        black = Resources.Load("Textures/UI/Black") as Texture2D;
        purple = Resources.Load("Textures/UI/Hover") as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        viewedItems = inv.Where(item => item.type == currentType).ToList();
    }

    public void InventoryWindow(int windowID)
    {
        float i = 0;
        float iH = 0.8f;
        float igapH = .035f;
        GUI.skin = invSkin;

        GUI.DrawTexture(new Rect(scrW * 0f, scrH * 0f, scrW * 8f, scrH * 6f), purple);

        // GUI.DrawTexture(new Rect(scrW * .1f, scrH * .1f, scrW * 7.8f, scrH * 5.8f), black);
        #region [Item Types]
        i = .1f;
        if(currentType == ItemType.Weapon)
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
                currentType = ItemType.Weapon;
        i += iH + igapH;

        if (currentType != ItemType.Armour)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
                currentType = ItemType.Armour;
        i += iH + igapH;

        if (currentType != ItemType.Craftable)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
                currentType = ItemType.Craftable;
        i += iH + igapH;

        if (currentType != ItemType.Valuables)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
                currentType = ItemType.Valuables;
        i += iH + igapH;

        if (currentType != ItemType.Potion)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
                currentType = ItemType.Potion;
        i += iH + igapH;

        if (currentType != ItemType.Consumable)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
                currentType = ItemType.Consumable;
        i += iH + igapH;

        if (currentType != ItemType.Quest)
            if (GUI.Button(new Rect(scrW * 0, scrH * i, scrW * 0.5f, scrH * iH), ""))
                currentType = ItemType.Quest;
        GUI.color = new Color(1, 1, 1, 1f);
        #endregion

        #region [Inv List]
        // Background
        // GUI.DrawTexture(new Rect(scrW * .5f,scrH * 0f, scrW * 7.5f, scrH * 6f),purple);
        GUI.DrawTexture(new Rect(scrW * .6f, scrH * .1f, scrW * 7.3f, scrH * 5.8f), black);

        if (viewedItems.Count <= 11)
        {
            for (int a = 0; a < viewedItems.Count; a++)
            {
                // If type do thing
                if (GUI.Button(new Rect(scrW * 0.6f, 0.1f * scrH + a * (scrH * 0.5f), scrW * 3.5f, scrH * 0.5f), viewedItems[a].Name))
                {
                    selectedItem = viewedItems[a];                  
                }
            }
        }
        else
        {
            scrollPos = GUI.BeginScrollView(new Rect(scrW * 0f, scrH * 0, scrW * 4f, scrH * 5.9f), scrollPos, new Rect(0,0,0, scrH * inv.Count * 0.5f),false,true);

            for (int a = 0; a < viewedItems.Count; a++)
            {
                // If type do thing
                if (GUI.Button(new Rect(scrW * 0.6f, 0.1f * scrH + a * (scrH * 0.5f), scrW * 3.5f, scrH * 0.5f), viewedItems[a].Name))
                {
                    selectedItem = viewedItems[a];
                }
            }

            GUI.EndScrollView();
        }

        // List of Items
        #endregion

        #region [Selected Item Display]
        GUI.DrawTexture(new Rect(scrW * 4f, scrH * 0f, scrW * 4f, scrH * 6f), purple);
        GUI.DrawTexture(new Rect(scrW * 4.1f, scrH * .1f, scrW * 3.8f, scrH * 5.8f), black);

        if (selectedItem != null)
        {
            // Icon
            GUI.DrawTexture(new Rect(scrW * 4.5f, scrH * .5f, scrW * 3f, scrH * 3f), purple);
            GUI.DrawTexture(new Rect(scrW * 4.6f, scrH * .6f, scrW * 2.8f, scrH * 2.8f), black);
            GUI.DrawTexture(new Rect(scrW * 4.6f, scrH * .6f, scrW * 2.8f, scrH * 2.8f), selectedItem.Icon);
            // Stats
            GUI.Label(new Rect(scrW * 4.5f, scrH * 3.5f, scrW * 3.4f, scrH * .25f), "Name: " + selectedItem.Name, textSize);
            GUI.Label(new Rect(scrW * 4.5f, scrH * 3.75f, scrW * 3.4f, scrH * .25f), "Value: " + selectedItem.Value, textSize);
            GUI.Label(new Rect(scrW * 4.5f, scrH * 4f, scrW * 3.1f, scrH * .25f), "Description: " + selectedItem.Description, textSize);
            // Button options
            if (chest.open)
            {
                if (GUI.Button(new Rect(scrW * 6.9f,scrH * 5.5f,scrW * 1,scrH * .5f),"Store"))
                {
                    chest.chestStorage.Add(selectedItem);
                    inv.Remove(selectedItem);
                    selectedItem = null;
                }
            }
        }


        #endregion

        GUI.DragWindow();
    }
}
 
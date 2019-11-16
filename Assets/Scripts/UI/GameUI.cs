using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Texture2D purple;
    public GUISkin stats, invSkin;

    public Inventory inventory;
    public Shop shop;
    public Chest chest;

    private Player player;
    private Pause pause;
    private Rect statsWindow, inventoryWindow, shopWindow, chestWindow;

    private float scrW, scrH;
    public bool showInventory, showStats, showShop, showChest;

    // Use this for initialization
    void Start()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;

        statsWindow = new Rect(scrW * 10.8f, scrH * 6.3f, scrW * 5.3f, scrH * 2.7f);
        inventoryWindow = new Rect(scrW * 0.00f, scrH * 0, scrW * 8, scrH * 6f);
        shopWindow = new Rect(scrW * 8.1f, scrH * 0, scrW * 8, scrH * 6);
        chestWindow = new Rect(scrW * 8.1f, scrH * 0, scrW * 8, scrH * 6);

        pause = GetComponent<Pause>();
        shop = GetComponent<Shop>();
        player = FindObjectOfType<Player>();
        chest = FindObjectOfType<Chest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pause.currentScreen == Pause.ScreenState.isPlaying)
        {
            Toggles();
        }


    }

    private void OnGUI()
    {
        if (pause.currentScreen == Pause.ScreenState.isPlaying)
        {
            GUI.skin = invSkin;
            if (showInventory)
                inventoryWindow = ClampToScreen(GUI.Window(1, inventoryWindow, inventory.InventoryWindow, ""));
            if (showStats)
                statsWindow = ClampToScreen(GUI.Window(0, statsWindow, StatsWindow, ""));
            if (showShop)
                shopWindow = ClampToScreen(GUI.Window(2, shopWindow, shop.ShopWindow, "SHOP"));
            if (chest.open)
                chestWindow = ClampToScreen(GUI.Window(2, chestWindow, chest.chestWindow, "CHEST"));
            GUI.skin = null;
        }
    }

    void Toggles()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            showInventory = !showInventory;
        if (Input.GetKeyDown(KeyCode.I))
            showStats = !showStats;
        if (Input.GetKeyDown(KeyCode.O))
            if (showChest)
            {
                showChest = false;
                showShop = !showShop;
            }
            else
            {
                showShop = !showShop;
            }
        if (Input.GetKeyDown(KeyCode.P))
            if (showShop)
            {
                showShop = false;
                showChest = !showChest;
            }
            else
            {
                showChest = !showChest;
            }
    }

    void StatsWindow(int windowID)
    {
        float i;

        GUI.skin = stats;
        i = .2f;
        GUI.Label(new Rect(scrW * 0f, scrH * (i), scrW * 1.5f, scrH * 0.5f), "Health");
        player.health = GUI.HorizontalSlider(new Rect(scrW * 1.5f, scrH * (i + .2f), scrW * 3.5f, scrH * 0.5f), player.health, 0, 100);
        i += .4f;
        GUI.Label(new Rect(scrW * 0f, scrH * (i), scrW * 1.5f, scrH * 0.5f), "Health");
        GUI.DrawTexture(new Rect(scrW * 1.5f, scrH * (i + .1f), scrW * (player.health * 0.035f), scrH * 0.25f), purple);
        i += .4f;
        GUI.Label(new Rect(scrW * 0f, scrH * (i), scrW * 1.5f, scrH * 0.5f), "Damage");
        GUI.DrawTexture(new Rect(scrW * 1.5f, scrH * (i + .1f), scrW * (player.damage * 0.035f), scrH * 0.25f), purple);
        i += .4f;
        GUI.Label(new Rect(scrW * 0f, scrH * (i), scrW * 1.5f, scrH * 0.5f), "Defence");
        GUI.DrawTexture(new Rect(scrW * 1.5f, scrH * (i + .1f), scrW * (player.defense * 0.035f), scrH * 0.25f), purple);
        i += .4f;
        GUI.Label(new Rect(scrW * 0f, scrH * (i), scrW * 1.5f, scrH * 0.5f), "Stamina");
        GUI.DrawTexture(new Rect(scrW * 1.5f, scrH * (i + .1f), scrW * (player.stamina * 0.035f), scrH * 0.25f), purple);
        i += .4f;
        GUI.Label(new Rect(scrW * 0f, scrH * (i), scrW * 1.5f, scrH * 0.5f), "Agility");
        GUI.DrawTexture(new Rect(scrW * 1.5f, scrH * (i + .1f), scrW * (player.agility * 0.035f), scrH * 0.25f), purple);
        GUI.skin = null;

        GUI.DragWindow();
    }


    Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
}

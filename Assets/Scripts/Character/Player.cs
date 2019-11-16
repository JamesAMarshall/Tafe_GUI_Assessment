using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] helmet, head, body, weapon, shield, robot, icon;

    public int i_Helmet, i_Head, i_Body, i_Weapon, i_Shield, i_Robot, i_Class;
    [Range(0,100)]
    public float health, damage, defense, stamina, agility;
    public bool showHelmet, showWeapon, showShield;
    public SpriteRenderer[] playerSprites;

    private Transform left;
    private Transform right;

    private float speed = 10;

    public Facing facing;
    public enum Facing
    {
        Left,
        Right,
        Up,
        Down
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

        i_Helmet = PlayerPrefs.GetInt("Helmet");
        i_Head = PlayerPrefs.GetInt("Head");
        i_Body = PlayerPrefs.GetInt("Body");
        i_Weapon = PlayerPrefs.GetInt("Weapon");
        i_Shield = PlayerPrefs.GetInt("Shield");
        i_Robot = PlayerPrefs.GetInt("Robot");
        i_Class = PlayerPrefs.GetInt("Class");

        health = PlayerPrefs.GetFloat("Health");
        damage = PlayerPrefs.GetFloat("Damage");
        defense = PlayerPrefs.GetFloat("Defense");
        stamina = PlayerPrefs.GetFloat("Stamina");
        agility = PlayerPrefs.GetFloat("Agility");

        if (PlayerPrefs.GetInt("ShowHelmet") == 1)
            showShield = true;
        else
            showShield = false;

        if (PlayerPrefs.GetInt("ShowShield") == 1)
            showShield = true;
        else
            showShield = false;

        if (PlayerPrefs.GetInt("ShowWeapon") == 1)
            showShield = true;
        else
            showShield = false;

        playerSprites = GetComponentsInChildren<SpriteRenderer>();

        playerSprites[0].sprite = helmet[Mathf.Clamp(i_Helmet, 0, 5)]; // Set Helmet
        playerSprites[1].sprite = head[Mathf.Clamp(i_Head, 0, 3)]; // Set Head
        playerSprites[2].sprite = body[Mathf.Clamp(i_Body, 0, 3)]; // Set Body
        playerSprites[3].sprite = weapon[Mathf.Clamp(i_Weapon, 0, 6)]; // Set Weapon
        playerSprites[4].sprite = shield[Mathf.Clamp(i_Shield, 0, 2)]; // Set Shield
        playerSprites[5].sprite = robot[Mathf.Clamp(i_Robot, 0, 9)]; // Set Robot

        playerSprites[0].enabled = showHelmet ? true : false; // Toggle the Helmet Render on/off
        playerSprites[3].enabled = showWeapon ? true : false; // Toggle the Weapon Render on/off
        playerSprites[4].enabled = showShield ? true : false; // Toggle the Shield Render on/off
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //SetFacing();
    }

    void SetFacing()
    {
        switch (facing)
        {
            case Facing.Left:
                break;
            case Facing.Right:

                break;
            case Facing.Up:
                break;
            case Facing.Down:
                break;
        }
    }

    void Movement()
    {
        Vector3 tempPos = transform.position;

        float inputX = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float inputZ = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        tempPos.x += inputX;
        tempPos.z += inputZ;

        transform.position = tempPos;
    }


    void SetRight()
    {

    }
}

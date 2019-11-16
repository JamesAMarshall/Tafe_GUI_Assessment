using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : MonoBehaviour
{
    [Range(0,3)]
    public int i_Head;
    [Range(0, 1)]
    public int i_HeadState;
    [Range(0, 3)]
    public int i_Body;
    [Range(0, 3)]
    public int i_BodyState;

    private Sprite[] head00, head01, head02, head03, body00, body01, body02, body03;
    private Sprite[][] heads = new Sprite [4][];
    private Sprite[][] body = new Sprite[4][];

    private SpriteRenderer[] portraitSprites;
    private Player player;
    private SpriteRenderer back;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        back = GameObject.Find("Back").GetComponent<SpriteRenderer>();
        head00 = Resources.LoadAll<Sprite>("Sprites/Head00(Icon)");
        head01 = Resources.LoadAll<Sprite>("Sprites/Head01(Icon)");
        head02 = Resources.LoadAll<Sprite>("Sprites/Head02(Icon)");
        head03 = Resources.LoadAll<Sprite>("Sprites/Head03(Icon)");
        body00 = Resources.LoadAll<Sprite>("Sprites/Shirt00(Icon)");
        body01 = Resources.LoadAll<Sprite>("Sprites/Shirt01(Icon)");
        body02 = Resources.LoadAll<Sprite>("Sprites/Shirt02(Icon)");
        body03 = Resources.LoadAll<Sprite>("Sprites/Shirt03(Icon)");

        portraitSprites = GetComponentsInChildren<SpriteRenderer>();

        heads[0] = head00;
        heads[1] = head01;
        heads[2] = head02;
        heads[3] = head03;

        body[0] = body00;
        body[1] = body01;
        body[2] = body02;
        body[3] = body03;

        i_Head = PlayerPrefs.GetInt("Head");
        i_Body = PlayerPrefs.GetInt("Body");
    }

    // Update is called once per frame
    void Update()
    {
        SetSprites();

        if (player.health <= 25)
        {
            i_BodyState = 3;
            i_HeadState = 1;
            back.color = new Color(.9f,0,0);
        }
        else if (player.health <= 50)
        {
            i_BodyState = 3;
            i_HeadState = 0;
            back.color = new Color(.6f, 0, 0);
        }
        else if (player.health <= 75)
        {
            i_BodyState = 2;
            i_HeadState = 0;
            back.color = new Color(.4f, 0, 0);
        }
        else if (player.health <= 90)
        {
            i_BodyState = 1;
            i_HeadState = 0;
            back.color = new Color(.2f, 0, 0);
        }
        else if (player.health >= 90)
        {
            i_BodyState = 0;
            i_HeadState = 0;
            back.color = new Color(0, 0, 0);
        }
    }

    void SetSprites()
    {
        portraitSprites[0].sprite = heads[i_Head][i_HeadState];
        portraitSprites[1].sprite = body[i_Body][i_BodyState];
    }

}

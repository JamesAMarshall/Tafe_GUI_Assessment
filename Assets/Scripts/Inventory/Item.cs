using UnityEngine;

public class Item
{
    public ItemType type;

    private int id;
    private int spriteId;
    private Sprite sprite;
    private Texture2D icon;
    private string name, description;
    private int value, amount, price;
    private float health, damage, defense, stamina, agility;
    private float weight, durability;


    void Init()
    {
        name = "Unkown";
        description = "Unkown";
        value = 0;
        type = ItemType.Craftable;
        id = 0;
    }

    void Init(string name, string description, int value, ItemType type, int id)
    {
        this.name = name;
        this.description = description;
        this.value = value;
        this.type = type;
        this.id = id;

    }
    //Get Set
    #region


    public int SpriteIndex
    {
        get { return spriteId; }
        set { spriteId = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }
    public int Price
    {
        get { return price; }
        set { this.price = value; }
    }
    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float Defense
    {
        get { return defense; }
        set { defense = value; }
    }
    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }
    public float Agility
    {
        get { return agility; }
        set { agility = value; }
    }
    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }
    public float Durability
    {
        get { return durability; }
        set { durability = value; }
    }
    public Texture2D Icon
    {
        get { return icon; }
        set { icon = value; }
    }
    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }
    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    #endregion 

}
public enum ItemType
{
    Null,
    Weapon,
    Armour,
    Craftable,
    Valuables,
    Potion,
    Consumable,
    Quest
}


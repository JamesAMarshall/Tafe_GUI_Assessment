using UnityEngine;

public static class ItemDatabase 
{
    public static Item createItem(int ID)
    {
        #region Member Variables
        Item temp = new Item();
        ItemType type = ItemType.Craftable;

        string icon = "";
        string sprite = "";       
        string name = "";
        string description = "";

        int spriteId = 0;
        int value = 0;
        int price = 0;
        int amount = 0;

        float health = 0;
        float damage = 0;
        float defense = 0;
        float stamina = 0;
        float agility = 0;

        float weight = 0;
        float durabilty = 0;
        #endregion

        switch (ID)
        {
            #region Test 000
            case 000:
                type = ItemType.Null;

                name = "Test";
                description = "Used for Testing Purposes";
                icon = "";
                sprite = "";

                value = 0;
                price = 0;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            #endregion
            #region Weapons 100 - 199
            case 100:
                type = ItemType.Weapon;

                name = "Sword";
                description = "A good solid choice for cutting, slicing and dicing. \n Warning: dont hold the glowing part";
                icon = "Sword";
                sprite = "Sword";

                value = 10;
                price = 15;

                health = 0;
                damage = 20;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 30;
                break;
            case 101:
                type = ItemType.Weapon;

                name = "Nano Shield";
                description = "Deployable Shield, blocks attacks using a plasma feild, just make sure you have some spare AA battries";
                icon = "Shield";
                sprite = "Shield";

                value = 10;
                price = 15;

                health = 0;
                damage = 20;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 30;
                break;
            case 102:
                type = ItemType.Weapon;

                name = "Daggers";
                description = "Quick and pointy";
                icon = "Daggers";
                sprite = "Daggers";

                value = 10;
                price = 15;

                health = 0;
                damage = 20;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 30;
                break;
            case 103:
                type = ItemType.Weapon;

                name = "Grenades";
                description = "Do you like blowing things up, who doesn't... BANG";
                icon = "Grenades";
                sprite = "Grenades";

                value = 10;
                price = 15;

                health = 0;
                damage = 20;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 30;
                break;
            case 104:
                type = ItemType.Weapon;

                name = "Pistol";
                description = "It shoots, Do not point at yourself";
                icon = "Pistol";
                sprite = "Pistol";

                value = 10;
                price = 15;

                health = 0;
                damage = 20;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 30;
                break;
            case 105:
                type = ItemType.Weapon;

                name = "Ammo";
                description = "You may needs these for you gun to shoot, but see how you go";
                icon = "Ammo";
                sprite = "Ammo";

                value = 5;
                price = 5;

                health = 0;
                damage = 20;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 30;
                break;
            #endregion
            #region Armour 200 - 299
            case 200:
                type = ItemType.Armour;

                name = "Diploable Chest Piece";
                description = "The perfect light weight solution to protecting your body";
                icon = "ChestPiece";
                sprite = "ChestPiece";

                value = 15;
                price = 20;

                health = 0;
                damage = 0;
                defense = 30;
                stamina = 0;
                agility = -10;

                weight = 5;
                durabilty = 100;
                break;
            case 201:
                type = ItemType.Armour;

                name = "Radar Cloack";
                description = "Hides you from the any scanners and radars";
                icon = "Cloak";
                sprite = "Cloak";

                value = 15;
                price = 20;

                health = 0;
                damage = 0;
                defense = 30;
                stamina = 0;
                agility = -10;

                weight = 5;
                durabilty = 100;
                break;
            #endregion
            #region Craftables 300 - 399
            case 300:
                type = ItemType.Craftable;

                name = "Steel";
                description = "Durable, useful for lower level craftable gear";
                icon = "Steel";
                sprite = "Steel";

                value = 2;
                price = 1;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            case 301:
                type = ItemType.Craftable;

                name = "Platnium";
                description = "Strong, useful for medium level craftable gear";
                icon = "Platnium";
                sprite = "Platnium";

                value = 2;
                price = 1;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            case 302:
                type = ItemType.Craftable;

                name = "Titanium";
                description = "Durable, useful for high level craftable gear";
                icon = "Titanium";
                sprite = "Titanium";

                value = 2;
                price = 1;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            case 303:
                type = ItemType.Craftable;

                name = "CPU Chip";
                description = "Scavanged from a offline robot";
                icon = "CPUChip";
                sprite = "CPUChip";

                value = 2;
                price = 1;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            #endregion
            #region Valuables 400 - 499
            case 400:
                type = ItemType.Valuables;

                name = "Credits";
                description = "Local Currency";
                icon = "Credits";
                sprite = "Credits";

                value = 1;
                price = 1;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            case 401:
                type = ItemType.Valuables;

                name = "Rare Gem";
                description = "Collect for bonuses";
                icon = "RareGem";
                sprite = "RareGem";

                value = 100;
                price = 0;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            case 402:
                type = ItemType.Valuables;

                name = "Diamonds";
                description = "Very Shiny";
                icon = "Diamonds";
                sprite = "Diamonds";

                value = 1;
                price = 1;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            case 403:
                type = ItemType.Valuables;

                name = "Blips and Chits Ticket";
                description = "Admits One";
                icon = "BlipsTicket";
                sprite = "BlipsTicket";

                value = 1;
                price = 1;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            #endregion
            #region Potion 500 - 599
            case 500:
                type = ItemType.Potion;

                name = "Health Potion";
                description = "Preferably use bedore you die... it may be to late after";
                icon = "HealthPotion";
                sprite = "HealthPotion";

                value = 10;
                price = 5;

                health = 20;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 1;
                break;
            case 501:
                type = ItemType.Potion;

                name = "Bonus Damage Potion";
                description = "Inject for a mighty swing";
                icon = "DamagePotion";
                sprite = "DamagePotion";

                value = 10;
                price = 5;

                health = 20;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 1;
                break;
            case 502:
                type = ItemType.Potion;

                name = "Stamina Potion";
                description = "Feeling Tired... Need a pick up. Try this";
                icon = "StaminaPotion";
                sprite = "StaminaPotion";

                value = 10;
                price = 5;

                health = 20;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 1;
                break;
            case 503:
                type = ItemType.Potion;

                name = "OverShield";
                description = "Use gain a temporary shield";
                icon = "OverShield";
                sprite = "OverShield";

                value = 10;
                price = 5;

                health = 20;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 1;
                break;
            #endregion
            #region Consumables 600 - 699
            case 600:
                type = ItemType.Consumable;

                name = "Blergy Burger";
                description = "The 'Blergy Burger'";
                icon = "BBurger";
                sprite = "BBurger";

                value = 30;
                price = 20;

                health = 5;
                damage = 0;
                defense = 0;
                stamina = 20;
                agility = 0;

                weight = 0;
                durabilty = 1;
                break;
            case 601:
                type = ItemType.Consumable;

                name = "Space Drink";
                description = "The 'Blergy Brotheers' invented a really tasty drink sold universally. Smells supicously of 'Knarkle' excrement";
                icon = "SpaceDrink";
                sprite = "SpaceDrink";

                value = 30;
                price = 20;

                health = 5;
                damage = 0;
                defense = 0;
                stamina = 20;
                agility = 0;

                weight = 0;
                durabilty = 1;
                break;
            #endregion
            #region Quest 700 - 799
            case 700:
                type = ItemType.Quest;

                name = "Missing Robot";
                description = "You picked up a peice a digital notice, There is a missing robot with quite the price for a reward";
                icon = "MissingRobotQuest";
                sprite = "MissingRobotQuest";

                value = 200;
                price = 0;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            case 701:
                type = ItemType.Quest;

                name = "Find a Scarf ";
                description = "Someone is in desperate need of a scarf that can hide behind";
                icon = "ScarfQuest";
                sprite = "ScarfQuest";

                value = 0;
                price = 0;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
            case 702:
                type = ItemType.Quest;

                name = "Go the Market";
                description = "You read the title... Go to the market";
                icon = "MarketQuest";
                sprite = "MarketQuest";

                value = 0;
                price = 0;

                health = 0;
                damage = 0;
                defense = 0;
                stamina = 0;
                agility = 0;

                weight = 0;
                durabilty = 0;
                break;
                #endregion

        }

        #region Temp set
        
        temp.Id = ID;
        temp.Type = type;

        temp.Name = name;
        temp.Description = description;

        temp.Value = value;
        temp.Amount = amount;
        temp.Price = price;

        temp.Health = health;
        temp.Damage = damage;
        temp.Defense = defense;
        temp.Stamina = stamina;
        temp.Agility = agility;

        temp.Weight = weight;
        temp.Durability = durabilty;

        temp.SpriteIndex = spriteId;

        temp.Sprite = Resources.Load<Sprite>("Sprites/" + sprite);
        temp.Icon = Resources.Load("Icons/Icon_" + icon) as Texture2D;

        return temp;
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using LitJson;

//Json에서 리스트를 가져오는것.
public class ItemDatabase : MonoBehaviour {
    private List<Item> weaponDatabase = new List<Item>();
    private List<Item> sculptureDatabase = new List<Item>();
    private List<Item> materialDatabase = new List<Item>();
    private JsonData itemData;
    public static Sprite[] sprites;

    public static string filePath = string.Empty;

    void Start()
    {
        DataSaver.Initalize();
        TextAsset file = Resources.Load("Data/Items") as TextAsset;
        string content = file.ToString();
        itemData = JsonMapper.ToObject(content);
        ConstructItemDataBase();
    }

    public Item FetchWeaponItemById(int id)
    {
        foreach (Item ItemTemp in weaponDatabase)
        {
            if (ItemTemp.Id == id) return ItemTemp;
        }

        return null;
    }
    public Item FetchSculptureItemById(int id)
    {
        foreach (Item ItemTemp in sculptureDatabase)
        {
            if (ItemTemp.Id == id) return ItemTemp;
        }

        return null;
    }
    public Item FetchMaterialItemById(int id)
    {
        foreach (Item ItemTemp in materialDatabase)
        {
            if (ItemTemp.Id == id) return ItemTemp;
        }

        return null;
    }

    void ConstructItemDataBase()
    {
        for (int i = 0; i < itemData["weapon"].Count; i++)
        {
            weaponDatabase.Add(new Item((int)itemData["weapon"][i]["id"], (int)itemData["weapon"][i]["price"], (int)itemData["weapon"][i]["fame"], itemData["weapon"][i]["type"].ToString(), itemData["weapon"][i]["type2"].ToString(), itemData["weapon"][i]["title"].ToString(),
                (int)itemData["weapon"][i]["stats"]["sculptDamage"], (int)itemData["weapon"][i]["stats"]["damage"],
                (int)itemData["weapon"][i]["stats"]["criticalProbability"], (int)itemData["weapon"][i]["stats"]["criticalDamage"],
                (int)itemData["weapon"][i]["stats"]["attackSpeed"], itemData["weapon"][i]["stats"]["attackSpeedSlug"].ToString(),
                (int)itemData["weapon"][i]["upgrade"]["upgradeLevel"], (int)itemData["weapon"][i]["upgrade"]["upgradeRating"],
                (int)itemData["weapon"][i]["weight"], (int)itemData["weapon"][i]["specialAbility"]["slot1"],
                (int)itemData["weapon"][i]["specialAbility"]["slot2"], (int)itemData["weapon"][i]["specialAbility"]["slot3"],
                itemData["weapon"][i]["description"].ToString(), (bool)itemData["weapon"][i]["stackable"], (int)itemData["weapon"][i]["maxStack"],
                (int)itemData["weapon"][i]["rarity"], itemData["weapon"][i]["raritySlug"].ToString(), itemData["weapon"][i]["slug"].ToString(),
                itemData["weapon"][i]["material"].ToString(), itemData["weapon"][i]["materialSlug"].ToString(), itemData["weapon"][i]["path"].ToString()
            ));
        }
        for (int i = 0; i < itemData["sculpture"].Count; i++)
        {
            sculptureDatabase.Add(new Item((int)itemData["sculpture"][i]["id"], (int)itemData["sculpture"][i]["price"], (int)itemData["sculpture"][i]["price2"], (int)itemData["sculpture"][i]["fame"], itemData["sculpture"][i]["type"].ToString(), itemData["sculpture"][i]["type2"].ToString(), itemData["sculpture"][i]["title"].ToString(),
                (int)itemData["sculpture"][i]["stats"]["sculptDamage"], (int)itemData["sculpture"][i]["stats"]["damage"],
                (int)itemData["sculpture"][i]["stats"]["criticalProbability"], (int)itemData["sculpture"][i]["stats"]["criticalDamage"],
                (int)itemData["sculpture"][i]["stats"]["attackSpeed"], itemData["sculpture"][i]["stats"]["attackSpeedSlug"].ToString(),
                (int)itemData["sculpture"][i]["upgrade"]["upgradeLevel"], (int)itemData["sculpture"][i]["upgrade"]["upgradeRating"],
                (int)itemData["sculpture"][i]["weight"], (int)itemData["sculpture"][i]["specialAbility"]["slot1"],
                (int)itemData["sculpture"][i]["specialAbility"]["slot2"], (int)itemData["sculpture"][i]["specialAbility"]["slot3"],
                itemData["sculpture"][i]["description"].ToString(), (bool)itemData["sculpture"][i]["stackable"], (int)itemData["sculpture"][i]["maxStack"],
                (int)itemData["sculpture"][i]["rarity"], itemData["sculpture"][i]["raritySlug"].ToString(), itemData["sculpture"][i]["slug"].ToString(),
                itemData["sculpture"][i]["material"].ToString(), itemData["sculpture"][i]["materialSlug"].ToString(),
                (int)itemData["sculpture"][i]["requireHealth"], (int)itemData["sculpture"][i]["requireWorkValue"], itemData["sculpture"][i]["path"].ToString(),
                (int)itemData["sculpture"][i]["exp"], (int)itemData["sculpture"][i]["rank"]
            ));
        }
        for (int i = 0; i < itemData["sculpture"].Count; i++)
        {
            sprites = Resources.LoadAll<Sprite>("Images/Items/Sculptures/sculptures");
            sculptureDatabase[i].sprite = sprites[i];
        }
        for (int i = 0; i < itemData["material"].Count; i++)
        {
            materialDatabase.Add(new Item((int)itemData["material"][i]["id"], (int)itemData["material"][i]["price"], (int)itemData["material"][i]["fame"], itemData["material"][i]["type"].ToString(), itemData["material"][i]["type2"].ToString(), itemData["material"][i]["title"].ToString(),(int)itemData["material"][i]["weight"],
                itemData["material"][i]["description"].ToString(), (bool)itemData["material"][i]["stackable"], (int)itemData["material"][i]["maxStack"],
                (int)itemData["material"][i]["rarity"], itemData["material"][i]["raritySlug"].ToString(), itemData["material"][i]["slug"].ToString(),
                itemData["material"][i]["material"].ToString(), itemData["material"][i]["materialSlug"].ToString(), itemData["material"][i]["path"].ToString()
            ));
        }
    }
}

[Serializable]
public class Item
{
    public int Id { get; set; }
    public int Fame { get; set; }
    public int Price { get; set; }
    public int Price2 { get; set; }
    public string Type { get; set; }
    public string Type2 { get; set; }
    public string Title { get; set; }
    public int SculptDamage { get; set; }
    public int Damage { get; set; }
    public int CriticalProbability { get; set; }
    public int CriticalDamage { get; set; }
    public int AttackSpeed { get; set; }
    public string AttackSpeedSlug { get; set; }
    public int UpgradeLevel { get; set; }
    public int UpgradeRating { get; set; }
    public int Weight { get; set; }
    public int AbilitySlot1 { get; set; }
    public int AbilitySlot2 { get; set; }
    public int AbilitySlot3 { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int MaxStack { get; set; }
    public int Rarity { get; set; }
    public string RaritySlug { get; set; }
    public string Slug { get; set; }
    public string Slot { get; set; }
    public string Material { get; set; }
    public string MaterialSlug { get; set; }
    public int RequireHealth { get; set; }
    public int RequireWorkValue { get; set; }
    public string Path { get; set; }
    public int Exp { get; set; }
    public int Rank { get; set; }

    int id;
    int fame;
    int price;
    int price2;
    string type;
    string type2;
    string title;
    int sculptDamage;
    int damage;
    int criticalProbability;
    int criticalDamage;
    int attackSpeed;
    string attackSpeedSlug;
    int upgradeLevel;
    int upgradeRatring;
    int weight;
    int abilitySlot1;
    int abilitySlot2;
    int abilitySlot3;
    string description;
    bool stackable;
    int maxStack;
    int rarity;
    string raritySlug;
    string slug;
    string slot;
    string material;
    string materialSlug;
    int requireHealth;
    int requireWorkValue;
    string path;
    int exp;
    int rank;

    [NonSerialized]
    public Sprite sprite;



    //무기
    public Item(int id, int price, int fame, string type, string type2, string title, int sculptdamage,int damage,int criticalProbability, int criticalDamage,
        int attackSpeed, string attackSpeedSlug,int upgradeLevel, int upgradeRating, int weight, int abilitySlot1,
        int abilitySlot2, int abilitySlot3, string description, bool stackable, int maxStack, int rarity,
        string raritySlug, string slug, string material, string materialSlug, string path
        )
    {
        this.Id = id;
        this.Fame = fame;
        this.Price = price;
        this.Type = type;
        this.Type2 = type2;
        this.Title = title;
        this.SculptDamage = sculptdamage;
        this.Damage = damage;
        this.CriticalProbability = criticalProbability;
        this.CriticalDamage = criticalDamage;
        this.AttackSpeed = attackSpeed;
        this.AttackSpeedSlug = attackSpeedSlug;
        this.UpgradeLevel = upgradeLevel;
        this.UpgradeRating = upgradeRating;
        this.Weight = weight;
        this.AbilitySlot1 = abilitySlot1;
        this.AbilitySlot2 = abilitySlot2;
        this.AbilitySlot3 = abilitySlot3;
        this.Description = description;
        this.Stackable = stackable;
        this.MaxStack = maxStack;
        this.Rarity = rarity;
        this.RaritySlug = raritySlug;
        this.Slug = slug;
        this.Material = material;
        this.MaterialSlug = materialSlug;
        this.Path = path;
        this.sprite = Resources.Load<Sprite>(Path + slug);
    }
    public Item(int id, string title)
    {
        this.Id = id;
        this.Title = title;
    }
    //Sculptures
    public Item(int id, int price, int price2, int fame, string type, string type2, string title, int sculptdamage, int damage, int criticalProbability, int criticalDamage,
        int attackSpeed, string attackSpeedSlug, int upgradeLevel, int upgradeRating, int weight, int abilitySlot1,
        int abilitySlot2, int abilitySlot3, string description, bool stackable, int maxStack, int rarity,
        string raritySlug, string slug, string material, string materialSlug, int requireHealth, int requireWorkValue,
        string path, int exp, int rank
        )
    {
        this.Id = id;
        this.Price = price;
        this.Price2 = price2;
        this.Fame = fame;
        this.Type = type;
        this.Type2 = type2;
        this.Title = title;
        this.SculptDamage = sculptdamage;
        this.Damage = damage;
        this.CriticalProbability = criticalProbability;
        this.CriticalDamage = criticalDamage;
        this.AttackSpeed = attackSpeed;
        this.AttackSpeedSlug = attackSpeedSlug;
        this.UpgradeLevel = upgradeLevel;
        this.UpgradeRating = upgradeRating;
        this.Weight = weight;
        this.AbilitySlot1 = abilitySlot1;
        this.AbilitySlot2 = abilitySlot2;
        this.AbilitySlot3 = abilitySlot3;
        this.Description = description;
        this.Stackable = stackable;
        this.MaxStack = maxStack;
        this.Rarity = rarity;
        this.RaritySlug = raritySlug;
        this.Slug = slug;
        this.Material = material;
        this.MaterialSlug = materialSlug;
        this.RequireHealth = requireHealth;
        this.RequireWorkValue = requireWorkValue;
        this.Path = path;
        this.Exp = exp;
        this.Rank = rank;

    }

    //MATERIAL
    public Item(int id, int price, int fame, string type, string type2, string title,int weight, string description, bool stackable, int maxStack,
        int rarity, string raritySlug, string slug, string material, string materialSlug, string path
        )
    {
        this.Id = id;
        this.Price = price;
        this.Fame = fame;
        this.Type = type;
        this.Type2 = type2;
        this.Title = title;  
        this.Weight = weight;
        this.Description = description;
        this.Stackable = stackable;
        this.MaxStack = maxStack;
        this.Rarity = rarity;
        this.RaritySlug = raritySlug;
        this.Slug = slug;
        this.Material = material;
        this.MaterialSlug = materialSlug;;
        this.Path = path;
        this.sprite = Resources.Load<Sprite>(Path + slug);
    }


    public Item()
    {
        this.Id = -1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataSaver
{
    public static string filePath = string.Empty;
    public static List<string> inventoryFilePath = new List<string>();
    public static string stackFilePath = string.Empty;
    public static string storageFilePath = string.Empty;
    public static string upgradeFilePath = string.Empty;
    public static string deliveryFilePath = string.Empty;
    public static string shopFilePath = string.Empty;
    public static string makingFilePath = string.Empty;

    // Use this for initialization
    public static void Initalize()
    {
        filePath = Application.persistentDataPath + "/Data/graver.bin";
        inventoryFilePath.Add(Application.persistentDataPath + "/Data/weaponInventory.bin");
        inventoryFilePath.Add(Application.persistentDataPath + "/Data/sculptureInventory.bin");
        inventoryFilePath.Add(Application.persistentDataPath + "/Data/materialInventory.bin");
        stackFilePath = Application.persistentDataPath + "/Data/stack.bin";
        storageFilePath = Application.persistentDataPath + "/Data/storage.bin";
        upgradeFilePath = Application.persistentDataPath + "/Data/upgrade.bin";
        deliveryFilePath = Application.persistentDataPath + "/Data/delivery.bin";
        shopFilePath = Application.persistentDataPath + "/Data/shop.bin";
        makingFilePath = Application.persistentDataPath + "/Data/making.bin";
    }

    public static void SaveData<T>(T t, string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        formatter.Serialize(stream, t);
        stream.Close();
    }

    public static T LoadData<T>(string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open);
        T t = (T)formatter.Deserialize(stream);
        stream.Close();

        return t;
    }

}

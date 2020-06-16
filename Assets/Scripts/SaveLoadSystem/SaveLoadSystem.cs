﻿using UnityEngine;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;
#if UNITY_EDITOR
using UnityEditor.Compilation;
#endif
using System.Collections.Generic;

namespace SaveLoadSystem
{
    public static class System
    {
        public static bool Save(string fname)
        {
            if (fname.Length < 1) 
                throw new InvalidDataException(fname);
            Game data = new Game(fname);
            Directory.CreateDirectory(data.rootFolderPath);
            CreateCopyOfAllScriptableObjects(ref data);
            Stream stream = File.Open($"{data.rootFolderPath}/save.cjc", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            //bformatter.Binder = new VersionDeserializationBinder();
            bformatter.Serialize(stream, data);
            stream.Close();
            return true;
        }

        public static void CreateCopyOfAllScriptableObjects(ref Game data)
        {
            foreach (var path in data.scriptObjAllPaths)
            {
                int pos = path.LastIndexOf("\\") + 1;
                string name = path.Substring(pos, path.Length - pos);
                File.Copy(path, $"{data.rootFolderPath}/{name}", true);
            }
        }

        public static void Load(string fname)
        {
            string path = $"{Application.persistentDataPath}/Games/{fname}/save.cjc";
            if (File.Exists(path))
            {
                Debug.Log($"Load: {path}");
                Stream stream = File.Open(path, FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                //bformatter.Binder = new VersionDeserializationBinder();
                Game data = (Game)bformatter.Deserialize(stream);
                stream.Close();
                reloadGame(ref data);
            } else
            {
                throw new InvalidDataException();
            }
        }

        private static void reloadGame(ref Game data)
        {
            PlayerManager.Instance.Player.transform.position = new Vector3(data.player.posX, data.player.posY, data.player.posZ);
            PlayerManager.Instance.HeroScript.setHealth(data.player.health);
            PlayerManager.Instance.HeroScript.setPlayerName(data.player.playerName);
            PlayerManager.Instance.HeroScript.setPlayerAmmo(data.player.playerAmmo);
            PlayerManager.Instance.HeroScript.setInGunAmmo(data.player.inGunAmmo);
            PlayerManager.Instance.HeroScript.setMoneys(data.player.moneys);
            PlayerManager.Instance.HeroScript.setInventory(data.player.inventory);
            foreach (var file in data.scriptObjAllPaths)
            {
                int pos = file.LastIndexOf("\\") + 1;
                string name = file.Substring(pos, file.Length - pos);
                // DISABLE ON DEBUG MODE
                //File.Copy($"{data.rootFolderPath}/{name}", file, true);
            }
        }
    }

    [Serializable]
    public class Game : ISerializable
    {
        public static Game current;
        public String seed;
        public String rootFolderPath;
        public string[] scriptObjAllPaths;
        public String day;
        public String time;
        public Player player;
        public Game(string fname) {
            seed = "";
            scriptObjAllPaths = Directory.GetFiles($"{Application.dataPath}/Resources", "*.asset", SearchOption.AllDirectories);
            day = DateTime.Now.ToString("MM/dd/yyyy");
            time = DateTime.Now.ToString("HH/mm/ss");
            rootFolderPath = $"{Application.persistentDataPath}/Games/{fname}-{this.day}-{this.time}";
            player = new Player();
        }

        public Game(SerializationInfo info, StreamingContext ctxt)
        {
            seed = (String)info.GetValue("seed", typeof(String));
            rootFolderPath = (String)info.GetValue("rootFolderPath", typeof(String));
            scriptObjAllPaths = (string[])info.GetValue("scriptObjAllPaths", typeof(string[]));
            day = (String)info.GetValue("day", typeof(String));
            time = (String)info.GetValue("time", typeof(String));
            player = (Player)info.GetValue("player", typeof(Player));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("seed", seed); 
            info.AddValue("rootFolderPath", rootFolderPath);
            info.AddValue("scriptObjAllPaths", scriptObjAllPaths);
            info.AddValue("day", day);
            info.AddValue("time", time);
            info.AddValue("player", player);
        }
    }


    public abstract class Character
    {
        public float posX { get; protected set; }
        public float posY { get; protected set; }
        public float posZ { get; protected set; }
        public float health { get; protected set; }
    }

    [Serializable]
    public class InventorySerializable : ISerializable
    {
        public Dictionary<ItemType, Item> items = new Dictionary<ItemType, Item>();
        public Dictionary<Slots, Item> slots = new Dictionary<Slots, Item>();
        public Weapon weapon;

        public InventorySerializable(Inventory inv)
        {
            items = inv.items;
            slots = inv.slots;
            weapon = WeaponManager.instance.weapon;
        }

        public InventorySerializable(SerializationInfo info, StreamingContext ctxt)
        {
            items = (Dictionary<ItemType, Item>)info.GetValue("items", typeof(Dictionary<ItemType, Item>));
            slots = (Dictionary<Slots, Item>)info.GetValue("slots", typeof(Dictionary<Slots, Item>));
            weapon = (Weapon)info.GetValue("weapon", typeof(Weapon));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("items", items);
            info.AddValue("slots", slots);
            info.AddValue("weapon", weapon);
        }
    }

    [Serializable]
    public class Player : Character, ISerializable
    {
        public int moneys { get; private set; }
        public string playerName { get; private set; }
        public ushort playerAmmo { get; private set; }
        public ushort inGunAmmo { get; private set; }
        public InventorySerializable inventory { get; private set; }

        public Player() {
            posX = PlayerManager.Instance.Player.transform.position.x;
            posY = PlayerManager.Instance.Player.transform.position.y;
            posZ = PlayerManager.Instance.Player.transform.position.z;
            health = PlayerManager.Instance.HeroScript.health;
            moneys = PlayerManager.Instance.HeroScript.moneys;
            playerName = PlayerManager.Instance.HeroScript.playerName;
            playerAmmo = PlayerManager.Instance.HeroScript.playerAmmo;
            inGunAmmo = PlayerManager.Instance.HeroScript.inGunAmmo;
            inventory = new InventorySerializable(PlayerManager.Instance.HeroScript.inventory);
        }
        public Player(SerializationInfo info, StreamingContext ctxt)
        {
            posX = (float)info.GetValue("posX", typeof(float));
            posY = (float)info.GetValue("posY", typeof(float));
            posZ = (float)info.GetValue("posZ", typeof(float));
            health = (float)info.GetValue("health", typeof(float));
            moneys = (int)info.GetValue("moneys", typeof(int));
            playerName = (string)info.GetValue("playerName", typeof(string));
            playerAmmo = (ushort)info.GetValue("playerAmmo", typeof(ushort));
            inGunAmmo = (ushort)info.GetValue("inGunAmmo", typeof(ushort));
            inventory = (InventorySerializable)info.GetValue("inventory", typeof(InventorySerializable));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("posX", posX);
            info.AddValue("posY", posY);
            info.AddValue("posZ", posZ);
            info.AddValue("health", health);
            info.AddValue("moneys", moneys);
            info.AddValue("playerName", playerName);
            info.AddValue("playerAmmo", playerAmmo);
            info.AddValue("inGunAmmo", inGunAmmo);
            info.AddValue("inventory", inventory);
        }
    }

    public sealed class VersionDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
            {
                Type typeToDeserialize = null;
#if UNITY_EDITOR
                //assemblyName = Assembly.GetExecutingAssembly().FullName;
                //typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
#endif
                return typeToDeserialize;
            }
            return null;
        }
    }
}

using UnityEngine;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;
using UnityEditor.Compilation;

namespace SaveLoadSystem
{
    public static class System
    {
        public static bool Save(string fname)
        {
            if (fname.Length < 1) 
                throw new InvalidDataException(fname);
            Game data = new Game();
            string path = $"{Application.persistentDataPath}/Games/{data.day}/{data.time}";
            Directory.CreateDirectory(path);
            Stream stream = File.Open($"{path}/{fname}.cjc", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            //bformatter.Binder = new VersionDeserializationBinder();
            bformatter.Serialize(stream, data);
            stream.Close();
            return true;
        }

        public static void Load(string fname)
        {
            Debug.Log("LOADING...");
            if (File.Exists($"{Application.persistentDataPath}/Games/{fname}.cjc"))
            {
                Stream stream = File.Open($"{Application.persistentDataPath}/{fname}.cjc", FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                //bformatter.Binder = new VersionDeserializationBinder();
                Game data = (Game)bformatter.Deserialize(stream);
                stream.Close();
                reloadGame(ref data);
            } else throw new InvalidDataException();
        }

        private static void reloadGame(ref Game data)
        {
            Debug.Log(data.seed);
            Debug.Log(data.player.posX);
            Debug.Log(data.player.posY);
            Debug.Log(data.player.posZ);
            Debug.Log(data.player.health);
            Debug.Log(data.player.coins);
            Debug.Log(data.player.playerName);
            Debug.Log(data.player.playerAmmo);
            Debug.Log(data.player.inGunAmmo);
        }
    }

    [Serializable]
    public class Game : ISerializable
    {
        public static Game current;
        public String seed;
        public String day;
        public String time;
        public Player player;
        public Game() {
            seed = "";
            day = DateTime.Now.ToString("MM/dd/yyyy");
            time = DateTime.Now.ToString("HH/mm/ss");
            player = new Player();
        }

        public Game(SerializationInfo info, StreamingContext ctxt)
        {
            seed = (String)info.GetValue("seed", typeof(String));
            day = (String)info.GetValue("day", typeof(String));
            time = (String)info.GetValue("time", typeof(String));
            player = (Player)info.GetValue("player", typeof(Player));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("seed", seed);
            info.AddValue("player", player);
        }
    }

    [Serializable]
    public class Inventory : ISerializable
    {
        //public Dictionary<ItemType, Item> items = new Dictionary<ItemType, Item>();
        //public Dictionary<Slots, Item> slots = new Dictionary<Slots, Item>();

        public Inventory()
        {

        }

        public Inventory(SerializationInfo info, StreamingContext ctxt)
        {
            //posX = (float)info.GetValue("posX", typeof(float));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            ///info.AddValue("posX", posX);
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
    public class Player : Character, ISerializable
    {
        public int coins { get; private set; }
        public string playerName { get; private set; }
        public ushort playerAmmo { get; private set; }
        public ushort inGunAmmo { get; private set; }
        Inventory inventory; 

        public Player() {
            posX = PlayerManager.Instance.Player.transform.position.x;
            posY = PlayerManager.Instance.Player.transform.position.y;
            posZ = PlayerManager.Instance.Player.transform.position.z;
            health = PlayerManager.Instance.HeroScript.health;
            coins = 0; // TODO: change class type
            playerName = PlayerManager.Instance.HeroScript.playerName;
            playerAmmo = PlayerManager.Instance.HeroScript.playerAmmo;
            inGunAmmo = PlayerManager.Instance.HeroScript.inGunAmmo;
            inventory = new Inventory();
        }
        public Player(SerializationInfo info, StreamingContext ctxt)
        {
            posX = (float)info.GetValue("posX", typeof(float));
            posY = (float)info.GetValue("posY", typeof(float));
            posZ = (float)info.GetValue("posZ", typeof(float));
            health = (float)info.GetValue("health", typeof(float));
            coins = (int)info.GetValue("coins", typeof(int));
            playerName = (string)info.GetValue("playerName", typeof(string));
            playerAmmo = (ushort)info.GetValue("playerAmmo", typeof(ushort));
            inGunAmmo = (ushort)info.GetValue("inGunAmmo", typeof(ushort));
            inventory = (Inventory)info.GetValue("inventory", typeof(Inventory));

        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("posX", posX);
            info.AddValue("posY", posY);
            info.AddValue("posZ", posZ);
            info.AddValue("health", health);
            info.AddValue("coins", coins);
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
                //assemblyName = Assembly.GetExecutingAssembly().FullName;
                //typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
                return typeToDeserialize;
            }
            return null;
        }
    }
}

using UnityEngine;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;
using System.Reflection;

namespace SaveLoadSystem
{
    public static class System
    {
        public static bool Save(string fname)
        {
            Game data = new Game();
            Stream stream = File.Open($"{Application.persistentDataPath}/{fname}.cjc", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Binder = new VersionDeserializationBinder();
            bformatter.Serialize(stream, data);
            stream.Close();
            return true;
        }

        public static bool Load(string fname)
        {
            Debug.Log("LOADING...");
            if (File.Exists($"{Application.persistentDataPath}/{fname}.cjc"))
            {
                Stream stream = File.Open($"{Application.persistentDataPath}/{fname}.cjc", FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Binder = new VersionDeserializationBinder();
                Game data = (Game)bformatter.Deserialize(stream);
                stream.Close();
                Debug.Log(data.seed);
                Debug.Log(data.player.posX);
                Debug.Log(data.player.posY);
                Debug.Log(data.player.posZ);
                Debug.Log(data.player.health);
                Debug.Log(data.player.Coins);
                Debug.Log(data.player.playerName);
                Debug.Log(data.player.playerAmmo);
                Debug.Log(data.player.inGunAmmo);
                return true;
            } else
            {
                Debug.LogWarning("FILE NOT EXISTS !!!");
                return false;
            }
        }
    }

    [Serializable]
    public class Game : ISerializable
    {
        public static Game current;
        public DateTime seed;
        public Player player;
        public Game() {
            seed = DateTime.Now;
            player = new Player();
        }
        public Game(SerializationInfo info, StreamingContext ctxt)
        {
            seed = (DateTime)info.GetValue("seed", typeof(DateTime));
            player = (Player)info.GetValue("player", typeof(Player));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("seed", seed);
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
    public class Player : Character, ISerializable
    {
        public float Coins { get; private set; }
        public string playerName { get; private set; }
        public ushort playerAmmo { get; private set; }
        public ushort inGunAmmo { get; private set; }
        //Inventory inventory; 
        public Player() {
            posX = PlayerManager.Instance.Player.transform.position.x;
            posY = PlayerManager.Instance.Player.transform.position.y;
            posZ = PlayerManager.Instance.Player.transform.position.z;
            health = PlayerManager.Instance.HeroScript.health;
            Coins = PlayerManager.Instance.HeroScript.Coins;
            playerName = PlayerManager.Instance.HeroScript.playerName;
            playerAmmo = PlayerManager.Instance.HeroScript.playerAmmo;
            inGunAmmo = PlayerManager.Instance.HeroScript.inGunAmmo;
        }
        public Player(SerializationInfo info, StreamingContext ctxt)
        {
            posX = (float)info.GetValue("posX", typeof(float));
            posY = (float)info.GetValue("posY", typeof(float));
            posZ = (float)info.GetValue("posZ", typeof(float));
            health = (float)info.GetValue("health", typeof(float));
            Coins = (float)info.GetValue("Coins", typeof(float));
            playerName = (string)info.GetValue("playerName", typeof(string));
            playerAmmo = (ushort)info.GetValue("playerAmmo", typeof(ushort));
            inGunAmmo = (ushort)info.GetValue("inGunAmmo", typeof(ushort));

        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("posX", posX);
            info.AddValue("posY", posY);
            info.AddValue("posZ", posZ);
            info.AddValue("health", health);
            info.AddValue("Coins", Coins);
            info.AddValue("playerName", playerName);
            info.AddValue("playerAmmo", playerAmmo);
            info.AddValue("inGunAmmo", inGunAmmo);
        }
    }

    public sealed class VersionDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
            {
                Type typeToDeserialize = null;
                assemblyName = Assembly.GetExecutingAssembly().FullName;
                typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
                return typeToDeserialize;
            }
            return null;
        }
    }
}

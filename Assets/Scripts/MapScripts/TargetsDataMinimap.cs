using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Targets", menuName = "ScriptableObjects/Minimap", order = 1)]
public class TargetsDataMinimap : ScriptableObject
{
    [SerializeField] LocalDictionary data;

    [System.Serializable] private class LocalDictionary : SerializableDictionary<Transform, GameObject> {}

    public bool Add(Transform target)
    {
        try { 
            data.Add(target, target.GetComponent<Marker>().Get());
            return true;
        } catch(ArgumentException) {
            return false;
        }
    }
    public Dictionary<Transform, GameObject> Get()
    {
        return data;
    }
    public bool Remove(Transform key)
    {
        return data.Remove(key) ? true : false;
    }
}

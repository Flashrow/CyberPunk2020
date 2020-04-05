using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "MoneyData", menuName = "MoneySystem", order = 2)]
public class MoneySystem : ScriptableObject {
    public int money { get; private set; } = 1000;

    public delegate void OnMoneyChange (int newValue);
    public static OnMoneyChange onMoneyChange;

    public void Add (int value) {
        money += value;
        onMoneyChange (money);
    }

    public void Set (int value) {
        money = value;
        onMoneyChange (money);
    }

    public bool Buy (int value) {
        if (money - value >= 0) {
            money -= value;
            onMoneyChange (money);
            return true;
        }
        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public int money { get; private set; } = 1000;

    public delegate void OnMoneyChange(int newValue);
    public static OnMoneyChange onMoneyChange;

    public void Add(int value)
    {
        money += value;
        onMoneyChange(money);
    }

    public void Set(int value)
    {
        money = value;
        onMoneyChange(money);
    }

    public bool Buy(int value)
    {
        if (money - value >= 0)
        {
            onMoneyChange(money);
            return true;
        }
        return false;
    }
}

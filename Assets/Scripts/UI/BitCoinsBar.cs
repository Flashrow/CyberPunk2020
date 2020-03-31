using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitCoinsBar : MonoBehaviour
{
    public Text coinsText;
    public Hero hero;

    [SerializeField]
    private MoneySystem moneySystem;
    // Start is called before the first frame update

    void Start()
    {
        if (hero == null) return;
        coinsText.text = $"{moneySystem.money}";
    }

    void OnMoneyChange(int value)
    {
        coinsText.text = $"{value}";
    }

    void OnEnable()
    {
        MoneySystem.onMoneyChange += OnMoneyChange;
        coinsText.text = $"{moneySystem.money}";
    }

    void OnDisable()
    {
        MoneySystem.onMoneyChange -= OnMoneyChange;
    }
}

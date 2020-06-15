using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BitCoinsBar : MonoBehaviour {
    public Text coinsText;
    public Hero hero;

    [SerializeField]
    private MoneySystem moneySystem;
    // Start is called before the first frame update

    public class LoadMoneyEvent: UnityEvent<int> { };
    public static LoadMoneyEvent loadMoneyEvent = new LoadMoneyEvent();

    void Start () {
        if (hero == null) return;
        moneySystem.Set(hero.moneys);
        coinsText.text = $"{moneySystem.money}";
        loadMoneyEvent.AddListener(value =>
        {
            moneySystem.Load(value);
            coinsText.text = $"{value}";
        });
    }

    void OnMoneyChange (int value) {
        coinsText.text = $"{value}";
    }

    void OnEnable () {
        MoneySystem.onMoneyChange += OnMoneyChange;
        coinsText.text = $"{moneySystem.money}";
    }

    void OnDisable () {
        MoneySystem.onMoneyChange -= OnMoneyChange;
    }
}
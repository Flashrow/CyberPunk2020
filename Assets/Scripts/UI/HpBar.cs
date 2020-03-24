using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform bar;
    public Text playerName;
    public Text hpText;
    public Hero hero;

    void Start()
    {
        float width = 350f;
        if (hero == null) return;
        playerName.text = hero.playerName;
        hpText.text = $"{(hero.Hp / hero.BaseHp) * 100}%";
        bar.offsetMax = new Vector2(-(width - ((hero.Hp / hero.BaseHp) * width)), bar.offsetMax.y);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

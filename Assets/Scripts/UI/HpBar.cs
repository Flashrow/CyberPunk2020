using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpBar : MonoBehaviour {
    // Start is called before the first frame update
    public RectTransform bar;
    public Text playerName;
    public Text hpText;
    float width = 350f;
    void Start () {
        if (PlayerManager.Instance.HeroScript == null) return;
        refreshStats ();
    }

    // Update is called once per frame
    void Update () {
        // TODO: Event
        refreshStats ();
    }

    void refreshStats () {
        playerName.text = PlayerManager.Instance.HeroScript.playerName;
        if (PlayerManager.Instance.HeroScript.health > 0)
        {
            hpText.text = $"{(PlayerManager.Instance.HeroScript.health / PlayerManager.Instance.HeroScript.BaseHp) * 100}%";
            bar.offsetMax = new Vector2 (-(width - ((PlayerManager.Instance.HeroScript.health / PlayerManager.Instance.HeroScript.BaseHp) * width)), bar.offsetMax.y);
        } else
        {
            hpText.text = "0%";
            bar.offsetMax = new Vector2(0, 0);
            Hero.OnDieEvent.Invoke();
            Destroy(this);
        }
    }
}
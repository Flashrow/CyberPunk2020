using UnityEngine;
using UnityEngine.UI;

public class UITextScript : MonoBehaviour {
    public Text HPText;
    public Text PlayerAmmoText;
    public Text GunAmmoText;

    // Start is called before the first frame update
    void Start () {
        HPText.text = this.GetComponent<Hero> ().health.ToString ();
        PlayerAmmoText.text = this.GetComponent<Hero> ().playerAmmo.ToString ();
        GunAmmoText.text = this.GetComponent<Hero> ().inGunAmmo.ToString ();

    }

    // Update is called once per frame
    void Update () {
        HPText.text = this.GetComponent<Hero> ().health.ToString ();
        PlayerAmmoText.text = this.GetComponent<Hero> ().playerAmmo.ToString ();
        GunAmmoText.text = this.GetComponent<Hero> ().inGunAmmo.ToString ();
    }
}
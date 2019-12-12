using System;
using TMPro; //IMPORTANT
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour {
    public GameObject StaminaText;
    public GameObject Player;
    public Mitochondrian Mitochondrian;
    private Player PlayerScript;
    public GameObject Cheats;
    // Start is called before the first frame update
    void Start() {
        PlayerScript = Player.GetComponent<Player>();
        ToggleCheats();
    }

    // Update is called once per frame
    void Update() { 
        StaminaText.GetComponent<TextMeshProUGUI>().text = "Stamina: "+Math.Round(PlayerScript.Stamina);
    }
    public void AddCO2() {
        Mitochondrian.M.co2.Num++;
    }
    public void AddH2O() {
        Mitochondrian.M.h2o.Num++;
    }
    public void ToggleCheats() {
        if(Cheats.activeSelf) {
            Cheats.SetActive(false);
        } else {
            Cheats.SetActive(true);
        }
    }
}

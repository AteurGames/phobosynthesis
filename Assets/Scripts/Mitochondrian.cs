using UnityEngine;
using Classes;
using TMPro;

public class Mitochondrian : MonoBehaviour {
    public Mitochondria M = new Mitochondria();
    public TextMeshPro Co2Text;
    public TextMeshPro H2oText;
    void Update() {
        M.Respirate(M.co2,M.h2o);
        Co2Text.text = $"CO2: {M.co2.Num}/6";
        H2oText.text = $"H2O: {M.h2o.Num}/12";
    }
}

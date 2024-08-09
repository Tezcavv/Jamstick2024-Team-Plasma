using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image InfectionFilling;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateInfectionFilling(int _infectionLevel)
    {
        //Mathf.Lerp(_infectionLevel, 0, _infectionLevel);
        InfectionFilling.fillAmount = _infectionLevel/100;
    }
}

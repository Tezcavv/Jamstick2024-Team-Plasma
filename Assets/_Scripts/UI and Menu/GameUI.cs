using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image InfectionFilling;

    private void Start()
    {
        
    }

    public void UpdateInfectionFilling(int _infectionLevel)
    {
        //Mathf.Lerp(_infectionLevel, 0, _infectionLevel);
        float fillConversion = _infectionLevel / 100f;
        InfectionFilling.fillAmount = fillConversion;
    }
}

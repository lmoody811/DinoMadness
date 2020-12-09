using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    private Image health_Stats, stamina_Stats;

    public TextMeshProUGUI level_Text;

    public void Display_HealthStats(float healthValue)
    {
        healthValue /= 100f;
        health_Stats.fillAmount = healthValue;


    }

    public void Display_StaminaStats(float staminaValue)
    {
        staminaValue /= 100f;
        stamina_Stats.fillAmount = staminaValue;


    }

    string getLevelText()
    {
        int curLevel = Dinosaur.level;
        string levelText = "";

        switch (curLevel)
        {
            case 1:
                levelText = "Level 1: Triassic Period";
                break;
            case 2:
                levelText = "Level 2: Jurassic Period";
                break;
            case 3:
                levelText = "Level 3: Cretaceous Period";
                break;
        }
        return levelText;

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowLevelText(3));
    }

    IEnumerator ShowLevelText(float delay)
    {
        level_Text.text = getLevelText();
        yield return new WaitForSeconds(delay);
        level_Text.text = "";
    }

}

using TMPro;
using UnityEngine;

public class GameOverStatsUI : MonoBehaviour
{

    public TMP_Text tmpUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmpUI.text = "Day: " + GlobalVariables.instance.day + "\nScore: " + GlobalVariables.instance.score + "\nPopulation: " + GlobalVariables.instance.population + "\nMorale: " + GlobalVariables.instance.morale;
    }
}

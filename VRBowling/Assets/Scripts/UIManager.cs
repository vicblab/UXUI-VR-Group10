using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager manager;
    public TextMeshProUGUI pinScore;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        GameManager.ResetPins += UpdateScore;
    }
    private void OnDestroy()
    {
        GameManager.ResetPins -= UpdateScore;
    }
    public void UpdateScore()
    {
        pinScore.text = $"{manager.FallenPins}\n{manager.TotalPins - manager.FallenPins}";
    }
}

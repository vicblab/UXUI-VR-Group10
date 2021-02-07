using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Valve.VR.InteractionSystem;

public class UIManager : MonoBehaviour
{
    private GameManager manager;
    public TextMeshProUGUI pinScore;
    public TMP_Text scoreFlavourtext;
    public string[] flavourTexts;
    public Transform scoreParent;
    public GameObject scoreTextPrefab;
    public GameObject[] tutorialObjects;
    private int tutorialPhase = 0;
    private bool tutorialDone;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        GameManager.ResetPins += UpdateScore;
        GameManager.ResetPins += SpawnScoreText;
        ActivateTutorial(0);
        UpdateScore();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            UpdateScore();
    }
    private void OnDestroy()
    {
        GameManager.ResetPins -= UpdateScore;
        GameManager.ResetPins -= SpawnScoreText;
    }
    public void UpdateScore()
    {
        pinScore.text = $"{manager.FallenPins}";
        pinScore.gameObject.GetComponent<Animator>().Play("Pop", 0, 0);
        scoreFlavourtext.text = flavourTexts[manager.FallenPins];
        if (tutorialPhase < tutorialObjects.Length && !tutorialDone)
        {
            foreach (BowlingBall ball in FindObjectsOfType<BowlingBall>())
            {
                ball.gameObject.GetComponent<InteractableHoverEvents>().onAttachedToHand.AddListener(() => ActivateTutorial(1));
            }
        }
        if (manager.FallenPins > 0)
            ActivateTutorial(2);
    }
    public void SpawnScoreText()
    {
        TMP_Text text = Instantiate(scoreTextPrefab, scoreParent).GetComponent<TMP_Text>();
        text.text = $"{manager.Score}";

        if (scoreParent.childCount >= 10)
            Destroy(scoreParent.GetChild(0).gameObject);
    }
    public void ActivateTutorial(int i)
    {
        if (!tutorialDone)
        {
            foreach (GameObject obj in tutorialObjects)
                obj.SetActive(false);

            tutorialPhase = i;
            if (i >= tutorialObjects.Length)
                tutorialDone = true;
            if (!tutorialDone)
            {
                tutorialObjects[i].SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{


    public static GameManager theGameManager;

    [SerializeField] private int totalPins = 10;
    [SerializeField] private int fallenPins = 0;
    [SerializeField] private int totalBalls = 5;
    [SerializeField] private int playedBalls = 0;
    //[SerializeField] private int currentGame = 0; // 0, 1 or 2
    //[SerializeField] private int score0 = 0;
    //[SerializeField] private int score1 = 0;
    //[SerializeField] private int score2 = 0;
    [SerializeField] private int score = 0;

    //Events
    public UnityEvent strikeEvent = new UnityEvent();
    public UnityEvent spareEvent = new UnityEvent();
    public UnityEvent pinHasFallen = new UnityEvent();

    public static UnityAction ResetPins;
    public Transform currentPins;
    public GameObject pinsPrefab;

    public int TotalBalls { get => totalBalls; set => totalBalls = value; }
    public int PlayedBalls { get => playedBalls; set => playedBalls = value; }
    public int TotalPins { get => totalPins; set => totalPins = value; }
    public int FallenPins { get => fallenPins; set => fallenPins = value; }
    public int Score { get => score; set => score = value; }


    // We make sure that there is only one instance of the Game Manager

    void Awake()
    {
        if (!theGameManager)
        {
            theGameManager = this;
            return;
        }

        Destroy(this.gameObject);

    }

    void Start()
    {
        currentPins = Instantiate(pinsPrefab).transform;
        //initialization
        totalPins = 10;
        fallenPins = 0;
        totalBalls = 5;
        playedBalls = 0;
        //currentGame = 0; // 0, 1 or 2
        //score0 = 0;
        //score1 = 0;
        //score2 = 0;
        score = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            Restart();
    }

    public void Restart()
    {
        Destroy(currentPins.gameObject);
        Start();
        ResetPins?.Invoke();
    }


    public void fallenPin()
    {
        fallenPins++;
        score++;
        pinHasFallen?.Invoke();
    }

}

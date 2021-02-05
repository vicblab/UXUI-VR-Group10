using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class VRButtonEvents : MonoBehaviour
{
    public static UnityAction resetBalls;
    public GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }
    public void ResetBalls()
    {
        resetBalls?.Invoke();
    }
    public void ResetPins()
    {
        manager.Restart();
    }
}

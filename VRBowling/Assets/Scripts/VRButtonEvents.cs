using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class VRButtonEvents : MonoBehaviour
{
    public static UnityAction resetBalls;

    public void ResetBalls()
    {
        resetBalls?.Invoke();
    }
}

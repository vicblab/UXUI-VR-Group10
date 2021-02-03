using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    private bool hasFallen=false;
    private GameManager gameManager;


    void Start(){

       gameManager = FindObjectOfType<GameManager>();

    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            if(!hasFallen){
            gameManager.fallenPin();
            hasFallen=true;
            gameManager.pinHasFallen.Invoke();
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    private bool hasFallen = false;
    private GameManager gameManager;
    private Vector3 ogPos;
    private Rigidbody rb;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ogPos = transform.parent.position;
        rb = GetComponentInParent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Debug.Log("floor");
            if (!hasFallen)
            {
                gameManager.fallenPin();
                rb.mass = 0.1f;
                hasFallen = true;
            }
        }

    }

}

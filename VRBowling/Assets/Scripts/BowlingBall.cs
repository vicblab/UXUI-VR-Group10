using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    private Vector3 ogPos;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
        rb = GetComponent<Rigidbody>();
        VRButtonEvents.resetBalls += ResetPos;
        GameManager.ResetPins += ResetPos;

    }
    private void OnDestroy()
    {
        VRButtonEvents.resetBalls -= ResetPos;
        GameManager.ResetPins -= ResetPos;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            ResetPos();
    }
    public void ResetPos()
    {
        transform.position = ogPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}

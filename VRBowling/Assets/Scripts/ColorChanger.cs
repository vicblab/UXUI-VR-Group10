using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Material mat;
    private TrailRenderer trail;
    public Renderer outlineRenderer;
    public Rigidbody rb;
    [SerializeField] Gradient gradient;

    private Vector3 ogPos;

    private void Start()
    {
        VRButtonEvents.resetBalls += ResetPos;
        ogPos = transform.parent.position;
        mat = outlineRenderer.material;
        trail = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
        if (!rb)
            rb = GetComponentInParent<Rigidbody>();

        //rb = GetComponent<Rigidbody>();
        SetColors(0f);
    }
    private void OnDestroy()
    {
        VRButtonEvents.resetBalls -= ResetPos;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            ResetPos();
    }
    public void ResetPos()
    {
        transform.parent.position = ogPos;
        rb.velocity = Vector3.zero;
    }
    private void FixedUpdate()
    {
        SetColors(rb.velocity.magnitude / 10f);
    }
    private void SetColors(float value)
    {
        Color color = gradient.Evaluate(value);
        mat.SetColor("g_vOutlineColor", color);
        trail.material.color = color;

    }
}

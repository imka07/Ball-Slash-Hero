using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Vector3 mousePosition;
    private bool isCutting = false;
    private TrailRenderer trailRenderer;
    private bool isKnifeEnabled = true;

    private void Start()
    {
        DisableKnife(false);
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }

    void Update()
    {

        if (isKnifeEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                trailRenderer.enabled = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                trailRenderer.enabled = false; 
            }

            if (isCutting)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f; 
                transform.position = mousePosition;
            }
        }
      
    }

    public void DisableKnife(bool active)
    {
        isKnifeEnabled = active;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isCutting && collision.CompareTag("Ball"))
        {

            BallSplit ballSplit = collision.GetComponent<BallSplit>();
            if (ballSplit != null)
            {
                ballSplit.SplitBall(); 
            }
        }
    }
}

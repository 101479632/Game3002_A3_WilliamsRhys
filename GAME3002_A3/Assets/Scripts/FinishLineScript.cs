using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishLineScript : MonoBehaviour
{
    public TMP_Text finishText; // Assign UI Text in inspector
    public CountdownTimer countdownTimer; // Assign the timer script in inspector

    private void Start()
    {
        if (finishText != null)
        {
            finishText.gameObject.SetActive(false);     // Hide on start
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (countdownTimer != null)
                countdownTimer.StopTimer();

            if (finishText != null)
                finishText.gameObject.SetActive(true);  // Show when triggered
        }
    }
}

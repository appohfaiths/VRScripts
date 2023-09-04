using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class TimerScript : MonoBehaviour
{
    public float timerDuration = 30f;
    public TMP_Text timerText;
    public float currentTime;
    public Canvas promptCanvas;
    public GymTeleportationController gymTeleportationController;
    public VideoPlayer cinemaVideoPlayer;

    
    // Start is called before the first frame update
    void Start()
    {
        promptCanvas.gameObject.SetActive(false);
        currentTime = timerDuration;
        UpdateTimerText();
        StartCoroutine(StartTimer());
    }
private IEnumerator StartTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerText();
        }
        ShowPrompt();
        cinemaVideoPlayer.Pause();
    }
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }
    private void ShowPrompt()
    {
        timerText.text = "Time's up!";
        promptCanvas.gameObject.SetActive(true);
    }

     public void OnYesButtonClicked()
    {
        // Call the function to teleport the user to the gym scene   
        promptCanvas.gameObject.SetActive(false);
        gymTeleportationController?.TeleportToGymDirectly();
    }

    public void OnNoButtonClicked()
    {
        RestartTimer();
    }

      public void RestartTimer()
    {
        // Hide the Canvas with the prompt
        promptCanvas.gameObject.SetActive(false);

        currentTime = timerDuration;
        UpdateTimerText();
        // timerText.gameObject.SetActive(true);

        // Stop the existing coroutine if it's running
        StopAllCoroutines();

        // Start the timer coroutine again
        StartCoroutine(StartTimer());
        cinemaVideoPlayer.Play();
    }

}

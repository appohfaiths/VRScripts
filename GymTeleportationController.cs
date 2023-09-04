using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Video;

public class GymTeleportationController : MonoBehaviour
{
    public OVRCameraRig ovrCameraRig;
    public TimerScript timerScript;
    public PlayableDirector CoachTimeline;
    public VideoPlayer cinemaVideoPlayer;

    public Transform teleportTargetGym;
    public Transform teleportTargetCinema;

    public OVRInput.Button gymTeleportButton = OVRInput.Button.One;
    public OVRInput.Button cinemaTeleportButton = OVRInput.Button.Two;

    private bool isTeleportingToGym = false;
    private bool isTeleportingToCinema = false;

    private enum TeleportLocation
    {
        Gym,
        Cinema
    }

    private void Update()
    {
        if (!isTeleportingToGym && (OVRInput.GetDown(gymTeleportButton) || Input.GetKey("x")))
        {
            StopAllCoroutines();
            StartCoroutine(TeleportToLocation(TeleportLocation.Gym));
        }
        if (!isTeleportingToCinema && (OVRInput.GetDown(cinemaTeleportButton) || Input.GetKey("c")))
        {
            StopAllCoroutines();
            StartCoroutine(TeleportToLocation(TeleportLocation.Cinema));
        }
    }

    private IEnumerator TeleportToLocation(TeleportLocation location)
    {
        if (location == TeleportLocation.Gym)
            isTeleportingToGym = true;
        else if (location == TeleportLocation.Cinema)
            isTeleportingToCinema = true;

        // Fade out the screen or use any teleportation effect you desire here

        // Teleport the player to the target position
        Vector3 targetPosition = location == TeleportLocation.Gym ? teleportTargetGym.position : teleportTargetCinema.position;
        ovrCameraRig.transform.position = targetPosition;

        // Wait for a short time (optional) to give time for the teleportation effect
        yield return new WaitForSeconds(0.5f);

        // Fade in the screen or end the teleportation effect here

        if (location == TeleportLocation.Gym)
            {
                isTeleportingToGym = false;
                cinemaVideoPlayer.Pause();
                CoachTimeline.Play();
        }
        else if (location == TeleportLocation.Cinema)
            {
                isTeleportingToCinema = false;
                Debug.Log("Restarting timer...");
                timerScript.RestartTimer();
                

            }
    }

    public void TeleportToGymDirectly()
    {
        StartCoroutine(TeleportToLocation(TeleportLocation.Gym));
    }
    public void TeleportToCinemaDirectly()
    {
        StartCoroutine(TeleportToLocation(TeleportLocation.Cinema));
    }
}

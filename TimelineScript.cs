using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineScript : MonoBehaviour
{
     public PlayableDirector exerciseTimeline;
     public GymTeleportationController gymTeleportationController;

    private void OnEnable()
    {
        exerciseTimeline.stopped += OnTimelineStopped;
    }

    private void OnDisable()
    {
        exerciseTimeline.stopped -= OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        // Teleport the user back or trigger other events
        gymTeleportationController?.TeleportToCinemaDirectly();
        Debug.Log("Exercise ended!");
    }
}

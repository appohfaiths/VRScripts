using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class CoachDavidAnimationStateController : MonoBehaviour
{

    public OVRInput.Axis1D BicyleCrunch = OVRInput.Axis1D.SecondaryIndexTrigger;
    public OVRInput.Axis1D Situps = OVRInput.Axis1D.SecondaryHandTrigger;

    Animator animator;
    public float actionThreshold = 0.5f;

    bool isActionTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // bool isHappyIdle = animator.GetBool("isHappyIdle");
        bool bicycleCrunch = Input.GetKey("a");
        bool situps = Input.GetKey("s");
        bool armStretching = Input.GetKey("d");
        bool airSquat = Input.GetKey("f");
        // if player presses a key
        if (bicycleCrunch)
        {
            // set isBicycleCrunch to true
            animator.SetBool("isBicycleCrunch", true);
        }
        if (!bicycleCrunch)
        {
            // set isBicycleCrunch to true
            animator.SetBool("isBicycleCrunch", false);
        }
        if (situps)
        {
            animator.SetBool("isSitups", true);
        }
        if (!situps)
        {
            animator.SetBool("isSitups", false);
        }
        if (armStretching)
        {
            animator.SetBool("isArmStretching", true);
        }
        if (!armStretching)
        {
            animator.SetBool("isArmStretching", false);
        }
        if (airSquat)
        {
            animator.SetBool("isAirSquat", true);
        }
        if (!airSquat)
        {
            animator.SetBool("isAirSquat", false);
        }

        // OVR input controls for triggers
        float axisIndexValue = OVRInput.Get(BicyleCrunch);
        float axisHandValue = OVRInput.Get(Situps);

        if (axisIndexValue >= actionThreshold && !isActionTriggered)
        {
            // Perform the action here
            animator.SetTrigger("ActionTrigger");
            animator.SetBool("isBicycleCrunch", true);
            isActionTriggered = true;
        }
        else if (axisIndexValue < actionThreshold && isActionTriggered)
        {
            // Reset the action state
            isActionTriggered = false;
        }

        if (axisHandValue >= actionThreshold && !isActionTriggered)
        {
            // Perform the action here
            animator.SetTrigger("ActionTrigger");
            animator.SetBool("isSitups", true);
            isActionTriggered = true;
        }
        else if (axisHandValue < actionThreshold && isActionTriggered)
        {
            // Reset the action state
            isActionTriggered = false;
        }
    }
}

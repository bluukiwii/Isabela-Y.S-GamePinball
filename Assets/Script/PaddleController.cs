using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public KeyCode input;
    public float springPower = 1000f;
    public float minRotation = 5f;
    public float maxRotation = 60f;

    private void Start()
    {
        HingeJoint hinge = GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        limits.min = minRotation;
        limits.max = maxRotation;
        hinge.limits = limits;
        hinge.useLimits = true;
    }
    
    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        HingeJoint hinge = GetComponent<HingeJoint>();
        JointSpring jointspring = hinge.spring;

        if (Input.GetKey(input))
        {
            jointspring.spring = springPower;
        }
        else
        {
            jointspring.spring = 0;
        }

        hinge.spring = jointspring;
    }
}
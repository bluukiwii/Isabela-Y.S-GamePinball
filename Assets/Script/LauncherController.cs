 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LauncherController : MonoBehaviour
{
    public Transform ballTransform;
    public Transform boardTransform;
    public CinemachineVirtualCamera vcam;
    public CinemachineFramingTransposer framingTransposer;
    public Collider bola;
    public KeyCode input;

    public float maxTimeHold;
    public float maxForce;

    private bool isHold = false;

    public void Start(){
        ballTransform = GameObject.Find("bola").GetComponent<Transform>();
        boardTransform = GameObject.Find("board").GetComponent<Transform>();

        vcam.Follow = ballTransform;
        vcam.LookAt = null;

        // Correct typo: vcam.tranform -> vcam.transform
        vcam.transform.rotation = Quaternion.Euler(33, 180, 0);

        // Get the CinemachineFramingTransposer component from the virtual camera
        framingTransposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (framingTransposer != null)
        {
            // Set the camera distance to 5 units at the start
            framingTransposer.m_CameraDistance = 5f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider == bola)
        {
            ReadInput(bola);
        }
    }

    private void ReadInput(Collider collider)
    {
        if (Input.GetKey(input) && !isHold)
        {
            StartCoroutine(StartHold(collider));
        }
    }

    private IEnumerator StartHold(Collider collider)
    {
        isHold = true;

        float force = 0.0f;
        float timeHold = 0.0f;

        while (Input.GetKey(input))
        {
            force = Mathf.Lerp(0, maxForce, timeHold / maxTimeHold);

            yield return new WaitForEndOfFrame();
            timeHold += Time.deltaTime;
        }

        collider.GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
        
        // Change Follow and LookAt to boardTransform when launching
        vcam.Follow = boardTransform;
        vcam.LookAt = null;
        vcam.transform.rotation = Quaternion.Euler(60, 180, 0);
        // Adjust the camera distance when switching to board
        if (framingTransposer != null)
        {
            framingTransposer.m_CameraDistance = 13f;
        }

        isHold = false;
    }
}
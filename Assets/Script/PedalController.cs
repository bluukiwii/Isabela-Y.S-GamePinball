using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedalController : MonoBehaviour
{
    public float rotationAngle = 30f;
    public float returnSpeed = 2f;
    public float pushForce = 500f;
    public Collider bola;

    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == bola)
        {
            Rigidbody bolaRb = other.GetComponent<Rigidbody>();
            if (bolaRb != null)
            {
                bolaRb.AddForce(transform.up * pushForce);
            }
            StartCoroutine(RotatePedal());
        }
    }

    private IEnumerator RotatePedal()
    {
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, rotationAngle, 0);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, elapsedTime * returnSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = originalRotation; 
    }
}

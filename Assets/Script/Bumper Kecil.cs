using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperKecil : MonoBehaviour
{
    public Collider bola;
    public float bounceMultiplier = 1.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == bola)
        {
            Rigidbody bolaRigidbody = bola.GetComponent<Rigidbody>();

            if (bolaRigidbody != null)
            {
                Vector3 normal = collision.contacts[0].normal;
                Vector3 newVelocity = Vector3.Reflect(bolaRigidbody.velocity, normal) * bounceMultiplier;
                bolaRigidbody.velocity = newVelocity;

                Debug.Log("Pantulan Bola Ditingkatkan");
            }
        }
    }
}

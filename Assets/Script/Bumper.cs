using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public Collider bola;
    public float multiplier;
    public Color color;

    public AudioManager audiomanager;
    public VFXManager vfxmanager;

    private Renderer renderer;
    private Animator animator;

    private void Start()
    {
       renderer = GetComponent<Renderer>();
       animator = GetComponent<Animator>();

       renderer.material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == bola)
        {
            Rigidbody bolaRig = bola.GetComponent<Rigidbody>();
            bolaRig.velocity *= multiplier;  

            //animasi
            animator.SetTrigger("hit");

            //playsfx
            audiomanager.PlaySFX(collision.transform.position);

            //playvfx
            vfxmanager.PlayVFX(collision.transform.position);
        }
    }
}

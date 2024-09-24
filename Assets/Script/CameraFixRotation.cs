 using UnityEngine;

public class CameraFixRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // Simpan rotasi awal kamera
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Tetapkan kembali rotasi kamera ke rotasi awal setiap frame
        transform.rotation = initialRotation;
    }
}
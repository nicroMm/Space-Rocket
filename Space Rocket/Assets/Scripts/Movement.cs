using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoost;
    [SerializeField] ParticleSystem leftBoost;
    [SerializeField] ParticleSystem rightBoost;

    Rigidbody rb;
    AudioSource audioSource;
    
    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
       audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

        void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rotateRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rotateLeft();
        }
        else
        {
            RotateStop();
        }

    }

    void StartThrusting()
    {
        if (!mainBoost.isPlaying)
        {
            mainBoost.Play();
        }
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

        void StopThrusting()
    {
        mainBoost.Stop();
        audioSource.Stop();
    }

    void rotateLeft()
    {
        if (!leftBoost.isPlaying)
        {
            leftBoost.Play();
        }
        ApplyRotation(-rotateThrust);
    }

    void rotateRight()
    {
        ApplyRotation(rotateThrust);
        if (!rightBoost.isPlaying)
        {
            rightBoost.Play();
        }
    }

        void RotateStop()
    {
        rightBoost.Stop();
        leftBoost.Stop();
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float RotateThrust = 25f;
    [SerializeField] AudioClip MainThrust;

    [SerializeField] ParticleSystem MainThrustParticles;
    [SerializeField] ParticleSystem LeftThrustParticle;
    [SerializeField] ParticleSystem RightThrustParticle;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
        else
        {
            StopRotating();
        }
    }


    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainThrust);
        }
        if (!MainThrustParticles.isPlaying)
        {
            MainThrustParticles.Play();

        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        MainThrustParticles.Stop();
    }

    void TurnLeft()
    {
        ApplyRotation(RotateThrust);
        if (!RightThrustParticle.isPlaying)
        {
            RightThrustParticle.Play();
        }
    }

    void TurnRight()
    {
        ApplyRotation(-RotateThrust);
        if (!LeftThrustParticle.isPlaying)
        {
            LeftThrustParticle.Play();
        }
    }

    void StopRotating()
    {
        RightThrustParticle.Stop();
        LeftThrustParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
        {
            rb.freezeRotation = true; //freezing rotation to manually rotate 
            transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
            rb.freezeRotation = false; // unfreeze rotation to allow physics system can take over 
        }
}
    
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float power;
    [SerializeField] float rotationPower;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainRocketParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;
    
    Rigidbody rb;
    AudioSource audioSource;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }
    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up* power*Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (!mainRocketParticle.isPlaying)
            {
                mainRocketParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainRocketParticle.Stop();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationPower);
            if (!rightThrusterParticle.isPlaying)
            {
                rightThrusterParticle.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationPower);
            if (!leftThrusterParticle.isPlaying) 
            {
                leftThrusterParticle.Play();
            }
        }
        else
        {
            leftThrusterParticle.Stop();
            rightThrusterParticle.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation=false;
    }
}

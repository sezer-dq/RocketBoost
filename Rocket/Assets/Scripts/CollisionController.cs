using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CollisionController : MonoBehaviour
{
    [SerializeField] float levelLoadDelay=1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crush;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crushParticle;
    AudioSource audioSource;

    bool isTransitioning=false;
    bool collisionDisabled=false;
    void Start()
    {
         audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondDebugKeys();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning==false&&collisionDisabled==false)
        {
            switch (collision.gameObject.tag) 
            {
                case "Start":
                    Debug.Log("start obj");
                    break;
                case "Finish":
                    StartSuccessSequance();
                    break;
                case "Fuel":
                    Debug.Log("fuel obj");
                    break;
                default:
                    StartCrushSequance();
                    break;
            }
        }
    }
    void RespondDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    void StartSuccessSequance()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        successParticle.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrushSequance()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(crush);
        isTransitioning = true;
        GetComponent<Movement>().enabled=false;
        crushParticle.Play();
        Invoke("ReloadScene", levelLoadDelay);
    }
    void ReloadScene()
    {
        int currentSceneIndex= SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}

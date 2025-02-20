using UnityEngine;

public class BackGroundForestManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!audioSource.isPlaying && GameObject.Find("Player")){
            audioSource.Play();
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // Disable looping
        audioSource.playOnAwake = false; // Disable auto-play on start
        StartCoroutine(StartMethod());
    }

    private IEnumerator StartMethod()
    {
        while (true)
        {
            var clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }

    }

    AudioClip GetRandomClip()
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        return audioClips[randomIndex];
    }
}
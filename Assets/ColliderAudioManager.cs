using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteamAudio;

[RequireComponent(typeof(AudioSource), typeof(SteamAudioSource))]
public class ColliderAudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private float VOLUME = 0.5f;
    [SerializeField] private AudioClip audioClip;

    [Header("Steam Audio")]
    [SerializeField] private SteamAudioBakedSource bakedSource;
    [SerializeField] private SteamAudioProbeBatch probeBatch;

    [Header("Audio Settings")]
    [SerializeField] private bool playOnce;

    private bool played = false;
    private AudioSource audioSource;
    private SteamAudioSource steamAudioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        steamAudioSource = gameObject.GetComponent<SteamAudioSource>();

        audioSource.volume = VOLUME;
        audioSource.clip = audioClip;
        audioSource.spatialize = true;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.minDistance = 3f;
        audioSource.maxDistance = 10f;

        steamAudioSource.distanceAttenuation = true;
        steamAudioSource.airAbsorption = false;
        steamAudioSource.directivity = false;
        steamAudioSource.occlusion = true;
        steamAudioSource.transmission = true;
        if (bakedSource != null)
        { 
            steamAudioSource.reflections = true;
            steamAudioSource.reflectionsType = ReflectionsType.BakedStaticSource;
            steamAudioSource.currentBakedSource = bakedSource;
        }
        if (probeBatch != null)
        {
            steamAudioSource.pathing = true;
            steamAudioSource.pathingProbeBatch = probeBatch;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !played)
        {
            if(playOnce) { played = true; }
            audioSource.Play();
        }
    }
}

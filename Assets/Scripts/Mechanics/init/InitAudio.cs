using System;
using System.Collections.Generic;
using UnityEngine;

public class InitAudio : InitAnimator
{
    [Header("Audio Clips")]
    [SerializeField] public AudioClip attackClip;
    [SerializeField] public AudioClip breakPlatformClip;
    [SerializeField] public AudioClip checkpointClip;
    [SerializeField] public AudioClip coinClip;
    [SerializeField] public AudioClip dashClip;
    [SerializeField] public AudioClip deathClip;
    [SerializeField] public AudioClip defeatClip;
    [SerializeField] public AudioClip doorClip;
    [SerializeField] public AudioClip enemyClip;
    [SerializeField] public AudioClip hurtClip;
    [SerializeField] public AudioClip idleClip;
    [SerializeField] public AudioClip jumpAudio;
    [SerializeField] public AudioClip jumpClip;
    [SerializeField] public AudioClip keyClip;
    [SerializeField] public AudioClip landClip;
    [SerializeField] public AudioClip ouchAudio;
    [SerializeField] public AudioClip powerUpClip;
    [SerializeField] public AudioClip respawnAudio;
    [SerializeField] public AudioClip runClip;
    [SerializeField] public AudioClip spawnClip;
    [SerializeField] public AudioClip victoryClip;
    [SerializeField] public AudioClip walkClip;

    [SerializeField] protected Dictionary<string, (AudioClip Clip, Func<AudioClip, AudioClip> Action)> stateAudioMap0;
    [SerializeField] protected Dictionary<string, AudioModel> stateAudioMap;

    [SerializeField] protected List<(string, AudioModel)> playerAudioList = new List<(string key, AudioModel value)>();

    public virtual void TriggerAudioState(string stateName)
    {
    }

    public AudioClip PlayLoop(AudioClip clip)
    {
        return PlayLoop(clip, Source, gameObject);
    }
    public AudioClip PlayLoop(AudioClip clip, AudioSource source, GameObject obj)
    {
        AudioSource finalSource = source ?? Source;
        AudioClip finalClip = clip ?? Clip;
        GameObject finalObj = obj ?? gameObject;

        if (finalSource == null && obj != null)
        {
            finalSource = finalObj.GetComponent<AudioSource>();
        }

        if (finalClip == null && finalSource != null)
        {
            finalClip = finalSource.clip;
        }

        if (finalClip == null)
        {
            Debug.LogWarning("AudioClip is null. Please provide a valid AudioClip.");
            return null;
        }

        if (finalSource != null)
        {
            finalSource.clip = finalClip;
            finalSource.loop = true;
            finalSource.Play();
        }
        else
        {
            Debug.LogWarning($"AudioSource is not set for clip '{finalClip.name}'.");
        }

        return finalClip;
    }

    public AudioClip PlayOnce(AudioClip clip)
    {
        return PlayOnce(clip, Source, gameObject);
    }
    public AudioClip PlayOnce(AudioClip clip, AudioSource source, GameObject obj)
    {
        AudioSource finalSource = source ?? Source;
        AudioClip finalClip = clip ?? Clip;
        GameObject finalObj = obj ?? gameObject;

        if (finalSource == null && obj != null)
        {
            finalSource = finalObj.GetComponent<AudioSource>();
        }

        if (finalClip == null && finalSource != null)
        {
            finalClip = finalSource.clip;
        }

        if (finalClip == null)
        {
            Debug.LogWarning("AudioClip is null. Please provide a valid AudioClip.");
            return null;
        }

        if (finalSource != null)
        {
            finalSource.loop = false;
            finalSource.PlayOneShot(finalClip);
        }
        else
        {
            Debug.LogWarning($"AudioSource or AudioClip '{finalClip?.name}' is not set.");
        }

        return finalClip;
    }

}
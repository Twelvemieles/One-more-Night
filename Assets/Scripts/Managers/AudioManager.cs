using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SFXScriptableObject sfxConfig;
    [SerializeField] private AudioSource audioSource;
    public void PlaySFX(string audioID)
    {
        PlaySFX(audioID, audioSource);
    }
    public void PlaySFX(string audioID, AudioSource myAudioSource = null,float volumen = 1f)
    {
        AudioSource audioSourceToUse = myAudioSource == null ? audioSource : myAudioSource;
        audioSourceToUse.PlayOneShot(GetAudioUsingID(audioID), volumen);
    }
    public AudioClip GetAudioUsingID(string audioID)
    {
        SFXConfig config = sfxConfig.SFXConfigs.Find(x => x.id == audioID);
        return config.audioClip;
    }
}

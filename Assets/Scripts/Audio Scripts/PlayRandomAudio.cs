using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomAudio : MonoBehaviour
{
    public AudioClip[] audioClips;
    public float minPitch = 0.8f, maxPitch = 1.2f;
    public bool playOnAwake = true;

    private AudioSource thisAudio;

    // Start is called before the first frame update
    void Awake()
    {
        thisAudio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if(playOnAwake)
        {
            PlayAudio(Random.Range(0, audioClips.Length));
        }
    }

    public void PlayAudio(int audioId)
    {
        thisAudio.pitch = Random.Range(minPitch, maxPitch);
        thisAudio.PlayOneShot(audioClips[audioId]);
    }
}

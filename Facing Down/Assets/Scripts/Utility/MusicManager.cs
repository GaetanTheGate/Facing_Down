using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioMixerGroup audioMixer;
    public List<AudioClip> musicList;

    private AudioSource audioSource;
    private int currentMusicIndex;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.7f;
        audioSource.outputAudioMixerGroup = audioMixer;
        currentMusicIndex = 0;
        audioSource.clip = musicList[currentMusicIndex];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentMusicIndex = Random.Range(0, musicList.Count);
            audioSource.clip = musicList[currentMusicIndex];
            audioSource.Play();
        }
    }
}

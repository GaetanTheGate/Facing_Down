using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSoundAtParticlePosition : MonoBehaviour
{
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        GameObject particleEffectAudio = new GameObject("Particle Effect Audio");
        particleEffectAudio.transform.position = transform.position;
        particleEffectAudio.AddComponent<AudioSource>();

        particleEffectAudio.GetComponent<AudioSource>().volume = 0.5f;
        particleEffectAudio.GetComponent<AudioSource>().PlayOneShot(audioClip);
        Destroy(particleEffectAudio, audioClip.length);
    }
}

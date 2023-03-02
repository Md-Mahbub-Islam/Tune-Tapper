using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    static AudioClip userMusic;

    public static void SetAudioSource(AudioClip ac)
    {
        Debug.Log("SetAudioSource");
        userMusic = ac;
    }

    // Start is called before the first frame update
    void Start()
    {
        var music = GetComponent<AudioSource>();

        Debug.Log("userMusic: " + userMusic);
        if (userMusic != null)
        {
            music.clip = userMusic;
            music.Play();
        }

    }
}

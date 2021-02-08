using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource music;
    public AudioSource cafeMusic;

    public static float music_intensity;
    public static float cafeMusic_intensity;
    void Start()
    {
        music_intensity = music.volume;
        cafeMusic_intensity = cafeMusic.volume;
    }

    // Update is called once per frame
    void Update()
    {
        music.volume =  music_intensity;
        cafeMusic.volume = cafeMusic_intensity;
    }
}

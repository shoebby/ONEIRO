using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSource951 : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private int songNum;

    void Start()
    {
        songNum = 0;
        source.PlayOneShot(songs[songNum]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            songNum += 1;

            if (songNum > songs.Length)
                songNum = 0;

            source.PlayOneShot(songs[songNum]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music.volume = 0.01f;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

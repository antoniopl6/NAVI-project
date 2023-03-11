using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWackgroundMusic : MonoBehaviour
{
    public AudioSource wackgroundSound;
    //public AudioSource wackgroundSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playThisSoundEffect()
    {

        wackgroundSound.Play();
        //wackgroundSound.Play();

    }
}

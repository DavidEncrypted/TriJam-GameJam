using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource reloadsound;
    public AudioSource bagpopsound;
    public AudioSource hitsound;
    public AudioSource sniffsound;
    public AudioSource backgroundMusic;
    
    public void playReloadSound(){
        reloadsound.Play();
    }

    public void playBagPop(){
        bagpopsound.Play();
    }

    public void playHit(){
        hitsound.Play();
    }

    public void startBackgroundMusic(){
        backgroundMusic.Play();
    }

    public void playSniff(){
        sniffsound.Play();
    }

}

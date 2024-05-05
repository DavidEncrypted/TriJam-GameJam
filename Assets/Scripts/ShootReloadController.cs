using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootReloadController : MonoBehaviour
{
    public AudioManager audioManager;
    public Animator animSnos;
    public Animator animSnuisBuis;
    public GameObject zuigObject;
    public GameObject sniffTimerVisual;
    private static float reloadTime = 2.5f;
    private float reloadTimeRemaining = 0f;

    private static float sniffTimeMax = 8f;

    private float sniffTimeRemaining = sniffTimeMax;
    private bool shooting = false;
    private bool reloading = false;

    private bool bagPopSoundPlayed = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!reloading)
        {
            if (Input.GetMouseButton(0))
                shooting = true;
            else
                shooting = false;

            if (zuigObject.active){
                sniffTimeRemaining -= Time.deltaTime;
            }
            sniffTimerVisual.transform.localScale = new Vector3(2.0f * (sniffTimeRemaining/ sniffTimeMax),0.25f,1.0f);
            if (sniffTimeRemaining < 0 || Input.GetMouseButtonDown(1)){
                StartReload();
            }
        }
    }
    void FixedUpdate(){
        if (reloading){
            reloadTimeRemaining -= Time.fixedDeltaTime;
            if (!bagPopSoundPlayed){
                if (reloadTimeRemaining < 0.5f){
                    audioManager.playBagPop();
                    bagPopSoundPlayed = true;
                }
            }
            else if (reloadTimeRemaining < 0){
                reloading = false;
                animSnos.SetBool("Reloading", false);
                animSnuisBuis.SetBool("Reloading", false);
                sniffTimeRemaining = sniffTimeMax;
                zuigObject.SetActive(shooting);
            }
        }
        else{
            zuigObject.SetActive(shooting);
        }
    }
    private void StartReload(){
        if (sniffTimeRemaining != sniffTimeMax){
            shooting = false;
            reloadTimeRemaining = reloadTime;
            bagPopSoundPlayed = false;
            reloading = true;
            animSnos.SetBool("Reloading", true);
            animSnuisBuis.SetBool("Reloading", true);
            zuigObject.SetActive(false);
            audioManager.playReloadSound();
        }
    }
}

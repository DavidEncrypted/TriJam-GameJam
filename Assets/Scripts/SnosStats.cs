using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SnosStats : MonoBehaviour
{
    private int currentKillCount = 0;
    private int killThreshold = 10;

    public AudioManager audioManager;
    public delegate void OnKillThreshold();
    public static OnKillThreshold onKillThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentKillCount >= killThreshold)
        {
            onKillThreshold?.Invoke();
            currentKillCount = 0;
        }
    }

    private void OnEnable()
    {
        SpiderDamage.onSpiderDestroyed += (int value) =>
        {
            currentKillCount++;
            Globals.killCount += value;
        };

        SpiderDamage.onSpiderDestroyed += (int value) => audioManager.playSniff();
        
    }
}

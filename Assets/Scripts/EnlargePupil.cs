using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnlargePupil : MonoBehaviour
{
    public SpriteRenderer sr;

    public Sprite[] Sprites;
    private int pupilSize = 0;
    private Vector3 movement;

    public delegate void EyeFilled();

    public static EyeFilled eyeFilled;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = Sprites[pupilSize];
    }

    void EnlargePupilSize()
    {
        if (!Globals.sosModeEnabled)
        {
            return;
        }

        if (pupilSize < Sprites.Length - 1)
        {
            pupilSize++;
        }

        if (pupilSize > 10)
        {
            eyeFilled.Invoke();
        }
    }

    private void OnEnable()
    {
        SnosStats.onKillThreshold += EnlargePupilSize;
    }
}

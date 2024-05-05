using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject[] BarLines;
    public GameObject BarPrefab;

    private int CurrentHealth;
    private int MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HealthDown(){
        //Debug.Log("HealthDown");
        if (CurrentHealth > 0){
            BarLines[CurrentHealth-1].SetActive(false);
            CurrentHealth--;
        }
        
        

    }
    public void HealthUp(){
        Debug.Log("HealthUp");
        if (CurrentHealth < MaxHealth){
            CurrentHealth++;
            BarLines[CurrentHealth-1].SetActive(true);
        }
    }

    public void ResetHealth(){
        for (int i = CurrentHealth; i < MaxHealth; i++){
            BarLines[i-1].SetActive(true);
        }
        CurrentHealth = MaxHealth;
    }

    public bool isDead(){
        return CurrentHealth == 0;
    }

    public void SetupHealthBar(int Health){
        CurrentHealth = Health;
        MaxHealth = Health;
        BarLines = new GameObject[Health];

        for (int i = 0; i < Health; i++){
            BarLines[i] = Instantiate(BarPrefab, transform, false) as GameObject;
            BarLines[i].transform.Translate(0.2f + ((float)i*12.0f), 0, 0);
        }
    }
}

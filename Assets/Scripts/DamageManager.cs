using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public HealthBar healthBar;
    public AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject?.GetComponent<FollowPlayer>()){
            if (col.collider.gameObject.GetComponent<FollowPlayer>().Attacking == true){
                healthBar.HealthDown();
                audioManager.playHit();
            };
        }
        else if (col.collider.gameObject?.GetComponent<TriSpiderTeam>())
        if (col.collider.gameObject.GetComponent<TriSpiderTeam>().Attacking == true){
            healthBar.HealthDown();
            audioManager.playHit();
        }   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDamage : MonoBehaviour
{
    private GameObject Player;
    private GameObject zuigObject;
    public bool TakingDamage = false;
    
    private float TotalKillTime = 0.4f;
    private float remainingKillTime = 0.4f;

    private float TotalKillTimeTriTeam = 0.8f;

    private SpriteRenderer spriteRenderer;
    private Color initColor;
    private Color finalColor;

    public delegate void OnSpiderDestroyed(int value);
    public static OnSpiderDestroyed onSpiderDestroyed;
    
    // Start is called before the first frame update
    void Start()
    {
      if (gameObject.CompareTag("TriTeam")){
        TotalKillTime = TotalKillTimeTriTeam;
      }
      Player = GameObject.FindWithTag("Player");
      zuigObject = (Player.transform.Find("SnuisBuis/Zuigoppervlak")).gameObject;
      spriteRenderer = gameObject.GetComponent<SpriteRenderer>();   
      initColor = spriteRenderer.color;
      finalColor = Color.Lerp(initColor, Color.clear, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (zuigObject && !zuigObject.active){
            TakingDamage = false;
        }
        spriteRenderer.color = Color.Lerp(initColor, finalColor, Mathf.Clamp01((TotalKillTime - remainingKillTime) / TotalKillTime));
        if (TakingDamage){
            remainingKillTime -= Time.deltaTime;
            if(remainingKillTime < 0) {
                Destroy(transform.gameObject);

                int scoreValue = 10;
                if (gameObject.CompareTag("TriTeam"))
                {
                    scoreValue = 20;
                }
                onSpiderDestroyed?.Invoke(scoreValue);
            }
        }
        else if(remainingKillTime<TotalKillTime){
            remainingKillTime += Time.deltaTime;
            if (remainingKillTime>TotalKillTime) remainingKillTime = TotalKillTime;
        }
        
    }
}

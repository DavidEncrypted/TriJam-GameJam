using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZuigCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FollowPlayer>()) {
            DamageOn(other.gameObject);
        }
        else if (other.gameObject.GetComponent<TriSpiderTeam>()) {
            DamageOn(other.gameObject);
        }
        else if (other.gameObject.GetComponent<SwarmFollower>()) {
            DamageOn(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FollowPlayer>()) {
            DamageOff(other.gameObject);
        }
        else if (other.gameObject.GetComponent<TriSpiderTeam>()) {
            DamageOff(other.gameObject);
        }
        else if (other.gameObject.GetComponent<SwarmFollower>()) {
            DamageOff(other.gameObject);
        }
    }

    private void DamageOn(GameObject spider){
        var spiderDamageComponent = spider.GetComponent<SpiderDamage>();
        if (spiderDamageComponent != null)
            spiderDamageComponent.TakingDamage = true;
    }

    private void DamageOff(GameObject spider){
        var spiderDamageComponent = spider.GetComponent<SpiderDamage>();
        if (spiderDamageComponent != null)
            spiderDamageComponent.TakingDamage = false;
    }
}

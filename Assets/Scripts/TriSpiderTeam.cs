using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriSpiderTeam : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D PlayerRB;
    public Rigidbody2D TeamMember1RB;
    public Rigidbody2D TeamMember2RB;


    public float MoveSpeed = 0.5f;
    public Animator anim;

    public float TargetRange = 1.0f;
    public float AttackRange = 2.0f;

    public float TargetPullMultiplier = 2.8f;

    private Vector2 movement;
    private Vector2 targetDistance;
    private Vector2 TM1DistanceNormalized;
    private Vector2 TM2DistanceNormalized;
    private float AttackCharge = 0.0f;
    private float AttackTimer = 0.0f;
    public float AttackChargeTime = 2.0f;
    public float AttackTimerTime = 2.0f;
    public float AttackPower = 20.0f;
    public bool Attacking = false;
    private bool Charging = false;
    private Vector2 TeamMember1Position;
    private Vector2 TeamMember2Position;
    
    void Start(){
        PlayerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        //Difference = PlayerRB.position - rb.position;
        //targetDistance = Vector2.targetDistance(PlayerRB.position, rb.position);

        targetDistance = PlayerRB.position - rb.position;
        
        if (targetDistance.magnitude < AttackRange && Charging == false && Attacking == false){
            Charging = true;
            //Debug.Log("Charging true");
            AttackCharge = AttackChargeTime;
        }
        //Debug.Log(AttackCharge);
        if (AttackCharge > 0.0f){
            AttackCharge -= Time.deltaTime;
        }
        else if(Charging == true){
            Charging = false;
            doAttack();
            AttackTimer = AttackTimerTime;
            Attacking = true;
        }

        if (AttackTimer > 0.0f){
            AttackTimer -= Time.deltaTime;
        }
        else if(Attacking == true){
            Attacking = false;
        }

        if (targetDistance.magnitude > TargetRange){
            if (TeamMember1RB != null) { 
                TeamMember1Position = TeamMember1RB.position;
                TM1DistanceNormalized = (rb.position - TeamMember1Position).normalized;
            }
            else
            {
                TeamMember1Position = rb.position;
                TM1DistanceNormalized = Vector2.zero;
            }
            if (TeamMember2RB != null) { 
                TeamMember2Position = TeamMember2RB.position;
                TM2DistanceNormalized = (rb.position - TeamMember2Position).normalized;
            }
            else
            {
                TeamMember2Position = rb.position;
                TM2DistanceNormalized = Vector2.zero;
            }
            
            movement = TargetPullMultiplier * (targetDistance.normalized) + TM1DistanceNormalized + TM2DistanceNormalized;
            movement.Normalize();
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);
        }
        else movement = Vector2.zero;
        
    }

    void FixedUpdate()
    {
        //Movement
        if (Attacking == false && Charging == false)
            rb.MovePosition(rb.position + movement * (MoveSpeed * Time.fixedDeltaTime));

    }

    void doAttack(){
        
        // TODO: animations
        anim.SetTrigger("AttackingTrigger");
        //anim.SetBool("Attacking", false);
        //Debug.Log("ATTACK");
        rb.AddForce(targetDistance.normalized * AttackPower, ForceMode2D.Impulse);
    }
}
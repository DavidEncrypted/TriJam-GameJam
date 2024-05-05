using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    private Rigidbody2D PlayerRB;
    public float MoveSpeed = 0.5f;
    public Animator anim;

    public float TargetRange = 1.0f;
    public float AttackRange = 2.0f;

    private Vector2 movement;
    private Vector2 scrollMovement;
    private Vector2 Distance;

    private float AttackCharge = 0.0f;
    private float AttackTimer = 0.0f;
    public float AttackChargeTime = 2.0f;
    public float AttackTimerTime = 2.0f;
    public float AttackPower = 20.0f;
    public bool Attacking = false;
    private bool Charging = false;
    
    void Start(){
        PlayerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //Input
        //Difference = PlayerRB.position - rb.position;
        //Distance = Vector2.Distance(PlayerRB.position, rb.position);

        if (!PlayerRB)
        {
            return;
        }
        
        Distance = PlayerRB.position - rb.position;
        
        if (Distance.magnitude < AttackRange && Charging == false && Attacking == false){
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

        if (Distance.magnitude > TargetRange){
            movement = Distance;
            movement.Normalize();
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);
        }
        else movement = Vector2.zero;


        scrollMovement.x = -2f;
        scrollMovement.Normalize();   
    }

    void FixedUpdate()
    {
        //Movement
        Vector2 newPosition = rb.position + scrollMovement * (Constants.ScrollSpeed * MoveSpeed * Time.fixedDeltaTime);

        if (Attacking == false && Charging == false){
            newPosition += movement * (MoveSpeed * Time.fixedDeltaTime);

            rb.MovePosition(newPosition);
        }
    }

    void doAttack(){
        
        // TODO: animations
        anim.SetTrigger("AttackingTrigger");
        //anim.SetBool("Attacking", false);
        //Debug.Log("ATTACK");
        rb.AddForce(Distance.normalized * AttackPower, ForceMode2D.Impulse);
    }
}

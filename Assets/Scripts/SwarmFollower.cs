using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmFollower : MonoBehaviour
{

    private GameObject Target;
    public Rigidbody2D rb;
    Vector2 movement;
    Vector2 Distance;
    public float MoveSpeed = 0.5f;
    public float TargetRange = 1.0f;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindWithTag("Player");
        Debug.Log(Target);
    }

    // Update is called once per frame
    void Update()
    {
        Distance = (Vector2)Target.transform.position - rb.position;
        
        if (Distance.magnitude > TargetRange){
            movement = Distance;
            movement.Normalize();
        }
        else movement = Vector2.zero;

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * (MoveSpeed * Time.fixedDeltaTime));

    }
}

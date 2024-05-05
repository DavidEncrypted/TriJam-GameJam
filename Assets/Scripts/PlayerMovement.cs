using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject snuisBuis; 
    public float MoveSpeed = 1;
    public float RotationSpeed = 120f;
    //public Animator anim;

    
    private Transform snuisBuisTransform;
    private Vector2 movement;
    private Vector2 scrollMovement;

    private Camera camera;
    private Vector2 mousePosition = Vector2.zero;
    private Vector2 relativeMousePosition = Vector2.zero;
    private Vector2 mouseDirection;
    private float angle = 0f;
    private float angleRad = 0f;

    void Awake(){
        camera = Camera.main;
        snuisBuisTransform = snuisBuis.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");
        scrollMovement.x = -1f;

        
        movement.Normalize();
        scrollMovement.Normalize();

        mousePosition = Input.mousePosition;
        relativeMousePosition = camera.ScreenToWorldPoint(mousePosition);

        angleRad = Mathf.Atan2 (relativeMousePosition.y - gameObject.GetComponent<Transform>().position.y, relativeMousePosition.x - gameObject.GetComponent<Transform>().position.x);
        angle = (180 / Mathf.PI) * angleRad + 270;

        //Debug.Log(angle);

        // anim.SetFloat("Horizontal", movement.x);
        // anim.SetFloat("Vertical", movement.y);
        // anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        //Movement
        Vector2 newPosition = rb.position + movement * MoveSpeed * Time.fixedDeltaTime;
        newPosition += scrollMovement * (Constants.ScrollSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
        Vector3 toAngle = new Vector3(0,0,1) * angle;
        if (Vector3.Distance(snuisBuisTransform.eulerAngles, toAngle) > 0.5f)
        {
            snuisBuisTransform.rotation = Quaternion.RotateTowards(snuisBuisTransform.rotation, Quaternion.Euler(toAngle), RotationSpeed * Time.fixedDeltaTime);
        }
        else{
            snuisBuisTransform.eulerAngles = toAngle;
        }
    }
}

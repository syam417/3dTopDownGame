using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;    
    float newYRotation;
    public Quaternion newRotation{ get {return Quaternion.Euler(0, newYRotation, 0); } }
    public Vector2 moveInput {get{ return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); }}
    public bool jumping,grounded;
    [SerializeField] float groundedDistance;
    public AnimationCurve jumpingCurve;
    [SerializeField] float jumpForce, jumpDuration;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //perubahan posisi
            transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y, transform.position.z + Input.GetAxis("Vertical") * speed * Time.deltaTime);

            //perubahan rotasi
            Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            float atanInput = Mathf.Atan2(inputVector.x, inputVector.y);
            newYRotation = atanInput * Mathf.Rad2Deg;                        
        }

        if(Input.GetButtonDown("Jump")){
            if (grounded && !jumping)
            {
                StopCoroutine("Jumping");
                StartCoroutine("Jumping");  
            }
        }        


    }

    void FixedUpdate(){

        RaycastHit hit;        
        Debug.DrawRay(transform.position, -transform.up*groundedDistance);
        if(Physics.Raycast(transform.position, -transform.up,out hit, groundedDistance, 1<<3|1<<6|1<<7)){
            grounded = true;            
        }else{
            grounded = false;            
            if(!jumping){
                StopCoroutine("Falling");
                StartCoroutine("Falling");
            }
        }
    }

    IEnumerator Jumping(){
        jumping = true;
        float t = 0;
        float jumpStartPos = transform.position.y;
        float evaluationTime = 0;
        while(jumping){
            t+=Time.deltaTime;
            evaluationTime = Mathf.Clamp01(t / jumpDuration);
            float jumpPos = jumpingCurve.Evaluate(evaluationTime);
            jumping = grounded && evaluationTime>.5f ? false : true;
            transform.position = new Vector3(transform.position.x, transform.position.y + jumpPos*jumpForce, transform.position.z);
            yield return null;
        }
        jumping = false;
    }

    IEnumerator Falling(){        
        while(!grounded){            
            transform.position = new Vector3(transform.position.x, transform.position.y - (5 * Time.deltaTime), transform.position.z);            
            yield return null;
        }
    }


    void OnCollisionEnter(Collision other){
        // print("grounded");
        // jumping = false;
    }

    void OnTriggerExit(Collider other){
        // if(other.CompareTag("Ground")){
        //     grounded = false;
        // }
    }

    void OnTriggerEnter(Collider other){
        // if(other.CompareTag("Ground")){
        //     grounded = true;
        // }
    }
}

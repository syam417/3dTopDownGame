using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    float avatarRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Mathf.Abs(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical")) > 0)
        {
            transform.position = new Vector3(transform.position.x + (Input.GetAxis("Horizontal")*moveSpeed*Time.deltaTime), transform.position.y, transform.position.z + (Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime));            
            avatarRotation = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))*Mathf.Rad2Deg;
            Quaternion newRot = Quaternion.Euler(0, avatarRotation, 0);
            transform.GetChild(0).localRotation = Quaternion.RotateTowards(transform.GetChild(0).localRotation, newRot, rotationSpeed * Time.deltaTime);            
        }

        
    }
}

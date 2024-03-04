using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Transform charVis;
    public Vector2 moveInput {get{ return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); }}
    Rigidbody rb;
    bool jumping;
    [SerializeField] float jumpForce;
    [SerializeField] float rayDistance;
    bool grounded;
    public bool isGrounded {get{ return grounded; }}

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //input locomotion
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //perubahan posisi
            transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y, transform.position.z + Input.GetAxis("Vertical") * speed * Time.deltaTime);

            //perubahan rotasi
            Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            float atanInput = Mathf.Atan2(inputVector.x, inputVector.y);
            float newYRotation = atanInput * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.Euler(0, newYRotation, 0);
            charVis.rotation = Quaternion.RotateTowards(charVis.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }
    }

    void FixedUpdate(){

        //input Jump
        if(jumping){
            rb.AddForce(transform.up*jumpForce, ForceMode.Impulse);
            jumping = false;
        }

        RaycastHit hit;
        LayerMask layerMask = 1 << 6 | 1 << 7|1<<3;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit, rayDistance,layerMask)){
            grounded = true;
        }else{
            grounded = false;
        }

        Debug.DrawRay(transform.position, -Vector3.up * rayDistance);
        Debug.Log("grounded " + grounded);
    }
}

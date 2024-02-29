using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal")*speed*Time.deltaTime, transform.position.y, transform.position.z + Input.GetAxis("Vertical")*speed*Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    Animator anim;
    Movement playerInput;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponentInChildren<Animator>();
        playerInput = transform.root.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        //set animasi locomotion
        float velocity = playerInput.moveInput.magnitude;
        anim.SetFloat("Velocity", velocity);

        anim.SetBool("isGrounded", playerInput.isGrounded);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimation : MonoBehaviour
{
    Movement movementInput;
    PlayerInput pInput;
    Animator anim;
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float jumpStart, jumpApex, jumpDuration, jumpTime, jumpForce;
    bool isJumping;
    
    // Start is called before the first frame update
    void Start()
    {
        movementInput = transform.root.GetComponent<Movement>();
        pInput = transform.root.GetComponent<PlayerInput>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, pInput.newRotation, pInput.rotationSpeed * Time.deltaTime);
        anim.SetFloat("Speed", movementInput.moveInput.magnitude);

        anim.SetBool("Grounded", pInput.grounded);        

        if(!isJumping){
            StopCoroutine("Jumping");
            StartCoroutine("Jumping");            
        }
    }

    IEnumerator Jumping(){
        isJumping = true;
        float timer = 0;
        float yPos;
        bool isFalling=false;
        while(pInput.jumping){            
            anim.SetFloat("JumpingSequence", Mathf.Clamp01(timer));
            yPos = transform.position.y;
            if (isFalling) { timer += 1; }            
            yield return new WaitForFixedUpdate();
            isFalling = (transform.position.y - yPos) < 0 ? true : false;
            yield return null;
        }
        anim.SetFloat("JumpingSequence", 0);
        isJumping = false;
    }
}

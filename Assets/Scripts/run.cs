using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : MonoBehaviour
{
    public float speed;
    //private Animator animator;
    private float horizontalMoveInput;
    private float verticalMoveInput;
    private Rigidbody2D rb;
    private bool facingRight=true;


    private void Start()
    {
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        horizontalMoveInput = Input.GetAxis("Horizontal");
        verticalMoveInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontalMoveInput * speed, verticalMoveInput * speed);

        if (facingRight == false && horizontalMoveInput > 0 || facingRight == true && horizontalMoveInput < 0)
        {
            Flip();
        }
        /*
        if(horizontalMoveInput > 0|| horizontalMoveInput < 0||verticalMoveInput>0||verticalMoveInput<0)
        {
            animator.SetBool("is_running", true);
        }
        else
        {
            animator.SetBool("is_running", false);
        }
        */
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler=transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}

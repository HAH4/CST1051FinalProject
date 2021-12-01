using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpLength = 2;
    [SerializeField] private float jumpHeight = 5;
    
    private Collider2D coll;
    private bool facingLeft = true;


    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //Transition from Jump to Fall
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < -.1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        //Transiition from Fall to Idle
        if(coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            //ensures sprite is facing the correct way

            if (transform.position.x > leftCap)
            {

                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                //test to see if frog is on ground, if so jump
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }

            //if it is not greater then frog will face right
        }

        else
        {
            //check if the x value is greater than the leftCap
            //ensures sprite is facing the correct way
            if (transform.position.x < rightCap)
            {
                //test to see if frog is on ground, if so jump
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

}

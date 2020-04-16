using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    //Test non-preFabs
    //public Rigidbody2D fox;
    //public Animator foxAnim;
    //public Collider2D collFox;

    //Test preFabs
    private Rigidbody2D fox;
    private Animator foxAnim;
    private Collider2D collFox;
    private enum State { idle, running, jumping, falling, hurt }; //State {0, 1, 2, ....}
    private State state = State.idle; //keadaan

    [SerializeField] private LayerMask ground;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText;
    //public int cherries = 0;
    [SerializeField] private float hurtForce = 10f;

    private void Start() {
        fox = GetComponent<Rigidbody2D>();
        foxAnim = GetComponent<Animator>();
        collFox = GetComponent<Collider2D>();
    }    

    private void Update()
    {
        if (state != State.hurt) {
            Movement();
        }
        VelocityState();
        foxAnim.SetInteger("state",(int)state);
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        // if (Input.GetKey(KeyCode.A)) {
        if (hDirection < 0)
        {
            fox.velocity = new Vector2(-5, fox.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        // else if (Input.GetKey(KeyCode.D)) {
        else if (hDirection > 0)
        {
            //fox.velocity = new Vector2(5, 0);
            fox.velocity = new Vector2(5, fox.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        if (Input.GetButtonDown("Jump") && collFox.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        fox.velocity = new Vector2(fox.velocity.x, 7);
        state = State.jumping;
    }

    private void VelocityState()
    {

        if (state == State.jumping)
        {
            if (fox.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (collFox.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(fox.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(fox.velocity.x) > 2f)
        {
            //moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //testing
            //Destroy(collision.gameObject);

            if (state == State.falling)
            {
                Destroy(collision.gameObject);
                Jump();
            }
            else
            {
                state = State.hurt;
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    //enemy is to the right, respawn left
                    fox.velocity = new Vector2(-hurtForce, fox.velocity.y);
                }
                else
                {
                    //enemy is to the left, respawn right
                    fox.velocity = new Vector2(hurtForce, fox.velocity.y);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable") {
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
    }

}
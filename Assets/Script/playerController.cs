using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private enum State { idle, running, jumping, falling }; //State {0, 1, 2, ....}
    private State state = State.idle; //keadaan

    [SerializeField] private LayerMask ground;

    public int cherries = 0;

    private void Start() {
        fox = GetComponent<Rigidbody2D>();
        foxAnim = GetComponent<Animator>();
        collFox = GetComponent<Collider2D>();
    }    

private void Update()
{
    float hDirection = Input.GetAxis("Horizontal");

    // if (Input.GetKey(KeyCode.A)) {
    if (hDirection < 0) {
        fox.velocity = new Vector2(-5, fox.velocity.y);
        transform.localScale = new Vector2 (-1, 1);
    }

    // else if (Input.GetKey(KeyCode.D)) {
    else if (hDirection > 0) {
        //fox.velocity = new Vector2(5, 0);
        fox.velocity = new Vector2(5, fox.velocity.y);
        transform.localScale = new Vector2 (1, 1);
    }

    if (Input.GetButtonDown("Jump") && collFox.IsTouchingLayers(ground)) {
        fox.velocity = new Vector2(fox.velocity.x, 7);
            state = State.jumping;
    }

    VelocityState();
    foxAnim.SetInteger("state",(int)state);
}

    private void VelocityState() {
        
        if (state == State.jumping) {
            if (fox.velocity.y < .1f) {
                state = State.falling;
            }
        }
        else if (state == State.falling) {
            if (collFox.IsTouchingLayers(ground)) {
                state = State.idle;
            }
        } 

        else if (Mathf.Abs(fox.velocity.x) > 2f) {
            //moving
            state = State.running;
        }
        else {
            state = State.idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable") {
            Destroy(collision.gameObject);
            cherries += 1;
        }
    }

}
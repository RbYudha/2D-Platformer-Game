using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Rigidbody2D fox;
    public Animator foxAnim;
    private enum State {idle, running, jumping}; //State {0, 1, 2, ....}
    private State state = State.idle; //keadaan

    // private Rigidbody2D fox;
    //[SerializeField]
    // private Animator foxAnim;

// private void start() {
//     fox = GetComponent<Rigidbody2D>();
//     foxAnim = GetComponent<Animator>();
// }    

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

    else {
        
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
        fox.velocity = new Vector2(fox.velocity.x,5);
    }

    velocityState();
}

    private void velocityState() {
        
    }

}

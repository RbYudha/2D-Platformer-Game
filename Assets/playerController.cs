using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Rigidbody2D fox;

private void Update ()
{
    if (Input.GetKey(KeyCode.A)) {
        fox.velocity = new Vector2(-5, fox.velocity.y);
        transform.localScale = new Vector2 (-1, 1);
    }

    if (Input.GetKey(KeyCode.D)) {
        //fox.velocity = new Vector2(5, 0);
        fox.velocity = new Vector2(5, fox.velocity.y);
        transform.localScale = new Vector2 (1, 1);
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
        fox.velocity = new Vector2(fox.velocity.x,5);
    }

}

}

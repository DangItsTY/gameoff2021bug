using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandola : MonoBehaviour
{
    // Object References
    private Rigidbody2D rb;

    // Object Properties
    private float jumpSpeed = 7f;
    private float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float vx = Input.GetAxisRaw("Horizontal") * speed;
        float vy = Input.GetButtonDown("Jump") ? jumpSpeed : rb.velocity.y;
        rb.velocity = new Vector2(vx, vy);
    }
}

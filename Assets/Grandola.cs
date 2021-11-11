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
    private float vx;
    private float vy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // trying with force
        if (Input.GetAxis("Horizontal") != 0f)
        {
            vx = Input.GetAxis("Horizontal") * speed;
            rb.AddForce(new Vector2(vx, 0.0f), ForceMode2D.Force);
        }
        if (Input.GetButtonDown("Jump"))
        {
            vy = jumpSpeed;
            rb.AddForce(new Vector2(0.0f, vy), ForceMode2D.Impulse);
        }
        
        /*
        // old - directly modified velocity
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            vx = Input.GetAxis("Horizontal") * speed;
            vx += rb.velocity.x; // preserve vx
            vx = Mathf.Abs(vx) > speed ? speed * Mathf.Sign(vx) : vx; // cap at max speed
        }
        float vy = Input.GetButtonDown("Jump") ? jumpSpeed : rb.velocity.y;
        rb.velocity = new Vector2(vx, vy);
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collided " + collision.gameObject.name);
        if (collision.gameObject.name == "GrasshopperPlatform" || collision.gameObject.name == "Grasshopper")
        {
            //Debug.Log("collided " + collision.gameObject.name);

            //rb.position = collision.rigidbody.position;
            
            //rb.velocity = new Vector2(rb.velocity.x, collision.rigidbody.velocity.y);
            //collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, collision.rigidbody.velocity.y);
            //collision.rigidbody.velocity = new Vector2(0f, 0f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // grasshopper platform
        if (collision.gameObject.name == "GrasshopperPlatform")
        {
            //Debug.Log("triggered " + collision.gameObject.name);

            // ride grasshopper
            Rigidbody2D[] cols = collision.GetComponentsInParent<Rigidbody2D>();
            Rigidbody2D colP = null;
            foreach (Rigidbody2D e in cols)
            {
                if (e.gameObject.name == "Grasshopper")
                {
                    colP = e;
                    break;
                }
            }
            if (colP == null)
            {
                Debug.Log("could not find the parent grasshopper");
                return;
            }

            Rigidbody2D rbCol = collision.GetComponent<Rigidbody2D>();
            rb.position = new Vector2(rb.position.x, rbCol.position.y + 0.5f);
            if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(colP.velocity.x))
            {
                //Debug.Log("speed other " + colP.velocity.x);
                rb.velocity = new Vector2(colP.velocity.x, rb.velocity.y);
            }

            //rb.position = new Vector2(rbCol.position.x, rbCol.position.y + 0.5f);

            //rb.velocity = new Vector2(0.0f, 0.0f);
            //rb.velocity = new Vector2(0.0f, 0.0f);
        }
        // grasshopper
        if (collision.gameObject.name == "Grasshopper")
        {
            // jump kill grasshopper
            if (Input.GetButtonDown("Jump"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("triggered " + collision.gameObject.name);
        if (collision.gameObject.name == "GrasshopperPlatform" || collision.gameObject.name == "Grasshopper")
        {
            //Debug.Log("triggered " + collision.gameObject.name);
        }
    }
}

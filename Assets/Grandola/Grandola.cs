using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grandola : MonoBehaviour
{
    // Component References
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private AudioSource audioSource;

    // Object Properties
    private float jumpSpeed = 7f;
    private float speed = 4f;
    private float vx;
    private float vy;
    private GameObject grasshopper = null;
    private bool invulnerable = false;
    private bool riding = false;

    // Controls
    private bool jumpReady = false;
    private bool controlsDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("combo", 1);
    }

    // Update is called once per frame
    void Update()
    {
        // pre input

        if (grasshopper == null)
        {
            riding = false; // don't ride if no grasshopper target
        }

        // input
        if (!controlsDisabled)
        {
            float inputX = Input.GetAxis("Horizontal");
            if (inputX != 0f)
            {
                vx = inputX * speed;
                rb.AddForce(new Vector2(vx, 0.0f), ForceMode2D.Force);
                if (!animator.GetBool("WalkBool")) animator.SetBool("WalkBool", true);
                if (!sr.flipX && Mathf.Sign(inputX) == -1)
                {
                    sr.flipX = true;
                }
                else if (sr.flipX && Mathf.Sign(inputX) == 1)
                {
                    sr.flipX = false;
                }
            }
            else
            {
                if (animator.GetBool("WalkBool")) animator.SetBool("WalkBool", false);
            }
            if (Input.GetButtonDown("Jump") && jumpReady)
            {
                // jump
                vy = jumpSpeed;
                rb.AddForce(new Vector2(0.0f, vy), ForceMode2D.Impulse);
                jumpReady = false;
                animator.SetTrigger("JumpTrigger");

                // jump kill grasshopper
                if (grasshopper != null)
                {
                    //Debug.Log("Kill");
                    Destroy(grasshopper);
                    int combo = PlayerPrefs.GetInt("combo");
                    PlayerPrefs.SetInt("combo", combo + 1);
                    int score = PlayerPrefs.GetInt("score");
                    PlayerPrefs.SetInt("score", score + (1 * combo));
                }

                // always reset grasshopper on a jump
                grasshopper = null;
            }
        }

        // post input
        
        // invulnerable while jumping aka in positive vy
        if (rb.velocity.y > 0)
        {
            invulnerable = true;
        } else
        {
            invulnerable = false;
        }
    }
    private void FixedUpdate()
    {
        // ride grasshopper physics
        if (grasshopper != null && riding)
        {
            Rigidbody2D rbCol = grasshopper.GetComponent<Rigidbody2D>();
            rb.position = new Vector2(rb.position.x, rbCol.position.y + 1.0f);

            float vx = Mathf.Abs(rb.velocity.x) < Mathf.Abs(rbCol.velocity.x) ? rbCol.velocity.x : rb.velocity.x;
            rb.velocity = new Vector2(vx, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collided " + collision.gameObject.name);
        // ready jump if on a floor or platform
        if (collision.gameObject.name == "Floor" || collision.gameObject.name == "GrasshopperPlatform")
        {
            //Debug.Log("collided " + collision.gameObject.name);
            jumpReady = true;
        }
        if (collision.gameObject.name == "Floor")
        {
            PlayerPrefs.SetInt("combo", 1);
        }
        if (collision.gameObject.name == "GrasshopperPlatform")
        {
            riding = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GrasshopperPlatform")
        {
            grasshopper = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("triggered " + collision.gameObject.name);
        if (collision.gameObject.name == "Grasshopper" || collision.gameObject.name == "KillerGrasshopper")
        {
            grasshopper = collision.gameObject;
            jumpReady = true; // enable jump even while "inside" a grasshopper
        }
        // hurt player
        if (!invulnerable && collision.gameObject.tag == "Hitbox")
        {
            //Debug.Log("Hit!");
            StartCoroutine(Death());
        }
    }
    IEnumerator Death()
    {
        invulnerable = true;
        controlsDisabled = true;
        audioSource.Play();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Thankyou");
    }
}

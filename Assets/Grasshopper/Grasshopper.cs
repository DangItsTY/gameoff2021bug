using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grasshopper : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor" || collision.gameObject.name == "WallLeft" || collision.gameObject.name == "WallRight")
        {
            animator.SetTrigger("JumpTrigger");
        }
        if (collision.gameObject.name == "WallLeft" || collision.gameObject.name == "WallRight")
        {
            sr.flipX = sr.flipX ? false : true;
        }
    }
    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(audioSource.clip, gameObject.transform.position);
    }
}

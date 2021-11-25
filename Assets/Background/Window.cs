using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject original;
    public Vector2 position;
    public float vx = 2f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        /*animator = GetComponent<Animator>();*/
    }

    // Update is called once per frame
    void Update()
    {
/*        animator.GetAnimatorTransitionInfo
            animator.GetCurrentAnimatorClipInfo
            animator.GetCurrentAnimatorStateInfo
            animator.sta*/
/*        GameObject newObject = Instantiate(original, position, Quaternion.identity);
        newObject.name = original.name;
        newObject.GetComponent<Rigidbody2D>().velocity = new Vector2(vx, 0f);*/
    }
}

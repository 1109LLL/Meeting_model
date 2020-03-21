using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleToWave : MonoBehaviour
{
    public Animator animator;
    // float InputX;
    public float InputY;
    public bool Wave; 

    // Start is called before the first frame update
    void Start()
    {
        // Get animator
        animator = this.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        InputY = Input.GetAxis("Vertical");
    }

    public void startWaving()
    {
        animator.SetBool("Wave", true);
    }

    public void stopWaving()
    {
        animator.SetBool("Wave", false);
    }

    public void startThumbsUp()
    {
        animator.SetBool("ThumbsUp", true);
    }

    public void stopThumbsDown()
    {
        animator.SetBool("ThumbsUp", false);
    }
}

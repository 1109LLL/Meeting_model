using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleToWave : MonoBehaviour
{
    public Animator animator;
    float InputX;
    public float InputY;

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
        animator.SetFloat("InputY", -1);
    }

    public void stopWaving()
    {
        animator.SetFloat("InputY", 0);
    }
}

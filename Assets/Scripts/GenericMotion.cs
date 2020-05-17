using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMotion : MonoBehaviour
{
    Animator animator;

    float speed = 0.0f;
    bool isMoving = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }



    // Start is called before the first frame update
    public void Moving()
    {
        if (speed > 0)
            return;
        speed = 1.0f;
        animator.SetFloat("Speed", speed);
    }

    public void StopMove()
    {
        isMoving = false;
        speed = 0.0f;
        animator.SetFloat("Speed", speed);
    }

    public void IncreaseSpeed()
    {
        if (speed<3)
        {
            speed += 1.0f;
        }
        animator.SetFloat("Speed", speed);
    }
}

using LeoLuz.PlugAndPlayJoystick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    AnalogicKnob analogicKnob;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float rotateSpeed;

    [SerializeField]
    GenericMotion genericMotion;

    Rigidbody rigidbody;

    float movingTime = 0;
    float baseMoveSpeed = 0;
    float nextIncreaseSpeedTime = 0;
    int totalIncreaseSpeed = 0;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        baseMoveSpeed = moveSpeed;
    }


    public  void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        bool canMove = moveHorizontal != 0 || moveVertical != 0;

        if (canMove)
        {
            Vector3 moveVec = new Vector3(moveHorizontal, 0, moveVertical);
            rigidbody.AddForce(moveVec * moveSpeed);
            Turning();
            genericMotion.Moving();
           
           
            if (totalIncreaseSpeed < 3 &&  Time.time - movingTime > nextIncreaseSpeedTime)
            {
                moveSpeed += 500;
                genericMotion.IncreaseSpeed();
                movingTime = Time.time;
                nextIncreaseSpeedTime = Random.Range(5, 7);
                totalIncreaseSpeed++;
            }
                
        }
        else
        {
            //rigidbody.velocity = Vector3.zero;
            moveSpeed = baseMoveSpeed;
            movingTime = Time.time;
            nextIncreaseSpeedTime = Random.Range(5, 7);
            genericMotion.StopMove();
            totalIncreaseSpeed = 0;
        }
    }


    void Turning()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveVector = (Vector3.right * moveHorizontal + Vector3.forward * moveVertical);

        var currentRotationSpeed = rotateSpeed * Mathf.Max(0, (1 - 0.3f) / 0.7f);


        // The step size is equal to speed times frame time.
        float singleStep = currentRotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveVector, singleStep, 0.0f);

        Quaternion nextRotation = Quaternion.LookRotation(newDirection);

        rigidbody.MoveRotation(nextRotation);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] Vector3 position, velocity,aceleration;
    [SerializeField] float velocityLimit,inicialLimitVelocity,limitX,limitY,radBall,realLimitX,realLimitY;
    void Start()
    {
        realLimitX = limitX - radBall;
        realLimitY = limitY - radBall;
        velocityLimit = inicialLimitVelocity;
    }

    private void Move()
    {
        if (transform.position.x >= realLimitX)
        {
            position.x = realLimitX;
            transform.position = position;
            aceleration.x = aceleration.x * -1;
            velocity.x = velocity.x * -1;
            
        }
        else if (transform.position.x <= -realLimitX)
        {
            position.x = -realLimitX;
            transform.position = position;
            aceleration.x = aceleration.x * -1;
            velocity.x = velocity.x * -1;
        }
        else if (transform.position.y >= realLimitY)
        {
            position.y = realLimitY;
            transform.position = position;
            aceleration.y = aceleration.y * -1;
            velocity.y = velocity.y * -1;
        }
        else if (transform.position.y <= -realLimitY)
        {
            position.y = -realLimitY;
            transform.position = position;
            aceleration.y = aceleration.y * -1;
            velocity.y = velocity.y * -1;
        }
        velocity += aceleration * Time.deltaTime;
        velocity = limitVelocity(velocity, velocityLimit);
        position += velocity * Time.deltaTime;
        transform.position = position;
    }
    
    private Vector3 limitVelocity(Vector3 velocity,float limit)
    {
        if (velocity.magnitude > limit)
        {
            velocity = velocity.normalized * limit;
        }
        return velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.x < 0)
            {
                position.x = -realLimitX + 0.75f;
                transform.position = position;
            }
            else
            {
                position.x = realLimitX - 0.75f;
                transform.position = position;
            }
            aceleration.x = aceleration.x * -1;
            velocity.x = velocity.x * -1;
            if (velocityLimit < 20)
            {
                velocityLimit++;
            }
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}

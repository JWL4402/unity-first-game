using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public Rigidbody2D Body;
    public BoxCollider2D Box_Collider;
    public int jump_factor = 8;
    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Body.velocity;
        if (Input.GetKeyDown(KeyCode.Space) && Box_Collider.IsTouchingLayers())
        {
            velocity.y = jump_factor;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x = speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            velocity.x = speed * -1;
        }

        Body.velocity = velocity;
    }
}

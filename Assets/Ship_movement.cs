using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement direction based on the input
        Vector3 movement = new Vector3(horizontalInput, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;

        // Apply the movement to the Rigidbody
        transform.Translate(movement);

        // You can clamp the object's position if you want to keep it within certain bounds
        // For example, to restrict movement between -5 and 5 on the X-axis:
        // transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5f, 5f), transform.position.y, transform.position.z);


    }
}

//using UnityEngine;

//public class ObjectMovement : MonoBehaviour
//{
//    public float moveSpeed = 5.0f; // Adjust this speed to your liking

//    void Update()
//    {
//        float horizontalInput = Input.GetAxis("Horizontal");

//        // Calculate the movement direction based on the input
//        Vector3 movement = new Vector3(horizontalInput, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;

//        // Apply the movement to the Rigidbody
//        transform.Translate(movement);

//        // You can clamp the object's position if you want to keep it within certain bounds
//        // For example, to restrict movement between -5 and 5 on the X-axis:
//        // transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5f, 5f), transform.position.y, transform.position.z);
//    }
//}


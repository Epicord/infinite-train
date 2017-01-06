// Author(s): Paul Calande
// A script that handles player movement.

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // How fast the player moves. Higher = faster.
    private const float movementSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Initialize a new vector for storing the updated position.
        Vector3 newPosition = transform.position;
        // Let's check the player's keyboard inputs.
        // If the player presses left...
	    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Move left.
            // Time.deltaTime refers to the amount of time that has passed since the last frame.
            // Using this in object movement allows our game to run independently from framerate.
            newPosition.x -= movementSpeed * Time.deltaTime;
        }
        // If the player presses right...
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Move right.
            newPosition.x += movementSpeed * Time.deltaTime;
        }
        // If the player presses up...
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Move up.
            newPosition.z += movementSpeed * Time.deltaTime;
        }
        // If the player presses down...
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // Move down.
            newPosition.z -= movementSpeed * Time.deltaTime;
        }
        // Update the player's position with the new position.
        transform.position = newPosition;
	}
}

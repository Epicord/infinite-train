// Author(s): Paul Calande, Moshe Katzin-Nystrom
// A script that handles player movement.

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // How fast the player moves. Higher = faster.
    private const float movementSpeed = 5.0f;

    // A Rigidbody is a component that helps to handle game physics. This is what we apply movement
    // to, as opposed to the player model itself, so that there is rudimentary collision support.
    private Rigidbody body;
    
	// Use this for initialization
	void Start () {
        // Once everything loads, we can get the Rigidbody.
        body = GetComponent<Rigidbody>();
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
        body.MovePosition(newPosition);

        // HACK: We don't want the player rotating (at least for now), which happens sometimes because of physics calculations.
        // In Unity, rotations are represented by Quaternions, which shouldn't be modified directly, but can be constructed from a vector.
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
	}
}

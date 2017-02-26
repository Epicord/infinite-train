// Author(s): Paul Calande, Moshe Katzin-Nystrom, Micah Nichols
// A script that handles player movement.

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // How fast the player moves. Higher = faster.
    private const float movementSpeed = 7.2f;

    private float xmove = 0f; // Variable to store the movement along the X axis.
    private float zmove = 0f; // Variable to store the movement along the Z axis.
    private float slow = 1f;   // This number divides the overall movement by a value. This is used for diagonal movement.

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
        slow = 1f;  // Reset variables
        xmove = 0f;
        zmove = 0f;
        // Let's check the player's keyboard inputs.
        // If the player presses left...
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Move left.
            // Time.deltaTime refers to the amount of time that has passed since the last frame.
            // Using this in object movement allows our game to run independently from framerate.
            xmove -= movementSpeed * Time.deltaTime;
        }
        // If the player presses right...
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Move right.
            xmove += movementSpeed * Time.deltaTime;
        }
        // If the player presses up...
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Move up.
            zmove += movementSpeed * Time.deltaTime;
        }
        // If the player presses down...
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // Move down.
            zmove -= movementSpeed * Time.deltaTime;
        }
        if (xmove != 0f && zmove != 0f) // Are we moving diagonally?
        {
            // If so, let's slow down the movement.
            slow = 1.414f; // Approximately sqrt(2), because math
        }
        newPosition.x += xmove / slow;   // set the x movement
        newPosition.z += zmove / slow;   // and also set the z movement
        body.MovePosition(newPosition); // and MOVE!

        // HACK: We don't want the player rotating (at least for now), which happens sometimes because of physics calculations.
        // In Unity, rotations are represented by Quaternions, which shouldn't be modified directly, but can be constructed from a vector.
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

        // HACK: i am a professional hacker
        // it's time to kill NASA
	}
}

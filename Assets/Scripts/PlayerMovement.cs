// Author(s): Paul Calande, Moshe Katzin-Nystrom, Micah Nichols
// A script that handles player movement.

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private const float movementSpeed = 7.2f; // How fast the player moves. Higher = faster.
    public Vector3 move = Vector3.zero; // Stores the amount of movement on each frame.
    // A Rigidbody is a component that helps to handle game physics. This is what we apply movement
    // to, as opposed to the player model itself, so that there is rudimentary collision support.
    private Rigidbody body;
    
	void Start ()
    {
        // Once everything loads, we can get the Rigidbody.
        body = GetComponent<Rigidbody>();
        // Lock the cursor so it can't escape our grasp. Camera controls.
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Initialize a new vector for storing the updated position.
        Vector3 newPosition = transform.position;
        // Initalize a force vector.
        Vector3 applyForce = Vector3.zero;
        move = Vector3.zero;

        // If the player presses left...
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            // Move left.
            // Time.deltaTime refers to the amount of time that has passed since the last frame.
            // Using this in object movement allows our game to run independently from framerate.
            move.x -= movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            // Move right.
            move.x += movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            move.z += movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            move.z -= movementSpeed * Time.deltaTime;

        if (move.x != 0 && move.z != 0) move /= 1.414f; // make sure the player moves the same speed diagonally as they do orthogonally

        applyForce.y = move.y; // update applyForce
        newPosition += transform.rotation * move; // set the movement, accounting for whichever direction the player is facing
        body.MovePosition(newPosition);
        body.AddForce(applyForce); // apply the force to the body

        // horizontal mouselook rotation
        transform.Rotate(new Vector3(0f, Input.GetAxisRaw("Mouse X"), 0f));
    }
}

// Author(s): Paul Calande, Moshe Katzin-Nystrom, Micah Nichols
// A script that handles player movement.

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // How fast the player moves. Higher = faster.
    private const float movementSpeed = 7.2f;

    private float xmove = 0f; // Variable to store the movement along the X axis.
    private float ymove = 0f; // Variable to store the movement along the Y axis.
    private float zmove = 0f; // Variable to store the movement along the Z axis.
    private float slow = 1f;  // This number divides the overall movement by a value. This is used for diagonal movement.
    private bool canJump = true; // Boolean to check if the player can jump or nah
    private bool onGround = true; // Boolean to check if the player is on the ground

    private float testpos = 0f;

    public float jump; // Jump and gravity values can be modified in the unity editor in the Player object
    public float gravity;

    // A Rigidbody is a component that helps to handle game physics. This is what we apply movement
    // to, as opposed to the player model itself, so that there is rudimentary collision support.
    private Rigidbody body;
    
	// Use this for initialization
	void Start () {
        // Once everything loads, we can get the Rigidbody.
        body = GetComponent<Rigidbody>();
        // A delicious testing variable for seeing if we can jump later.
        testpos = body.position.y;
}
	
	// Update is called once per frame
	void Update ()
    {
        // Initialize a new vector for storing the updated position.
        Vector3 newPosition = transform.position;
        // Initalize a force vector.
        Vector3 applyForce = Vector3.zero;

        // Let's test if we're on the ground.
        if (body.position.y == testpos) // even after moving, we're still at the same y spot??
        {
            onGround = true; // that means we are on the ground
            canJump = true; // and therefore can JUMP!!!
        }
        else // or maybe not.
        {
            onGround = false; // oh.
            canJump = false; // that sucks.
        }

        // Testing position update, to see if we can jump later.
        testpos = body.position.y;
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
        // If the player presses space...
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            ymove = jump;
            canJump = false;
            onGround = false;
        }
        if (xmove != 0f && zmove != 0f) // Are we moving diagonally?
        {
            // If so, let's slow down the movement.
            slow = 1.414f; // Approximately sqrt(2), because math
        }
        applyForce.y = ymove;           // update applyForce
        newPosition.x += xmove / slow;  // set the x movement
        newPosition.z += zmove / slow;  // and also set the z movement
        body.MovePosition(newPosition); // and MOVE!
        body.AddForce(applyForce); // apply the force to the body
        
        if (body.position.y < 0.5) // player must be on baseplate
        {
            Vector3 groundpos = body.position; // get the player position
            groundpos.y = 0.5f; // make the y value on the ground
            body.MovePosition(groundpos); // force him there
            // i know there's a better way of doing this
            // so for the love of god
            // fix it
        }

        // HACK: We don't want the player rotating (at least for now), which happens sometimes because of physics calculations.
        // In Unity, rotations are represented by Quaternions, which shouldn't be modified directly, but can be constructed from a vector.
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

        // Gravity Falls Fanboys --v
        if (onGround == false)
        {
            ymove -= gravity;
            if (ymove < -100)
            {
                ymove = -100; // terminal velocity
            }
        }
        else
        {
            ymove = 0; //stop falling jfc
        }

        // HACK: i am a professional hacker
        // it's time to kill NASA
	}
}

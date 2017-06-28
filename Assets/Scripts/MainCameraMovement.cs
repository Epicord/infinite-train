// Author(s): Moshe Katzin-Nystrom
// A script that controls the camera location.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{

    private string cameraState = "followPlayer";
    private GameObject player;
    private Vector3 defaultTransformOffset = new Vector3(0f, 4.25f, -4.5f); // behind and above the player
    private Quaternion defaultRotationOffset = Quaternion.Euler(31f, 0f, 0f); // looking down towards the player

    void Start ()
    {
        player = GameObject.Find("Player");
        transform.position = player.transform.position + defaultTransformOffset;
        transform.rotation = defaultRotationOffset;
	}
	
	void Update ()
    {
        if (cameraState == "followPlayer")
        {
            // place the camera behind and above wherever the player is, accounting for the player's rotation
            transform.position = player.transform.position + player.transform.rotation * defaultTransformOffset;
            // face the camera the same direction the player is facing
            transform.rotation = player.transform.rotation * defaultRotationOffset;
        }
    }
}

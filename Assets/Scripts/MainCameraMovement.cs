// Author(s): Moshe Katzin-Nystrom
// A script that controls the camera location.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{

    private string cameraState = "followPlayer";
    private GameObject player;
    private Vector3 TransformOffset = new Vector3(0f, 4.25f, -4.5f); // camera moved behind and above the player
    private Quaternion RotationOffset = Quaternion.Euler(31f, 0f, 0f); // camera rotated down towards the player
    private Quaternion lookAngle = Quaternion.Euler(Vector3.zero); // camera rotation as controlled by the player
    private float lookAngleOffset = 0f; // a useful number derived from lookAngle

    void Start ()
    {
        player = GameObject.Find("Player");
        transform.position = player.transform.position + player.transform.rotation * TransformOffset;
        transform.rotation = player.transform.rotation * RotationOffset;
    }
	
	void Update ()
    {
        if (cameraState == "followPlayer")
        {
            // get camera rotation from mouse movement
            lookAngle *= Quaternion.Euler(-Input.GetAxisRaw("Mouse Y"), 0f, 0f);
            // do some math to get a useful number: linear from 0 to 1, looking at the horizon = 0, looking downwards = 1
            lookAngleOffset = ((lookAngle.eulerAngles.x + 121f) % 360 - 90) / 90;
            // keep the camera behind the player, but get closer when looking downwards
            TransformOffset.z = -5.75f + 5.75f * lookAngleOffset;
            // if looking forwards/downwardsish, keep the camera at roughly the same vertical height, going a bit higher when looking downwards
            if (lookAngleOffset >= 0) TransformOffset.y = 2.25f + 2 * lookAngleOffset;
            // but get real low to the ground when looking upwards
            else TransformOffset.y = 2.25f + 7 * lookAngleOffset;            
            
            // now actually move the camera where we want it to be
            transform.position = player.transform.position + player.transform.rotation * TransformOffset;
            transform.rotation = player.transform.rotation * RotationOffset * lookAngle;
        }
    }
}

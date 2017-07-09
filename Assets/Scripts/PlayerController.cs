using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private Vector3 playerPosition = Vector3.zero;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveForward = false;
    private bool moveBackward = false;

	private void Start ()
	{
		
	}
	
	private void Update ()
	{
        moveRight = Input.GetAxisRaw("Horizontal") > 0 ? moveRight = true : moveRight = false;
        moveLeft = Input.GetAxisRaw("Horizontal") < 0 ? moveLeft = true : moveLeft = false;
        moveForward = Input.GetAxisRaw("Vertical") > 0 ? moveForward = true : moveForward = false;
        moveBackward = Input.GetAxisRaw("Vertical") < 0 ? moveBackward = true : moveBackward = false;
    }

    private void FixedUpdate()
    {
        if (moveRight == true)
        {
            playerPosition.x += moveSpeed * Time.fixedDeltaTime;
        }

        if (moveLeft == true)
        {
            playerPosition.x -= moveSpeed * Time.fixedDeltaTime;
        }

        if (moveForward == true)
        {
            playerPosition.z += moveSpeed * Time.fixedDeltaTime;
        }

        if (moveBackward == true)
        {
            playerPosition.z -= moveSpeed * Time.fixedDeltaTime;
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        transform.position = playerPosition;
    }

}

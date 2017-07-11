using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public delegate void Shoot(float damage, float fireRate, float hitForce);
    public static event Shoot onPlayerShoot;

    private float moveSpeed = 10f;
    private Vector3 xMovement = Vector3.zero;
    private Vector3 yMovement = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private Vector3 mouseRotation = Vector3.zero;
    private Vector3 currentCameraRotation = Vector3.zero;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float xSensitivity = 3.5f;
    private float ySensitivity = 3.5f;
    private float cameraRotationLimit = 90f;
    private bool isCameraInverted = false;

    private float jumpForce = 5f;
    private bool isJumping = false;

    private Rigidbody myRigidbody;
    private Camera fpsCamera;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        CheckFireInput();
        CheckMovementInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        HandleMouseRotation();
    }

    private void CheckFireInput()
    {
        if (Input.GetButton("Fire1"))
        {
            if (onPlayerShoot != null)
            {
                onPlayerShoot(10f, 10f, 10f);
            }
        }
    }

    private void GetReferences()
    {
        myRigidbody = GetComponent<Rigidbody>();
        fpsCamera = GetComponentInChildren<Camera>();
    }

    private void CheckMovementInput()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * transform.right;
        yMovement = Input.GetAxisRaw("Vertical") * transform.forward;

        velocity = (xMovement + yMovement) * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (Physics.Raycast(transform.position, Vector3.down, 5f))
        {
            isJumping = false;
        }
    }

    private void MovePlayer()
    {
        if (velocity != Vector3.zero)
        {
            myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
        }

        if (isJumping)
        {
            myRigidbody.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            isJumping = false;
        }
    }

    private void HandleMouseRotation()
    {
        yRotation = Input.GetAxisRaw("Mouse X");
        mouseRotation = new Vector3(0f, yRotation, 0f) * ySensitivity;

        xRotation = Input.GetAxisRaw("Mouse Y");
        currentCameraRotation.x += xRotation * xSensitivity;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, -cameraRotationLimit, cameraRotationLimit);

        if (mouseRotation != Vector3.zero)
        {
            myRigidbody.MoveRotation(myRigidbody.rotation * Quaternion.Euler(mouseRotation));
        }

        if (fpsCamera != null)
        {
            

            if (isCameraInverted == false)
            {
                fpsCamera.transform.localEulerAngles = new Vector3(-currentCameraRotation.x, 0f, 0f);
            }
            else
            {
                fpsCamera.transform.localEulerAngles = new Vector3(currentCameraRotation.x, 0f, 0f);
            }
        }
        else
        {
            Debug.LogError("PlayerController script is missing an FPS camera, make sure it has one!");
        }
    }

}

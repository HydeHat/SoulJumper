using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //public variables
    public float moveSpeed = 10;
    public float rotationSpeed = 10;
    public bool onGround = false;
    //private variables
    [SerializeField]private Rigidbody playerRigidBody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Camera mainCamera;
    private Entity _entity;



    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        _entity = GetComponent<Entity>();
        mainCamera = Camera.main;


    }


    private void Update()
    {
        if (_entity.isPossesed)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            //basic Movement
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveVelocity = moveInput * moveSpeed;

            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLength;
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLookAt = cameraRay.GetPoint(rayLength);
                // Debug.DrawLine(mainCamera.transform.position, pointToLookAt);
                transform.LookAt(new Vector3(pointToLookAt.x, transform.position.y, pointToLookAt.z));
            }

            Ray floorColllisionCheck = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
            RaycastHit floorHit;
            if (Physics.Raycast(floorColllisionCheck, out floorHit, 1.2f))
            {

                if (floorHit.collider.CompareTag("Ground"))
                {
                    onGround = true;

                }
            }
            else
            {
                onGround = false;
            }

        }
 
    }

    private void FixedUpdate()
    {
        IsOnGround();
        playerRigidBody.velocity = (moveVelocity * Time.deltaTime);

    }

    private void IsOnGround()
    {
        if (!onGround)
        {
            Vector3 grav = new Vector3(0f, -9.8f, 0f);
            moveVelocity = playerRigidBody.velocity + grav;
        }
    }

    
}

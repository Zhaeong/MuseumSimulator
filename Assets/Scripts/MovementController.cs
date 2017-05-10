using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private Camera mainCamera;

    public float MovementSpeed;
    public float HorizRotationSpeed, VerticalRotationSpeed;
    private float yaw, pitch;


    private Vector3 moveDirection = Vector3.zero;

    private CharacterController CC;
    // Use this for initialization
    void Start () {
        CC = gameObject.GetComponent<CharacterController>();
        mainCamera = Camera.main;
        yaw = 0.0f;
        pitch = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        yaw += HorizRotationSpeed * Input.GetAxis("Mouse X");
        pitch -= VerticalRotationSpeed * Input.GetAxis("Mouse Y");

        //mainCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);            

        Vector3 MoveDirectionForward = mainCamera.transform.forward;        

        Vector3 ForwardNormalized = new Vector3(MoveDirectionForward.x, 0, MoveDirectionForward.z).normalized;
        Vector3 MoveRight = Vector3.Cross(ForwardNormalized, Vector3.down);
        Vector3 LeftNormalized = MoveRight.normalized;
        Vector3 BothNormalized = (ForwardNormalized + LeftNormalized).normalized;

        CC.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        Debug.DrawRay(transform.position, MoveDirectionForward.normalized, Color.blue);
        Debug.DrawRay(transform.position, ForwardNormalized, Color.red);
        Debug.DrawRay(transform.position, LeftNormalized, Color.red);
        Debug.DrawRay(transform.position, (ForwardNormalized + LeftNormalized).normalized, Color.green);

        Vector3 Movement = Vector3.zero;

        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0)
        {
            Movement = LeftNormalized * Input.GetAxis("Horizontal");
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") != 0)
        {
            Movement = ForwardNormalized * Input.GetAxis("Vertical");
        }
        else
        {
            Movement = new Vector3(BothNormalized.x * Input.GetAxis("Horizontal"), BothNormalized.y, BothNormalized.z * Input.GetAxis("Vertical"));
        }  

        Debug.DrawRay(transform.position, Movement, Color.black);

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));       


        CC.Move(Movement * MovementSpeed *  Time.deltaTime);
    }
}

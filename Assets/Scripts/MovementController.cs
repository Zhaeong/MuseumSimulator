using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private Camera mainCamera;
    public float smoothSpeed;
    public float MovementSpeed;
    public float HorizRotationSpeed, VerticalRotationSpeed;

    public float gravity;
    private float yaw, pitch;


    private Vector3 moveDirection = Vector3.zero;

    private Vector3 velocity = Vector3.zero;

    private float slerpVal;

    private bool isMoving;
    private Vector3 Movement = Vector3.zero;

    private CharacterController CC;

    // Use this for initialization
    void Start () {
        CC = gameObject.GetComponent<CharacterController>();
        mainCamera = Camera.main;
        yaw = 0.0f;
        pitch = 0.0f;

        slerpVal = 0;
    }
	
	// Update is called once per frame
	void Update () {
        yaw += HorizRotationSpeed * Input.GetAxis("Mouse X");
        pitch -= VerticalRotationSpeed * Input.GetAxis("Mouse Y");

        //mainCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);            

        //The movement is based on the way that the player is facing
        Vector3 MoveDirectionForward = mainCamera.transform.forward;        

        Vector3 ForwardNormalized = new Vector3(MoveDirectionForward.x, 0, MoveDirectionForward.z).normalized;
        Vector3 MoveRight = Vector3.Cross(ForwardNormalized, Vector3.down);
        Vector3 LeftNormalized = MoveRight.normalized;
        Vector3 BothNormalized = (ForwardNormalized + LeftNormalized).normalized;        

        //Debug.DrawRay(transform.position, MoveDirectionForward.normalized, Color.blue);
        //Debug.DrawRay(transform.position, ForwardNormalized, Color.blue);
        //Debug.DrawRay(transform.position, LeftNormalized, Color.red);
        //Debug.DrawRay(transform.position, (ForwardNormalized + LeftNormalized).normalized, Color.green);

        //
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;

            if (slerpVal < 1)
            {
                slerpVal += Time.deltaTime * smoothSpeed;
            }            

            //The if statements specify the direction that it will move in
            //Move Forward
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0)
            {
                Movement = LeftNormalized;
            }
            //Move Backward
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0)
            {
                Movement = -LeftNormalized;
            }
            //Move Left
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0)
            {
                Movement = ForwardNormalized;
            }
            //Move Right
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0)
            {
                Movement = -ForwardNormalized;
            }
            //Move Forward Left
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
            {
                Movement = BothNormalized;
            }
            //Move Forward Right
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
            {
                Movement = -BothNormalized;
            }
            //Move Backward Left
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
            {
                Vector3 newMovement = Vector3.Reflect(BothNormalized, ForwardNormalized);
                Movement = newMovement;
            }
            //Move Backward Right
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
            {
                Vector3 newMovement = Vector3.Reflect(BothNormalized, LeftNormalized);
                Movement = newMovement;
            }
        }        
        else
        {
            Movement = Vector3.zero;
            slerpVal = 0;
            if (slerpVal > 0)
            {
                slerpVal -= Time.deltaTime * smoothSpeed;
            }
            
        }     

        //Vector3 DirectionToMove = transform.position + Movement;

        //Debug.DrawRay(transform.position, new Vector3(BothNormalized.x , BothNormalized.y, BothNormalized.z), Color.yellow);
        //Debug.DrawRay(transform.position, new Vector3(-BothNormalized.x, -BothNormalized.y, -BothNormalized.z), Color.green);
        //Debug.DrawRay(transform.position, Vector3.Reflect(BothNormalized, ForwardNormalized), Color.gray);
        //Debug.DrawRay(transform.position, Vector3.Reflect(BothNormalized, LeftNormalized), Color.magenta);

        Debug.DrawRay(transform.position, Movement, Color.black);

        //transform.position = Vector3.SmoothDamp(transform.position, DirectionToMove, ref velocity, smoothSpeed);

        
        Movement.y = -gravity;

        CC.Move(Vector3.Slerp(new Vector3(0,-gravity, 0), Movement, slerpVal) * MovementSpeed * Time.deltaTime);
        
    }
}

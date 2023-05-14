using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{

    public float speed = 10.0f;

    // Sensitivity of the mouse
    public float mouseSensitivity = 100.0f;

    // Vertical angle limit of the camera
    public float angleLimit = 20f;

    public float tiltAngle = 10f;

    public bool afloat;
    // Rigidbody component of the character
    private Rigidbody rb;

    // Camera attached to the character
    private Camera camera;
    private Transform submarineModel;

    public float gravity = 9.81f;

    public ParticleSystem bubbleRight;
    public ParticleSystem bubbleLeft;
    public ParticleSystem bubbleCentre;

    public GameObject blades;
    [SerializeField] private float bladeSpeed = 1000f;

    void Start()
    {
        // Get the Rigidbody and Camera components
        rb = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;


        submarineModel = transform.GetChild(0).transform;
    }

    
    void Update()
    {

        //rb.AddForce(Vector3.down * gravity * rb.mass, ForceMode.Acceleration);

        blades.transform.Rotate(new Vector3(0, 0, Time.deltaTime * bladeSpeed), Space.Self);
        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        
        // Calculate the movement vector
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);


        // Rotate the movement vector towards the direction of the camera
        movement = camera.transform.rotation * movement;

        // Apply the movement to the Rigidbody
        rb.velocity = movement * speed;

        // Get input for camera rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the character and camera
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity * Time.deltaTime);
       
        transform.Rotate(-Vector3.right * mouseY * mouseSensitivity * Time.deltaTime);

        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);


        // Clamp the camera's vertical angle
        float angle = transform.localEulerAngles.x;
        angle = (angle + 180) % 360;
        angle = Mathf.Clamp(angle - 180, -angleLimit, angleLimit);
        transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);

        //moving right
        if (horizontalInput > 0)
        {
            //tilt right
            submarineModel.transform.localRotation = Quaternion.Euler(0, 0, -tiltAngle);
            bubbleCentre.gameObject.SetActive(false);
            bubbleLeft.gameObject.SetActive(true);
            bubbleRight.gameObject.SetActive(false);
            bladeSpeed = 1000;
        }
        //moving left
        if (horizontalInput < 0)
        {
            //tilt left
            submarineModel.transform.localRotation = Quaternion.Euler(0, 0, tiltAngle);
            bubbleCentre.gameObject.SetActive(false);
            bubbleLeft.gameObject.SetActive(false);
            bubbleRight.gameObject.SetActive(true);
            bladeSpeed = -1000;
        }
       
        if (horizontalInput == 0)
        {
            
            submarineModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
            bubbleCentre.gameObject.SetActive(false);
            bubbleLeft.gameObject.SetActive(false);
            bubbleRight.gameObject.SetActive(false);
            bladeSpeed = 0;
        }

        if (verticalInput != 0)
        {
            bubbleCentre.gameObject.SetActive(true);
        }
        if (verticalInput > 0)
        {
            bladeSpeed =  5000;
        }
        if(verticalInput < 0)
        {
            bladeSpeed = - 5000;
        }
    }
}

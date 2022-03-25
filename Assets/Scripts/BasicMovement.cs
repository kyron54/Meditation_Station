using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [Tooltip("The speed at which the camera rotates.")]
    public float camSpeed = 2f;
    [Tooltip("The speed at which the player moves.")]
    public float moveSpeed = 5f;

    // The horizontal angle of the camera.
    private float yaw = 0f;
    // The vertical angle of the camera.
    private float pitch = 0f;
    // The variable that controls Left and right movement speed.
    private float moveX;
    // The variable that controls Back and Forth movement speed
    private float moveZ;

    private bool isWalking = false;

    AudioSource normalStep;

    // Start is called before the first frame update
    void Start()
    {
        normalStep = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //OVRInput.Update();
        ControllerRotateCamera();
        MouseRotateCamera();
        MovePlayer();

    }

    private void FixedUpdate()
    {
        //OVRInput.FixedUpdate();
    }

    //controls for camera using mouse/Joystick
    void MouseRotateCamera()
    {
        yaw += camSpeed * Input.GetAxis("Mouse X");
        pitch -= camSpeed * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void ControllerRotateCamera()
    {
        yaw += camSpeed * Input.GetAxis("Horizontal 3rd");
        pitch -= camSpeed * Input.GetAxis("Vertical 3rd");

        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    /*void OculusControl()
    {
        //yaw += camSpeed * Input.GetAxis("Horizontal 3rd");
        // pitch -= camSpeed * Input.GetAxis("Vertical 3rd");

        OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }*/

    void MovePlayer()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        /*if(moveX > 0 || moveZ > 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }*/

        Vector3 camF = transform.forward;
        Vector3 camR = transform.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        //transform.position += new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;
        transform.position += (camF * moveZ + camR * moveX) * moveSpeed * Time.deltaTime;
    }
}

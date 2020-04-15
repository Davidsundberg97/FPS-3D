
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    private float speed;
    public float crouchspeed = 5f;
    public float walkspeed = 10f;
    public float sprintspeed = 20f;

    private float targetY = 1.65f;
    private float smoothTime = 0.3f;
    private float velocityY = 0;


    private float movespeed;
    

    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpheight = 3f;
    public Camera MainCam;

    public Transform groundcheck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;
    bool crouching = false;

    bool isgrounded;




    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);

        if (isgrounded && velocity.y < 0)
        {
            
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        //Sprint
        if (Input.GetKey(KeyCode.LeftShift) && crouching == false)
        {
            movespeed = sprintspeed;
        }
        else
        {
            movespeed = walkspeed;
        }
        
        //Crouch
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("hello");
            if (crouching)
            {
                crouching = false;
                targetY = 1.65f;
                
            }
            else
            {
                crouching = true;
                targetY = -0.8f;

            }
        }
        if (crouching)
        {
            movespeed = crouchspeed;
        }
        float newY = Mathf.SmoothDamp(Camera.main.transform.localPosition.y, targetY, ref velocityY, smoothTime);
        Camera.main.transform.localPosition = new Vector3(0, newY, 0);


        



        controller.Move(move * movespeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isgrounded)
        {
          velocity.y =  Mathf.Sqrt(jumpheight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

       

        controller.Move(velocity * Time.deltaTime);

    }

}

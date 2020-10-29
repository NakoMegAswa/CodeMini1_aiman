using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class PlayerController : MonoBehaviour
{
    float speed = 10.0f;
    float xLimit = 10.0f;
    float zLimit = 10.0f;

    float planeIndicator = 0;
    float planeALimits = 10.0f;
    float planeBLimits = 5.0f;



    public SphereCollider col;
    public LayerMask groundlayers;
    public bool sphereIsOnTheGround = true;

    private int currentJump = 0;
    private const int MAX_JUMP = 1;




    int spaceTrack = 0;

    float initYPos = 5;



    Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        float initYPos = 1000;

        playerRb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();


        Debug.Log(initYPos);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);



        if (planeIndicator == 0)
        {
            zLimit = planeALimits;
            xLimit = planeALimits;

            if (transform.position.z > zLimit)
            {
                if (transform.position.x > -xLimit / 2 && transform.position.x < xLimit / 2)
                {
                    transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
                }
            }
            else if (transform.position.z < -zLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            }
            else
            {
                transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
            }

            if (transform.position.x > xLimit)
            {
                transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -xLimit)
            {
                transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            }
        }
        else
        {
            zLimit = planeALimits + 2 * planeBLimits;
            xLimit = planeBLimits;

            if (transform.position.z < planeALimits)
            {
                if (transform.position.x > -planeALimits / 2 && transform.position.x < planeALimits / 2)
                {
                    transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, planeALimits);
                }
            }
            else if (transform.position.z > zLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            }
            else
            {
                transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
            }

            if (transform.position.x > xLimit)
            {
                transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -xLimit)
            {
                transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && (sphereIsOnTheGround || MAX_JUMP > currentJump))
        {
            playerRb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            sphereIsOnTheGround = false;
            currentJump++;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA"))
        {
            Debug.Log("In Plane A");
            planeIndicator = 0;
            sphereIsOnTheGround = true;
            currentJump = 0;
        }

        if (collision.gameObject.CompareTag("PlaneB"))
        {
            Debug.Log("In Plane B");
            planeIndicator = 1;
            sphereIsOnTheGround = true;
            currentJump = 0;
        }


    }







}
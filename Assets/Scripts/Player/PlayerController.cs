using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator playerAnimator;
    private Vector3 playerDirection;
    public LayerMask groundLayer;
    private Transform groundCheck;
    private bool isGrounded;
    private bool isSliding = false;
    public float jumpForce;
    public float gravityForce;
    public static float forwardSpeed;
    public static float maxSpeed = 30;
    public float laneDistance;
    private float laneChangeSpeed = 25;
    private int desiredLane = 1;//0:left 1:middle 2:right

    // Start is called before the first frame update
    void Start()
    {
        playerDirection = Vector3.zero;
        forwardSpeed = 8;
        groundCheck = GameObject.Find("GroundCheck").transform;
        playerAnimator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelUIManager.isGameStarted || LevelUIManager.isGameOver)
        {
            return;
        }
        //Increase speed
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.5f * Time.deltaTime;
        }

        SetAnimations();
        playerDirection.z = forwardSpeed;
        isGrounded = Physics.CheckSphere(groundCheck.position, characterController.radius, groundLayer);
        if (isGrounded)
        {
            if (SwipeManager.swipeUp && !isSliding)
            {
                StartCoroutine(Jump());
            }
        }
        else
        {
            playerDirection.y += gravityForce * Time.deltaTime;

        }
        if (SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }
        //gather the inputs on which lane we should be
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;

            }


        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }


        }

        //calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * laneChangeSpeed * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            {
                characterController.Move(moveDir);

            }
            else
            {
                characterController.Move(diff);
            }
        }
        //Move Player
        characterController.Move(playerDirection * Time.deltaTime);

    }

    private void SetAnimations()
    {
        playerAnimator.SetBool("isGameStarted", true);
        playerAnimator.SetBool("isGrounded", isGrounded);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle" || hit.transform.tag == "Destroyable")
        {
            LevelUIManager.isGameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");

        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        playerAnimator.SetBool(nameof(isSliding), true);
        characterController.center = new Vector3(0, -0.5f, 0);
        characterController.height = 0.3f;
        yield return new WaitForSeconds(0.5f);
        characterController.center = new Vector3(0, 0, 0);
        characterController.height = 2;
        playerAnimator.SetBool(nameof(isSliding), false);
        isSliding = false;
    }
    private IEnumerator Jump()
    {
        playerDirection.y = jumpForce;
        characterController.center = new Vector3(0, 0.5f, 0);
        characterController.height = 1f;
        yield return new WaitForSeconds(0.5f);
        characterController.center = new Vector3(0, 0, 0);
        characterController.height = 2;
    }

}

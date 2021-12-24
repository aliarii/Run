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
    private int desiredLane = 1;//0:left 1:middle 2:right

    void Start()
    {
        playerDirection = Vector3.zero;
        forwardSpeed = 8;
        groundCheck = GameObject.Find("GroundCheck").transform;
        playerAnimator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }
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
                return;
            }
            characterController.Move(Vector3.right * laneDistance);
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
                return;
            }
            characterController.Move(Vector3.left * laneDistance);
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
        if (hit.transform.tag == "Obstacle")
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

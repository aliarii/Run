using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CapsuleCollider characterCollider;
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

    void Start()
    {
        playerDirection = Vector3.zero;
        forwardSpeed = 8f;
        groundCheck = GameObject.Find("GroundCheck").transform;
        playerAnimator = GetComponentInChildren<Animator>();
        characterCollider = GetComponent<CapsuleCollider>();
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
        isGrounded = Physics.CheckSphere(groundCheck.position, characterCollider.radius, groundLayer);
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
            if (playerDirection.y < 0)
            {
                playerDirection.y = 0;
            }
        }
        if (SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }
        //gather the inputs on which lane we should be
        if (SwipeManager.swipeRight)
        {
            if (transform.position.x < 2)
            {
                transform.position += Vector3.right * laneDistance;
            }

        }
        if (SwipeManager.swipeLeft)
        {
            if (transform.position.x > -1)
            {
                transform.position += Vector3.left * laneDistance;
            }

        }
        if (transform.position.x > 2.5f || transform.position.x < -2.5f)
        {
            LevelUIManager.isGameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
        //Move Player
        transform.Translate(playerDirection * Time.deltaTime);
    }
    private void SetAnimations()
    {
        playerAnimator.SetBool("isGameStarted", true);
        playerAnimator.SetBool("isGrounded", isGrounded);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Obstacle")
        {
            LevelUIManager.isGameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        playerAnimator.SetBool(nameof(isSliding), true);
        characterCollider.center = new Vector3(0, -0.5f, 0);
        characterCollider.height = 0.3f;
        yield return new WaitForSeconds(0.5f);
        characterCollider.center = new Vector3(0, 0, 0);
        characterCollider.height = 2;
        playerAnimator.SetBool(nameof(isSliding), false);
        isSliding = false;
    }
    private IEnumerator Jump()
    {
        playerDirection.y = jumpForce;
        characterCollider.center = new Vector3(0, 0.5f, 0);
        characterCollider.height = 1f;
        yield return new WaitForSeconds(0.5f);
        characterCollider.center = new Vector3(0, 0, 0);
        characterCollider.height = 2;
    }

}

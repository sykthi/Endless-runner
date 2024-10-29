using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController player;

    Animator playerAnimation;

    Vector3 direction;

    public float moveSpeed = 11f;
    public float maxMoveSpeed = 25f;
    public float gravity = -20f;
    public float jumpvelocity = 9f;

    public static bool isJumping;
    private bool isRolling = false;

    int currentLane =1 ; //0->left, 1->middle, 2->right
    [SerializeField] float laneDistance = 3.5f;

    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimation = GetComponent<Animator>();

    }

    void Update()
    {
        ///////////////RunNimation///////////////////
        if (Events.isPlayClicked == true)
        {
            playerAnimation.SetBool("isMoving", true);
        }
        /////////////////////////////////////////////

        if (!Events.isPlayClicked) 
            return;

        // Speed Increase
        if(moveSpeed < maxMoveSpeed )
        {
            moveSpeed += 0.1f * Time.deltaTime;
        }

        // Constant movement in Z axis
        direction.z = moveSpeed;
        

        // getting Input
        ///JUMP-INPUT///
        if (Input.GetKeyDown(KeyCode.W) && player.isGrounded)
        {
            direction.y = -1;
            jump();
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        ///SLIDE-INPUT///
        if (Input.GetKeyDown(KeyCode.S) && !isRolling)
        {
            playerAnimation.SetTrigger("_Roll");
            StartCoroutine(roll());
        }
        ///LEFT-INPUT///
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentLane++;
            if(currentLane > 2)
            {
                currentLane = 2;
            }
        }
        ///LEFT-INPUT///
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentLane--;
            if (currentLane < 0)
            {
                currentLane = 0;
            }
        }

        // Calculating_TargetPosition
        Vector3 targetposition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (currentLane == 0)
        {
            targetposition += Vector3.left * laneDistance;
        }
        else if (currentLane == 2)
        {
            targetposition += Vector3.right * laneDistance;
        }

        if (transform.position == targetposition) 
            return;
        Vector3 diff = targetposition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            player.Move(moveDir);
        else
            player.Move(diff);
    }

    private void FixedUpdate()
    {
        if (!Events.isPlayClicked)
            return;

        player.Move(direction * Time.fixedDeltaTime);

    }

    //jump
    void jump()
    {
        direction.y = jumpvelocity;
        playerAnimation.SetTrigger("_Jump");
    }

    //slide
    private IEnumerator roll()
    {
        isRolling = true;
        Vector3 originalControllerCenter = player.center;
        Vector3 newControllerCenter = originalControllerCenter;
        player.height /= 2f;
        newControllerCenter.y -= player.height / 2;
        player.center = newControllerCenter;

        yield return new WaitForSeconds(1f);

        player.height *= 2f;
        player.center = originalControllerCenter;
        isRolling = false;
    }

    //Obstacle-collision
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Opps")
        {
            GameManager.gameOver = true;
            FindObjectOfType<AudioManager>().playSound("gameover");
        }
    } 
}

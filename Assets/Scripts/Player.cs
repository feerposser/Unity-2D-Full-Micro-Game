using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private Animator animator;
    private bool isJumping = false, doubleJump = false;
    private float yLastPosition;

    public float speed = 5, jumpingForce = 1;
    public int health = 3;

    public GameObject bowPrefab;
    public Transform bownPosition;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        yLastPosition = transform.position.y;
        GameController.instance.SetNumberHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Fall();
        Bow();
        yLastPosition = transform.position.y;
    }

    private void Move()
    {
        float movement = Input.GetAxis("Horizontal");

        rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);

        MovingAnimation(movement);

    }

    private void MovingAnimation(float movement)
    {
        if (movement != 0)
        {
            animator.SetBool("isRunning", true);
            MovingRotate(movement);
            return;
        }
        
        animator.SetBool("isRunning", false);
    }

    private void MovingRotate(float movement)
    {
        if (movement > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                JumpingMovement();
                doubleJump = true;
            } else if(doubleJump)
            {
                JumpingMovement();
                doubleJump = false;
            } 
        }
    }

    private void JumpingMovement()
    {
        rigidBody.AddForce(new Vector2(0, jumpingForce), ForceMode2D.Impulse);
        isJumping = true;
        animator.SetBool("isJumping", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if(collision.gameObject.layer == 3)
        {
        
        }*/
        isJumping = false;
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);

    }

    private void Fall()
    {
        if ((yLastPosition - transform.position.y) * Time.deltaTime > 0)
        {
            animator.SetBool("isFalling", true);
        } else
        {
            animator.SetBool("isFalling", false);
        }
    }

    private void Bow()
    {
        StartCoroutine("Bowing");
    }

    IEnumerator Bowing()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("isBowing", true);
            GameObject BowObject = Instantiate(bowPrefab, bownPosition.position, bownPosition.rotation);

            if (transform.rotation.y == 0)
            {
                BowObject.GetComponent<Bow>().isBowRight = true;
            } else if (transform.rotation.y == -180)
            {
                BowObject.GetComponent<Bow>().isBowRight = false;
            }

            yield return new WaitForSeconds(0.2f);
            animator.SetBool("isBowing", false);
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        GameController.instance.SetNumberHealth(health);
        animator.SetTrigger("hit");

        if (transform.rotation.y == 0)
        {
            transform.position += new Vector3(-1, 0, 0);
        } else
        {
            transform.position += new Vector3(1, 0, 0);
        }

        if (health <= 0)
        {
            GameController.instance.GameOver();
        }
    }

    public void IncreaseHealth(int value)
    {
        health += value;
        GameController.instance.SetNumberHealth(health);
    }
}

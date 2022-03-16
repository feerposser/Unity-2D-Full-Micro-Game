using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolGuy : MonoBehaviour
{
    public float speed;
    public float walkTime;
    public int health;
    public int damage = 1;

    private float timer;
    private bool walkRight;

    private Rigidbody2D rigidBody;
    private Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = 2;
        walkTime = 2;
        health = 5;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= walkTime)
        {
            walkRight = !walkRight;
            timer = 0;
        }

        if (walkRight)
        {
            transform.eulerAngles = new Vector2(0, 180);
            rigidBody.velocity = Vector2.right * speed;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            rigidBody.velocity = Vector2.left * speed;
        }
    }

    public void Hit(int damage)
    {
        health -= damage;
        animator.SetTrigger("hit");

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
}

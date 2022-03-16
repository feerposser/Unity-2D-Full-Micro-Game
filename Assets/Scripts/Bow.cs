using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float speed;
    public bool isBowRight;
    public int damage;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        speed = 10;
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switch(isBowRight)
        {
            case true:
                ShotRight();
                break;
            case false:
                ShotLeft();
                break;
            default:
                ShotRight();
                break;
        }
    }

    private void ShotRight()
    {
        rigidBody.velocity = Vector2.right * speed;
    }

    private void ShotLeft()
    {
        rigidBody.velocity = Vector2.left * speed;
    }

    public void SetIsBowRight(bool isBowRight)
    {
        this.isBowRight = isBowRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyPatrolGuy>().Hit(damage);
            Destroy(gameObject);
        }
    }
}

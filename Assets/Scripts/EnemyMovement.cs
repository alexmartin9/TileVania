using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemyVelocity = 2f;
    //[SerializeField] float enemyVelocity = 2f;
    Rigidbody2D enemyRb;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HorizontalMove();
    }

    void HorizontalMove()
    {
        enemyRb.velocity = new Vector2(enemyVelocity, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        enemyVelocity *= -1;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        float currentScaleX = transform.localScale.x;
        float currentScaleY = transform.localScale.y;
        transform.localScale = new Vector2(-Mathf.Sign(enemyRb.velocity.x) * Mathf.Abs(currentScaleX), currentScaleY);
    }


}

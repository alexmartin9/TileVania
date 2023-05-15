using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody2D arrowRb;
    [SerializeField] float arrowVelocity = 10f;
    PlayerMovement player;
    float arrowOrientation;
    Collider2D arrowColider;

    void Start()
    {
        arrowRb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        arrowOrientation = Mathf.Sign(player.transform.localScale.x);
        arrowColider = GetComponent<Collider2D>();
        transform.Rotate(0, 0, -90);
    }

    void Update()
    {
        arrowRb.velocity = new Vector2(arrowOrientation * arrowVelocity, 0f);
        

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy")
        {
            Destroy(target.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}

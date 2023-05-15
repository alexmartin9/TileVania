using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{

    GameSession gameSession;
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int coinPoints = 5;
    bool picked = false;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !picked) {
            picked = true;
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
            gameSession.AddPoints(coinPoints);
        }
    }
}

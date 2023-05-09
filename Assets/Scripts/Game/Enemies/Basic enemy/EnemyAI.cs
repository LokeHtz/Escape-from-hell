using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRadius = 5f;  
    public float movementSpeed = 2f;
    bool playerDetected = false;
    public float maxHealth = 10;
    public float currentHealth; 

    public GameObject bulletPrefab;

    public int dmg;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                playerDetected = true;
            }

            if (distanceToPlayer >= detectionRadius * 2)
            {
                playerDetected = false;
            }

            if (playerDetected)
            {
                Vector2 directionToPlayer = (player.position - transform.position).normalized;
                transform.position += new Vector3(
                    directionToPlayer.x * movementSpeed * Time.deltaTime,
                    directionToPlayer.y * movementSpeed * Time.deltaTime,
                    0);
            }
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            currentHealth -= bullet.dmg;
            playerDetected = true;
            Destroy(other.gameObject);

            if (currentHealth <= 0) 
            {
                Destroy(gameObject);
            }
        }
    }
}

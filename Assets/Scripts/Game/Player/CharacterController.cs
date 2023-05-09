using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    public float speed;

    public GameObject PSMEmpty;
    
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 smoothedMoveInput;
    private Vector2 smoothedMoveVelocity;

    private SpriteRenderer _renderer;

    public float predashSpeed;
    public float dashSpeed;
    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    public Collider2D[] Hitbox; 


    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }

        rb = GetComponent<Rigidbody2D>();

        Hitbox = GetComponents<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {

        faceMouse();

        playerStatManager PSM = PSMEmpty.GetComponent<playerStatManager>();
        speed = PSM.playerMoveSpeed;

        smoothedMoveInput = Vector2.SmoothDamp(
            smoothedMoveInput, 
            movementInput, 
            ref smoothedMoveVelocity, 
            0.1f);

        rb.velocity = smoothedMoveInput * speed;

        if (Input.GetKey(KeyCode.Space))
        {            
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                predashSpeed = speed;
                dashSpeed = speed * 3;
                PSM.playerMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
               /* Hitbox[0].enabled = false;
                Hitbox[1].enabled = false; */
            }
        }
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                PSM.playerMoveSpeed = predashSpeed;
                dashCoolCounter = dashCooldown;
               /* Hitbox[0].enabled = true;
                Hitbox[1].enabled = true; */
            }
        }
        if (dashCoolCounter >= 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    void faceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
            );

        if (direction.x <= 0)
        {
            _renderer.flipX = true;
        }
        else
        {
            _renderer.flipX = false;
        }
    }
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerStatManager PSM = PSMEmpty.GetComponent<playerStatManager>();
            EnemyAI enemyAI = other.GetComponent<EnemyAI>();
            PSM.playerHurt(enemyAI.dmg);
        }
    }
}

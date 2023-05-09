using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStatManager : MonoBehaviour
{
    public GameObject player;

    public float playerAttackSpeed = 1;
    public float playerMoveSpeed = 1;
    public float playerAttackDamage = 1;

    public int playerHealth = 100;
    public int playerMaxHealth = 100;

    public healthBar HealthBar;

    public int collectedAmount;

    private void Start()
    {
        HealthBar.setMaxHealth(playerMaxHealth);
    }

    public void updatePlayerHealth(int x)
    {
        playerMaxHealth += x;
        playerHealth += x;
    }

    public void updatePlayerDamage(float x, float y)
    {
        playerAttackDamage += x;
        playerAttackDamage = playerAttackDamage * y;
    }

    public void updatePlayerSpeed(float x)
    {
        playerMoveSpeed += x;
    }

    public void updatePlayerAttackSpeed(float x)
    {
        playerAttackSpeed -= x;   
    }
    public void playerHurt(int dmg)
    {
        playerHealth -= dmg;
        HealthBar.SetHealth(playerHealth);

        if (playerHealth <= 0)
        {
            Destroy(player);
        }
    }
}
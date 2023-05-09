using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    private void Awake()
    {
        GameObject PSMEmpty = GameObject.FindWithTag("PSM");    
        playerStatManager PSM = PSMEmpty.GetComponent<playerStatManager>();
        dmg = dmg * PSM.playerAttackDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}

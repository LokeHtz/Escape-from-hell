using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;

    public string description;

    public Sprite itemImage;
}
public class CollectionController : MonoBehaviour
{
    public GameObject PSMEmpty;

    public Item item;

    public int healthChange;

    public float moveSpeedChange;

    public float attackSpeedChange;

    public float attackDamageChange;

    public float attackDamageMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        PSMEmpty = GameObject.FindGameObjectWithTag("PSM");
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerStatManager psm = PSMEmpty.GetComponent<playerStatManager>();
            psm.collectedAmount++;
            psm.updatePlayerHealth(healthChange);
            psm.updatePlayerSpeed(moveSpeedChange);
            psm.updatePlayerAttackSpeed(attackSpeedChange);
            psm.updatePlayerDamage(attackDamageChange, attackDamageMultiplier);
            Destroy(gameObject);
        }
    }
}

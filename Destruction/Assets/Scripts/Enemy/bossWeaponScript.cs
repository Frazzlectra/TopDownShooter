using UnityEngine;
using System.Collections;

public class bossWeaponScript : MonoBehaviour {

    GameObject player;
    PlayerHealthScript playerHealth;
    int damage = 10;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealthScript>();  
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}

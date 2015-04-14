using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;
    PlayerHealthScript playerHealth;
    SphereCollider sphereCollider;
    public GameObject bossCube;
   
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    
    void Awake()//get player and player health and enemy health
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealthScript>();       
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void OnTriggerEnter(Collider other)//player is in range
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            //Debug.Log("entered trigger");
        }

    }

    void OnTriggerExit(Collider other)//player is not in range
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()//update timer and call attack
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }
    }

    void Attack()// hurt player
    {
        //Debug.Log("Attack");
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            if (gameObject.tag == "Boss")
            {
                Instantiate(bossCube, transform.position, transform.rotation);
            }
            else
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}

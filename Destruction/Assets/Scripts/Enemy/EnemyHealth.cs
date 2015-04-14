using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 1.5f;
    EnemyScript enemyScript;
    EnemyManager enemyManager;
    //for winning
    //Canvas HUDCanvas;
    //GameOverManager gameOverManager;
    
    //Score UI
    //ScoreManager scoreManager;
    public int scoreValue = 10;

    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    void Awake()
    {
        switch (MainMenuScript.core)
        {
            case 1:
                startingHealth = startingHealth / 2;
                break;
            case 2:
                startingHealth = startingHealth / 2 + 20;
                break;
            default:
                startingHealth = startingHealth + 10;
                break;
        }
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        enemyScript = GetComponent<EnemyScript>();
        //HUDCanvas = FindObjectOfType<Canvas>();
        //gameOverManager = HUDCanvas.GetComponent<GameOverManager>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            //move the enemy down by the sinking speed per second
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }
        else
        {
            currentHealth -= amount;
        }
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        capsuleCollider.isTrigger = true;
        StartSinking();
        isDead = true;
        enemyScript.SendMessage("IsDead");
        if (EnemyManager.theBoss)
        {

            GameOverManager.gameWon = true; ;
            return;
        }
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
        //for boss fight
        --EnemyManager.numEnemies;
        //Debug.Log("number of Enemies: " + EnemyManager.numEnemies);
    }
}

using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    public PlayerHealthScript playerHealth;
    public Transform playerPos;
    public GameObject enemy;
    public GameObject boss;
    public float spawnTime = .3f;  
    //getting spawn points
    public Transform[] spawnPoints;
    public int minSpawnDistance = 10;
    //Vector3[] spawnPoints;
    public float timer;
    public float scoreToWin = 1000;
    float timeIncrease = 10;
    //boss fight
    public static bool endless;
    public static int numEnemies;
    public static bool bossFight;
    public static bool theBoss;

    void Start()
    {
        switch (MainMenuScript.core)
        {
            case 1:
                scoreToWin = 500;
                break;
            case 2:
                scoreToWin = 1000;
                break;
            case 3:
                scoreToWin = 1500;
                break;
            default:
                scoreToWin = 1000;
                break;
        }
        //start spawning
        InvokeRepeating("Spawn", spawnTime, spawnTime);        
        //Debug.Log(MainMenuScript.core);
        bossFight = false;
        theBoss = false;
        numEnemies = 0;
    }
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > timeIncrease && spawnTime > 0)
        {
            spawnTime -= .3f;
            timeIncrease = timer + 20;
            //Debug.Log(spawnTime);
        }
        if (!endless)
        {
            //stop spawning enemies
            if (ScoreManager.score >= scoreToWin)
            {
                bossFight = true;
            }
            //spawn boss;
            if (bossFight && numEnemies == 0 && !theBoss)
            {
                BossFight();
               // Debug.Log("BossFight");
            }
        }
    }
    void Spawn()
    {        
        int spawnNum = 0;
        int spawnPointIndexer;
        //Debug.Log("Spawn Enemies");
        if (playerHealth.currentHealth <= 0 || bossFight == true)
        {
            CancelInvoke();
            return;
        }
        else if (MainMenuScript.core == 1 && numEnemies <= 10)//spawn soft core
        {
            //spawn 2 enemies at random spawn points and make sure it not next to player add to numEnemies
            while (spawnNum < 2)
            { 
                spawnPointIndexer = Random.Range(1, spawnPoints.Length);
                if(Vector3.Distance(spawnPoints[spawnPointIndexer].position, playerPos.position) > minSpawnDistance)
                {
                    Instantiate(enemy, spawnPoints[spawnPointIndexer].position, spawnPoints[spawnPointIndexer].rotation);                   
                    ++spawnNum;
                    ++numEnemies;
                }
            }
            //Debug.Log("Num Enemies: " + numEnemies);
        }
        else if (MainMenuScript.core == 2 && numEnemies < 25)//spawn medium core
        {
            //spawning 3 enemies at random points adding them to numEnemies make sure its not spawning next to player
            while (spawnNum < 3)
            {
                spawnPointIndexer = Random.Range(1, spawnPoints.Length);
                if (Vector3.Distance(spawnPoints[spawnPointIndexer].position, playerPos.position) > minSpawnDistance)
                {
                    Instantiate(enemy, spawnPoints[spawnPointIndexer].position, spawnPoints[spawnPointIndexer].rotation);                  
                    ++spawnNum;
                    ++numEnemies;
                }
            }         
            //Debug.Log("Num Enemies: " + numEnemies);
        }
        else if (MainMenuScript.core == 3)//spawn hard core
        {
            //spawning 4 enemies at random points adding them to numEnemies make sure its not next to player
            while (spawnNum < 4)
            {
                spawnPointIndexer = Random.Range(1, spawnPoints.Length);
                if (Vector3.Distance(spawnPoints[spawnPointIndexer].position, playerPos.position) > minSpawnDistance)
                {
                    Instantiate(enemy, spawnPoints[spawnPointIndexer].position, spawnPoints[spawnPointIndexer].rotation);
                    ++spawnNum;
                    ++numEnemies;
                }
            }
            //Debug.Log("Num Enemies: " + numEnemies);
        }
    }
    void BossFight()
    {
        Debug.Log("Spawn Boss");
        Instantiate(boss, spawnPoints[0].position, playerPos.rotation);
        theBoss = true;
    }
}
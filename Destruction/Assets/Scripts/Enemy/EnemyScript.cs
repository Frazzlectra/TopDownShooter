using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    Transform player;
    NavMeshAgent nav;
    bool isDead;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        isDead = false;
    }

    void Update()
    {
        if (!isDead)
        {
            nav.SetDestination(player.position);
        }
    }
    void IsDead()
    {
        isDead = true;
    }
}
using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public int maxBullets = 15;//machine gun
    public int bulletsRemaining;//machine gun
    public float range = 100f;
    //audio
    AudioSource audioSource;
    public AudioClip clip;

    float timer;        //counts ammount of time between shoots
    Ray shootRay;      
    RaycastHit shootHit; //shoot enemy
    int shootableMask;   //gets layer shootable
    LineRenderer gunLine;  
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    
    void Awake ()
    {
        bulletsRemaining = maxBullets; //machinegun
        shootableMask = LayerMask.GetMask("Shootable");
        gunLight = GetComponentInChildren<Light>();
        gunLine = GetComponentInChildren<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
     
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }
        else if (Input.GetButton("Fire2") && timer >= timeBetweenBullets && bulletsRemaining > 0)//machine gun
        {
            Shoot();
        }
        if (Input.GetButtonUp("Fire2") || Input.GetButtonUp("Fire1"))
        {
            bulletsRemaining = maxBullets;
        }
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }        
    }

    public void DisableEffects()//for when player dies
    {
        gunLight.enabled = false;
        gunLine.enabled = false;
    }
    void Shoot()
    {
        timer = 0f;
        --bulletsRemaining; //for machine gun
        gunLight.enabled = true;
        //particle system stuff
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        //prefom the raysast against gameobject on the shootable layer 
        if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) 
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}

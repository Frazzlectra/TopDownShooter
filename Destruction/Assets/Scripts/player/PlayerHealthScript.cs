using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    PlayerScript playerMovement;
    PlayerShooting playerShooting;
    //public EnemyScript enemyScript;
    //??public EnemyManager enemyManager;
    bool isDead;
    bool damaged;
    
    //sound
    AudioSource audioSource;
    public AudioClip clip;

    void Awake()
    {
        
        switch (MainMenuScript.core)
        {
            case 3:
                startingHealth = 150;
                break;
            default:
                startingHealth = 100;
                break;
        }
         
        playerMovement = GetComponent<PlayerScript>();
        playerShooting = GetComponent<PlayerShooting>();
        audioSource = GetComponent<AudioSource>();
        //set the initial health of the player
        currentHealth = startingHealth;
        //health slider
        healthSlider.maxValue = startingHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        if (damaged)
        {
            //set the color of the damage Image to the flash color
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {       
        //damageImage will flash
        damaged = true;
        //reduce health 
        currentHealth -= amount;
        //reduce health bar
        healthSlider.value = currentHealth;
        //sound 
        audioSource.clip = clip;
        audioSource.Play();
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    void Death()
    {
        isDead = true;
        playerShooting.DisableEffects();
        playerMovement.enabled = false;
        playerShooting.enabled = false;
        //enemyScript.SendMessage("IsDead");
    }
}

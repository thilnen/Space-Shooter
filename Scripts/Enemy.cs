using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Enemy Death Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip enemyDeathSFX;
    [SerializeField] [Range(0, 1)] float deathVolume = 0.5f;

    [Header("Projectile")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyProjectilePrefab;
    [SerializeField] float enemyProjectileSpeed = 10f;
    [SerializeField] AudioClip projectileSFX;
    [SerializeField] [Range(0, 1)] float projectileVolume = 0.5f;

 

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyProjectile = Instantiate(
             enemyProjectilePrefab,
             transform.position,
             Quaternion.Euler(0, 0, 180)) as GameObject;
        enemyProjectile.GetComponent<Rigidbody2D>().velocity =
            new Vector2(0, -enemyProjectileSpeed);
        PlayProjectileSound();

    }

    private void PlayProjectileSound()
    {

        //audioSource = GetComponent<AudioSource>();
        //audioSource.volume = projectileVolume;
        AudioSource.PlayClipAtPoint(projectileSFX, 
            Camera.main.transform.position, projectileVolume);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {           
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameStatus>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(
           deathVFX,
           transform.position,
           Quaternion.identity);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(enemyDeathSFX,
        Camera.main.transform.position, deathVolume);        
    }
}

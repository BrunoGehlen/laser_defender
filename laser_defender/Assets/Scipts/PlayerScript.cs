using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float xMoveSpeed = 10f;
    [SerializeField] float yMoveSpeed = 5f;
    [SerializeField] int health = 300;
    [Header("SFX")]
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.20f;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.75f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSound;

    [Header("projectile")]
    [SerializeField] GameObject LaserRed;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    Coroutine firingCoroutine;
    float xMin;
    float xMax;
    float yMax;
    float yMin;
    void Start() {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries() {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0.1f, 0,0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(0.9f, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0.05f,0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.9f, 0)).y;
    }

    void Update() {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        
        if (!damageDealer) {
            return;
        }
        PorcessHit(damageDealer);
    }

    private void PorcessHit(DamageDealer damageDealer) {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint
            (deathSFX, Camera.main.transform.position, deathSFXVolume);
        FindObjectOfType<Level>().LoadGameOver();
    }

    public int GetHealth()
    {
        return health;
    }


    private void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }
    }
    IEnumerator FireContinuosly() {
        while (true) {
            GameObject laser = Instantiate
                (LaserRed, transform.position, Quaternion.identity)
                as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint
                (shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move() {
        //Horizontal Move

        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * xMoveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * yMoveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, transform.position.y);

        //Vertical Move
        transform.position = new Vector2(newXPos, newYPos);
    }
}

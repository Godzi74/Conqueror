using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Photon.MonoBehaviour
{
    public float MoveSpeed;
    public int direction = -1;
    public float damage;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Canvas enemyCanvas;
    public Slider hpBar;
    public float maxRespawnTimer = 30;
    public float respawnTimer;
    public bool dead = false;
    public General_Health enemyHealth = new General_Health();
    public XPDrop enemyXPdrop;
    public float health;
    public float maxHealth;
    void Start()
    {
        respawnTimer = maxRespawnTimer;
    }


    [PunRPC]
    public void respawnEnemy()
    {
        //checks if the enemy is dead and disables and re enables the enemy
        hpBar.value = enemyHealth.health;
        hpBar.maxValue = enemyHealth.maxHealth;
        if (health <= 0 && dead == false)
        {
            //drops experience when dying
            dead = true;
            if (enemyHealth.isBoss == false) 
            {
                enemyXPdrop.DropExp(enemyXPdrop.GetComponent<XPDrop>().xpAmount, "exp");
            }

            if (enemyHealth.isBoss == true)
            {
                enemyXPdrop.DropExp(enemyXPdrop.GetComponent<XPDrop>().xpAmount, "bossKey1");
            }

        }

        if (dead == true)
        {
            //make enemy invisible
            sr.GetComponent<Renderer>().enabled = false;
            rb.GetComponent<Rigidbody2D>().simulated = false;
            hpBar.enabled = false;
            enemyCanvas.enabled = false;
        }

        if (dead == true && respawnTimer <= 0)
        {
            dead = false;
            enemyHealth.health = maxHealth;
            sr.GetComponent<Renderer>().enabled = true;
            rb.GetComponent<Rigidbody2D>().simulated = true;
            if (enemyHealth.isBoss == false)
            {
                hpBar.enabled = true;
                enemyCanvas.enabled = true;
            }
            respawnTimer = maxRespawnTimer;
        }

        if (respawnTimer >= 0 && dead == true)
        {
            respawnTimer -= Time.deltaTime;
            print(respawnTimer);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "enemyBorder")
        {
            //sets the movement boundary for the enemy. The enemy will move in the other direction when contacting the boundary
            direction = direction * -1;
            Debug.Log("Do something else here");
        }

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        //checks for player collision
        if (coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<Player>().buffer <= 0)
        {
            coll.gameObject.GetComponent<Player>().buffer = 1.5f;
            coll.gameObject.GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, damage);
            coll.gameObject.GetComponent<AudioSource>().PlayOneShot(coll.gameObject.GetComponent<Player>().hurtSound);
            print("I'm working");

        }
    }

    private void FixedUpdate()
    {
        health = enemyHealth.health;
        maxHealth = enemyHealth.maxHealth;
        respawnEnemy();

    }
}

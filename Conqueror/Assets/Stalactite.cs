using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : Photon.MonoBehaviour
{
    public Transform spawnPoint;
    public float respawnTimer = 0;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public GameObject projectile;
    public float projSpeed;
    public General_Health stalHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [PunRPC]
    public void LaunchStalactite()
    {
        //spawns the stalactite to fall when damaged by the player
        if (stalHealth.health <= 0f)
        {
            var Shoot = Instantiate(projectile);
            Shoot.transform.position = spawnPoint.position;
            Shoot.GetComponent<Rigidbody2D>().velocity = spawnPoint.up * -projSpeed;
            stalHealth.health = 1f;
            respawnTimer = 20f;
            //Destroy(Shoot);
        }

    }

    [PunRPC]

    public void RespawnStalactite()
    {
        //respawns the stalactite after a duration
        if (respawnTimer > 0)
        {
            sr.GetComponent<Renderer>().enabled = false;
            rb.GetComponent<Rigidbody2D>().simulated = false;

        }

        if (respawnTimer <= 0) 
        {
            sr.GetComponent<Renderer>().enabled = true;
            rb.GetComponent<Rigidbody2D>().simulated = true;
        }

        if (respawnTimer >= 0)
        {
            respawnTimer -= Time.deltaTime;
        }
    }
    void Update()
    {
        LaunchStalactite();
        RespawnStalactite();
    }
}

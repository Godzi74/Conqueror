using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttack : Photon.MonoBehaviour
{
    public GameObject projectile;
    public Enemy getEnemyDead;
    public float attackCD;
    public float projDamage;
    public float projSpeed;
    public Transform birdLocation;
    void Start()
    {
        
    }

    [PunRPC]

    public void ProjectileCD()
    {
       //cooldown for the enemy's projectile attack
        if (attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }

        if (attackCD <= 0)
        {
            ShootProjectile();
        }
    }

    public void ShootProjectile()
    {
        //creates a projectile then adds speed to it
        var Shoot = Instantiate(projectile);
        Shoot.transform.position = birdLocation.position;
        Shoot.GetComponent<Rigidbody2D>().velocity = birdLocation.up * -projSpeed;
        Destroy(Shoot, 2.5f);
        attackCD = 3;
    }
    // Update is called once per frame
    void Update()
    {
        //prevents the enemy from firing a projectile whilst dead
        if (getEnemyDead.dead == false)
        {
            ProjectileCD();
        }  
    }
}

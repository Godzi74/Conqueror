using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General_Health : Photon.MonoBehaviour
{
    public float health;
    public float maxHealth;
    public bool isBoss;
    void Start()
    {
        
    }

    //these methods set the health; they are called via RPCs.
    [PunRPC]
    private void takeDamage(float amount)
    {
        health -= amount;
    }

    [PunRPC]
    private void healHealth(float amount)
    {
        health += amount;
    }
    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Photon.MonoBehaviour
{
    public string chestType;
    public XPDrop enemyXPdrop;
    public bool dead;
    public SpriteRenderer sr;
    public float respawnTimer;
    public BoxCollider2D bc;
    public General_Health chestHP;
    public AudioSource chestSound;
    void Start()
    {
        
    }
    

    [PunRPC]
    public void respawnChest()
    {
        chestSound.volume = 0.25f;
        //checks what kind of chest it is, then drops the corresponding drop
        if (chestHP.health <= 0 && dead == false && chestType == "exp")
        {
            dead = true;
            enemyXPdrop.DropExp(enemyXPdrop.GetComponent<XPDrop>().xpAmount, "exp");
            chestSound.Play();
        }

        if (chestHP.health <= 0 && dead == false && chestType == "lifesteal")
        {
            dead = true;
            enemyXPdrop.DropExp(enemyXPdrop.GetComponent<XPDrop>().xpAmount, "lifesteal");
            chestSound.Play();
        }

        if (chestHP.health <= 0 && dead == false && chestType == "moveSpeed")
        {
            dead = true;
            enemyXPdrop.DropExp(enemyXPdrop.GetComponent<XPDrop>().xpAmount, "moveSpeed");
            chestSound.Play();
        }

        if (chestHP.health <= 0 && dead == false && chestType == "weaponSkill")
        {
            dead = true;
            enemyXPdrop.DropExp(enemyXPdrop.GetComponent<XPDrop>().xpAmount, "weaponSkill");
            chestSound.Play();
        }

        //sets a respawn time for the chest
        if (dead == true)
        {
            //make enemy invisible
            sr.GetComponent<Renderer>().enabled = false;
            bc.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (dead == true && respawnTimer <= 0)
        {
            dead = false;
            chestHP.health = chestHP.maxHealth;
            sr.GetComponent<Renderer>().enabled = true;
            bc.GetComponent<BoxCollider2D>().enabled = true;
            respawnTimer = 180;
        }

        if (respawnTimer >= 0 && dead == true)
        {
            respawnTimer -= Time.deltaTime;
            print(respawnTimer);
        }

    }

    private void FixedUpdate()
    {
        respawnChest();
    }
}

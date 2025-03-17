using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnHP : MonoBehaviour
{
    public bool alive = true;
    public float respawnTimer = 60f;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RespawnPack()
    {
        //respawns health packs
        if (alive == false)
        {
            sr.GetComponent<Renderer>().enabled = false;
            rb.GetComponent<Rigidbody2D>().simulated = false;
            respawnTimer -= Time.deltaTime;
        }

        if (respawnTimer <= 0)
        {
            alive = true;
            sr.GetComponent<Renderer>().enabled = true;
            rb.GetComponent<Rigidbody2D>().simulated = true;
            respawnTimer = 60f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        RespawnPack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHB : Photon.MonoBehaviour
{
    public BirdAttack getProjDMG;
    public GameObject thisObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checks for player collision & ground collision
        if (collision.gameObject.tag == "Player")
        {
            Destroy(thisObject);
        }

        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().buffer <= 0)
        {
            collision.gameObject.GetComponent<Player>().buffer = 1.5f;
            collision.gameObject.GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, getProjDMG.projDamage);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(collision.gameObject.GetComponent<Player>().hurtSound);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(thisObject);
            print("destroyed!");
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

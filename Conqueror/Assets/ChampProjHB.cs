using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampProjHB : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks for player collision
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().buffer <= 0)
        {
            collision.gameObject.GetComponent<Player>().buffer = 1.5f;
            collision.gameObject.GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.AllBuffered, damage);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(collision.gameObject.GetComponent<Player>().hurtSound);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteHB : Photon.MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks for collisions
        if (collision.gameObject.tag == "Boss")
        {
            Destroy(this.gameObject);
            collision.GetComponent<RockGolem>().armourTimer = 0;
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
            print("destroyed!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRtActive : MonoBehaviour
{
    public RockGolem getBossVariables;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //sets the boss to be active or inactive if there is a player in its arena
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            getBossVariables.bossIsActive = true;
            collision.gameObject.GetComponent<Player>().fightingBoss = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            getBossVariables.bossIsActive = false;
            collision.gameObject.GetComponent<Player>().fightingBoss = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

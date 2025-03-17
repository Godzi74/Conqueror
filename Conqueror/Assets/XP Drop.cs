using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPDrop : Photon.MonoBehaviour
{
    //public GameObject xp_object;
    public float xpAmount;
    public Transform xpDropLocation;
    public GameObject XP;
    public GameObject LS;
    public GameObject MS;
    public GameObject WS;
    public GameObject BK1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [PunRPC]
    public void DropExp(float amount, string type)
    {
        //spawns the type of drop and sets how much experience points it gives
        if (type == "exp")
        {
           GameObject xp_object = Instantiate(XP, xpDropLocation.position, Quaternion.identity);
            xp_object.GetComponent<xp_variables>().xpAmount = amount;
        }

        if (type == "lifesteal")
        {
            GameObject xp_object = Instantiate(LS, xpDropLocation.position, Quaternion.identity);
            xp_object.GetComponent<xp_variables>().xpAmount = amount;
        }

        if (type == "moveSpeed")
        {
            GameObject xp_object = Instantiate(MS, xpDropLocation.position, Quaternion.identity);
            xp_object.GetComponent<xp_variables>().xpAmount = amount;
        }

        if (type == "weaponSkill")
        {
            GameObject xp_object = Instantiate(WS, xpDropLocation.position, Quaternion.identity);
            xp_object.GetComponent<xp_variables>().xpAmount = amount;
        }

        if (type == "bossKey1")
        {
            GameObject xp_object = Instantiate(BK1, xpDropLocation.position, Quaternion.identity);
            xp_object.GetComponent<xp_variables>().xpAmount = amount;
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

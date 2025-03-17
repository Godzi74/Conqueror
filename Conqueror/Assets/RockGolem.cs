using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolem : MonoBehaviour
{
    public Enemy getGolemVariables;
    public General_Health golemHP;
    public GameObject boulderProjectile;
    public GameObject warningArea;
    public Transform boulderSP1;
    public Transform boulderSP2;
    public Transform boulderSP3;
    public Transform boulderSP4;
    public Transform golemOrigin;
    public GameObject bossBarrier;
    public GameObject rockArmour;
    //public SpriteRenderer renderer;
    public float projSpeed;
    public float armourTimer = 20;
    public int boulderOrder = 1;
    public float boulderCD = 5;
    public bool bossIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public void Boulder()
    {
        float ypos1 = boulderSP1.position.y;
        float ypos2 = boulderSP2.position.y;
        float ypos3 = boulderSP3.position.y;
        float ypos4 = boulderSP4.position.y;

        if (boulderCD > 0)
        {
            boulderCD -= Time.deltaTime;
        }

        if (boulderOrder > 4)
        {
            boulderOrder = 1;
        }

        if (boulderCD > 4)
        {
            warningArea.SetActive(false);
        }

        if (boulderCD < 4 && boulderCD > 0)
        {
            warningArea.SetActive(true);

            if (boulderOrder == 1)
            {
                Vector2 getYPos = new Vector2(warningArea.transform.position.x, boulderSP1.position.y);
                warningArea.transform.position = getYPos;
            }
            if (boulderOrder == 2)
            {
                Vector2 getYPos = new Vector2(warningArea.transform.position.x, boulderSP2.position.y);
                warningArea.transform.position = getYPos;
            }
            if (boulderOrder == 3)
            {
                Vector2 getYPos = new Vector2(warningArea.transform.position.x, boulderSP3.position.y);
                warningArea.transform.position = getYPos;
            }
            if (boulderOrder == 4)
            {
                Vector2 getYPos = new Vector2(warningArea.transform.position.x, boulderSP4.position.y);
                warningArea.transform.position = getYPos;
            }
        }

        if (boulderCD <= 0 && boulderOrder == 1) 
        {
            var Shoot = Instantiate(boulderProjectile);
            Shoot.transform.position = boulderSP1.position;
            Shoot.GetComponent<Rigidbody2D>().velocity = boulderSP1.right * -projSpeed;
            Destroy(Shoot, 0.8f);
            boulderCD = 5;
            boulderOrder++;
        }

        if (boulderCD <= 0 && boulderOrder == 2)
        {
            var Shoot = Instantiate(boulderProjectile);
            Shoot.transform.position = boulderSP2.position;
            Shoot.GetComponent<Rigidbody2D>().velocity = boulderSP2.right * projSpeed;
            Destroy(Shoot, 0.8f);
            boulderCD = 5;
            boulderOrder++;
        }

        if (boulderCD <= 0 && boulderOrder == 3)
        {
            var Shoot = Instantiate(boulderProjectile);
            Shoot.transform.position = boulderSP3.position;
            Shoot.GetComponent<Rigidbody2D>().velocity = boulderSP3.right * -projSpeed;
            Destroy(Shoot, 0.8f);
            boulderCD = 5;
            boulderOrder++;
        }

        if (boulderCD <= 0 && boulderOrder == 4)
        {
            var Shoot = Instantiate(boulderProjectile);
            Shoot.transform.position = boulderSP4.position;
            Shoot.GetComponent<Rigidbody2D>().velocity = boulderSP4.right * projSpeed;
            Destroy(Shoot, 0.8f);
            boulderCD = 5;
            boulderOrder++;
        }
    }

    public void MoveGolem()
    {
        if (getGolemVariables.health <= getGolemVariables.maxHealth / 2)
        {
            getGolemVariables.MoveSpeed = 150f;
        }

        if (golemHP.health >= golemHP.maxHealth / 2)
        {
            getGolemVariables.MoveSpeed = 0f;
            this.gameObject.transform.position = golemOrigin.position;
        }
    }

    /*public void FlashWarning()
    {
        if (boulderCD < 4 && boulderCD > 0 && )
        {

        }
    }*/

    void RockArmour()
    {
        if (armourTimer >= 20)
        {
            rockArmour.SetActive(true);
        }

        if (armourTimer < 20)
        {
            rockArmour.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (getGolemVariables.dead == false && bossIsActive == true)
        {
            Boulder();
        }

        if (getGolemVariables.dead == false && armourTimer < 20)
        {
            armourTimer += Time.deltaTime;
        }

        if (getGolemVariables.dead == true && bossIsActive == true)
        {
            bossBarrier.SetActive(false);
        }

        if (getGolemVariables.dead == false && bossIsActive == true)
        {
            bossBarrier.SetActive(true);
        }

        if (getGolemVariables.dead == false && bossIsActive == false)
        {
            bossBarrier.SetActive(false);
            golemHP.health = golemHP.maxHealth;
            this.gameObject.transform.position = golemOrigin.position;
        }
        MoveGolem();
        RockArmour();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_System : MonoBehaviour
{
    public string weaponType;
    public float weaponLevel = 1;
    public int level = 1;
    public float exp;
    public int expRequired = 125;
    public float hbNALocal = 1f;
    public float hbRSLocal = 1.5f;
    public float hbDPLocal = 3.4f;
    public float skill1;
    public float skill2;
    public float skill3;
    public float atkIncreasePerLvl;
    public float hpIncreasePerLvl;
    public Player playerInfo = new Player();
    public General_Health playerHP = new General_Health();
    void Start()
    {
        
    }

    void LevelUp()
    {
        //checks player experience and calculates their current level and required exp for the next level
        if (level == 1 && exp >= expRequired)
        {
            level = 2;
            exp -= expRequired;
            expRequired = 259;
            playerInfo.attack += atkIncreasePerLvl;
            playerHP.maxHealth += hpIncreasePerLvl;
            playerHP.health = playerHP.maxHealth;
        }

        if (level == 2 && exp >= expRequired)
        {
            level = 3;
            exp -= expRequired;
            expRequired = 396;
            playerInfo.attack += atkIncreasePerLvl;
            playerHP.maxHealth += hpIncreasePerLvl;
            playerHP.health = playerHP.maxHealth;
        }

        if (level == 3 && exp >= expRequired)
        {
            level = 4;
            exp -= expRequired;
            expRequired = 536;
            playerInfo.attack += atkIncreasePerLvl;
            playerHP.maxHealth += hpIncreasePerLvl;
            playerHP.health = playerHP.maxHealth;
        }

        if (level == 4 && exp >= expRequired)
        {
            level = 5;
            exp -= expRequired;
            expRequired = 677;
            playerInfo.attack += atkIncreasePerLvl;
            playerHP.maxHealth += hpIncreasePerLvl;
            playerHP.health = playerHP.maxHealth;
        }

        if (level == 5 && exp >= expRequired)
        {
            level = 6;
            exp -= expRequired;
            expRequired = 820;
            playerInfo.attack += atkIncreasePerLvl;
            playerHP.maxHealth += hpIncreasePerLvl;
            playerHP.health = playerHP.maxHealth;
        }

        if (level == 6 && exp >= expRequired)
        {
            level = 7;
            exp -= expRequired;
            expRequired = 964;
            playerInfo.attack += atkIncreasePerLvl;
            playerHP.maxHealth += hpIncreasePerLvl;
            playerHP.health = playerHP.maxHealth;
        }

        if (level == 7 && exp >= expRequired)
        {
            level = 8;
            exp -= expRequired;
            expRequired = 1110;
            playerInfo.attack += atkIncreasePerLvl;
            playerHP.maxHealth += hpIncreasePerLvl;
            playerHP.health = playerHP.maxHealth;
        }

        if (level == 8 && exp >= expRequired)
        {
            level = 9;
            exp -= expRequired;
            expRequired = 1256;
            playerInfo.attack += atkIncreasePerLvl;
            playerHP.maxHealth += hpIncreasePerLvl;
            playerHP.health = playerHP.maxHealth;
        }

        if (level == 9 && exp >= expRequired)
        {
            level = 10;
            exp = expRequired;
            expRequired = 1256;
            playerInfo.attack += atkIncreasePerLvl;
            playerHP.maxHealth += hpIncreasePerLvl;
            playerHP.health = playerHP.maxHealth;
        }
    }

    //sets the damage of weapon skills based on their levels
    void WeaponValues()
    {
        if (weaponType == "sword")
        {
            skill1 = (1 + ((weaponLevel - 1) / 10)) * 100 * hbNALocal;
            skill2 = (1 + ((weaponLevel - 1) / 10)) * 100 * hbRSLocal;
            skill3 = (1 + ((weaponLevel - 1) / 10)) * 100 * hbDPLocal;
            playerInfo.naAV = skill1;
            playerInfo.skill1AV = skill2;
            playerInfo.skill2AV = skill3;
        }
        

    }

    //sets the stat increase per level
    void playerStats()
    {
        if (weaponType == "sword")
        {
            atkIncreasePerLvl = 10;
            hpIncreasePerLvl = 330;
        }
    }
    // Update is called once per frame
    void Update()
    {
        LevelUp();
        WeaponValues();
        playerStats();
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionAttack : MonoBehaviour
{
    public Enemy enemyVariables;
    public Animator animator;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public float attackCD = 5f;
    public bool attacked = false;
    public GameObject projectile;
    public float projSpeed;
    void Start()
    {
        
    }

    [PunRPC]
    public void Attack()
    {
        //checks for player inside its hit range
        Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        for (int i = 0; i < enemiesToHit.Length; i++)
        {
            if (enemiesToHit[i].tag == "Player")
            {
                if (enemiesToHit[i].GetComponent<Player>().buffer <= 0)
                {
                    enemiesToHit[i].GetComponent<Player>().buffer = 1.5f;
                    enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.AllBuffered, enemyVariables.damage);
                    enemiesToHit[i].gameObject.GetComponent<AudioSource>().PlayOneShot(enemiesToHit[i].gameObject.GetComponent<Player>().hurtSound);
                }
            }
        }

        //creates a projectile that launches in two directions
        var Shoot = Instantiate(projectile);
        Shoot.transform.position = attackPos.position;
        Shoot.GetComponent<Rigidbody2D>().velocity = attackPos.right * -projSpeed;
        Destroy(Shoot, 0.6f);

        var Shoot1 = Instantiate(projectile);
        Shoot1.transform.position = attackPos.position;
        Shoot1.GetComponent<Rigidbody2D>().velocity = attackPos.right * projSpeed;
        Destroy(Shoot1, 0.6f);
    }

    [PunRPC]

    //resets all the enemy cooldowns
    public void RestartMovement()
    {
        attackCD = 5;
        attacked = false;
        enemyVariables.MoveSpeed = 80f;
        animator.SetTrigger("running");
    }

    [PunRPC]

    //counts attack cooldown
    public void AttackTimer()
    {
        if (attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }

        if (attackCD <= 0 && attacked == false)
        {
            animator.SetTrigger("Attack");
            attacked = true;
            enemyVariables.MoveSpeed = 0f;
        }
    }

    [PunRPC]
    public void SetHitboxPos()
    {
        //sets enemy hitboxes
        if (enemyVariables.direction == 1)
        {
            attackPos.position = transform.position + new Vector3(139f, 68.6f, 0);
        }

        if (enemyVariables.direction == -1)
        {
            attackPos.position = transform.position + new Vector3(-139f, 68.6f, 0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    void Update()
    {
        if (enemyVariables.dead == false)
        {
            AttackTimer();
        }
        SetHitboxPos();
    }
}

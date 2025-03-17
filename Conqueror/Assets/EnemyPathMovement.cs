using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMovement : Photon.MonoBehaviour
{
    public Enemy enemyVariables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [PunRPC]
    private void moveEnemy()
    {
        //enables enemy to move if they're not dead
        var move = new Vector3(enemyVariables.MoveSpeed, 0);
        if (enemyVariables.health > 0)
        {
            transform.position += move * enemyVariables.direction * Time.deltaTime;
        }

        if (enemyVariables.direction == 1)
        {
            enemyVariables.sr.flipX = false;
        }

        if (enemyVariables.direction == -1)
        {
            enemyVariables.sr.flipX = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }
}

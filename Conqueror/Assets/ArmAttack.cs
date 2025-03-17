using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmAttack : MonoBehaviour
{
    public float armMoveSpeed;
    public float armTimer;
    public Transform armOrigin;
    public Transform armDestination;
    public GameObject warning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //armOrigin.position = this.gameObject.transform.position;
    }

    [PunRPC]

    //causes the arm to move based on a cooldown
    public void MoveArm()
    {
        var move = new Vector3(0, armMoveSpeed);
        if (armTimer <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, armDestination.position, armMoveSpeed);
        }

        if (armTimer > 0)
        {
            armTimer -= Time.deltaTime;
        }

        if (transform.position == armDestination.position)
        {
            armTimer = 20;
        }

        if (transform.position != armOrigin.position && armTimer > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, armOrigin.position, armMoveSpeed / 10);
        }

        if (armTimer < 3.5f)
        {
            warning.SetActive(true);
        }

        if (armTimer >= 3.5f)
        {
            warning.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        MoveArm();
    }
}

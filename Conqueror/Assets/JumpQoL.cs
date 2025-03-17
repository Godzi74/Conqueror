using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class JumpQoL : Photon.MonoBehaviour
{
    // Start is called before the first frame update

    public float fallMultiplier;
    public float lowJumpMultiplier;
    public Rigidbody2D rb;
    public PlayerControls inputs;
    public Player getJumpHeld;
    void Update()
    {
        //increases fall speed the longer the player is falling for
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //enables player to hold jump to increase jump height
        else if (rb.velocity.y > 0 && getJumpHeld.GetComponent<Player>().jumpHeld == 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}

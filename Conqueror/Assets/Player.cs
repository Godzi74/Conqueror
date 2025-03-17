using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : Photon.MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public GameObject PlayerCamera;
    public new PhotonView photonView;
    public SpriteRenderer sr;
    public Text UsernameText;
    //public Slider hp;
    public Animator animator;
    public AudioSource playerAudioSource;
    public GameObject thisGameObject;

    public bool isGrounded = false;
    private string weaponType = "sword";
    public float MoveSpeed;
    public float JumpForce;
    public int airJumps;
    public int maxAirJumps;
    public int jumpHeld;
    public GameObject flameEffect;
    public int level = 1;
    // public float maxHealth = 1100;
    // public float curHealth = 1100;
    public float lifestealValue;
    public float attack = 100;
    public float naAV;
    public float skill1AV;
    public float skill2AV;
    public float hellbladeMultiplier;
    public bool hbState = false;
    float getMove;
    private float getPlayerID;
    public float time2attack = 0;
    public float time2skill1 = 0;
    public float time2skill2 = 0;
    private float tunnelCd = 0;
    public float bloodBar = 0;
    private float bloodBarMaxValue = 100f;
    public bool keyObtained;
    public bool fightingBoss = false;
    public float attackTimer;
    public Transform attackPos;
    public Transform attackPos2;
    public Transform attackPos3;
    public Transform attackPos4;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public bool isDead = false;
    public Transform recentRespawnPoint;
    public float respawnTimer = 15;
    public General_Health playerHealth = new General_Health();
    public XPDrop playerDrops;
    public Level_System playerLevel;
    public float health;
    public float maxHealth;
    public float buffer = 0;
    public AudioSource musicSource;
    public AudioClip hurtSound;
    public AudioClip respawnSound;
    public AudioClip naSound;
    public AudioClip skillSound;
    public AudioClip skill2Sound;
    public AudioClip healSound;
    public AudioClip collectSound;


    PlayerControls inputs;
    private void Awake()
    {
        //initialises player stats
        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;
        getPlayerID = photonView.viewID;

        inputs = new PlayerControls();

        if (photonView.isMine)
        {
            //sets player camera and user player
            GameManager.Instance.LocalPlayer = this.gameObject;
            PlayerCamera.SetActive(true);
            UsernameText.text = PhotonNetwork.playerName;
            
        }

        else
        {
            //sets as other player if it is not the user's
            UsernameText.text = photonView.owner.name;
            UsernameText.color = Color.white;
        }
    }

    //movement
    private void CheckInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        float getMove = Input.GetAxisRaw("Horizontal");
        transform.position += move * MoveSpeed * Time.deltaTime;

        if (inputs.Gameplay.Jump.IsPressed() && isGrounded == true && jumpHeld == 0)
        {
            //airJumps = airJumps - 1;
            //jumpHeld = 1;
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            rb.velocity = Vector2.up * JumpForce;
        }

        if (inputs.Gameplay.Jump.IsPressed() && isGrounded == false && airJumps > 0 && jumpHeld == 0)
        {
            //jumpHeld = 1;
            airJumps = airJumps - 1;
            animator.SetBool("isGrounded", false);
            rb.velocity = Vector2.up * JumpForce;
        }

        if (inputs.Gameplay.Jump.IsPressed())
        {
            jumpHeld = 1;
        }

        if (!inputs.Gameplay.Jump.IsPressed())
        {
            jumpHeld = 0;
        }



        //sets the player direction globally

        if (Input.GetKeyDown(KeyCode.A) || (Input.GetAxisRaw("Horizontal") < 0))
        {
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.D) || (Input.GetAxisRaw("Horizontal") > 0))
        {
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
        }

        
        animator.SetFloat("currentSpd", Mathf.Abs(getMove));


    }

    //uses a raycast to check whther the player is on the ground
    private void CheckGrounded()
    {
        RaycastHit2D[] collisions = Physics2D.RaycastAll(transform.position, -Vector2.up, 25f);
        Debug.DrawRay(transform.position, -Vector2.up, Color.yellow);

        if (collisions != null)
        {
            foreach (RaycastHit2D collision in collisions)
            {
                if (collision.collider.CompareTag("Ground"))
                {
                    isGrounded = true;
                    airJumps = maxAirJumps;
                    animator.SetBool("isGrounded", true);
                }
                else
                {
                    isGrounded = false;
                    animator.SetBool("isGrounded", false);
                }
            }
            print(isGrounded);
        }
    }

    //checks if blood bar is full
    private void CheckHBState()
    {
        if (hbState == true)
        {
            hellbladeMultiplier = 1.3f;
            flameEffect.SetActive(true);
            thisGameObject.GetComponent<PhotonView>().RPC("BloodBarCounter", PhotonTargets.All);
        }
        if (hbState == false)
        {
            hellbladeMultiplier = 1.0f;
            flameEffect.SetActive(false);
        }

        if (bloodBar > 100)
        {
            hbState = true;
            bloodBar = 100;
        }
    }

    //handles player hitboxes and damage formulae, which is called through an RPC
    private void DoDamage()
    {

        if (time2attack <= 0)
        {
            if (inputs.Gameplay.NormalAttack.IsPressed())
            {
                animator.SetTrigger("Attack");
                playerAudioSource.PlayOneShot(naSound, 1f);
                //checks the type of enemy in the hitbox
                Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToHit.Length; i++)
                {
                    if (enemiesToHit[i].tag == "stalactite")
                    {
                        enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, attack * hellbladeMultiplier * (naAV / 100));
                    }

                    if (enemiesToHit[i].GetComponent<General_Health>().isBoss == false)
                    {
                        enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, attack * hellbladeMultiplier * (naAV / 100));
                    }

                    if (enemiesToHit[i].GetComponent<General_Health>().isBoss == true)
                    {
                        if (enemiesToHit[i].GetComponent<RockGolem>().armourTimer >= 20)
                        {
                            enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, 0f);
                        }
                        if (enemiesToHit[i].GetComponent<RockGolem>().armourTimer < 20)
                        {
                            enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, attack * hellbladeMultiplier * (naAV / 100));
                        }

                    }
           
                    thisGameObject.GetComponent<PhotonView>().RPC("healHealth", PhotonTargets.All, playerHealth.maxHealth * (lifestealValue / 100));
                        time2skill1 -= 2;

                }

                time2attack = attackTimer;
            }
        }

        else
        {
            time2attack -= Time.deltaTime;
        }

        if (time2skill1 <= 0)
        {
            if (inputs.Gameplay.Skill1.IsPressed())
            {
                animator.SetTrigger("skill1");
                playerAudioSource.PlayOneShot(skillSound, 0.1f);
                //checks the type of enemy in the hitbox
                Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos2.position, attackRange + 6, whatIsEnemy);
                for (int i = 0; i < enemiesToHit.Length; i++)
                {

                    if (enemiesToHit[i].GetComponent<General_Health>().isBoss == false)
                    {
                        enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, attack * hellbladeMultiplier * (skill1AV / 100));
                    }

                    if (enemiesToHit[i].GetComponent<General_Health>().isBoss == true)
                    {
                        if (enemiesToHit[i].GetComponent<RockGolem>().armourTimer >= 20)
                        {
                            enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, 0f);
                        }
                        if (enemiesToHit[i].GetComponent<RockGolem>().armourTimer < 20)
                        {
                            enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, attack * hellbladeMultiplier * (skill1AV / 100));
                        }
                    }

                        thisGameObject.GetComponent<PhotonView>().RPC("healHealth", PhotonTargets.All, playerHealth.maxHealth * (lifestealValue / 100));
                    if (hbState == false)
                    {

                        thisGameObject.GetComponent<PhotonView>().RPC("AddToBloodBar", PhotonTargets.All, 35f);
                    }

                    if (hbState == true)
                    {
                        thisGameObject.GetComponent<PhotonView>().RPC("AddToBloodBar", PhotonTargets.All, 10f);
                        thisGameObject.GetComponent<PhotonView>().RPC("healHealth", PhotonTargets.All, playerHealth.maxHealth * (1 / 100));

                    }

                }

                time2skill1 = 12;
            }
        }

        else
        {
            time2skill1 -= Time.deltaTime;
        }


        if (time2skill2 <= 0 && playerLevel.GetComponent<Level_System>().weaponLevel > 1)
        {
            if (inputs.Gameplay.Skill2.IsPressed())
            {
                animator.SetTrigger("skill2");
                playerAudioSource.PlayOneShot(skill2Sound, 0.5f);

                if (isGrounded == false)
                {
                    rb.velocity = Vector2.up * JumpForce;
                }

                //check what enemy is overlapping the hitbox
                Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos3.position, attackRange + 40, whatIsEnemy);
                for (int i = 0; i < enemiesToHit.Length; i++)
                {

                    if(enemiesToHit[i].GetComponent<PhotonView>().viewID != getPlayerID)
                    {

                        if (enemiesToHit[i].GetComponent<General_Health>().isBoss == false)
                        {
                            enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, attack * hellbladeMultiplier * (skill2AV / 100));
                        }
                        if (enemiesToHit[i].GetComponent<General_Health>().isBoss == true)
                        {
                            if (enemiesToHit[i].GetComponent<RockGolem>().armourTimer >= 20)
                            {
                                enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, 0f);
                            }
                            if (enemiesToHit[i].GetComponent<RockGolem>().armourTimer < 20)
                            {
                                enemiesToHit[i].GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, attack * hellbladeMultiplier * (skill2AV / 100));
                            }
                        }

                            thisGameObject.GetComponent<PhotonView>().RPC("healHealth", PhotonTargets.All, playerHealth.maxHealth * (lifestealValue / 100));
                        if (hbState == false)
                        {
  
                            thisGameObject.GetComponent<PhotonView>().RPC("AddToBloodBar", PhotonTargets.All, 35f);
                        }

                        if (hbState == true)
                        {
                            thisGameObject.GetComponent<PhotonView>().RPC("healHealth", PhotonTargets.All, playerHealth.maxHealth * (1 / 100));

                        }
                    }

                    
                }



                time2skill2 = 12;
            }
        }

        else
        {
            time2skill2 -= Time.deltaTime;
        }

        if (buffer > 0)
        {
            buffer -= Time.deltaTime;
        }
    }

    //bloodbar update methods
    [PunRPC]
    public void AddToBloodBar(float amount)
    {
        bloodBar += amount;
    }

    [PunRPC]
    public void BloodBarCounter()
    {
        if (hbState == true && bloodBar > 0)
        {
            bloodBar -= Time.deltaTime * 5;
        }

        if (hbState == true && bloodBar <= 0)
        {
            hbState = false;
            bloodBar = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // sets player respawn location
        if (collision.gameObject.tag == "Respawn")
        {
            recentRespawnPoint = collision.transform;
            Debug.Log("New spawn set");
        }


        //checks what kind of pickup the player collided with, then acts accordingly
        if(collision.gameObject.tag == "drop")
        {
            playerAudioSource.PlayOneShot(collectSound, 0.1f);
            playerLevel.GetComponent<Level_System>().exp += collision.GetComponent<xp_variables>().xpAmount;

            if(collision.GetComponent<xp_variables>().dropType == "weaponSkill" && playerLevel.GetComponent<Level_System>().weaponLevel < 10)
            {
                playerLevel.GetComponent<Level_System>().weaponLevel += 1;
            }

            if (collision.GetComponent<xp_variables>().dropType == "moveSpeed" && MoveSpeed <200)
            {
                MoveSpeed += 10;
            }

            if (collision.GetComponent<xp_variables>().dropType == "lifesteal" && lifestealValue < 2.5)
            {
                lifestealValue += 0.25f;
            }

            if (collision.GetComponent<xp_variables>().dropType == "bossKey1")
            {
                keyObtained = true;
            }
            DestroyObject(collision.gameObject);
        }

        if (collision.gameObject.tag == "HP" && collision.gameObject.GetComponent<RespawnHP>().alive == true)
        {
            thisGameObject.GetComponent<PhotonView>().RPC("healHealth", PhotonTargets.All, playerHealth.maxHealth * 0.25f);
            collision.gameObject.GetComponent<RespawnHP>().alive = false;
            playerAudioSource.PlayOneShot(healSound, 0.5f); ;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (inputs.Gameplay.Use.IsPressed())
        {
            if (collision.gameObject.tag == "tunnel" && tunnelCd <= 0)
            {
                thisGameObject.transform.position = collision.gameObject.GetComponent<TunnelTP>().NewTunnelLocation.transform.position;
                tunnelCd = 1;
            }
        }
        
    }

    //handles player UI
    public void xpUI()
    {
        GameObject XP = GameObject.Find("XPBar");
        GameObject XPText = GameObject.Find("XPText");
        XPText.GetComponent<Text>().text = "Lvl: " + playerLevel.level;
        XP.GetComponent<UnityEngine.UI.Slider>().value = playerLevel.exp;
        XP.GetComponent<UnityEngine.UI.Slider>().maxValue = playerLevel.expRequired;
    }

    public void BloodBarUI()
    {
        GameObject BBar = GameObject.Find("BloodBar");
        BBar.GetComponent<UnityEngine.UI.Slider>().maxValue = bloodBarMaxValue;
        BBar.GetComponent<UnityEngine.UI.Slider>().value = bloodBar;
    }

    public void WeaponUI()
    {
        GameObject Skill2 = GameObject.Find("Skill2");
        GameObject X1 = GameObject.Find("X1");
        GameObject X2 = GameObject.Find("X2");

        if (time2skill1 <= 0)
        {
            X1.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }

        else if (time2skill1 > 0)
        {
            X1.GetComponent<UnityEngine.UI.Image>().enabled = true;
        }

       
        if (playerLevel.weaponLevel < 2)
        {
            Skill2.GetComponent<UnityEngine.UI.Image>().enabled = false; ;
        }

        else if (playerLevel.weaponLevel >= 2)
        {
            Skill2.GetComponent<UnityEngine.UI.Image>().enabled = true; ;
        }

        if (time2skill2 <= 0)
        {
            X2.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }

        else if (time2skill2 > 0)
        {
            X2.GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
        
    }

    public void RespawnUI()
    {
        GameObject RespawnPanel = GameObject.Find("RespawnPanel");
        GameObject RespawnText = GameObject.Find("RespawnText");

        if (isDead == true)
        {
            RespawnPanel.GetComponent<UnityEngine.UI.Image>().enabled = true;
            RespawnText.GetComponent<Text>().enabled = true;
            RespawnText.GetComponent<Text>().text = "Respawning in..." + respawnTimer.ToString("F2");
        }

        if (isDead == false)
        {
            RespawnPanel.GetComponent<UnityEngine.UI.Image>().enabled = false;
            RespawnText.GetComponent<Text>().enabled = false;

        }
    }

    public void BossUI()
    {
        GameObject BossHPSlider = GameObject.Find("BossHP");
        GameObject BossUIObjects = GameObject.Find("BossUI");
        GameObject BossHead = GameObject.Find("Boss head");

        BossHPSlider.GetComponent<UnityEngine.UI.Slider>().value = BossHead.GetComponent<General_Health>().health;
        BossHPSlider.GetComponent<UnityEngine.UI.Slider>().maxValue = BossHead.GetComponent<General_Health>().maxHealth;

        if (fightingBoss == true)
        {
            BossUIObjects.GetComponent<Canvas>().enabled = true;
        }
        if (fightingBoss == false)
        {
            BossUIObjects.GetComponent<Canvas>().enabled = false;
        }
    }

    public void KeyUI()
    {
        GameObject KeyUI = GameObject.Find("KeyImage");

        if (keyObtained == false)
        {
            KeyUI.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }

        if (keyObtained == true)
        {
            KeyUI.GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
    }

    //sets a respawn timer and disables player for its duration
    private void respawnPlayer()
    {
        if(health <= 0)
        {
            isDead = true;
        }

        if (isDead == true)
        {
            sr.GetComponent<Renderer>().enabled = false;
            rb.GetComponent<Rigidbody2D>().simulated = false;
        }

        if(respawnTimer > 0 && isDead == true)
        {
            respawnTimer -= Time.deltaTime;
            print(respawnTimer);
        }

        if (isDead == true && respawnTimer <= 0)
        {
            isDead = false;
            playerHealth.health = maxHealth;
            sr.GetComponent<Renderer>().enabled = true;
            rb.GetComponent<Rigidbody2D>().simulated = true;
            respawnTimer = 15;
            transform.position = recentRespawnPoint.position;
        }

        if (isDead == true && keyObtained == true)
        {
            keyObtained = false;
            //photonView.RPC("DropExp", PhotonTargets.All, 0f, "bossKey1");
            playerDrops.DropExp(playerDrops.GetComponent<XPDrop>().xpAmount, "bossKey1");

        }
    }

    private void OnEnable()
    {
       inputs.Gameplay.Enable();
    }

    private void OnDisable()
    {
      inputs.Gameplay.Disable();
    }

    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
        attackPos.position = transform.position + new Vector3(-39.9f, 0.8f, 0);
        attackPos2.position = transform.position + new Vector3(-45.3f, 0.8f, 0);
    }

    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
        attackPos.position = transform.position + new Vector3(39.9f, 0.8f, 0);
        attackPos2.position = transform.position + new Vector3(45.3f, 0.8f, 0);
    }

    private void FixedUpdate()
    {
        if (photonView.isMine)
        {
            xpUI();
            BloodBarUI();
            WeaponUI();
            RespawnUI();
            BossUI();
            KeyUI();
            GameObject hp = GameObject.Find("Slider");
            hp.GetComponent<UnityEngine.UI.Slider>().value = playerHealth.health;
            hp.GetComponent<UnityEngine.UI.Slider>().maxValue = playerHealth.maxHealth;
            if (isDead == false)
            {
                CheckInput();
                DoDamage();
            }
            
        }

        CheckGrounded();
        respawnPlayer();
        CheckHBState();

        //sets a cooldown on the tunnel so that the player can't spam it.
        if (tunnelCd > 0)
        {
            tunnelCd -= Time.deltaTime;
        }
        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;

 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    void Start()
    {
        
    }
}

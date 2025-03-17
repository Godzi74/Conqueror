using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryTunnel : Photon.MonoBehaviour
{
    public GameManager turnOnCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().keyObtained == true)
        {
            //PhotonNetwork.LeaveLobby();
            //PhotonNetwork.LoadLevel("MainMenu");
            //GameObject EndScreen = GameObject.Find("EndGameCanvas");
            GameObject GameScreen = GameObject.Find("GameCanvas");
            GameScreen.GetComponent<Canvas>().enabled = false;
            //EndScreen.GetComponent<Canvas>().enabled = true;
            turnOnCanvas.SceneCamera.SetActive(true);
            turnOnCanvas.EndCanvas.GetComponent<Canvas>().enabled = true;
            GameObject WinnerText = GameObject.Find("Winner");
            WinnerText.GetComponent<Text>().text = collision.gameObject.GetComponent<Player>().UsernameText.text + " wins!";
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

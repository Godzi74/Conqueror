using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameObject PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject SceneCamera;
    //public GameObject StartCanvas;
    public GameObject EndCanvas;
    public Text PingText;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public Transform spawn5;
    public Transform spawn6;
    public Transform spawn7;
    public Transform spawn8;

    public GameObject LocalPlayer;

    private void Update()
    {
        PingText.text = "Ping:" + PhotonNetwork.GetPing();
    }

    private void Awake()
    {
        Instance = this;
        GameCanvas.SetActive(true);
    }
    public void PlayerSpawner()
    {
        //instantiates player object and sets their spawn based on their playerID.
        float randomValue = Random.Range(0.95f, 0.97f);

        //PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * randomValue, 500), Quaternion.identity, 0);
        GameObject test = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * randomValue, 500), Quaternion.identity, 0);
        if (test.GetComponent<PhotonView>().viewID == 1001)
        {
            test.transform.position = spawn1.position;
        }

        if (test.GetComponent<PhotonView>().viewID == 2001)
        {
            test.transform.position = spawn2.position;
        }

        if (test.GetComponent<PhotonView>().viewID == 3001)
        {
            test.transform.position = spawn3.position;
        }

        if (test.GetComponent<PhotonView>().viewID == 4001)
        {
            test.transform.position = spawn4.position;
        }

        if (test.GetComponent<PhotonView>().viewID == 5001)
        {
            test.transform.position = spawn5.position;
        }

        if (test.GetComponent<PhotonView>().viewID == 6001)
        {
            test.transform.position = spawn6.position;
        }

        if (test.GetComponent<PhotonView>().viewID == 7001)
        {
            test.transform.position = spawn7.position;
        }

        if (test.GetComponent<PhotonView>().viewID == 8001)
        {
            test.transform.position = spawn8.position;
        }

        GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public void QuitGame()
    {
        PhotonNetwork.LeaveLobby();
        Application.Quit();
    }
}

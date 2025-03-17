using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField] private string VersionNumber = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;
    // Start is called before the first frame update

    [SerializeField] private GameObject StartButton;
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionNumber);
    }
    //connects player to a lobby
    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Welcome!");
    }
    void Start()
    {
        UsernameMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeUserNameInput()
    {
        if (UsernameInput.text.Length >= 3)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }
    public void SetUserName()
    {
        UsernameMenu?.SetActive(false);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    public void CreateGame()
    {
        //creates a game room with a max nunmber of players
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { maxPlayers = 8 }, null);
    }
    public void JoinGame()
        {
        //enables player to join a room
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.maxPlayers = 8;
            PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);

        }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HelpScreen()
    {
        SceneManager.LoadScene("Help Screen");
    }

    private void OnJoinedRoom()
    {
        //loads the player into the game
        PhotonNetwork.LoadLevel("MainGame");
    }
}

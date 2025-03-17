using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpScreenUI : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HelpScreen()
    {
        SceneManager.LoadScene("Help Screen");
    }

    public void WeaponScreen()
    {
        SceneManager.LoadScene("Weapon Screen");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

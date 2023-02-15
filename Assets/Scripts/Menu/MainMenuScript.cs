using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public Button buttonPlay;
    public Button buttonSettings;
    public Button buttonQuit;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Demolevel");
    }
}

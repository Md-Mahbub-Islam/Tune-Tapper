using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;  
#endif
using UnityEngine.Networking;
using TMPro;

public class MainMenuScript : MonoBehaviour
{

    public Button buttonPlay;
    public Button buttonSettings;
    public Button buttonQuit;
    
    

    public TMP_InputField heightAdjust;
    public TMP_InputField scaleAdjust;
    public TMP_InputField selectedSong;

    string path;


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

    //Quit
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


    
    public void PlayOwnSong()
    {
        StartCoroutine(GetAudioClip());
    }

    public void LoadPath()
    {
        #if UNITY_EDITOR // loading will not work in a build, only in the editor
        path = EditorUtility.OpenFilePanel("Select song", "", "mp3");
        selectedSong.text = path;
        #endif
    }

    IEnumerator GetAudioClip()
    {
        Debug.Log("path: " + path);
        UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(webRequest);
            //clip.name = fileName;
            //audioClips.Add(clip);

            float fHeightAdjust = 4;
            float.TryParse(heightAdjust.text, out fHeightAdjust);

            float fScaleAdjust = 4;
            float.TryParse(scaleAdjust.text, out fScaleAdjust);

            EnvinronmentController.SetAudioSource(clip, fHeightAdjust, fScaleAdjust);
            MusicController.SetAudioSource(clip);
            SceneManager.LoadScene("Demolevel");
        }
    }
}

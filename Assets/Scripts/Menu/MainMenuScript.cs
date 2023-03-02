using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
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


    
    public void PlayOwnSong()
    {
        StartCoroutine(GetAudioClip());
    }

    public void LoadPath()
    {
        path = EditorUtility.OpenFilePanel("Select song", "", "mp3");
        selectedSong.text = path;
    }

    IEnumerator GetAudioClip()
    {
        Debug.Log("path: " + path);
        UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UsingImgur : MonoBehaviour
{
    ImgurSettings settings;
    private const string ApplicationSettingsPath = "ImgurSettings";

    private void Awake()
    {
        settings = Resources.Load<ImgurSettings>(ApplicationSettingsPath);

        if (settings == null)
            throw new System.Exception("Imgur setting is not found. Please create new Imgur Setting.");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (settings == null)
            return;

        if (string.IsNullOrEmpty(settings.secretKey))
        {
            Debug.LogError("Secret key is empty. Please setup the Secret key when you create your application");
            return;
        }

        if (string.IsNullOrEmpty(settings.clientSecret))
        {
            Debug.LogError("Client secret is empty. Please setup the Secret key when you create your application");
            return;
        }

        checkState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkState()
    {
        switch (settings.appState)
        {
            case ImgurSettings.AppState.none:
                // Just start the app
                break;
            case ImgurSettings.AppState.getAuthorization:

                break;
            case ImgurSettings.AppState.authorized:

                break;
            case ImgurSettings.AppState.getToken:

                break;
        }
       
        PlayerPrefs.SetInt("AppState",(int)settings.appState);
       // settings.appState =(ImgurSettings.AppState)PlayerPrefs.GetInt("AppState");
    }
}

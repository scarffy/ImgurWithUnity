using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UsingImgur : MonoBehaviour
{
    ImgurSettings settings;
    private const string ApplicationSettingsPath = "ImgurSettings";

    [Space(20)]
    public TMP_InputField pinInput;
    string clientId;
    string clientSecret;
    string pin;

    [Space(20)]
    string tokenUrl;
    string postImageUrl;

    [Space(20)]
    public TokenReturn returnAuth;

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

        if (string.IsNullOrEmpty(settings.clientId))
        {
            Debug.LogError("Secret key is empty. Please setup the Secret key when you create your application");
            return;
        }

        if (string.IsNullOrEmpty(settings.clientSecret))
        {
            Debug.LogError("Client secret is empty. Please setup the Secret key when you create your application");
            return;
        }

        clientId = settings.clientId;
        clientSecret = settings.clientSecret;
        tokenUrl = settings.tokenUrl;
        postImageUrl = settings.PostImageUrl;

        checkState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AuthorizePin()
    {
        if (string.IsNullOrEmpty(pinInput.text))
        {
            Debug.LogError("Pin is empty");
            return;
        }
        pin = pinInput.text;
        if (string.IsNullOrEmpty(pin))
        {
            Debug.LogError("Pin is still empty");
            return;
        }
        StartCoroutine(GetAuthorization());
    }

    IEnumerator GetAuthorization()
    {
        WWWForm form = new WWWForm();
        form.AddField("client_id", clientId);
        form.AddField("client_secret", clientSecret);
        form.AddField("grant_type", "pin");
        form.AddField("pin", pin);

        using (UnityWebRequest wr = UnityWebRequest.Post(tokenUrl, form))
        {
            yield return wr.SendWebRequest();

            if (!wr.isHttpError || !wr.isNetworkError)
            {
                Debug.Log(wr.downloadHandler.text);
                string json = wr.downloadHandler.text;
                returnAuth = JsonUtility.FromJson<TokenReturn>(json);

                if (!string.IsNullOrEmpty(returnAuth.access_token))
                {
                    returnAuth.success = true;
                    returnAuth.status = (int)wr.responseCode;

                    if (returnAuth.success)
                    {
                        nextState();
                    }
                    else
                    {
                        checkState();
                    }
                }
            }
            else
                Debug.LogError(wr.error);
        }
    }

    IEnumerator GetToken()
    {
        string authorizationUrl = settings.tokenUrl;

        WWWForm form = new WWWForm();
        form.AddField("client_id", clientId);
        form.AddField("client_secret", clientSecret);
        form.AddField("grant_type", "pin");
        form.AddField("pin", pin);

        using (UnityWebRequest wr = UnityWebRequest.Post(authorizationUrl, form))
        {
            yield return wr.SendWebRequest();

            if (!wr.isHttpError || !wr.isNetworkError)
            {
                Debug.Log(wr.downloadHandler.text);
                string json = wr.downloadHandler.text;
                returnAuth = JsonUtility.FromJson<TokenReturn>(json);

                if (!string.IsNullOrEmpty(returnAuth.access_token))
                {
                    returnAuth.success = true;
                    returnAuth.status = (int)wr.responseCode;
                }
            }
            else
                Debug.LogError(wr.error);
        }
    }

    public void OnClick_PostImageWithAccount()
    {
        StartCoroutine(PostImageWithAccount(null, null, null));
    }


    IEnumerator PostImageWithAccount()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest wr = UnityWebRequest.Post(postImageUrl,form))
        {
            yield return wr.SendWebRequest();

            if (wr.error != null)
            {
                Debug.Log(wr.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Unity Web Request Error : " + wr.error);
            }
        }
    }

    IEnumerator PostImageWithAccount(string message, string longMessage, byte[] imageData)
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageData,"uploadImage", "image/jpeg");
        form.AddField("type", "file");
        form.AddField("title", message);
        form.AddField("description", longMessage);

        using (UnityWebRequest wr = UnityWebRequest.Post(postImageUrl, form))
        {
            yield return wr.SendWebRequest();

            if (wr.error != null)
            {
                Debug.Log(wr.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Unity Web Request Error : " + wr.error);
            }
        }
    }

    IEnumerator PostImageWithAccountToAlbum()
    {
        yield return null;
    }

    #region State
    void checkState()
    {
        if (checkKey())
            settings.appState = (ImgurSettings.AppState)PlayerPrefs.GetInt("AppState");
        else
            settings.appState = ImgurSettings.AppState.getAuthorization;

        string url = settings.authorizationBaseUrl + settings.clientId + settings.authorizationExtUrl + "pin";

        switch (settings.appState)
        {
            case ImgurSettings.AppState.getAuthorization:
                if(settings.appState == ImgurSettings.AppState.getAuthorization)
                {
                    Application.OpenURL(url);
                    nextState();
                }                
                
                break;
           
            case ImgurSettings.AppState.getToken:
                if (PlayerPrefs.HasKey("UserToken"))
                {
                    string json = PlayerPrefs.GetString("UserToken");
                    returnAuth = JsonUtility.FromJson<TokenReturn>(json);
                }
                else
                {
                    settings.appState = ImgurSettings.AppState.getAuthorization;
                    Application.OpenURL(url);
                }
                break;

            case ImgurSettings.AppState.authorized:
                if (PlayerPrefs.HasKey("UserToken"))
                {
                    string json = PlayerPrefs.GetString("UserToken");
                    returnAuth = JsonUtility.FromJson<TokenReturn>(json);
                }
                else
                {
                    Debug.LogError("no user token");
                    settings.appState = ImgurSettings.AppState.getAuthorization;
                    Application.OpenURL(url);
                }

                break;
        }
       
        PlayerPrefs.SetInt("AppState",(int)settings.appState);
       // settings.appState =(ImgurSettings.AppState)PlayerPrefs.GetInt("AppState");
    }

    void nextState()
    {
        switch (settings.appState)
        {
            case ImgurSettings.AppState.getAuthorization:
                settings.appState = ImgurSettings.AppState.getToken;
                break;

            case ImgurSettings.AppState.getToken:
                settings.appState = ImgurSettings.AppState.authorized;

                string json = JsonUtility.ToJson(returnAuth);
                PlayerPrefs.SetString("UserToken", json);
                break;
           
        }

        PlayerPrefs.SetInt("AppState", (int)settings.appState);
    }

    bool checkKey()
    {
        if (PlayerPrefs.HasKey("AppState"))
            return true;
        else
            return false;
    }
    #endregion
}

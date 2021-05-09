using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgurSettings : ScriptableObject
{
    // Required when posting image to imgur
    public string clientId = "";
    // Required when requesting access token
    public string clientSecret = "";

    [Space(20)]
    [HideInInspector] public string authorizationBaseUrl = "https://api.imgur.com/oauth2/authorize?client_id=";
    [HideInInspector] public string authorizationExtUrl = "&response_type=";
    [HideInInspector] public string tokenUrl = "https://api.imgur.com/oauth2/token";
    [HideInInspector] public string accountInfoUrl = "https://api.imgur.com/3/account/me/";

    public enum AppState
    {
        getAuthorization,
        getToken,
        authorized,
    }

    [Space(20)]
    public AppState appState;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgurSettings : ScriptableObject
{
    // Required when posting image to imgur
    public string secretKey = "";
    // Required when requesting access token
    public string clientSecret = "";

    [Space(20)]
    string authorizationBaseUrl = "https://api.imgur.com/oauth2/authorize?client_id=";
    string authorizationExtUrl = "&response_type=";
    string tokenUrl = "https://api.imgur.com/oauth2/token";
    string accountInfoUrl = "https://api.imgur.com/3/account/me/";

    public enum AppState
    {
        none,
        getAuthorization,
        getToken,
        authorized,
    }

    [Space(20)]
    public AppState appState;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgurSettings : ScriptableObject
{
    public string secretKey = "";
    public string clientSecret = "";

    [Space(20)]
    string authorizationUrl = "https://api.imgur.com/oauth2/authorize?client_id=0f7ccf701d03f63&response_type=pin";
    string tokenUrl = "https://api.imgur.com/oauth2/token";
    string accountInfoUrl = "https://api.imgur.com/3/account/me/";
}

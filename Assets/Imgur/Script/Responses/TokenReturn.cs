using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TokenReturn
{
    public bool success;
    public int status;

    public string access_token;
    public string expires_in;
    public string token_type;
    public string refresh_token;
    public string account_id;
    public string account_username;

    public TokenData data;
}

[System.Serializable]
public class TokenData
{
    public string error;
    public string request;
    public string method;
}
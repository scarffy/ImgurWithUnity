using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;
using System;

public class Util : MonoBehaviour
{
    // Imgur and Unity

    string url;

    IEnumerator GetImage()
    {
        var wr = UnityWebRequestTexture.GetTexture(url);
        yield return wr.SendWebRequest();

        if(wr.error != null)
        {
            Texture2D texture = ((DownloadHandlerTexture)wr.downloadHandler).texture;
            Sprite sprite =  Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

        }
        else
        {
            Debug.LogError(wr.error);
        }
    }

    #region GetImage
    IEnumerator GetImage(string url,UnityAction<Texture2D> action)
    {
        var wr = UnityWebRequestTexture.GetTexture(url);
        yield return wr.SendWebRequest();

        if(string.IsNullOrEmpty(wr.error))
        {
            action(((DownloadHandlerTexture)wr.downloadHandler).texture);
        }
    }

    IEnumerator GetImage(string url, UnityAction<Sprite> action)
    {
        var wr = UnityWebRequestTexture.GetTexture(url);
        yield return wr.SendWebRequest();

        if (string.IsNullOrEmpty(wr.error))
        {
            Texture2D texture = ((DownloadHandlerTexture)wr.downloadHandler).texture;
            action(Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f));
        }
    }

    IEnumerator GetImage(string url, UnityAction<Texture> action)
    {
        var wr = UnityWebRequestTexture.GetTexture(url);
        yield return wr.SendWebRequest();

        if (string.IsNullOrEmpty(wr.error))
        {
            action(((DownloadHandlerTexture)wr.downloadHandler).texture);
        }
    }
    #endregion
}

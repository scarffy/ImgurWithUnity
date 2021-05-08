using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingImgur : MonoBehaviour
{
    public ImgurSettings settings;
    private const string ApplicationSettingsPath = "ImgurSettings";

    // Start is called before the first frame update
    void Start()
    {
        var applicationSettings =
            Resources.Load<ImgurSettings>(ApplicationSettingsPath);
        Debug.Log(ApplicationSettingsPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

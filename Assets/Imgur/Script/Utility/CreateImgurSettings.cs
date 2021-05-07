using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateImgurSettings 
{
    [MenuItem("Imgur/Create Shared Imgur Settings")]
    static void CreateSetting()
    {

        ImgurSettings setting;
        string path = "Assets/Resources/ImgurSettings.asset";
        if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path)))
        {
            //Debug.Log("Asset existed");
            EditorUtility.FocusProjectWindow();
            setting = AssetDatabase.LoadAssetAtPath(path, typeof(ImgurSettings)) as ImgurSettings;
            Selection.activeObject = setting;
            return;
        }
        else {
            //Debug.Log("Asset null");
            setting = ScriptableObject.CreateInstance<ImgurSettings>();
            AssetDatabase.CreateAsset(setting, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = setting;
        }
        
    }
}

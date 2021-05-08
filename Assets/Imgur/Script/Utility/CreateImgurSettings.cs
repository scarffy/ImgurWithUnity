using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateImgurSettings 
{
    [MenuItem("Imgur/Create Shared Imgur Settings")]
    static void CreateSetting()
    {

        ImgurSettings setting;
        string path = "Assets/Resources/ImgurSettings.asset";
        
        if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path)))
        {
            Debug.Log("No Path");
            if (!Directory.Exists("Assets/Resources"))
            {
                Debug.Log("No Directory");
                string guid = AssetDatabase.CreateFolder("Assets", "Resources");
                setting = ScriptableObject.CreateInstance<ImgurSettings>();
              
            }

            setting = ScriptableObject.CreateInstance<ImgurSettings>();
            AssetDatabase.CreateAsset(setting, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = setting;
        }
        else {
            if (!Directory.Exists("Assets/Resources"))
            {
                Debug.Log("No Directory");
                Directory.CreateDirectory("Assets/Resources");

                setting = ScriptableObject.CreateInstance<ImgurSettings>();
                AssetDatabase.CreateAsset(setting, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = setting;
            }
            EditorUtility.FocusProjectWindow();
            setting = AssetDatabase.LoadAssetAtPath(path, typeof(ImgurSettings)) as ImgurSettings;
            Selection.activeObject = setting;

        }
        
    }
}

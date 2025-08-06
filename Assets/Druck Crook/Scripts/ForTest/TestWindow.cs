using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class TestWindow : EditorWindow
{

    private const string PrefKey = "StartWindow";

    [InitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {

        if (!EditorPrefs.GetBool(PrefKey, false))
        {

            EditorPrefs.SetBool(PrefKey, true);
            EditorApplication.delayCall += ShowWindow;

        }

    }


    [MenuItem("Window/Test Window")]
    public static void ShowWindow()
    {

        GetWindow<TestWindow>("Test Window");

    }

    void OnGUI()
    {

        GUILayout.Label("Works on any scene", EditorStyles.largeLabel);

        if (GUILayout.Button("Add 1000 Coins"))
        {

            PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") + 1000);
            PlayerPrefs.Save();

        }

        if (GUILayout.Button("DeleteAll"))
        {

            PlayerPrefs.DeleteAll();

            PlayerPrefs.SetInt("isBought_0", 1);
            PlayerPrefs.SetFloat("Music", 1);
            PlayerPrefs.SetFloat("SFX", 1);
            PlayerPrefs.SetFloat("Vibration", 1);
            //PlayerPrefs.SetFloat("Settings_Music", 0.8f);

            PlayerPrefs.Save();

        }

    }

}

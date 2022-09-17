using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{
    public static string Key;
    static string json = File.ReadAllText(Application.dataPath + "/Scripts/Keys.json");
    public static KeyParse pathsFromKeys = JsonConvert.DeserializeObject<KeyParse>(json);
    public static bool change2Scenes = false;
    public static bool changeAllScene = false;
    public static bool multiswitch = true;
    public static string secondScene;
    //public static string ThirdScene;

    public enum FileType
    {
        model, pic, vid, pic_model, pic_vid, model_vid, All, animation
    }

    [System.Serializable]
    public class KeyParse
    {
        public Dictionary<string, Paths> Keys { get; set; }
    }

    [System.Serializable]
    public struct Animation
    {
        public string model { get; set; }
        public string animation { get; set; }
    }

    [System.Serializable]
    public class Paths
    {
        public FileType PathType { get; set; }
        public string VidPath { get; set; }
        public string ModelPath { get; set; }
        public string PicPath { get; set; }
        public Animation AnimationPaths { get; set; }
        public int Duration { get; set; }
    }

    public static void findScene()
    {
        switch (pathsFromKeys.Keys[Key].PathType)
        {
            case FileType.model:
                SceneManager.LoadScene("Present Model");
                break;
            case FileType.pic:
                SceneManager.LoadScene("Present Pic");
                break;
            case FileType.vid:
                SceneManager.LoadScene("Present Video");
                break;
            case FileType.pic_model:
                change2Scenes = true;
                SceneManager.LoadScene("Present Pic");
                secondScene = "Present Model";
                //Switch2Scenes("Present Pic", "Present Model");
                break;
            case FileType.model_vid:
                change2Scenes = true;
                SceneManager.LoadScene("Present Model");
                secondScene = "Present Video";
                //Switch2Scenes("Present Model", "Present Video");
                break;
            case FileType.pic_vid:
                change2Scenes = true;
                SceneManager.LoadScene("Present Pic");
                secondScene = "Present Video";
                //Switch2Scenes("Present Pic", "Present Video");
                break;
            case FileType.All:
                changeAllScene = true;
                SceneManager.LoadScene("Present Pic");
                //SwitchAllScenes();
                break;
            case FileType.animation:
                SceneManager.LoadScene("Present Animation");
                break;
        }
    }

    public static IEnumerator Switch2Scenes()
    {
        yield return new WaitForSeconds(pathsFromKeys.Keys[Key].Duration); // this will make it wait 5 second then execute the code below
        SceneManager.LoadScene(secondScene);
        change2Scenes = false;
    }

    public static IEnumerator SwitchScenes1()
    {
        yield return new WaitForSeconds(pathsFromKeys.Keys[Key].Duration);
        SceneManager.LoadScene("Present Model");
        multiswitch = false;
    }

    public static IEnumerator SwitchScenes2()
    {
        yield return new WaitForSeconds(pathsFromKeys.Keys[Key].Duration);
        SceneManager.LoadScene("Present Video");
        changeAllScene = false;
        multiswitch = true;
    }
}
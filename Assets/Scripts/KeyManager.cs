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

    public enum FileType
    {
        model,
        pic,
        vid,
        pic_model,
        pic_vid,
        model_vid,
        All
    }

    [System.Serializable]
    public class KeyParse
    {
        public Dictionary<string, Paths> Keys { get; set; }
    }

    [System.Serializable]
    public class Paths
    {
        public FileType PathType { get; set; }
        public string VidPath { get; set; }
        public string ModelPath { get; set; }
        public string PicPath { get; set; }
        public int Duration { get; set; }
    }

    public static string OutputPathF()
    {
        return Findfile(Key);
    }

    public static string OutputPathJ()
    {
        return JsonParse(Key);
    }

    private static string JsonParse(string key)
    {
        switch (pathsFromKeys.Keys[key].PathType)
        {
            case FileType.model:
                return pathsFromKeys.Keys[key].ModelPath;
                break;
            case FileType.pic:
                return pathsFromKeys.Keys[key].PicPath;
                break;
            case FileType.vid:
                return pathsFromKeys.Keys[key].VidPath;
                break;
        }
        return "Nothing";
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
                timing("Present Pic", "Present Model");
                break;
            case FileType.model_vid:
                timing("Present Model", "Present Video");
                break;
            case FileType.pic_vid:
                timing("Present Pic", "Present Video");
                break;
            case FileType.All:
                timing();
                break;
        }
    }

    private static void timing(string scene1, string scene2)
    {
        SceneManager.LoadScene(scene1);
        Thread.Sleep(pathsFromKeys.Keys[Key].Duration * 60000);
        SceneManager.LoadScene(scene2);
    }

    private static void timing()
    {
        SceneManager.LoadScene("Present Pic");
        Thread.Sleep(pathsFromKeys.Keys[Key].Duration * 60000);
        SceneManager.LoadScene("Present Model");
        Thread.Sleep(pathsFromKeys.Keys[Key].Duration * 60000);
        SceneManager.LoadScene("Present Video");
    }

    private static string Findfile(string Key)
    {
        //var directory = @"C:\Users\Public\JET\3d models obj";
        var directory = Application.dataPath;
        int compare;
        bool inFile = false;
        string target = "", all = directory + "\\" + Key;
        string path;

        foreach (string file in Directory.EnumerateFiles(directory, "*.obj"))
        {
            compare = stringCompare(file, all);
            if (compare == 4)
            {
                target = file;
                inFile = true;
                break;
            }
        }

        if (inFile)
        {
            path = target;
        }
        else
        {
            path = "No Path";
            ErrorManager.WirteInFile("Path was not found\n");
        }

        return path;
    }

    private static int stringCompare(string str1, string str2)
    {

        int l1 = str1.Length;
        int l2 = str2.Length;
        int lmin = l1 - l2;

        for (int i = 0; i < lmin; i++)
        {
            int str1_ch = str1[i];
            int str2_ch = str2[i];

            if (str1_ch != str2_ch)
            {
                return str1_ch - str2_ch;
            }
        }

        if (l1 != l2)
        {
            return l1 - l2;
        }
        else
        {
            return 0;
        }
    }
}
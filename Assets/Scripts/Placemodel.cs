using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Placemodel : MonoBehaviour
{
    static string objPath = string.Empty;
    static string MtlPath = string.Empty;
    GameObject loadedObject;
    public GameObject Gparent;

    public TextMeshProUGUI path;
    public TextMeshProUGUI errorMessages;

    void Start()
    {
        objPath = OutputPath();
        MtlPath = OutputMtlPath();
        path.text = OutputPath();
        ErrorManager.CreatingTextFile();
        DisplayModel();
    }

    void DisplayModel() {

        //file path
        if (!File.Exists(objPath))
        {
            errorMessages.text = "File doesn't exist";
            ErrorManager.WirteInFile("File doesn't exist\n");
        }
        else{
            if(loadedObject != null)
            {
                Destroy(loadedObject);
            }

            //Loading 3D Model
            loadedObject = new OBJLoader().Load(objPath, MtlPath);

            //parenting
            loadedObject.transform.parent = Gparent.transform;
            loadedObject.transform.localPosition = new Vector3(0, -5, 0);

            //end
            errorMessages.text = "No Errors";
            ErrorManager.WirteInFile("No Errors\n");
        }
    }

    public static string OutputPath()
    {
        return Findfile(KeyManager.Key);
    }

    public static string OutputMtlPath()
    {
        return FindMtl(KeyManager.Key);
    }
    
    public void onClickReturnToInput()
    {
        SceneManager.LoadScene("Getting Key");
    }

    private static string Findfile(string Key)
    {
        //var directory = @"C:\Users\Public\JET\3d models obj";
        var directory = Application.dataPath; //file path to the directory where the 3d model will be
        int compare;
        bool inFile = false;
        string target = "", all = directory + "\\" + Key;
        string path;

        foreach (string file in Directory.EnumerateFiles(directory, "*.obj"))
        {
            compare = stringCompare(file, all);
            if(compare == 4){
                target = file;
                inFile = true;
                break;
            }
        }

        if(inFile)
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

    private static string FindMtl(string Key)
    {
        //string directory = @"\storage\emulated\0\Download";
        var directory = @"C:\Users\Public\JET\3d models obj";
        int compare;
        bool inFile = false;
        string target = "", all = directory + "\\" + Key;
        string path;

        foreach (string file in Directory.EnumerateFiles(directory, "*.mtl"))
        {
            compare = stringCompare(file, all);
            if(compare == 4){
                target = file;
                inFile = true;
                break;
            }
        }

        if(inFile)
        {
            path = target;
        }
        else
        { 
            path = null;
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

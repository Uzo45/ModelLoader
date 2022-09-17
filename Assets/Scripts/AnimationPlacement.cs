using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AnimationPlacement : MonoBehaviour
{
    public static string fbxPath = KeyManager.pathsFromKeys.Keys[KeyManager.Key].AnimationPaths.model;
    public static string animation = KeyManager.pathsFromKeys.Keys[KeyManager.Key].AnimationPaths.animation;
    public GameObject Eve;
    GameObject fbx;

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(fbxPath))
        {
            fbx = ModelImporter.Importer.Import(fbxPath);
            fbx.transform.parent = Eve.transform;

            //adjustments
            fbx.transform.localRotation = new Quaternion(0, 180, 0, 0);
            Eve.transform.localPosition = new Vector3(3.5f, -2.2f, -55.7f); 
            fbx.transform.localScale = new Vector3(137.05f, 137.05f, 137.05f);
        }
        else
            Debug.Log("The file path doesn't work");
    }
}

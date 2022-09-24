using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Timeline;

public class AnimationPlacement : MonoBehaviour
{
    public static string fbxPath = KeyManager.pathsFromKeys.Keys[KeyManager.Key].AnimationPaths.model;
    public static string animation = KeyManager.pathsFromKeys.Keys[KeyManager.Key].AnimationPaths.animation;
    public GameObject Eve;
    public TimelineAsset time;
    GameObject fbx;
    Animator motion;

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(fbxPath))
        {
            Debug.Log("It exist!");
            fbx = ModelImporter.Importer.Import(fbxPath);
            fbx.transform.parent = Eve.transform;

            //adjustments
            fbx.transform.localRotation = new Quaternion(0, 180, 0, 0);
            Eve.transform.localPosition = new Vector3(3.5f, -2.2f, -55.7f);
            fbx.transform.localScale = new Vector3(137.05f, 137.05f, 137.05f);

            Debug.Log(KeyManager.pathsFromKeys.Keys[KeyManager.Key].AnimationPaths.animation);

            if (File.Exists(animation))
            {
                Debug.Log("It exist again!");
                motion = fbx.AddComponent<Animator>() as Animator;
                //motion.Avatar = fbxPath;
            }
            else
                Debug.Log("The file path for the animation doesn't work");
        }
        else
            Debug.Log("The file path for the model doesn't work");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestModelLoader : MonoBehaviour
{
    public GameObject Eve;
    GameObject fbx;
    static string fbxPath = "C:\\Users\\uanae\\Unity\\ModelLoader\\Assets\\Animation\\Ch36_nonPBR.fbx";

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(fbxPath))
        {
            fbx = ModelImporter.Importer.Import(fbxPath);
            fbx.transform.parent = Eve.transform;
            fbx.transform.localPosition = new Vector3(0.5f, 0, 0);
            fbx.transform.localScale = new Vector3(12.9937506f, 12.9937506f, 12.9937506f);
        }
        else
            Debug.Log("The file path doesn't work");
    }
}

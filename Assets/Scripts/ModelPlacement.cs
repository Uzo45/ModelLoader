using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelPlacement : MonoBehaviour
{
    string objPath = string.Empty;
    //static string MtlPath = string.Empty;
    GameObject loadedObject;
    public GameObject Gparent;

    public TextMeshProUGUI path;
    public TextMeshProUGUI errorMessages;

    void Start()
    {
        objPath = KeyManager.pathsFromKeys.Keys[KeyManager.Key].ModelPath;
        //MtlPath = OutputMtlPath();
        path.text = KeyManager.pathsFromKeys.Keys[KeyManager.Key].ModelPath;
        DisplayModel();
    }

    void DisplayModel()
    {

        //file path
        if (!File.Exists(objPath))
        {
            errorMessages.text = "File doesn't exist";
        }
        else
        {
            if (loadedObject != null)
            {
                Destroy(loadedObject);
            }

            //Loading 3D Model
            loadedObject = new OBJLoader().Load(objPath);

            //parenting
            loadedObject.transform.parent = Gparent.transform;
            loadedObject.transform.localPosition = new Vector3(0, -5, 0);

            //end
            errorMessages.text = "No Errors";
        }
    }

    public void onClickReturnToInput()
    {
        SceneManager.LoadScene("Getting Key");
    }
}

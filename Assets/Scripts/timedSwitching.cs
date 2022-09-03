using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class timedSwitching : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(KeyManager.change2Scenes)
        {
            StartCoroutine(KeyManager.Switch2Scenes());
        }
    }
}

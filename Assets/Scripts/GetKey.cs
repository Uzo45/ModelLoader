using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetKey : MonoBehaviour
{
    public TMP_InputField keyInput;

    public void onClickSetKey()
    {
        if(keyInput.text != null)
        {
            KeyManager.Key = keyInput.text;
            SceneManager.LoadScene("Present Model");
        }
        else
            Debug.Log("Input is null");
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlacement : MonoBehaviour
{
    public VideoPlayer vid;
    string vidpath;

    public TextMeshProUGUI path;

    void Start()
    {
        vidpath = KeyManager.pathsFromKeys.Keys[KeyManager.Key].VidPath;
        vid.url = vidpath;
        path.text = KeyManager.pathsFromKeys.Keys[KeyManager.Key].VidPath;
        Debug.Log(KeyManager.pathsFromKeys.Keys[KeyManager.Key].VidPath);
        vid.Play();
    }

    public void onClickReturnToInput()
    {
        SceneManager.LoadScene("Getting Key");
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PicturePlament : MonoBehaviour
{
	public Image image;
	string imageUrl = string.Empty;
	public GameObject loading;

	public TextMeshProUGUI path;
	public TextMeshProUGUI errorMessages;

	void Start()
	{
		Debug.Log(KeyManager.pathsFromKeys.Keys[KeyManager.Key].PicPath);
		imageUrl = KeyManager.pathsFromKeys.Keys[KeyManager.Key].PicPath;
		path.text = KeyManager.pathsFromKeys.Keys[KeyManager.Key].PicPath;
		presentPic();
	}

	void presentPic()
	{
		loading.SetActive(true);
		if (File.Exists(imageUrl))
        {
			loading.SetActive(false);
			Davinci.get().load(imageUrl).into(image).start();
			errorMessages.text = "No Error";
		}
        else
        {
			errorMessages.text = "The path does not exist";
		}
	}

	public void onClickReturnToInput()
	{
		SceneManager.LoadScene("Getting Key");
	}
}

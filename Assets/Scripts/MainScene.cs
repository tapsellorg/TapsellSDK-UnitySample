using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using TapsellSDK;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    private static string TAPSELL_KEY = "kilkhmaqckffopkpfnacjkobgrgnidkphkcbtmbcdhiokqetigljpnnrbfbnpnhmeikjbq";

    void Start()
    {
        Tapsell.initialize(TAPSELL_KEY);
        Debug.Log("Tapsell Version: " + Tapsell.getVersion());
        Tapsell.setDebugMode(true);
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}

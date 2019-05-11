using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapsellSDK;

public class FirstScene : MonoBehaviour {

  private readonly string TAPSELL_KEY = "kilkhmaqckffopkpfnacjkobgrgnidkphkcbtmbcdhiokqetigljpnnrbfbnpnhmeikjbq";

  void Start () {
    Tapsell.initialize (TAPSELL_KEY);
  }

  public void changeScenes (string name) {
    SceneManager.LoadScene (name);
  }
}
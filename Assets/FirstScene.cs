using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour {

  void Start () { }

  public void changeScenes (string name) {
    SceneManager.LoadScene (name);
  }
}
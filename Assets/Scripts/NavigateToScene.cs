using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class NavigateToScene : MonoBehaviour {
    AppManager appManager;

    // Use this for initialization
    void Start()
    {

    }
    public void LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("3-AR-Character");

       

    }

    // Update is called once per frame
    void Update () {
		
	}
    
}

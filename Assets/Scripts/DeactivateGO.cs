using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class DeactivateGO : MonoBehaviour {
    public GameObject[] ObjectsToDeactivate;
    AppManager appManager;

    // Use this for initialization
    void Start () {
        appManager = AppManager.GetInstance();

    }

    // Update is called once per frame
    void Update () {
		
	}
    public void DeactivateObjects()
    {
        for (int i = 0; i < ObjectsToDeactivate.Length; i++)
        {
            ObjectsToDeactivate[i].SetActive(false);
        }

        
    }
}

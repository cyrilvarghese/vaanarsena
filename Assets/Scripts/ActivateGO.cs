using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGO : MonoBehaviour
{
    public GameObject[] ObjectsToActivate;
    public GameObject ErrorMessage;
    // Use this for initialization
    void OnEnable()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ErrorMessage.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActivateObjects()
    {

        for (int i = 0; i < ObjectsToActivate.Length; i++)
        {
            ObjectsToActivate[i].SetActive(true);

        }
    }
    public void DismissErrorPanel()
    {
        ErrorMessage.SetActive(false);

    }

}


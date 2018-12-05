using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AssetManager : MonoBehaviour {
    public GameObject parentGO;
    // Use this for initialization
    IEnumerator Start()
    {
        //var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "ganesh3.story"));
        //if (myLoadedAssetBundle == null)
        //{
        //    Debug.Log("Failed to load AssetBundle!");
        //    return;
        //}

        string uri = "https://s3-ap-southeast-1.amazonaws.com/vaanarsena/ganesh3.story";
        UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(uri);
        yield return request.SendWebRequest();
        AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);
        var prefab = myLoadedAssetBundle.LoadAsset("ganesh3");
        Instantiate(prefab, parentGO.transform);
        //ganesh.transform.parent = parentGO.transform;

    }

    IEnumerator InstantiateObject()

    {
        string uri = "https://s3-ap-southeast-1.amazonaws.com/vaanarsena/parasu-ar";
        UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(uri);
        yield return request.SendWebRequest();
        AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);
        var prefab = myLoadedAssetBundle.LoadAsset("parasu-ar");
        GameObject parasuRam = Instantiate(prefab, parentGO.transform) as GameObject;
        //ganesh.transform.parent = parentGO.transform;
    }

    // Update is called once per frame
    void Update () {
		
	}
}

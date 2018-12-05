using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARSpriteInitializer : MonoBehaviour {
    public GameObject ARSpriteContainer;
    AppManager appManager;
 
    void OnEnable()
    {
        appManager = AppManager.GetInstance();
        var assets = AssetBundle.GetAllLoadedAssetBundles();
      //  AssetBundleCreateRequest bundle = AssetBundle.LoadFromFileAsync(Application.persistentDataPath+ "/AssetData/"+ appManager.characterId+".unity3d");



        var ARGameObjects = AssetBundleManager.GetARGameObjects();
        char[] charSeparators = new char[] { '-' };
        foreach (var item in ARGameObjects)
        {
            ARSpriteContainer = GameObject.FindGameObjectWithTag(item.Key.Split(charSeparators)[0]);
            Instantiate(item.Value.assetGO, ARSpriteContainer.transform);
        }
      //  var prefab = AssetBundleManager.getAssetPrefab(appManager.characterId + "-ar");

    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

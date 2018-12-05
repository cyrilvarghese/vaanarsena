using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ArSpriteInitialize : MonoBehaviour
{
    public GameObject ARSpriteContainer;
    AppManager appManager;
    AssetBundle bundle;

    // Use this for initialization
    void OnEnable()
    {
        appManager = AppManager.GetInstance();
        var assets = AssetBundle.GetAllLoadedAssetBundles();
        ARSpriteContainer = GameObject.FindGameObjectWithTag(appManager.characterId);
        var prefab = AssetBundleManager.getAssetPrefab(appManager.characterId + "-ar"); 
        Instantiate(prefab, ARSpriteContainer.transform);

    }
    void OnDisable()
    {
        AssetBundle.UnloadAllAssetBundles(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

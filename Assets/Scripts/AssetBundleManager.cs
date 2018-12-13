using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;
using System.IO;

static public class AssetBundleManager
{
    // A dictionary to hold the AssetBundle references
    static private Dictionary<string, AssetBundleRef> dictAssetBundleRefs;
    static private List<string> arrayNames;
    static AssetBundleManager()
    {
        dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
        List<string> arrayNames = new List<string>();

    }
    // Class with the AssetBundle reference, url and version
    public class AssetBundleRef
    {
        public AssetBundle assetBundle = null;
        public GameObject assetGO = null;
        public int version;
        public string url;
        public AssetBundleRef(string strUrlIn, int intVersionIn)
        {
            url = strUrlIn;
            version = intVersionIn;
        }
    };
    // Get an AssetBundle
    public static AssetBundle getAssetBundle(string name)
    {
        string keyName = name;
        AssetBundleRef abRef;
        if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
            return abRef.assetBundle;
        else
            return null;
    }

    public static AssetBundleRef getAssetBundleRef(string name)
    {
        string keyName = name;
        AssetBundleRef abRef;
        if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
            return abRef;
        else
            return null;
    }
    public static GameObject getAssetPrefab(string name)
    {
        string keyName = name;
        AssetBundleRef abRef;
        if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
            return abRef.assetGO;
        else
            return null;
    }
    // Download an AssetBundle
    public static IEnumerator downloadAssetBundle(string url, int version, string name)
    {
        string keyName = name;
        if (dictAssetBundleRefs.ContainsKey(keyName))
            yield return null;
        else
        {
            while (!Caching.ready)
                yield return null;

            //using (WWW www = WWW.LoadFromCacheOrDownload(url, version))
            //{
            //    yield return www;
            //    if (www.error != null)
            //        throw new Exception("WWW download:" + www.error);
            //    AssetBundleRef abRef = new AssetBundleRef(url, version);
            //    abRef.assetBundle = www.assetBundle;
            //    dictAssetBundleRefs.Add(keyName, abRef);
            //}

            string uriAR = url;
            UnityEngine.Networking.UnityWebRequest requestAR = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(uriAR);
            yield return requestAR.SendWebRequest();
            AssetBundle myLoadedAssetBundleAR = DownloadHandlerAssetBundle.GetContent(requestAR);
            AssetBundleRef abRef = new AssetBundleRef(url, version);
              abRef.assetBundle = myLoadedAssetBundleAR;
              dictAssetBundleRefs.Add(keyName, abRef);
        }
    }
    // Unload an AssetBundle
    public static void Unload(string url, int version, bool allObjects)
    {
        string keyName = url + version.ToString();
        AssetBundleRef abRef;
        if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
        {
            abRef.assetBundle.Unload(allObjects);
            abRef.assetBundle = null;
            dictAssetBundleRefs.Remove(keyName);
        }
    }
    public static void AddAssetBundle(AssetBundle bundle,string url, string name,GameObject assetGO)
    {
        AssetBundleRef abRef = new AssetBundleRef(url, 1);
        abRef.assetBundle = bundle;
        abRef.assetGO = assetGO;
        if (!dictAssetBundleRefs.ContainsKey(name))
        {
            dictAssetBundleRefs.Add(name, abRef);

        }
    }
    public static Dictionary<string, AssetBundleRef> GetARGameObjects()
    {
        //var newDictionaty= from kvp in dictAssetBundleRefs
        //       where kvp.Value.ToString().Contains("ar")
        //       select kvp;
       // var newDictionaty = dictAssetBundleRefs.Where(kvp => kvp.Value.ToString().Contains("ar")) as Dictionary<string,AssetBundleRef>;
        return dictAssetBundleRefs.Where(i => i.Key.ToString().Contains("-ar"))
        .ToDictionary(i => i.Key, i => i.Value);

        

    }

    public static void Save(byte[] data, string path)
    {
        //Create the Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            File.WriteAllBytes(path, data);
            Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }
    public static AssetBundle GetBundleFromCache(string assetName)
    {
       
        AssetBundle bundle = null;
        var bundlePath = Path.Combine(Path.Combine(Application.persistentDataPath, "AssetData"), assetName + ".unity3d");
        var info = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "AssetData"));
        var fileInfo = info.GetFiles();
        if (System.IO.File.Exists(bundlePath)){
            bundle = AssetBundle.LoadFromFile(bundlePath);
        }

        return bundle;
    }
    public static List<string> GetAllAssetNames()
    {
        if (arrayNames==null)
        {
            arrayNames = new List<string>();
        }
        var assetBundles = AssetBundle.GetAllLoadedAssetBundles();
        foreach (var item in assetBundles)
        {
            arrayNames.Add(item.name);
        }
        return arrayNames;
    }
}
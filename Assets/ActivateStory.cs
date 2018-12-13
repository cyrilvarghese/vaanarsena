using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;


public class ActivateStory : MonoBehaviour
{
    public GameObject LightBoxContainer;
    public Slider ProgressSlider;
    public Text ProgressText;
    public Text ProgressTextCircular;
    public GameObject ARBtn;
    public GameObject LoadingIndicator;
    public GameObject[] Stories;
    private string assetName;
    public RawImage m_SpinnerImage;
    public GameObject ErrorMessage;
    private string BaseURL = "https://s3-ap-southeast-1.amazonaws.com/vaanarsena/iOS/";

    public Dictionary<int, string> StoryDictionary;
    AppManager appManager;
    private void Start()
    {
        appManager = AppManager.GetInstance();
    }
    // Use this for initialization
    void OnEnable()
    {

        assetName = "";
        StoryDictionary = new Dictionary<int, string>()
        {
            {0,"parasu1" },
            {1,"parasu2" },
            {2,"parasu3" },
            {3,"ram1" },
            {4,"ram2" },
            {5,"ram3" },
            {6,"durga1" },
            {7,"durga2" },
            {8,"durga3" },
            {9,"hanuman1" },
            {10,"hanuman2" },
            {11,"hanuman3" },
            {12,"krishna1" },
            {13,"krishna2" },
            {14,"krishna3" },
            {15,"ganesh1" },
            {16,"ganesh2" },
            {17,"ganesh3" },



        };

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateStoryAtIndex(int index)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ErrorMessage.SetActive(true);
        }
        else
        {
            StoryDictionary.TryGetValue(index, out assetName);
            for (int i = 0; i < Stories.Length; i++)
            {
                Stories[i].SetActive(false);
            }
            LightBoxContainer.SetActive(true);
            CleanupChildren(index);
            DownloadStoryFromURL(index);
        }


    }
    public void CleanupChildren(int index)
    {
        foreach (Transform child in Stories[index].transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void DownloadStoryFromURL(int index)
    {
        //assetname is ket same as prefab /gameobject name
        appManager.characterId = assetName;
        AssetBundle bundle = AssetBundleManager.getAssetBundle(assetName);

        if (!bundle)//if not found in app cache/ was not loaded in this app session 
        {
            string uri = BaseURL + assetName;
            AssetBundle bundleFromCache = AssetBundleManager.GetBundleFromCache(assetName);
            if (bundleFromCache)//then load the assetbundle from the file of fole exit
            {
                Debug.Log("found story on disk");
                InstantiateStoryAndAddtoList(bundleFromCache, index);
            }
            else
            {
                Debug.Log("did not find story on disk.getting from server");

                UnityEngine.Networking.UnityWebRequest request = UnityWebRequest.Get(uri);
                request.SendWebRequest();
                StartCoroutine(ShowCircularProgress(index, uri, request));
            }
        }
        else
        {
            Instantiate(bundle.LoadAsset(assetName), Stories[index].transform);
            Stories[index].SetActive(true);
            ARBtn.gameObject.SetActive(true);
        }

    }

    public IEnumerator ShowCircularProgress(int index, string uri, UnityEngine.Networking.UnityWebRequest request)
    {
        while (!request.isDone)
        {
            ProgressTextCircular.text = Math.Round(request.downloadProgress * 100) + "% ";
            m_SpinnerImage.rectTransform.Rotate(Vector3.forward, 90.0f * Time.deltaTime);
            yield return new WaitForSeconds(.1f);
        }
        ARBtn.gameObject.SetActive(true);

        var bundlePath = Path.Combine(Path.Combine(Application.persistentDataPath, "AssetData"), assetName + ".unity3d");
        AssetBundleManager.Save(request.downloadHandler.data, bundlePath);
        AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(bundlePath);
        InstantiateStoryAndAddtoList(myLoadedAssetBundle, index);

        //start AR call
        AssetBundle bundle = AssetBundleManager.getAssetBundle(assetName + "ar");


        GetARData();

    }


    public void GetARData()
    {
        AssetBundle bundle = AssetBundleManager.getAssetBundle(assetName + "-ar");

        if (!bundle)//if not found in app cache/ was not loaded in this app session 
        {
            Debug.Log("found AR asset on app cache");
            AssetBundle bundleFromCache = AssetBundleManager.GetBundleFromCache(assetName + "-ar");
            if (bundleFromCache)//then load the assetbundle from the file of fole exit
            {
                Debug.Log("found AR asset on disk");
                AddARAssetBundleToList(bundleFromCache);
                UnityEngine.SceneManagement.SceneManager.LoadScene("3-AR-Character");

            }
            else
            {
                Debug.Log("did not find AR asset on disk. Downloading asset");
                string uriAR = BaseURL + assetName + "-ar";
                UnityEngine.Networking.UnityWebRequest requestAR = UnityWebRequest.Get(uriAR);
                requestAR.SendWebRequest();

                StartCoroutine(ShowProgress(uriAR, requestAR));

            }
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("3-AR-Character");
        }

    }
    public IEnumerator ShowProgress(string uriAR, UnityEngine.Networking.UnityWebRequest requestAR)
    {

        while (!requestAR.isDone)
        {
            Debug.Log(string.Format("Downloaded {0:P1}", requestAR.downloadProgress));
            ProgressSlider.value = requestAR.downloadProgress;
            ProgressText.text = Math.Round(requestAR.downloadProgress * 100) + "% loaded...";
            yield return new WaitForSeconds(.1f);
        }

        if (ProgressSlider.gameObject.activeSelf)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("3-AR-Character");
        }
        ProgressSlider.value = 1;
        ProgressSlider.gameObject.SetActive(false);

        var bundlePath = Path.Combine(Path.Combine(Application.persistentDataPath, "AssetData"), assetName + "-ar" + ".unity3d");
        AssetBundleManager.Save(requestAR.downloadHandler.data, bundlePath);
        AssetBundle myLoadedAssetBundleAR = AssetBundle.LoadFromFile(bundlePath);
        AssetBundleManager.AddAssetBundle(myLoadedAssetBundleAR, uriAR, assetName + "-ar", myLoadedAssetBundleAR.LoadAsset(assetName) as GameObject);

    }
    public void AddARAssetBundleToList(AssetBundle myLoadedAssetBundleAR)
    {
        AssetBundleManager.AddAssetBundle(myLoadedAssetBundleAR, BaseURL + assetName + "-ar", assetName + "-ar", myLoadedAssetBundleAR.LoadAsset(assetName) as GameObject);
    }


    public void InstantiateStoryAndAddtoList(AssetBundle myLoadedAssetBundle, int index)
    {
        var prefab = myLoadedAssetBundle.LoadAsset(assetName);
        Stories[index].SetActive(true);
        Instantiate(prefab, Stories[index].transform);
        ARBtn.gameObject.SetActive(true);

        AssetBundleManager.AddAssetBundle(myLoadedAssetBundle, BaseURL + assetName, assetName, myLoadedAssetBundle.LoadAsset(assetName) as GameObject);

    }


    public void LoadScene()
    {

        if (ProgressSlider.value == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("3-AR-Character");

        }
        else
        {
            GetARData();
            AssetBundle bundle = AssetBundleManager.getAssetBundle(assetName + "-ar");
            if (!bundle)//if not found in app cache/ was not loaded in this app session 
            {
                ProgressSlider.gameObject.SetActive(true);
                ARBtn.SetActive(false);
            }

        }
    }

}


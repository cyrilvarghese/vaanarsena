using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Vuforia
{
    public class AppManager : MonoBehaviour
    {
        private AppManager appManager;

        // Use this for initialization
        void Start()
        {
            appManager = AppManager.GetInstance();
            // / Create the Directory if it does not exist

            string dataFileName = "WaterVehicles";
            string tempPath = Path.Combine(Application.persistentDataPath, "AssetData");
            tempPath = Path.Combine(tempPath, dataFileName + ".unity3d");
            if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
            }


        }

        // Update is called once per frame
        void Update()
        {

        }
        private static AppManager instance = null;

        public GameObject effects;
        public string characterId = "";

        public List<string> SelectedFiles = new List<string>();


        private void OnEnable()
        {

            //   SceneManager.sceneLoaded += HandleSceneLoaded;
        }



        private void OnDisable()
        {
            //  SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        public static AppManager GetInstance()
        {
            return instance;
        }

        private class AssetBundleRef
        {
            public AssetBundle assetBundle = null;
            public int version;
            public string url;
            public AssetBundleRef(string strUrlIn, int intVersionIn)
            {
                url = strUrlIn;
                version = intVersionIn;
            }
        };

    }

}
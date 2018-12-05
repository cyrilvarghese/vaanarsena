using System.Collections;
using System.Collections.Generic;
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
           
        }

        // Update is called once per frame
        void Update()
        {

        }
        private static AppManager instance = null;

        public GameObject effects;
        public string  characterId="";
        
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
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CustomButton : MonoBehaviour
{

    public Button buttonComponent;
    private Item item;
    private ActivateStory scrollList;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(Item currentItem, ActivateStory currentScrollList)
    {
        item = currentItem;
        
        StartCoroutine(GetTexture(item));

        scrollList = currentScrollList;

    }

    public void HandleClick()
    {
        scrollList.ActivateStoryAtIndex(item.index);
    }
    IEnumerator GetTexture(Item buttonInfo)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(buttonInfo.imageThumbnailURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            buttonComponent.GetComponent<Image>().sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0, 0));

        }
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
  
public class StoryScrollList : MonoBehaviour
{

    public List<Item> itemList;
    public Transform contentPanel;
 
    public SimpleObjectPool buttonObjectPool;

    public float gold = 20f;


    // Use this for initialization
    void Start()
    {
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            Item item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            CustomButton customBtn = newButton.GetComponent<CustomButton>();
           // customBtn.Setup(item, this);
        }
    }

    public void TryTransferItemToOtherShop(Item item)
    {
        
        Debug.Log("attempted");
    }

    void AddItem(Item itemToAdd, ActivateStory shopList)
    {
        shopList.itemList.Add(itemToAdd);
    }

    private void RemoveItem(Item itemToRemove, ActivateStory shopList)
    {
        for (int i = shopList.itemList.Count - 1; i >= 0; i--)
        {
            if (shopList.itemList[i] == itemToRemove)
            {
                shopList.itemList.RemoveAt(i);
            }
        }
    }
}
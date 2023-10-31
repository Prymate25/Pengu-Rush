using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    public TextMeshProUGUI totalFish;
    public TextMeshProUGUI currentHatName;
    public HatLogic hatLogic;
    private bool isInit = false;
    private int hatCount;
    private int unlockedHatCount;

    // Shop Item
    public GameObject hatPrefab;
    public Transform hatContainer;
    private Hat[] hats;

    // Completion Circle
    public Image completionCircle;
    public TextMeshProUGUI completionText;

    public void Awake(){
        hats = Resources.LoadAll<Hat>("Hat");
        //GameManager.Instance.SaveCoin();
        
    }

    public void PopulateShop(){
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(hatPrefab, hatContainer);
            // Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));
            // Thumbnail
            go.transform.GetChild(0).GetComponent<Image>().sprite = hats[index].Thumbnail;
            // ItemName
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;
            // Price
            
        }
        //}
        

    }
    private void OnHatClick(int i){
        Debug.Log("Clicked");
        //currentHatName.text=hats[i].ItemName;
        hatLogic.SelectHat(i);
    }

    
    

    

    

           
}





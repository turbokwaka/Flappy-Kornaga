using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerDownHandler
{ 
    public string itemName;
    public int cost;
    private ShopManager _shopManager;

    public Image UI_itemImage;
    public Text UI_itemName;
    public Text UI_itemRarity;
    public Text UI_itemDescription;

    private Item itemInfo;
    
    private void Start()
    {
        _shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();

        if (_shopManager.collection.Inventory.Contains(itemName))
        {
            gameObject.GetComponentInChildren<Button>().gameObject.SetActive(false);
        }
        
        // search for item info in item collection
        itemInfo = _shopManager.collection.Items
            .FirstOrDefault(i => i.itemName == itemName);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // making item selected
        // showing item description
        _shopManager.selectedObject = gameObject;

        UI_itemImage.sprite = itemInfo?.sprite;
        UI_itemName.text = itemName;
        UI_itemRarity.text = itemInfo.rarity;
        UI_itemDescription.text = itemInfo?.description;
    }
    
    
}
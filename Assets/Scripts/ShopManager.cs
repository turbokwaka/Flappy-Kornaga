using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public HatCollection collection;
    public List<GameObject> shopSlots;
    public GameObject selectedObject;

    private void Start()
    {
        foreach (var slot in shopSlots)
        {
            var slotName = slot.GetComponent<ShopSlot>().itemName;
            var slotSprite = collection.Items.FirstOrDefault(i => i.itemName == slotName)?.sprite;
            slot.GetComponent<Image>().sprite = slotSprite;
            
            // displaying item cost
            Text costText = slot.GetComponentInChildren<Button>().GetComponentInChildren<Text>();
            costText.text = $"{slot.GetComponent<ShopSlot>().cost}";
        }

        // Вибір одягненого предмету
        var equippedSlot = shopSlots.FirstOrDefault(slot => 
            slot.GetComponent<ShopSlot>().itemName == collection.equippedItemName);
    
        if (equippedSlot != null)
        {
            equippedSlot.GetComponent<ShopSlot>().OnPointerDown(null);
        }
    }


    public void BuyItem(GameObject obj)
    {
        var playerCoins = PlayerPrefs.GetInt("PlayerCoins");
        var selectedItemCost  = selectedObject.GetComponent<ShopSlot>().cost;
        
        if (playerCoins >= selectedItemCost)
        {
            // subtract cost from player coins
            playerCoins -= selectedItemCost;
            PlayerPrefs.SetInt("PlayerCoins", playerCoins);
            
            // add item to inventory and equip
            AddItemToInventory();
            EquipItem();
            
            obj.GetComponentInChildren<Button>().gameObject.SetActive(false);
        }
    }
    
    public void AddItemToInventory()
    {
        var selectedItemName = selectedObject.GetComponent<ShopSlot>().itemName;
        collection.Inventory.Add(selectedItemName);
    }
    
    public void EquipItem()
    {
        var selectedItemName = selectedObject.GetComponent<ShopSlot>().itemName;

        if (collection.Inventory.Contains(selectedItemName))
        {
            collection.equippedItemName = selectedItemName;
        }
        else
        {
            Debug.Log("You dont have this item;");
        }
    }
}

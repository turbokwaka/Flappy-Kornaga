using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "HatCollection", menuName = "ScriptableObjects/HatCollection", order = 1)]
public class HatCollection : ScriptableObject
{
    public List<Item> Items;
    public List<string> Inventory;
    public string equippedItemName;

    public Item EquippedItem()
    {
        var item = Items.FirstOrDefault(i => i.itemName == equippedItemName);

        if (item == null)
        {
            Debug.Log("Ти підор).");
        }
            
        return item;
    }
}

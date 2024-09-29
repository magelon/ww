using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    public void AddItem(string name)
    {
        LoadInventory();
        // Check if the item already exists and can be stacked
        foreach (InventorySlot slot in slots)
        {
            if (slot.ittem != null && slot.ittem.itemName == name )
            {
                slot.amount += 1;
                SaveInventory();
                LoadInventory();
                return;
            }
        }

        Ittem newItem = new Ittem
        {
            itemName = name,
            value = 0
        };

        InventorySlot newSlot = new InventorySlot
        {
            ittem = newItem,
            amount = 1 // Default amount
        };

        slots.Add(newSlot);
        SaveInventory();
        LoadInventory();
        
    }

    public void RemoveItem(string name)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.ittem.itemName == name)
            {
                slot.amount -= 1;
                if (slot.amount <= 0)
                {
                    slots.Remove(slot);
                }
                SaveInventory();
                LoadInventory();
                return;
            }
        }
        
        Debug.Log("Item not found in inventory!");
    }

    public void SaveInventory()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            PlayerPrefs.SetString("Item_" + i + "_Name", slots[i].ittem.itemName);
            PlayerPrefs.SetInt("Item_" + i + "_Amount", slots[i].amount);
        }
        PlayerPrefs.SetInt("Item_Count", slots.Count);
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        int itemCount = PlayerPrefs.GetInt("Item_Count", 0);
        
        slots.Clear();

        for (int i = 0; i < itemCount; i++)
        {
            
            string itemNamee = PlayerPrefs.GetString("Item_" + i + "_Name");
            //Debug.Log("add "+ itemNamee);
            int amountt = PlayerPrefs.GetInt("Item_" + i + "_Amount");
            Ittem newItem = new Ittem
                    {
                        itemName = itemNamee,
                        value=0
                    };

                    InventorySlot newSlot = new InventorySlot
                    {
                        ittem = newItem,
                        amount = amountt
                    };

                    slots.Add(newSlot);
        }

    }


}

[System.Serializable]
public class InventorySlot
{
    public Ittem ittem;
    public int amount;

    public InventorySlot() {}

    public InventorySlot(Ittem ittem, int amount)
    {
        this.ittem = ittem;
        this.amount = amount;
    }
}

[System.Serializable]
public class Ittem
{
    public string itemName;
    public int value;

    public Ittem(){}
    public Ittem(string itemName,int value){
        this.itemName=itemName;
        this.value=value;
    }
}

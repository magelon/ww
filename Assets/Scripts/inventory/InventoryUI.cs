using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab; // Prefab for inventory slots
    public Transform slotParent;  // Parent object that holds the slots
    public Inventory inventory;   // Reference to the player's inventory
    

void Start(){
    //PlayerPrefs.DeleteAll();
    inventory.LoadInventory();
    //inventory.AddItem("banana");
    UpdateUI();
}

    void UpdateUI()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject); // Clear old slots
        }

        foreach (InventorySlot slot in inventory.slots)
        {
            
            GameObject slotGO = Instantiate(slotPrefab,slotParent);
            
            Image icon = slotGO.GetComponentInChildren<Image>();
            
            TextMeshProUGUI amountText = slotGO.GetComponentInChildren<TextMeshProUGUI>();

            icon.sprite = Resources.Load<Sprite>("sumPrefabs/goodImgs/" + slot.ittem.itemName);
            
            amountText.text = slot.amount > 1 ? "x" + slot.amount.ToString() : "";
        }
    }

    void Update()
    {
        //UpdateUI();
    }
}

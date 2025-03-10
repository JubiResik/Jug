using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipCardUI : MonoBehaviour
{
    public PlayerInventory playerInventory;  // Reference to the player's inventory
    public Transform cardSlotParent;  // The parent UI element for the slots (should be set in Inspector)
    public GameObject cardSlotPrefab;  // The prefab for each card slot (button with image)

    void Start()
    {
        // Update UI initially
        UpdateUI();
    }

    // Equip a card to a specific slot (slot 0, 1, 2)
    public void EquipCard(int slot, int cardIndex)
    {
        if (cardIndex < 0 || cardIndex >= playerInventory.cards.Count)
        {
            Debug.LogError("Invalid card index!");
            return;
        }

        // Equip the selected card to the specified slot
        playerInventory.EquipCard(playerInventory.cards[cardIndex], slot);
        UpdateUI();
    }

    // Update the UI with the current equipped cards
    void UpdateUI()
    {
        // Clear existing UI
        foreach (Transform child in cardSlotParent)
        {
            Destroy(child.gameObject);
        }

        // Create new slots for each card in the inventory
        for (int i = 0; i < 3; i++)
        {
            if (playerInventory.equippedCards[i] != null)
            {
                GameObject newCardSlot = Instantiate(cardSlotPrefab, cardSlotParent);
                CardSlotUI cardSlotUI = newCardSlot.GetComponent<CardSlotUI>();
                cardSlotUI.SetCard(playerInventory.equippedCards[i]);
            }
        }
    }
}

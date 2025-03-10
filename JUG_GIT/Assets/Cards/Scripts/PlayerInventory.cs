using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // List of all cards in the player's inventory (e.g., collected cards)
    public List<CardData> cards = new List<CardData>();

    // List of equipped cards for battle (limit to 3 cards for simplicity)
    public List<CardData> equippedCards = new List<CardData>(3);

    // Add a card to the inventory
    public void AddCardToInventory(CardData newCard)
    {
        cards.Add(newCard);
        Debug.Log("Card added to inventory: " + newCard.cardName);
    }

    // Equip a card for battle (to a specific slot)
    public void EquipCard(CardData card, int slot)
    {
        // Ensure valid slot (we only have 3 slots for now)
        if (slot < 0 || slot >= 3)
        {
            Debug.LogError("Invalid slot!");
            return;
        }

        // Equip the card in the specified slot
        equippedCards[slot] = card;
        Debug.Log("Card equipped: " + card.cardName + " in slot " + slot);
    }

    // Unequip a card from a specific slot
    public void UnequipCard(int slot)
    {
        if (slot >= 0 && slot < equippedCards.Count)
        {
            equippedCards[slot] = null;
            Debug.Log("Card unequipped from slot: " + slot);
        }
    }

    // Get all equipped cards (for easy reference, e.g., for battle)
    public List<CardData> GetEquippedCards()
    {
        return equippedCards;
    }
}

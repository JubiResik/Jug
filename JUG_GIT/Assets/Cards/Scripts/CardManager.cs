using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardSlotPrefab; // Reference to the card slot prefab to instantiate
    public GameObject[] rarityPanels; // Panels for each rarity

    // Add the card to the UI and corresponding rarity panel
    public void AddCardToPanel(int rarityIndex, CardData card)
    {
        if (rarityIndex >= 0 && rarityIndex < rarityPanels.Length)
        {
            GameObject newCardSlot = Instantiate(cardSlotPrefab, rarityPanels[rarityIndex].transform);
            CardSlotUI cardSlotUI = newCardSlot.GetComponent<CardSlotUI>();
            cardSlotUI.SetCard(card);  // Set the card data to the UI
        }
    }
}

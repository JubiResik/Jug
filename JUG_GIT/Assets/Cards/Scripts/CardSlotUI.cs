using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSlotUI : MonoBehaviour
{
    public Image cardImage;  // Image component for the card's art
    public TextMeshProUGUI cardNameText;  // TextMeshPro for the card's name
    public TextMeshProUGUI manaCostText;  // TextMeshPro for the card's mana cost

    private CardData cardData;  // The card associated with this slot

    // Method to set up the card slot UI
    public void SetCard(CardData card)
    {
        cardData = card;

        // Set the card's image and name
        cardImage.sprite = card.cardArt;  // Set the image to the card's art
        cardNameText.text = card.cardName;  // Set the card's name
        manaCostText.text = "Mana: " + card.manaCost;  // Set the mana cost of the card
    }
}

using UnityEngine;
using TMPro;  // For TextMeshPro
using UnityEngine.UI;  // For Image component
using System.Linq; // For LINQ methods like Select

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;  // TextMeshPro for card name
    public TextMeshProUGUI strengthText;  // TextMeshPro for card strength
    public TextMeshProUGUI effectText;  // TextMeshPro for card effects
    public TextMeshProUGUI manaCostText; // TextMeshPro for card mana cost
    public Image cardArt; // Image component for card art

    private CardData cardData;

    public void SetCard(CardData data)
    {
        cardData = data;
        nameText.text = data.cardName;  // Set attack name
        strengthText.text = "DMG: " + data.baseDamage;  // Set attack strength
        manaCostText.text = "Mana Cost: " + data.manaCost; // Set mana cost

        if (data.effects.Count > 0)
        {
            effectText.text = "Effect: " + string.Join(", ", data.effects.Select(e => e.ToString()));
        }
        else
        {
            effectText.text = "No Effect";
        }

        cardArt.sprite = data.cardArt; // Set card artwork
    }
}

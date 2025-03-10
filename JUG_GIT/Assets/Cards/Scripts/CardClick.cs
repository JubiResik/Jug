using UnityEngine;
using UnityEngine.EventSystems;

public class CardClick : MonoBehaviour, IPointerClickHandler
{
    private CardData cardData;

    public void SetCard(CardData data)
    {
        cardData = data;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BattleSystem battleSystem = Object.FindFirstObjectByType<BattleSystem>(); // FIXED
        if (battleSystem != null)
        {
            battleSystem.UseCard(0); // Use slot 0 for now (modify as needed)
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("BattleSystem not found!");
        }
    }
}

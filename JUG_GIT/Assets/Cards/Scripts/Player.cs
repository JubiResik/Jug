using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Player Stats")]
    public int level = 1;
    public int health = 100;
    public int maxHealth = 100;
    public int mana = 50;

    [Header("XP System")]
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    [Header("Equipped Cards")]
    public List<CardData> equippedCards = new List<CardData>(3);

    void Awake()
    {
        instance = this;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player took " + amount + " damage!");
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        Debug.Log("Player healed by " + amount);
    }

    public void ApplyStatusEffect(string effect, int duration)
    {
        Debug.Log("Player affected by " + effect + " for " + duration + " turns.");
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        Debug.Log("Gained " + amount + " XP!");

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentXP -= xpToNextLevel;
        xpToNextLevel += 50;
        maxHealth += 10;
        health = maxHealth;
        mana += 5;
        Debug.Log("Leveled up! Now Level " + level);
    }

    public void UseCard(int slot)
    {
        if (slot < 0 || slot >= equippedCards.Count) return;

        CardData selectedCard = equippedCards[slot];
        if (mana < selectedCard.manaCost)
        {
            Debug.Log("Not enough mana!");
            return;
        }

        mana -= selectedCard.manaCost;
        Debug.Log("Player used " + selectedCard.cardName);
        BattleSystem.instance.ApplyCardEffects(selectedCard, true);
    }
}

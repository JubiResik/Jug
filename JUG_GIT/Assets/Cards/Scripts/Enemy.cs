using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;

    [Header("Enemy Stats")]
    public string enemyName;
    public int health;
    public int maxHealth;
    public int experienceReward;

    [Header("Equipped Cards")]
    public List<CardData> equippedCards = new List<CardData>(3);

    void Awake()
    {
        instance = this;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(enemyName + " took " + amount + " damage!");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(enemyName + " was defeated!");
        Player.instance.GainXP(experienceReward);
        Destroy(gameObject);
    }

    public void EnemyTurn()
    {
        if (equippedCards.Count == 0)
        {
            Debug.LogWarning(enemyName + " has no cards equipped!");
            return;
        }

        CardData selectedCard = ChooseBestCard();
        Debug.Log(enemyName + " uses " + selectedCard.cardName);
        BattleSystem.instance.ApplyCardEffects(selectedCard, false);
    }

    private CardData ChooseBestCard()
    {
        CardData bestCard = equippedCards[0];
        int bestPriority = -1;

        foreach (CardData card in equippedCards)
        {
            int priority = EvaluateCardPriority(card);

            if (priority > bestPriority)
            {
                bestCard = card;
                bestPriority = priority;
            }
        }

        return bestCard;
    }

    private int EvaluateCardPriority(CardData card)
    {
        int priority = 0;

        if (card.cardTypes.Contains(CardData.CardType.Attack))
        {
            if (Player.instance.health <= 20) priority += 3;  // Player is weak, attack hard
            else priority += 2;
        }

        if (card.effects.Contains(CardData.EffectType.Heal))
        {
            if (health < maxHealth * 0.3f) priority += 4;  // Heal when low HP
        }

        if (card.effects.Contains(CardData.EffectType.Shield))
        {
            if (health < maxHealth * 0.5f) priority += 2;  // Shield when under 50% HP
        }

        if (card.effects.Contains(CardData.EffectType.Burn))
        {
            if (Player.instance.health > 50) priority += 1;  // Burn stronger enemies
        }

        if (card.effects.Contains(CardData.EffectType.Poison))
        {
            priority += 2; // Poison is always useful over time
        }

        return priority;
    }
}

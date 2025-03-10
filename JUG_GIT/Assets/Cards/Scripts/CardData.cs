using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card System/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite cardArt;

    [Header("Card Settings")]
    public List<CardType> cardTypes = new List<CardType>(); // Multiple card types
    public int manaCost;
    public int baseDamage;

    [Header("Effect Settings")]
    public List<EffectType> effects = new List<EffectType>(); // Multiple effects
    public int effectDuration; // Duration for Poison, Burn, etc.
    public int effectDamage; // Extra damage over time for effects

    [Header("Card Rarity")]
    public Rarity cardRarity; // New Rarity field

    public enum CardType { Attack, Spell, Heal, Buff, Debuff }
    public enum EffectType { None, Burn, Poison, Freeze, Heal, Shield, LifeDrain }
    public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }

    public float GetRarityMultiplier()
    {
        switch (cardRarity)
        {
            case Rarity.Common: return 1.0f;
            case Rarity.Uncommon: return 1.2f;
            case Rarity.Rare: return 1.5f;
            case Rarity.Epic: return 2.0f;
            case Rarity.Legendary: return 3.0f;
            default: return 1.0f;
        }
    }
}

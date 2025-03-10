using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;

    public Player player;
    public Enemy enemy;
    public Text battleLog;

    private bool playerTurn;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DetermineFirstTurn();
    }

    void DetermineFirstTurn()
    {
        playerTurn = Random.Range(0, 2) == 0;
        if (playerTurn)
        {
            battleLog.text = "Player goes first!";
        }
        else
        {
            battleLog.text = enemy.enemyName + " goes first!";
            StartCoroutine(EnemyTurnDelayed());
        }
    }

    public void UseCard(int slot)
    {
        if (!playerTurn)
        {
            battleLog.text = "Wait for your turn!";
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player reference is missing in BattleSystem!");
            return;
        }

        if (slot < 0 || slot >= player.equippedCards.Count)
        {
            Debug.LogError("Invalid card slot selected.");
            return;
        }

        CardData selectedCard = player.equippedCards[slot];

        if (player.mana < selectedCard.manaCost)
        {
            battleLog.text = "Not enough mana to use " + selectedCard.cardName;
            return;
        }

        player.mana -= selectedCard.manaCost;
        battleLog.text = "Player used " + selectedCard.cardName + " (DMG: " + selectedCard.baseDamage + ")";

        if (selectedCard.effects.Count > 0)
        {
            battleLog.text += " [" + string.Join(", ", selectedCard.effects) + "]";
        }

        ApplyCardEffects(selectedCard, true);

        playerTurn = false;
        StartCoroutine(EnemyTurnDelayed());
    }

    IEnumerator EnemyTurnDelayed()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyTurn();
    }

    public void EnemyTurn()
    {
        enemy.EnemyTurn();
        playerTurn = true;
        battleLog.text = "Your turn!";
    }

    public void ApplyCardEffects(CardData card, bool usedByPlayer)
    {
        float rarityMultiplier = card.GetRarityMultiplier();

        if (usedByPlayer)
        {
            if (card.cardTypes.Contains(CardData.CardType.Attack))
            {
                enemy.TakeDamage(Mathf.RoundToInt(card.baseDamage * rarityMultiplier));
            }
        }
        else
        {
            if (card.cardTypes.Contains(CardData.CardType.Attack))
            {
                player.TakeDamage(Mathf.RoundToInt(card.baseDamage * rarityMultiplier));
            }
        }

        foreach (CardData.EffectType effect in card.effects)
        {
            if (usedByPlayer)
            {
                ApplyEffectToEnemy(effect, card.effectDuration, Mathf.RoundToInt(card.effectDamage * rarityMultiplier));
            }
            else
            {
                ApplyEffectToPlayer(effect, card.effectDuration, Mathf.RoundToInt(card.effectDamage * rarityMultiplier));
            }
        }

        battleLog.text = (usedByPlayer ? "Player" : enemy.enemyName) + " used " + card.cardName;
    }

    void ApplyEffectToEnemy(CardData.EffectType effect, int duration, int damage)
    {
        switch (effect)
        {
            case CardData.EffectType.Burn:
                StartCoroutine(ApplyDamageOverTime(enemy, damage, duration, "Burning"));
                break;
            case CardData.EffectType.Poison:
                StartCoroutine(ApplyDamageOverTime(enemy, damage / 2, duration, "Poisoned"));
                break;
            case CardData.EffectType.LifeDrain:
                player.Heal(damage);
                enemy.TakeDamage(damage);
                break;
        }
    }

    void ApplyEffectToPlayer(CardData.EffectType effect, int duration, int damage)
    {
        switch (effect)
        {
            case CardData.EffectType.Burn:
                StartCoroutine(ApplyDamageOverTime(player, damage, duration, "Burning"));
                break;
            case CardData.EffectType.Poison:
                StartCoroutine(ApplyDamageOverTime(player, damage / 2, duration, "Poisoned"));
                break;
            case CardData.EffectType.Heal:
                player.Heal(damage);
                break;
        }
    }

    IEnumerator ApplyDamageOverTime(Player target, int damagePerTurn, int duration, string effectName)
    {
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1.0f);
            target.TakeDamage(damagePerTurn);
            battleLog.text = "Player is " + effectName + "!";
        }
    }

    IEnumerator ApplyDamageOverTime(Enemy target, int damagePerTurn, int duration, string effectName)
    {
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1.0f);
            target.TakeDamage(damagePerTurn);
            battleLog.text = enemy.enemyName + " is " + effectName + "!";
        }
    }
}

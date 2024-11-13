using UnityEngine;

[CreateAssetMenu(fileName = "Card Damage Strategy", menuName = "SOs/CardStrategy/DamageStrategy")]
public class CardDamageStrategy : CardEffectStrategy
{
    [field: SerializeField]
    public int Damage { get; private set; } = 1;
    public override void ApplyEffect(ICardEffectable target)
    {
        //make sure your target has a way to add damage
        //target.
        target.ApplyDamage(Damage);
    }
}


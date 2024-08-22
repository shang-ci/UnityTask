using UnityEngine;


[CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effect/DamageEffect")]

public class DamageEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        if(target == null) return;
        
        switch (targetType)
        {
            case EffectTargetType.Target:
                target.TakeDamage(value);
                break;
            case EffectTargetType.All:
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(value);
                }
                break;
        }
    }
}

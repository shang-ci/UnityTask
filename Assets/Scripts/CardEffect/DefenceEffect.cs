using UnityEngine;

[CreateAssetMenu(fileName = "DefenceEffect", menuName = "Card Effect/DefenceEffect")]

public class DefenceEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        if (targetType == EffectTargetType.Self)
        {
            from.UpdateDefence(value);
        }

        if (targetType == EffectTargetType.Target)
        {
            target.UpdateDefence(value);
        }
    }
}

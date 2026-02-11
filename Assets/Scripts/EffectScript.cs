
using UnityEngine;


public enum Effect
{
    None,
    Damage,
    Burn,
    Freeze,
    Poison,
    Heal

}

public class EffectScript : MonoBehaviour
{
    [SerializeField] private Effect thisEffect;

    
    public virtual Effect GetEffect()
    {
        return thisEffect;
    }

    public virtual void SetEffect(Effect newEffect)
    {
        thisEffect = newEffect;
    }

}


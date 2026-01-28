
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

    void Start()
    {
    }

    public virtual Effect GetEffect()
    {
        return thisEffect;
    }

    public virtual void SetEffect(Effect newEffect)
    {
        thisEffect = newEffect;
    }

}


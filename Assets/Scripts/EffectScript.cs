
using UnityEngine;

public class EffectScript: MonoBehaviour
{

    public enum Effect
    {
        unnassigned,
        none,
        damage,
        burn,
        freeze,
        poison,
        heal
        
    }
    //public Effect thisEffect;

    public virtual Effect GetEffect()
    {
        return Effect.unnassigned;
    }

}


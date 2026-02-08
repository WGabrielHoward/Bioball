
using Scripts.Interface;
using UnityEngine;

public class ActiveDamageEffect
{

    public int EntityId;               // HealthSystem identifier
    public Element TargetElement;      // For immunity / rules

    public int DamagePerTick;          // Amount per tick or total for instant
    public float TickRate;             // 0 for instant damage
    public float TimeUntilNextTick;

    public bool IsColliding = true;    // true while target is still in contact
    public float LingerTimeRemaining;  // counts down after exit

    // Convenience property
    public bool IsInstant => TickRate <= 0f;
}
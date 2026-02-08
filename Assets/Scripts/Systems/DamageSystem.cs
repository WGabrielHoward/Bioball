using System.Collections.Generic;
using UnityEngine;

using Scripts.Interface;
using Scripts.Systems;

using Scripts.Data;

namespace Scripts.Systems
{
    public class DamageSystem : MonoBehaviour
    {
        private readonly List<ActiveDamageEffect> activeEffects = new();

        private void FixedUpdate()
        {
            float dt = Time.deltaTime;

            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                var effect = activeEffects[i];

                if (effect.IsInstant)
                {
                    DamageRouter.Instance.ApplyDamage(effect.EntityId, effect.DamagePerTick);
                    activeEffects.RemoveAt(i);
                    continue;
                }

                effect.TimeUntilNextTick -= dt;
                if (effect.TimeUntilNextTick <= 0f)
                {
                    DamageRouter.Instance.ApplyDamage(effect.EntityId, effect.DamagePerTick);
                    effect.TimeUntilNextTick = effect.TickRate;
                }


                // Linger / expiration
                if (!effect.IsColliding)
                {
                    effect.LingerTimeRemaining -= dt;
                    if (effect.LingerTimeRemaining <= 0f)
                    {
                        activeEffects.RemoveAt(i);
                        continue;
                    }
                }
            }
        }

        // Unified method: DOT or instant
        public void ApplyDamage(
            int entityId,
            Element targetElement,
            int damageAmount,
            float tickRate = 0f,
            float lingerDuration = 0f
        )
        {
            activeEffects.Add(new ActiveDamageEffect
            {
                EntityId = entityId,
                TargetElement = targetElement,
                DamagePerTick = damageAmount,
                TickRate = tickRate,
                TimeUntilNextTick = tickRate, // tickRate=0 → applies instantly in FixedUpdate
                IsColliding = true,
                LingerTimeRemaining = lingerDuration
            });
        }

        // Trigger linger after exit
        public void StartLinger(int entityId, float lingerDuration)
        {
            foreach (var effect in activeEffects)
            {
                if (effect.EntityId == entityId && effect.IsColliding)
                {
                    effect.IsColliding = false;
                    if (effect.LingerTimeRemaining <= 0f)
                        effect.LingerTimeRemaining = lingerDuration;
                }
            }
        }

        // Remove all effects for an entity
        public void RemoveAllEffectsForTarget(int entityId)
        {
            activeEffects.RemoveAll(e => e.EntityId == entityId);
        }
    }
}

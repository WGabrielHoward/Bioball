using System.Collections.Generic;
using UnityEngine;

namespace Assets.Systems.Damage
{
    public class DamageSystem : MonoBehaviour
    {
        private readonly List<ActiveDamageEffect> activeEffects = new();

        void FixedUpdate()
        {
            float dt = Time.deltaTime;

            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                var effect = activeEffects[i];

                // safe null check for destroyed NPCs/players
                if (effect.target is UnityEngine.Object unityTarget && unityTarget == null)
                {
                    activeEffects.RemoveAt(i);
                    continue;
                }


                // Tick logic
                effect.timeUntilNextTick -= dt;
                if (effect.timeUntilNextTick <= 0f)
                {
                    effect.target.ApplyDamage(effect.damagePerTick);
                    effect.timeUntilNextTick = effect.tickRate;
                    Debug.Log($"Applying {effect.damagePerTick} to {effect.target}");
                }

                // Linger / expiration logic
                if (!effect.isColliding)
                {
                    effect.lingerTimeRemaining -= dt;
                    if (effect.lingerTimeRemaining <= 0f)
                    {
                        activeEffects.RemoveAt(i);
                        continue;
                    }
                }
            }
        }

        public void ApplyDamageOverTime(IDamageable target, int damagePerTick, float tickRate, float lingerDuration)
        {

            activeEffects.Add(new ActiveDamageEffect
            {
                target = target,
                damagePerTick = damagePerTick,
                tickRate = tickRate,
                timeUntilNextTick = tickRate,
                isColliding = true,           // active while player is in contact
                lingerTimeRemaining = 0f       // will be set on exit
            });

            Debug.Log($"DamageSystem started DOT for {target}");
        }

        public void StartLinger(IDamageable target, float lingerDuration)
        {
            foreach (var effect in activeEffects)
            {
                if (effect.target == target && effect.isColliding)
                {
                    effect.isColliding = false;

                    // Only set lingerTimeRemaining if it hasn't already been set
                    if (effect.lingerTimeRemaining <= 0f)
                    {
                        effect.lingerTimeRemaining = lingerDuration;
                        Debug.Log($"DamageSystem starting linger for {target} ({lingerDuration}s)");
                    }
                }
            }
        }

        public void RemoveAllEffectsForTarget(IDamageable target)
        {
            activeEffects.RemoveAll(e => e.target == target);
        }
    

        public void RemoveEffectForTarget(IDamageable target)
        {
            var effect = activeEffects.Find(e => e.target == target);
            if (effect != null)
                activeEffects.Remove(effect);
        }

       
    }

}

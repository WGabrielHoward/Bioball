using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Systems.Damage
{

    public interface IDamageable
    {
        void ApplyDamage(int amount);
        Element Element { get; }

    }

    public enum Element
    {
        None,
        Immune,
        Heal,
        Fire,
        Ice,
        Poison,
        Lightning,
        Light,
        Dark,
        Wind,
        Earth,
        Sound,
        Time

    }
}

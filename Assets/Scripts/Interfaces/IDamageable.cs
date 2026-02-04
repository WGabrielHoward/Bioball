using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Interface
{

    public interface IDamageable
    {
        int EntityId { get; }
        Element Element { get; }
        void ApplyDamage(int amount);

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
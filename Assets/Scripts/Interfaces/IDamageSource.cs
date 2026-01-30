using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Interface
{

    public interface IDamageSource
    {
        Element Element { get; }
        int DamagePerTick { get; }
        float TickRate { get; }
        GameObject Owner { get; }
    }

}

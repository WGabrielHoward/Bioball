using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scripts.Interface;

    public class ActiveDamageEffect
    {
        public IDamageable target;
        public int damagePerTick;
        public float tickRate;
        public float timeUntilNextTick;

        public bool isColliding = true;        // true while target is still in contact
        public float lingerTimeRemaining;       // counts down after exit
    }


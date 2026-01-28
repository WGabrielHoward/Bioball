using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Systems.Damage;

    public static class ElementRules
    {
        public static Effect GetStatusForElement(Element element)
        {
        return element switch
        {
            Element.Fire => Effect.Burn,
            Element.Ice => Effect.Freeze,
            Element.Poison => Effect.Poison,
            Element.Heal => Effect.Heal,
            _ => Effect.None,

        };
        }
    }

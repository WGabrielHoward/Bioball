

namespace Scripts.Interface
{

    public interface IDamageSource
    {
        Element Element { get; }
        int DamagePerTick { get; }
        float TickRate { get; }
    }

}

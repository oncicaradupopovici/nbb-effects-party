using NBB.Core.Effects;

namespace Effects
{
    public static class Guid
    {
        public static Effect<System.Guid> NewGuid = Effect.From(System.Guid.NewGuid);
    }
}

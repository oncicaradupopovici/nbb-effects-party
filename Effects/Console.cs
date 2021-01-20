using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NBB.Core.Effects;

namespace Effects
{
    static class ConsoleEffects
    {
        internal class WriteLine
        {
            internal record SideEffect(string Value) : ISideEffect;

            internal class Handler : ISideEffectHandler<SideEffect, Unit>
            {
                public Task<Unit> Handle(SideEffect sideEffect, CancellationToken cancellationToken = new CancellationToken())
                {
                    System.Console.WriteLine(sideEffect.Value);
                    return Unit.Task;
                }
            }
        }

        internal class ReadLine
        {
            internal record SideEffect : ISideEffect<string>;

            internal static string Handle(SideEffect _) => System.Console.ReadLine();
        }
    }

    public static class Console
    {
        public static Effect<Unit> WriteLine(string value) =>
            Effect.Of<ConsoleEffects.WriteLine.SideEffect, Unit>(new (value));

        public static Effect<string> ReadLine = Effect.Of<ConsoleEffects.ReadLine.SideEffect, string>(new());
    }

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddConsoleEffects(this IServiceCollection services)
        {
            services
                .AddSingleton<ISideEffectHandler<ConsoleEffects.WriteLine.SideEffect, Unit>,
                    ConsoleEffects.WriteLine.Handler>()
                .AddSideEffectHandler<ConsoleEffects.ReadLine.SideEffect, string>(ConsoleEffects.ReadLine.Handle);
            return services;
        }
    }
}

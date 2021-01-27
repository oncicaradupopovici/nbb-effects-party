#r "nuget: NBB.Core.Effects.FSharp"
#r "bin/debug/net5.0/Effects.dll"

open NBB.Core.Effects.FSharp
open NBB.Core.Effects.FSharp.Interpreter
open Microsoft.Extensions.DependencyInjection
open Effects

let interpret eff = 
    let configureServices (services: IServiceCollection) =
        services.AddConsoleEffects() |> ignore

    use interpreter = createInterpreterWith configureServices

    eff 
    |> Effect.interpret interpreter
    |> Async.RunSynchronously
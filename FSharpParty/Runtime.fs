module Runtime

open NBB.Core.Effects.FSharp.Interpreter
open NBB.Core.Effects.FSharp
open Microsoft.Extensions.DependencyInjection
open Effects

let configureServices (services: IServiceCollection) =
    services.AddConsoleEffects() |> ignore

let interpreter = createInterpreterWith configureServices

Program.main 
|> Effect.interpret interpreter
|> Async.RunSynchronously
|> ignore


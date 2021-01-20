using CSharpParty;
using Effects;
using NBB.Core.Effects;

await using var interpreter = Interpreter.CreateDefault(services => services.AddConsoleEffects());
await interpreter.Interpret(Program.Main);
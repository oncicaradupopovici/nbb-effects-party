
using System.Collections.Generic;
using System.Linq;
using NBB.Core.Effects;
using Console = Effects.Console;
using Guid = Effects.Guid;

namespace CSharpParty
{
    static class Program
    {
        public static readonly Effect<Unit> Main =
            Console.WriteLine("Give me a number")
                .Then(Console.ReadLine)
                .Then(int.Parse)
                .Then(FizzBuzz)
                .Then(Console.WriteLine);


        //public static readonly Effect<Unit> Main =
        //    Effect.Sequence(
        //        Enumerable.Range(0, 100)
        //            .Select(x=> x.ToString())
        //            .Select(Console.WriteLine));


        // public static readonly Effect<Unit> Main =
        //    Console.WriteLine("What is your name?")
        //        .Then(Console.ReadLine)
        //        .Then(name => Guid.NewGuid.Then(userId => Greet(name, userId)));

        //public static readonly Effect<Unit> Main =
        //    Console.WriteLine("What is your name?")
        //        .Then(Effect.Parallel(Console.ReadLine, Guid.NewGuid)
        //            .Then(x => Greet(x.Item1, x.Item2)));

        //public static readonly Effect<Unit> Main =
        //    Effect.Sequence(new List<Effect<Unit>>
        //    {
        //        Console.WriteLine("What is your name?"),
        //        Effect.Parallel(Console.ReadLine, Guid.NewGuid)
        //            .Then(x => Greet(x.Item1, x.Item2)),
        //        Console.WriteLine("Good bye!")
        //    });

        // public static readonly Effect<Unit> Main =
        //     from id in Guid.NewGuid
        //     from name in GetNameById(id)
        //     let name1 = name.ToUpper()
        //     from _ in Console.WriteLine(name1)
        //     select Unit.Value;


        static Effect<Unit> Greet(string name, System.Guid userId)
            => Console.WriteLine($"Hello {name}, nice to meet you! Your UserId is {userId}");

        static string FizzBuzz(int n) =>
            n switch
            {
                { } value when (value % 3 == 0 && value % 5 == 0) => "FizzBuzz",
                { } value when (value % 3 == 0) => "Fizz",
                { } value when (value % 5 == 0) => "Buzz",
                _ => "No Fizz No Buzz"
            };

        static Effect<string> GetNameById(System.Guid id)
            => Effect.Pure(id.ToString());

    }
}

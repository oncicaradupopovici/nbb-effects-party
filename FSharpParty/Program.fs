module Program

open NBB.Core.Effects.FSharp

module Console =
    let writeLine =
        Effects.Console.WriteLine >> Effect.ignore

    let readLine = Effects.Console.ReadLine

let fizzBuzz n =
    match n with
    | value when (value % 3 = 0 && value % 5 = 0) -> "FizzBuzz"
    | value when (value % 3 = 0) -> "Fizz"
    | value when (value % 5 = 0) -> "Buzz"
    | _ -> "No Fizz No Buzz"


let main =
    effect {
        do! Console.writeLine "Give me a number"
        let! str = Console.readLine
        let fb = str |> System.Int32.Parse |> fizzBuzz
        do! Console.writeLine fb
    }

let main1 =
    effect' {
        do System.Console.WriteLine "Give me a number"
        let str = System.Console.ReadLine()
        let fb = str |> System.Int32.Parse |> fizzBuzz
        do System.Console.WriteLine fb
    }

let main2 =
    Console.writeLine "Give me a number"
    >>= fun _ ->
        Console.readLine
        >>= fun str ->
                let n = str |> System.Int32.Parse |> fizzBuzz
                Console.writeLine n

let main3 =
    [ Console.writeLine "Give me a number"
      System.Int32.Parse >> fizzBuzz
      <!> Console.readLine
      >>= Console.writeLine ]
    |> List.sequence_

let main4 =
    Console.writeLine "Give me a number"
    |> Effect.bind
        (fun _ ->
            Console.readLine
            |> Effect.map (System.Int32.Parse >> fizzBuzz)
            |> Effect.bind Console.writeLine)



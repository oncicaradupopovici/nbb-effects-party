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

let main5 =
    [ "Hello"; "World" ]
    |> List.traverse_ Console.writeLine

let chrismasTree n =
    let replicate n s =
        s
        |> Seq.collect (fun e -> Seq.init n (fun _ -> e))
        |> Seq.toList

    let intersperse sep ls =
        List.foldBack
            (fun x ->
                function
                | [] -> [ x ]
                | xs -> x :: sep :: xs)
            ls
            []

    let line i =
        let spaces = replicate (n - i) " "
        let stars = intersperse ' ' (replicate i "*")
        spaces @ stars |> System.String.Concat

    let tree = [ 1 .. n ] |> List.map line

    effect {
        do! tree |> List.traverse_ Console.writeLine
        do! Console.writeLine "R7D 4ever!"
    }

module Tests

open System
open FsUnit
open FsCheck
open NUnit.Framework
open Swensen.Unquote


let Add input = 
    match input with
    | "" -> 0
    | _ when input.Contains("//") ->
        let delim = input.Substring(0, input.IndexOf("//")) 
        [|for n in input.Substring(input.IndexOf("//") + 2).Split([|delim|], StringSplitOptions.RemoveEmptyEntries) -> Int32.Parse n |] 
        |> Array.sum 
    | _  -> 
        [|for n in input.Split(',', '\n') -> Int32.Parse n |] 
        |> Array.sum 

[<Test>]
let Add__no_parameters__returns_zero () = 
        Add("") |> should equal 0

[<Test>]
let Add__one_parameters__returns_parameters_value () = 
        Add("1") |> should equal 1

[<Test>]
let Add__one_parameters__returns_parameters_value_01 () = 
        Add("2") |> should equal 2

[<Test>]
let Add__two_parameters__returns_sum () = 
        Add("1,2") |> should equal 3

[<Test>]
let Add__two_parameters__returns_sum_01 () = 
        Add("2,2") |> should equal 4

[<Test>]
let Add_multiple_parameters__returns_sum() = 
        Add("1,2,3") |> should equal 6

[<Test>]
let New_line_can_be_used_as_delimeter_instead_of_comma() =
    Add("1\n2,3") |> should equal 6

[<Test>]
let New_line_can_be_used_as_exclusive_delimeter() =
    Add("1\n2\n3") |> should equal 6

[<Test>]
let You_can_specify_delimiter_at_the_beginning_of_the_input_string() =
    Add(";//1;2") |> should equal 3

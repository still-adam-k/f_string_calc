module Tests

open System
open FsUnit
open FsCheck
open NUnit.Framework
open Swensen.Unquote


let Add input = 
    match input with
    | "" -> 0
    | _ when input.Contains(",") -> 
        [for n in input.Split(',') -> Int32.Parse n ] 
        |>  List.toArray 
        |> Array.sum 
    | _ -> Int32.Parse(input)

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

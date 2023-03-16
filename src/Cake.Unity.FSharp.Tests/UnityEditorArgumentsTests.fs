module Cake.Unity.Tests.UnityEditorArgumentsTests

open System
open Cake.Unity.Arguments
open FSharp.Interop.Dynamic
open FsUnit.TopLevelOperators
open NUnit.Framework
open Cake.Core.IO
open Cake.Unity

let commandLineArguments (arguments : UnityEditorArguments) =
    let apply (arguments : UnityEditorArguments) builder =
        arguments.CustomizeCommandLineArguments (builder, null)
    let render (builder : ProcessArgumentBuilder) = builder.Render()

    () |> ProcessArgumentBuilder |> apply arguments |> render

let setBatchMode value (arguments : UnityEditorArguments) =
    arguments.BatchMode <- value
    arguments

let setExecuteMethod value (arguments : UnityEditorArguments) =
    arguments.ExecuteMethod <- value
    arguments

let setCustomBuildTarget value (arguments : UnityEditorArguments) =
    arguments.CustomBuildTarget <- value
    arguments


[<Test>]
let ``CLI arguments with enabled BatchMode should contain "-batchmode"`` () =
    () |> UnityEditorArguments |> setBatchMode true |> commandLineArguments |> should haveSubstring "-batchmode"

[<Test>]
let ``CLI arguments with custom argument "age" of value 18 should contain "--age=18"`` () =
    let arguments = UnityEditorArguments ()
    arguments.Custom?age <- 18
    arguments |> commandLineArguments |> should haveSubstring "--age=18"

[<Test>]
let ``CLI arguments with custom argument "login" of value "admin" should contain "--login=admin"`` () =
    let arguments = UnityEditorArguments ()
    arguments.Custom?login <- "admin"
    arguments |> commandLineArguments |> should haveSubstring "--login=admin"

[<Test>]
let ``CLI arguments with ExecuteMethod "Game.Build.Run" should contain "-executeMethod Game.Build.Run"`` () =
    () |> UnityEditorArguments |> setExecuteMethod "Game.Build.Run" |> commandLineArguments |> should haveSubstring "-executeMethod Game.Build.Run"

[<Test>]
let ``default BatchMode value should be true`` () =
    UnityEditorArguments().BatchMode |> should equal true

[<Test>]
let ``default Quit value should be true`` () =
    UnityEditorArguments().Quit |> should equal true

[<Test>]
let ``CLI arguments with Custom argument "CustomBuildTarget" of value "PS5" should contain "-buildTarget PS5"`` () =
    () |> UnityEditorArguments |> setCustomBuildTarget "PS5" |> commandLineArguments |> should haveSubstring "-buildTarget PS5"


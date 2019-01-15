module Cake.Unity.Tests.UnityEditorArgumentsTests

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

[<Test>]
let ``CLI arguments with enabled BatchMode should contain "-batchmode"`` () =
    () |> UnityEditorArguments |> setBatchMode true |> commandLineArguments |> should haveSubstring "-batchmode"

[<Test>]
let ``CLI arguments with custom argument "age" equal 18 should contain "--age=18"`` () =
    let arguments = UnityEditorArguments ()
    arguments.Custom?age <- 18
    arguments |> commandLineArguments |> should haveSubstring "--age=18"

[<Test>]
let ``CLI arguments with custom argument "login" equal "admin" should contain "--login=admin"`` () =
    let arguments = UnityEditorArguments ()
    arguments.Custom?login <- "admin"
    arguments |> commandLineArguments |> should haveSubstring "--login=admin"

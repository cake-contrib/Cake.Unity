module Cake.Unity.Tests.UnityEditorArgumentsTests

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

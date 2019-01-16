module Cake.Unity.Tests.UnityVersionTests

open FsUnit.TopLevelOperators
open NUnit.Framework
open Cake.Unity.Version

let stage (version : UnityVersion) = version.Stage

[<Test>]
let ``Unity version with 'a' suffix should be in Alpha stage`` () =
    (2019, 1, 0, 'a', 9) |> UnityVersion |> stage |> should equal UnityReleaseStage.Alpha

[<Test>]
let ``Unity version with 'b' suffix should be in Beta stage`` () =
    (2018, 3, 0, 'b', 2) |> UnityVersion |> stage |> should equal UnityReleaseStage.Beta

[<Test>]
let ``Unity version with 'p' suffix should be in Patch stage`` () =
    (2017, 2, 2, 'p', 1) |> UnityVersion |> stage |> should equal UnityReleaseStage.Patch

[<Test>]
let ``Unity version with 'f' suffix should be in Final stage`` () =
    (2018, 2, 20, 'f', 1) |> UnityVersion |> stage |> should equal UnityReleaseStage.Final

[<Test>]
let ``Unity version without suffix should be in Unknown stage`` () =
    (2018, 4, 13) |> UnityVersion |> stage |> should equal UnityReleaseStage.Unknown

[<Test>]
let ``Unity version with unknown suffix should be in Unknown stage`` () =
    (2022, 2, 2, 'x', 2) |> UnityVersion |> stage |> should equal UnityReleaseStage.Unknown

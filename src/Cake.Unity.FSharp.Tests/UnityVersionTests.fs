module UnityVersionTests

open FsUnit.TopLevelOperators
open NUnit.Framework
open Cake.Unity.Version

let stage (version : UnityVersion) = version.Stage

[<Test>]
let ``Unity version with 'a' suffix should be in Alpha stage`` () =
    (2019, 1, 0, 'a', 9) |> UnityVersion |> stage |> should equal UnityReleaseStage.Alpha

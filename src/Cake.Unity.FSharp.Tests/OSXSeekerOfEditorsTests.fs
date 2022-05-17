module Cake.Unity.Tests.OSXSeekerOfEditorsTests

open Cake.Core
open Cake.Core.IO
open NUnit.Framework
open Cake.Unity.SeekersOfEditors
open Cake.Testing

let seek (seeker : OSXSeekerOfEditors) = seeker.Seek ()

[<Test>]
let ``seek should not fail on real file system`` () =
    let environment = FakeEnvironment PlatformFamily.OSX
    let fileSystem = FileSystem ()
    let globber = Globber (fileSystem, environment)
    let log = FakeLog ()

    (environment, globber, log, fileSystem)
    |> OSXSeekerOfEditors
    |> seek
    |> ignore

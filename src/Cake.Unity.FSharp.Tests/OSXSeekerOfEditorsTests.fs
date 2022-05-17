module Cake.Unity.Tests.OSXSeekerOfEditorsTests

open Cake.Core
open Cake.Core.IO
open NUnit.Framework
open Cake.Unity.SeekersOfEditors
open Cake.Testing
open System.Runtime.InteropServices

let seek (seeker : OSXSeekerOfEditors) = seeker.Seek ()
let runningOnWindows = RuntimeInformation.IsOSPlatform OSPlatform.Windows

[<Test>]
let ``seek should not fail on real file system`` () =

    if runningOnWindows then
        Assert.Inconclusive ()

    else
        let environment = FakeEnvironment PlatformFamily.OSX
        let fileSystem = FileSystem ()
        let globber = Globber (fileSystem, environment)
        let log = FakeLog ()

        (environment, globber, log, fileSystem)
        |> OSXSeekerOfEditors
        |> seek
        |> ignore

module Cake.Unity.Tests.InfoPlistParserTests

open FsUnit.TopLevelOperators
open NUnit.Framework
open Cake.Unity.SeekersOfEditors
open System.IO
open System.Text

[<Test>]
let ``Parse Info.plist`` () =

    // arrange
    let xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">
<plist version=""1.0"">
<dict>
	<key>NSAppTransportSecurity</key>
	<dict>
		<key>NSAllowsArbitraryLoads</key>
		<true/>
	</dict>
	<key>CFBundleURLTypes</key>
	<array>
		<dict>
			<key>CFBundleURLName</key>
			<string>Unity Asset Store</string>
			<key>CFBundleURLSchemes</key>
			<array>
				<string>com.unity3d.kharma</string>
			</array>
		</dict>
	</array>
	<key>CFBundleDevelopmentRegion</key>
	<string>English</string>
	<key>CFBundleDocumentTypes</key>
	<array>
		<dict>
			<key>CFBundleTypeExtensions</key>
			<array>
				<string>unity</string>
				<string>unityPackage</string>
			</array>
			<key>CFBundleTypeIconFile</key>
			<string>UnityDocumentIcon</string>
			<key>CFBundleTypeOSTypes</key>
			<array>
				<string>????</string>
			</array>
			<key>CFBundleTypeRole</key>
			<string>Editor</string>
		</dict>
	</array>
	<key>CFBundleExecutable</key>
	<string>Unity</string>
	<key>CFBundleGetInfoString</key>
	<string>Unity version 2017.4.25f1 (9cba1c3a94f1). (c) 2019 Unity Technologies ApS. All rights reserved.</string>
	<key>CFBundleIconFile</key>
	<string>UnityAppIcon</string>
	<key>CFBundleIdentifier</key>
	<string>com.unity3d.UnityEditor5.x</string>
	<key>CFBundleInfoDictionaryVersion</key>
	<string>6.0</string>
	<key>CFBundleName</key>
	<string>Unity</string>
	<key>CFBundlePackageType</key>
	<string>APPL</string>
	<key>CFBundleShortVersionString</key>
	<string>Unity version 2017.4.25f1</string>
	<key>CFBundleSignature</key>
	<string>UNED</string>
	<key>CFBundleVersion</key>
	<string>2017.4.25f1</string>
	<key>NSMainNibFile</key>
	<string>MainMenuNew</string>
	<key>NSPrincipalClass</key>
	<string>EditorApplicationPrincipalClass</string>
	<key>UnityBuildNumber</key>
	<string>9cba1c3a94f1</string>
	<key>LSMinimumSystemVersion</key>
	<string>10.9.0</string>
</dict>
</plist>
"
    let xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xml))
    let parser = new InfoPlistParser()

    // act
    let version = parser.UnityVersionFromInfoPlist(xmlStream)

    // assert
    version |> should equal "2017.4.25f1"

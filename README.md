Cake.Unity
==========

Unity build support for [Cake](https://github.com/cake-build/cake).

This is currently a work in progress and only supports building for Windows.   
The API is not in any way final and will change.

Examples
-------
```csharp
#addin Cake.Unity

Task("Find-Unity-Editors").Does(() =>
{
    foreach (var editor in FindUnityEditors())
        Information("Found Unity Editor {0} at path {1}", editor.Version, editor.Path);
});

Task("Find-Unity-Editor-2018.3").Does(() =>
{
    var editor = FindUnityEditor(2018, 3);
    if (editor != null)
        Information("Found Unity Editor {0} at path {1}", editor.Version, editor.Path);
    else
        Warning("Cannot find Unity Editor 2018.3");
});

Task("Find-Latest-Unity-Editor").Does(() =>
{
    var editor = FindUnityEditor();
    if (editor != null)
        Information("Found Unity Editor {0} at path {1}", editor.Version, editor.Path);
    else
        Warning("Cannot find Unity Editor");
});

Task("Default")
    .IsDependentOn("Find-Unity-Editors")
    .IsDependentOn("Find-Unity-Editor-2018.3")
    .IsDependentOn("Find-Latest-Unity-Editor");

RunTarget("Default");
```

Older API Example
-------

```csharp
#r "tools/Cake.Unity.dll"

Task("Build")
	.Description("Builds the game.")
	.Does(() =>
{
	var projectPath = @"C:\Project\UnityGame";
	var outputPath = @"C:\Output\Game.exe";

	// Build for Windows (x86)
	UnityBuild(projectPath, new WindowsPlatform(outputPath) {
		NoGraphics = true });

	// Build for Windows (x64)
	UnityBuild(projectPath, new WindowsPlatform(outputPath) {
		PlatformTarget = UnityPlatformTarget.x64 });
});

RunTarget("Build");
```

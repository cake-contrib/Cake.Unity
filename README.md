Cake.Unity
==========

Unity build support for [Cake](https://github.com/cake-build/cake).

This is currently a work in progress and only supports building for Windows.   
The API is not in any way final and will change.

Example
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
	Unity(projectPath, new WindowsPlatform(outputPath) { NoGraphics = true });

	// Build for Windows (x64)
	Unity(projectPath, new WindowsPlatform(true, outputPath) { NoGraphics = true });
});

RunTarget("Build");
```
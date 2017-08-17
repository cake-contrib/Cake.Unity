Cake.Unity
==========

Unity build support for [Cake](https://github.com/cake-build/cake).

This is currently a work in progress, the API is not in any way final and will change.

Example
-------

```csharp
#reference "tools/Cake.Unity.dll"

Task("Unity")
	.Does(() =>
{
	var projectPath = @"C:\Project\UnityGame";

    // Build for Windows (x86)
    UnityBuild(projectPath, UnityBuildPlayer.Windows, @"C:\Output\Game.exe");

    // Execute script method
    UnityExecuteMethod(projectPath, "Class.Method");
    
    // Export .unitypackage
    UnityExportPackage(projectPath, "Result.unitypackage", @"Assets\ToExport");
    
    // Import .unitypackage
    UnityExportPackage(projectPath, "Import.unitypackage");
});

RunTarget("Unity");
```

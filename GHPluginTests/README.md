# Adding Testing to Grasshopper Plugin from template
 - References:
	- [Nugets](https://www.nuget.org/profiles/McNeel)
			- & Associated Project Files
	- [Plugin]()
	- [Test Procedure](https://github.com/mcneel/Rhino.Testing?tab=readme-ov-file#settin-up-your-project)
	- [Test Example Reference Project]()
 - Setup:
	- HP ZBook Fury 12 G9
	- 12thGen i9, 32GB Ram
	- Win 11 Enterprise
	- VS Professional
		- VS Testing with verbose logging
		- ReSharper Testing Addon
	- RH Student License
## Workflow
 1. Start Git source tracking
 2. Download [Grasshopper Plugin Template](https://www.nuget.org/api/v2/package/Rhino.Templates/8.0.0)
 3. Start new Project: GHPluginRH8
 		- based on: Grasshopper Assembly for Rhino 3D (C#)
 4. Discovering build errors:
	- ```
		Severity	Code	Description	Project	File	Line	Suppression State	Details
		Error (active)	CS1069	The type name 'Bitmap' could not be found in the namespace 'System.Drawing'. This type has been forwarded to assembly 'System.Drawing.Common, Version=0.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51' Consider adding a reference to that assembly.	GHPluginRH8 (net7.0)	C:\...\GHPluginTests\GHPluginRH8\GHPluginRH8Info.cs	13		
		Error (active)	CS0012	The type 'Bitmap' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Drawing.Common, Version=0.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'.	GHPluginRH8 (net7.0)	C:\...\GHPluginTests\GHPluginRH8\GHPluginRH8Info.cs	13		
		Error (active)	CS1069	The type name 'Bitmap' could not be found in the namespace 'System.Drawing'. This type has been forwarded to assembly 'System.Drawing.Common, Version=0.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51' Consider adding a reference to that assembly.	GHPluginRH8 (net7.0)	C:\...\GHPluginTests\GHPluginRH8\GHPluginRHComponent.cs	143		
		Error (active)	CS0012	The type 'Bitmap' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Drawing.Common, Version=0.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'.	GHPluginRH8 (net7.0)	C:\...\GHPluginTests\GHPluginRH8\GHPluginRHComponent.cs	143		
		Warning (active)	NU1701	Package 'Grasshopper 8.0.23164.14305-wip' was restored using '.NETFramework,Version=v4.6.1, .NETFramework,Version=v4.6.2, .NETFramework,Version=v4.7, .NETFramework,Version=v4.7.1, .NETFramework,Version=v4.7.2, .NETFramework,Version=v4.8, .NETFramework,Version=v4.8.1' instead of the project target framework 'net7.0'. This package may not be fully compatible with your project.	GHPluginRH8 (net48), GHPluginRH8 (net7.0)	C:\...\GHPluginTests\GHPluginRH8\GHPluginRH8.csproj	1		
		Warning (active)	NU1701	Package 'RhinoCommon 8.0.23164.14305-wip' was restored using '.NETFramework,Version=v4.6.1, .NETFramework,Version=v4.6.2, .NETFramework,Version=v4.7, .NETFramework,Version=v4.7.1, .NETFramework,Version=v4.7.2, .NETFramework,Version=v4.8, .NETFramework,Version=v4.8.1' instead of the project target framework 'net7.0'. This package may not be fully compatible with your project.	GHPluginRH8 (net48), GHPluginRH8 (net7.0)	C:\...\GHPluginTests\GHPluginRH8\GHPluginRH8.csproj	1		
		```
	- Solving by adding `System.Drawing.Common`
 5. Debug Testrun
	- Adding Debug Folder: `C:\...\GHPluginTests\GHPluginRH8\bin\Debug\net7.0\` to `GrasshopperDeveloperSettings` in Rhino
	- Opening Grasshopper
	- Searching for Component via `NickName`: `ASpi`
	- Component works as expected
 6. Add NUnit Tests
	- make CreateSpiral public
	- use CSharper to `Create Unit Tests` via context menu
		- Framework: NUnit
		- Test Project: <New Test Project>
		- Projct Name Format: [Project]Tests
		- Namespace: [Namespace].Tests
		- Output: <New Test File>
		- Class Name Format: [Class]Tests
		- Method Name Format: [Method]Tests
		- Code: Assert failure
	- import missing usings
 7. Run test:
	- `[Error] Microsoft.VisualStudio.TestPlatform.ObjectModel.TestPlatformException: Testhost process for source(s) 'C:\...\GHPluginTests\GHPluginRH8Tests1\bin\Debug\net7.0\GHPluginRH8Tests1.dll' exited with error: Failed to create CoreCLR, HRESULT: 0x80070057`
 8. Changing **Test Project** according to [Rhino.Testing Doc](https://github.com/mcneel/Rhino.Testing?tab=readme-ov-file#settin-up-your-project)
  - `GHPluginRH8Tests1.csproj`:
	- comment out:
		- ImplicitUsings, Nullable
	- adding:
		- local RhinoCommon
		- Rhino Directory Variable
		- local RhinoCommon
		- Rhino Testing
		- Testing.Config file to output
	- auto update:
		- Microsoft.NET.Test.Sdk, NUnit, NUnit3TestAdapter
	- manually update all other packages
	- change TargetFramework
		- `net7.0` to `net7.0-windows`
 9. Change **Plugin** to target the same frameworks
  - close and reopen solution
	>suspicion that plugin might not be testable because the project is set up for degugging and .dll creation and might mismatch to work with testing project
 10. Add `Rhino.Testing.Configs.xml` to **Test Project**
  - reference local rhino path
  - load grasshopper
 11. Add `SetupFixture` to **Test Project**
 12. Change any other .csproj and using differences to match the [SimpleNUnitTesting]() Project
  - Failed to create CoreCLR, HRESULT: 0x80070057
		- See [Log](/TestsLog.txt)
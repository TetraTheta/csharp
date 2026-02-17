# NOTE

I'll leave this note here for my future self.

## How about migrating to .NET 10 or further .NET LTS?

Migrating to .NET 10 (or the next LTS version) is possible, but I’m not strongly inclined to do so right now.

Such a migration would require restructuring every project as follows:

* Depends on the .NET 10 Runtime (not self-contained):<br>
  I don't want to bloat the binaries beyond > 5MB. Using the shared runtime is acceptable.
* Limited Native AOT support:<br>
  Native AOT (including dead code elimination / trimming) works very well for console applications.<br>
  However, it is more complicated for WinForm applications.<br>
  WinForm relies heavily on reflection, which makes trimming and dead code elimination difficult.<br>
  I could force it using `<_SuppressWinFormsTrimError>true</_SuppressWinFormsTrimError>`, but that may still result in runtime issues.
* Replace `CommandLineParser`:<br>
  My current usage targets .NET Framework 4.8.<br>
  Although the latest versions of CommandLineParser support .NET Standard (and therefore modern .NET), upgrading or switching to a different parser such as `System.CommandLine` may require significant refactoring.

Potential benefits:

* Faster startup time:<br>
  I’m not entirely certain, but modern .NET versions generally have better cold-start performance compared to .NET Framework 4.8.<br>
  If this holds true for my applications, migrating could provide a noticeable benefit.
* Dead Code Elimination (trimming):<br>
  Modern .NET supports trimming unused framework and dependency code, which can reduce the final binary size.<br>
  This capability does not exist in the .NET Framework.

Note that I must run `dotnet publish` instead of `dotnet build` to get the proper binary for distribution.

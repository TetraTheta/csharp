# Clear Temp

Remove every file and directory under directories declared in `ClearTemp.txt`.

## Content of `ClearTemp.txt`

```plaintext
%LocalAppData%\CrashDumps
%LocalAppData%\Microsoft\Windows\Explorer
%LocalAppData%\Microsoft\Windows\INetCache
%LocalAppData%\Temp
%ProgramData%\temp
%TEMP%
%TMP%
%windir%\SoftwareDistribution\Download
%windir%\Temp
```

Any duplications that point same directory will be silently ignored.

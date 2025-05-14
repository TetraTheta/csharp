# Skip UAC

Skip UAC Prompt by utilizing Task Scheduler

The name of the task will be decided by the name of the config file.<br>
`SkipUAC.exe Test.txt /register` → Task `SkipUAC\Test` will be created or updated.

## Usage

```
SkipUAC.exe <config file> [/register]
```

`config file`: `.txt` file that each line is command-line for program execution.
```plaintext
C:\Windows\explorer.exe /NoUACCheck
"C:\Program Files\Windows Media Player\wmplayer.exe"
```

`/register`: Re-register `config file` forcibly.

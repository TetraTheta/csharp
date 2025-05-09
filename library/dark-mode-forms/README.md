# DarkModeForms

Personal fork of [BlueMystical/Dark-Mode-Forms](https://github.com/BlueMystical/Dark-Mode-Forms)

Original License: GPL-3.0 license

## Syntax

### DarkModeForms

```csharp
private DarkModeCS dm = null;

public Form1()
{
  InitializeComponent();
  dm = new DarkModeCS(this) {
    //[Optional] Choose your preferred color mode here:
    ColorMode = DarkModeCS.DisplayMode.SystemDefault
  };
}
```

### MessageBox

```csharp
try {
  if (Messenger.MessageBox("Hello World!", "You got a Message:", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK) {
    //Do Something here.
  }
} catch (Exception ex) {
  Messenger.MessageBox(ex);
}
```


### InputBox

```csharp
// Definition of a Single Field:
var BoolControl = new KeyValue("Boolean", "true", KeyValue.ValueTypes.Boolean);

// Can Validate User Inputs on the Field:
BoolControl.Validate += (object? _control, KeyValue.ValidateEventArgs _e) => {
  string OldValue = _e.OldValue;
  if (_e.NewValue == "False") {
    //_e.Cancel = true; //<- CAN CANCEL THE MODIFICATION
    _e.ErrorText = "No puede ser Falso!";
  }
};

// Custom Values for 'Dynamic' Fields:
List<KeyValue> Dtypes = new List<KeyValue> {
  new KeyValue("RichText Format", "0"),
  new KeyValue("Plain Text",      "1"),
  new KeyValue("AccountManager",  "2")
};

// Definition of Multiple Fields:
List<KeyValue> _Fields = new List<KeyValue> {
  new KeyValue("String",  "String",   KeyValue.ValueTypes.String),
  new KeyValue("Password","Password", KeyValue.ValueTypes.Password),
  new KeyValue("Integer", "1000",     KeyValue.ValueTypes.Integer),
  new KeyValue("Decimal", "3,141638", KeyValue.ValueTypes.Decimal),
  BoolControl,
  new KeyValue("Dynamic", "1",        KeyValue.ValueTypes.Dynamic, Dtypes),
};

// Dialog Show:
if (Messenger.InputBox("Custom InputBox", "Please Fill the Form:", ref _Fields, MsgIcon.Edit, MessageBoxButtons.OKCancel) == DialogResult.OK) {
  Debug.WriteLine(string.Format("The New Password is: '{0}'", _Fields[0].Value));
}
```

```csharp
// Example of a LoginForm with Password Validation
List<KeyValue> _Fields = new List<KeyValue> {
  new KeyValue("User Name", "user", KeyValue.ValueTypes.String),
  new KeyValue("Password",  string.Empty, KeyValue.ValueTypes.Password)
};

// Can Validate All the Controls before Closing the Dialog:
Messenger.ValidateControls += (object? sender, CancelEventArgs e) => {
  string _userName = _Fields[0].Value;
  string _password = _Fields[1].Value;

  //TODO: Here you should send the User/Password to your BackEnd for Validation
  if (_password != "password") {
    _Fields[1].ErrorText = "Incorrect Password!";
    e.Cancel = true; //<- Prevents the Dialog to be closed until is valid
  }
};

// Dialog Show:
if (Messenger.InputBox("Login", "Please Input your Credentials:", ref _Fields, MsgIcon.Lock, MessageBoxButtons.OKCancel) == DialogResult.OK) {
  Messenger.MessageBox(string.Format("The User '{0}' is Logged!", _Fields[0].Value), "Login Correct!",
  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
}
```

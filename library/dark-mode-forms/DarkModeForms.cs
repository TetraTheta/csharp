using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DarkModeForms {
/// <summary>This tries to automatically apply Windows Dark Mode (if enabled) to a Form.
/// <para>Author: BlueMystic (bluemystic.play@gmail.com)  2024</para>
/// Source <see href="https://github.com/BlueMystical/Dark-Mode-Forms"/>
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class DarkModeForms {
  #region Win32 API Declarations

  public struct DwmColorizationColors {
    // ReSharper disable once UnassignedField.Global
    public uint ColorizationColor, ColorizationAfterglow, ColorizationColorBalance, ColorizationAfterglowBalance, ColorizationBlurBalance, ColorizationGlassReflectionIntensity, ColorizationOpaqueBlend;
  }

  [Flags]
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  [SuppressMessage("ReSharper", "UnusedMember.Local")]
  private enum Dwmwindowattribute : uint {
    /// <summary>
    /// Use with DwmGetWindowAttribute. Discovers whether non-client rendering is enabled. The retrieved value is of type BOOL. TRUE if non-client rendering is enabled; otherwise, FALSE.
    /// </summary>
    DWMWA_NCRENDERING_ENABLED = 1,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Sets the non-client rendering policy. The pvAttribute parameter points to a value from the DWMNCRENDERINGPOLICY enumeration.
    /// </summary>
    DWMWA_NCRENDERING_POLICY,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Enables or forcibly disables DWM transitions. The pvAttribute parameter points to a value of type BOOL. TRUE to disable transitions, or FALSE to enable transitions.
    /// </summary>
    DWMWA_TRANSITIONS_FORCEDISABLED,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Enables content rendered in the non-client area to be visible on the frame drawn by DWM. The pvAttribute parameter points to a value of type BOOL. TRUE to enable content rendered in the non-client area to be visible on the frame; otherwise, FALSE.
    /// </summary>
    DWMWA_ALLOW_NCPAINT,

    /// <summary>
    /// Use with DwmGetWindowAttribute. Retrieves the bounds of the caption button area in the window-relative space. The retrieved value is of type RECT. If the window is minimized or otherwise not visible to the user, then the value of the RECT retrieved is undefined. You should check whether the retrieved RECT contains a boundary that you can work with, and if it doesn't then you can conclude that the window is minimized or otherwise not visible.
    /// </summary>
    DWMWA_CAPTION_BUTTON_BOUNDS,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies whether non-client content is right-to-left (RTL) mirrored. The pvAttribute parameter points to a value of type BOOL. TRUE if the non-client content is right-to-left (RTL) mirrored; otherwise, FALSE.
    /// </summary>
    DWMWA_NONCLIENT_RTL_LAYOUT,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Forces the window to display an iconic thumbnail or peek representation (a static bitmap), even if a live or snapshot representation of the window is available. This value is normally set during a window's creation, and not changed throughout the window's lifetime. Some scenarios, however, might require the value to change over time. The pvAttribute parameter points to a value of type BOOL. TRUE to require a iconic thumbnail or peek representation; otherwise, FALSE.
    /// </summary>
    DWMWA_FORCE_ICONIC_REPRESENTATION,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Sets how Flip3D treats the window. The pvAttribute parameter points to a value from the DWMFLIP3DWINDOWPOLICY enumeration.
    /// </summary>
    DWMWA_FLIP3D_POLICY,

    /// <summary>
    /// Use with DwmGetWindowAttribute. Retrieves the extended frame bounds rectangle in screen space. The retrieved value is of type RECT.
    /// </summary>
    DWMWA_EXTENDED_FRAME_BOUNDS,

    /// <summary>
    /// Use with DwmSetWindowAttribute. The window will provide a bitmap for use by DWM as an iconic thumbnail or peek representation (a static bitmap) for the window. DWMWA_HAS_ICONIC_BITMAP can be specified with DWMWA_FORCE_ICONIC_REPRESENTATION. DWMWA_HAS_ICONIC_BITMAP normally is set during a window's creation and not changed throughout the window's lifetime. Some scenarios, however, might require the value to change over time. The pvAttribute parameter points to a value of type BOOL. TRUE to inform DWM that the window will provide an iconic thumbnail or peek representation; otherwise, FALSE. Windows Vista and earlier: This value is not supported.
    /// </summary>
    DWMWA_HAS_ICONIC_BITMAP,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Do not show peek preview for the window. The peek view shows a full-sized preview of the window when the mouse hovers over the window's thumbnail in the taskbar. If this attribute is set, hovering the mouse pointer over the window's thumbnail dismisses peek (in case another window in the group has a peek preview showing). The pvAttribute parameter points to a value of type BOOL. TRUE to prevent peek functionality, or FALSE to allow it. Windows Vista and earlier: This value is not supported.
    /// </summary>
    DWMWA_DISALLOW_PEEK,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Prevents a window from fading to a glass sheet when peek is invoked. The pvAttribute parameter points to a value of type BOOL. TRUE to prevent the window from fading during another window's peek, or FALSE for normal behavior. Windows Vista and earlier: This value is not supported.
    /// </summary>
    DWMWA_EXCLUDED_FROM_PEEK,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Cloaks the window such that it is not visible to the user. The window is still composed by DWM. Using with DirectComposition: Use the DWMWA_CLOAK flag to cloak the layered child window when animating a representation of the window's content via a DirectComposition visual that has been associated with the layered child window. For more details on this usage case, see How to animate the bitmap of a layered child window. Windows 7 and earlier: This value is not supported.
    /// </summary>
    DWMWA_CLOAK,

    /// <summary>
    /// Use with DwmGetWindowAttribute. If the window is cloaked, provides one of the following values explaining why. DWM_CLOAKED_APP (value 0x0000001). The window was cloaked by its owner application. DWM_CLOAKED_SHELL(value 0x0000002). The window was cloaked by the Shell. DWM_CLOAKED_INHERITED(value 0x0000004). The cloak value was inherited from its owner window. Windows 7 and earlier: This value is not supported.
    /// </summary>
    DWMWA_CLOAKED,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Freeze the window's thumbnail image with its current visuals. Do no further live updates on the thumbnail image to match the window's contents. Windows 7 and earlier: This value is not supported.
    /// </summary>
    DWMWA_FREEZE_REPRESENTATION,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Enables a non-UWP window to use host backdrop brushes. If this flag is set, then a Win32 app that calls Windows::UI::Composition APIs can build transparency effects using the host backdrop brush (see Compositor.CreateHostBackdropBrush). The pvAttribute parameter points to a value of type BOOL. TRUE to enable host backdrop brushes for the window, or FALSE to disable it. This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_USE_HOSTBACKDROPBRUSH,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Allows the window frame for this window to be drawn in dark mode colors when the dark mode system setting is enabled. For compatibility reasons, all windows default to light mode regardless of the system setting. The pvAttribute parameter points to a value of type BOOL. TRUE to honor dark mode for the window, FALSE to always use light mode. This value is supported starting with Windows 10 Build 17763.
    /// </summary>
    DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Allows the window frame for this window to be drawn in dark mode colors when the dark mode system setting is enabled. For compatibility reasons, all windows default to light mode regardless of the system setting. The pvAttribute parameter points to a value of type BOOL. TRUE to honor dark mode for the window, FALSE to always use light mode. This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_USE_IMMERSIVE_DARK_MODE = 20,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies the rounded corner preference for a window. The pvAttribute parameter points to a value of type DWM_WINDOW_CORNER_PREFERENCE. This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_WINDOW_CORNER_PREFERENCE = 33,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies the color of the window border. The pvAttribute parameter points to a value of type COLORREF. The app is responsible for changing the border color according to state changes, such as a change in window activation. This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_BORDER_COLOR,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies the color of the caption. The pvAttribute parameter points to a value of type COLORREF. This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_CAPTION_COLOR,

    /// <summary>
    /// Use with DwmSetWindowAttribute. Specifies the color of the caption text. The pvAttribute parameter points to a value of type COLORREF. This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_TEXT_COLOR,

    /// <summary>
    /// Use with DwmGetWindowAttribute. Retrieves the width of the outer border that the DWM would draw around this window. The value can vary depending on the DPI of the window. The pvAttribute parameter points to a value of type UINT. This value is supported starting with Windows 11 Build 22000.
    /// </summary>
    DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,

    /// <summary>
    /// The maximum recognized DWMWINDOWATTRIBUTE value, used for validation purposes.
    /// </summary>
    DWMWA_LAST,
  }

  [Flags]
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  public enum DwmWindowCornerPreference {
    DWMWCP_DEFAULT = 0,
    DWMWCP_DONOTROUND = 1,
    DWMWCP_ROUND = 2,
    DWMWCP_ROUNDSMALL = 3
  }

  [Serializable, StructLayout(LayoutKind.Sequential)]
  public struct Rect {
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;

    public Rectangle ToRectangle() {
      return Rectangle.FromLTRB(Left, Top, Right, Bottom);
    }
  }

  public const int EmSetCueBanner = 5377;
  public const int WmSettingsChange = 0x001A;
  public const int WmThemeChanged = 0x031A;
  public const int GwlpWndProc = -4;

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

  [DllImport("dwmapi.dll")]
  public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

  [DllImport("dwmapi.dll")]
  public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out Rect pvAttribute, int cbAttribute);

  [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
  private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

  [DllImport("dwmapi.dll", EntryPoint = "#127")]
  public static extern void DwmGetColorizationParameters(ref DwmColorizationColors colors);

  [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
  private static extern IntPtr CreateRoundRectRgn(
    int nLeftRect, // x-coordinate of upper-left corner
    int nTopRect, // y-coordinate of upper-left corner
    int nRightRect, // x-coordinate of lower-right corner
    int nBottomRect, // y-coordinate of lower-right corner
    int nWidthEllipse, // height of ellipse
    int nHeightEllipse // width of ellipse
  );

  [DllImport("user32.dll")]
  // ReSharper disable once UnusedMember.Local
  private static extern IntPtr GetDC(IntPtr hwnd);

  [DllImport("user32.dll")]
  // ReSharper disable once UnusedMember.Local
  private static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);

  // ReSharper disable once UnusedMember.Local
  private static IntPtr GetHeaderControl(ListView list) {
    const int lvmGetHeader = 0x1000 + 31;
    return SendMessage(list.Handle, lvmGetHeader, IntPtr.Zero, "");
  }

  [UnmanagedFunctionPointer(CallingConvention.StdCall)]
  public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

  // This helper static method is required because the 32-bit version of user32.dll does not contain this API
  // (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
  // to the correct function (GetWindowLong in 32-bit mode and GetWindowLongPtr in 64-bit mode)
  public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong) {
    return IntPtr.Size == 8 ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong) : new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
  }

  [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
  private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

  [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
  private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

  //		If that doesn't work, the following signature can be used alternatively.
  [DllImport("user32.dll")]
  // ReSharper disable once UnusedMember.Local
  private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

  //[DllImport("user32.dll", SetLastError = true)]
  //private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

  [DllImport("user32.dll", SetLastError = true)]
  private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

  #endregion Win32 API Declarations

  #region Private Static Members

  /// <summary>
  /// Stores additional info related to the Controls
  /// </summary>
  private static readonly ControlStatusStorage ControlStatusStorage = new ControlStatusStorage();

  /// <summary>
  /// stores the event handler reference in order to prevent its uncontrolled multiple addition
  /// </summary>
  private static ControlEventHandler _ownerFormControlAdded;

  /// <summary>
  /// stores the event handler reference in order to prevent its uncontrolled multiple addition
  /// </summary>
  private static EventHandler _controlHandleCreated;

  /// <summary>
  /// stores the event handler reference in order to prevent its uncontrolled multiple addition
  /// </summary>
  private static ControlEventHandler _controlControlAdded;

  private IntPtr _originalWndProc;

  // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
  private WndProc _newWndProcDelegate;
#pragma warning disable CS0169 // 필드가 사용되지 않습니다
  private IntPtr _formHandle;
#pragma warning restore CS0169 // 필드가 사용되지 않습니다
  private bool _applyingTheme; // Flag to prevent recursion
  private bool _isFormLoaded;

  #endregion

  #region Public Members

  public enum DisplayMode {
    /// <summary>Uses the Color Mode of the System, set by the User in Windows Settings.</summary>
    SystemDefault,

    /// <summary>Forces to use Clear Mode</summary>
    ClearMode,

    /// <summary>Forces to use Dark Mode</summary>
    DarkMode
  }

  private DisplayMode _colorMode = DisplayMode.SystemDefault;

  /// <summary>Gets or Sets the Display color mode applied to the Form and Controls.</summary>
  public DisplayMode ColorMode {
    get => _colorMode;
    set {
      if (value == _colorMode) return;
      _colorMode = value;
      ApplyColorMode();
      if (_isFormLoaded) { ApplyTheme(); }
    }
  }

  ///// <summary>[Read Only] 'true' if Dark Mode Color is applied to the Form.</summary>
  public bool IsDarkMode { get; private set; }

  /// <summary>Option to re-colorize all Icons in Toolbars and Menus.</summary>
  public bool ColorizeIcons { get; set; }

  /// <summary>Option to make all Panels Borders Rounded</summary>
  public bool RoundedPanels { get; set; }

  /// <summary>The Parent form for them all.</summary>
  public Form OwnerForm { get; set; }

  /// <summary>The Parent components.</summary>
  public ComponentCollection Components { get; set; }

  /// <summary>Windows Colors. Can be customized.</summary>
  public OsThemeColors OScolors { get; private set; }

  #endregion Public Members

  #region Constructors

  /// <summary>This tries to automatically apply Windows Dark Mode (if enabled) to a Form.</summary>
  /// <param name="form">The Form to become Dark</param>
  /// <param name="newWndProcDelegate"></param>
  /// <param name="colorizeIcons">[OPTIONAL] re-colorize all Icons in Toolbars and Menus.</param>
  /// <param name="roundedPanels">[OPTIONAL] make all Panels Borders Rounded</param>
  public DarkModeForms(Form form, WndProc newWndProcDelegate, bool colorizeIcons = true, bool roundedPanels = false) {
    //Sets the Properties:
    OwnerForm = form;
    _newWndProcDelegate = newWndProcDelegate;
    Components = null;
    ColorizeIcons = colorizeIcons;
    RoundedPanels = roundedPanels;
    ApplyColorMode();
    if (_originalWndProc == IntPtr.Zero) {
      form.HandleCreated += (sender, e) => {
        HandleRef handleRef = new HandleRef(form, form.Handle);
        _newWndProcDelegate = CustomWndProc;
        _originalWndProc = SetWindowLongPtr(handleRef, GwlpWndProc, Marshal.GetFunctionPointerForDelegate(_newWndProcDelegate));
      };
    }

    // This Fires after the normal 'Form_Load' event
    form.Load += (sender, e) => {
      ApplyTheme();
      _isFormLoaded = true;
    };
  }

  private void ApplyColorMode() {
    IsDarkMode = ColorMode == DisplayMode.DarkMode;
    if (ColorMode == DisplayMode.SystemDefault) {
      IsDarkMode = IsSystemDarkMode(); //<- Gets the current color mode from Windows
    }

    OScolors = GetSystemColors(IsDarkMode ? 0 : 1);
  }

  private IntPtr CustomWndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam) {
    // Handle the WM_THEMECHANGED message
    // Debug.WriteLine($"Message Code: {msg}"); // <-- should not be enabled in production as it slows everything down!
    if (msg != WmSettingsChange || _applyingTheme) return CallWindowProc(_originalWndProc, hWnd, msg, wParam, lParam);
    _applyingTheme = true; // Set the flag to prevent recursion
    ApplyTheme();
    _applyingTheme = false; // Reset the flag

    // Call the original WndProc
    return CallWindowProc(_originalWndProc, hWnd, msg, wParam, lParam);
  }

  public bool IsSystemDarkMode() {
    return GetWindowsColorMode() <= 0;
  }

  #endregion Constructors

  #region Public Methods

  /// <summary>Apply the Theme into the Window and all its controls.</summary>
  private void ApplyTheme() {
    try {
      if (OScolors == null) return;
      //Apply Window's Dark Mode to the Form's Title bar:
      ApplySystemDarkTheme(OwnerForm, IsDarkMode);

      OwnerForm.BackColor = OScolors.Background;
      OwnerForm.ForeColor = OScolors.TextInactive;

      if (OwnerForm?.Controls != null) {
        foreach (Control control in OwnerForm.Controls) { ThemeControl(control); }

        if (_ownerFormControlAdded == null)
          _ownerFormControlAdded = (sender, e) => { ThemeControl(e.Control); };
        OwnerForm.ControlAdded -= _ownerFormControlAdded; //prevent uncontrolled multiple addition
        OwnerForm.ControlAdded += _ownerFormControlAdded;
      }

      if (Components == null) return;
      foreach (ContextMenuStrip item in Components.OfType<ContextMenuStrip>())
        ThemeControl(item);
    } catch (Exception ex) { MessageBox.Show(ex.Message + ex.StackTrace, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
  }

  /// <summary>Recursively apply the Colors from 'OScolors' to the Control and all its childs.</summary>
  /// <param name="control">Can be a Form or any Winforms Control.</param>
  public void ThemeControl(Control control) {
    ControlStatusInfo info = ControlStatusStorage.GetControlStatusInfo(control);
    if (info != null) {
      //we already have some information about this Control

      //if the user chose to skip the control, exit
      if (info.IsExcluded) return;

      //prevent applying a theme multiple times to the same control
      //without this, it happens at least is some MDI forms
      //if the Control already has the current theme, exit (otherwise we are going to re-theme it)
      if (info.LastThemeAppliedIsDark == IsDarkMode) return;

      //we remember it will soon have the current theme
      info.LastThemeAppliedIsDark = IsDarkMode;
    } else {
      //this is the first time we see this Control
      //we remember it will soon have the current theme
      ControlStatusStorage.RegisterProcessedControl(control, IsDarkMode);
    }

    BorderStyle bStyle = (IsDarkMode ? BorderStyle.FixedSingle : BorderStyle.Fixed3D);
    // ReSharper disable once UnusedVariable
    FlatStyle fStyle = (IsDarkMode ? FlatStyle.Flat : FlatStyle.Standard);

    if (_controlHandleCreated == null) _controlHandleCreated = (sender, e) => { ApplySystemDarkTheme((Control)sender, IsDarkMode); };
    control.HandleCreated -= _controlHandleCreated; //prevent uncontrolled multiple addition
    control.HandleCreated += _controlHandleCreated;

    if (_controlControlAdded == null) _controlControlAdded = (sender, e) => { ThemeControl(e.Control); };
    control.ControlAdded -= _controlControlAdded; //prevent uncontrolled multiple addition
    control.ControlAdded += _controlControlAdded;

    string mode = IsDarkMode ? "DarkMode_Explorer" : "ClearMode_Explorer";
    SetWindowTheme(control.Handle, mode, null); //<- Attempts to apply Dark Mode using Win32 API if available.

    control.GetType().GetProperty("BackColor")?.SetValue(control, OScolors.Control);
    control.GetType().GetProperty("ForeColor")?.SetValue(control, OScolors.TextActive);

    /* Here we Finetune individual Controls  */
    if (control is Label lbl) {
      control.GetType().GetProperty("BackColor")?.SetValue(control, control.Parent.BackColor);
      control.GetType().GetProperty("BorderStyle")?.SetValue(control, BorderStyle.None);
      control.Paint += (sender, e) => {
        if (control.Enabled || !IsDarkMode) return;
        e.Graphics.Clear(control.Parent.BackColor);
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

        using (Brush b = new SolidBrush(control.ForeColor)) {
          //StringFormat sf = lbl.CreateStringFormat();
          MethodInfo mi = lbl.GetType().GetMethod("CreateStringFormat", BindingFlags.NonPublic | BindingFlags.Instance);
          var sf = mi?.Invoke(lbl, new object[] { }) as StringFormat;
          e.Graphics.DrawString(lbl.Text, lbl.Font, b, new PointF(1, 0), sf);
        }
      };
    }

    switch (control) {
      case LinkLabel _:
        control.GetType().GetProperty("LinkColor")?.SetValue(control, OScolors.AccentLight);
        control.GetType().GetProperty("VisitedLinkColor")?.SetValue(control, OScolors.Primary);
        break;
      case TextBox _:
        //SetRoundBorders(tb, 4, OScolors.SurfaceDark, 1);
        control.GetType().GetProperty("BorderStyle")?.SetValue(control, bStyle);
        break;
      case NumericUpDown _:
        //Mode = IsDarkMode ? "DarkMode_CFD" : "ClearMode_CFD";
        mode = IsDarkMode ? "DarkMode_ItemsView" : "ClearMode_ItemsView";
        SetWindowTheme(control.Handle, mode, null);
        break;
      case Button button1: {
        button1.FlatStyle = IsDarkMode ? FlatStyle.Flat : FlatStyle.Standard;
        button1.FlatAppearance.CheckedBackColor = OScolors.Accent;
        button1.BackColor = OScolors.Control;
        button1.FlatAppearance.BorderColor = (OwnerForm.AcceptButton == button1) ? OScolors.Accent : OScolors.Control;
        break;
      }
      case ComboBox comboBox: {
        // Fixing a glitch that makes all instances of the ComboBox showing as having a Selected value, even when they dont
        if (comboBox.DropDownStyle != ComboBoxStyle.DropDownList) { comboBox.SelectionStart = comboBox.Text.Length; }

        //control.BeginInvoke(() => (control as ComboBox).SelectionLength = 0);
        control.BeginInvoke(
          new Action(() => {
              if (!((ComboBox)control).DropDownStyle.Equals(ComboBoxStyle.DropDownList))
                ((ComboBox)control).SelectionLength = 0;
            }
          )
        );

        // Fixes a glitch showing the Combo Backgroud white when the control is Disabled:
        if (!control.Enabled && IsDarkMode) { comboBox.DropDownStyle = ComboBoxStyle.DropDownList; }

        // Apply Windows Color Mode:
        mode = IsDarkMode ? "DarkMode_CFD" : "ClearMode_CFD";
        SetWindowTheme(control.Handle, mode, null);
        break;
      }
      case Panel panel1: {
        // Process the panel within the container
        panel1.BackColor = OScolors.Background;
        panel1.BorderStyle = BorderStyle.None;
        if (!(panel1.Parent is TabControl) || !(panel1.Parent is TableLayoutPanel)) {
          if (RoundedPanels) { SetRoundBorders(panel1, 6, OScolors.SurfaceDark, 1); }
        }

        break;
      }
      case GroupBox _:
        control.GetType().GetProperty("BackColor")?.SetValue(control, control.Parent.BackColor);
        control.GetType().GetProperty("ForeColor")?.SetValue(control, OScolors.TextActive);
        control.Paint += (sender, e) => {
          if (control.Enabled || !IsDarkMode) return;
          var radio = (sender as GroupBox);
          Brush b = new SolidBrush(control.ForeColor);
          e.Graphics.DrawString(radio?.Text, radio?.Font, b, new PointF(6, 0));
        };
        break;
    }

    switch (control) {
      case TableLayoutPanel _:
        control.GetType().GetProperty("BackColor")?.SetValue(control, control.Parent.BackColor);
        control.GetType().GetProperty("ForeColor")?.SetValue(control, OScolors.TextInactive);
        control.GetType().GetProperty("BorderStyle")?.SetValue(control, BorderStyle.None);
        break;
      case TabControl tabControl: {
        TabControl tab = tabControl;
        tab.Appearance = TabAppearance.Normal;
        tab.DrawMode = TabDrawMode.OwnerDrawFixed;
        tab.DrawItem += (sender, e) => {
          //Draw the background of the main control
          using (var backColor = new SolidBrush(tab.Parent.BackColor)) { e.Graphics.FillRectangle(backColor, tab.ClientRectangle); }

          using (Brush tabBack = new SolidBrush(OScolors.Surface)) {
            for (int i = 0; i < tab.TabPages.Count; i++) {
              TabPage tabPage = tab.TabPages[i];
              tabPage.BackColor = OScolors.Surface;
              tabPage.BorderStyle = BorderStyle.FixedSingle;

              tabPage.ControlAdded -= TabPageAdded; //prevent uncontrolled multiple addition
              tabPage.ControlAdded += TabPageAdded;

              Rectangle tabRect = tab.GetTabRect(i);

              bool isSelected = (tab.SelectedIndex == i);
              if (isSelected) {
                e.Graphics.FillRectangle(tabBack, tabRect);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, OScolors.TextActive);
              } else { TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, OScolors.TextInactive); }
            }
          }
        };
        break;
      }
      //if (control is FlatTabControl)
      //{
      //	control.GetType().GetProperty("BackColor")?.SetValue(control, OScolors.Background);
      //	control.GetType().GetProperty("TabColor")?.SetValue(control, OScolors.Surface);
      //	control.GetType().GetProperty("SelectTabColor")?.SetValue(control, OScolors.Control);
      //	control.GetType().GetProperty("SelectedForeColor")?.SetValue(control, OScolors.TextActive);
      //	control.GetType().GetProperty("ForeColor")?.SetValue(control, OScolors.TextInactive);
      //	control.GetType().GetProperty("LineColor")?.SetValue(control, OScolors.Background);
      //}
      case PictureBox _:
        control.GetType().GetProperty("BackColor")?.SetValue(control, control.Parent.BackColor);
        control.GetType().GetProperty("ForeColor")?.SetValue(control, OScolors.TextActive);
        control.GetType().GetProperty("BorderStyle")?.SetValue(control, BorderStyle.None);
        break;
      case CheckBox _:
        control.GetType().GetProperty("BackColor")?.SetValue(control, control.Parent.BackColor);
        control.ForeColor = control.Enabled ? OScolors.TextActive : OScolors.TextInactive;
        control.Paint += (sender, e) => {
          if (control.Enabled || !IsDarkMode) return;
          var radio = (sender as CheckBox);
          Brush b = new SolidBrush(control.ForeColor);
          e.Graphics.DrawString(radio?.Text, radio?.Font, b, new PointF(16, 0));
        };
        break;
      case RadioButton _:
        control.GetType().GetProperty("BackColor")?.SetValue(control, control.Parent.BackColor);
        control.ForeColor = control.Enabled ? OScolors.TextActive : OScolors.TextInactive;
        control.Paint += (sender, e) => {
          if (control.Enabled || !IsDarkMode) return;
          var radio = (sender as RadioButton);
          Brush b = new SolidBrush(control.ForeColor);
          e.Graphics.DrawString(radio?.Text, radio?.Font, b, new PointF(16, 0));
        };
        break;
      case MenuStrip strip:
        strip.RenderMode = ToolStripRenderMode.Professional;
        strip.Renderer = new MyRenderer(new CustomColorTable(OScolors), ColorizeIcons) { MyColors = OScolors };
        break;
    }

    switch (control) {
      case ToolStrip strip:
        strip.RenderMode = ToolStripRenderMode.Professional;
        strip.Renderer = new MyRenderer(new CustomColorTable(OScolors), ColorizeIcons) { MyColors = OScolors };
        break;
      //<- empty area around ToolStrip
      case ToolStripPanel _:
        control.GetType().GetProperty("BackColor")?.SetValue(control, control.Parent.BackColor);
        break;
    }

    if (control is ToolStripDropDown down) {
      down.Opening -= Tsdd_Opening; //just to make sure
      down.Opening += Tsdd_Opening;
    }

    if (control is ToolStripDropDownMenu menu) {
      menu.Opening -= Tsdd_Opening; //just to make sure
      menu.Opening += Tsdd_Opening;
    }

    switch (control) {
      case ContextMenuStrip strip:
        strip.RenderMode = ToolStripRenderMode.Professional;
        strip.Renderer = new MyRenderer(new CustomColorTable(OScolors), ColorizeIcons) { MyColors = OScolors };
        strip.Opening -= Tsdd_Opening; //just to make sure
        strip.Opening += Tsdd_Opening;
        break;
      //<- empty area of MDI container window
      case MdiClient _:
        control.GetType().GetProperty("BackColor")?.SetValue(control, OScolors.Surface);
        break;
      case PropertyGrid grid: {
        grid.BackColor = OScolors.Control;
        grid.ViewBackColor = OScolors.Control;
        grid.LineColor = OScolors.Surface;
        grid.ViewForeColor = OScolors.TextActive;
        grid.ViewBorderColor = OScolors.ControlDark;
        grid.CategoryForeColor = OScolors.TextActive;
        grid.CategorySplitterColor = OScolors.ControlLight;
        break;
      }
      case ListView view: {
        var lView = view;
        //Mode = IsDarkMode ? "DarkMode_ItemsView" : "ClearMode_ItemsView";
        mode = IsDarkMode ? "DarkMode_Explorer" : "ClearMode_Explorer";
        SetWindowTheme(view.Handle, mode, null);

        if (lView.View == View.Details) {
          //lView.BackColor = OScolors.Surface;
          if (lView.Items.Count > 0) lView.Items[0].UseItemStyleForSubItems = false;
          lView.OwnerDraw = true;
          lView.DrawColumnHeader += (sender, e) => {
            //e.DrawDefault = true;
            //e.DrawBackground();
            //e.DrawText();

            //Draws the Column's Text
            using (SolidBrush backBrush = new SolidBrush(OScolors.ControlLight)) {
              using (SolidBrush foreBrush = new SolidBrush(OScolors.TextActive)) {
                using (var sf = new StringFormat()) {
                  sf.Alignment = StringAlignment.Center;
                  e.Graphics.FillRectangle(backBrush, e.Bounds);
                  e.Graphics.DrawString(e.Header.Text, lView.Font, foreBrush, e.Bounds, sf);
                }
              }
            }
          };
          lView.DrawItem += (sender, e) => { e.DrawDefault = true; };
          lView.DrawSubItem += (sender, e) => {
            e.DrawDefault = true;

            //IntPtr headerControl = GetHeaderControl(lView);
            //IntPtr hdc = GetDC(headerControl);
            //Rectangle rc = new Rectangle(
            //  e.Bounds.Right, //<- Right instead of Left - offsets the rectangle
            //  e.Bounds.Top,
            //  e.Bounds.Width,
            //  e.Bounds.Height
            //);
            //rc.Width += 200;

            //using (SolidBrush backBrush = new SolidBrush(OScolors.ControlLight))
            //{
            //	e.Graphics.FillRectangle(backBrush, rc);
            //}

            //ReleaseDC(headerControl, hdc);
          };

          mode = IsDarkMode ? "DarkMode_Explorer" : "ClearMode_Explorer";
          SetWindowTheme(view.Handle, mode, null);
        }

        break;
      }
      case TreeView _:
        control.GetType().GetProperty("BorderStyle")?.SetValue(control, BorderStyle.None);
        //tree.DrawNode += (object? sender, DrawTreeNodeEventArgs e) =>
        //{
        //  if (e.Node.ImageIndex != -1)
        //  {
        //	Image image = tree.ImageList.Images[e.Node.ImageIndex];
        //	using (Graphics g = Graphics.FromImage(image))
        //	{
        //	  g.InterpolationMode = InterpolationMode.HighQualityBilinear;
        //	  g.CompositingQuality = CompositingQuality.HighQuality;
        //	  g.SmoothingMode = SmoothingMode.HighQuality;

        //	  g.DrawImage(DarkModeCS.ChangeToColor(image, OScolors.TextInactive), new Point(0,0));
        //	}
        //	tree.ImageList.Images[e.Node.ImageIndex] = image;
        //  }
        //  tree.Invalidate();
        //};
        break;
      case DataGridView view: {
        view.EnableHeadersVisualStyles = false;
        view.BorderStyle = BorderStyle.FixedSingle;
        view.BackgroundColor = OScolors.Control;
        view.GridColor = OScolors.Control;

        //paint the bottom right corner where the scrollbars meet
        view.Paint += (sender, e) => {
          var dgv = sender as DataGridView;

          //get the value of dgv.HorizontalScrollBar protected property
          var hs = (HScrollBar)typeof(DataGridView).GetProperty("HorizontalScrollBar", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(dgv);
          if (hs != null && !hs.Visible) return;
          //get the value of dgv.VerticalScrollBar protected property
          var vs = (VScrollBar)typeof(DataGridView).GetProperty("VerticalScrollBar", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(dgv);

          if (vs != null && !vs.Visible) return;
          //only when both the scrollbars are visible, do the actual painting
          Brush brush = new SolidBrush(OScolors.SurfaceDark);
          if (vs == null) return;
          int w = vs.Size.Width;
          if (hs == null) return;
          int h = hs.Size.Height;
          if (dgv != null) e.Graphics.FillRectangle(brush, dgv.ClientRectangle.X + dgv.ClientRectangle.Width - w - 1, dgv.ClientRectangle.Y + dgv.ClientRectangle.Height - h - 1, w, h);
        };

        view.DefaultCellStyle.BackColor = OScolors.Surface;
        view.DefaultCellStyle.ForeColor = OScolors.TextActive;

        view.ColumnHeadersDefaultCellStyle.BackColor = OScolors.Surface;
        view.ColumnHeadersDefaultCellStyle.ForeColor = OScolors.TextActive;
        view.ColumnHeadersDefaultCellStyle.SelectionBackColor = OScolors.Surface;
        view.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

        view.RowHeadersDefaultCellStyle.BackColor = OScolors.Surface;
        view.RowHeadersDefaultCellStyle.ForeColor = OScolors.TextActive;
        view.RowHeadersDefaultCellStyle.SelectionBackColor = OScolors.Surface;
        view.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        break;
      }
      case RichTextBox richText:
        richText.BackColor = richText.Parent.BackColor;
        richText.BorderStyle = BorderStyle.None;
        break;
      case FlowLayoutPanel flowLayout:
        flowLayout.BackColor = flowLayout.Parent.BackColor;
        flowLayout.BorderStyle = BorderStyle.None;
        break;
    }

    //Debug.Print(string.Format("{0}: {1}", control.Name, control.GetType().Name));

    if (control.ContextMenuStrip != null)
      ThemeControl(control.ContextMenuStrip);

    foreach (Control childControl in control.Controls) {
      // Recursively process its children
      ThemeControl(childControl);
    }
  }

  private void TabPageAdded(object s, ControlEventArgs e) {
    ThemeControl(e.Control);
  }

  /// <summary>
  /// Registers the Control as processed. Prevents applying theme to the Control.
  /// Call it before applying the theme to your Form (or to any other Control containing (directly or indirectly) this Control)
  /// </summary>
  public static void ExcludeFromProcessing(Control control) {
    ControlStatusStorage.ExcludeFromProcessing(control);
  }

  /// <summary>Returns Windows Color Mode for Applications.
  /// <para>0=dark theme, 1=light theme</para>
  /// </summary>
  public static int GetWindowsColorMode(bool getSystemColorModeInstead = false) {
    try { return (int)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", getSystemColorModeInstead ? "SystemUsesLightTheme" : "AppsUseLightTheme", -1); } catch { return 1; }
  }

  /// <summary>Returns the Accent Color used by Windows.</summary>
  /// <returns>a Color</returns>
  public static Color GetWindowsAccentColor() {
    try {
      var colors = new DwmColorizationColors();
      DwmGetColorizationParameters(ref colors);

      //get the theme --> only if Windows 10 or newer
      if (IsWindows10OrGreater()) {
        uint color = colors.ColorizationColor;

        long colorValue = long.Parse(color.ToString(), System.Globalization.NumberStyles.HexNumber);

        long transparency = (colorValue >> 24) & 0xFF;
        long red = (colorValue >> 16) & 0xFF;
        long green = (colorValue >> 8) & 0xFF;
        long blue = (colorValue >> 0) & 0xFF;

        return Color.FromArgb((int)transparency, (int)red, (int)green, (int)blue);
      } else { return Color.CadetBlue; }
    } catch (Exception) { return Color.CadetBlue; }
  }

  /// <summary>Returns the Accent Color used by Windows.</summary>
  /// <returns>an opaque Color</returns>
  public static Color GetWindowsAccentOpaqueColor() {
    var colors = new DwmColorizationColors();
    DwmGetColorizationParameters(ref colors);

    //get the theme --> only if Windows 10 or newer
    if (IsWindows10OrGreater()) {
      uint color = colors.ColorizationColor;

      long colorValue = long.Parse(color.ToString(), System.Globalization.NumberStyles.HexNumber);

      long red = (colorValue >> 16) & 0xFF;
      long green = (colorValue >> 8) & 0xFF;
      long blue = (colorValue >> 0) & 0xFF;

      return Color.FromArgb(255, (int)red, (int)green, (int)blue);
    } else { return Color.CadetBlue; }
  }

  /// <summary>Returns Windows's System Colors for UI components following Google Material Design concepts.</summary>
  /// <returns>List of Colors:  Background, OnBackground, Surface, OnSurface, Primary, OnPrimary, Secondary, OnSecondary</returns>
  public static OsThemeColors GetSystemColors(int colorMode = 0) //<- O: DarkMode, 1: LightMode
  {
    var ret = new OsThemeColors();

    if (colorMode > 0) return ret;
    ret.Background = Color.FromArgb(32, 32, 32); //<- Negro Claro
    ret.BackgroundDark = Color.FromArgb(18, 18, 18);
    ret.BackgroundLight = ControlPaint.Light(ret.Background);

    ret.Surface = Color.FromArgb(43, 43, 43); //<- Gris Oscuro
    ret.SurfaceLight = Color.FromArgb(50, 50, 50);
    ret.SurfaceDark = Color.FromArgb(29, 29, 29);

    ret.TextActive = Color.White;
    ret.TextInactive = Color.FromArgb(176, 176, 176); //<- Blanco Palido
    ret.TextInAccent = GetReadableColor(ret.Accent);

    ret.Control = Color.FromArgb(55, 55, 55); //<- Gris Oscuro
    ret.ControlDark = ControlPaint.Dark(ret.Control);
    ret.ControlLight = Color.FromArgb(67, 67, 67);

    ret.Primary = Color.FromArgb(3, 218, 198); //<- Verde Pastel
    ret.Secondary = Color.MediumSlateBlue; //<- Magenta Claro

    return ret;
  }

  /// <summary>Apply Round Corners to the indicated Control or Form.</summary>
  /// <param name="control">the one who will have rounded Corners. Set BorderStyle = None.</param>
  /// <param name="radius">Radious for the Corners</param>
  /// <param name="borderColor">Color for the Border</param>
  /// <param name="borderSize">Size in pixels of the border line</param>
  /// <param name="underlinedStyle"></param>
  public static void SetRoundBorders(Control control, int radius = 10, Color? borderColor = null, int borderSize = 2, bool underlinedStyle = false) {
    borderColor = borderColor ?? Color.MediumSlateBlue;

    if (control == null) return;
    control.GetType().GetProperty("BorderStyle")?.SetValue(control, BorderStyle.None);
    control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, radius, radius));
    control.Paint += (sender, e) => {
      //base.OnPaint(e);
      Graphics graph = e.Graphics;

      if (radius <= 1) return; //Rounded TextBox
      //-Fields
      Rectangle rectBorderSmooth = control.ClientRectangle;
      Rectangle rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
      int smoothSize = borderSize > 0 ? borderSize : 1;

      using (GraphicsPath pathBorderSmooth = GetFigurePath(rectBorderSmooth, radius))
      using (GraphicsPath pathBorder = GetFigurePath(rectBorder, radius - borderSize))
      using (var penBorderSmooth = new Pen(control.Parent.BackColor, smoothSize))
      using (var penBorder = new Pen((Color)borderColor, borderSize)) {
        //-Drawing
        control.Region = new Region(pathBorderSmooth); //Set the rounded region of UserControl
        //Set the rounded region of TextBox component
        if (radius > 15) {
          using (GraphicsPath pathTxt = GetFigurePath(control.ClientRectangle, borderSize * 2)) { control.Region = new Region(pathTxt); }
        }

        graph.SmoothingMode = SmoothingMode.AntiAlias;
        penBorder.Alignment = PenAlignment.Center;
        //if (isFocused) penBorder.Color = borderFocusColor;

        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
        if (underlinedStyle) {
          //Line Style
          //Draw border smoothing
          //Draw border
          graph.SmoothingMode = SmoothingMode.None;
          graph.DrawLine(penBorder, 0, control.Height - 1, control.Width, control.Height - 1);
        } else {
          //Normal Style
          //Draw border smoothing
          //Draw border
          graph.DrawPath(penBorder, pathBorder);
        }
      }
    };
  }

  /// <summary>Colorea una imagen usando una Matrix de Color.</summary>
  /// <param name="bmp">Imagen a Colorear</param>
  /// <param name="c">Color a Utilizar</param>
  public static Bitmap ChangeToColor(Bitmap bmp, Color c) {
    var bmp2 = new Bitmap(bmp.Width, bmp.Height);
    using (Graphics g = Graphics.FromImage(bmp2)) {
      g.InterpolationMode = InterpolationMode.HighQualityBilinear;
      g.CompositingQuality = CompositingQuality.HighQuality;
      g.SmoothingMode = SmoothingMode.HighQuality;

      float tR = c.R / 255f;
      float tG = c.G / 255f;
      float tB = c.B / 255f;

      var colorMatrix = new System.Drawing.Imaging.ColorMatrix(new[] { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, 1, 0 }, new[] { tR, tG, tB, 0, 1 } }); //<- not changing alpha

      var attributes = new System.Drawing.Imaging.ImageAttributes();
      attributes.SetColorMatrix(colorMatrix);

      g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes);
    }

    return bmp2;
  }

  public static Image ChangeToColor(Image bmp, Color c) => ChangeToColor((Bitmap)bmp, c);

  #endregion Public Methods

  #region Private Methods

  /// <summary>
  /// handle hierarchical context menus (otherwise, only the root level gets themed)
  /// </summary>
  private void Tsdd_Opening(object sender, CancelEventArgs e) {
    if (!(sender is ToolStripDropDown tsdd)) return; //should not occur

    foreach (ToolStripMenuItem toolStripMenuItem in tsdd.Items.OfType<ToolStripMenuItem>()) {
      toolStripMenuItem.DropDownOpening -= Tsmi_DropDownOpening; //just to make sure
      toolStripMenuItem.DropDownOpening += Tsmi_DropDownOpening;
    }
  }

  /// <summary>
  /// handle hierarchical context menus (otherwise, only the root level gets themed)
  /// </summary>
  private void Tsmi_DropDownOpening(object sender, EventArgs e) {
    if (!(sender is ToolStripMenuItem tsmi)) return; //should not occur

    if (tsmi.DropDown.Items.Count > 0) ThemeControl(tsmi.DropDown);

    //once processed, remove itself to prevent multiple executions (when user leaves and reenters the sub-menu)
    tsmi.DropDownOpening -= Tsmi_DropDownOpening;
  }

  /// <summary>Attemps to apply Window's Dark Style to the Control and all its childs.</summary>
  /// <param name="control"></param>
  /// <param name="isDarkMode"></param>
  private static void ApplySystemDarkTheme(Control control = null, bool isDarkMode = true) {
    /*
      DWMWA_USE_IMMERSIVE_DARK_MODE:   https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute

      Use with DwmSetWindowAttribute. Allows the window frame for this window to be drawn in dark mode colors when the dark mode system setting is enabled.
      For compatibility reasons, all windows default to light mode regardless of the system setting.
      The pvAttribute parameter points to a value of type BOOL. TRUE to honor dark mode for the window, FALSE to always use light mode.

      This value is supported starting with Windows 11 Build 22000.

      SetWindowTheme:     https://learn.microsoft.com/en-us/windows/win32/api/uxtheme/nf-uxtheme-setwindowtheme
      Causes a window to use a different set of visual style information than its class normally uses. Fix for Scrollbars!
     */
    int[] darkModeOn = isDarkMode ? new[] { 0x01 } : new[] { 0x00 }; //<- 1=True, 0=False
    string mode = isDarkMode ? "DarkMode_Explorer" : "ClearMode_Explorer";

    if (control == null) return;
    SetWindowTheme(control.Handle, mode, null); //DarkMode_Explorer, ClearMode_Explorer, DarkMode_CFD, DarkMode_ItemsView,

    if (DwmSetWindowAttribute(control.Handle, (int)Dwmwindowattribute.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, darkModeOn, 4) != 0)
      DwmSetWindowAttribute(control.Handle, (int)Dwmwindowattribute.DWMWA_USE_IMMERSIVE_DARK_MODE, darkModeOn, 4);

    foreach (Control child in control.Controls) {
      if (child.Controls.Count != 0)
        ApplySystemDarkTheme(child, isDarkMode);
    }
  }

  private static bool IsWindows10OrGreater() {
    return WindowsVersion() >= 10;
  }

  private static int WindowsVersion() {
    //for .Net4.8 and Minor
    int result;
    try {
      RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
      string[] productName = reg?.GetValue("ProductName").ToString().Split((char)32);
      int.TryParse(productName?[1], out result);
    } catch (Exception) {
      OperatingSystem os = Environment.OSVersion;
      result = os.Version.Major;
    }

    return result;

    //fixed .Net6
    //return System.Environment.OSVersion.Version.Major;
  }

  private static Color GetReadableColor(Color backgroundColor) {
    // Calculate the relative luminance of the background color.
    // Normalize values to 0-1 range first.
    double normalizedR = backgroundColor.R / 255.0;
    double normalizedG = backgroundColor.G / 255.0;
    double normalizedB = backgroundColor.B / 255.0;
    double luminance = 0.299 * normalizedR + 0.587 * normalizedG + 0.114 * normalizedB;

    // Choose a contrasting foreground color based on the luminance,
    // with a slight bias towards lighter colors for better readability.
    return luminance < 0.5 ? Color.FromArgb(182, 180, 215) : Color.FromArgb(34, 34, 34); // Dark gray for light backgrounds
  }

  // For Rounded Corners:
  private static GraphicsPath GetFigurePath(Rectangle rect, int radius) {
    var path = new GraphicsPath();
    float curveSize = radius * 2F;

    path.StartFigure();
    path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
    path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
    path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
    path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
    path.CloseFigure();
    return path;
  }

  #endregion Private Methods
}

/// <summary>Windows 10+ System Colors for Clear Color Mode.</summary>
public class OsThemeColors {
  /// <summary>For the very back of the Window</summary>
  public Color Background { get; set; } = SystemColors.Control;

  /// <summary>For Borders around the Background</summary>
  // ReSharper disable once UnusedAutoPropertyAccessor.Global
  public Color BackgroundDark { get; set; } = SystemColors.ControlDark;

  /// <summary>For hightlights over the Background</summary>
  // ReSharper disable once UnusedAutoPropertyAccessor.Global
  public Color BackgroundLight { get; set; } = SystemColors.ControlLight;

  /// <summary>For Container above the Background</summary>
  public Color Surface { get; set; } = SystemColors.ControlLightLight;

  /// <summary>For Borders around the Surface</summary>
  public Color SurfaceDark { get; set; } = SystemColors.ControlLight;

  /// <summary>For Highligh over the Surface</summary>
  // ReSharper disable once UnusedAutoPropertyAccessor.Global
  public Color SurfaceLight { get; set; } = Color.White;

  /// <summary>For Main Texts</summary>
  public Color TextActive { get; set; } = SystemColors.ControlText;

  /// <summary>For Inactive Texts</summary>
  public Color TextInactive { get; set; } = SystemColors.GrayText;

  /// <summary>For Hightligh Texts</summary>
  // ReSharper disable once UnusedAutoPropertyAccessor.Global
  public Color TextInAccent { get; set; } = SystemColors.HighlightText;

  /// <summary>For the background of any Control</summary>
  public Color Control { get; set; } = SystemColors.ButtonFace;

  /// <summary>For Bordes of any Control</summary>
  public Color ControlDark { get; set; } = SystemColors.ButtonShadow;

  /// <summary>For Highlight elements in a Control</summary>
  public Color ControlLight { get; set; } = SystemColors.ButtonHighlight;

  /// <summary>Windows 10+ Chosen Accent Color</summary>
  // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
  public Color Accent { get; set; } = DarkModeForms.GetWindowsAccentColor();

  // ReSharper disable once UnusedMember.Global
  public Color AccentOpaque { get; set; } = DarkModeForms.GetWindowsAccentOpaqueColor();

  // ReSharper disable once UnusedMember.Global
  public Color AccentDark => ControlPaint.Dark(Accent);

  public Color AccentLight => ControlPaint.Light(Accent);

  /// <summary>the color displayed most frequently across your app's screens and components.</summary>
  public Color Primary { get; set; } = SystemColors.Highlight;

  // ReSharper disable once UnusedMember.Global
  public Color PrimaryDark => ControlPaint.Dark(Primary);

  // ReSharper disable once UnusedMember.Global
  public Color PrimaryLight => ControlPaint.Light(Primary);

  /// <summary>to accent select parts of your UI.</summary>
  public Color Secondary { get; set; } = SystemColors.HotTrack;

  // ReSharper disable once UnusedMember.Global
  public Color SecondaryDark => ControlPaint.Dark(Secondary);

  // ReSharper disable once UnusedMember.Global
  public Color SecondaryLight => ControlPaint.Light(Secondary);
}

/* Custom Renderers for Menus and ToolBars */
public class MyRenderer : ToolStripProfessionalRenderer {
  private bool ColorizeIcons { get; set; }
  public OsThemeColors MyColors { get; set; } //<- Your Custom Colors Colection

  public MyRenderer(ProfessionalColorTable table, bool pColorizeIcons = true) : base(table) {
    ColorizeIcons = pColorizeIcons;
  }

  [SuppressMessage("ReSharper", "UnusedParameter.Local")]
  private static void DrawTitleBar(Graphics g, Rectangle rect) {
    // Assign the image for the grip.
    //Image titlebarGrip = titleBarGripBmp;

    // Fill the titlebar.
    // This produces the gradient and the rounded-corner effect.
    //g.DrawLine(new Pen(titlebarColor1), rect.X, rect.Y, rect.X + rect.Width, rect.Y);
    //g.DrawLine(new Pen(titlebarColor2), rect.X, rect.Y + 1, rect.X + rect.Width, rect.Y + 1);
    //g.DrawLine(new Pen(titlebarColor3), rect.X, rect.Y + 2, rect.X + rect.Width, rect.Y + 2);
    //g.DrawLine(new Pen(titlebarColor4), rect.X, rect.Y + 3, rect.X + rect.Width, rect.Y + 3);
    //g.DrawLine(new Pen(titlebarColor5), rect.X, rect.Y + 4, rect.X + rect.Width, rect.Y + 4);
    //g.DrawLine(new Pen(titlebarColor6), rect.X, rect.Y + 5, rect.X + rect.Width, rect.Y + 5);
    //g.DrawLine(new Pen(titlebarColor7), rect.X, rect.Y + 6, rect.X + rect.Width, rect.Y + 6);

    // Center the titlebar grip.
    //g.DrawImage(
    //  titlebarGrip,
    //  new Point(rect.X + ((rect.Width / 2) - (titlebarGrip.Width / 2)),
    //  rect.Y + 1));
  }

  // This method handles the RenderGrip event.
  protected override void OnRenderGrip(ToolStripGripRenderEventArgs e) {
    DrawTitleBar(e.Graphics, new Rectangle(0, 0, e.ToolStrip.Width, 7));
  }

  // This method handles the RenderToolStripBorder event.
  protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
    DrawTitleBar(e.Graphics, new Rectangle(0, 0, e.ToolStrip.Width, 7));
  }

  // Background of the whole ToolBar Or MenuBar:
  protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) {
    e.ToolStrip.BackColor = MyColors.Background;
    base.OnRenderToolStripBackground(e);
  }

  // For Normal Buttons on a ToolBar:
  protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
    Graphics g = e.Graphics;
    var bounds = new Rectangle(Point.Empty, e.Item.Size);

    Color gradientBegin = MyColors.Background; // Color.FromArgb(203, 225, 252);
    Color gradientEnd = MyColors.Background;

    var bordersPencil = new Pen(MyColors.Background);

    var button = e.Item as ToolStripButton;
    if (button != null) {
      if (button.Pressed || button.Checked) {
        gradientBegin = MyColors.Control;
        gradientEnd = MyColors.Control;
      } else if (button.Selected) {
        gradientBegin = MyColors.Accent;
        gradientEnd = MyColors.Accent;
      }
    }

    using (Brush b = new LinearGradientBrush(bounds, gradientBegin, gradientEnd, LinearGradientMode.Vertical)) { g.FillRectangle(b, bounds); }

    e.Graphics.DrawRectangle(bordersPencil, bounds);

    g.DrawLine(bordersPencil, bounds.X, bounds.Y, bounds.Width - 1, bounds.Y);

    g.DrawLine(bordersPencil, bounds.X, bounds.Y, bounds.X, bounds.Height - 1);

    // ReSharper disable once UnusedVariable
    ToolStrip toolStrip = button?.Owner;

    if (!(button?.Owner.GetItemAt(button.Bounds.X, button.Bounds.Bottom + 1) is ToolStripButton)) { g.DrawLine(bordersPencil, bounds.X, bounds.Height - 1, bounds.X + bounds.Width - 1, bounds.Height - 1); }
  }

  // For DropDown Buttons on a ToolBar:
  protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e) {
    // ReSharper disable once UnusedVariable
    Graphics g = e.Graphics;
    var bounds = new Rectangle(Point.Empty, e.Item.Size);
    Color gradientBegin = MyColors.Background; // Color.FromArgb(203, 225, 252);
    Color gradientEnd = MyColors.Background;

    // ReSharper disable once UnusedVariable
    var bordersPencil = new Pen(MyColors.Background);

    //1. Determine the colors to use:
    if (e.Item.Pressed) {
      gradientBegin = MyColors.Control;
      gradientEnd = MyColors.Control;
    } else if (e.Item.Selected) {
      gradientBegin = MyColors.Accent;
      gradientEnd = MyColors.Accent;
    }

    //2. Draw the Box around the Control
    using (Brush b = new LinearGradientBrush(bounds, gradientBegin, gradientEnd, LinearGradientMode.Vertical)) { e.Graphics.FillRectangle(b, bounds); }

    //3. Draws the Chevron:

    #region Chevron

    //int Padding = 2; //<- From the right side
    //Size cSize = new Size(8, 4); //<- Size of the Chevron: 8x4 px
    //Pen ChevronPen = new Pen(MyColors.TextInactive, 2); //<- Color and Border Width
    //Point P1 = new Point(bounds.Width - (cSize.Width + Padding), (bounds.Height / 2) - (cSize.Height / 2));
    //Point P2 = new Point(bounds.Width - Padding, (bounds.Height / 2) - (cSize.Height / 2));
    //Point P3 = new Point(bounds.Width - (cSize.Width / 2 + Padding), (bounds.Height / 2) + (cSize.Height / 2));

    //e.Graphics.DrawLine(ChevronPen, P1, P3);
    //e.Graphics.DrawLine(ChevronPen, P2, P3);

    #endregion Chevron
  }

  // For SplitButtons on a ToolBar:
  protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e) {
    var bounds = new Rectangle(Point.Empty, e.Item.Size);
    Color gradientBegin = MyColors.Background; // Color.FromArgb(203, 225, 252);
    Color gradientEnd = MyColors.Background;

    //1. Determine the colors to use:
    if (e.Item.Pressed) {
      gradientBegin = MyColors.Control;
      gradientEnd = MyColors.Control;
    } else if (e.Item.Selected) {
      gradientBegin = MyColors.Accent;
      gradientEnd = MyColors.Accent;
    }

    //2. Draw the Box around the Control
    using (Brush b = new LinearGradientBrush(bounds, gradientBegin, gradientEnd, LinearGradientMode.Vertical)) { e.Graphics.FillRectangle(b, bounds); }

    //3. Draws the Chevron:

    #region Chevron

    const int padding = 2; //<- From the right side
    var cSize = new Size(8, 4); //<- Size of the Chevron: 8x4 px
    var chevronPen = new Pen(MyColors.TextInactive, 2); //<- Color and Border Width
    var p1 = new Point(bounds.Width - (cSize.Width + padding), (bounds.Height / 2) - (cSize.Height / 2));
    var p2 = new Point(bounds.Width - padding, (bounds.Height / 2) - (cSize.Height / 2));
    var p3 = new Point(bounds.Width - (cSize.Width / 2 + padding), (bounds.Height / 2) + (cSize.Height / 2));

    e.Graphics.DrawLine(chevronPen, p1, p3);
    e.Graphics.DrawLine(chevronPen, p2, p3);

    #endregion Chevron
  }

  // For the Text Color of all Items:
  protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
    e.TextColor = e.Item.Enabled ? MyColors.TextActive : MyColors.TextInactive;

    base.OnRenderItemText(e);
  }

  protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e) {
    base.OnRenderItemBackground(e);

    // Only draw border for ComboBox items
#pragma warning disable CS0184 // 'is' 식의 지정된 식이 제공된 형식이 아닙니다.
    if (!(e.Item is ComboBox)) return;
#pragma warning restore CS0184 // 'is' 식의 지정된 식이 제공된 형식이 아닙니다.
    // ReSharper disable once HeuristicUnreachableCode
    var rect = new Rectangle(Point.Empty, e.Item.Size);
    e.Graphics.DrawRectangle(new Pen(MyColors.ControlLight, 1), rect);
  }

  // For Menu Items BackColor:
  protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
    Graphics g = e.Graphics;
    var bounds = new Rectangle(Point.Empty, e.Item.Size);

    Color gradientBegin = MyColors.Background; // Color.FromArgb(203, 225, 252);
    Color gradientEnd = MyColors.Background; // Color.FromArgb(125, 165, 224);

    bool drawIt = false;
    ToolStripItem menu = e.Item;
    if (menu.Pressed) {
      gradientBegin = MyColors.Control; // Color.FromArgb(254, 128, 62);
      gradientEnd = MyColors.Control; // Color.FromArgb(255, 223, 154);
      drawIt = true;
    } else if (menu.Selected) {
      gradientBegin = MyColors.Accent; // Color.FromArgb(255, 255, 222);
      gradientEnd = MyColors.Accent; // Color.FromArgb(255, 203, 136);
      drawIt = true;
    }

    if (!drawIt) return;
    using (Brush b = new LinearGradientBrush(bounds, gradientBegin, gradientEnd, LinearGradientMode.Vertical)) { g.FillRectangle(b, bounds); }
  }

  // Re-Colors the Icon Images to a Clear color:
  protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e) {
    if (e.Item.GetType().FullName == "System.Windows.Forms.MdiControlStrip+ControlBoxMenuItem") {
      //Window Controls - Minimize, Maximize, Close button of a maximized MDI child windows
      //are realized as ControlBoxMenuItem contained in the MenuStrip
      //by default they would be painted black on a dark surface
      //so to make them more visible, we paint them ourselves:
      Image image = e.Image;
      Color clearColor = e.Item.Enabled ? MyColors.TextActive : MyColors.SurfaceDark;

      using (Image adjustedImage = DarkModeForms.ChangeToColor(image, clearColor)) {
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
        e.Graphics.CompositingQuality = CompositingQuality.AssumeLinear; //looks thinner and less fuzzy than HighQuality
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        e.Graphics.DrawImage(adjustedImage, e.ImageRectangle);
      }

      return;
    }

    if (ColorizeIcons && e.Image != null) {
      // Get the current icon
      Image image = e.Image;
      Color clearColor = e.Item.Enabled ? MyColors.TextInactive : MyColors.SurfaceDark;

      // Create a new image with the desired color adjustments
      using (Image adjustedImage = DarkModeForms.ChangeToColor(image, clearColor)) {
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
        e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        e.Graphics.DrawImage(adjustedImage, e.ImageRectangle);
      }
    } else { base.OnRenderItemImage(e); }
  }
}

public class CustomColorTable : ProfessionalColorTable {
  private OsThemeColors Colors { get; set; }

  public CustomColorTable(OsThemeColors colors) {
    Colors = colors;
    UseSystemColors = false;
  }

  public override Color ImageMarginGradientBegin => Colors.Control;

  public override Color ImageMarginGradientMiddle => Colors.Control;

  public override Color ImageMarginGradientEnd => Colors.Control;
}

/// <summary>
/// Stores additional info related to the Controls
/// </summary>
public class ControlStatusStorage {
  /// <summary>
  /// Storage for the data. ConditionalWeakTable ensures there are no unnecessary references left, preventing garbage collection.
  /// </summary>
  private readonly ConditionalWeakTable<Control, ControlStatusInfo> _controlsProcessed = new ConditionalWeakTable<Control, ControlStatusInfo>();

  /// <summary>
  /// Registers the Control as processed. Prevents applying theme to the Control.
  /// Call it before applying the theme to your Form (or to any other Control containing (directly or indirectly) this Control)
  /// </summary>
  public void ExcludeFromProcessing(Control control) {
    _controlsProcessed.Remove(control);
    _controlsProcessed.Add(control, new ControlStatusInfo() { IsExcluded = true });
  }

  /// <summary>
  /// Gets the additional info associated with a Control
  /// </summary>
  /// <returns>a ControlStatusInfo object if the control has been already processed or marked for exclusion, null otherwise</returns>
  public ControlStatusInfo GetControlStatusInfo(Control control) {
    _controlsProcessed.TryGetValue(control, out ControlStatusInfo info);
    return info;
  }

  public void RegisterProcessedControl(Control control, bool isDarkMode) {
    _controlsProcessed.Add(control, new ControlStatusInfo() { IsExcluded = false, LastThemeAppliedIsDark = isDarkMode });
  }
}

/// <summary>
/// Additional information related to the Controls
/// </summary>
public class ControlStatusInfo {
  /// <summary>
  /// true if the user wants to skip theming the Control
  /// </summary>
  public bool IsExcluded { get; set; }

  /// <summary>
  /// whether the last theme applied was dark
  /// </summary>
  public bool LastThemeAppliedIsDark { get; set; }
}
}

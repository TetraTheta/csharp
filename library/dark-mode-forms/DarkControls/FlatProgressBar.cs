using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace DarkModeForms.DarkControls {
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class FlatProgressBar : ProgressBar {
  public FlatProgressBar() {
    SetStyle(ControlStyles.UserPaint, true);
    //this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
  }

  protected override void OnPaintBackground(PaintEventArgs pevent) {
    // None... Helps control the flicker.
  }

  private int _min; // Minimum value for progress range
  private int _max = 100; // Maximum value for progress range
  private int _val; // Current progress
  private Color _barColor = Color.Green; // Color of progress meter

  protected override void OnResize(EventArgs e) {
    // Invalidate the control to get a repaint.
    this.Invalidate();
  }

  protected override void OnPaint(PaintEventArgs e) {
    Graphics g = e.Graphics;
    using (var brush = new SolidBrush(_barColor)) {
      Brush backBrush = new SolidBrush(BackColor);

      float percent = (_val - _min) / (float)(_max - _min);
      Rectangle rect = ClientRectangle;

      // Calculate area for drawing the progress.
      rect.Width = (int)(rect.Width * percent);

      g.FillRectangle(backBrush, this.ClientRectangle); //Draw the brackgound
      g.FillRectangle(brush, rect); // Draw the progress meter.
      //ProgressBarRenderer.DrawHorizontalBar(g, rect);

      // Draw a three-dimensional border around the control.
      Draw3DBorder(g);

      // Clean up.
      // ReSharper disable once DisposeOnUsingVariable
      brush.Dispose();
      g.Dispose();
    }
  }

  public new int Minimum {
    get => _min;

    set {
      // Prevent a negative value.
      if (value < 0) { value = 0; }

      // Make sure that the minimum value is never set higher than the maximum value.
      if (value > _max) { _max = value; }

      _min = value;

      // Ensure value is still in range
      if (_val < _min) { _val = _min; }

      // Invalidate the control to get a repaint.
      this.Invalidate();
    }
  }

  public new int Maximum {
    get => _max;

    set {
      // Make sure that the maximum value is never set lower than the minimum value.
      if (value < _min) { _min = value; }

      _max = value;

      // Make sure that value is still in range.
      if (_val > _max) { _val = _max; }

      // Invalidate the control to get a repaint.
      this.Invalidate();
    }
  }

  public new int Value {
    get => _val;

    set {
      int oldValue = _val;

      // Make sure that the value does not stray outside the valid range.
      if (value < _min) { _val = _min; } else if (value > _max) { _val = _max; } else { _val = value; }

      // Invalidate only the changed area.

      Rectangle newValueRect = this.ClientRectangle;
      Rectangle oldValueRect = this.ClientRectangle;

      // Use a new value to calculate the rectangle for progress.
      float percent = (_val - _min) / (float)(_max - _min);
      newValueRect.Width = (int)(newValueRect.Width * percent);

      // Use an old value to calculate the rectangle for progress.
      percent = (oldValue - _min) / (float)(_max - _min);
      oldValueRect.Width = (int)(oldValueRect.Width * percent);

      var updateRect = new Rectangle();

      // Find only the part of the screen that must be updated.
      if (newValueRect.Width > oldValueRect.Width) {
        updateRect.X = oldValueRect.Size.Width;
        updateRect.Width = newValueRect.Width - oldValueRect.Width;
      } else {
        updateRect.X = newValueRect.Size.Width;
        updateRect.Width = oldValueRect.Width - newValueRect.Width;
      }

      updateRect.Height = this.Height;

      // Invalidate the intersection region only.
      this.Invalidate(updateRect);
    }
  }

  public Color ProgressBarColor {
    get => _barColor;

    set {
      _barColor = value;

      // Invalidate the control to get a repaint.
      this.Invalidate();
    }
  }

  private void Draw3DBorder(Graphics g) {
    int penWidth = (int)Pens.White.Width;

    g.DrawLine(Pens.DarkGray, new Point(this.ClientRectangle.Left, this.ClientRectangle.Top), new Point(this.ClientRectangle.Width - penWidth, this.ClientRectangle.Top));

    g.DrawLine(Pens.DarkGray, new Point(this.ClientRectangle.Left, this.ClientRectangle.Top), new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - penWidth));

    g.DrawLine(Pens.DarkGray, new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - penWidth), new Point(this.ClientRectangle.Width - penWidth, this.ClientRectangle.Height - penWidth));

    g.DrawLine(Pens.DarkGray, new Point(this.ClientRectangle.Width - penWidth, this.ClientRectangle.Top), new Point(this.ClientRectangle.Width - penWidth, this.ClientRectangle.Height - penWidth));
  }
}
}

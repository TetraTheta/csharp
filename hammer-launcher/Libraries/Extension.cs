using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace HammerLauncher.Libraries;

// I'll use grayscaled image instead
public static class ImageExtension {

  private static readonly ColorMatrix grayMatrix = new([
    [.2126f, .2126f, .2126f, 0, 0],
    [.7152f, .7152f, .7152f, 0, 0],
    [.0722f, .0722f, .0722f, 0, 0],
    [0, 0, 0, 1, 0],
    [0, 0, 0, 0, 1]
  ]);

  public static Bitmap ToGrayScale(this Image source) {
    var grayImage = new Bitmap(source.Width, source.Height, source.PixelFormat);
    grayImage.SetResolution(source.HorizontalResolution, source.VerticalResolution);

    using var g = Graphics.FromImage(grayImage);
    using var attributes = new ImageAttributes();
    attributes.SetColorMatrix(grayMatrix);
    g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height), 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);
    return grayImage;
  }
}

public static class TextBoxExtension {
  public static void SelectEnd(this TextBox textBox) {
    textBox.Select(textBox.Text.Length, 0);
  }
}

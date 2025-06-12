using DarkModeForms;
using System.Windows.Forms;

namespace MarkdownHelper.Forms;

public partial class NewPostForm : Form {
  public NewPostForm() {
    InitializeComponent();
    DMHelper.Apply(this);
  }
}

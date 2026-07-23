using System.Windows.Forms;

using rcalc.Libraries;
using rcalc.Properties;

namespace rcalc.Forms;

partial class CalculatorForm {
  /// <summary>
  /// 필수 디자이너 변수입니다.
  /// </summary>
  private System.ComponentModel.IContainer components = null;

  /// <summary>
  /// 사용 중인 모든 리소스를 정리합니다.
  /// </summary>
  /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
  protected override void Dispose(bool disposing) {
    if (disposing && (components != null)) components.Dispose();
    base.Dispose(disposing);
  }

  #region Windows Form 디자이너에서 생성한 코드

  /// <summary>
  /// 디자이너 지원에 필요한 메서드입니다.
  /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
  /// </summary>
  private void InitializeComponent() {
    buttonCalculate = new Button();
    labelA = new Label();
    labelB = new Label();
    labelX = new Label();
    labelY = new Label();
    listBoxResult = new ListBox();
    numericUpDownA = new CleanNumericUpDown();
    numericUpDownB = new CleanNumericUpDown();
    numericUpDownX = new CleanNumericUpDown();
    numericUpDownY = new CleanNumericUpDown();
    textBoxResult = new TextBox();
    ((System.ComponentModel.ISupportInitialize)numericUpDownA).BeginInit();
    ((System.ComponentModel.ISupportInitialize)numericUpDownB).BeginInit();
    ((System.ComponentModel.ISupportInitialize)numericUpDownX).BeginInit();
    ((System.ComponentModel.ISupportInitialize)numericUpDownY).BeginInit();
    SuspendLayout();
    // 
    // buttonCalculate
    // 
    buttonCalculate.Location = new System.Drawing.Point(341, 7);
    buttonCalculate.Margin = new Padding(3, 4, 3, 4);
    buttonCalculate.Name = "buttonCalculate";
    buttonCalculate.Size = new System.Drawing.Size(131, 58);
    buttonCalculate.TabIndex = 8;
    buttonCalculate.Text = "Calculate";
    buttonCalculate.UseVisualStyleBackColor = true;
    buttonCalculate.Click += ButtonCalculate_Click;
    // 
    // labelA
    // 
    labelA.AutoSize = true;
    labelA.Location = new System.Drawing.Point(12, 42);
    labelA.Name = "labelA";
    labelA.Size = new System.Drawing.Size(18, 19);
    labelA.TabIndex = 2;
    labelA.Text = "A";
    // 
    // labelB
    // 
    labelB.AutoSize = true;
    labelB.Location = new System.Drawing.Point(181, 42);
    labelB.Name = "labelB";
    labelB.Size = new System.Drawing.Size(17, 19);
    labelB.TabIndex = 3;
    labelB.Text = "B";
    // 
    // labelX
    // 
    labelX.AutoSize = true;
    labelX.Location = new System.Drawing.Point(12, 9);
    labelX.Name = "labelX";
    labelX.Size = new System.Drawing.Size(17, 19);
    labelX.TabIndex = 0;
    labelX.Text = "X";
    // 
    // labelY
    // 
    labelY.AutoSize = true;
    labelY.Location = new System.Drawing.Point(181, 9);
    labelY.Name = "labelY";
    labelY.Size = new System.Drawing.Size(17, 19);
    labelY.TabIndex = 1;
    labelY.Text = "Y";
    // 
    // listBoxResult
    // 
    listBoxResult.FormattingEnabled = true;
    listBoxResult.Location = new System.Drawing.Point(10, 105);
    listBoxResult.Margin = new Padding(3, 4, 3, 4);
    listBoxResult.Name = "listBoxResult";
    listBoxResult.Size = new System.Drawing.Size(462, 293);
    listBoxResult.TabIndex = 10;
    listBoxResult.MouseDoubleClick += ListBoxResult_MouseDoubleClick;
    // 
    // numericUpDownA
    // 
    numericUpDownA.DecimalPlaces = 2;
    numericUpDownA.Location = new System.Drawing.Point(35, 40);
    numericUpDownA.Margin = new Padding(3, 4, 3, 4);
    numericUpDownA.Maximum = new decimal(new int[] { -1486618625, 232830643, 0, 0 });
    numericUpDownA.Minimum = new decimal(new int[] { -1486618625, 232830643, 0, int.MinValue });
    numericUpDownA.Name = "numericUpDownA";
    numericUpDownA.Size = new System.Drawing.Size(131, 25);
    numericUpDownA.TabIndex = 13;
    // 
    // numericUpDownB
    // 
    numericUpDownB.DecimalPlaces = 3;
    numericUpDownB.Location = new System.Drawing.Point(204, 40);
    numericUpDownB.Margin = new Padding(3, 4, 3, 4);
    numericUpDownB.Maximum = new decimal(new int[] { -1486618625, 232830643, 0, 0 });
    numericUpDownB.Minimum = new decimal(new int[] { -1486618625, 232830643, 0, int.MinValue });
    numericUpDownB.Name = "numericUpDownB";
    numericUpDownB.ReadOnly = true;
    numericUpDownB.Size = new System.Drawing.Size(130, 25);
    numericUpDownB.TabIndex = 14;
    // 
    // numericUpDownX
    // 
    numericUpDownX.DecimalPlaces = 2;
    numericUpDownX.Location = new System.Drawing.Point(35, 7);
    numericUpDownX.Margin = new Padding(3, 4, 3, 4);
    numericUpDownX.Maximum = new decimal(new int[] { -1486618625, 232830643, 0, 0 });
    numericUpDownX.Minimum = new decimal(new int[] { -1486618625, 232830643, 0, int.MinValue });
    numericUpDownX.Name = "numericUpDownX";
    numericUpDownX.Size = new System.Drawing.Size(131, 25);
    numericUpDownX.TabIndex = 11;
    // 
    // numericUpDownY
    // 
    numericUpDownY.DecimalPlaces = 2;
    numericUpDownY.Location = new System.Drawing.Point(204, 7);
    numericUpDownY.Margin = new Padding(3, 4, 3, 4);
    numericUpDownY.Maximum = new decimal(new int[] { -1486618625, 232830643, 0, 0 });
    numericUpDownY.Minimum = new decimal(new int[] { -1486618625, 232830643, 0, int.MinValue });
    numericUpDownY.Name = "numericUpDownY";
    numericUpDownY.Size = new System.Drawing.Size(131, 25);
    numericUpDownY.TabIndex = 12;
    // 
    // textBoxResult
    // 
    textBoxResult.Location = new System.Drawing.Point(10, 73);
    textBoxResult.Margin = new Padding(3, 4, 3, 4);
    textBoxResult.Name = "textBoxResult";
    textBoxResult.ReadOnly = true;
    textBoxResult.Size = new System.Drawing.Size(462, 25);
    textBoxResult.TabIndex = 9;
    // 
    // CalculatorForm
    // 
    AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
    AutoScaleMode = AutoScaleMode.Font;
    ClientSize = new System.Drawing.Size(484, 411);
    Controls.Add(numericUpDownB);
    Controls.Add(numericUpDownA);
    Controls.Add(numericUpDownY);
    Controls.Add(numericUpDownX);
    Controls.Add(listBoxResult);
    Controls.Add(textBoxResult);
    Controls.Add(buttonCalculate);
    Controls.Add(labelB);
    Controls.Add(labelA);
    Controls.Add(labelY);
    Controls.Add(labelX);
    Font = new System.Drawing.Font("Segoe UI", 10F);
    FormBorderStyle = FormBorderStyle.FixedSingle;
    Icon = RuntimeResources.MainIcon;
    Margin = new Padding(3, 4, 3, 4);
    Name = "CalculatorForm";
    StartPosition = FormStartPosition.CenterParent;
    Text = "rcalc";
    ((System.ComponentModel.ISupportInitialize)numericUpDownA).EndInit();
    ((System.ComponentModel.ISupportInitialize)numericUpDownB).EndInit();
    ((System.ComponentModel.ISupportInitialize)numericUpDownX).EndInit();
    ((System.ComponentModel.ISupportInitialize)numericUpDownY).EndInit();
    ResumeLayout(false);
    PerformLayout();
  }

  #endregion

  private Button buttonCalculate;
  private CleanNumericUpDown numericUpDownA;
  private CleanNumericUpDown numericUpDownB;
  private CleanNumericUpDown numericUpDownX;
  private CleanNumericUpDown numericUpDownY;
  private Label labelA;
  private Label labelB;
  private Label labelX;
  private Label labelY;
  private ListBox listBoxResult;
  private TextBox textBoxResult;
}

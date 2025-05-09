namespace rcalc
{
  partial class CalculatorForm
  {
    /// <summary>
    /// 필수 디자이너 변수입니다.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 사용 중인 모든 리소스를 정리합니다.
    /// </summary>
    /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form 디자이너에서 생성한 코드

    /// <summary>
    /// 디자이너 지원에 필요한 메서드입니다. 
    /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
    /// </summary>
    private void InitializeComponent()
    {
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelA = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.listBoxResult = new System.Windows.Forms.ListBox();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownA = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownB = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(12, 9);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(17, 19);
            this.labelX.TabIndex = 0;
            this.labelX.Text = "X";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(181, 9);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(17, 19);
            this.labelY.TabIndex = 1;
            this.labelY.Text = "Y";
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Location = new System.Drawing.Point(12, 42);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(18, 19);
            this.labelA.TabIndex = 2;
            this.labelA.Text = "A";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(181, 42);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(17, 19);
            this.labelB.TabIndex = 3;
            this.labelB.Text = "B";
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(341, 7);
            this.buttonCalculate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(131, 58);
            this.buttonCalculate.TabIndex = 8;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.ButtonCalculate_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(10, 73);
            this.textBoxResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.Size = new System.Drawing.Size(462, 25);
            this.textBoxResult.TabIndex = 9;
            // 
            // listBoxResult
            // 
            this.listBoxResult.FormattingEnabled = true;
            this.listBoxResult.ItemHeight = 17;
            this.listBoxResult.Location = new System.Drawing.Point(10, 105);
            this.listBoxResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBoxResult.Name = "listBoxResult";
            this.listBoxResult.Size = new System.Drawing.Size(462, 293);
            this.listBoxResult.TabIndex = 10;
            this.listBoxResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxResult_MouseDoubleClick);
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.DecimalPlaces = 2;
            this.numericUpDownX.Location = new System.Drawing.Point(35, 7);
            this.numericUpDownX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            -2147483648});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(131, 25);
            this.numericUpDownX.TabIndex = 11;
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.DecimalPlaces = 2;
            this.numericUpDownY.Location = new System.Drawing.Point(204, 7);
            this.numericUpDownY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            -2147483648});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(131, 25);
            this.numericUpDownY.TabIndex = 12;
            // 
            // numericUpDownA
            // 
            this.numericUpDownA.DecimalPlaces = 2;
            this.numericUpDownA.Location = new System.Drawing.Point(35, 40);
            this.numericUpDownA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownA.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.numericUpDownA.Minimum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            -2147483648});
            this.numericUpDownA.Name = "numericUpDownA";
            this.numericUpDownA.Size = new System.Drawing.Size(131, 25);
            this.numericUpDownA.TabIndex = 13;
            // 
            // numericUpDownB
            // 
            this.numericUpDownB.DecimalPlaces = 3;
            this.numericUpDownB.Location = new System.Drawing.Point(204, 40);
            this.numericUpDownB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownB.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.numericUpDownB.Minimum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            -2147483648});
            this.numericUpDownB.Name = "numericUpDownB";
            this.numericUpDownB.ReadOnly = true;
            this.numericUpDownB.Size = new System.Drawing.Size(130, 25);
            this.numericUpDownB.TabIndex = 14;
            // 
            // CalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.numericUpDownB);
            this.Controls.Add(this.numericUpDownA);
            this.Controls.Add(this.numericUpDownY);
            this.Controls.Add(this.numericUpDownX);
            this.Controls.Add(this.listBoxResult);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.labelB);
            this.Controls.Add(this.labelA);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::rcalc.Properties.Resources.MainIcon;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CalculatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "rcalc";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelX;
    private System.Windows.Forms.Label labelY;
    private System.Windows.Forms.Label labelA;
    private System.Windows.Forms.Label labelB;
    private System.Windows.Forms.Button buttonCalculate;
    private System.Windows.Forms.TextBox textBoxResult;
    private System.Windows.Forms.ListBox listBoxResult;
    private System.Windows.Forms.NumericUpDown numericUpDownX;
    private System.Windows.Forms.NumericUpDown numericUpDownY;
    private System.Windows.Forms.NumericUpDown numericUpDownA;
    private System.Windows.Forms.NumericUpDown numericUpDownB;
  }
}


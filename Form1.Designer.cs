namespace PlaySO;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        listBox1 = new ListBox();
        addAtalho = new Button();
        abrirAtalho = new Button();
        apagarAtalho = new Button();
        alterarAtalho = new Button();
        btnSair = new Button();
        SuspendLayout();
        // 
        // listBox1
        // 
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 15;
        listBox1.Location = new Point(12, 134);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(776, 304);
        listBox1.TabIndex = 0;
        // 
        // addAtalho
        // 
        addAtalho.Location = new Point(12, 12);
        addAtalho.Name = "addAtalho";
        addAtalho.Size = new Size(75, 23);
        addAtalho.TabIndex = 1;
        addAtalho.Text = "Adicionar";
        addAtalho.UseVisualStyleBackColor = true;
        // 
        // abrirAtalho
        // 
        abrirAtalho.Location = new Point(713, 68);
        abrirAtalho.Name = "abrirAtalho";
        abrirAtalho.Size = new Size(75, 60);
        abrirAtalho.TabIndex = 2;
        abrirAtalho.Text = "Abrir";
        abrirAtalho.UseVisualStyleBackColor = true;
        // 
        // apagarAtalho
        // 
        apagarAtalho.Location = new Point(228, 12);
        apagarAtalho.Name = "apagarAtalho";
        apagarAtalho.Size = new Size(75, 23);
        apagarAtalho.TabIndex = 3;
        apagarAtalho.Text = "Deletar";
        apagarAtalho.UseVisualStyleBackColor = true;
        // 
        // alterarAtalho
        // 
        alterarAtalho.Location = new Point(119, 12);
        alterarAtalho.Name = "alterarAtalho";
        alterarAtalho.Size = new Size(75, 23);
        alterarAtalho.TabIndex = 4;
        alterarAtalho.Text = "Alterar";
        alterarAtalho.UseVisualStyleBackColor = true;
        // 
        // btnSair
        // 
        btnSair.Location = new Point(713, 12);
        btnSair.Name = "btnSair";
        btnSair.Size = new Size(75, 23);
        btnSair.TabIndex = 5;
        btnSair.Text = "Sair";
        btnSair.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(btnSair);
        Controls.Add(alterarAtalho);
        Controls.Add(apagarAtalho);
        Controls.Add(abrirAtalho);
        Controls.Add(addAtalho);
        Controls.Add(listBox1);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion

    private ListBox listBox1;
    private Button addAtalho;
    private Button abrirAtalho;
    private Button apagarAtalho;
    private Button alterarAtalho;
    private Button btnSair;
}

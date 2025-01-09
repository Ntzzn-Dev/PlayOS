namespace PlaySO;

partial class Form3
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
        components = new System.ComponentModel.Container();
        btnNext = new Button();
        pictureBox1 = new PictureBox();
        btnPrev = new Button();
        btnDeletar = new Button();
        nameGame = new Label();
        pathGame = new Label();
        btnEditar = new Button();
        btnAbrir = new Button();
        btnAdicionar = new Button();
        btnFechar = new Button();
        iconPic = new PictureBox();
        appIcon = new PictureBox();
        lblNomeApp = new Label();
        picPuxarApps = new PictureBox();
        pnlApps = new Panel();
        pnlAppTransition = new System.Windows.Forms.Timer(components);
        lblClockAtalho = new Label();
        lblDataAtalhos = new Label();
        lblUltimaSessao = new Label();
        lblDuracaoSessao = new Label();
        lblDataSessao = new Label();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)iconPic).BeginInit();
        ((System.ComponentModel.ISupportInitialize)appIcon).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picPuxarApps).BeginInit();
        SuspendLayout();
        // 
        // btnNext
        // 
        btnNext.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        btnNext.BackColor = Color.Transparent;
        btnNext.FlatAppearance.BorderSize = 0;
        btnNext.FlatStyle = FlatStyle.Flat;
        btnNext.Font = new Font("Segoe UI Black", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnNext.ForeColor = SystemColors.ControlDarkDark;
        btnNext.Location = new Point(1525, 328);
        btnNext.Margin = new Padding(60);
        btnNext.Name = "btnNext";
        btnNext.Size = new Size(75, 75);
        btnNext.TabIndex = 3;
        btnNext.Text = ">";
        btnNext.UseVisualStyleBackColor = true;
        // 
        // pictureBox1
        // 
        pictureBox1.Dock = DockStyle.Fill;
        pictureBox1.Location = new Point(0, 0);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(1669, 726);
        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.TabIndex = 10;
        pictureBox1.TabStop = false;
        // 
        // btnPrev
        // 
        btnPrev.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        btnPrev.BackColor = Color.Transparent;
        btnPrev.FlatAppearance.BorderSize = 0;
        btnPrev.FlatStyle = FlatStyle.Flat;
        btnPrev.Font = new Font("Segoe UI Black", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnPrev.ForeColor = SystemColors.ControlDarkDark;
        btnPrev.Location = new Point(69, 328);
        btnPrev.Margin = new Padding(60);
        btnPrev.Name = "btnPrev";
        btnPrev.Size = new Size(75, 75);
        btnPrev.TabIndex = 2;
        btnPrev.Text = "<";
        btnPrev.UseVisualStyleBackColor = true;
        // 
        // btnDeletar
        // 
        btnDeletar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnDeletar.BackColor = Color.Transparent;
        btnDeletar.FlatStyle = FlatStyle.Flat;
        btnDeletar.ForeColor = SystemColors.ControlDarkDark;
        btnDeletar.Location = new Point(1565, 645);
        btnDeletar.Name = "btnDeletar";
        btnDeletar.Size = new Size(75, 58);
        btnDeletar.TabIndex = 6;
        btnDeletar.Text = "Deletar";
        btnDeletar.UseVisualStyleBackColor = false;
        // 
        // nameGame
        // 
        nameGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        nameGame.AutoSize = true;
        nameGame.BackColor = Color.Transparent;
        nameGame.FlatStyle = FlatStyle.Flat;
        nameGame.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
        nameGame.ForeColor = SystemColors.ControlDarkDark;
        nameGame.Location = new Point(1220, 535);
        nameGame.Name = "nameGame";
        nameGame.Size = new Size(243, 45);
        nameGame.TabIndex = 12;
        nameGame.Text = "Nome do Jogo";
        nameGame.TextAlign = ContentAlignment.MiddleRight;
        // 
        // pathGame
        // 
        pathGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        pathGame.AutoSize = true;
        pathGame.BackColor = Color.Transparent;
        pathGame.FlatStyle = FlatStyle.Flat;
        pathGame.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
        pathGame.ForeColor = SystemColors.ControlDark;
        pathGame.Location = new Point(1380, 580);
        pathGame.Name = "pathGame";
        pathGame.RightToLeft = RightToLeft.No;
        pathGame.Size = new Size(90, 28);
        pathGame.TabIndex = 13;
        pathGame.Text = "Caminho";
        pathGame.TextAlign = ContentAlignment.MiddleRight;
        // 
        // btnEditar
        // 
        btnEditar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnEditar.BackColor = Color.Transparent;
        btnEditar.FlatStyle = FlatStyle.Flat;
        btnEditar.ForeColor = SystemColors.ControlDarkDark;
        btnEditar.Location = new Point(1484, 645);
        btnEditar.Name = "btnEditar";
        btnEditar.Size = new Size(75, 58);
        btnEditar.TabIndex = 5;
        btnEditar.Text = "Editar";
        btnEditar.UseVisualStyleBackColor = false;
        // 
        // btnAbrir
        // 
        btnAbrir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        btnAbrir.BackColor = Color.Transparent;
        btnAbrir.FlatStyle = FlatStyle.Flat;
        btnAbrir.ForeColor = SystemColors.ControlDarkDark;
        btnAbrir.Location = new Point(797, 628);
        btnAbrir.Name = "btnAbrir";
        btnAbrir.Size = new Size(75, 75);
        btnAbrir.TabIndex = 4;
        btnAbrir.Text = "Abrir";
        btnAbrir.UseVisualStyleBackColor = false;
        // 
        // btnAdicionar
        // 
        btnAdicionar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdicionar.BackColor = Color.Transparent;
        btnAdicionar.FlatStyle = FlatStyle.Flat;
        btnAdicionar.ForeColor = SystemColors.ControlDarkDark;
        btnAdicionar.Location = new Point(1582, 12);
        btnAdicionar.Name = "btnAdicionar";
        btnAdicionar.Size = new Size(75, 75);
        btnAdicionar.TabIndex = 1;
        btnAdicionar.Text = "Novo";
        btnAdicionar.UseVisualStyleBackColor = false;
        // 
        // btnFechar
        // 
        btnFechar.BackColor = Color.Transparent;
        btnFechar.FlatStyle = FlatStyle.Flat;
        btnFechar.ForeColor = SystemColors.ControlDarkDark;
        btnFechar.Location = new Point(12, 12);
        btnFechar.Name = "btnFechar";
        btnFechar.Size = new Size(75, 75);
        btnFechar.TabIndex = 0;
        btnFechar.Text = "Fechar";
        btnFechar.UseVisualStyleBackColor = false;
        // 
        // iconPic
        // 
        iconPic.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        iconPic.BackColor = Color.Transparent;
        iconPic.Location = new Point(1270, 332);
        iconPic.Name = "iconPic";
        iconPic.Size = new Size(200, 200);
        iconPic.SizeMode = PictureBoxSizeMode.Zoom;
        iconPic.TabIndex = 18;
        iconPic.TabStop = false;
        // 
        // appIcon
        // 
        appIcon.Location = new Point(0, 0);
        appIcon.Name = "appIcon";
        appIcon.Size = new Size(100, 50);
        appIcon.TabIndex = 0;
        appIcon.TabStop = false;
        // 
        // lblNomeApp
        // 
        lblNomeApp.Location = new Point(0, 0);
        lblNomeApp.Name = "lblNomeApp";
        lblNomeApp.Size = new Size(100, 23);
        lblNomeApp.TabIndex = 0;
        // 
        // picPuxarApps
        // 
        picPuxarApps.Anchor = AnchorStyles.Top;
        picPuxarApps.BackColor = Color.FromArgb(70, 70, 70);
        picPuxarApps.Location = new Point(752, 0);
        picPuxarApps.Margin = new Padding(0);
        picPuxarApps.Name = "picPuxarApps";
        picPuxarApps.Size = new Size(158, 41);
        picPuxarApps.SizeMode = PictureBoxSizeMode.Zoom;
        picPuxarApps.TabIndex = 0;
        picPuxarApps.TabStop = false;
        // 
        // pnlApps
        // 
        pnlApps.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlApps.BackColor = Color.FromArgb(70, 70, 70);
        pnlApps.Location = new Point(0, 0);
        pnlApps.Margin = new Padding(0);
        pnlApps.Name = "pnlApps";
        pnlApps.Size = new Size(1669, 0);
        pnlApps.TabIndex = 22;
        // 
        // pnlAppTransition
        // 
        pnlAppTransition.Interval = 10;
        // 
        // lblClockAtalho
        // 
        lblClockAtalho.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblClockAtalho.AutoSize = true;
        lblClockAtalho.BackColor = Color.Transparent;
        lblClockAtalho.Font = new Font("Verdana", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblClockAtalho.ForeColor = SystemColors.ControlDarkDark;
        lblClockAtalho.Location = new Point(1346, 88);
        lblClockAtalho.Name = "lblClockAtalho";
        lblClockAtalho.RightToLeft = RightToLeft.No;
        lblClockAtalho.Size = new Size(186, 38);
        lblClockAtalho.TabIndex = 23;
        lblClockAtalho.Text = "12:12 PM";
        lblClockAtalho.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblDataAtalhos
        // 
        lblDataAtalhos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblDataAtalhos.AutoSize = true;
        lblDataAtalhos.BackColor = Color.Transparent;
        lblDataAtalhos.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblDataAtalhos.ForeColor = SystemColors.ControlDark;
        lblDataAtalhos.Location = new Point(1342, 126);
        lblDataAtalhos.Name = "lblDataAtalhos";
        lblDataAtalhos.RightToLeft = RightToLeft.No;
        lblDataAtalhos.Size = new Size(183, 16);
        lblDataAtalhos.TabIndex = 24;
        lblDataAtalhos.Text = "12 de setembro de 2012";
        lblDataAtalhos.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblUltimaSessao
        // 
        lblUltimaSessao.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblUltimaSessao.AutoSize = true;
        lblUltimaSessao.BackColor = Color.Transparent;
        lblUltimaSessao.FlatStyle = FlatStyle.Flat;
        lblUltimaSessao.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblUltimaSessao.ForeColor = SystemColors.ControlDarkDark;
        lblUltimaSessao.Location = new Point(69, 535);
        lblUltimaSessao.Name = "lblUltimaSessao";
        lblUltimaSessao.Size = new Size(233, 45);
        lblUltimaSessao.TabIndex = 25;
        lblUltimaSessao.Text = "Ultima sessão:";
        lblUltimaSessao.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblDuracaoSessao
        // 
        lblDuracaoSessao.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblDuracaoSessao.AutoSize = true;
        lblDuracaoSessao.BackColor = Color.Transparent;
        lblDuracaoSessao.FlatStyle = FlatStyle.Flat;
        lblDuracaoSessao.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblDuracaoSessao.ForeColor = SystemColors.ControlDark;
        lblDuracaoSessao.Location = new Point(73, 571);
        lblDuracaoSessao.Name = "lblDuracaoSessao";
        lblDuracaoSessao.RightToLeft = RightToLeft.No;
        lblDuracaoSessao.Size = new Size(137, 28);
        lblDuracaoSessao.TabIndex = 26;
        lblDuracaoSessao.Text = "Durou: 2h32m";
        lblDuracaoSessao.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblDataSessao
        // 
        lblDataSessao.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblDataSessao.AutoSize = true;
        lblDataSessao.BackColor = Color.Transparent;
        lblDataSessao.FlatStyle = FlatStyle.Flat;
        lblDataSessao.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblDataSessao.ForeColor = SystemColors.ControlDark;
        lblDataSessao.Location = new Point(75, 597);
        lblDataSessao.Name = "lblDataSessao";
        lblDataSessao.RightToLeft = RightToLeft.No;
        lblDataSessao.Size = new Size(159, 21);
        lblDataSessao.TabIndex = 27;
        lblDataSessao.Text = "Dia: 12/12/24 - 12:33";
        lblDataSessao.TextAlign = ContentAlignment.MiddleRight;
        // 
        // Form3
        // 
        ClientSize = new Size(1669, 726);
        Controls.Add(lblUltimaSessao);
        Controls.Add(lblDataSessao);
        Controls.Add(lblDuracaoSessao);
        Controls.Add(lblDataAtalhos);
        Controls.Add(lblClockAtalho);
        Controls.Add(picPuxarApps);
        Controls.Add(pnlApps);
        Controls.Add(iconPic);
        Controls.Add(btnFechar);
        Controls.Add(btnAdicionar);
        Controls.Add(btnAbrir);
        Controls.Add(btnEditar);
        Controls.Add(pathGame);
        Controls.Add(nameGame);
        Controls.Add(btnDeletar);
        Controls.Add(btnPrev);
        Controls.Add(btnNext);
        Controls.Add(pictureBox1);
        Name = "Form3";
        Text = "GameRoll";
        WindowState = FormWindowState.Maximized;
        nameGame.Parent = pictureBox1; 
        pathGame.Parent = pictureBox1;
        btnNext.Parent = pictureBox1; 
        btnPrev.Parent = pictureBox1;
        btnAbrir.Parent = pictureBox1;
        btnEditar.Parent = pictureBox1;
        btnDeletar.Parent = pictureBox1;
        btnAdicionar.Parent = pictureBox1;
        btnFechar.Parent = pictureBox1;
        iconPic.Parent = pictureBox1;
        picPuxarApps.Parent = pictureBox1;
        lblClockAtalho.Parent = pictureBox1;
        lblDataAtalhos.Parent = pictureBox1;
        lblDataSessao.Parent = pictureBox1;
        lblUltimaSessao.Parent = pictureBox1;
        lblDuracaoSessao.Parent = pictureBox1;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)iconPic).EndInit();
        ((System.ComponentModel.ISupportInitialize)appIcon).EndInit();
        ((System.ComponentModel.ISupportInitialize)picPuxarApps).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    /*
        nameGame.Parent = pictureBox1; 
        pathGame.Parent = pictureBox1;
        btnNext.Parent = pictureBox1; 
        btnPrev.Parent = pictureBox1;
        btnAbrir.Parent = pictureBox1;
        btnEditar.Parent = pictureBox1;
        btnDeletar.Parent = pictureBox1;
        btnAdicionar.Parent = pictureBox1;
        btnFechar.Parent = pictureBox1;
        iconPic.Parent = pictureBox1;
        picPuxarApps.Parent = pictureBox1;
        lblClockAtalho.Parent = pictureBox1;
        lblDataAtalhos.Parent = pictureBox1;
        lblDataSessao.Parent = pictureBox1;
        lblUltimaSessao.Parent = pictureBox1;
        lblDuracaoSessao.Parent = pictureBox1;
    */

    #endregion

    private TextBox nomeGame;
    private TextBox caminhoGame;
    private Label nomedojogo;
    private Label caminhodojogo;
    private Button btnSalvar;
    private Button btnCancelar;
    private Label parametrodojogo;
    private TextBox parametroGame;
    private Button btnNext;
    private Button btnPrev;
    private PictureBox pictureBox1;
    private Button btnDeletar;
    private Label nameGame;
    private Label pathGame;
    private Button btnEditar;
    private Button btnAbrir;
    private Button btnAdicionar;
    private Button btnFechar;
    private PictureBox iconPic;
    private PictureBox appIcon;
    private PictureBox picPuxarApps;
    private Label lblNomeApp;
    private Panel pnlApps;
    private System.Windows.Forms.Timer pnlAppTransition;
    private Label lblClockAtalho;
    private Label lblDataAtalhos;
    private Label lblUltimaSessao;
    private Label lblDuracaoSessao;
    private Label lblDataSessao;
}

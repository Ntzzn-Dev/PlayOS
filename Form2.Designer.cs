namespace PlaySO;

partial class Form2
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
        nomeGame = new TextBox();
        caminhoGame = new TextBox();
        nomedojogo = new Label();
        caminhodojogo = new Label();
        btnSalvar = new Button();
        btnCancelar = new Button();
        parametrodojogo = new Label();
        parametroGame = new TextBox();
        btnProcurarArq = new Button();
        imgdoJogo = new Label();
        imgGame = new TextBox();
        btnImgLocal = new Button();
        btnImgGoogle = new Button();
        pictureImgGame = new PictureBox();
        btnImportAtalho = new Button();
        btnIconGoogle = new Button();
        btnIconLocal = new Button();
        iconDoJogo = new Label();
        iconGame = new TextBox();
        pictureIconGame = new PictureBox();
        tabAbasCadastros = new TabControl();
        tabPageCdtAtalho = new TabPage();
        tabPageCdtApps = new TabPage();
        btnURLExtApp = new Button();
        btnAjudaApp = new Button();
        lblNomeApp = new Label();
        picImgIconeApp = new PictureBox();
        txtbxNomeApp = new TextBox();
        btnIconOnlineApp = new Button();
        txtbxURLApp = new TextBox();
        btnIconLocalApp = new Button();
        lblURLApp = new Label();
        lblImgIconeApp = new Label();
        btnSalvarApp = new Button();
        txtbxImgIconeApp = new TextBox();
        btnCancelarApp = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureImgGame).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureIconGame).BeginInit();
        tabAbasCadastros.SuspendLayout();
        tabPageCdtAtalho.SuspendLayout();
        tabPageCdtApps.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picImgIconeApp).BeginInit();
        SuspendLayout();
        // 
        // nomeGame
        // 
        nomeGame.Location = new Point(19, 41);
        nomeGame.Name = "nomeGame";
        nomeGame.PlaceholderText = "Nome do Jogo";
        nomeGame.Size = new Size(629, 23);
        nomeGame.TabIndex = 0;
        // 
        // caminhoGame
        // 
        caminhoGame.Location = new Point(19, 100);
        caminhoGame.Name = "caminhoGame";
        caminhoGame.PlaceholderText = "Caminho";
        caminhoGame.Size = new Size(548, 23);
        caminhoGame.TabIndex = 1;
        // 
        // nomedojogo
        // 
        nomedojogo.AutoSize = true;
        nomedojogo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        nomedojogo.Location = new Point(19, 8);
        nomedojogo.Name = "nomedojogo";
        nomedojogo.Size = new Size(159, 30);
        nomedojogo.TabIndex = 2;
        nomedojogo.Text = "Nome do Jogo";
        // 
        // caminhodojogo
        // 
        caminhodojogo.AutoSize = true;
        caminhodojogo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        caminhodojogo.Location = new Point(19, 67);
        caminhodojogo.Name = "caminhodojogo";
        caminhodojogo.Size = new Size(187, 30);
        caminhodojogo.TabIndex = 3;
        caminhodojogo.Text = "Caminho do Jogo";
        // 
        // btnSalvar
        // 
        btnSalvar.Location = new Point(404, 460);
        btnSalvar.Name = "btnSalvar";
        btnSalvar.Size = new Size(119, 41);
        btnSalvar.TabIndex = 11;
        btnSalvar.Text = "Salvar";
        btnSalvar.UseVisualStyleBackColor = true;
        // 
        // btnCancelar
        // 
        btnCancelar.Location = new Point(529, 460);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(119, 41);
        btnCancelar.TabIndex = 12;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        // 
        // parametrodojogo
        // 
        parametrodojogo.AutoSize = true;
        parametrodojogo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        parametrodojogo.Location = new Point(17, 125);
        parametrodojogo.Name = "parametrodojogo";
        parametrodojogo.Size = new Size(245, 30);
        parametrodojogo.TabIndex = 7;
        parametrodojogo.Text = "Parametros para o Jogo";
        // 
        // parametroGame
        // 
        parametroGame.Location = new Point(19, 158);
        parametroGame.Name = "parametroGame";
        parametroGame.PlaceholderText = "parametros (opcional)";
        parametroGame.Size = new Size(629, 23);
        parametroGame.TabIndex = 3;
        // 
        // btnProcurarArq
        // 
        btnProcurarArq.Location = new Point(573, 100);
        btnProcurarArq.Name = "btnProcurarArq";
        btnProcurarArq.Size = new Size(75, 23);
        btnProcurarArq.TabIndex = 2;
        btnProcurarArq.Text = "Procurar";
        btnProcurarArq.UseVisualStyleBackColor = true;
        // 
        // imgdoJogo
        // 
        imgdoJogo.AutoSize = true;
        imgdoJogo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        imgdoJogo.Location = new Point(19, 183);
        imgdoJogo.Name = "imgdoJogo";
        imgdoJogo.Size = new Size(211, 30);
        imgdoJogo.TabIndex = 10;
        imgdoJogo.Text = "Imagem de exibição";
        // 
        // imgGame
        // 
        imgGame.Location = new Point(19, 216);
        imgGame.Name = "imgGame";
        imgGame.PlaceholderText = "Imagem (URL da internet ou caminho local)";
        imgGame.Size = new Size(379, 23);
        imgGame.TabIndex = 4;
        // 
        // btnImgLocal
        // 
        btnImgLocal.Location = new Point(529, 215);
        btnImgLocal.Name = "btnImgLocal";
        btnImgLocal.Size = new Size(119, 23);
        btnImgLocal.TabIndex = 6;
        btnImgLocal.Text = "Procurar";
        btnImgLocal.UseVisualStyleBackColor = true;
        // 
        // btnImgGoogle
        // 
        btnImgGoogle.Location = new Point(404, 215);
        btnImgGoogle.Name = "btnImgGoogle";
        btnImgGoogle.Size = new Size(119, 23);
        btnImgGoogle.TabIndex = 5;
        btnImgGoogle.Text = "Pesquisar online";
        btnImgGoogle.UseVisualStyleBackColor = true;
        // 
        // pictureImgGame
        // 
        pictureImgGame.Location = new Point(19, 308);
        pictureImgGame.Name = "pictureImgGame";
        pictureImgGame.Size = new Size(344, 193);
        pictureImgGame.SizeMode = PictureBoxSizeMode.Zoom;
        pictureImgGame.TabIndex = 13;
        pictureImgGame.TabStop = false;
        // 
        // btnImportAtalho
        // 
        btnImportAtalho.Location = new Point(404, 420);
        btnImportAtalho.Name = "btnImportAtalho";
        btnImportAtalho.Size = new Size(244, 34);
        btnImportAtalho.TabIndex = 10;
        btnImportAtalho.Text = "Importar atalho";
        btnImportAtalho.UseVisualStyleBackColor = true;
        // 
        // btnIconGoogle
        // 
        btnIconGoogle.Location = new Point(404, 273);
        btnIconGoogle.Name = "btnIconGoogle";
        btnIconGoogle.Size = new Size(119, 23);
        btnIconGoogle.TabIndex = 8;
        btnIconGoogle.Text = "Pesquisar online";
        btnIconGoogle.UseVisualStyleBackColor = true;
        // 
        // btnIconLocal
        // 
        btnIconLocal.Location = new Point(529, 273);
        btnIconLocal.Name = "btnIconLocal";
        btnIconLocal.Size = new Size(119, 23);
        btnIconLocal.TabIndex = 9;
        btnIconLocal.Text = "Procurar";
        btnIconLocal.UseVisualStyleBackColor = true;
        // 
        // iconDoJogo
        // 
        iconDoJogo.AutoSize = true;
        iconDoJogo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        iconDoJogo.Location = new Point(19, 241);
        iconDoJogo.Name = "iconDoJogo";
        iconDoJogo.Size = new Size(184, 30);
        iconDoJogo.TabIndex = 16;
        iconDoJogo.Text = "Imagem do icone";
        // 
        // iconGame
        // 
        iconGame.Location = new Point(19, 274);
        iconGame.Name = "iconGame";
        iconGame.PlaceholderText = "Icone (automático do executavel [low scale])";
        iconGame.Size = new Size(379, 23);
        iconGame.TabIndex = 7;
        // 
        // pictureIconGame
        // 
        pictureIconGame.Location = new Point(548, 308);
        pictureIconGame.Name = "pictureIconGame";
        pictureIconGame.Size = new Size(100, 100);
        pictureIconGame.SizeMode = PictureBoxSizeMode.Zoom;
        pictureIconGame.TabIndex = 19;
        pictureIconGame.TabStop = false;
        // 
        // tabAbasCadastros
        // 
        tabAbasCadastros.Controls.Add(tabPageCdtAtalho);
        tabAbasCadastros.Controls.Add(tabPageCdtApps);
        tabAbasCadastros.Dock = DockStyle.Fill;
        tabAbasCadastros.Location = new Point(0, 0);
        tabAbasCadastros.Name = "tabAbasCadastros";
        tabAbasCadastros.SelectedIndex = 0;
        tabAbasCadastros.Size = new Size(677, 553);
        tabAbasCadastros.TabIndex = 20;
        // 
        // tabPageCdtAtalho
        // 
        tabPageCdtAtalho.Controls.Add(nomedojogo);
        tabPageCdtAtalho.Controls.Add(pictureIconGame);
        tabPageCdtAtalho.Controls.Add(nomeGame);
        tabPageCdtAtalho.Controls.Add(btnIconGoogle);
        tabPageCdtAtalho.Controls.Add(caminhoGame);
        tabPageCdtAtalho.Controls.Add(btnIconLocal);
        tabPageCdtAtalho.Controls.Add(caminhodojogo);
        tabPageCdtAtalho.Controls.Add(iconDoJogo);
        tabPageCdtAtalho.Controls.Add(btnSalvar);
        tabPageCdtAtalho.Controls.Add(iconGame);
        tabPageCdtAtalho.Controls.Add(btnCancelar);
        tabPageCdtAtalho.Controls.Add(btnImportAtalho);
        tabPageCdtAtalho.Controls.Add(parametroGame);
        tabPageCdtAtalho.Controls.Add(pictureImgGame);
        tabPageCdtAtalho.Controls.Add(parametrodojogo);
        tabPageCdtAtalho.Controls.Add(btnImgGoogle);
        tabPageCdtAtalho.Controls.Add(btnProcurarArq);
        tabPageCdtAtalho.Controls.Add(btnImgLocal);
        tabPageCdtAtalho.Controls.Add(imgGame);
        tabPageCdtAtalho.Controls.Add(imgdoJogo);
        tabPageCdtAtalho.Location = new Point(4, 24);
        tabPageCdtAtalho.Name = "tabPageCdtAtalho";
        tabPageCdtAtalho.Padding = new Padding(3);
        tabPageCdtAtalho.Size = new Size(669, 525);
        tabPageCdtAtalho.TabIndex = 0;
        tabPageCdtAtalho.Text = "Atalhos";
        tabPageCdtAtalho.UseVisualStyleBackColor = true;
        // 
        // tabPageCdtApps
        // 
        tabPageCdtApps.Controls.Add(btnURLExtApp);
        tabPageCdtApps.Controls.Add(btnAjudaApp);
        tabPageCdtApps.Controls.Add(lblNomeApp);
        tabPageCdtApps.Controls.Add(picImgIconeApp);
        tabPageCdtApps.Controls.Add(txtbxNomeApp);
        tabPageCdtApps.Controls.Add(btnIconOnlineApp);
        tabPageCdtApps.Controls.Add(txtbxURLApp);
        tabPageCdtApps.Controls.Add(btnIconLocalApp);
        tabPageCdtApps.Controls.Add(lblURLApp);
        tabPageCdtApps.Controls.Add(lblImgIconeApp);
        tabPageCdtApps.Controls.Add(btnSalvarApp);
        tabPageCdtApps.Controls.Add(txtbxImgIconeApp);
        tabPageCdtApps.Controls.Add(btnCancelarApp);
        tabPageCdtApps.Location = new Point(4, 24);
        tabPageCdtApps.Name = "tabPageCdtApps";
        tabPageCdtApps.Padding = new Padding(3);
        tabPageCdtApps.Size = new Size(669, 525);
        tabPageCdtApps.TabIndex = 1;
        tabPageCdtApps.Text = "Aplicativos";
        tabPageCdtApps.UseVisualStyleBackColor = true;
        // 
        // btnURLExtApp
        // 
        btnURLExtApp.Location = new Point(528, 99);
        btnURLExtApp.Name = "btnURLExtApp";
        btnURLExtApp.Size = new Size(119, 23);
        btnURLExtApp.TabIndex = 2;
        btnURLExtApp.Text = "Procurar Codigo";
        btnURLExtApp.UseVisualStyleBackColor = true;
        // 
        // btnAjudaApp
        // 
        btnAjudaApp.Location = new Point(405, 421);
        btnAjudaApp.Name = "btnAjudaApp";
        btnAjudaApp.Size = new Size(244, 41);
        btnAjudaApp.TabIndex = 6;
        btnAjudaApp.Text = "Ajuda";
        btnAjudaApp.UseVisualStyleBackColor = true;
        // 
        // lblNomeApp
        // 
        lblNomeApp.AutoSize = true;
        lblNomeApp.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblNomeApp.Location = new Point(18, 7);
        lblNomeApp.Name = "lblNomeApp";
        lblNomeApp.Size = new Size(210, 30);
        lblNomeApp.TabIndex = 22;
        lblNomeApp.Text = "Nome do Aplicativo";
        // 
        // picImgIconeApp
        // 
        picImgIconeApp.Location = new Point(18, 186);
        picImgIconeApp.Name = "picImgIconeApp";
        picImgIconeApp.Size = new Size(323, 323);
        picImgIconeApp.SizeMode = PictureBoxSizeMode.Zoom;
        picImgIconeApp.TabIndex = 32;
        picImgIconeApp.TabStop = false;
        // 
        // txtbxNomeApp
        // 
        txtbxNomeApp.Location = new Point(18, 40);
        txtbxNomeApp.Name = "txtbxNomeApp";
        txtbxNomeApp.PlaceholderText = "Nome do Aplicativo";
        txtbxNomeApp.Size = new Size(629, 23);
        txtbxNomeApp.TabIndex = 0;
        // 
        // btnIconOnlineApp
        // 
        btnIconOnlineApp.Location = new Point(403, 156);
        btnIconOnlineApp.Name = "btnIconOnlineApp";
        btnIconOnlineApp.Size = new Size(119, 23);
        btnIconOnlineApp.TabIndex = 4;
        btnIconOnlineApp.Text = "Pesquisar online";
        btnIconOnlineApp.UseVisualStyleBackColor = true;
        // 
        // txtbxURLApp
        // 
        txtbxURLApp.Location = new Point(18, 99);
        txtbxURLApp.Name = "txtbxURLApp";
        txtbxURLApp.PlaceholderText = "URL ou URI para abrir app";
        txtbxURLApp.Size = new Size(504, 23);
        txtbxURLApp.TabIndex = 1;
        // 
        // btnIconLocalApp
        // 
        btnIconLocalApp.Location = new Point(528, 156);
        btnIconLocalApp.Name = "btnIconLocalApp";
        btnIconLocalApp.Size = new Size(119, 23);
        btnIconLocalApp.TabIndex = 5;
        btnIconLocalApp.Text = "Procurar";
        btnIconLocalApp.UseVisualStyleBackColor = true;
        // 
        // lblURLApp
        // 
        lblURLApp.AutoSize = true;
        lblURLApp.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblURLApp.Location = new Point(18, 66);
        lblURLApp.Name = "lblURLApp";
        lblURLApp.Size = new Size(190, 30);
        lblURLApp.TabIndex = 23;
        lblURLApp.Text = "URL do Aplicativo";
        // 
        // lblImgIconeApp
        // 
        lblImgIconeApp.AutoSize = true;
        lblImgIconeApp.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblImgIconeApp.Location = new Point(18, 124);
        lblImgIconeApp.Name = "lblImgIconeApp";
        lblImgIconeApp.Size = new Size(227, 30);
        lblImgIconeApp.TabIndex = 29;
        lblImgIconeApp.Text = "Imagem do icone app";
        // 
        // btnSalvarApp
        // 
        btnSalvarApp.Location = new Point(405, 468);
        btnSalvarApp.Name = "btnSalvarApp";
        btnSalvarApp.Size = new Size(119, 41);
        btnSalvarApp.TabIndex = 7;
        btnSalvarApp.Text = "Salvar";
        btnSalvarApp.UseVisualStyleBackColor = true;
        // 
        // txtbxImgIconeApp
        // 
        txtbxImgIconeApp.Location = new Point(18, 157);
        txtbxImgIconeApp.Name = "txtbxImgIconeApp";
        txtbxImgIconeApp.PlaceholderText = "Icone";
        txtbxImgIconeApp.Size = new Size(379, 23);
        txtbxImgIconeApp.TabIndex = 3;
        // 
        // btnCancelarApp
        // 
        btnCancelarApp.Location = new Point(530, 468);
        btnCancelarApp.Name = "btnCancelarApp";
        btnCancelarApp.Size = new Size(119, 41);
        btnCancelarApp.TabIndex = 8;
        btnCancelarApp.Text = "Cancelar";
        btnCancelarApp.UseVisualStyleBackColor = true;
        // 
        // Form2
        // 
        ClientSize = new Size(677, 553);
        Controls.Add(tabAbasCadastros);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "Form2";
        Text = "Cadastro de Atalho";
        ((System.ComponentModel.ISupportInitialize)pictureImgGame).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureIconGame).EndInit();
        tabAbasCadastros.ResumeLayout(false);
        tabPageCdtAtalho.ResumeLayout(false);
        tabPageCdtAtalho.PerformLayout();
        tabPageCdtApps.ResumeLayout(false);
        tabPageCdtApps.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picImgIconeApp).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TextBox nomeGame;
    private TextBox caminhoGame;
    private OpenFileDialog openFileDialog1;
    private Label nomedojogo;
    private Label caminhodojogo;
    private Button btnSalvar;
    private Button btnCancelar;
    private Label parametrodojogo;
    private TextBox parametroGame;
    private Button btnProcurarArq;
    private Label imgdoJogo;
    private TextBox imgGame;
    private Button btnImgLocal;
    private Button btnImgGoogle;
    private PictureBox pictureImgGame;
    private Button btnImportAtalho;
    private Button btnIconGoogle;
    private Button btnIconLocal;
    private Label iconDoJogo;
    private TextBox iconGame;
    private PictureBox pictureIconGame;
    private TabControl tabAbasCadastros;
    private TabPage tabPageCdtAtalho;
    private TabPage tabPageCdtApps;
    private Label lblNomeApp;
    private PictureBox picImgIconeApp;
    private TextBox txtbxNomeApp;
    private Button btnIconOnlineApp;
    private TextBox txtbxURLApp;
    private Button btnIconLocalApp;
    private Label lblURLApp;
    private Label lblImgIconeApp;
    private Button btnSalvarApp;
    private TextBox txtbxImgIconeApp;
    private Button btnCancelarApp;
    private Button btnAjudaApp;
    private Button btnURLExtApp;
}

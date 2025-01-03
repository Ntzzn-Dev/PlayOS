namespace PlaySO;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using Microsoft.Win32;
using System.Security.Principal;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

public partial class Form3 : Form
{
    private ArrayList ids = new ArrayList();
    private List<Aplicativos> appsContext = new List<Aplicativos>();
    private Atalhos atalhoAtual;
    private int idAtual = 1;
    private NotifyIcon notifyIcon;
    private Image imgAtual, iconAtual;
    private bool appsOcultos = true;
    int heightPnlApps;
    public Form3()
    {
        this.WindowState = FormWindowState.Maximized;
        this.FormBorderStyle = FormBorderStyle.None;

        InitializeComponent();

        PegarIds();

        DefinirGatilhos();

        CriarNotificacao();
    }

    private void DefinirGatilhos(){
        this.Load += AoCarregar;

        btnAbrir.Click += BtnAbrirAtalho;
        btnFechar.Click += (s, e) => this.Close();
        btnEditar.Click += BtnEditar;
        btnAdicionar.Click += (s, e) => AdicionarCadastro();
        btnDeletar.Click += BtnDeletar;

        btnNext.Click += (s, e) => GameNext();
        btnPrev.Click += (s, e) => GamePrev();

        picPuxarApps.Click += BtnPicAlternarApps;

        pnlAppTransition.Tick += PicAlternarApps_Transition;
    }
    private void CriarNotificacao()
    {
        notifyIcon = new NotifyIcon();
        notifyIcon.Icon = SystemIcons.Information;
        notifyIcon.Visible = true;

        ContextMenuStrip contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Restaurar", null, RestaurarForm);
        contextMenu.Items.Add("Sair", null, SairApp);
        notifyIcon.ContextMenuStrip = contextMenu;
    }
    private void AoCarregar(object sender, EventArgs e)
    {
        IMGSwap(imgAtual);
        DefinirCorDeFundo(imgAtual);
        PegarApps();
    }
    private void DefinirCorDeFundo(Image imgData)
    {
        if (imgData == null)
            return;

        Bitmap bitmap = new Bitmap(imgData);
        Color corBordaSuperior = bitmap.GetPixel(bitmap.Width / 2, 0);
        Color corBordaInferior = bitmap.GetPixel(bitmap.Width / 2, bitmap.Height - 1);
        Color corBordaEsquerda = bitmap.GetPixel(0, bitmap.Height / 2);
        Color corBordaDireita = bitmap.GetPixel(bitmap.Width - 1, bitmap.Height / 2);

        int r = (corBordaSuperior.R + corBordaInferior.R + corBordaEsquerda.R + corBordaDireita.R) / 4;
        int g = (corBordaSuperior.G + corBordaInferior.G + corBordaEsquerda.G + corBordaDireita.G) / 4;
        int b = (corBordaSuperior.B + corBordaInferior.B + corBordaEsquerda.B + corBordaDireita.B) / 4;

        pictureBox1.BackColor = Color.FromArgb(r, g, b);
    }
    private void PegarIds()
    {
        ids.Clear();
        try
        {
            string connectionString = "Data Source=applicationsShortcuts.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string selectCommand = "SELECT Id FROM AtalhosdeAplicativos";
                using (var command = new SqliteCommand(selectCommand, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);

                        ids.Add(id);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao listar atalhos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        if (ids.Count > 0)
        {
            if(!ids.Contains(idAtual)){
                idAtual = (int)ids[0];
            }
            ListarAtalho(idAtual);
        } else {
            AdicionarCadastro();
        }

        //ListarAtalho(idAtual);
    }
    private void ListarAtalho(int idatual)
    {
        Atalhos atalhoContextual = new Atalhos(idatual);
        atalhoAtual = atalhoContextual;
        imgAtual = atalhoAtual.getImgAtalho();
        iconAtual = atalhoAtual.getIconeAtalho();

        AtualizarLabel(atalhoAtual.getNomeAtalho(), nameGame);
        AtualizarLabel(atalhoAtual.getCaminhoAtalho(), pathGame);

        IMGSwap(imgAtual);
        DefinirCorDeFundo(imgAtual);
        ArredondarBordas(iconPic);
        iconPic.Image = iconAtual;
    }
    private void AtualizarLabel(string novoTexto, Label textToEdit)
    {
        int antigaLargura = textToEdit.Width;
        textToEdit.Text = novoTexto;

        int novaLargura = textToEdit.Width;
        int deslocamento = novaLargura - antigaLargura;

        textToEdit.Left -= deslocamento;
    }
    private void IMGSwap(Image imgData)
    {
        Bitmap original = new Bitmap(imgData);

        Bitmap vinheta = new Bitmap(@".\Assets\Vinheta.png");

        Bitmap resultado = new Bitmap(pictureBox1.Width, pictureBox1.Height);

        using (Graphics g = Graphics.FromImage(resultado))
        {
            float scaleXOr = (float)pictureBox1.Width / original.Width;
            float scaleYOr = (float)pictureBox1.Height / original.Height;
            float scaleOr = Math.Min(scaleXOr, scaleYOr);

            int originalWidth = (int)(original.Width * scaleOr);
            int originalHeight = (int)(original.Height * scaleOr);

            int posXOr = (pictureBox1.Width - originalWidth) / 2;
            int posYOr = (pictureBox1.Height - originalHeight) / 2;

            g.DrawImage(original,
                new Rectangle(posXOr, posYOr, originalWidth, originalHeight),
                new Rectangle(0, 0, original.Width, original.Height),
                GraphicsUnit.Pixel);

            g.DrawImage(vinheta,
                new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height),
                new Rectangle(0, 0, vinheta.Width, vinheta.Height),
                GraphicsUnit.Pixel);
        }
        pictureBox1.Image = resultado;
    }
    private void BtnPicAlternarApps(object sender, EventArgs e)
    {
        pnlAppTransition.Start();      
        heightPnlApps = pnlApps.Size.Height;  
    }
    private void PicAlternarApps_Transition(object sender, EventArgs e)
    {
        int meioDaTela = (this.Width/2) - picPuxarApps.Width/2;
        if(appsOcultos)
        {
            heightPnlApps += 10;
            pnlApps.Size = new Size(this.Width, heightPnlApps);
            picPuxarApps.Location = new Point(meioDaTela, pnlApps.Height);
            if(pnlApps.Size.Height >= 150)
            {
                pnlApps.Size = new Size(this.Width, 150);
                picPuxarApps.Location = new Point(meioDaTela, pnlApps.Height);
                appsOcultos = false;
                pnlAppTransition.Stop();
            }
        }
        else if(appsOcultos == false)
        {
            heightPnlApps -= 10;
            pnlApps.Size = new Size(this.Width, heightPnlApps);
            picPuxarApps.Location = new Point(meioDaTela, pnlApps.Height);
            if(pnlApps.Size.Height <= 0)
            {
                pnlApps.Size = new Size(this.Width, 0);
                picPuxarApps.Location = new Point(meioDaTela, pnlApps.Height);
                appsOcultos = true;
                pnlAppTransition.Stop();
            }
        }
    }
    private void BtnAbrirAtalho(object sender, EventArgs e)
    {
        try
        {
            bool reabrirLaucher = true;
            string permissao = "runas";
            
            if(atalhoAtual.getCaminhoAtalho().Contains("epicgames:") || atalhoAtual.getCaminhoAtalho().Contains("steam:"))
            {
                reabrirLaucher = false;
                permissao = "";
            }
            if(atalhoAtual.getCaminhoAtalho().Contains("XboxGames"))
            {
                reabrirLaucher = false;
            }

            string diretorioTrabalho = System.IO.Path.GetDirectoryName(atalhoAtual.getCaminhoAtalho());

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = atalhoAtual.getCaminhoAtalho(),
                WorkingDirectory = diretorioTrabalho,
                Arguments = atalhoAtual.getParametroAtalho(),
                Verb = permissao,
                UseShellExecute = true
            };

            Process jogoAberto = Process.Start(psi);

            if (jogoAberto != null && reabrirLaucher)
            {
                jogoAberto.EnableRaisingEvents = true;
                jogoAberto.Exited += (s, args) =>
                {
                    Invoke(new Action(() =>
                    {
                        this.Show();
                        this.WindowState = FormWindowState.Maximized;
                    }));
                };

                MandarPraBandeja();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao abrir o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void RestaurarForm(object sender, EventArgs e)
    {
        this.Show();
        this.WindowState = FormWindowState.Maximized;
    }
    private void SairApp(object sender, EventArgs e)
    {
        Application.Exit();
    }
    private void MandarPraBandeja()
    {
        this.Hide();
        notifyIcon.ShowBalloonTip(1000, "Aplicativo Minimizado", "Clique para restaurar", ToolTipIcon.Info);
    }
    private bool GameNext()
    {
        int indiceAtual = ids.IndexOf(idAtual);
        if (indiceAtual < ids.Count - 1)
        {
            idAtual = (int)ids[indiceAtual + 1];
            ListarAtalho(idAtual);
            return true;
        }

        return false;
    }
    private bool GamePrev()
    {
        int indiceAtual = ids.IndexOf(idAtual);
        if (indiceAtual > 0)
        {
            idAtual = (int)ids[indiceAtual - 1];
            ListarAtalho(idAtual);
            return true;
        }

        return false;
    }
    private void SetUAC(int enable)
    {
        if (!IsAdministrator())
        {
            MessageBox.Show("Execute o programa como administrador para alterar o UAC.", "Permissão Necessária", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            const string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
            {
                if (key != null)
                {
                    key.SetValue("EnableLUA", enable, RegistryValueKind.DWord);
                    MessageBox.Show($"UAC {(enable == 1 ? "ativado" : "desativado")}. Reinicie o computador para aplicar as mudanças.", "Alteração Bem-sucedida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não foi possível acessar o registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao alterar o UAC: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private bool IsAdministrator()
    {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
        {
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
    private void AdicionarCadastro()
    {
        Form2 telaCadastro = new Form2();
        telaCadastro.FormClosed += (s, e) => { PegarIds(); PegarApps(); };
        telaCadastro.Owner = this;
        telaCadastro.ShowDialog();
    }
    private void BtnEditar(object sender, EventArgs e)
    {
        Form2 telaCadastro = new Form2(idAtual, 0);
        telaCadastro.FormClosed += (s, e) => PegarIds();
        telaCadastro.Owner = this;
        telaCadastro.ShowDialog();
    }
    private void BtnDeletar(object sender, EventArgs e)
    {
        Atalhos atalhoAtual = new Atalhos();
        atalhoAtual.Deletar(idAtual);

        if (!GameNext())
        {
            GamePrev();
        }
        
        PegarIds();
    }
    private void AddApp(){
        for(int i = 0; i < appsContext.Count; i++){
            pnlApps.Controls.Add(CriacaoApp(appsContext[i], OrganizacaoApps(appsContext.Count, i+1)));
        }

        appsContext.Clear();
    }
    private void PegarApps()
    {
        pnlApps.Controls.Clear();
        ArrayList idsApps = new ArrayList();
        try
        {
            string connectionString = "Data Source=applicationsShortcuts.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string selectCommand = "SELECT Id FROM AplicativosExtras";
                using (var command = new SqliteCommand(selectCommand, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);

                        idsApps.Add(id);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao listar atalhos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        foreach(int id in idsApps){
            Aplicativos appAtual = new Aplicativos(id);
            appsContext.Add(appAtual);
        }

        AddApp();
    }
    private Point OrganizacaoApps(int quantidadeDeApps, int posicaoDoApp)
    {
        int tamanhoApp = 75;
        int margemApps = 48;
        int meiaTela = this.Width / 2;
        int posicao = meiaTela - tamanhoApp / 2;

        if(quantidadeDeApps%2 == 0){
            posicao += 62; //Centraliza para quantidades pares
        }

        int metadeDosApps = quantidadeDeApps / 2 + 1;

        //posicao diminui o tamanho do panel * posicao em relacao a metade - margem de distancia entre os apps
        posicao += -tamanhoApp * (metadeDosApps - posicaoDoApp) -margemApps * (metadeDosApps - posicaoDoApp);

        return new Point(posicao, 12);
    }
    private Panel CriacaoApp(Aplicativos appEmUso, Point localDoAplicativo){
        PictureBox picAppIcon = new PictureBox{
            Image = appEmUso.getIconeAplicativo(),
            Location = new Point(0, 0),
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Name = "picIconApp_" + appEmUso.getNomeAplicativo() + ">" + appEmUso.getIdAplicativo(),
            Size = new Size(75, 75),
            SizeMode = PictureBoxSizeMode.Zoom
        };
        ArredondarBordas(picAppIcon);
        //
        Label lblAppNome = new Label{
            Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
            Anchor = AnchorStyles.Top,
            BackColor = Color.Transparent,
            Location = new Point(0, 75),
            Margin = new Padding(0),
            Name = "lblNomeApp_" + appEmUso.getNomeAplicativo() + ">" + appEmUso.getIdAplicativo(),
            Size = new Size(75, 48),
            TabIndex = 22,
            Text = appEmUso.getNomeAplicativo(),
            TextAlign = ContentAlignment.MiddleCenter
        };
        //
        Panel pnlBackground = new Panel{
            Location = localDoAplicativo,
            Margin = new Padding(0),
            Name = "pnl_" + appEmUso.getNomeAplicativo() + ">" + appEmUso.getIdAplicativo(),
            Size = new Size(75, 123),
            TabIndex = 21
        };
        pnlBackground.Controls.Add(picAppIcon);
        pnlBackground.Controls.Add(lblAppNome);

        Color cor = Color.FromArgb(67, 0, 0, 0);

        pnlBackground.MouseEnter += (s, e) => pnlBackground.BackColor = cor;
        lblAppNome.MouseEnter += (s, e) => pnlBackground.BackColor = cor;
        picAppIcon.MouseEnter += (s, e) => pnlBackground.BackColor = cor;
        pnlBackground.MouseLeave += (s, e) => pnlBackground.BackColor = Color.Transparent;
        lblAppNome.MouseLeave += (s, e) => pnlBackground.BackColor = Color.Transparent;
        picAppIcon.MouseLeave += (s, e) => pnlBackground.BackColor = Color.Transparent;

        picAppIcon.MouseClick += (s, e) =>
        {
            if (e.Button == MouseButtons.Right)
            {
                CriarPopup(appEmUso.getIdAplicativo());
            }
            if (e.Button == MouseButtons.Left)
            {
                BtnAbrirExtraApp(s, e);
            }
        };
        lblAppNome.MouseClick += (s, e) =>
        {
            if (e.Button == MouseButtons.Right)
            {
                CriarPopup(appEmUso.getIdAplicativo());
            }
            if (e.Button == MouseButtons.Left)
            {
                BtnAbrirExtraApp(s, e);
            }
        };

        return pnlBackground;
    }
    private void ArredondarBordas(PictureBox pictureBox)
    {
        int cornerRadius = 30;
        using (GraphicsPath gp = new GraphicsPath())
        {
            gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90); // Canto superior esquerdo
            gp.AddArc(pictureBox.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90); // Canto superior direito
            gp.AddArc(pictureBox.Width - cornerRadius, pictureBox.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90); // Inferior direito
            gp.AddArc(0, pictureBox.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90); // Inferior esquerdo
            gp.CloseFigure();

            pictureBox.Region = new Region(gp);
        }
    }
    private void BtnAbrirExtraApp(object sender, EventArgs e){
        PictureBox pic = sender as PictureBox;
        Label lbl = sender as Label;
        int id = 0;
        if (pic != null && pic.Name.Contains(">"))
        {
            id = int.Parse(pic.Name.Split(">")[1]);
        } else
        if (lbl != null && lbl.Name.Contains(">"))
        {
            id = int.Parse(lbl.Name.Split(">")[1]);
        }

        try
        {
            Aplicativos appPraAbrir = new Aplicativos(id);

            string url = appPraAbrir.getCaminhoAplicativo();

            string diretorioTrabalho = System.IO.Path.GetDirectoryName(url);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                WorkingDirectory = diretorioTrabalho,
                UseShellExecute = true
            };

            Process.Start(psi);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao abrir o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void CriarPopup(int id)
    {
        Form popup = new Form
        {
            FormBorderStyle = FormBorderStyle.None,
            StartPosition = FormStartPosition.Manual,
            ShowInTaskbar = false,
            TopMost = true,
            Size = new Size(284, 150),
            BackColor = Color.FromArgb(255, 51, 51, 51)
        };
        Label lblAbrirApp = new Label
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            BackColor = Color.FromArgb(63, 63, 63),
            Font = new Font("Segoe UI Black", 20F, FontStyle.Bold, GraphicsUnit.Point, 0),
            Location = new Point(12, 12),
            Margin = new Padding(3),
            Name = "lblAbrirApp>" + id,
            Size = new Size(260, 38),
            TabIndex = 0,
            Text = "Abrir",
            TextAlign = ContentAlignment.MiddleCenter
        };
        Label lblEditarApp = new Label
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            BackColor = Color.Transparent,
            Font = new Font("Segoe UI Black", 20F, FontStyle.Bold, GraphicsUnit.Point, 0),
            Location = new Point(12, 56),
            Margin = new Padding(3),
            Name = "lblEditarApp>" + id,
            Size = new Size(260, 38),
            TabIndex = 1,
            Text = "Editar",
            TextAlign = ContentAlignment.MiddleCenter
        };
        Label lblApagarApp = new Label
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            BackColor = Color.FromArgb(63, 63, 63),
            Font = new Font("Segoe UI Black", 20F, FontStyle.Bold, GraphicsUnit.Point, 0),
            Location = new Point(12, 100),
            Margin = new Padding(3),
            Name = "lblApagarApp>" + id,
            Size = new Size(260, 38),
            TabIndex = 2,
            Text = "Apagar",
            TextAlign = ContentAlignment.MiddleCenter
        };
        popup.Controls.Add(lblAbrirApp);
        popup.Controls.Add(lblEditarApp);
        popup.Controls.Add(lblApagarApp);

        lblAbrirApp.Click += BtnAbrirExtraApp;
        lblEditarApp.Click += BtnEditarApp;
        lblApagarApp.Click += (s, e) => pnlApps.Controls.Clear();

        popup.Location = Cursor.Position;

        Color cor = Color.FromArgb(67, 0, 0, 0);

        lblAbrirApp.MouseEnter += (s, e) => lblAbrirApp.BackColor = Color.FromArgb(109, 109, 110);
        lblEditarApp.MouseEnter += (s, e) => lblEditarApp.BackColor = Color.FromArgb(109, 109, 110);
        lblApagarApp.MouseEnter += (s, e) => lblApagarApp.BackColor = Color.FromArgb(109, 109, 110);
        lblAbrirApp.MouseLeave += (s, e) => lblAbrirApp.BackColor = Color.FromArgb(63, 63, 63);
        lblEditarApp.MouseLeave += (s, e) => lblEditarApp.BackColor = Color.Transparent;
        lblApagarApp.MouseLeave += (s, e) => lblApagarApp.BackColor = Color.FromArgb(63, 63, 63);

        popup.Load += (s, e) => popup.Deactivate += (s, e) => popup.Close();

        popup.Show();
    }
    private void BtnEditarApp(object sender, EventArgs e)
    {
        Label lbl = sender as Label;
        int id = int.Parse(lbl.Name.Split(">")[1]);

        Form2 telaCadastro = new Form2(id, 1);
        telaCadastro.FormClosed += (s, e) => PegarApps();
        telaCadastro.Owner = this;
        telaCadastro.Show();
    }
}
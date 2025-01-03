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
using System.Drawing.Drawing2D;
using System.Timers;
using System.Runtime.InteropServices;

public partial class Form3 : Form
{
    // Atalhos ========================
    private ArrayList ids = new ArrayList();
    private Atalhos atalhoAtual;
    private Process jogoAberto;
    private int idAtual = 1;
    // Mandeja ========================
    private NotifyIcon notifyIcon;
    // Painel APPS ====================
    private bool appsOcultos = true;
    int heightPnlApps;
    // Temporizadores =================
    private static System.Timers.Timer temporizadorDoRelogio, timerProcessoEstabilizar;
    private static DateTime horario;
    // Hotkeys e DLL ==================
    private const int WM_HOTKEY = 0x0312;

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    [DllImport("user32.dll")]
    private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    public Form3()
    {
        this.WindowState = FormWindowState.Maximized;
        this.FormBorderStyle = FormBorderStyle.None;

        InitializeComponent();

        AtalhoPegarIds();

        DefinirGatilhos();

        CriarNotificacao();

        CriarRelogio();
        
        RegisterHotKey(this.Handle, 1, 0, (uint)Keys.NumPad5);                                              //Numpad5
        RegisterHotKey(this.Handle, 2, (uint)ModifierKeys.Control | (uint)ModifierKeys.Alt , (uint)Keys.T); //Ctrl+Alt+T
    }

    // Atalho para voltar ao app minimizado //
    
    [Flags]
    public enum ModifierKeys : uint
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8
    }
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == WM_HOTKEY)
        {
            int idDoAtalho = m.WParam.ToInt32();
            if (idDoAtalho == 1)
            {
                FecharAtalho();
                this.Show();
                this.WindowState = FormWindowState.Maximized;
            }
            else if (idDoAtalho == 2) {
                MessageBox.Show("n fechar");
            }
        }
    }
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        UnregisterHotKey(this.Handle, 1);
        UnregisterHotKey(this.Handle, 2);
        base.OnFormClosing(e);
    }
    
    // Verificar jogo Maximizado //
    
    [StructLayout(LayoutKind.Sequential)]
    private struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public int showCmd;
        public POINT ptMinPosition;
        public POINT ptMaxPosition;
        public RECT rcNormalPosition;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int x;
        public int y;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
    public static bool IsFullscreenWithoutBorders(Process processo)
    {
        IntPtr handle = processo.MainWindowHandle;
        if (handle == IntPtr.Zero)
            return false; // Sem janela principal.

        if (GetWindowRect(handle, out RECT rect))
        {
            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            // Obter a resolução da tela principal.
            int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            // Comparar dimensões e posição.
            return width == screenWidth && height == screenHeight && rect.left == 0 && rect.top == 0;
        }

        return false;
    }

    // Substituir acao das teclas //

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Left)
        {
            GamePrev();
            return true;
        }
        else if (keyData == Keys.Right)
        {
            GameNext();
            return true;
        }
        if (keyData == Keys.Up)
        {
            appsOcultos = false;
            pnlAppTransition.Start();
            heightPnlApps = pnlApps.Size.Height;
            return true;
        }
        else if (keyData == Keys.Down)
        {
            appsOcultos = true;
            pnlAppTransition.Start();
            heightPnlApps = pnlApps.Size.Height;
            return true;
        }
        else if (keyData == Keys.Space)
        {
            BtnAbrirAtalho(null, null);
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }
    
    // Inicio -------------------------------------------------------------------------------------
    private void DefinirGatilhos(){
        this.Load += AoCarregar;

        btnAbrir.Click += BtnAbrirAtalho;
        btnFechar.Click += (s, e) => this.Close();
        btnEditar.Click += BtnEditarAtalho;
        btnAdicionar.Click += (s, e) => Cadastrar();
        btnDeletar.Click += BtnDeletarAtalho;

        btnNext.Click += (s, e) => GameNext();
        btnPrev.Click += (s, e) => GamePrev();

        picPuxarApps.Click += BtnAlternarAppsOcultos;

        pnlAppTransition.Tick += TransicaoAppsOcultos;
    }
    private void AoCarregar(object sender, EventArgs e)
    {
        if(atalhoAtual != null){
            PicMergeImages(atalhoAtual.getImgAtalho());
            PicDefinirCorDeFundo(atalhoAtual.getImgAtalho(), pictureBox1);
        }
        picPuxarApps.Image = Image.FromFile(Referencias.caminhoPicAppsShow);
        PicArredondarBordas(picPuxarApps, 0, 0, 30, 30);
        PegarApps();
    }
    private void Cadastrar()
    {
        Form2 telaCadastro = new Form2();
        telaCadastro.FormClosed += (s, e) => { AtalhoPegarIds(); PegarApps(); };
        telaCadastro.Owner = this;
        telaCadastro.Show();
    }

    // Criar icone e notificacao ------------------------------------------------------------------
    private void CriarNotificacao()
    {
        notifyIcon = new NotifyIcon();
        notifyIcon.Icon = SystemIcons.Information;
        notifyIcon.Visible = true;

        ContextMenuStrip contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Restaurar", null, RestaurarPlayOS);
        contextMenu.Items.Add("Sair", null, SairPlayOS);
        notifyIcon.ContextMenuStrip = contextMenu;
    }
    private void RestaurarPlayOS(object sender, EventArgs e)
    {
        this.Show();
        this.WindowState = FormWindowState.Maximized;
    }
    private void SairPlayOS(object sender, EventArgs e)
    {
        Application.Exit();
    }
    private void MandarPraBandeja()
    {
        this.Hide();
        notifyIcon.ShowBalloonTip(1000, "Aplicativo Minimizado", "Clique para restaurar", ToolTipIcon.Info);
        timerProcessoEstabilizar.Enabled = false;
    }

    // Atalho atual -------------------------------------------------------------------------------
    private void AtalhoPegarIds()
    {
        ids.Clear();
        try
        {
            ids = Atalhos.ConsultarIDs();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao listar ids: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        if (ids.Count > 0)
        {
            if (!ids.Contains(idAtual)) { idAtual = (int)ids[0]; }
            AtalhoListar(idAtual);
        } else {
            Cadastrar();
        }
    }
    private void AtalhoListar(int idatual)
    {
        atalhoAtual = new Atalhos(idatual);

        LblPosicionarCorretamente(atalhoAtual.getNomeAtalho(), nameGame);
        LblPosicionarCorretamente(atalhoAtual.getCaminhoAtalho(), pathGame);

        PicMergeImages(atalhoAtual.getImgAtalho());
        PicDefinirCorDeFundo(atalhoAtual.getImgAtalho(), pictureBox1);
        PicArredondarBordas(iconPic, 30, 30, 30, 30);
        iconPic.Image = atalhoAtual.getIconeAtalho();
    }
    private string GetEpicGames()
    {
        string registryKey = @"HKEY_CLASSES_ROOT\com.epicgames.launcher\shell\open\command";

        string command = (string)Registry.GetValue(registryKey, null, string.Empty);

        if (!string.IsNullOrEmpty(command))
        {
            string[] parts = command.Split(" %");

            string commandToRun = parts[0].Trim();

            return commandToRun;
        }

        return null;
    }
    private void AbrirEpicGames(string caminho,string diretorioTrabalho,string argumentacao,string permissao){
        this.TopMost = true;
        Process.Start(caminho);

        System.Timers.Timer temporizadorEpicAberta = new System.Timers.Timer(7000);
        temporizadorEpicAberta.Elapsed += (s, e) =>
        {
            var processosAtuais = Process.GetProcesses();
            Process epic = null;
            foreach (var processo in processosAtuais)
            {
                if (processo.ProcessName.ToLower().Contains("epicgameslauncher"))
                {
                    epic = processo;
                    break;
                }
            }
            if (epic != null)
            {
                temporizadorEpicAberta.Enabled = false;
                this.TopMost = false;
                epic.CloseMainWindow();
                AbrirAtalho(caminho, diretorioTrabalho, argumentacao, permissao);
            }
            else
            {
                MessageBox.Show("Epic Games Launcher não foi encontrado.");
            }
        };
        
        temporizadorEpicAberta.Enabled = true;

    }
    private void AbrirAtalho (string caminho,string diretorioTrabalho,string argumentacao,string permissao){
        try{
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = caminho,
                WorkingDirectory = diretorioTrabalho,
                Arguments = argumentacao,
                Verb = permissao,
                UseShellExecute = true
            };
            jogoAberto = Process.Start(psi);

            MonitoramentoDeProcesos();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao abrir o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void FecharAtalho()
    {
        if (jogoAberto != null && !jogoAberto.HasExited)
        {
            jogoAberto.CloseMainWindow();
            if (!jogoAberto.HasExited)
            {
                jogoAberto.Kill();
            }
        }
    }
    private void AoFecharAtalho(object sender, EventArgs e)
    {
        var processo = sender as Process;
        if (processo != null)
        {
            TimeSpan tempoDecorrido = DateTime.Now - processo.StartTime;
            MessageBox.Show($"O jogo '{processo.ProcessName}' foi fechado. Tempo de execução: {tempoDecorrido.TotalSeconds} segundos.");
        }

        jogoAberto = null;
        this.Show();
        this.WindowState = FormWindowState.Maximized;
    }
    private bool GameNext()
    {
        int indiceAtual = ids.IndexOf(idAtual);
        if (indiceAtual < ids.Count - 1)
        {
            idAtual = (int)ids[indiceAtual + 1];
            AtalhoListar(idAtual);
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
            AtalhoListar(idAtual);
            return true;
        }

        return false;
    }
    private void BtnAbrirAtalho(object sender, EventArgs e) {
        try
        {
            string caminho = atalhoAtual.getCaminhoAtalho();
            string permissao = "runas";
            string argumentacao = atalhoAtual.getParametroAtalho();
            string diretorioTrabalho = System.IO.Path.GetDirectoryName(caminho);
            if(caminho.Contains("steam:"))
            {
                permissao = "";
            }
            if(caminho.Contains("epicgames"))
            {
                argumentacao = caminho;
                caminho = GetEpicGames();
                diretorioTrabalho = System.IO.Path.GetDirectoryName(caminho);
                AbrirEpicGames(caminho, diretorioTrabalho, argumentacao, permissao);
            }else{
                AbrirAtalho(caminho, diretorioTrabalho, argumentacao, permissao);
            }
            PicGIFAbrindoJogo();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro no botao de abrir atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void BtnEditarAtalho(object sender, EventArgs e)
    {
        Form2 telaCadastro = new Form2(idAtual, 0);
        telaCadastro.FormClosed += (s, e) => AtalhoPegarIds();
        telaCadastro.Owner = this;
        telaCadastro.ShowDialog();
    }
    private void BtnDeletarAtalho(object sender, EventArgs e)
    {
        Atalhos.Deletar(idAtual);

        if (!GameNext())
        {
            GamePrev();
        }
        
        AtalhoPegarIds();
    }

    // Monitoramento de processos -----------------------------------------------------------------
    private void MonitoramentoDeProcesos(){
        timerProcessoEstabilizar = new System.Timers.Timer(5000); //Tempo maximo de monitoramento de processos
        timerProcessoEstabilizar.Elapsed += VerificacaoProcessoEstabilizado;
        timerProcessoEstabilizar.AutoReset = true;
        timerProcessoEstabilizar.Enabled = true;
    }
    private void VerificacaoProcessoEstabilizado(object sender, ElapsedEventArgs e){
        var processosAtuais = Process.GetProcesses();
        
        foreach (var processo in processosAtuais)
        {
            try{
                if (IgnorarProcesso(processo))
                        continue;

                if(IsFullscreenWithoutBorders(processo)){
                    MessageBox.Show(processo.ProcessName);
                    jogoAberto.Exited -= AoFecharAtalho;
                    jogoAberto = processo;
                    jogoAberto.EnableRaisingEvents = true;
                    jogoAberto.Exited += AoFecharAtalho;
                    PicGIFRemover();
                    MandarPraBandeja();
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar processo {processo.Id}: {ex.Message}");
            }
        }
    }
    private bool IgnorarProcesso(Process processo)
    {
        try
        {
            string[] processosIgnorados = { "chrome", "firefox", "opera", "spotify", "edge", "steam", "textinput", "code", "playos", "xbox", "dwm", "taskmgr", "protected", "playso", "discord", "settings", "explorer", "svchost", "dllhost", "taskhost", "service", "application", "explorer" };

            if (processosIgnorados.Any(nome => processo.ProcessName.ToLower().Contains(nome)))
                return true;

            return processo.MainWindowHandle == IntPtr.Zero;
        }
        catch
        {
            return true;
        }
    }

    // Aplicativos --------------------------------------------------------------------------------
    private void PegarApps()
    {
        pnlApps.Controls.Clear();
        ArrayList idsApps = Aplicativos.ConsultarIDs();
        List<Aplicativos> appsContext = new List<Aplicativos>();

        foreach(int id in idsApps){
            Aplicativos appAtual = new Aplicativos(id);
            appsContext.Add(appAtual);
        }

        PnlPnlAddApp(appsContext);
    }
    private void PnlPnlAddApp(List<Aplicativos> appsContext){
        for(int i = 0; i < appsContext.Count; i++){
            pnlApps.Controls.Add(CriacaoApp(appsContext[i], OrganizacaoApps(appsContext.Count, i+1)));
        }

        appsContext.Clear();
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
        PicArredondarBordas(picAppIcon, 30, 30, 30, 30);
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
                BtnAbrirAplicativos(s, e);
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
                BtnAbrirAplicativos(s, e);
            }
        };

        return pnlBackground;
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

        lblAbrirApp.Click += BtnAbrirAplicativos;
        lblEditarApp.Click += BtnEditarAplicativos;
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
    private void TransicaoAppsOcultos(object sender, EventArgs e)
    {
        int meioDaTela = (this.Width/2) - picPuxarApps.Width/2;
        if(appsOcultos)
        {
            heightPnlApps += 10;
            pnlApps.Size = new Size(this.Width, heightPnlApps);
            picPuxarApps.Location = new Point(meioDaTela, pnlApps.Height);
            if(pnlApps.Size.Height >= 150)
            {
                picPuxarApps.Image = Image.FromFile(Referencias.caminhoPicAppsHide);
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
                picPuxarApps.Image = Image.FromFile(Referencias.caminhoPicAppsShow);
                pnlApps.Size = new Size(this.Width, 0);
                picPuxarApps.Location = new Point(meioDaTela, pnlApps.Height);
                appsOcultos = true;
                pnlAppTransition.Stop();
            }
        }
    }
    private void BtnAlternarAppsOcultos(object sender, EventArgs e)
    {
        pnlAppTransition.Start();      
        heightPnlApps = pnlApps.Size.Height;  
    }
    private void BtnAbrirAplicativos(object sender, EventArgs e){
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
    private void BtnEditarAplicativos(object sender, EventArgs e)
    {
        Label lbl = sender as Label;
        int id = int.Parse(lbl.Name.Split(">")[1]);

        Form2 telaCadastro = new Form2(id, 1);
        telaCadastro.FormClosed += (s, e) => PegarApps();
        telaCadastro.Owner = this;
        telaCadastro.Show();
    }

    // Tratamento pictureBoxes --------------------------------------------------------------------
    private void PicDefinirCorDeFundo(Image imgData, PictureBox pic)
    {
        if (imgData == null)
            return;

        Bitmap bitmap = new Bitmap(imgData);
        Color corBordaSuperior = bitmap.GetPixel(bitmap.Width / 2, 0);
        Color corBordaInferior = bitmap.GetPixel(bitmap.Width / 2, bitmap.Height - 1);
        Color corBordaEsquerda = bitmap.GetPixel(0, bitmap.Height / 2);
        Color corBordaDireita = bitmap.GetPixel(bitmap.Width - 1, bitmap.Height / 2);

        int a = (corBordaSuperior.A + corBordaInferior.A + corBordaEsquerda.A + corBordaDireita.A) / 4;
        int r = (corBordaSuperior.R + corBordaInferior.R + corBordaEsquerda.R + corBordaDireita.R) / 4;
        int g = (corBordaSuperior.G + corBordaInferior.G + corBordaEsquerda.G + corBordaDireita.G) / 4;
        int b = (corBordaSuperior.B + corBordaInferior.B + corBordaEsquerda.B + corBordaDireita.B) / 4;

        pic.BackColor = Color.FromArgb(a, r, g, b);
    }
    private void PicMergeImages(Image imgData)
    {
        Bitmap original = new Bitmap(imgData);

        Bitmap vinheta = new Bitmap(Referencias.caminhoVinheta);

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
    private void PicGIFAbrindoJogo(){
        Image imgGIF = Image.FromFile(Referencias.caminhoGifCarregamento);
        int widthPic = 400;
        int heightPic = 200;
        int rightPic = this.Width/2 - widthPic/2;
        int topPic = this.Height/2 - heightPic/2;
        PictureBox picGIFCarregamento = new PictureBox
        {
            Image = imgGIF,
            Anchor = AnchorStyles.Top,
            Location = new Point(rightPic, topPic),
            Name = "picGIFCarregamento",
            Size = new Size(widthPic, heightPic),
            SizeMode = PictureBoxSizeMode.Zoom,
            TabStop = false,
            Parent = pictureBox1
        };

        pictureBox1.Controls.Add(picGIFCarregamento);

        PicDefinirCorDeFundo(imgGIF, picGIFCarregamento);
        PicArredondarBordas(picGIFCarregamento, 30, 30, 30, 30);
    }
    private void PicGIFRemover()
    {
        Control[] controlesEncontrados = pictureBox1.Controls.Find("picGIFCarregamento", true);
        if (controlesEncontrados[0] != null && pictureBox1.Controls.Contains(controlesEncontrados[0]))
        {
            pictureBox1.Controls.Remove(controlesEncontrados[0]);
            
            controlesEncontrados[0].Dispose();
        }
    }
    private void PicArredondarBordas(PictureBox pictureBox, int topLeftRadius, int topRightRadius, int bottomRightRadius, int bottomLeftRadius)
    {
        using (GraphicsPath gp = new GraphicsPath())
        {
            // Canto superior esquerdo
            if (topLeftRadius > 0)
                gp.AddArc(0, 0, topLeftRadius * 2, topLeftRadius * 2, 180, 90);
            else
                gp.AddLine(0, 0, 0, 0); // Pontudo

            // Canto superior direito
            if (topRightRadius > 0)
                gp.AddArc(pictureBox.Width - topRightRadius * 2, 0, topRightRadius * 2, topRightRadius * 2, 270, 90);
            else
                gp.AddLine(pictureBox.Width, 0, pictureBox.Width, 0); // Pontudo

            // Canto inferior direito
            if (bottomRightRadius > 0)
                gp.AddArc(pictureBox.Width - bottomRightRadius * 2, pictureBox.Height - bottomRightRadius * 2, bottomRightRadius * 2, bottomRightRadius * 2, 0, 90);
            else
                gp.AddLine(pictureBox.Width, pictureBox.Height, pictureBox.Width, pictureBox.Height); // Pontudo

            // Canto inferior esquerdo
            if (bottomLeftRadius > 0)
                gp.AddArc(0, pictureBox.Height - bottomLeftRadius * 2, bottomLeftRadius * 2, bottomLeftRadius * 2, 90, 90);
            else
                gp.AddLine(0, pictureBox.Height, 0, pictureBox.Height); // Pontudo

            gp.CloseFigure();

            pictureBox.Region = new Region(gp);
        }
    }

    // Tratamento Labels ---------------------------------------------------------------------------
    private void LblPosicionarCorretamente(string novoTexto, Label textToEdit)
    {
        int antigaLargura = textToEdit.Width;
        textToEdit.Text = novoTexto;

        int novaLargura = textToEdit.Width;
        int deslocamento = novaLargura - antigaLargura;

        textToEdit.Left -= deslocamento;
    }

    // Data e hora --------------------------------------------------------------------------------
    private void CriarRelogio()
    {
        horario = DateTime.Now;
        int segundosParaProximoMinuto = 60 - horario.Second;
        LblPosicionarCorretamente(horario.ToString("HH:mm") + " hr", lblClockAtalho);
        LblPosicionarCorretamente(horario.ToString("D"), lblDataAtalhos);

        temporizadorDoRelogio = new System.Timers.Timer(segundosParaProximoMinuto*1000);
        temporizadorDoRelogio.Elapsed += AtualizarHorario;
        temporizadorDoRelogio.AutoReset = true;
        temporizadorDoRelogio.Enabled = true;
    }
    private void AtualizarHorario(object sender, ElapsedEventArgs e)
    {
        DateTime horarioAtual = DateTime.Now;
        
        if(temporizadorDoRelogio.Interval != 60000){
            AtualizarTemporizador(horarioAtual);
        }

        horario = horarioAtual;

        LblPosicionarCorretamente(horario.ToString("HH:mm") + " hr", lblClockAtalho);
    }
    private void AtualizarTemporizador(DateTime horarioAtual){
        if(!horario.ToString("HH:mm").Equals(horarioAtual.ToString("HH:mm")))
        {
            temporizadorDoRelogio.Enabled = false;
            
            temporizadorDoRelogio = new System.Timers.Timer(60000);
            temporizadorDoRelogio.Elapsed += AtualizarHorario;
            temporizadorDoRelogio.AutoReset = true;
            temporizadorDoRelogio.Enabled = true;
        }
    }

    // Configuracoes ------------------------------------------------------------------------------
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
}
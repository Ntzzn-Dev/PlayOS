namespace PlaySO;
using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using SkiaSharp;
using Microsoft.Win32;
using Microsoft.Data.Sqlite;
using System.Collections;

public partial class Form2 : Form
{
    private int idDeAlteracao = 0, imgCarregada = 0;
    public Form2()
    {
        InitializeComponent();
        DefinirGatilhos();
        DefinirImgsPadrao();

        btnSalvarAtalho.Click += (s, e) => SalvarAtalho();
        btnSalvarApp.Click += (s, e) => SalvarApp();
    }
    public Form2(int id, int tipoDeEdicao)
    {
        InitializeComponent();
        DefinirGatilhos();

        switch (tipoDeEdicao)
        {
            case 0:
                DadosAtalho(id);
                TrocarPage(1);
                if (idDeAlteracao != 0) { btnSalvarAtalho.Click += (s, e) => AlterarAtalho(); }
                break;
            case 1:
                DadosApp(id);
                TrocarPage(2);
                if (idDeAlteracao != 0) { btnSalvarApp.Click += (s, e) => AlterarApp(); }
                break;
        }

    }
    //Primeira aba - Atalhos
    private void DefinirImgsPadrao()
    {
        picImgAtalho.Image = Properties.Resources.Morgan;
        picImgIconAtalho.Image = Properties.Resources.Morgan;
        picImgIconeApp.Image = Properties.Resources.Morgan;
    }
    private void SalvarAtalho()
    {
        Atalhos AtalhoSalvamento = new Atalhos();
        AtalhoSalvamento.setNomeAtalho(txtbxNomeAtalho.Texto);
        AtalhoSalvamento.setCaminhoAtalho(txtbxPathAtalho.Texto);
        AtalhoSalvamento.setParametroAtalho(txtbxParamAtalho.Texto);
        AtalhoSalvamento.setImgAtalho(picImgAtalho.Image);
        AtalhoSalvamento.setIconeAtalho(picImgIconAtalho.Image);

        Atalhos.Salvar(AtalhoSalvamento);

        FecharCadastro();
    }
    private void AlterarAtalho()
    {
        Atalhos atalhoAlteracao = new Atalhos();
        atalhoAlteracao.setIdAtalho(idDeAlteracao);
        atalhoAlteracao.setNomeAtalho(txtbxNomeAtalho.Texto);
        atalhoAlteracao.setCaminhoAtalho(txtbxPathAtalho.Texto);
        atalhoAlteracao.setParametroAtalho(txtbxParamAtalho.Texto);
        atalhoAlteracao.setImgAtalho(picImgAtalho.Image);
        atalhoAlteracao.setIconeAtalho(picImgIconAtalho.Image);

        Atalhos.Alterar(atalhoAlteracao);

        FecharCadastro();
    }
    private void DadosAtalho(int id)
    {
        Atalhos atalhoAtual = new Atalhos(id);

        idDeAlteracao = atalhoAtual.getIdAtalho();
        txtbxNomeAtalho.Texto = atalhoAtual.getNomeAtalho();
        txtbxPathAtalho.Texto = atalhoAtual.getCaminhoAtalho();
        txtbxParamAtalho.Texto = atalhoAtual.getParametroAtalho();
        picImgAtalho.Image = atalhoAtual.getImgAtalho();
        picImgIconAtalho.Image = atalhoAtual.getIconeAtalho();

        txtbxImgAtalho.LblPlaceholder = "Deixe em branco para manter a imagem";
        txtbxImgIconAtalho.LblPlaceholder = "Deixe em branco para manter o icone";
    }
    private void ObterDestinoAtalho(string atalho)
    {
        Type shellType = Type.GetTypeFromProgID("WScript.Shell");
        dynamic wshShell = Activator.CreateInstance(shellType);

        dynamic atalhos = wshShell.CreateShortcut(atalho);

        string destino = atalhos.TargetPath;

        string extensao = Path.GetExtension(destino);

        string argumentos = "";

        string nomeAtalho = Path.GetFileNameWithoutExtension(atalho);

        if (destino == "")
        {
            destino = $@"{ObterPastaXbox()}\{nomeAtalho}\Content\gamelaunchhelper.exe";
        }
        if (extensao != "")
        {
            argumentos = atalhos.Arguments;
        }

        txtbxPathAtalho.Texto = destino;
        txtbxParamAtalho.Texto = argumentos;
        txtbxNomeAtalho.Texto = nomeAtalho;
    }
    private string ObterPastaXbox()
    {
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady && drive.DriveType == DriveType.Fixed)
            {
                string xboxGamesPath = Path.Combine(drive.RootDirectory.FullName, "XboxGames");

                if (Directory.Exists(xboxGamesPath))
                {
                    return xboxGamesPath;
                }
            }
        }
        return "";
    }
    private bool PegarIds()
    {
        ArrayList ids = Atalhos.ConsultarIDs();

        if (ids.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void BtnProcurarExecutavel(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "Arquivos Executáveis (*.exe)|*.exe";

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            txtbxPathAtalho.Texto = ofd.FileName;
        }
    }
    private void BtnImportarAtalho(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Title = "Selecione um arquivo de atalho";
            openFileDialog.Filter = "Todos os Arquivos (*.url;*.lnk)|*.url;*.lnk";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ObterDestinoAtalho(openFileDialog.FileName);
            }
        }
    }
    private void EnterToEndAtalhos(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            if (sender == txtbxNomeAtalho)
            {
                txtbxPathAtalho.Focus();
            }
            else if (sender == txtbxPathAtalho)
            {
                txtbxParamAtalho.Focus();
            }
            else if (sender == txtbxParamAtalho)
            {
                txtbxImgAtalho.Focus();
            }
            else if (sender == txtbxImgAtalho)
            {
                txtbxImgIconAtalho.Focus();
            }
            else if (sender == txtbxImgIconAtalho)
            {
                if (imgCarregada == 1)
                {
                    if (idDeAlteracao == 0) { SalvarAtalho(); }
                    else { AlterarAtalho(); }
                    imgCarregada = 0;
                }
            }
        }
    }

    //Segunda Aba - Aplicativos

    private void SalvarApp()
    {
        Aplicativos appAtual = new Aplicativos();
        appAtual.setNomeAplicativo(txtbxNomeApp.Texto);
        appAtual.setCaminhoAplicativo(txtbxURLApp.Texto);
        appAtual.setIconeAplicativo(picImgIconeApp.Image);

        Aplicativos.Salvar(appAtual);

        FecharCadastro();
    }
    private void AlterarApp()
    {
        Aplicativos appAlteracao = new Aplicativos();
        appAlteracao.setIdAplicativo(idDeAlteracao);
        appAlteracao.setNomeAplicativo(txtbxNomeApp.Texto);
        appAlteracao.setCaminhoAplicativo(txtbxURLApp.Texto);
        appAlteracao.setIconeAplicativo(picImgIconeApp.Image);

        Aplicativos.Alterar(appAlteracao);

        FecharCadastro();
    }
    private void DadosApp(int id)
    {
        Aplicativos appAtual = new Aplicativos(id);

        idDeAlteracao = appAtual.getIdAplicativo();
        txtbxNomeApp.Texto = appAtual.getNomeAplicativo();
        txtbxURLApp.Texto = appAtual.getCaminhoAplicativo();
        picImgIconeApp.Image = appAtual.getIconeAplicativo();

        txtbxImgIconeApp.LblPlaceholder = "Deixe em branco para manter o icone";
    }
    private void EnterToEndApps(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            if (sender == txtbxNomeApp)
            {
                txtbxURLApp.Focus();
            }
            else if (sender == txtbxURLApp)
            {
                txtbxImgIconeApp.Focus();
            }
            else if (sender == txtbxImgIconeApp)
            {
                if (imgCarregada == 1)
                {
                    if (idDeAlteracao == 0) { SalvarApp(); }
                    else { AlterarAtalho(); }
                    imgCarregada = 0;
                }
            }
        }
    }

    //Meio Termo - Atalhos e Aplicativos

    public void DefinirGatilhos()
    {
        btnProcurarArq.Click += BtnProcurarExecutavel;
        btnImgAtalhoLocal.Click += BtnProcurarImgLocal;
        btnImgAtalhoOnline.Click += BtnProcurarImgOnline;
        btnImgIconAtalhoLocal.Click += BtnProcurarImgLocal;
        btnImgIconAtalhoOnline.Click += BtnProcurarImgOnline;
        btnImportAtalho.Click += BtnImportarAtalho;
        btnCancelarAtalho.Click += (s, e) => FecharCadastro();

        titleBar.FecharCustom += (s, e) => FecharCadastro();

        btnPageAtalhos.Click += (s, e) => TrocarPage(1);
        btnPageAplicativo.Click += (s, e) => TrocarPage(2);

        txtbxImgAtalho.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { BaixarImgs(txtbxImgAtalho.Texto, picImgAtalho); } };
        txtbxImgIconAtalho.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { BaixarImgs(txtbxImgIconAtalho.Texto, picImgIconAtalho); } };
        txtbxImgAtalho.TextoChanged += (s, e) => BaixarImgs(txtbxImgAtalho.Texto, picImgAtalho);
        txtbxImgIconAtalho.TextoChanged += (s, e) => BaixarImgs(txtbxImgIconAtalho.Texto, picImgIconAtalho);

        txtbxNomeAtalho.KeyDown += EnterToEndAtalhos;
        txtbxPathAtalho.KeyDown += EnterToEndAtalhos;
        txtbxParamAtalho.KeyDown += EnterToEndAtalhos;
        txtbxImgAtalho.KeyDown += EnterToEndAtalhos;
        txtbxImgIconAtalho.KeyDown += EnterToEndAtalhos;

        //Segunda Aba - Aplicativos

        btnIconOnlineApp.Click += BtnProcurarImgOnline;
        btnIconLocalApp.Click += BtnProcurarImgLocal;
        btnURLExtApp.Click += (s, e) => CriarTelaDeCola(null, "Colar URL ou URI");
        btnCancelarApp.Click += (s, e) => FecharCadastro();

        txtbxImgIconeApp.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { BaixarImgs(txtbxImgIconeApp.Texto, picImgIconeApp); } };
        txtbxImgIconeApp.TextoChanged += (s, e) => BaixarImgs(txtbxImgIconeApp.Texto, picImgIconeApp);

        txtbxNomeApp.KeyDown += EnterToEndApps;
        txtbxURLApp.KeyDown += EnterToEndApps;
        txtbxImgIconeApp.KeyDown += EnterToEndApps;
    }
    private void TrocarPage(int indice)
    {
        int atalho = 0, app = 0;

        btnPageAtalhos.BackColor = Color.FromArgb(44, 44, 44);
        btnPageAplicativo.BackColor = Color.FromArgb(44, 44, 44);

        switch (indice)
        {
            case 1:
                atalho = 511;
                btnPageAtalhos.BackColor = Color.FromArgb(26, 26, 26);
                break;
            case 2:
                app = 511;
                btnPageAplicativo.BackColor = Color.FromArgb(26, 26, 26);
                break;
        }

        pnlCdtAtalho.Size = new Size(669, atalho);
        pnlCdtAplicativo.Size = new Size(669, app);
    }
    public void DadoRecebidoOnline(string urlRecebida, int labelEspecificado)
    {
        switch (labelEspecificado)
        {
            case 0:
                txtbxImgAtalho.Texto = urlRecebida;
                break;
            case 1:
                txtbxImgIconAtalho.Texto = urlRecebida;
                break;
            case 2:
                txtbxImgIconeApp.Texto = urlRecebida;
                break;
            case 3:
                txtbxURLApp.Texto = urlRecebida;
                break;
        }
    }
    private void Reaparecer(string nomeNavegador)
    {
        this.Show();
        this.Owner.Show();

        Process[] navegatorProcesses = Process.GetProcessesByName(nomeNavegador);
        foreach (var process in navegatorProcesses)
        {
            try
            {
                if (process != null && !process.HasExited)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao encerrar o processo: {ex.Message}");
            }
        }
    }
    private void BtnProcurarImgOnline(object sender, EventArgs e)
    {
        string palavraChave = txtbxNomeAtalho.Texto;
        string urlPesquisa = $"https://www.google.com/search?hl=pt-BR&tbm=isch&q={Uri.EscapeDataString(palavraChave)}";

        if (sender == btnIconOnlineApp) { palavraChave = txtbxNomeApp.Texto; }

        if (sender == btnImgIconAtalhoOnline || sender == btnIconOnlineApp)
        {
            palavraChave += " Logo";
            urlPesquisa = $"https://www.google.com/search?as_st=y&hl=pt-BR&as_q={Uri.EscapeDataString(palavraChave)}&as_epq=&as_oq=&as_eq=&imgar=s&imgcolor=&imgtype=&cr=&as_sitesearch=&as_filetype=&tbs=&udm=2";
        }

        try
        {
            string caminhoNavegador = GetDefaultBrowserPath();

            Process navegadorAberto;

            if (File.Exists(caminhoNavegador))
            {
                navegadorAberto = Process.Start(caminhoNavegador, urlPesquisa);
            }
            else
            {
                navegadorAberto = Process.Start(urlPesquisa);
            }

            string acaoTelaCola = "";
            if (sender == btnImgAtalhoOnline) { acaoTelaCola = lblImgAtalho.Text; }
            else
            if (sender == btnImgIconAtalhoOnline) { acaoTelaCola = lblImgIconeApp.Text; }
            else
            if (sender == btnIconOnlineApp) { acaoTelaCola = lblImgIconeApp.Text; }

            CriarTelaDeCola(navegadorAberto, acaoTelaCola);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao abrir o navegador: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private string GetDefaultBrowserPath()
    {
        string browserPath = string.Empty;

        string userChoicePath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice";

        string progId = Registry.GetValue(userChoicePath, "ProgId", null) as string;

        if (string.IsNullOrEmpty(progId))
            throw new Exception("Navegador padrão não encontrado.");

        string browserRegPath = $@"HKEY_CLASSES_ROOT\{progId}\shell\open\command";
        browserPath = Registry.GetValue(browserRegPath, null, null) as string;

        if (string.IsNullOrEmpty(browserPath))
            throw new Exception("Caminho do navegador padrão não encontrado.");

        int firstQuote = browserPath.IndexOf('"');
        if (firstQuote >= 0)
        {
            int secondQuote = browserPath.IndexOf('"', firstQuote + 1);
            if (secondQuote > firstQuote)
            {
                browserPath = browserPath.Substring(firstQuote + 1, secondQuote - firstQuote - 1);
            }
        }

        return browserPath;
    }
    public void BtnProcurarImgLocal(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "Imagens (*.jpg;*.png;*.webp;*.jpeg)|*.jpg;*.png;*.webp;*.jpeg";

        if (sender == btnImgAtalhoLocal)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtbxImgAtalho.Texto = ofd.FileName;
            }
        }
        if (sender == btnImgIconAtalhoLocal)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtbxImgIconAtalho.Texto = ofd.FileName;
            }
        }
        if (sender == btnIconLocalApp)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtbxImgIconeApp.Texto = ofd.FileName;
            }
        }
    }
    public void FecharCadastro()
    {
        if (PegarIds()) { this.Owner.Close(); }
        this.Close();
    }
    private void CriarTelaDeCola(Process navegadorAberto, string acaoTelaCola)
    {
        if (navegadorAberto != null && !navegadorAberto.HasExited)
        {
            string nomeDoProcessador = navegadorAberto.ProcessName;
            Form4 telaCola = new Form4(navegadorAberto, acaoTelaCola);
            telaCola.Owner = this;
            telaCola.FormClosed += (s, e) => Reaparecer(nomeDoProcessador);

            this.Owner.Hide();
            this.Hide();

            telaCola.ShowDialog();
        }
        else
        {
            Form4 telaCola = new Form4(acaoTelaCola);
            telaCola.Owner = this;

            this.Owner.Hide();
            this.Hide();

            telaCola.ShowDialog();
        }
    }
    private async Task BaixarImgs(string pathToImg, PictureBox pcbxEmUso)
    {
        try
        {
            string formato = await DetectarFormatoAsync(pathToImg);
            bool eIcone = pcbxEmUso == picImgIconAtalho || pcbxEmUso == picImgIconeApp;

            if (string.IsNullOrEmpty(pathToImg))
            {
                if (txtbxPathAtalho.Texto != "")
                {
                    pcbxEmUso.Image = Properties.Resources.Morgan;
                }
                return;
            }

            if (formato == "LOCAL")
            {
                Image imgCarregada = Image.FromFile(pathToImg);
                if (imgCarregada.Width != imgCarregada.Height && eIcone)
                {
                    txtbxImgIconAtalho.Texto = "";
                    MessageBox.Show("a imagem deve ser quadrada");
                }
                else
                {
                    pcbxEmUso.Image = imgCarregada;
                }
            }
            else if (formato == "BASE64")
            {
                var base64Data = pathToImg.Split(',')[1];
                byte[] bytesDaImg2 = Convert.FromBase64String(base64Data);

                using (MemoryStream stream = new MemoryStream(bytesDaImg2))
                {
                    Image imgCarregada = Image.FromStream(stream);
                    if (imgCarregada.Width != imgCarregada.Height && eIcone)
                    {
                        txtbxImgIconAtalho.Text = "";
                        MessageBox.Show("a imagem deve ser quadrada");
                    }
                    else
                    {
                        pcbxEmUso.Image = imgCarregada;
                    }
                }
            }
            else if (formato == "OUTRO")
            {
                using (HttpClient client = new HttpClient())
                {
                    byte[] bytesDaImg = await client.GetByteArrayAsync(pathToImg);

                    using (MemoryStream stream = new MemoryStream(bytesDaImg))
                    {
                        Image imgCarregada = Image.FromStream(stream);
                        if (imgCarregada.Width != imgCarregada.Height && eIcone)
                        {
                            txtbxImgIconAtalho.Texto = "";
                            MessageBox.Show("a imagem deve ser quadrada");
                        }
                        else
                        {
                            pcbxEmUso.Image = imgCarregada;
                        }
                    }
                }
            }
            else if (formato == "WEBP")
            {
                Image imagem = await CarregarImagemWebpAsync(pathToImg);

                if (imagem != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bitmap = new Bitmap(imagem);
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                        pcbxEmUso.Image = bitmap;
                    }
                }
            }
            else if (formato == "ICO")
            {
                Image imagem = await BaixarEConverterIcoAsync(pathToImg);

                if (imagem != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bitmap = new Bitmap(imagem);
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                        pcbxEmUso.Image = bitmap;
                    }
                }
            }
            else { return; }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao baixar a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private async Task<string> DetectarFormatoAsync(string url)
    {
        if (url.StartsWith("data:", StringComparison.OrdinalIgnoreCase)) { return "BASE64"; }
        else
        if (File.Exists(url)) { return "LOCAL"; }
        else
        if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] bytes = await client.GetByteArrayAsync(url);

                if (bytes.Length >= 12)
                {
                    string header = BitConverter.ToString(bytes.Take(12).ToArray()).Replace("-", "");

                    if (header.StartsWith("52494646") && header.Contains("57454250")) // WEBP
                        return "WEBP";

                    if (header.StartsWith("00000100") || header.StartsWith("00000200")) // ICO
                        return "ICO";
                }
                return "OUTRO";
            }
        }
        return "NENHUM";
    }
    private async Task<Image> CarregarImagemWebpAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            byte[] bytes = await client.GetByteArrayAsync(url);

            using (var ms = new MemoryStream(bytes))
            {
                SKBitmap bitmap = SKBitmap.Decode(ms);
                return Image.FromStream(bitmap.Encode(SKEncodedImageFormat.Png, 100).AsStream());
            }
        }
    }
    private async Task<Image> BaixarEConverterIcoAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            byte[] icoBytes = await client.GetByteArrayAsync(url);

            using (var ms = new MemoryStream(icoBytes))
            {
                SKBitmap bitmap = SKBitmap.Decode(ms);
                if (bitmap == null)
                    throw new Exception("Não foi possível decodificar a imagem como um ícone válido.");

                using (MemoryStream pngStream = new MemoryStream())
                {
                    bitmap.Encode(pngStream, SKEncodedImageFormat.Png, 100);
                    pngStream.Seek(0, SeekOrigin.Begin);
                    return Image.FromStream(pngStream);
                }
            }
        }
    }
}
namespace PlaySO;
using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using System.Runtime.InteropServices;
using SkiaSharp;

public partial class Form2 : Form
{
    private int idDeAlteracao = 0, imgCarregada = 0;
    private Image img, icn;
    public static string caminhoImgPadrao = @".\Assets\Morgan.jpg";
    public Form2()
    {
        InitializeComponent();
        DefinirGatilhos();
        DefinirImgsPadrao();
    }
    public Form2(int id, int tipoDeEdicao)
    {
        InitializeComponent();
        DefinirGatilhos();

        if(tipoDeEdicao == 0){
            DadosAtalho(id);
            tabPageCdtApps.Enabled = false;
            if (idDeAlteracao != 0) { btnSalvar.Click -= BtnSalvarAtalho; btnSalvar.Click += (s, e) => AlterarAtalho(); }
        } else
        if(tipoDeEdicao == 1){
            DadosApp(id);
            tabAbasCadastros.SelectedIndex = 1;
            tabPageCdtAtalho.Enabled = false;
            if (idDeAlteracao != 0) { btnSalvarApp.Click -= BtnSalvarApp; btnSalvarApp.Click += (s, e) => AlterarApp(); }
        }
    }
    //Primeira aba - Atalhos
    private void DefinirImgsPadrao()
    {
        img = Image.FromFile(caminhoImgPadrao);
        icn = Image.FromFile(caminhoImgPadrao);
        pictureImgGame.Image = img;
        pictureIconGame.Image = img;
        picImgIconeApp.Image = img;
    }
    private void SalvarAtalho()
    {
        Atalhos AtalhoSalvamento = new Atalhos();
        AtalhoSalvamento.setNomeAtalho(nomeGame.Text);
        AtalhoSalvamento.setCaminhoAtalho(caminhoGame.Text);
        AtalhoSalvamento.setParametroAtalho(parametroGame.Text);
        AtalhoSalvamento.setImgAtalho(img);
        AtalhoSalvamento.setIconeAtalho(icn);

        AtalhoSalvamento.Salvar(AtalhoSalvamento);

        FecharCadastro();
    }
    private void AlterarAtalho()
    {
        Atalhos atalhoAlteracao = new Atalhos();
        atalhoAlteracao.setIdAtalho(idDeAlteracao);
        atalhoAlteracao.setNomeAtalho(nomeGame.Text);
        atalhoAlteracao.setCaminhoAtalho(caminhoGame.Text);
        atalhoAlteracao.setParametroAtalho(parametroGame.Text);
        atalhoAlteracao.setImgAtalho(img);
        atalhoAlteracao.setIconeAtalho(icn);

        atalhoAlteracao.Alterar(atalhoAlteracao);

        FecharCadastro();
    }
    private void DadosAtalho(int id)
    {
        Atalhos atalhoAtual = new Atalhos(id);

        idDeAlteracao = atalhoAtual.getIdAtalho();
        nomeGame.Text = atalhoAtual.getNomeAtalho();
        caminhoGame.Text = atalhoAtual.getCaminhoAtalho();
        parametroGame.Text = atalhoAtual.getParametroAtalho();
        pictureImgGame.Image = atalhoAtual.getImgAtalho();
        pictureIconGame.Image = atalhoAtual.getIconeAtalho();

        imgGame.PlaceholderText = "Deixe em branco para manter a imagem";
        iconGame.PlaceholderText = "Deixe em branco para manter o icone";
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
            destino = $@"C:\XboxGames\{nomeAtalho}\Content\gamelaunchhelper.exe";
        }
        if (extensao != "")
        {
            argumentos = atalhos.Arguments;
        }

        caminhoGame.Text = destino;
        parametroGame.Text = argumentos;
        nomeGame.Text = nomeAtalho;

        BtnIconeDoExecutavel();
    }
    private void DefinirImgIcone()
    {
        if (caminhoGame.Text.Equals("") && File.Exists(caminhoGame.Text) && iconGame.Text.Equals("") && idDeAlteracao == 0)
        {
            Icon icone = Icon.ExtractAssociatedIcon(caminhoGame.Text);
            pictureIconGame.Image = icone.ToBitmap();
        }
    }
    private void BtnIconeDoExecutavel()
    {
        if (iconGame.Text.Equals("")) { DefinirImgIcone(); }
    }
    public void BtnProcurarExecutavel(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "Arquivos Executáveis (*.exe)|*.exe";

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            caminhoGame.Text = ofd.FileName;
            BtnIconeDoExecutavel();
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
    private void BtnSalvarAtalho(object sender, EventArgs e)
    {
        SalvarAtalho();
    }
    private void EnterToEndAtalhos(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            if (sender == nomeGame)
            {
                caminhoGame.Focus();
            }
            else if (sender == caminhoGame)
            {
                parametroGame.Focus();
            }
            else if (sender == parametroGame)
            {
                imgGame.Focus();
            }
            else if (sender == imgGame)
            {
                iconGame.Focus();
            }
            else if (sender == iconGame)
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
        appAtual.setNomeAplicativo(txtbxNomeApp.Text);
        appAtual.setCaminhoAplicativo(txtbxURLApp.Text);
        appAtual.setIconeAplicativo(icn);

        appAtual.Salvar(appAtual);

        FecharCadastro();
    }
    private void AlterarApp()
    {
        Aplicativos appAlteracao = new Aplicativos();
        appAlteracao.setIdAplicativo(idDeAlteracao);
        appAlteracao.setNomeAplicativo(txtbxNomeApp.Text);
        appAlteracao.setCaminhoAplicativo(txtbxURLApp.Text);
        appAlteracao.setIconeAplicativo(icn);

        appAlteracao.Alterar(appAlteracao);

        FecharCadastro();
    }
    private void DadosApp(int id)
    {
        Aplicativos appAtual = new Aplicativos(id);

        idDeAlteracao = appAtual.getIdAplicativo();
        txtbxNomeApp.Text = appAtual.getNomeAplicativo();
        txtbxURLApp.Text = appAtual.getCaminhoAplicativo();
        picImgIconeApp.Image = appAtual.getIconeAplicativo();

        txtbxImgIconeApp.PlaceholderText = "Deixe em branco para manter o icone";
    }
    private void BtnSalvarApp(object sender, EventArgs e)
    {
        SalvarApp();
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
        btnImgLocal.Click += BtnProcurarImgLocal;
        btnImgGoogle.Click += BtnProcurarImgOnline;
        btnIconLocal.Click += BtnProcurarImgLocal;
        btnIconGoogle.Click += BtnProcurarImgOnline;
        btnImportAtalho.Click += BtnImportarAtalho;
        btnSalvar.Click += BtnSalvarAtalho;
        btnCancelar.Click += (s, e) => FecharCadastro();

        imgGame.KeyDown += (s, e) => {if(e.KeyCode == Keys.Enter){ BaixarImgs(imgGame.Text, pictureImgGame);}}; 
        iconGame.KeyDown += (s, e) => {if(e.KeyCode == Keys.Enter){ BaixarImgs(iconGame.Text, pictureIconGame);}}; 
        txtbxImgIconeApp.KeyDown += (s, e) => {if(e.KeyCode == Keys.Enter){ BaixarImgs(txtbxImgIconeApp.Text, picImgIconeApp);}};

        caminhoGame.Leave += (s, e) => BtnIconeDoExecutavel();

        nomeGame.KeyDown += EnterToEndAtalhos;
        caminhoGame.KeyDown += EnterToEndAtalhos;
        parametroGame.KeyDown += EnterToEndAtalhos;
        imgGame.KeyDown += EnterToEndAtalhos;
        iconGame.KeyDown += EnterToEndAtalhos;

        //Segunda Aba - Aplicativos

        btnIconOnlineApp.Click += BtnProcurarImgOnline;
        btnIconLocalApp.Click += BtnProcurarImgLocal;
        btnURLExtApp.Click += (s, e) => CriarTelaDeCola(null, "Colar URL ou URI");
        btnSalvarApp.Click += BtnSalvarApp;
        btnCancelarApp.Click += (s, e) => FecharCadastro();

        txtbxImgIconeApp.KeyDown += (s, e) => {if(e.KeyCode == Keys.Enter){BaixarImgs(txtbxImgIconeApp.Text, picImgIconeApp);}};

        txtbxNomeApp.KeyDown += EnterToEndApps;
        txtbxURLApp.KeyDown += EnterToEndApps;
        txtbxImgIconeApp.KeyDown += EnterToEndApps;
    }
    public void DadoRecebidoOnline(string urlRecebida, int labelEspecificado)
    {
        switch (labelEspecificado)
        {
            case 0:
                imgGame.Text = urlRecebida;
                break;
            case 1:
                iconGame.Text = urlRecebida;
                break;
            case 2:
                txtbxImgIconeApp.Text = urlRecebida;
                break;
            case 3:
                txtbxURLApp.Text = urlRecebida;
                break;
        }
    }
    private void Reaparecer(Process navigator)
    {
        this.Show();
        this.Owner.Show();

        if (navigator != null && !navigator.HasExited)
        {
            navigator.Kill();
        }
    }
    private void BtnProcurarImgOnline(object sender, EventArgs e)
    {
        string palavraChave = nomeGame.Text;
        string urlPesquisa = $"https://www.google.com/search?hl=pt-BR&tbm=isch&q={Uri.EscapeDataString(palavraChave)}";

        if (sender == btnIconOnlineApp) { palavraChave = txtbxNomeApp.Text; }

        if (sender == btnIconGoogle || sender == btnIconOnlineApp)
        {
            palavraChave += " Logo";
            urlPesquisa = $"https://www.google.com/search?as_st=y&hl=pt-BR&as_q={Uri.EscapeDataString(palavraChave)}&as_epq=&as_oq=&as_eq=&imgar=s&imgcolor=&imgtype=&cr=&as_sitesearch=&as_filetype=&tbs=&udm=2";
        }

        try
        {
            string caminhoNavegador = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

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
            if (sender == btnImgGoogle) { acaoTelaCola = imgdoJogo.Text; }
            else
            if (sender == btnIconGoogle) { acaoTelaCola = iconDoJogo.Text; }
            else
            if (sender == btnIconOnlineApp) { acaoTelaCola = lblImgIconeApp.Text; }

            CriarTelaDeCola(navegadorAberto, acaoTelaCola);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao abrir o navegador: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public void BtnProcurarImgLocal(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "Imagens (*.jpg;*.png;*.webp;*.jpeg)|*.jpg;*.png;*.webp;*.jpeg";

        if (sender == btnImgLocal)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgGame.Text = ofd.FileName;
            }
        }
        if (sender == btnIconLocal)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                iconGame.Text = ofd.FileName;
            }
        }
        if (sender == btnIconLocalApp)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtbxImgIconeApp.Text = ofd.FileName;
            }
        }
    }
    public void FecharCadastro()
    {
        this.Close();
    }
    private void CriarTelaDeCola(Process navegadorAberto, string acaoTelaCola)
    {
        if (navegadorAberto == null)
        {
            Form4 telaCola = new Form4(acaoTelaCola);
            telaCola.Owner = this;

            this.Owner.Hide();
            this.Hide();

            telaCola.ShowDialog();
        }
        else
        {
            Form4 telaCola = new Form4(navegadorAberto, acaoTelaCola);
            telaCola.Owner = this;
            telaCola.FormClosed += (s, e) => Reaparecer(navegadorAberto);

            this.Owner.Hide();
            this.Hide();

            telaCola.ShowDialog();
        }

    }
    private async Task BaixarImgs(string pathToImg, PictureBox pcbxEmUso)
    {
        try
        {
            bool eIcone = pcbxEmUso == pictureIconGame || pcbxEmUso == picImgIconeApp;
            byte[] bytesDaImg = new byte[0];
            string formato = await DetectarFormatoAsync(pathToImg);
            
            if (string.IsNullOrEmpty(pathToImg))
            {
                if (caminhoGame.Text != "")
                {
                    pcbxEmUso.Image = Image.FromFile(caminhoImgPadrao);
                    bytesDaImg = File.ReadAllBytes(caminhoImgPadrao);
                }
                return;
            }
            if (File.Exists(pathToImg))
            {
                Image imgCarregada = Image.FromFile(pathToImg);
                if (imgCarregada.Width != imgCarregada.Height && eIcone)
                {
                    iconGame.Text = "";
                    MessageBox.Show("a imagem deve ser quadrada");
                }
                else
                {
                    bytesDaImg = File.ReadAllBytes(pathToImg);
                    pcbxEmUso.Image = imgCarregada;
                }
            }
            else if (pathToImg.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                var base64Data = pathToImg.Split(',')[1];
                byte[] bytesDaImg2 = Convert.FromBase64String(base64Data);

                using (MemoryStream stream = new MemoryStream(bytesDaImg2))
                {
                    Image imgCarregada = Image.FromStream(stream);
                    if (imgCarregada.Width != imgCarregada.Height && eIcone)
                    {
                        iconGame.Text = "";
                        MessageBox.Show("a imagem deve ser quadrada");
                    }
                    else
                    {
                        bytesDaImg = Convert.FromBase64String(base64Data);
                        pcbxEmUso.Image = imgCarregada;
                    }
                }
            } 
            else if (formato == "WEBP")
            {
                Image imagem = await CarregarImagemWebpAsync(pathToImg);

                if (imagem != null){
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bitmap = new Bitmap(imagem);
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                        bytesDaImg = ms.ToArray();

                        pcbxEmUso.Image = bitmap;
                    }
                }    
            }
            else if (formato == "ICO")
            {
                Image imagem = await BaixarEConverterIcoAsync(pathToImg);

                if (imagem != null){
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bitmap = new Bitmap(imagem);
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                        bytesDaImg = ms.ToArray();

                        pcbxEmUso.Image = bitmap;
                    }
                }                
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    bytesDaImg = await client.GetByteArrayAsync(pathToImg);

                    using (MemoryStream stream = new MemoryStream(bytesDaImg))
                    {
                        Image imgCarregada = Image.FromStream(stream);
                        if (imgCarregada.Width != imgCarregada.Height && eIcone)
                        {
                            iconGame.Text = "";
                            MessageBox.Show("a imagem deve ser quadrada");
                        }
                        else
                        {
                            pcbxEmUso.Image = imgCarregada;
                        }
                    }
                }
            }

            if (pcbxEmUso == pictureIconGame || pcbxEmUso == picImgIconeApp)
            {
                using (MemoryStream ms = new MemoryStream(bytesDaImg)) { icn = Image.FromStream(ms); }
                imgCarregada = 1;
            }
            else
            if (pcbxEmUso == pictureImgGame)
            {
                using (MemoryStream ms = new MemoryStream(bytesDaImg)) { img = Image.FromStream(ms); }
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao baixar a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private async Task<string> DetectarFormatoAsync(string url)
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
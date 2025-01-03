namespace PlaySO;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        this.WindowState = FormWindowState.Maximized;
        this.FormBorderStyle = FormBorderStyle.None;

        abrirAtalho.Click += BtnAbrirAtalho_Click;
        addAtalho.Click += BtnCriarCadastro_Click;
        alterarAtalho.Click += BtnAlterarCadastro_Click;
        apagarAtalho.Click += BtnApagarCadastro_Click;
        btnSair.Click += BtnSairTela_Click;

        ListarAtalhos();
    }
    private void BtnSairTela_Click(object sender, EventArgs e)
    {
        /*this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.Sizable;*/

        this.Close();
    }
    private void BtnCriarCadastro_Click(object sender, EventArgs e)
    {
        Form2 telaCadastro = new Form2();
        //telaCadastro.PerdeuFocoEvent += ListarAtalhos;
        telaCadastro.ShowDialog();
    }
    private void BtnAlterarCadastro_Click(object sender, EventArgs e)
    {
        try
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um atalho primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pega o caminho do atalho selecionado
            string linhaSelecionada = listBox1.SelectedItem.ToString();
            string id = linhaSelecionada.Split('>')[0].Trim();

            Form2 telaCadastro = new Form2();
            //telaCadastro.PerdeuFocoEvent += ListarAtalhos;
            //telaCadastro.idDoJogo(int.Parse(id), linhaSelecionada.Split('>')[1].Trim(), linhaSelecionada.Split('>')[2].Trim(), linhaSelecionada.Split('>')[3].Trim());
            telaCadastro.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao abrir o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void BtnApagarCadastro_Click(object sender, EventArgs e)
    {
        try
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um atalho primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pega o caminho do atalho selecionado
            string linhaSelecionada = listBox1.SelectedItem.ToString();
            string id = linhaSelecionada.Split('>')[0].Trim();

            string connectionString = "Data Source=applicationsShortcuts.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string deleteCommand = "DELETE FROM AtalhosdeAplicativos WHERE id = @id";

                using (var command = new SqliteCommand(deleteCommand, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    
                    /* Executa o comando de exclusão
                    if (rowsAffected > 0) { MessageBox.Show("Atalho excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else { MessageBox.Show("Nenhum atalho encontrado com esse id.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); }*/
                }
            }
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso ocorra alguma exceção
            MessageBox.Show($"Erro ao excluir o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        ListarAtalhos();
    }

    private void ListarAtalhos()
    {
        try
        {
            string connectionString = "Data Source=applicationsShortcuts.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // Comando para buscar todos os atalhos
                string selectCommand = "SELECT Id, Nome, Caminho, Parametro FROM AtalhosdeAplicativos";
                using (var command = new SqliteCommand(selectCommand, connection))
                using (var reader = command.ExecuteReader())
                {
                    listBox1.Items.Clear(); // Limpa a lista antes de carregar os itens

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nome = reader.GetString(1);
                        string caminho = reader.GetString(2);
                        string parametro = reader.GetString(3);

                        // Adiciona os atalhos ao ListBox
                        listBox1.Items.Add($"{id} > {nome} > {caminho} > {parametro}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao listar atalhos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnAbrirAtalho_Click(object sender, EventArgs e)
    {
        try
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um atalho primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pega o caminho do atalho selecionado
            string linhaSelecionada = listBox1.SelectedItem.ToString();
            string caminhoExecutavel = linhaSelecionada.Split('>')[2].Trim();
            string diretorioTrabalho = System.IO.Path.GetDirectoryName(caminhoExecutavel);
            string parametrosAdicionais = linhaSelecionada.Split('>')[3].Trim();

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = caminhoExecutavel,
                WorkingDirectory = diretorioTrabalho,
                Arguments = parametrosAdicionais,
                Verb = "runas", // Isso garante que o processo será iniciado com privilégios de administrador
                UseShellExecute = true
            };

            Process.Start(psi);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao abrir o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /*
    
    private Panel ClonePanel(Panel originalPanel, string nomeApp)
    {
        Panel clonedPanel = new Panel
        {
            Margin = new Padding(0),
            Size = originalPanel.Size,
            Location = new Point(originalPanel.Location.X + 100, originalPanel.Location.Y),
            BackColor = Color.AliceBlue,
            BorderStyle = originalPanel.BorderStyle,
            Name = originalPanel.Name + "_Nome"
        };

        PictureBox originalPictureBox = originalPanel.Controls.OfType<PictureBox>().FirstOrDefault();
        if (originalPictureBox != null)
        {
            PictureBox clonedPictureBox = new PictureBox
            {
                Size = originalPictureBox.Size,
                Location = originalPictureBox.Location,
                Image = originalPictureBox.Image,
                SizeMode = originalPictureBox.SizeMode,
                Name = originalPictureBox.Name + "_Clone",
                Margin = new Padding(0)
            };
            clonedPanel.Controls.Add(clonedPictureBox);
        }

        Label originalLabel = originalPanel.Controls.OfType<Label>().FirstOrDefault();
        if (originalLabel != null)
        {
            Label clonedLabel = new Label
            {
                Anchor = AnchorStyles.Top,
                Size = originalLabel.Size,
                Location = originalLabel.Location,
                Margin = new Padding(0),
                Text = originalLabel.Text,
                Font = originalLabel.Font,
                BackColor = originalLabel.BackColor,
                ForeColor = originalLabel.ForeColor,
                Name = originalLabel.Name + "_Clone"
            };
            clonedPanel.Controls.Add(clonedLabel);
        }

        return clonedPanel;
    }*/
}


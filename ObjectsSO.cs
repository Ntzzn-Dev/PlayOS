using System.Collections;
using System.ServiceModel.Channels;
using Microsoft.Data.Sqlite;

public class Aplicativos {
    private int id;
    private string nome;
    private string caminho;
    private Image icon;

    public Aplicativos()
    {

    }
    public Aplicativos(int idDePesquisa)
    {
        try
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "applicationsShortcuts.db");
            string connectionString = $"Data Source={dbPath}";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string selectCommand = "SELECT Nome, Caminho, Icon FROM AplicativosExtras WHERE Id = @id";
                using (var command = new SqliteCommand(selectCommand, connection))
                {
                    command.Parameters.AddWithValue("@id", idDePesquisa);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nome = reader.GetString(0);
                            string caminho = reader.GetString(1);
                            long tamanhoBlobImg = reader.GetBytes(2, 0, null, 0, 0);

                            byte[] bufferImg = new byte[tamanhoBlobImg];
                            reader.GetBytes(2, 0, bufferImg, 0, (int)tamanhoBlobImg);

                            using (MemoryStream ms = new MemoryStream(bufferImg))
                            {
                                Image iconAtual = Image.FromStream(ms);
                                setIconeAplicativo(iconAtual);
                            }

                            setIdAplicativo(idDePesquisa);
                            setNomeAplicativo(nome);
                            setCaminhoAplicativo(caminho);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao listar atalhos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void Salvar(Aplicativos appParaSalvamento){
        try
        {
            string nome = appParaSalvamento.getNomeAplicativo();
            string url = appParaSalvamento.getCaminhoAplicativo();
            byte[] icnEmBytes = Referencias.ImageToByteArray(appParaSalvamento.getIconeAplicativo());

            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "applicationsShortcuts.db");
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string insertCommand = "INSERT INTO AplicativosExtras (Nome, Caminho, Icon) VALUES (@nome, @Caminho, @icon)";
                using (var command = new SqliteCommand(insertCommand, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@Caminho", url);
                    command.Parameters.AddWithValue("@icon", icnEmBytes);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao adicionar o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public static void Alterar(Aplicativos appParaAlteracao){
        try
        {
            int idDeAlteracao = appParaAlteracao.getIdAplicativo();
            string nome = appParaAlteracao.getNomeAplicativo();
            string caminho = appParaAlteracao.getCaminhoAplicativo();
            byte[] imgEmBytes = Referencias.ImageToByteArray(appParaAlteracao.getIconeAplicativo());

            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "applicationsShortcuts.db");
            string connectionString = $"Data Source={dbPath}";
            
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string condicaoCommand = " WHERE id = @id";
                string insertCommand = "UPDATE AplicativosExtras SET Nome = @nome, Caminho = @caminho";
                
                if (imgEmBytes.Length != 0) { condicaoCommand = ", Icon = @icon" + condicaoCommand; }

                insertCommand = insertCommand + condicaoCommand;
                
                using (var command = new SqliteCommand(insertCommand, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@caminho", caminho);
                    if (imgEmBytes.Length != 0) { command.Parameters.AddWithValue("@icon", imgEmBytes); }
                    command.Parameters.AddWithValue("@id", idDeAlteracao);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao alterar o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public static ArrayList ConsultarIDs(){
        ArrayList idsApps = new ArrayList();
        try
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "applicationsShortcuts.db");
            string connectionString = $"Data Source={dbPath}";

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
            MessageBox.Show($"Erro ao listar aplicativos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return idsApps;
    }

    public int getIdAplicativo(){
        return this.id;
    }
    public void setIdAplicativo(int novoId){
        this.id = novoId;
    }
    public string getNomeAplicativo(){
        return this.nome;
    }
    public void setNomeAplicativo(string novoNome){
        this.nome = novoNome;
    }
    public string getCaminhoAplicativo(){
        return this.caminho;
    }
    public void setCaminhoAplicativo(string novoCaminho){
        this.caminho = novoCaminho;
    }
    public Image getIconeAplicativo(){
        return this.icon;
    }
    public void setIconeAplicativo(Image novoIcon){
        this.icon = novoIcon;
    }
}

public class Atalhos {
    private int id;
    private string nome;
    private string caminho;
    private string parametro;
    private Image img;
    private Image icon;

    public Atalhos()
    {

    }
    public Atalhos(int idDePesquisa)
    {
        try
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "applicationsShortcuts.db");
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string selectCommand = "SELECT Nome, Caminho, Parametro, Imagem, Icon FROM AtalhosdeAplicativos WHERE Id = @id";
                using (var command = new SqliteCommand(selectCommand, connection))
                {
                    command.Parameters.AddWithValue("@id", idDePesquisa);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nome = reader.GetString(0);
                            string caminho = reader.GetString(1);
                            string parametro = reader.GetString(2);

                            long tamanhoBlobImg = reader.GetBytes(3, 0, null, 0, 0);

                            byte[] bufferImg = new byte[tamanhoBlobImg];
                            reader.GetBytes(3, 0, bufferImg, 0, (int)tamanhoBlobImg);

                            using (MemoryStream ms = new MemoryStream(bufferImg))
                            {
                                Image imgAtual = Image.FromStream(ms);
                                setImgAtalho(imgAtual);
                            }

                            long tamanhoBlobIcon = reader.GetBytes(4, 0, null, 0, 0);

                            byte[] bufferIcon = new byte[tamanhoBlobIcon];
                            reader.GetBytes(4, 0, bufferIcon, 0, (int)tamanhoBlobIcon);

                            using (MemoryStream ms = new MemoryStream(bufferIcon))
                            {
                                Image iconAtual = Image.FromStream(ms);
                                setIconeAtalho(iconAtual);
                            }

                            setIdAtalho(idDePesquisa);
                            setNomeAtalho(nome);
                            setCaminhoAtalho(caminho);
                            setParametroAtalho(parametro);
                        }
                    }
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao listar atalhos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void Salvar(Atalhos atalhoParaSalvamento){
        try
        {
            string nome = atalhoParaSalvamento.getNomeAtalho();
            string caminho = atalhoParaSalvamento.getCaminhoAtalho();
            string parametro = atalhoParaSalvamento.getParametroAtalho();
            byte[] imgEmBytes = Referencias.ImageToByteArray(atalhoParaSalvamento.getImgAtalho());
            byte[] icnEmBytes = Referencias.ImageToByteArray(atalhoParaSalvamento.getIconeAtalho());

            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "applicationsShortcuts.db");
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string insertCommand = "INSERT INTO AtalhosdeAplicativos (Nome, Caminho, Parametro, Imagem, Icon) VALUES (@nome, @caminho, @parametro, @img, @icon)";
                using (var command = new SqliteCommand(insertCommand, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@caminho", caminho);
                    command.Parameters.AddWithValue("@parametro", parametro);
                    command.Parameters.AddWithValue("@img", imgEmBytes);
                    command.Parameters.AddWithValue("@icon", icnEmBytes);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao adicionar o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public static void Alterar(Atalhos atalhoParaAlteracao){
        try
        {
            int idDeAlteracao = atalhoParaAlteracao.getIdAtalho();
            string nome = atalhoParaAlteracao.getNomeAtalho();
            string caminho = atalhoParaAlteracao.getCaminhoAtalho();
            string parametro = atalhoParaAlteracao.getParametroAtalho();
            byte[] imgEmBytes = Referencias.ImageToByteArray(atalhoParaAlteracao.getImgAtalho());
            byte[] icnEmBytes = Referencias.ImageToByteArray(atalhoParaAlteracao.getIconeAtalho());

            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "applicationsShortcuts.db");
            string connectionString = $"Data Source={dbPath}";
            
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string condicaoCommand = " WHERE id = @id";
                string insertCommand = "UPDATE AtalhosdeAplicativos SET Nome = @nome, Caminho = @caminho, Parametro = @parametro";
                
                if (icnEmBytes.Length != 0) { condicaoCommand = ", Icon = @icn" + condicaoCommand; }
                if (imgEmBytes.Length != 0) { condicaoCommand = ", Imagem = @img" + condicaoCommand; }

                insertCommand = insertCommand + condicaoCommand;
                
                using (var command = new SqliteCommand(insertCommand, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@caminho", caminho);
                    command.Parameters.AddWithValue("@parametro", parametro);
                    if (imgEmBytes.Length != 0) { command.Parameters.AddWithValue("@img", imgEmBytes); }
                    if (icnEmBytes.Length != 0) { command.Parameters.AddWithValue("@icn", icnEmBytes); }
                    command.Parameters.AddWithValue("@id", idDeAlteracao);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao alterar o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public static void Deletar(int idDeExclusao){
        try
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "applicationsShortcuts.db");
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string deleteCommand = "DELETE FROM AtalhosdeAplicativos WHERE id = @id";

                using (var command = new SqliteCommand(deleteCommand, connection))
                {
                    command.Parameters.AddWithValue("@id", idDeExclusao);

                    int rowsAffected = command.ExecuteNonQuery();

                    /* Executa o comando de exclusão
                    if (rowsAffected > 0) { MessageBox.Show("Atalho excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else { MessageBox.Show("Nenhum atalho encontrado com esse id.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); }*/
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso ocorra alguma exceção
            MessageBox.Show($"Erro ao excluir o atalho: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public static ArrayList ConsultarIDs(){
        ArrayList ids = new ArrayList();
        try
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "applicationsShortcuts.db");
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string selectCommand = "SELECT Id FROM AtalhosdeAplicativos";
                using (var command = new SqliteCommand(selectCommand, connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            ids.Add(id);
                        }
                    }
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao listar ids: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return ids;
    }

    public int getIdAtalho(){
        return this.id;
    }
    public void setIdAtalho(int novoId){
        this.id = novoId;
    }
    public string getNomeAtalho(){
        return this.nome;
    }
    public void setNomeAtalho(string novoNome){
        this.nome = novoNome;
    }
    public string getCaminhoAtalho(){
        return this.caminho;
    }
    public void setCaminhoAtalho(string novoCaminho){
        this.caminho = novoCaminho;
    }
    public string getParametroAtalho(){
        return this.parametro;
    }
    public void setParametroAtalho(string novoParametro){
        this.parametro = novoParametro;
    }
    public Image getImgAtalho(){
        return this.img;
    }
    public void setImgAtalho(Image novaImg){
        this.img = novaImg;
    }
    public Image getIconeAtalho(){
        return this.icon;
    }
    public void setIconeAtalho(Image novoIcon){
        this.icon = novoIcon;
    }
}

public class Referencias{
    public static string caminhoImgPadrao = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Morgan.jpg");
    public static string caminhoVinheta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Vinheta.png");
    public static string caminhoGifCarregamento = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Carregando.gif");
    public static string caminhoPicAppsShow = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "PicAppsShow.png");
    public static string caminhoPicAppsHide = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "PicAppsHide.png");
    public Referencias(){

    }

    public static byte[] ImageToByteArray(Image image)
    {
        if (image != null){
            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap bitmap = new Bitmap(image);
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        return new byte[0];
    }
}
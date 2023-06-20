using MySql.Data.MySqlClient;

namespace portfólio_5
{
    public partial class Form1 : Form
    {
        static string strConexao = "server=localhost;uid=root;pwd=root;database=condominio";
        MySqlConnection conexao = new MySqlConnection(strConexao);
        MySqlCommand comando = new MySqlCommand();
        MySqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Testando a conexão ao abrir o projeto.
            try
            {
                // server = endereço (geralmente localhost);
                // uid = user id (geralmente root)
                // pwd = password (geralmente root);
                // database = bando de dados.

                conexao.Open();
                MessageBox.Show("Conexão bem sucedida!\nClique em \"OK\" para continuar.");
                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar conectar no banco de dados: " + ex.Message);
            }
        }

        // Cadastrando um morador
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {

                    conexao.Open();
                    string nome = textBox1.Text;
                    string apartamento = textBox2.Text;
                    comando.Connection = conexao;
                    comando.CommandText = $"INSERT INTO moradores(nome, apartamento) VALUES('{nome}','{apartamento}')";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Morador cadastrado com exito! ");
                    conexao.Close();
                    // Limpa os campos para um novo registro
                    textBox1.Text = "";
                    textBox2.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
        }

        // Cadastrando evento
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    conexao.Open();
                    string data = textBox3.Text;
                    string nomeEvento = textBox4.Text;
                    string apartamento = textBox5.Text;
                    int qtdPessoas = int.Parse(textBox6.Text);
                    comando.Connection = conexao;
                    comando.CommandText = $"INSERT INTO evento(dataEvento, nomeEvento, apartamentoResponsavel, quantidadePessoas) VALUES('{data}','{nomeEvento}','{apartamento}', {qtdPessoas})";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Evento cadastrado com sucesso!");
                    conexao.Close();
                    // Limpa os campos para um novo registro
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox5.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
        }

        // Listar moradores
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            conexao.Open();
            comando.Connection = conexao;
            comando.CommandText = $"SELECT * FROM moradores";
            dr = comando.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add($"Apartamento {dr["apartamento"]} - Morador: {dr["nome"]}");
                listBox1.Items.Add("------------------------------------------------------------------------------");
            }
            conexao.Close();
        }

        // Listar eventos
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                listBox2.Items.Clear();
                conexao.Open();
                comando.Connection = conexao;
                comando.CommandText = $"SELECT * FROM evento";
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    listBox2.Items.Add($"Data do evento: {dr["dataEvento"]}");
                    listBox2.Items.Add($"- Nome do evento: {dr["nomeEvento"]}");
                    listBox2.Items.Add($"- Apartamento responsável: {dr["apartamentoResponsavel"]}");
                    listBox2.Items.Add($"- Quantidade de pessoas: {dr["quantidadePessoas"]};");
                    listBox2.Items.Add("------------------------------------------------------------------------------");
                }
                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
        }

        // Verificar se o usuário existe para uma possível alteração de dados
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox7.Text == "" || textBox8.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    string nome = textBox7.Text;
                    string apartamento = textBox8.Text;

                    conexao.Open();
                    comando.Connection = conexao;
                    comando.CommandText = $"SELECT * from moradores where nome = '{nome}' AND apartamento = '{apartamento}'";

                    int existe = (int)comando.ExecuteScalar();
                    conexao.Close();

                    if (existe > 0)
                    {
                        MessageBox.Show("Morador encontrado!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
        }

        // Altera os dados do morador
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox9.Text == "" || textBox10.Text == "" || textBox7.Text == "" || textBox8.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    string nome = textBox9.Text;
                    string apartamento = textBox10.Text;

                    conexao.Open();
                    comando.Connection = conexao;
                    comando.CommandText = $"UPDATE moradores SET nome = '{nome}', apartamento = '{apartamento}' WHERE nome = '{textBox7.Text}' AND apartamento = '{textBox8.Text}'";
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    MessageBox.Show("Dados alterados com sucesso!");
                    // Limpa os campos para um novo registro
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox7.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
        }

        // Verifica se existe um evento nessa data para uma possível alteração
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox15.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    string data = textBox15.Text;

                    conexao.Open();
                    comando.Connection = conexao;
                    comando.CommandText = $"SELECT * from evento where dataEvento = '{data}'";

                    int existe = (int)comando.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Evento encontrado!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
            conexao.Close();
        }

        // Atualiza os dados do evento
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox15.Text == "" || textBox11.Text == "" || textBox12.Text == "" || textBox13.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    string nome = textBox11.Text;
                    string apartamento = textBox12.Text;
                    int quantidade = int.Parse(textBox13.Text);
                    conexao.Open();
                    comando.Connection = conexao;
                    comando.CommandText = $"UPDATE evento SET nomeEvento = '{nome}', apartamentoResponsavel = '{apartamento}', quantidadePessoas = {quantidade} WHERE dataEvento = '{textBox15.Text}'";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    MessageBox.Show("Evento atualizado!");
                    // Limpa os campos para um novo registro
                    textBox15.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                    textBox13.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
        }

        // Verificar se o morador existe para uma possível exclusão de dados
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox14.Text == "" || textBox16.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    string nome = textBox14.Text;
                    string apartamento = textBox16.Text;

                    conexao.Open();
                    comando.Connection = conexao;
                    comando.CommandText = $"SELECT * from moradores where nome = '{nome}' AND apartamento = '{apartamento}'";

                    int existe = (int)comando.ExecuteScalar();

                    conexao.Close();
                    if (existe > 0)
                    {
                        MessageBox.Show("Morador encontrado!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
        }

        // Exclui o evento informado anteriormente
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox14.Text == "" || textBox16.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    string nome = textBox14.Text;
                    string apartamento = textBox16.Text;

                    conexao.Open();
                    comando.Connection = conexao;
                    comando.CommandText = $"DELETE FROM moradores WHERE nome = '{nome}' AND apartamento = '{apartamento}'";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    MessageBox.Show("Registro excluído com sucesso!");

                    // Limpa os campos para um novo registro
                    textBox14.Text = "";
                    textBox16.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
        }

        // Verifica se o evento existe para uma possível exclusão
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox17.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    string data = textBox17.Text;

                    conexao.Open();
                    comando.Connection = conexao;
                    comando.CommandText = $"SELECT * from evento where dataEvento = '{data}'";

                    int existe = (int)comando.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Evento encontrado!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
            conexao.Close();
        }

        // Exclui o evento selecionado previamente
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se todos os campos estão preenchidos
                if (textBox17.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    string data = textBox17.Text;

                    conexao.Open();
                    comando.Connection = conexao;
                    comando.CommandText = $"DELETE FROM evento WHERE dataEvento = '{data}'";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    MessageBox.Show("Registro excluído com sucesso!");

                    // Limpa os campos para um novo registro
                    textBox17.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo deu errado: " + ex.Message);
            }
            conexao.Close();

        }

    }
}

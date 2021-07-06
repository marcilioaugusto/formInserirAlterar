using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TI89
{
    public partial class frmCadastro : Form
    {
        public frmCadastro()
        {
            InitializeComponent();
        }


        private void frmCadastro_Load(object sender, EventArgs e)
        {
            btnAlterar.Enabled = false; // botão fica desabilitado
            btnExcluir.Enabled = false;
        }

        public void Limpa() 
        {
            txtID.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;

            //
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //instanciar 
            Cliente us = new Cliente(txtNome.Text,txtEmail.Text);
            us.Inserir();
            MessageBox.Show(us.mensagem,"Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
            txtID.Text = Convert.ToString(us.id);
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            Cliente us = new Cliente();
            // testar Verificação
            if (txtID.Text == string.Empty) // se a caixa do formulario estiver vazio 
            {
                MessageBox.Show("Informar o id de busca", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtID.Focus();
                return;  // verifica se está vazio se estiver ele retorna 

            }
            else
            {
                us.Consultar(Convert.ToInt32(txtID.Text));
            }
            // verificar o valor das variaveis 
            if (us.achou == true)
            {
                txtNome.Text = us.Nome;
                txtEmail.Text = us.Email;
                btnAlterar.Enabled = true; // botão fica habilitado
                btnExcluir.Enabled = true;
            }
            else
            {
                MessageBox.Show(us.mensagem, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpa();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Cliente us = new Cliente(Convert.ToInt32(txtID.Text), txtNome.Text, txtEmail.Text);
            us.Alterar();
            MessageBox.Show(us.mensagem, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

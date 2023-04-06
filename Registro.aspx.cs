using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace confabular
{
    public partial class Registro : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtNome.Text = "";
                txtUser.Text = "";
                txtEmail.Text = "";
                txtBio.Text = "";
                Checkmail_View.Checked = false;
                CheckCake_View.Checked = false;                
                btnSalvar.Visible = true;     
                PanelEdit.Visible = true;
                PanelSenha.Visible = true;
                lblMensagem.Text = "";
            }
        }                     
                  
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
           // Testa se já foi cadastrado (Usuário ou email)
            var t = dados.PopulaComDataSetPersonalizado("SELECT usuario, mail FROM cfuser WHERE usuario = '"
                + gen.AntiSqlInjection(txtUser.Text) + "' or mail = '" + gen.AntiSqlInjection(txtEmail.Text) + "'").Tables;
            
            var dataTable = t[0]; // seleciona a primeira tabela do conjunto
            if (dataTable.Rows.Count > 0)              
            {
                lblMensagem.Text = gen.MensagensOldMan("Usuário ou e-mail<br> já cadastrado.");
               
                return;
            }

            if (txtSenha.Text.Length < 5)
            {
                lblMensagem.Text = gen.MensagensOldMan("Sua senha precia ter<br> mais de 5 caractéres.");
                return;
            }

            // Cadastra
            var d = dados.SqlInsert("cfuser", this, new Dictionary<string, string>()
            {
                {"nome",  gen.AntiSqlInjection(txtNome.Text) },
                {"usuario", gen.AntiSqlInjection( txtUser.Text) },          
                {"mail",  gen.AntiSqlInjection(txtEmail.Text) },
                {"mail_view", Checkmail_View.Checked.ToString() },
                {"bio",  gen.AntiSqlInjection(txtBio.Text) },
                {"genero", dropGenero.SelectedValue },
                {"cake_view", CheckCake_View.Checked.ToString() },
                {"tipo_user", "1" },
                {"status", "1" }, // 1 = Ativo    
                {"cakeday", DateTime.Now.ToString() },
                {"pass", gen.Criptografar(txtSenha.Text) }
            });

            if (d.ToLower().Contains("sucesso"))
            {
                lblMensagem.Text = gen.MensagensOldMan("Bem-vindo(a)<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Confabulista!"); 
                Response.Redirect("MyBook.aspx");
            }
        }            
    }
}
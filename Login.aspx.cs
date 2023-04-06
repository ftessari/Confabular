using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace confabular
{
    public partial class Login : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Visible = false;

            // Mensagem Externa
            if ((Session["msg"] != null) && (Session["msg"].ToString() != ""))
            {
                lblMensagem.Text = gen.MensagensErro(Convert.ToString(Session["msg"]));
                Session["msg"] = "";
                txtUser.Focus();
            }
            
            // mensagem Interna (de login)
            if (lblMensagem.Text != "") 
            {
                lblMensagem.Visible = true;
            }

            lblReg.Text = gen.MensagensOldMan("Ainda não faz parte?<br><a href='Registro.aspx' >Registre-se</a>");

            if (!IsPostBack) // carrega somente uma vez
            {
                if (Convert.ToInt32(Session["id"]) < 1)
                {
                    txtUser.Focus();
                    //  PopulaGrid();
                    //  dados.PopulaDrop("select * from cftipuser", dropTipoUser); // DropBox do Tipo de usuário    
                }
                else
                {
                    Response.Redirect("myBook.aspx");
                }
            }

            Page.Form.DefaultButton = btnLogar.UniqueID; // Botão padrão da página. Para usar Enter
        }

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            ClasseDados dados = new ClasseDados();
            ClasseGenerica gen = new ClasseGenerica();

            //     var d = dados.PopulaComDataSetPersonalizado("SELECT * FROM cfuser WHERE usuario = '" + gen.AntiSqlInjection(txtUser.Text)+"'").Tables;

            var d = dados.PopulaComDataSetPersonalizado("SELECT * FROM cfuser WHERE (usuario = '" + gen.AntiSqlInjection(txtUser.Text) + "' or mail = '" +
                gen.AntiSqlInjection(txtUser.Text) + "') and pass = '" + gen.AntiSqlInjection(gen.Criptografar(txtSenha.Text)) + "'").Tables;

            if (d.Count != 1)
            {
                lblMensagem.Text = gen.MensagensErro("Usuário ou senha incorretos. :(");
                txtUser.Focus();
                return;
            }

            try
            {
                if (d[0].Rows[0]["status"].ToString() == "0")
                {
                    lblMensagem.Text = gen.MensagensErro("Atenção! Usuário desativado. Algumas funcionalidades estão desabilitadas. Habilite em suas configurações.");
                }

                if ((d[0].Rows[0]["status"].ToString() == "0") || (d[0].Rows[0]["status"].ToString() == "1"))
                {
                    Session["id"] = Convert.ToInt32(d[0].Rows[0]["id"].ToString());
                    Session["usuario"] = d[0].Rows[0]["usuario"].ToString();
                    Session["tipo_user"] = Convert.ToInt32(d[0].Rows[0]["tipo_user"].ToString());
                    Session["status"] = Convert.ToInt32(d[0].Rows[0]["status"].ToString());
                    Session["avatar"] = d[0].Rows[0]["avatar"].ToString();

                    Response.Redirect("MyBook.aspx");
                }
                else if (d[0].Rows[0]["status"].ToString() == "2")
                {
                    lblMensagem.Text = gen.MensagensErro("Usuário banido. :( Talvez você queira entre em contato com a Adminstração.");
                    Response.Redirect("index.aspx");
                }
            }
            catch
            {
                lblMensagem.Text = gen.MensagensErro("Erro na autenticação. Tente novamente ou entre em contado com a Administração.");
            }
        }
    }
}
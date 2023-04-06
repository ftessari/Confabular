using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace confabular
{
    public partial class CadUser : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["tipo_user"]) == 2) // admin
                {
                    PopulaGrid();

                    dados.PopulaDrop("select * from cftipuser", dropTipoUser); // DropBox do Tipo de usuário  
                }
                else
                {
                    Response.Redirect("index.aspx");
                }       
            }
        }

        private void PopulaGrid()
        {
            dados.populaGrid(@"SELECT USU.*, 
                             CASE  
                               WHEN USU.status = 1 THEN 'Ativo' 
                               WHEN USU.status = 2 THEN 'Bloqueado'
                               WHEN USU.status = 3 THEN 'Banido'
                                 ELSE 'Desativado' end AS USTATUS,
                             TIP.tipo from cfuser USU
                                 inner join cftipuser TIP on USU.tipo_user = TIP.id
                                 order by nome", GridView1);
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            PanelEdit.Visible = false;
            PanelSenha.Visible = false;
            PanelGrid.Visible = true;

            btnNovo.Visible = true;
            btnSalvar.Visible = false;
            btnVoltar.Visible = false;
            btnAlterar.Visible = false;
            BtnExcluir.Visible = false;

            lblMensagem.Text = "";
            lblMensagemGrid.Text = "";
        }
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtUser.Text = "";
            txtEmail.Text ="";
            txtBio.Text = "";
            imgAvatar.Visible = true;            
            Checkmail_View.Checked = false;
            CheckCake_View.Checked = false;
            dropTipoUser.SelectedIndex = 0;
            dropStatus.SelectedIndex = 0;
            btnVoltar.Visible = true;
            btnNovo.Visible = true;
            btnAlterar.Visible = false;
            BtnExcluir.Visible = false;
            btnSalvar.Visible = true;
            btnSalvaSenha.Visible = false;
            btnAvatar.Visible = false;
            fileAvatar.Visible = false;

            PanelGrid.Visible = false;
            PanelEdit.Visible = true;
            PanelSenha.Visible = true;

            lblMensagem.Text = "";
            lblMensagemGrid.Text = "";
            imgAvatar.ImageUrl = @"imgs\avatar\Thumb\avatar.png";
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var d = dados.PopulaComDataSetPersonalizado("SELECT * FROM cfuser WHERE id = " + GridView1.SelectedValue).Tables[0].Rows[0];
            txtNome.Text = d["nome"].ToString();
            txtUser.Text = d["usuario"].ToString();
            txtEmail.Text = d["mail"].ToString();
            txtBio.Text = d["bio"].ToString();

            if (d["avatar"].ToString() != "")
            {
                imgAvatar.ImageUrl = @"imgs\avatar\Thumb\" + d["avatar"].ToString();
            }
            else
            {
                imgAvatar.ImageUrl = @"imgs\avatar\Thumb\avatar.png";
            }

            dropGenero.SelectedValue = d["genero"].ToString();
            if (d["mail_view"].ToString() == "True")
            {
                Checkmail_View.Checked = true;
            }
            else
            {
                Checkmail_View.Checked = false;
            }

            if (d["cake_view"].ToString() == "True")
            {
                CheckCake_View.Checked = true;
            }
            else
            {
                CheckCake_View.Checked = false;
            }

            dropTipoUser.SelectedValue = d["tipo_user"].ToString();
            dropStatus.SelectedValue = d["status"].ToString();

            btnVoltar.Visible = true; 
            btnNovo.Visible = true;
            btnSalvar.Visible = false;
            btnAlterar.Visible = true;
          //  BtnExcluir.Visible = true;
            btnSalvaSenha.Visible = true;
            btnAvatar.Visible = true;
            fileAvatar.Visible = true;
            imgAvatar.Visible = true;

            PanelGrid.Visible = false;
            PanelEdit.Visible = true;
            PanelSenha.Visible = true;
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            var d = dados.SqlUpdate("cfuser", "id = " + GridView1.SelectedValue, new Dictionary<string, string>()
            {
                {"nome",  gen.AntiSqlInjection(txtNome.Text) },
                {"usuario",  gen.AntiSqlInjection(txtUser.Text) },
                {"mail",  gen.AntiSqlInjection(txtEmail.Text) },
                {"mail_view", Checkmail_View.Checked.ToString() },
                {"bio",  gen.AntiSqlInjection(txtBio.Text) },
                {"genero", dropGenero.SelectedValue },
                {"cake_view", CheckCake_View.Checked.ToString() },
                {"tipo_user", dropTipoUser.SelectedValue },
                {"status", dropStatus.SelectedValue} // Desativado = 0; Ativo = 1; Bloqueado = 2                 
             
        });

            lblMensagem.Text = d; // Mensagem de confirmação

            if (d.ToLower().Contains("sucesso"))
            {
                txtNome.Text = "";
                txtUser.Text = "";
                txtEmail.Text = "";
                Checkmail_View.Checked = false;
                dropGenero.SelectedIndex = 0;
                CheckCake_View.Checked = false;
                dropTipoUser.SelectedIndex = 0;
                dropStatus.SelectedIndex = 0;

                PopulaGrid();

                btnNovo.Visible = true;
                btnSalvar.Visible = true;
                btnAlterar.Visible = false;
                BtnExcluir.Visible = false;
                btnSalvaSenha.Visible = true;
                btnAvatar.Visible = false;
                fileAvatar.Visible = false;
                PanelEdit.Visible = false;
                PanelSenha.Visible = false;
                PanelGrid.Visible = true;
                btnVoltar.Visible = false;
            }
        }

        protected void BtnExcluir_Click(object sender, EventArgs e)
        {
            var d = dados.DeletaRegistro("cfuser", "id", Convert.ToInt32(GridView1.SelectedValue));

            lblMensagemGrid.Text = d;

            if (d.ToLower().Contains("sucesso"))
            {
                txtNome.Text = "";
                txtUser.Text = "";
                txtEmail.Text = "";
                Checkmail_View.Checked = false;
                dropGenero.SelectedIndex = 0;
                CheckCake_View.Checked = false;
                dropTipoUser.SelectedIndex = 0;
                dropStatus.SelectedIndex = 0;

                PopulaGrid();
                btnNovo.Visible = true;
                btnSalvar.Visible = false;
                btnAlterar.Visible = false;
                BtnExcluir.Visible = false;
                btnSalvaSenha.Visible = false;
                btnAvatar.Visible = false;
                fileAvatar.Visible = false;

                PanelEdit.Visible = false;
                PanelSenha.Visible = false;
                PanelGrid.Visible = true;
                btnVoltar.Visible = false;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

           // Testa se já foi cadastrado (Usuário ou email)
            var t = dados.PopulaComDataSetPersonalizado("SELECT usuario, mail FROM cfuser WHERE usuario = '"
                + gen.AntiSqlInjection(txtUser.Text) + "' or mail = '" + gen.AntiSqlInjection(txtEmail.Text) + "'").Tables;

            if (t.Count > 0)
            {
                lblMensagem.Text = gen.MensagensErro("Usuário ou e-mail já cadastrado.");
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
                {"tipo_user", dropTipoUser.SelectedValue },
                {"status", dropStatus.SelectedValue }, // 1 = Ativo    
                {"cakeday", DateTime.Now.ToString() },
                {"pass", gen.Criptografar(txtSenha.Text) }
            });

            lblMensagemGrid.Text = d;

            if (d.ToLower().Contains("sucesso"))
            {
                txtNome.Text = "";
                txtUser.Text = "";
                txtEmail.Text = "";
                Checkmail_View.Checked = false;
                dropGenero.SelectedIndex = 0;
                CheckCake_View.Checked = false;
                dropTipoUser.SelectedIndex = 0;
                dropStatus.SelectedIndex = 1;

                btnAlterar.Visible = false;
                BtnExcluir.Visible = false;
                btnSalvaSenha.Visible = false;
                btnSalvar.Visible = false;
                btnNovo.Visible = true;
                btnVoltar.Visible = false;
                PanelEdit.Visible = false;
                PanelSenha.Visible = false;
                PanelGrid.Visible = true;

                PopulaGrid();
            }
        }

        protected void btnSalvaSenha_Click(object sender, EventArgs e)
        {
            var d = dados.SqlUpdate("cfuser", "id = " + GridView1.SelectedValue, new Dictionary<string, string>()
            {
                {"pass", gen.Criptografar(txtSenha.Text) }
            });

            lblMensagemGrid.Text = d; // Mensagem de confirmação

            if (d.ToLower().Contains("sucesso"))
            {
                txtSenha.Text = "";
                txConftSenha.Text = "";
                PopulaGrid();

                btnNovo.Visible = true;
                PanelEdit.Visible = false;
                PanelSenha.Visible = false;
                btnVoltar.Visible = false;
                PanelGrid.Visible = true;
                btnSalvaSenha.Visible = false;
            }
        }

        protected void btnAvatar_Click(object sender, EventArgs e)
        {
            var CurrentDirectory = HttpContext.Current.Server.MapPath("~");

            // Limpar diretório de arquivos cru 
            string directoryPath = CurrentDirectory + @"imgs\avatar";
            string[] imageFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            var imageFiles = from file in directory.GetFiles()
                             where imageFileExtensions.Contains(file.Extension.ToLowerInvariant())
                             select file;

            foreach (var file in imageFiles)
            {
                file.Delete();
            }
            // Limpar diretório de arquivos cru - fim
            
            ClasseImagens img = new ClasseImagens();

            // Pega imagem antiga            
            var a = dados.PopulaComDataSetPersonalizado("SELECT avatar FROM cfuser WHERE id = " + GridView1.SelectedValue).Tables[0].Rows[0];
            string antAvatar = a["avatar"].ToString();

            var imgNm = img.resizeImgPersonalizado(fileAvatar, this, "imgs\\avatar", "A", 200, 200);

            var d = dados.SqlUpdate("cfuser", "id = " + GridView1.SelectedValue, new Dictionary<string, string>()
            {
                 { "avatar", imgNm }
            });

            lblMensagemGrid.Text = d; // Mensagem de confirmação

            if (d.ToLower().Contains("sucesso"))
            {
                PopulaGrid();
                btnNovo.Visible = true;
                PanelEdit.Visible = false;
                PanelSenha.Visible = false;
                btnVoltar.Visible = false;
                PanelGrid.Visible = true;
                btnAvatar.Visible = false;
                fileAvatar.Visible = false;

                // Deleta imagens antigas
                if ((antAvatar.Trim() != "") && (antAvatar != null))
                {
                    File.Delete(CurrentDirectory + @"imgs\avatar\Thumb\" + antAvatar);
                }
            }

            if (imgNm != "")
            {
                imgAvatar.ImageUrl = @"imgs\avatar\Thumb\" + imgNm;
            }
            else
            {
                imgAvatar.ImageUrl = @"imgs\avatar\Thumb\avatar.png";
            }
        }

    }
}
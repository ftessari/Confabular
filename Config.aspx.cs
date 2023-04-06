using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace confabular
{
    public partial class Config : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulaGrid();

                //  dados.PopulaDrop("select * from cftipuser", dropTipoUser); // DropBox do Tipo de usuário    
            }
        }

        private void PopulaGrid()
        {
            if (Convert.ToInt32(Session["id"]) > 0)
            {
                var strSql = @"SELECT USU.*, 
                             CASE  
                               WHEN USU.status = 1 THEN 'Ativo' 
                               WHEN USU.status = 2 THEN 'Banido'
                                 ELSE 'Desativado' 
                             End AS USTATUS,
                                TIP.tipo 
                             From cfuser USU
                                 inner join cftipuser TIP on USU.tipo_user = TIP.id
                                 where USU.id = " + Convert.ToString(Session["id"]);

                // dados.populaGrid(strSql, GridView1);
                var d = dados.PopulaComDataSetPersonalizado(strSql).Tables[0].Rows[0];

                txtNome.Text = d["nome"].ToString();
                txtUser.Text = d["usuario"].ToString();
                txtEmail.Text = d["mail"].ToString();
                txtBio.Text = d["bio"].ToString();
                dropGenero.SelectedValue = d["genero"].ToString();

                if (d["avatar"].ToString() != "")
                {
                    imgAvatar.ImageUrl = @"imgs\avatar\Thumb\" + d["avatar"].ToString();
                }
                else
                {
                    imgAvatar.ImageUrl = @"imgs\avatar\Thumb\avatar.png";
                }

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

                dropStatus.SelectedValue = d["status"].ToString();

                btnAlterar.Visible = true;
                btnSalvaSenha.Visible = true;
                btnAvatar.Visible = true;
                fileAvatar.Visible = true;
                imgAvatar.Visible = true;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            var d = dados.SqlUpdate("cfuser", "id = " + Convert.ToString(Session["id"]), new Dictionary<string, string>()
            {
                {"nome", gen.AntiSqlInjection(txtNome.Text) },
             //   {"usuario", txtUser.Text },
                {"mail", gen.AntiSqlInjection(txtEmail.Text) },
                {"bio",  gen.AntiSqlInjection(txtBio.Text) },
                {"mail_view", Checkmail_View.Checked.ToString() },
                {"genero", dropGenero.SelectedValue },
                {"cake_view", CheckCake_View.Checked.ToString() },
                {"status", dropStatus.SelectedValue} // Desativado = 0; Ativo = 1; Bloqueado = 2                
             
        });

            lblMensagem.Text = d; // Mensagem de confirmação

            if (d.ToLower().Contains("sucesso"))
            {
                PopulaGrid();
            }
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx?u=" + Session["usuario"]);
        }

        protected void btnSalvaSenha_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text != txConftSenha.Text)
            {
                lblMensagem.Text = gen.MensagensErro("Não foi possível salvar. As senhas não coincidem.");
                return;
            }

            var d = dados.SqlUpdate("cfuser", "id = " + Convert.ToInt32(Session["id"]), new Dictionary<string, string>()
            {
                {"pass", gen.Criptografar(txtSenha.Text) }
            });

            lblMensagem.Text = d; // Mensagem de confirmação

            if (d.ToLower().Contains("sucesso"))
            {
                txtSenha.Text = "";
                txConftSenha.Text = "";
                PopulaGrid();
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
            var a = dados.PopulaComDataSetPersonalizado("SELECT avatar FROM cfuser WHERE id = " + Convert.ToInt32(Session["id"])).Tables[0].Rows[0];
            string antAvatar = a["avatar"].ToString();

            var imgNm = img.resizeImgPersonalizado(fileAvatar, this, "imgs\\avatar", "A", 200, 200);

            var d = dados.SqlUpdate("cfuser", "id = " + Convert.ToInt32(Session["id"]), new Dictionary<string, string>()
            {
                 { "avatar", imgNm }
            });
            Session["avatar"] = imgNm;

            lblMensagem.Text = d; // Mensagem de confirmação

            if (d.ToLower().Contains("sucesso"))
            {
                // Deleta imagens antigas
                if ((antAvatar.Trim() != "") && (antAvatar != null))
                {
                    File.Delete(CurrentDirectory + @"imgs\avatar\Thumb\" + antAvatar);
                }

                PopulaGrid();
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
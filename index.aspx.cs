using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace confabular
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["id"]) > 0) // Usuário logado
                {
                    lblTop.Text = "";
                }
                else
                {
                    lblTop.Text = gen.MensagensOldMan("Bem-vindo(a)<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ao Confabular!");
                    lblTop.Text += "<p>Um lugar dedicado a contar histórias e fazer amigos.<br><br><br><a href=\"Login.aspx\">[login]</a> <a href=\"Registro.aspx\">[Registre-se]</a></p>";
                }

                var busca = Convert.ToString(Session["veja"]); // termo de pesquisa
                if ((busca != null) && (busca.Trim() != ""))
                {
                    PesquisaLivros(busca);
                }
                else
                {
                    PesquisaLivros("");
                }
            }
        }

        private void PesquisaLivros(string veja)
        {
            try
            {
                // Criação da tabela HTML usando um StringBuilder
                var sql = @"SELECT TOP 9 BOK.id, BOK.titulo, BOK.subtitulo,
                            GEN.genero, BOK.id_user, BOK.img, BOK.finais, BOK.tags, BOK.resumo, USU.nome, USU.avatar 
                            From cfbook BOK
                             left join cfgenbook GEN on BOK.genero = GEN.id                            
                             left join cfUser    USU on BOK.id_user = USU.id
                            Where BOK.STATUS = 1 ";

                lblTopTitle.Text = "Novos";

                if (veja.Trim() != "")
                {
                    sql += " and BOK.titulo like '%" + veja + "%'" +
                           " or BOK.subtitulo like '%" + veja + "%'" +
                           " or BOK.genero like '" + veja + "%'" +
                           " or BOK.resumo like '%" + veja + "%'" +
                           " or USU.nome like '" + veja + "%'";

                    lblTopTitle.Text = "Pesquisa: " + veja;
                }
                Session["veja"] = ""; // Limpa pesquisa

                var livros = dados.PopulaComDataSetPersonalizado(sql + " Order by atual desc").Tables[0].Rows;
                string strCapa = "";
                string strAvatar = "";
                // Mostrar Vitrine
                lblLivros.Text = "<div class='row'>";
                foreach (DataRow lv in livros)
                {
                    if (File.Exists(Server.MapPath("~") + @"imgs\pages\Thumb\" + lv["img"].ToString()))
                    {
                        strCapa = @"imgs\pages\Thumb\" + lv["img"].ToString();
                    }
                    else
                    {
                        strCapa = @"imgs\pages\Thumb\book_cover.png";
                    }

                    // Avatar
                    if (File.Exists(Server.MapPath("~") + @"imgs\avatar\Thumb\" + lv["avatar"].ToString()))
                    {
                        strAvatar = @"imgs\avatar\Thumb\" + lv["avatar"].ToString();
                    }
                    else
                    {
                        strAvatar = @"imgs\avatar\Thumb\avatar.png";
                    }

                    lblLivros.Text += "<div class='col-md-6 col-lg-4'>";
                    lblLivros.Text += "     <div class='card'>";
                    lblLivros.Text += "         <center>";
                    lblLivros.Text += "             <a href='" + @"\Bookz.aspx?book=" + lv["id"] + "' Visible='True'>";
                    lblLivros.Text += "                  <img class='card-img-top' Style='Height:220px; Width:180px;' src='" + strCapa + "' />";
                    lblLivros.Text += "             </a><br><a Width='100%' href='" + @"\Bookz.aspx?book=" + lv["id"] + "' Class='btn btn-info' Visible='True'>Jogar</a><br>";
                    lblLivros.Text += "         </center>";
                    lblLivros.Text += "         <div style='margin-left:15px'>";
                    lblLivros.Text += "             <div class='card-title'>";
                    lblLivros.Text += "                 <center>";
                    lblLivros.Text += "                     <b>" + lv["titulo"] + "</b><br>" + lv["subtitulo"];
                    lblLivros.Text += "                 </center>";
                    lblLivros.Text += "             </div>";
                    lblLivros.Text += "             <div class='card-block'>";
                    lblLivros.Text += "                 <div class='card-text'>";
                    lblLivros.Text += "                     <i>" + lv["resumo"] + "</i><br>";
                    lblLivros.Text += "                     <b>Genero:</b> " + lv["genero"] + "<br>";
                    lblLivros.Text += "                     <b>Finais:</b> [" + lv["finais"] + "]<br><br>";
                    lblLivros.Text += "                     <a href='" + @"\Profile.aspx?u=" + lv["nome"] + "'>";
                    lblLivros.Text += "                        <img class='img-circle elevation-2' Style='Height:32px; Width:32px;' src='" + strAvatar + "' /> ";
                    lblLivros.Text += lv["nome"];
                    lblLivros.Text += "                     </a>";
                    //  lblLivros.Text += "                     <b>Tag´s:</b> " + lv["tags"] + "<br>";
                    lblLivros.Text += "                 </div><br>";
                    lblLivros.Text += "             </div>";
                    lblLivros.Text += "         </div>";
                    lblLivros.Text += "     </div>";
                    lblLivros.Text += "</div>";
                }
                lblLivros.Text += "</div>";

            }
            catch
            {
                // não tem livros para esta pesquisa       
                lblTopTitle.Text = gen.MensagensOldMan("Pesquisa: <i> " + veja + "</i><br> não encontrada.");
                lblTop.Text = "";
            }
        }
    }
}
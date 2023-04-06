using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace confabular
{
    public partial class Prediletos : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Prediletos 
                try
                {
                    if (Convert.ToInt32(Session["id"]) < 1)
                    {
                        Response.Redirect("Login.aspx");
                    }

                    // Criação da tabela HTML usando um StringBuilder
                    var sql = @"SELECT TOP 9 BOK.id, BOK.titulo, BOK.subtitulo,
                            GEN.genero, BOK.id_user, BOK.img, BOK.finais, BOK.tags, BOK.resumo, USU.nome, USU.avatar  
                            From cfbook BOK
                             left join cfgenbook   GEN on BOK.genero = GEN.id                            
                             left join cfUser      USU on BOK.id_user = USU.id
                             left join bkfavoritos FAV on BOK.id = FAV.id_book
                            Where BOK.STATUS = 1 
                             and FAV.id_user = " + Session["id"].ToString();

                    var favoritos = dados.PopulaComDataSetPersonalizado(sql + " Order by atual desc").Tables[0].Rows;
                    string strCapa = "";
                    string strAvatar = "";

                    if (favoritos.Count > 0)
                    {
                        // Mostrar Vitrine
                        lblFavoritos.Text = "<div class='row'>";
                        foreach (DataRow fv in favoritos)
                        {
                            if (File.Exists(Server.MapPath("~") + @"imgs\pages\Thumb\" + fv["img"].ToString()))
                            {
                                strCapa = @"imgs\pages\Thumb\" + fv["img"].ToString();
                            }
                            else
                            {
                                strCapa = @"imgs\pages\Thumb\book_cover.png";
                            }

                            // Avatar
                            if (File.Exists(Server.MapPath("~") + @"imgs\avatar\Thumb\" + fv["avatar"].ToString()))
                            {
                                strAvatar = @"imgs\avatar\Thumb\" + fv["avatar"].ToString();
                            }
                            else
                            {
                                strAvatar = @"imgs\avatar\Thumb\avatar.png";
                            }

                            lblFavoritos.Text += "<div class='col-md-6 col-lg-4'>";
                            lblFavoritos.Text += "     <div class='card'>";
                            lblFavoritos.Text += "         <center>";
                            lblFavoritos.Text += "             <a href='" + @"\Bookz.aspx?book=" + fv["id"] + "' Visible='True'>";
                            lblFavoritos.Text += "                  <img class='card-img-top' Style='Height:220px; Width:180px;' src='" + strCapa + "' />";
                            lblFavoritos.Text += "             </a><br><a Width='100%' href='" + @"\Bookz.aspx?book=" + fv["id"] + "' Class='btn btn-info' Visible='True'>Jogar</a><br>";
                            lblFavoritos.Text += "         </center>";
                            lblFavoritos.Text += "         <div style='margin-left:15px'>";
                            lblFavoritos.Text += "             <div class='card-title'>";
                            lblFavoritos.Text += "                 <center>";
                            lblFavoritos.Text += "                     <b>" + fv["titulo"] + "</b><br>" + fv["subtitulo"];
                            lblFavoritos.Text += "                 </center>";
                            lblFavoritos.Text += "             </div>";
                            lblFavoritos.Text += "             <div class='card-block'>";
                            lblFavoritos.Text += "                 <div class='card-text'>";
                            lblFavoritos.Text += "                     <i>" + fv["resumo"] + "</i><br>";
                            lblFavoritos.Text += "                     <b>Genero:</b> " + fv["genero"] + "<br>";
                            lblFavoritos.Text += "                     <b>Finais:</b> [" + fv["finais"] + "]<br>";
                            //  lblFavoritos.Text += "                     <b>Tag´s:</b> " + lv["tags"] + "<br>";
                            lblFavoritos.Text += "                     <a href='" + @"\Profile.aspx?u=" + fv["nome"] + "'>";
                            lblFavoritos.Text += "                        <img class='img-circle elevation-2' Style='Height:32px; Width:32px;' src='" + strAvatar + "' /> ";
                            lblFavoritos.Text += fv["nome"];
                            lblFavoritos.Text += "                     </a>";
                            lblFavoritos.Text += "                 </div><br>";
                            lblFavoritos.Text += "             </div>";
                            lblFavoritos.Text += "         </div>";
                            lblFavoritos.Text += "     </div>";
                            lblFavoritos.Text += "</div>";
                        }
                        lblFavoritos.Text += "</div>";
                    }
                    else { lblFavoritos.Text = gen.MensagensOldMan("Você ainda não selecionou nenhum."); }

                }
                catch
                {
                    // não tem livros
                    lblFavoritos.Text = gen.MensagensErro("Ocorreu algum erro. :/");
                }
            }
        }
    }

}
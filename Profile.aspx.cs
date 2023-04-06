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
    public partial class Profile : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();
        string u = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                u = Request.QueryString["u"]; // Id_user
                var d = dados.PopulaComDataSetPersonalizado(@"SELECT *, tipo FROM cfuser us 
                                                            Left Join cftipuser tipo on tipo.id = us.tipo_user
                                                            WHERE usuario = '" + u + "'").Tables[0].Rows[0];

                lblNome.Text = d["nome"].ToString();
                lblUser.Text = d["usuario"].ToString();
                lblTipo_user.Text = d["tipo"].ToString();
                if (d["genero"].ToString() == "1")
                {
                    imgSex.ImageUrl = @"imgs\masc.png";
                }
                else if (d["genero"].ToString() == "2")
                {
                    imgSex.ImageUrl = @"imgs\fem.png";
                }
                else 
                {
                    imgSex.ImageUrl = @"imgs\usb.png";
                }

                /*  , status INT NOT NULL

                           */
                if (d["cake_view"].ToString() == "True")
                {
                    lblCakeday.Text = d["cakeday"].ToString();
                }
                else
                {
                    lblCakeday.Text = "";
                }

                if (d["mail_view"].ToString() == "True")
                {
                    lblMail.Text = d["mail"].ToString();
                }
                else
                {
                    lblMail.Text = "";
                }

                if (d["avatar"].ToString() != "")
                {
                    imgAvatar.ImageUrl = @"imgs\avatar\Thumb\" + d["avatar"].ToString();
                }
                else
                {
                    imgAvatar.ImageUrl = @"imgs\avatar\Thumb\avatar.png";
                }
                mmBio.Text = d["bio"].ToString();

                // Meus Livros --------------------
                try
                {
                    // Criação da tabela HTML usando um StringBuilder
                    var sql = @"SELECT TOP 9 BOK.id, BOK.titulo, BOK.subtitulo,
                            GEN.genero, BOK.id_user, BOK.img, BOK.finais, BOK.tags, BOK.resumo, USU.nome  
                            From cfbook BOK
                             left join cfgenbook GEN on BOK.genero = GEN.id                            
                             left join cfUser    USU on BOK.id_user = USU.id
                            Where BOK.STATUS = 1 
                         and BOK.id_user = " + d["id"].ToString();

                    var livros = dados.PopulaComDataSetPersonalizado(sql + " Order by atual desc").Tables[0].Rows;
                    string strCapa = "";
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

                        //  strCapa = @"imgs\pages\Thumb\" + lv["img"].ToString();

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
                        lblLivros.Text += "                     <b>Finais:</b> [" + lv["finais"] + "]<br>";
                        //  lblLivros.Text += "                     <b>Tag´s:</b> " + lv["tags"] + "<br>";
                     //   lblLivros.Text += "                     <b>Confabulista:</b> <a href='" + @"\Profile.aspx?u=" + lv["nome"] + "'>" + lv["nome"] + "</a>";
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
                    // não tem livros deste autor 
                }
                // Fim - Meus Livros --------------

                // Meus Favoritos -----------------
                try
                {
                    // Criação da tabela HTML usando um StringBuilder
                    var sql = @"SELECT TOP 9 BOK.id, BOK.titulo, BOK.subtitulo,
                            GEN.genero, BOK.id_user, BOK.img, BOK.finais, BOK.tags, BOK.resumo, USU.nome, USU.avatar  
                            From cfbook BOK
                             left join cfgenbook   GEN on BOK.genero = GEN.id                            
                             left join cfUser      USU on BOK.id_user = USU.id
                             left join bkfavoritos FAV on BOK.id = FAV.id_book
                            Where BOK.STATUS = 1 
                             and FAV.id_user = " + d["id"].ToString();

                    var favoritos = dados.PopulaComDataSetPersonalizado(sql + " Order by atual desc").Tables[0].Rows;
                    string strCapa = "";
                    string strAvatar = "";
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
                        lblFavoritos.Text +=                           fv["nome"];
                        lblFavoritos.Text += "                     </a>"; 
                        lblFavoritos.Text += "                 </div><br>";
                        lblFavoritos.Text += "             </div>";
                        lblFavoritos.Text += "         </div>";
                        lblFavoritos.Text += "     </div>";
                        lblFavoritos.Text += "</div>";
                    }
                    lblFavoritos.Text += "</div>";

                }
                catch
                {
                    // não tem livros   
                }
                // Fim - Meus Favoritos --------------                              

            }
        }
    }

}
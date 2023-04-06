using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace confabular
{
    public partial class Bookz : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HdfBook.Value = Request.QueryString["book"]; // Id ou titulo do livro
                var sql = "";

                // Busca por Id ou Título
                try
                {
                    if (Convert.ToInt32(HdfBook.Value) > 0)
                    {
                        sql = "bk.id = " + HdfBook.Value;
                    }
                }
                catch
                {
                    sql = "bk.titulo = '" + HdfBook.Value + "'";
                }

                try
                {
                    var d = dados.PopulaComDataSetPersonalizado(@"SELECT bk.*, us.usuario, us.id, ge.genero as GEN 
                                                    FROM cfbook bk 
                                                    Left join cfgenbook ge on bk.genero  = ge.id
                                                    Left join cfuser    us on bk.id_user = us.id 
                                                    WHERE " + sql
                                                    ).Tables[0].Rows[0];

                    // Libera sessões anteriores do jogo
                    Session["page"] = "0";

                    Session["livro"] = d["id"]; // Seleciona Livro
                    lblAutor.Text = d["usuario"].ToString();
                    lblTitulo.Text = d["titulo"].ToString();
                    lblSubTitulo.Text = d["subTitulo"].ToString();
                    lblSinopse.Text = d["resumo"].ToString();
                    lblTags.Text = d["tags"].ToString();
                    lblFinais.Text = d["finais"].ToString();
                    lblGen.Text = d["GEN"].ToString();

                    if (d["status"].ToString() == "2")
                    {
                        lblStatus.Text = "Bloqueado!";
                        btnJogar.Visible = false;
                    }
                    else
                    {
                        lblStatus.Text = "";
                        btnJogar.Visible = true;
                    }

                    if (File.Exists(Server.MapPath("~") + @"imgs\pages\Thumb\" + d["img"].ToString()))
                    {
                        imgCapa.ImageUrl = @"imgs\pages\Thumb\" + d["img"].ToString();
                    }
                    else
                    {
                        imgCapa.ImageUrl = @"imgs\pages\Thumb\book_cover.png";
                    }
                    /*
                    if (d["img"].ToString() != "")
                    */

                }
                catch
                {
                    Response.Redirect("index.aspx");
                }

                // Botão de favoritos
                if (Convert.ToInt32(Session["id"]) > 0)
                {
                    btnFav.Visible = true;

                    try 
                    {
                        var j = dados.PopulaComDataSetPersonalizado("SELECT * FROM bkfavoritos WHERE id_user =" + Session["id"] + " and id_book = " + HdfBook.Value).Tables[0].Rows[0];
                        btnFav.CssClass = "btn btn-secondary btnRem";
                    }
                    catch 
                    {
                        btnFav.CssClass = "btn btn-secondary btnAdd";
                    }
                }
                else
                {
                    btnFav.Visible = false;
                }
                /*
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
                                    imgAvatar.ImageUrl = @"imgs\avatar\ImgGaleriaThumb\" + d["avatar"].ToString();
                                }
                                else
                                {
                                    imgAvatar.ImageUrl = @"imgs\avatar\ImgGaleriaThumb\avatar.png";
                                }
                                mmBio.Text = d["bio"].ToString();


                                /*
                                 , mail VARCHAR(50) NOT NULL
                                 , mail_view BIT NULL
                                 , genero INT NOT NULL
                                 ,cakeday DATETIME NOT NULL
                                 , status INT NOT NULL
                                 ,cake_view BIT NULL
                                 ,tipo_user INT NOT NULL
                                 , usuario VARCHAR(30) NOT NULL
                                 , avatar VARCHAR(50) NULL
                                 ,bio TEXT NULL
                                */

            }
        }

        protected void btnJogar_Click(object sender, EventArgs e)
        {
            //   Session["livro"] = Convert.ToInt32();
            Response.Redirect("Board.aspx"); // id pego por Session
        }
        protected void btnFav_Click(object sender, EventArgs e)
        {
            lblMensagem.Visible = true;
            try
            {
                var j = dados.PopulaComDataSetPersonalizado("SELECT * FROM bkfavoritos WHERE id_user =" + Session["id"] + " and id_book = " + HdfBook.Value).Tables[0].Rows[0];

                // Remover dos Favoritos
                var d = dados.DeletaRegistro("bkfavoritos", "id", Convert.ToInt32(j["id"]));

                btnFav.CssClass = "btn btn-secondary btnAdd";
                               
                lblMensagem.Text = gen.MensagensOldMan("Livro removido<br> de seus Favoritos.");
            }
            catch
            {   // Adicionar nos Favoritos
                var i = dados.SqlInsert("bkfavoritos", this, new Dictionary<string, string>()
                {
                    {"id_user", (Session["id"].ToString()) },
                    {"id_book", HdfBook.Value }
                });

                btnFav.CssClass = "btn btn-secondary btnRem";

                lblMensagem.Text = gen.MensagensOldMan("Livro adicionado<br> em seus Favoritos.");
            }           
        }
    }
}
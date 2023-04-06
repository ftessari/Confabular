using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Configuration;

namespace confabular
{
    public partial class Board : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();

        public class Globals // Variáveis Globais
        {
            public static int intUp { get; set; }
            public static int intDown { get; set; }
            public static int intLeft { get; set; }
            public static int intRight { get; set; }
            public static int intQ1 { get; set; }
            public static int intQ2 { get; set; }
            public static int intQ3 { get; set; }
            public static int intQ4 { get; set; }
            public static int intQ5 { get; set; }
            public static int intD6Win { get; set; }
            public static int intD6Los { get; set; }
            public static int d6Dif { get; set; }
            public static string d6txtWin { get; set; }
            public static string d6txtLos { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /* Será importante para sistema de 'Save'
                 * if (Convert.ToInt32(Session["id"]) < 1)
                  {
                      Session["msg"] = Convert.ToString("Você precisa estar logado"); 
                      Response.Redirect("Login.aspx");   
                  }
                */

                // lblMensagem.Visible = false;

                string msg = Convert.ToString(Session["msg"]);
                if (msg != "")
                {
                    lblMensagem.Visible = true;
                    lblMensagem.Text = msg;
                    Session["msg"] = "";
                }

                // Caso não tenha página, vai para a 1ª
                try
                {
                    var d = dados.PopulaComDataSetPersonalizado(@"SELECT * FROM cfpagesbook 
                                                            WHERE id_book = '" + Session["livro"] + "'" +
                                                            " and ini_page = 1").Tables[0].Rows[0]; // TOP 1 = Pega somente a 1ª página

                    // Direciona página
                    if (Convert.ToInt32(Session["page"]) > 0)
                    {
                        d = dados.PopulaComDataSetPersonalizado(@"SELECT * FROM cfpagesbook WHERE id_book = " + Session["livro"] + " and id = " + Session["page"]).Tables[0].Rows[0];
                    }

                    if (d["titulo"].ToString() != "")
                    {
                        lblTitulo.Text = d["titulo"].ToString();
                    }
                    else
                    {
                        lblTitulo.Text = "";
                    }

                    if (d["texto"].ToString() != "")
                    {
                        txtTexto.Text = d["texto"].ToString();
                    }
                    else
                    {
                        txtTexto.Text = "";
                    }

                    lblImgInfo.Visible = false; // Somente em msg de erro

                    if ((d["img"].ToString() != "-- Não informar --") && (d["img"].ToString() != ""))
                    {
                        if (File.Exists(Server.MapPath("~") + @"imgs\pages\Thumb\" + d["img"].ToString()))
                        {
                            imgPage.ImageUrl = @"imgs\pages\Thumb\" + d["img"].ToString();
                        }
                        else
                        {
                            lblImgInfo.Visible = true;
                            lblImgInfo.Text = gen.MensagensOldMan("Imagem não<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;localizada");
                        }
                    }
                    else
                    {
                        imgPage.Visible = false;
                    }

                    // Direções
                    if (Convert.ToInt32(d["btn_up_link"].ToString()) > 0)
                    {
                        imgBtnUp.Visible = true;
                        Globals.intUp = Convert.ToInt32(d["btn_up_link"]);
                    }
                    else
                    {
                        imgBtnUp.Visible = false;
                    }

                    if (Convert.ToInt32(d["btn_down_link"].ToString()) > 0)
                    {
                        imgBtnDw.Visible = true;
                        Globals.intDown = Convert.ToInt32(d["btn_down_link"]);
                    }
                    else
                    {
                        imgBtnDw.Visible = false;
                    }

                    if (Convert.ToInt32(d["btn_left_link"].ToString()) > 0)
                    {
                        imgBtnLf.Visible = true;
                        Globals.intLeft = Convert.ToInt32(d["btn_left_link"]);
                    }
                    else
                    {
                        imgBtnLf.Visible = false;
                    }

                    if (Convert.ToInt32(d["btn_right_link"].ToString()) > 0)
                    {
                        imgBtnRg.Visible = true;
                        Globals.intRight = Convert.ToInt32(d["btn_right_link"]);
                    }
                    else
                    {
                        imgBtnRg.Visible = false;
                    }

                    // Enquete / Quest
                    if (d["enqt_01_caption"].ToString().Trim() != "")
                    {
                        btnQuest1.Text = d["enqt_01_caption"].ToString();
                        Globals.intQ1 = Convert.ToInt32(d["enqt_01_link"]);
                        btnQuest1.Visible = true;
                    }
                    else
                    {
                        btnQuest1.Visible = false;
                    }

                    if (d["enqt_02_caption"].ToString().Trim() != "")
                    {
                        btnQuest2.Text = d["enqt_02_caption"].ToString();
                        Globals.intQ2 = Convert.ToInt32(d["enqt_02_link"]);
                        btnQuest2.Visible = true;
                    }
                    else
                    {
                        btnQuest2.Visible = false;
                    }

                    if (d["enqt_03_caption"].ToString().Trim() != "")
                    {
                        btnQuest3.Text = d["enqt_03_caption"].ToString();
                        Globals.intQ3 = Convert.ToInt32(d["enqt_03_link"]);
                        btnQuest3.Visible = true;
                    }
                    else
                    {
                        btnQuest3.Visible = false;
                    }

                    if (d["enqt_04_caption"].ToString().Trim() != "")
                    {
                        btnQuest4.Text = d["enqt_04_caption"].ToString();
                        Globals.intQ4 = Convert.ToInt32(d["enqt_04_link"]);
                        btnQuest4.Visible = true;
                    }
                    else
                    {
                        btnQuest4.Visible = false;
                    }

                    if (d["enqt_05_caption"].ToString().Trim() != "")
                    {
                        btnQuest5.Text = d["enqt_05_caption"].ToString();
                        Globals.intQ5 = Convert.ToInt32(d["enqt_05_link"]);
                        btnQuest5.Visible = true;
                    }
                    else
                    {
                        btnQuest5.Visible = false;
                    }

                    if (Convert.ToInt32(d["d6Win"].ToString()) > 0)
                    {
                        imgBtnD6.Visible = true;
                        Globals.d6Dif = Convert.ToInt32(d["d6Dif"].ToString());   // Nível de Dificuldade do D6                      
                        Globals.intD6Win = Convert.ToInt32(d["d6Win"].ToString());
                        Globals.intD6Los = Convert.ToInt32(d["d6Los"].ToString());
                        Globals.d6txtWin = d["d6txtWin"].ToString();
                        Globals.d6txtLos = d["d6txtLos"].ToString();
                    }
                    else
                    {
                        imgBtnD6.Visible = false;
                    }

                }
                catch
                {
                    // Não existe a página &nbsp;
                    lblTitulo.Text = gen.MensagensOldMan("A página não existe.");

                    txtTexto.Visible = false;
                    imgBtnRg.Visible = false;
                    imgBtnLf.Visible = false;
                    imgBtnDw.Visible = false;
                    imgBtnUp.Visible = false;
                    imgPage.Visible = false;
                    btnQuest1.Visible = false;
                    btnQuest2.Visible = false;
                    btnQuest3.Visible = false;
                    btnQuest4.Visible = false;
                    btnQuest5.Visible = false;

                    Session["page"] = "0";
                }
            }
        }

        protected void btnDadosD6_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int resultado = random.Next(1, 7); // Gera um número aleatório entre 1 e 6 (inclusos)

            // string urlimg = Server.MapPath("~") + "imgs\\d6_1.gif"; 
           // imgD6.ImageUrl = "imgs\\d6_1.gif";
        
            imgBtnRg.Visible = false;
            imgBtnLf.Visible = false;
            imgBtnDw.Visible = false;
            imgBtnUp.Visible = false;
            imgBtnD6.Visible = false;
            btnQuest1.Visible = false;
            btnQuest2.Visible = false;
            btnQuest3.Visible = false;
            btnQuest4.Visible = false;
            btnQuest5.Visible = false;
            lblMensagem.Visible = true;

         //   updatePanel1.Update();

         //   TimerDados.Enabled = false; // desativa o Timer após exibir a imagem
        //    System.Threading.Thread.Sleep(1); // aguarda 1 segundos = 1000

          //  TimerDados.Enabled = true;


            if (resultado >= Globals.d6Dif)
            {
                Globals.d6txtWin = Convert.ToString("D6=[" + Convert.ToString(resultado) + "] " + Globals.d6txtWin);
                Session["msg"] = gen.MensagensInfoGeral(Globals.d6txtWin);
                Session["page"] = Globals.intD6Win;
            }
            else
            {
                Globals.d6txtLos = Convert.ToString("D6=[" + Convert.ToString(resultado) + "] " + Globals.d6txtLos);
                Session["msg"] = gen.MensagensErro(Globals.d6txtLos);
                Session["page"] = Globals.intD6Los;
            }

            Response.Redirect("Board.aspx");
        }

        // Próximo click -> Direção || Escolha
        protected void btnClickUp(object sender, EventArgs e)
        {
            Session["page"] = Globals.intUp;
            Response.Redirect("Board.aspx");
        }
        protected void btnClickDown(object sender, EventArgs e)
        {
            Session["page"] = Globals.intDown;
            Response.Redirect("Board.aspx");
        }
        protected void btnClickLeft(object sender, EventArgs e)
        {
            Session["page"] = Globals.intLeft;
            Response.Redirect("Board.aspx");
        }
        protected void btnClickRight(object sender, EventArgs e)
        {
            Session["page"] = Globals.intRight;
            Response.Redirect("Board.aspx");
        }
        // Quest´s
        protected void btnClickQ1(object sender, EventArgs e)
        {
            Session["page"] = Globals.intQ1;
            Response.Redirect("Board.aspx");
        }
        protected void btnClickQ2(object sender, EventArgs e)
        {
            Session["page"] = Globals.intQ2;
            Response.Redirect("Board.aspx");
        }
        protected void btnClickQ3(object sender, EventArgs e)
        {
            Session["page"] = Globals.intQ3;
            Response.Redirect("Board.aspx");
        }
        protected void btnClickQ4(object sender, EventArgs e)
        {
            Session["page"] = Globals.intQ4;
            Response.Redirect("Board.aspx");
        }
        protected void btnClickQ5(object sender, EventArgs e)
        {
            Session["page"] = Globals.intQ5;
            Response.Redirect("Board.aspx");
        }
    }
}
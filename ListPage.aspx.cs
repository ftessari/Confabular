using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace confabular
{
    public partial class Book : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();

        private static string imgNm = "";
        public static string ImgNm
        {
            get { return imgNm; }
            set { imgNm = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HdfBook.Value = Request.QueryString["book"]; // Id do livro

                if ((Convert.ToInt32(Session["id"]) > 0) && (HdfBook.Value != null))
                {
                    try
                    {
                        // Apenas para pegar o Título
                        var d = dados.PopulaComDataSetPersonalizado(@"Select BK.id, BK.titulo, BK.subTitulo, PG.titulo pgTitulo, PG.texto, PG.img, PG.nota
                                                                      From cfbook BK
                                                                        left join cfpagesbook PG ON BK.id = PG.id_book 
                                                                      WHERE BK.id =" + HdfBook.Value + " and BK.id_user =" + Session["id"]).Tables[0].Rows[0];

                        lblTitulo.Text = d["Titulo"].ToString();
                        lblSubTitulo.Text = d["subTitulo"].ToString();

                        // Lista de imagens
                        dados.PopulaDropMult(@"Select img, ref From imgRepo WHERE id_user = " + Session["id"], DropDownListImg, true);

                        PopulaGrid();
                    }
                    catch
                    {
                        NovaPagina();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void Img_Select(object sender, EventArgs e)
        {
            imgCena.Visible = true;
            if (DropDownListImg.Text != "-- Não informar --")
            {
                imgCena.ImageUrl = @"imgs\pages\Thumb\" + DropDownListImg.Text;
                imgNm = DropDownListImg.Text;
            }
            else
            {
                imgCena.ImageUrl = "";
                imgNm = "";
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            PopulaGrid();
            PanelEdit.Visible = false;
            Panel_Superior.Visible = false;
            btnSalvar.Visible = false;
            btnAlterar.Visible = false;
            BtnExcluir.Visible = false;
            btnVoltar.Visible = false;
            PanelGrid.Visible = true;
            lblNPages.Visible = true;

            // Imagem           
            imgCena.Visible = false;
            lblSize.Visible = false;
            fileCena.Visible = false;
            DropDownListImg.Visible = false;
            TextBoxRefImg.Visible = false;
        }

        protected void btnVoltar_LJ_Click(object sender, EventArgs e)
        {
            Response.Redirect("myBook.aspx");
        }

        protected void btnNovaPage_Click(object sender, EventArgs e)
        {
            NovaPagina();
        }
        protected void btnDelImg_Click(object sender, EventArgs e)
        {
            if (DropDownListImg.Text != "-- Não informar --")
            {
                try
                {
                    var s = dados.PopulaComDataSetPersonalizado(@"Select id, img from imgRepo where ref='" +
                             DropDownListImg.SelectedItem.Text + "' and id_user=" + Session["id"].ToString()).Tables[0].Rows[0];

                    var i = dados.DeletaRegistro("imgRepo", "id", Convert.ToInt32(s["id"]));

                    var CurrentDirectory = HttpContext.Current.Server.MapPath("~");
                    if (File.Exists(CurrentDirectory + @"imgs\pages\Thumb\" + s["img"]))
                    {
                        File.Delete(CurrentDirectory + @"imgs\pages\Thumb\" + s["img"]);
                    }

                    // Atualiza Lista de imagens
                    dados.PopulaDropMult(@"Select img, ref From imgRepo WHERE id_user = " + Session["id"], DropDownListImg, true);
                    imgCena.ImageUrl = "";
                    imgNm = "";

                    lblMensagem.Text = gen.MensagensOldMan("Imagem removida<br> de sua galeria.");
                }
                catch { }
            }
        }
        private void NovaPagina()
        {
            PopulaDrop();
            txtNota.Text = "";
            txtTitulo.Text = "";
            TextBoxTexto.Text = "";
            txtPagina.Text = "";
            CheckIniPage.Checked = false;
            TextBoxRefImg.Text = "";

            enqt_01_caption.Text = "";
            enqt_02_caption.Text = "";
            enqt_03_caption.Text = "";
            enqt_04_caption.Text = "";
            enqt_05_caption.Text = "";
            txtWin.Text = "";
            txtLos.Text = "";

            drop_d6_win.SelectedIndex = 0;
            drop_d6_los.SelectedIndex = 0;
            txtDif.Text = "4"; // Média = 4

            drop_up_link.SelectedIndex = 0;
            drop_down_link.SelectedIndex = 0;
            drop_left_link.SelectedIndex = 0;
            drop_right_link.SelectedIndex = 0;

            drop_enqt_01.SelectedIndex = 0;
            drop_enqt_02.SelectedIndex = 0;
            drop_enqt_03.SelectedIndex = 0;
            drop_enqt_04.SelectedIndex = 0;
            drop_enqt_05.SelectedIndex = 0;

            PanelEdit.Visible = true;
            Panel_Superior.Visible = true;
            lblNota.Visible = true;
            txtNota.Visible = true;
            txtTitulo.Visible = true;
            TextBoxTexto.Visible = true;
            txtPagina.Visible = true;

            enqt_01_caption.Visible = true;
            enqt_02_caption.Visible = true;
            enqt_03_caption.Visible = true;
            enqt_04_caption.Visible = true;
            enqt_05_caption.Visible = true;
            txtWin.Visible = true;
            txtLos.Visible = true;
            drop_up_link.Visible = true;
            drop_down_link.Visible = true;
            drop_left_link.Visible = true;
            drop_right_link.Visible = true;
            drop_enqt_01.Visible = true;
            drop_enqt_02.Visible = true;
            drop_enqt_03.Visible = true;
            drop_enqt_04.Visible = true;
            drop_enqt_05.Visible = true;

            // Controles
            btnSalvar.Visible = true;
            btnVoltar.Visible = true;
            btnAlterar.Visible = false;
            BtnExcluir.Visible = false;
            PanelGrid.Visible = false;
            lblNPages.Visible = false;

            // Imagem           
            imgCena.Visible = false;
            lblSize.Visible = true;
            fileCena.Visible = true;
            DropDownListImg.Visible = true;
            TextBoxRefImg.Visible = true;

            lblMensagem.Visible = false;
        }

        private void PopulaGrid()
        {
            try
            {   // Nº de Páginas
                var d = dados.PopulaComDataSetPersonalizado(@"SELECT COUNT(*) nPage from cfpagesbook where id_book =" + Request.QueryString["book"] + " and id_user =" + Session["id"]).Tables[0].Rows[0];
                lblNPages.Text = d["nPage"].ToString();
            }
            catch { lblNPages.Text = "0"; }
            lblNPages.Visible = true;

            dados.populaGrid(@"SELECT *, CASE WHEN ini_page = 1 THEN " + "'<img width=\"32\" src=\"imgs\\paged.gif\" />'" + " ELSE '' END pageINI from cfpagesbook where id_book =" + Request.QueryString["book"] + " and id_user =" + Session["id"], GridView1);
        }

        private void PopulaDrop()
        {
            try
            {
                // Setas
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_up_link, false);
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_down_link, false);
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_left_link, false);
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_right_link, false);
                // D6
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_d6_win, false);
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_d6_los, false);
                // Quest´s
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_enqt_01, false);
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_enqt_02, false);
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_enqt_03, false);
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_enqt_04, false);
                dados.PopulaDropMult("select id, pagina from cfpagesbook where id_book = '" + HdfBook.Value + "' and id_user = '" + Session["id"] + "'", drop_enqt_05, true);
            }
            catch { } // Não possui páginas
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaDrop();

            // Imagem
            lblSize.Visible = true;
            fileCena.Visible = true;
            DropDownListImg.Visible = true;
            TextBoxRefImg.Visible = true;
            lblMensagem.Visible = false;

            var d = dados.PopulaComDataSetPersonalizado(@"SELECT * FROM cfpagesbook
                                                         Left Join imgRepo ON cfpagesbook.img = imgRepo.img
                                                        WHERE cfpagesbook.id = " + GridView1.SelectedValue).Tables[0].Rows[0];

            txtNota.Text = d["nota"].ToString();
            txtTitulo.Text = d["Titulo"].ToString();
            TextBoxTexto.Text = d["texto"].ToString();
            txtPagina.Text = d["pagina"].ToString();
            if (d["ref"].ToString() != "")
            {
                TextBoxRefImg.Text = d["ref"].ToString();
                imgNm = d["ref"].ToString(); // Para poder editar
            }
            else
            {
                TextBoxRefImg.Text = d["img"].ToString();
                imgNm = d["img"].ToString();
            }

            if (d["ini_page"].ToString() == "True")
            {
                CheckIniPage.Checked = true;
            }
            else
            {
                CheckIniPage.Checked = false;
            }

            enqt_01_caption.Text = d["enqt_01_caption"].ToString();
            enqt_02_caption.Text = d["enqt_02_caption"].ToString();
            enqt_03_caption.Text = d["enqt_03_caption"].ToString();
            enqt_04_caption.Text = d["enqt_04_caption"].ToString();
            enqt_05_caption.Text = d["enqt_05_caption"].ToString();
            txtWin.Text = d["d6txtWin"].ToString();
            txtLos.Text = d["d6txtLos"].ToString();

            if (d["btn_up_link"].ToString() == "0")
            {
                drop_up_link.SelectedIndex = 0;
            }
            else
            {
                drop_up_link.SelectedValue = d["btn_up_link"].ToString();
            }

            if (d["btn_down_link"].ToString() == "0")
            {
                drop_down_link.SelectedIndex = 0;
            }
            else
            {
                drop_down_link.SelectedValue = d["btn_down_link"].ToString();
            }

            if (d["btn_left_link"].ToString() == "0")
            {
                drop_left_link.SelectedIndex = 0;
            }
            else
            {
                drop_left_link.SelectedValue = d["btn_left_link"].ToString();
            }
            if (d["btn_right_link"].ToString() == "0")
            {
                drop_right_link.SelectedIndex = 0;
            }
            else
            {
                drop_right_link.SelectedValue = d["btn_right_link"].ToString();
            }

            if (d["enqt_01_link"].ToString() == "0")
            {
                drop_enqt_01.SelectedIndex = 0;
            }
            else
            {
                drop_enqt_01.SelectedValue = d["enqt_01_link"].ToString();
            }
            if (d["enqt_02_link"].ToString() == "0")
            {
                drop_enqt_02.SelectedIndex = 0;
            }
            else
            {
                drop_enqt_02.SelectedValue = d["enqt_02_link"].ToString();
            }
            if (d["enqt_03_link"].ToString() == "0")
            {
                drop_enqt_03.SelectedIndex = 0;
            }
            else
            {
                drop_enqt_03.SelectedValue = d["enqt_03_link"].ToString();
            }
            if (d["enqt_04_link"].ToString() == "0")
            {
                drop_enqt_04.SelectedIndex = 0;
            }
            else
            {
                drop_enqt_04.SelectedValue = d["enqt_04_link"].ToString();
            }
            if (d["enqt_05_link"].ToString() == "0")
            {
                drop_enqt_05.SelectedIndex = 0;
            }
            else
            {
                drop_enqt_05.SelectedValue = d["enqt_05_link"].ToString();
            }

            if (File.Exists(Server.MapPath("~") + @"imgs\pages\Thumb\" + d["img"].ToString()))
            {
                imgCena.ImageUrl = @"imgs\pages\Thumb\" + d["img"].ToString();
                imgCena.Visible = true;
            }
            else
            {
                imgCena.Visible = false;
            }

            if (d["d6Win"].ToString() == "0")
            {
                drop_d6_win.SelectedIndex = 0;
                drop_d6_los.SelectedIndex = 0;
                txtDif.Text = "4";
            }
            else
            {
                drop_d6_win.SelectedValue = d["d6Win"].ToString();
                drop_d6_los.SelectedValue = d["d6Los"].ToString();
                txtDif.Text = d["d6dif"].ToString();
            }

            PanelEdit.Visible = true;
            Panel_Superior.Visible = true;
            lblNota.Visible = true;
            txtNota.Visible = true;
            txtTitulo.Visible = true;
            TextBoxTexto.Visible = true;
            txtPagina.Visible = true;

            enqt_01_caption.Visible = true;
            enqt_02_caption.Visible = true;
            enqt_03_caption.Visible = true;
            enqt_04_caption.Visible = true;
            enqt_05_caption.Visible = true;
            txtWin.Visible = true;
            txtLos.Visible = true;
            drop_up_link.Visible = true;
            drop_down_link.Visible = true;
            drop_left_link.Visible = true;
            drop_right_link.Visible = true;
            drop_enqt_01.Visible = true;
            drop_enqt_02.Visible = true;
            drop_enqt_03.Visible = true;
            drop_enqt_04.Visible = true;
            drop_enqt_05.Visible = true;

            btnSalvar.Visible = false;
            btnAlterar.Visible = true;
            BtnExcluir.Visible = true;
            btnVoltar.Visible = true;
            PanelGrid.Visible = false;
            lblNPages.Visible = false;

            lblMensagem.Visible = false;
        }

        protected void BtnExcluir_Click(object sender, EventArgs e)
        {
            var d = dados.DeletaRegistro("cfpagesbook", "id", Convert.ToInt32(GridView1.SelectedValue));

            lblMensagem.Text = gen.MensagensOldMan("Livro-Jogo<br> removido.");

            PopulaGrid();

            // Volta à lista
            PanelEdit.Visible = false;
            Panel_Superior.Visible = false;
            PanelGrid.Visible = true;
            lblNPages.Visible = true;

            //   btnNovo.Visible = true;
            btnSalvar.Visible = false;
            btnVoltar.Visible = false;
            btnAlterar.Visible = false;
            BtnExcluir.Visible = false;
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            int linkUp = 0,
                linkDown = 0,
                linkLeft = 0,
                linkRight = 0,

                linkEQ01 = 0,
                linkEQ02 = 0,
                linkEQ03 = 0,
                linkEQ04 = 0,
                linkEQ05 = 0,

                d6Win = 0,
                d6Los = 0;

            // Setas
            if (drop_up_link.SelectedIndex != 0)
            {
                linkUp = Convert.ToInt32(drop_up_link.SelectedValue);
            }
            if (drop_down_link.SelectedIndex != 0)
            {
                linkDown = Convert.ToInt32(drop_down_link.SelectedValue);
            }
            if (drop_left_link.SelectedIndex != 0)
            {
                linkLeft = Convert.ToInt32(drop_left_link.SelectedValue);
            }
            if (drop_right_link.SelectedIndex != 0)
            {
                linkRight = Convert.ToInt32(drop_right_link.SelectedValue);
            }

            // Questões
            if (drop_enqt_01.SelectedIndex != 0)
            {
                linkEQ01 = Convert.ToInt32(drop_enqt_01.SelectedValue);
            }
            if (drop_enqt_02.SelectedIndex != 0)
            {
                linkEQ02 = Convert.ToInt32(drop_enqt_02.SelectedValue);
            }
            if (drop_enqt_03.SelectedIndex != 0)
            {
                linkEQ03 = Convert.ToInt32(drop_enqt_03.SelectedValue);
            }
            if (drop_enqt_04.SelectedIndex != 0)
            {
                linkEQ04 = Convert.ToInt32(drop_enqt_04.SelectedValue);
            }
            if (drop_enqt_05.SelectedIndex != 0)
            {
                linkEQ05 = Convert.ToInt32(drop_enqt_05.SelectedValue);
            }

            // D6
            if (drop_d6_win.SelectedIndex != 0)
            {
                d6Win = Convert.ToInt32(drop_d6_win.SelectedValue);
            }
            if (drop_d6_los.SelectedIndex != 0)
            {
                d6Los = Convert.ToInt32(drop_d6_los.SelectedValue);
            }

            // Imagem
            ClasseImagens img = new ClasseImagens();

            var CurrentDirectory = HttpContext.Current.Server.MapPath("~");

            // Limpar diretório de arquivos cru 
            try
            {
                string directoryPath = CurrentDirectory + @"imgs\pages";
                string[] imageFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

                DirectoryInfo directory = new DirectoryInfo(directoryPath);

                var imageFiles = from file in directory.GetFiles()
                                 where imageFileExtensions.Contains(file.Extension.ToLowerInvariant())
                                 select file;

                foreach (var file in imageFiles)
                {
                    file.Delete();
                }
            }
            catch { }
            // Limpar diretório de arquivos cru - fim

            // Teste para ver se vai enviar img
            string imgNova = "";
            try
            {
                // Pegar tamanho da imagem em pixel (Alutra largura)
                System.Drawing.Image imgz = System.Drawing.Image.FromStream(fileCena.FileContent);
                int largura = imgz.Width;
                int altura = imgz.Height;

                imgNova = img.resizeImgPersonalizado(fileCena, this, @"imgs\pages", "P", largura, altura);
            }
            catch
            {
                if (DropDownListImg.Text != "-- Não informar --")
                {
                    imgNova = DropDownListImg.Text; // Mantem a que foi carregada na abertura
                }
                else
                {
                    if (TextBoxRefImg.Text != "")
                    {
                        imgNova = TextBoxRefImg.Text;
                    }
                    else
                    {
                        imgNova = "";
                    }
                }
            }

            // Não permite Referência de imagem vazia
            if (TextBoxRefImg.Text.Trim() == "")
            {
                TextBoxRefImg.Text = imgNm;
            }

            // Repositório de imagens
            try
            {
                if (fileCena.HasFile)
                { throw new Exception("Novo arquivo."); }

                dados.PopulaDrop(@"SELECT img, ref from imgRepo where img= '" + imgNm + "'", DropDownListTeste);

                // Repositório de imagens [UPDATE]                    
                var imgRepo_UP = dados.SqlUpdate("imgRepo", "img= '" + imgNm + "'", new Dictionary<string, string>()
                    {
                        { "img", imgNova },
                        { "ref", TextBoxRefImg.Text }
                    });
            }
            catch
            {
                // Repositório de imagens [INSERT]
                var imgRepo_IN = dados.SqlInsert("imgRepo", this, new Dictionary<string, string>()
                     {
                         { "img", imgNova },
                         { "ref", TextBoxRefImg.Text },
                         { "id_user", Session["id"].ToString() }
                     });
            }

            // Atualiza Capa do livro [EDIT]

            var d = dados.SqlUpdate("cfpagesbook", "id = " + GridView1.SelectedValue, new Dictionary<string, string>()
                {
             // {"id_book", Request.QueryString["book"] },
             // {"id_user", Convert.ToString(Session["id"]) },
                {"nota", gen.AntiSqlInjection(txtNota.Text) },
                {"titulo", gen.AntiSqlInjection(txtTitulo.Text) }, // 50
                {"texto",  gen.AntiSqlInjection(TextBoxTexto.Text) },
                {"pagina",  gen.AntiSqlInjection(txtPagina.Text) }, // 80 Referência da página

                {"enqt_01_caption", gen.AntiSqlInjection(enqt_01_caption.Text) },
                {"enqt_02_caption", gen.AntiSqlInjection(enqt_02_caption.Text) },
                {"enqt_03_caption", gen.AntiSqlInjection(enqt_03_caption.Text) },
                {"enqt_04_caption", gen.AntiSqlInjection(enqt_04_caption.Text) },
                {"enqt_05_caption", gen.AntiSqlInjection(enqt_05_caption.Text) },
                {"d6txtWin", gen.AntiSqlInjection(txtWin.Text) },
                {"d6txtLos", gen.AntiSqlInjection(txtLos.Text) },              

            // Pega 'cfpagesbook.id' da Página a parir do campo 'cfpagesbook.pagina'
                { "btn_up_link", Convert.ToString(linkUp) },
                {"btn_down_link",Convert.ToString(linkDown) },
                {"btn_left_link", Convert.ToString(linkLeft) },
                {"btn_right_link", Convert.ToString(linkRight) },

                {"enqt_01_link", Convert.ToString(linkEQ01) },
                {"enqt_02_link", Convert.ToString(linkEQ02) },
                {"enqt_03_link", Convert.ToString(linkEQ03) },
                {"enqt_04_link", Convert.ToString(linkEQ04) },
                {"enqt_05_link", Convert.ToString(linkEQ05) },

                {"d6Win", Convert.ToString(d6Win) },
                {"d6Los", Convert.ToString(d6Los) },
                {"d6dif", gen.AntiSqlInjection(txtDif.Text) },

                {"img", imgNm },
                {"ini_page", CheckIniPage.Checked.ToString() },

                {"atual", DateTime.Now.ToString() }
            });

            // Remove qualquer outra Página Inicial
            if (CheckIniPage.Checked == true)
            {
                var ini_page = dados.SqlUpdate("cfpagesbook", "id != " + GridView1.SelectedValue + " and id_book =" + HdfBook.Value, new Dictionary<string, string>()
                {
                    { "ini_page", "0" }
                });
            }

            // Lista de imagens
            dados.PopulaDropMult(@"Select img, ref From imgRepo WHERE id_user = " + Session["id"], DropDownListImg, true);

            // Mensagem de confirmação
            lblMensagem.Visible = true;
            lblMensagem.Text = gen.MensagensOldMan("Página atualizada<br> com sucesso.");

            if (d.ToLower().Contains("sucesso"))
            {
                // Ocultar
                PanelEdit.Visible = false;
                Panel_Superior.Visible = false;
                btnSalvar.Visible = false;
                btnAlterar.Visible = false;
                BtnExcluir.Visible = false;
                btnVoltar.Visible = false;

                PopulaGrid();
                PanelGrid.Visible = true;
                lblNPages.Visible = true;

                // Imagem
                imgCena.Visible = false;
                lblSize.Visible = false;
                fileCena.Visible = false;
                DropDownListImg.Visible = false;
                TextBoxRefImg.Visible = false;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int linkUp = 0,
                linkDown = 0,
                linkLeft = 0,
                linkRight = 0,

                linkEQ01 = 0,
                linkEQ02 = 0,
                linkEQ03 = 0,
                linkEQ04 = 0,
                linkEQ05 = 0,

                d6Win = 0,
                d6Los = 0;

            if (drop_up_link.SelectedIndex != 0)
            {
                linkUp = Convert.ToInt32(drop_up_link.SelectedValue);
            }
            if (drop_down_link.SelectedIndex != 0)
            {
                linkDown = Convert.ToInt32(drop_down_link.SelectedValue);
            }
            if (drop_left_link.SelectedIndex != 0)
            {
                linkLeft = Convert.ToInt32(drop_left_link.SelectedValue);
            }
            if (drop_right_link.SelectedIndex != 0)
            {
                linkRight = Convert.ToInt32(drop_right_link.SelectedValue);
            }
            //----
            if (drop_enqt_01.SelectedIndex != 0)
            {
                linkEQ01 = Convert.ToInt32(drop_enqt_01.SelectedValue);
            }
            if (drop_enqt_02.SelectedIndex != 0)
            {
                linkEQ02 = Convert.ToInt32(drop_enqt_02.SelectedValue);
            }
            if (drop_enqt_03.SelectedIndex != 0)
            {
                linkEQ03 = Convert.ToInt32(drop_enqt_03.SelectedValue);
            }
            if (drop_enqt_04.SelectedIndex != 0)
            {
                linkEQ04 = Convert.ToInt32(drop_enqt_04.SelectedValue);
            }
            if (drop_enqt_05.SelectedIndex != 0)
            {
                linkEQ05 = Convert.ToInt32(drop_enqt_05.SelectedValue);
            }

            // D6
            if (drop_d6_win.SelectedIndex != 0)
            {
                d6Win = Convert.ToInt32(drop_d6_win.SelectedValue);
            }
            if (drop_d6_los.SelectedIndex != 0)
            {
                d6Los = Convert.ToInt32(drop_d6_los.SelectedValue);
            }

            var CurrentDirectory = HttpContext.Current.Server.MapPath("~");

            // Imagem
            ClasseImagens img = new ClasseImagens();

            // Envio de imagem
            var imgNm = "";

            // Limpar diretório de arquivos cru 
            try
            {
                string directoryPath = CurrentDirectory + @"imgs\pages";
                string[] imageFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

                DirectoryInfo directory = new DirectoryInfo(directoryPath);

                var imageFiles = from file in directory.GetFiles()
                                 where imageFileExtensions.Contains(file.Extension.ToLowerInvariant())
                                 select file;

                foreach (var file in imageFiles)
                {
                    file.Delete();
                }
            }
            catch { }
            // Limpar diretório de arquivos cru - fim

            try
            {
                // Pegar tamanho da imagem em pixel (Alutra largura)
                System.Drawing.Image imgz = System.Drawing.Image.FromStream(fileCena.FileContent);
                int largura = imgz.Width;
                int altura = imgz.Height;

                imgNm = img.resizeImgPersonalizado(fileCena, this, @"imgs\pages", "P", largura, altura);
            }
            catch
            {
                if (DropDownListImg.Text != "-- Não informar --")
                {
                    imgNm = DropDownListImg.Text; // Mantem a que foi carregada na abertura
                }
                else
                {
                    if (TextBoxRefImg.Text != "")
                    {
                        imgNm = TextBoxRefImg.Text;
                    }
                    else
                    {
                        imgNm = "";
                    }
                }
            }

            // Não permite Referência de imagem vazia
            if (TextBoxRefImg.Text.Trim() == "")
            {
                TextBoxRefImg.Text = imgNm;
            }

            // Repositório de imagens
            try
            {
                if (fileCena.HasFile)
                { throw new Exception("Novo arquivo."); }

                dados.PopulaDrop(@"SELECT img, ref from imgRepo where img= '" + imgNm + "'", DropDownListTeste);

                // Repositório de imagens [UPDATE]                    
                var imgRepo_UP = dados.SqlUpdate("imgRepo", "img= '" + imgNm + "'", new Dictionary<string, string>()
                    {
                        { "img", imgNm },
                        { "ref", TextBoxRefImg.Text }
                    });
            }
            catch
            {
                // Repositório de imagens [INSERT]
                var imgRepo_IN = dados.SqlInsert("imgRepo", this, new Dictionary<string, string>()
                     {
                         { "img", imgNm },
                         { "ref", TextBoxRefImg.Text },
                         { "id_user", Session["id"].ToString() }
                     });
            }

            var d = dados.SqlInsert("cfpagesbook", this, new Dictionary<string, string>()
            {
                {"id_book", Request.QueryString["book"] },
                {"id_user", Convert.ToString(Session["id"]) },
                {"nota", gen.AntiSqlInjection(txtNota.Text) },
                {"titulo", gen.AntiSqlInjection(txtTitulo.Text) }, // 50
                {"texto",  gen.AntiSqlInjection(TextBoxTexto.Text) },
                {"pagina",  gen.AntiSqlInjection(txtPagina.Text) }, // 80 Referência da página

                {"enqt_01_caption", gen.AntiSqlInjection(enqt_01_caption.Text) },
                {"enqt_02_caption", gen.AntiSqlInjection(enqt_02_caption.Text) },
                {"enqt_03_caption", gen.AntiSqlInjection(enqt_03_caption.Text) },
                {"enqt_04_caption", gen.AntiSqlInjection(enqt_04_caption.Text) },
                {"enqt_05_caption", gen.AntiSqlInjection(enqt_05_caption.Text) },
                {"d6txtWin", gen.AntiSqlInjection(txtWin.Text) },
                {"d6txtLos", gen.AntiSqlInjection(txtLos.Text) },                 

            // Pega 'cfpagesbook.id' da Página a parir do campo 'cfpagesbook.pagina'             
                {"btn_up_link", Convert.ToString(linkUp) },
                {"btn_down_link", Convert.ToString(linkDown) },
                {"btn_left_link", Convert.ToString(linkLeft) },
                {"btn_right_link", Convert.ToString(linkRight) },

                {"enqt_01_link", Convert.ToString(linkEQ01) },
                {"enqt_02_link", Convert.ToString(linkEQ02) },
                {"enqt_03_link", Convert.ToString(linkEQ03) },
                {"enqt_04_link", Convert.ToString(linkEQ04) },
                {"enqt_05_link", Convert.ToString(linkEQ05) },

                {"d6Win", Convert.ToString(d6Win) },
                {"d6Los", Convert.ToString(d6Los) },
                {"d6dif", gen.AntiSqlInjection(txtDif.Text) },

                {"img", imgNm },
                {"ini_page", CheckIniPage.Checked.ToString() },
                {"atual", DateTime.Now.ToString() }
            });

            // Lista de imagens
            dados.PopulaDropMult(@"Select img, ref From imgRepo WHERE id_user = " + Session["id"], DropDownListImg, true);

            // Mensagem de confirmação
            lblMensagem.Visible = true;
            lblMensagem.Text = gen.MensagensOldMan("Página salva<br> com sucesso.");

            if (d.ToLower().Contains("sucesso"))
            {
                // Ocultar
                PanelEdit.Visible = false;
                Panel_Superior.Visible = false;
                btnSalvar.Visible = false;
                btnAlterar.Visible = false;
                BtnExcluir.Visible = false;
                btnVoltar.Visible = false;

                PopulaGrid();
                PanelGrid.Visible = true;
                lblNPages.Visible = true;

                // Imagem
                imgCena.Visible = false;
                lblSize.Visible = false;
                fileCena.Visible = false;
                DropDownListImg.Visible = false;
                TextBoxRefImg.Visible = false;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            PopulaGrid();
            GridView1.DataBind();
        }
    }
}
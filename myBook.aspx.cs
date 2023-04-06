using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;

namespace confabular
{
    public partial class myBook : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        ClasseGenerica gen = new ClasseGenerica();
        string book = null;

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
                if (Convert.ToInt32(Session["id"]) > 0)
                {
                    PopulaGrid();
                    dados.PopulaDrop("select * from cfgenbook", dropGen); // DropBox do genero literário  

                    imgCapa.ImageUrl = @"imgs\pages\Thumb\book_cover.png";
                    dados.PopulaDropRef(@"Select img, ref From imgRepo WHERE id_user = " + Session["id"], DropDownListImg);

                    book = Request.QueryString["book"]; // Id do livro                    
                    if (book != null)
                    {
                        GridView1_SelectedIndexChanged(this, e);
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
            imgCapa.Visible = true;
            if (DropDownListImg.Text != "-- Não informar --")
            {
                imgCapa.ImageUrl = @"imgs\pages\Thumb\" + DropDownListImg.Text;
                imgNm = DropDownListImg.Text;
            }
            else
            {
                imgCapa.ImageUrl = @"imgs\pages\Thumb\book_cover.png";
                imgNm = "";
            }
        }

        private void PopulaGrid()
        {
            try
            {
                dados.populaGrid(@"SELECT BOK.id, BOK.titulo, GEN.genero as GENE,  
                                   BOK.atual, BOK.id_user, BOK.img,
                                    CASE 
                                          WHEN BOK.STATUS = 0 THEN 'Rascunho' 
                                          WHEN BOK.STATUS = 1 THEN 'Publicado' 
                                          Else 'Bloqueado'
                                      end as Bstatus,
                                    COUNT(PAG.id) npges 
                                  From cfbook BOK
                                    left join cfgenbook GEN on BOK.genero = GEN.id 
                                    left join cfpagesbook PAG on BOK.id = PAG.id_book
                                  Where BOK.id_user =" + Convert.ToString(Session["id"]) +
                                  "and BOK.status <> 999" + // 999 = Desativado/ Oculto
                                  " GROUP BY BOK.id, BOK.titulo, GEN.genero, BOK.STATUS," +
                                  "BOK.finais, BOK.atual, BOK.id_user, BOK.img" +
                                  " Order by titulo", GridView1);

                if (GridView1.Rows.Count < 1)
                    lblMensagem.Text = gen.MensagensInfoGeral("Comece agora mesmo a construir sua aventura! : ) Click no botão Novo");

            }
            catch
            {
                lblMensagem.Text = gen.MensagensErro("Erro: Falha na consulta de Livros. #Er0001");
                // dados.populaGrid(@"SELECT * from cfbook where id_user =-1", GridView1);
                return;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = @"SELECT *, imgRepo.ref FROM cfbook 
                          Left join imgRepo on cfbook.img = imgRepo.img
                          WHERE cfbook.id =";

            var d = dados.PopulaComDataSetPersonalizado(sql + GridView1.SelectedValue).Tables[0].Rows[0];

            book = Request.QueryString["book"]; // Id do livro  
            if (book != null)
            {
                d = dados.PopulaComDataSetPersonalizado(sql + book).Tables[0].Rows[0];
            }

            txtTitulo.Text = d["titulo"].ToString();
            txtsubTitulo.Text = d["subtitulo"].ToString();
            txtSinopse.Text = d["resumo"].ToString();
            txtTags.Text = d["tags"].ToString();
            TextFinais.Text = d["finais"].ToString();
            dropGen.SelectedValue = d["genero"].ToString();
            dropStatus.Visible = true;
            CheckTermos.Visible = true;
            lblTermos.Visible = true;
            try
            {
                var s = dados.PopulaComDataSetPersonalizado(@"SELECT ini_page from cfpagesbook where id_book =" + d["id"].ToString()).Tables[0].Rows[0];
                if (s["ini_page"].ToString() != "")
                {
                    dropStatus.Enabled = true;
                    lblAvisoStatus.Visible = false;
                }
            }
            catch
            {
                lblAvisoStatus.Text = "(<i>É necessário definir uma <a href='ListPage.aspx?book=" + d["id"].ToString() + "'>Página Inicial</a> antes de Publicar</i>)";
                lblAvisoStatus.Visible = true;
                CheckTermos.Checked = false;
            }

            dropStatus.SelectedValue = d["status"].ToString();
            if (dropStatus.SelectedValue == "1") // Já publicado
            {
                CheckTermos.Checked = true;
            }
            else
            {
                CheckTermos.Checked = false;
            }

            TextBoxRefImg.Text = d["ref"].ToString();

            if (File.Exists(Server.MapPath("~") + @"imgs\pages\Thumb\" + d["img"].ToString()))
            {
                imgCapa.ImageUrl = @"imgs\pages\Thumb\" + d["img"].ToString();
            }
            else
            {
                imgCapa.ImageUrl = @"imgs\pages\Thumb\book_cover.png";
            }

            imgNm = d["img"].ToString();

            PanelEdit.Visible = true;
            PanelGrid.Visible = false;

            btnVoltar.Visible = true;
            btnNovo.Visible = false;
            btnSalvar.Visible = false;
            btnAlterar.Visible = true;
            BtnExcluir.Visible = true;
            fileCapa.Visible = true;
            lblcapa.Visible = true;
            DropDownListImg.Visible = true;
            TextBoxRefImg.Visible = true;
            lblMensagem.Visible = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(TextFinais.Text) < 1)
            {
                TextFinais.Text = "1";
            }

            // Publicar
            if ((dropStatus.SelectedValue == "1") && (!CheckTermos.Checked))
            {
                lblMensagem.Text = gen.MensagensErro("Para publicar você precisa confirmar que concorda com os Termos de uso.");
                return;
            }

            var d = dados.SqlInsert("cfbook", this, new Dictionary<string, string>()
            {
            { "titulo", txtTitulo.Text },
            { "subtitulo", txtsubTitulo.Text },
            { "resumo", txtSinopse.Text },
            { "tags", txtTags.Text },
            { "finais", TextFinais.Text },
            { "genero", dropGen.SelectedValue },
            { "status", "0"}, // Rascunho = 0; Publicado = 1; Bloqueado = 2   
            { "id_user", Session["id"].ToString()}, //pegar ID 
            { "atual", DateTime.Now.ToString() },
            { "img", imgNm }
        });

            lblMensagem.Text = d;

            if (d.ToLower().Contains("sucesso"))
            {
                txtTitulo.Text = "";
                txtsubTitulo.Text = "";
                txtSinopse.Text = "";
                txtTags.Text = "";
                TextFinais.Text = "1";
                dropGen.SelectedValue = "1";
                dropStatus.SelectedValue = "0";
                TextBoxRefImg.Text = "";

                PopulaGrid();
                PanelEdit.Visible = false;
                PanelGrid.Visible = true;

                btnNovo.Visible = true;
                btnSalvar.Visible = false;
                btnAlterar.Visible = false;
                BtnExcluir.Visible = false;
                fileCapa.Visible = false;
                lblcapa.Visible = false;
                DropDownListImg.Visible = false;
                TextBoxRefImg.Visible = false;
            }
        }
        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            // Publicar
            if ((dropStatus.SelectedValue == "1") && (!CheckTermos.Checked))
            {
                lblMensagem.Visible = true;
                lblMensagem.Text = gen.MensagensErro("Para publicar você precisa confirmar que concorda com os Termos de uso.");
                return;
            }

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
            try
            {
                // Pegar tamanho da imagem em pixel (Alutra largura)
                System.Drawing.Image imgz = System.Drawing.Image.FromStream(fileCapa.FileContent);
                int largura = imgz.Width;
                int altura = imgz.Height;

                imgNm = img.resizeImgPersonalizado(fileCapa, this, @"imgs\pages", "B", largura, altura);
            }
            catch
            {
                imgNm = DropDownListImg.Text; // Mantem a que foi carregada na abertura
            }

            // Não permite Referência de imagem vazia
            if (TextBoxRefImg.Text.Trim() == "")
            {
                TextBoxRefImg.Text = imgNm;
            }

            // Repositório de imagens
            try
            {
                if (fileCapa.HasFile)
                { throw new Exception("Novo arquivo."); }

                dados.PopulaDrop(@"SELECT img, ref from imgRepo where img= '" + imgNm + "'", DropDownListTeste);

                // Repositório de imagens [UPDATE]                    
                var imgRepo_UP = dados.SqlUpdate("imgRepo", "img= '" + imgNm + "'", new Dictionary<string, string>()
                    {
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

            // Atualiza Capa do livro [EDIT]
            //   if (imgNm != "")
            //  {
            var d = dados.SqlUpdate("cfbook", "id = " + GridView1.SelectedValue, new Dictionary<string, string>()
                {
                    { "titulo", txtTitulo.Text },
                    { "subtitulo", txtsubTitulo.Text },
                    { "resumo", txtSinopse.Text },
                    { "tags", txtTags.Text },
                    { "finais", TextFinais.Text },
                    { "genero", dropGen.SelectedValue },
                    { "status", dropStatus.SelectedValue}, // Rascunho = 0; Publicado = 1; Bloqueado = 2               
                    { "atual", DateTime.Now.ToString() },
                    { "img", imgNm }
                });
            // Mensagem de confirmação
            lblMensagem.Text = gen.MensagensOldMan("Dados salvos!");


            if (d.ToLower().Contains("sucesso"))
            {
                txtTitulo.Text = "";
                txtsubTitulo.Text = "";
                txtSinopse.Text = "";
                txtTags.Text = "";
                TextFinais.Text = "1";
                dropGen.SelectedValue = "1";
                dropStatus.SelectedValue = "0";

                PanelEdit.Visible = false;
                PanelGrid.Visible = true;

                btnNovo.Visible = true;
                btnSalvar.Visible = false;
                btnAlterar.Visible = false;
                BtnExcluir.Visible = false;
                fileCapa.Visible = false;
                lblcapa.Visible = false;
                DropDownListImg.Visible = false;
                TextBoxRefImg.Visible = false;

                PopulaGrid();
            }
            // }
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {   // Volta à lista
            PanelEdit.Visible = false;
            PanelGrid.Visible = true;

            btnNovo.Visible = true;
            btnSalvar.Visible = false;
            btnVoltar.Visible = false;
            btnAlterar.Visible = false;
            BtnExcluir.Visible = false;
            lblMensagem.Visible = false;
            CheckTermos.Visible = false;
            lblTermos.Visible = false;
        }
        protected void BtnExcluir_Click(object sender, EventArgs e)
        {
            var r = dados.SqlUpdate("cfbook", "id = " + GridView1.SelectedValue, new Dictionary<string, string>()
                {
                    { "status", "999" } // 999 = Desativado e fica invisível para o criador e leitores
                });

            // Mensagem de confirmação
            lblMensagem.Text = gen.MensagensOldMan("Livro-Jogo removido. :(");

            PopulaGrid();

            // Volta à lista
            PanelEdit.Visible = false;
            PanelGrid.Visible = true;

            btnNovo.Visible = true;
            btnSalvar.Visible = false;
            btnVoltar.Visible = false;
            btnAlterar.Visible = false;
            BtnExcluir.Visible = false;
            CheckTermos.Visible = false;
            lblTermos.Visible = false;
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {   // Abri formulário para criar um novo
            txtTitulo.Text = "";
            txtsubTitulo.Text = "";
            txtSinopse.Text = "";
            txtTags.Text = "";
            TextFinais.Text = "1";
            dropGen.SelectedValue = "1";
            dropStatus.Visible = false;
            dropStatus.SelectedIndex = 0;
            imgCapa.ImageUrl = @"imgs\pages\Thumb\book_cover.png";
            TextBoxRefImg.Text = "";

            PanelEdit.Visible = true;
            PanelGrid.Visible = false;
            btnVoltar.Visible = true;
            btnNovo.Visible = true;
            btnAlterar.Visible = false;
            BtnExcluir.Visible = false;
            btnSalvar.Visible = true;
            fileCapa.Visible = true;
            DropDownListImg.Visible = true;
            TextBoxRefImg.Visible = true;
            lblMensagem.Visible = false;
            CheckTermos.Visible = false;
            lblTermos.Visible = false;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            PopulaGrid();
            GridView1.DataBind();
        }
    }
}
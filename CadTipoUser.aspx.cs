using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace confabular
{
    public partial class CadTipoUser : System.Web.UI.Page
    {
        ClasseDados dados = new ClasseDados();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["tipo_user"]) == 2) // admin
                {
                    PopulaGrid();
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
            }
        }

        private void PopulaGrid()
        {
            dados.populaGrid("Select * from cftipuser order by tipo", GridView1);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var d = dados.PopulaComDataSetPersonalizado("SELECT * FROM cftipuser WHERE id = " + GridView1.SelectedValue).Tables[0].Rows[0];
            txtTipo.Text = d["tipo"].ToString();
            btnNovo.Visible = false;
            btnAlterar.Visible = true;
            BtnExcluir.Visible = true;
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            var d = dados.SqlUpdate("cftipuser", "id = " + GridView1.SelectedValue, new Dictionary<string, string>()
            {
                {"tipo", txtTipo.Text.ToUpper() },
            });

            lblMensagem.Text = d;

            if (d.ToLower().Contains("sucesso"))
            {
                txtTipo.Text = "";
                PopulaGrid();
                btnNovo.Visible = true;
                btnAlterar.Visible = false;
                BtnExcluir.Visible = false;
            }
        }

        protected void BtnExcluir_Click(object sender, EventArgs e)
        {
            var d = dados.DeletaRegistro("cftipuser", "id", Convert.ToInt32(GridView1.SelectedValue));
            
            lblMensagem.Text = d;

            if (d.ToLower().Contains("sucesso"))
            {
                txtTipo.Text = "";
                PopulaGrid();
                btnNovo.Visible = true;
                btnAlterar.Visible = false;
                BtnExcluir.Visible = false;
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            var d = dados.SqlInsert("cftipuser", this, new Dictionary<string, string>()
            {
                {"tipo", txtTipo.Text.ToUpper() }
            });

            lblMensagem.Text = d;

            if (d.ToLower().Contains("sucesso"))
            {
                txtTipo.Text = "";
                PopulaGrid();
            }
        }
    }
}
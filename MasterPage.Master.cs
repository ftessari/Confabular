using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace confabular
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // carrega somente uma vez
            {
              //  Imggamejolt.ImageUrl = @"imgs\gamejolt.png";
                imgBR.ImageUrl = @"imgs\brflag.png";

                if (Convert.ToInt32(Session["id"]) > 0)
                {
                    ClasseDados dados = new ClasseDados();
                    
                    Panel_login_top.Visible = false;
                    Panel_UserBtn.Visible = true;  // profile
                    Panel_MinhasCoisas.Visible = true;
                    Btn_User.Text = Convert.ToString(Session["usuario"]);

                    lblLoginImg.Text = "";
                    if (File.Exists(Server.MapPath("~") + @"imgs\avatar\Thumb\" + Session["avatar"].ToString()))
                    {
                        imgAvatar.ImageUrl = @"imgs\avatar\Thumb\" + Session["avatar"].ToString();
                    }
                    else
                    {
                        imgAvatar.ImageUrl = @"imgs\avatar\Thumb\avatar.png";
                    }
                }
                else 
                {
                    lblLoginImg.Text = "<i class='nav-icon fas fa-user-circle fa-2x'></i>"; 
                    Panel_login_top.Visible = true;
                    Panel_UserBtn.Visible = false;  // profile
                    Panel_MinhasCoisas.Visible = false;
                    Btn_User.Text = "";
                    return;
                }

                if (Convert.ToInt32(Session["tipo_user"]) == 2) // Admin tools
                {
                    Panel_Admin.Visible = true;
                }
                else 
                { 
                    Panel_Admin.Visible = false; 
                }
            }
        }
        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            Session["veja"] = txtPesq.Text;
            Response.Redirect("Index.aspx");
        }
        private void initSession()
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Index.aspx");
        }

        protected void btnDesconectar_Click(object sender, EventArgs e)
        {
            // Desconectar usuário
            Session["id"] = Convert.ToInt32(-1);
            Session["usuario"] = "";
            Session["tipo_user"] = Convert.ToInt32(0);
            Session["status"] = Convert.ToInt32(2);
            Session["avatar"] = "";

            Response.Redirect("Index.aspx");
        }
    }
}
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="confabular.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">
    <h1>  Login</h1>
    <div class="login-box">
        <!-- /.login-logo -->
        <asp:Label ID="lblMensagem" runat="server" Text="" Visible="false"></asp:Label>
        <div class="card">
            <div class="card-body login-card-body">
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtUser" placeholder="e-mail ou usuário" MaxLength="30" CssClass="form-control" runat="server" ValidationGroup="grupo1" EnableTheming="False"></asp:TextBox>    
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-envelope"></span>
                        </div>
                    </div>
                </div>
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtSenha" placeholder="Password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-lock"></span>
                        </div>
                    </div>
                </div>
                <div class="row">
                  <!--   <div class="col-8">
                        <div class="icheck-primary">
                            <input type="checkbox" id="remember">
                            <label for="remember">
                                Lembrar-se
                            </label>
                        </div>
                    </div> -->

                    <!-- /.col -->
                    <div class="col-md-4">
                        <asp:Button ID="btnLogar" Width="100%" runat="server" Style="margin: 3px;" OnClick="btnLogar_Click" CssClass="btn btn-primary" Text="Entrar" />

                    </div>
                    <div class="col-md-8">
                       <center> 
                           <p>                             
                                <asp:Label ID="lblReg" runat="server" Text=""></asp:Label>                                                       
                          </p>
                       </center>
                    </div>
                    <!-- /.col -->
                </div>

                <!--
      <div class="social-auth-links text-center mb-3">
        <p>- OR -</p>
        <a href="#" class="btn btn-block btn-primary">
          <i class="fab fa-facebook mr-2"></i> Sign in using Facebook
        </a>
        <a href="#" class="btn btn-block btn-danger">
          <i class="fab fa-google-plus mr-2"></i> Sign in using Google+
        </a>
      </div>
      <!-- /.social-auth-links -->

              <!--  <p class="mb-1">
                    <a href="forgot-password.html">Esqueci minha senha</a> 
                </p>-->
                
            </div>
            <!-- /.login-card-body -->
        </div>
    </div>
    <!-- /.login-box -->
   
</asp:Content>

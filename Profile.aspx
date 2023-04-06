<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="confabular.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
    <!-- Profile -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">
    <!-- timeline -->
    <div class="row">
        <div class="col-md-4">
            <div class="card text-white">
                <div class="card-title">
                    <div class="card-body">
                        <center>
                            <asp:Image ID="imgAvatar" class="img-fluid img-thumbnail" Style="border-radius: 100%; overflow: hidden;" runat="server" /><br>
                            <asp:Label ID="lblNome" runat="server" Text=""></asp:Label>
                            (<i>@<asp:Label ID="lblUser" runat="server" Text=""></asp:Label></i>)&nbsp;<asp:Image ID="imgSex" Width="16" runat="server" /><br>
                            <br>
                            <asp:Label ID="lblTipo_user" runat="server" Text=""></asp:Label>
                        </center>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-8">
            <div class="card text-white">
                <div class="card-title">
                    <div class="card-body">
                        <p style="margin-left: 7px">Sobre</p>
                        <asp:Label ID="mmBio" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>           
            <div class="card text-white">
                <div class="card-body">
                    <asp:Label ID="lblMail" runat="server" Text=""></asp:Label><br>
                    <asp:Label ID="lblCakeday" runat="server" Text=""></asp:Label><br>
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>

        <div class="col-12">
            <div class="card text-white">
                <div class="card-title">
                    <p style="margin-left: 7px">Meus Livros</p>
                    <div class="card-body">
                        <asp:Label ID="lblLivros" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12">
            <div class="card text-white">
                <div class="card-title">
                    <p style="margin-left: 7px">Meus Favoritos</p>
                    <div class="card-body">
                        <asp:Label ID="lblFavoritos" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



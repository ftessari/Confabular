<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Bookz.aspx.cs" Inherits="confabular.Bookz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">
    <div class="card text-white">
        <div class="card-title">
            <div class="card-body">
                <h3 style="margin-left: 7px">
                    <asp:Label ID="lblMensagem" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblSubTitulo" runat="server" Text=""></asp:Label>
                </h3>
                <i><b>
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                </b></i>
            </div>
        </div>
    </div>
    <!-- timeline -->
    <div class="row">
        <div class="col-md-4">
            <div class="card text-white">
                <div class="card-title">
                    <div class="card-body">
                        <center>                            
                            <asp:Image ID="imgCapa" class="card-img-top" runat="server" />
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-8">
            <div class="card text-white">
                <div class="card-title">
                    <p style="margin-left: 7px">Sobre</p>
                    <div style="margin-top: -20px" class="card-body">
                        Confabulista:
                        <asp:Label ID="lblAutor" runat="server" Text=""></asp:Label><br>
                        Gênero:
                        <asp:Label ID="lblGen" runat="server" Text=""></asp:Label><br>
                        nº Finais: [<asp:Label ID="lblFinais" runat="server" Text=""></asp:Label>]<br>
                        Tag´s:
                        <asp:Label ID="lblTags" runat="server" Text=""></asp:Label><br>
                        <asp:Button ID="btnJogar" Width="100%" runat="server" Style="margin: 3px;" OnClick="btnJogar_Click" CssClass="btn btn-info" Visible="True" Text="Jogar : )" />
                        <asp:Button ID="btnFav" Width="60px" runat="server" Style="margin: 3px;" OnClick="btnFav_Click" Visible="True" Text="" />

                        <asp:HiddenField ID="HdfBook" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12">
            <div class="card text-white">
                <div class="card-title">
                    <p style="margin-left: 7px">Sinopse</p>
                    <div style="margin-top: -20px" class="card-body">
                        <asp:Label ID="lblSinopse" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



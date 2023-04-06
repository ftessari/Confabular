<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Prediletos.aspx.cs" Inherits="confabular.Prediletos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
    <!-- Profile -->
    Prediletos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">   
        <div class="col-12">
            <div class="card text-white">
                <div class="card-title">                   
                    <div class="card-body">
                        <asp:Label ID="lblFavoritos" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>  
</asp:Content>



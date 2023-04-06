<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="confabular.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
    <!-- vazio -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">
    <asp:Panel ID="Panel_2" runat="server">
        <div class="row mb-2">
            <div class="col-md-6">
                <asp:Label ID="lblTop" runat="server" Text=""></asp:Label>                
            </div>
            <div class="col-md-6 ">
             <!--   <iframe width="424" height="238" src="https://www.youtube.com/embed/Pg85RXHLg04" title="Brusspup Channel Trailer" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture allowfullscreen"></iframe>
           --> </div>
        </div>
    </asp:Panel>
    
    <asp:Panel ID="PanelLivros" ScrollBars="Vertical" runat="server" Visible="true">      
       <!-- <div style="background-image:url('imgs\\d6_1.gif'); background-size: cover; width: 200px; height: 200px; position: relative;">
             <p style='position: absolute; top: 40%; left: 49%; transform: translate(-5%, -5%); color: black; font-size: 28px; font-weight: bold;'>6</p>

         </div> 
        <div style="background-image:url('imgs\\d20.gif'); background-size: cover; width: 148px; height: 110px; position: relative;">
             <p style='position: absolute; top: 40%; left: 49%; transform: translate(-5%, -5%); color: white; font-size: 28px; font-weight: bold;'>6</p>

         </div>-->
        
        <h3><asp:Label ID="lblTopTitle" runat="server" Text="Novos"></asp:Label></h3>
        <div class="container"> 
             <asp:Label ID="lblLivros" runat="server" Text=""></asp:Label>        
        </div>    

    </asp:Panel>
    <br />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Board.aspx.cs" Inherits="confabular.Board" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
    <asp:HiddenField ID="HdfBook" runat="server" />
    <!-- Profile -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">
    <!-- Mensagem de confirmação -->
    <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>

    <div class="row">
        <div class="col-12">
            <h1>
                <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
            </h1>
            <center>
                <asp:Label ID="lblImgInfo" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Image ID="imgPage" runat="server" ImageAlign="Middle" />
            </center>
            <br>
            <asp:TextBox ID="txtTexto" runat="server" ReadOnly="True" Width="100%" Height="200" Style="padding: 15px;" CssClass="accent-dark" TextMode="MultiLine" BorderStyle="Dotted" BorderColor="White" Font-Italic="False" ForeColor="#CCCCCC" BackColor="#666666" BorderWidth="3"></asp:TextBox>
        </div>
        <div class="col-12">
            <div class="card text-white">
                <div class="card-title">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div style="margin-left: 15px">
                                    <asp:Button ID="btnQuest1" runat="server" Style="margin-bottom: 5px" class="btn btn-outline-light" OnClick="btnClickQ1" /><br>
                                    <asp:Button ID="btnQuest2" runat="server" Style="margin-bottom: 5px" class="btn btn-outline-light" OnClick="btnClickQ2" /><br>
                                    <asp:Button ID="btnQuest3" runat="server" Style="margin-bottom: 5px" class="btn btn-outline-light" OnClick="btnClickQ3" /><br>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="row card">
                                    <center>
                                        <div class="col-12" style="margin-bottom: 5px; margin-top: 5px">
                                            <center>
                                                <asp:ImageButton class="btn btn-outline-light" ID="imgBtnUp" runat="server" ImageUrl="~/imgs/up.png" OnClick="btnClickUp" />
                                            </center>
                                        </div>
                                        <div class="row col-12" style="margin-bottom: 5px">
                                            <div class="col-4">
                                                <center>
                                                    <asp:ImageButton Style="margin-bottom: 5px;" class="btn btn-outline-light" ID="imgBtnLf" runat="server" ImageUrl="~/imgs/left.png" OnClick="btnClickLeft" />
                                                </center>
                                            </div>
                                            <div class="col-4">
                                                <center>                                                  
                                                     <asp:ImageButton class="btn btn-outline-light" ID="imgBtnD6" runat="server" ImageUrl="~/imgs/d6.png" OnClick="btnDadosD6_Click" Visible="false" />
                                                   </center>
                                            </div>
                                            <div class="col-4">
                                                <center>
                                                    <asp:ImageButton Style="margin-bottom: 5px;" class="btn btn-outline-light" ID="imgBtnRg" runat="server" ImageUrl="~/imgs/right.png" OnClick="btnClickRight" />
                                                </center>
                                            </div>
                                        </div>
                                        <div class="col-12" style="margin-bottom: 5px">
                                            <center>
                                                <asp:ImageButton Style="margin-bottom: 5px" class="btn btn-outline-light" ID="imgBtnDw" runat="server" ImageUrl="~/imgs/down.png" OnClick="btnClickDown" />
                                            </center>
                                        </div>
                                    </center>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnQuest4" runat="server" Style="margin-bottom: 5px" class="btn btn-outline-light" OnClick="btnClickQ4" /><br>
                                <asp:Button ID="btnQuest5" runat="server" class="btn btn-outline-light" OnClick="btnClickQ5" /><br>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



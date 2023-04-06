<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="confabular.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
    Registrar-se
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">

    <asp:Panel ID="PanelEdit" runat="server" Visible="false">
        <!-- Mensagem de confirmação-->
        <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>

        <div class="row">
            <div class="col-md-6">
                Usuário (acesso)
                <asp:TextBox ID="txtUser" MaxLength="30" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUser" runat="server" ErrorMessage="Usuário (acesso): Campo requerido" Display="None" ForeColor="Red" ValidationGroup="grupo1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                Email
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email incorreto" SetFocusOnError="True" ControlToValidate="txtEmail" ValidationGroup="grupo1" ForeColor="#FF9900" ValidationExpression="^\S+@\S+$" Display="None"></asp:RegularExpressionValidator>
                <p>
                    <asp:CheckBox ID="Checkmail_View" CssClass="form-check-label" runat="server" />
                    <label for="Checkmail_View">Deixar email visível a todos</label><br>
                    <asp:CheckBox ID="CheckCake_View" CssClass="form-check-label" runat="server" />
                    <label for="CheckCake_View">Cake Day :3 (Mostrar data de seu registro)</label>
                </p>
            </div>
            <div class="col-md-6">
                Nome Completo
                <asp:TextBox ID="txtNome" MaxLength="80" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNome" runat="server" ErrorMessage="Nome: Campo requerido" Display="None" ForeColor="Red" ValidationGroup="grupo1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                Gênero
                <asp:DropDownList ID="dropGenero" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Não informar</asp:ListItem>
                    <asp:ListItem Value="1">Masculino</asp:ListItem>
                    <asp:ListItem Value="2">Feminino</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                Bio
            <asp:TextBox ID="txtBio" Height="120px" CssClass="form-control" runat="server" ValidationGroup="grupo1" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="grupo1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </asp:Panel>
    <hr>
    <asp:Panel ID="PanelSenha" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-6">
                Senha
            <asp:TextBox ID="txtSenha" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                Confirme a Senha
            <asp:TextBox ID="txConftSenha" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Confira a senha..." ControlToCompare="txConftSenha" ControlToValidate="txtSenha" Display="None" ForeColor="#FF9900"></asp:CompareValidator>
            </div>
        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-md-2">
            <asp:Button ID="btnSalvar" runat="server" Width="100%" Style="margin: 3px;" CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvar_Click" Visible="False" ValidationGroup="grupo1" />
        </div>
    </div>
    <br>
</asp:Content>

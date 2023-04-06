<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Config.aspx.cs" Inherits="confabular.Config" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">
        Cadastro de Usuários
        <div class="row">
            <div class="col-3">
                <asp:Button ID="btnVoltar_LJ" Width="150px" runat="server" Style="margin: 3px;" OnClick="btnProfile_Click" CssClass="btn btn-primary" Visible="true" Text="Ver" />
            </div>
        </div>
    <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
    <!-- Mensagem de confirmação-->
    <div class="row mb-2">
        <div class="col-md-3">
            Usuário (acesso)
                <asp:TextBox Enabled="false" ID="txtUser" MaxLength="30" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUser" runat="server" ErrorMessage="Usuário (acesso): Campo requerido" Display="None" ForeColor="Red" ValidationGroup="grupo1" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-3">
            Nome Completo
                <asp:TextBox ID="txtNome" MaxLength="80" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNome" runat="server" ErrorMessage="Nome: Campo requerido" Display="None" ForeColor="Red" ValidationGroup="grupo1" SetFocusOnError="True"></asp:RequiredFieldValidator>

        </div>

        <div class="col-6">
            Avatar<br>
            <asp:Image ID="imgAvatar" runat="server" /><br>
            <asp:FileUpload ID="fileAvatar" runat="server" Visible="False" />
            <p style="font-size:12px">(Tamanho recomendado: 200px x 200px)</p>
            <asp:Button ID="btnAvatar" Width="150px" runat="server" Style="margin: 3px;" OnClick="btnAvatar_Click" CssClass="btn btn-primary" Text="Enviar" Visible="False" />
        </div>
    </div>

    <div class="row">
        <div class="col-6">
            Email
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email incorreto" SetFocusOnError="True" ControlToValidate="txtEmail" ValidationGroup="grupo1" ForeColor="#FF9900" ValidationExpression="^\S+@\S+$" Display="None"></asp:RegularExpressionValidator>
            <p>
                <asp:CheckBox ID="Checkmail_View" CssClass="form-check-label" runat="server" />
                <label for="Checkmail_View">Deixar email visível a todos</label>
            </p>

            Bio
            <asp:TextBox ID="txtBio" Height="120px" CssClass="form-control" runat="server" ValidationGroup="grupo1" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-6">
            Gênero
                <asp:DropDownList ID="dropGenero" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Não informar</asp:ListItem>
                    <asp:ListItem Value="1">Masculino</asp:ListItem>
                    <asp:ListItem Value="2">Feminino</asp:ListItem>
                </asp:DropDownList>

            Status
                <asp:DropDownList ID="dropStatus" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Desativado</asp:ListItem>
                    <asp:ListItem Value="1">Ativo</asp:ListItem>
                </asp:DropDownList>

            Outras Opções
        <p>
            <asp:CheckBox ID="CheckCake_View" CssClass="form-check-label" runat="server" />
            <label for="CheckCake_View">Cake Day :3 (Mostrar data de seu cadastro)</label>
        </p>
        </div>
    </div>

    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="grupo1" runat="server" ShowMessageBox="True" ShowSummary="False" />

    <div class="row">
        <div class="col-md-2">
            <asp:Button ID="btnAlterar" Width="150px" runat="server" Style="margin: 3px;" CssClass="btn btn-success" Text="Salvar" OnClick="btnAlterar_Click" Visible="True" ValidationGroup="grupo1" />
        </div>
    </div>
    <hr>
    <div class="row">
        <div class="col-6">
            Senha
            <asp:TextBox ID="txtSenha" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-6">
            Confirme a Senha
            <asp:TextBox ID="txConftSenha" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Confira a senha..." ControlToCompare="txConftSenha" ControlToValidate="txtSenha" ForeColor="#FF9900"></asp:CompareValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Button ID="btnSalvaSenha" Width="150px" runat="server" Style="margin: 3px;" OnClick="btnSalvaSenha_Click" CssClass="btn btn-primary" Text="Salvar" ValidationGroup="grupo1" />
        </div>
    </div>
    
</asp:Content>

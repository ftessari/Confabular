<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CadUser.aspx.cs" Inherits="confabular.CadUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
    Cadastro de Usuários
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">
    <div class="row">
        <div class="col-3">
            <asp:Button ID="btnVoltar" Width="100%" runat="server" Style="margin: 3px;" OnClick="btnVoltar_Click" CssClass="btn btn-primary" Visible="false" Text="Voltar" />
        </div>
        <div class="col-3">
        </div>
        <div class="col-3">
        </div>
        <div class="col-3">
            <asp:Button ID="btnNovo" Width="100%" runat="server" Style="margin: 3px;" OnClick="btnNovo_Click" CssClass="btn btn-primary" Visible="true" Text="Novo" />
        </div>
    </div>

    <asp:Panel ID="PanelEdit" ScrollBars="Horizontal" runat="server" Visible="false">
        <!-- Mensagem de confirmação-->
        <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>

        <div class="row">
            <div class="col-3">
                Usuário (acesso)
                <asp:TextBox ID="txtUser" MaxLength="30" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUser" runat="server" ErrorMessage="Usuário (acesso): Campo requerido" Display="None" ForeColor="Red" ValidationGroup="grupo1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                Email
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email incorreto" SetFocusOnError="True" ControlToValidate="txtEmail" ValidationGroup="grupo1" ForeColor="#FF9900" ValidationExpression="^\S+@\S+$" Display="None"></asp:RegularExpressionValidator>
                <p>
                    <asp:CheckBox ID="Checkmail_View" CssClass="form-check-label" runat="server" />
                    <label for="Checkmail_View">Deixar email visível a todos</label>
                </p>
            </div>
            <div class="col-3">
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
            <div class="col-6">
                Avatar<br>
                <asp:Image ID="imgAvatar" runat="server" /><br>
                <asp:FileUpload ID="fileAvatar" runat="server" Visible="False" />
                <h6><i>(200x200)</i></h6>
                <asp:Button ID="btnAvatar" Width="100%" runat="server" Style="margin: 3px;" OnClick="btnAvatar_Click" CssClass="btn btn-primary" Text="Enviar" Visible="False" />
            </div>
        </div>

        <div class="row">
            <div class="col-6">
                Tipo de Usuário
                <asp:DropDownList ID="dropTipoUser" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-6">
                Status
                <asp:DropDownList ID="dropStatus" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Desativado</asp:ListItem>
                    <asp:ListItem Value="1">Ativo</asp:ListItem>
                    <asp:ListItem Value="2">Bloqueado</asp:ListItem>
                    <asp:ListItem Value="3">Banido</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <br>
        <div class="row">
            <div class="col-6">
                <!-- -->
            </div>
            <div class="col-6">
                Outras Opções
        <p>
            <asp:CheckBox ID="CheckCake_View" CssClass="form-check-label" runat="server" />
            <label for="CheckCake_View">Cake Day :3 (Mostrar data de seu cadastro)</label>
        </p>
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

    <div class="row">
        <div class="col-md-2">
            <asp:Button ID="btnSalvar" runat="server" Width="100%" Style="margin: 3px;" CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvar_Click" Visible="False" ValidationGroup="grupo1" />
            <asp:Button ID="btnAlterar" runat="server" Width="100%" Style="margin: 3px;" CssClass="btn btn-success" Text="Alterar" OnClick="btnAlterar_Click" Visible="False" ValidationGroup="grupo1" />
            <asp:Button ID="BtnExcluir" runat="server" Width="100%" Style="margin: 3px;" CssClass="btn btn-danger" Text="Excluir" OnClick="BtnExcluir_Click" Visible="False" />
        </div>
    </div>
    <hr>
    <asp:Panel ID="PanelSenha" ScrollBars="Horizontal" runat="server" Visible="false">
        <div class="row">
            <div class="col-6">
                Senha
            <asp:TextBox ID="txtSenha" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-6">
                Confirme a Senha
            <asp:TextBox ID="txConftSenha" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Confira a senha..." ControlToCompare="txConftSenha" ControlToValidate="txtSenha" Display="None" ForeColor="#FF9900"></asp:CompareValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <%--<asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="Button1" ConfirmText="Confirma a Exclusão?"></asp:ConfirmButtonExtender>--%>
                <asp:Button ID="btnSalvaSenha" Width="100%" runat="server" Style="margin: 3px;" OnClick="btnSalvaSenha_Click" CssClass="btn btn-primary" Text="Salvar" ValidationGroup="grupo2" />
            </div>
        </div>
    </asp:Panel>
    <br>
    <asp:Panel ID="PanelGrid" ScrollBars="Horizontal" runat="server">
        <asp:Label ID="lblMensagemGrid" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridView1" runat="server" DataKeyNames="id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="100%" AutoGenerateColumns="False" CssClass="table table-dark">
            <Columns>
                <asp:TemplateField HeaderText="Usuários">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" Text='<%# Eval("Nome") %>' ForeColor="#CCCCCC"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Select" Text='<%# Eval("tipo") %>' ForeColor="#CCCCCC"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cake Day">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Select" Text='<%# Eval("cakeday") %>' ForeColor="#CCCCCC"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Select" Text='<%# Eval("USTATUS") %>' ForeColor="#CCCCCC"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="e-Mail">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Select" Text='<%# Eval("mail") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <br>
</asp:Content>

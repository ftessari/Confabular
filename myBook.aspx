<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="myBook.aspx.cs" Inherits="confabular.myBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
    Livros-Jogos
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

    <!-- Mensagem de confirmação-->
    <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>

    <asp:Panel ID="PanelEdit" ScrollBars="Horizontal" runat="server" Visible="false">
        <div class="row mb-2">
            <div class="col-6">
                Título
                <asp:TextBox ID="txtTitulo" MaxLength="40" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitulo" runat="server" ErrorMessage="Título: Campo requerido" Display="None" ForeColor="Red" ValidationGroup="grupo1" SetFocusOnError="True"></asp:RequiredFieldValidator>

                Sub-Título
                <asp:TextBox ID="txtsubTitulo" MaxLength="80" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>

                Tag´s
                <asp:TextBox ID="txtTags" MaxLength="500" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>

                Finais (Quantos finais tem?)
                <asp:TextBox ID="TextFinais" MaxLength="3" CssClass="form-control" runat="server" ValidationGroup="grupo1" TextMode="Number"></asp:TextBox>
            </div>

            <div class="col-6">
                <asp:Panel ID="Panel_Capa" runat="server">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblcapa" runat="server" Text="Label" Visible="False">Capa</asp:Label><br>
                            <asp:Image ID="imgCapa" runat="server" Height="220" Width="180" BorderColor="White" ImageAlign="Middle" /><br>
                        </div>
                        <div class="col-md-6">
                            Selecionar imagem
                            <asp:DropDownList ID="DropDownListImg" Width="180" CssClass="form-control" runat="server" OnSelectedIndexChanged="Img_Select" Visible="false" AutoPostBack="True"></asp:DropDownList>
                            <asp:DropDownList ID="DropDownListTeste" runat="server" Visible="false"></asp:DropDownList>
                            <br>
                            <asp:FileUpload ID="fileCapa" runat="server" Visible="False" />
                            <p style="font-size: 12px">(Tamanho recomendado: 250px x 200px)</p>
                            Referência da imagem
                            <asp:TextBox ID="TextBoxRefImg" MaxLength="80" Width="180" CssClass="form-control" runat="server" Visible="false" ValidationGroup="grupo1"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                Sinopse (Resumo)
                <asp:TextBox ID="txtSinopse" Height="120px" CssClass="form-control" runat="server" ValidationGroup="grupo1" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSinopse" runat="server" ErrorMessage="Sinopse: Campo requerido" Display="None" ForeColor="Red" ValidationGroup="grupo1" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                Gênero Literário
                <asp:DropDownList ID="dropGen" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                Status<br>
                <asp:Label ID="lblAvisoStatus" runat="server" ForeColor="#FF9966" Text=""></asp:Label>
                <asp:DropDownList ID="dropStatus" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Rascunho</asp:ListItem>
                    <asp:ListItem Value="1">Publicado</asp:ListItem>
                    <asp:ListItem Value="2">Bloqueado</asp:ListItem>
                </asp:DropDownList>           
                    <asp:CheckBox ID="CheckTermos" CssClass="form-check-label" runat="server" Visible="false"/> 
                    <asp:label id="lblTermos" for="CheckRegras" runat="server"  Visible="false">Eu li e concordo com os <a target="_blank" href="./termos.aspx">Termos de uso</a>.</asp:label><br>
            </div>
        </div>
        <hr>
        <!--<div class="row">
            <div class="col-12">
                <b>Classificação:</b> Este livro-jogo contém elementos (narrativos ou visuais)...
            </div>
            <div class="col-6">

                <asp:CheckBox ID="CheckViolencia" CssClass="form-check-label" runat="server" />
                <label for="CheckViolencia">com violência.</label><br>

                <asp:CheckBox ID="CheckSexo" CssClass="form-check-label" runat="server" />
                <label for="CheckSexo">com sexo ou nudez.</label><br>

                <asp:CheckBox ID="CheckDrogas" CssClass="form-check-label" runat="server" />
                <label for="CheckDrogas">com drogas (lícitas ou ilícitas).</label><br>
            </div>
            <div class="col-6">
                <p>
                </p>
            </div>
        </div> -->
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="grupo1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    <br>
    <div class="row">
        <div class="col-6">
            <asp:Button ID="btnSalvar" runat="server" Style="margin: 3px;" CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvar_Click" Visible="False" ValidationGroup="grupo1" />
            <asp:Button ID="btnAlterar" runat="server" Style="margin: 3px;" CssClass="btn btn-success" Text="Alterar" OnClick="btnAlterar_Click" Visible="False" ValidationGroup="grupo1" />
            <asp:Button ID="BtnExcluir" runat="server" CssClass="btn btn-danger" Text="Excluir" OnClick="BtnExcluir_Click" Visible="False" OnClientClick="return confirm('Tem certeza que deseja Excluir este Livro-Jogo?');" />
        </div>
    </div>
    <hr>

    <asp:Panel ID="PanelGrid" ScrollBars="Vertical" runat="server" Visible="true">
        <h3>Meus Livros-Jogos</h3>
        <asp:GridView ID="GridView1" runat="server" DataKeyNames="id" Width="100%" AutoGenerateColumns="False" CssClass="table table-dark" AllowPaging="True" PageSize="30" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PagerSettings-Position="Bottom">
            <Columns>
                <asp:TemplateField HeaderText="Título">
                    <ItemTemplate>
                        <b>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" Text='<%# Eval("titulo") + " ("+Eval("npges") +")" %>' ForeColor="#CCCCCC"></asp:LinkButton></b>
                        <br>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CssClass="btn btn-success" CommandName="Select" Text='Editar'></asp:LinkButton></b>
                        <asp:HyperLink ID="HyperLinkPlay" runat="server" CssClass="btn btn-info" NavigateUrl='<%# "Bookz.aspx?book=" + Eval("ID") %>'>Testar</asp:HyperLink>
                        <asp:HyperLink ID="HyperLinkCPage" runat="server" CssClass="btn btn-success" NavigateUrl='<%# "ListPage.aspx?book=" + Eval("ID") %>'>Criar Páginas</asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Gênero">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Select" Text='<%# Eval("GENE") %>' ForeColor="#CCCCCC"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Select" Text='<%# Eval("Bstatus") %>' ForeColor="#CCCCCC"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Atualização">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Select" Text='<%# Eval("atual") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>

            </Columns>
            <PagerSettings Position="TopAndBottom" />
        </asp:GridView>
    </asp:Panel>
    <br />
</asp:Content>

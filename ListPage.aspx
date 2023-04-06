<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListPage.aspx.cs" Inherits="confabular.Book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
    Lista de Páginas de
    <b>
        <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblSubTitulo" runat="server" Text=""></asp:Label>
    </b>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">

    <!-- Mensagem de confirmação -->
    <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>

    <div class="row">
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
            <asp:Button ID="btnVoltar_LJ" Width="150px" runat="server" Style="margin: 3px;" OnClick="btnVoltar_LJ_Click" CssClass="btn btn-primary" Visible="true" Text="Livros-Jogos" />

        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            <asp:Button ID="btnVoltar" Width="150px" runat="server" Style="margin: 3px;" OnClick="btnVoltar_Click" CssClass="btn btn-primary" Visible="false" Text="Voltar" />
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
            <asp:Button ID="btnNovaPage" Width="150px" runat="server" Style="margin: 3px;" OnClick="btnNovaPage_Click" CssClass="btn btn-primary" Visible="true" Text="Nova Página" />
        </div>
    </div>
    <asp:Panel ID="Panel_Superior" Visible="false" runat="server">
        Nota
        <asp:Label ID="lblNota" runat="server" Style="font-size: 12px" Text="(Não será visível ao leitor-jogador.)" Visible="false"></asp:Label>
        <asp:TextBox ID="txtNota" Height="60px" CssClass="form-control" runat="server" ValidationGroup="grupo1" TextMode="MultiLine"></asp:TextBox>
        <br>
        <div class="card">
            <div style="margin-left: 10px">
                <b>Imagem de Cabeçalho</b>
                <asp:Image ID="imgCena" runat="server" BorderColor="White" ImageAlign="Middle" Visible="false" /><br>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblSize" runat="server" Style="font-size: 12px" Text="(Tamanho recomendado: 1080px x 400px)" Visible="false"></asp:Label>
                        <asp:DropDownList ID="DropDownListImg" Width="180" CssClass="form-control" runat="server" OnSelectedIndexChanged="Img_Select" Visible="false" AutoPostBack="True"></asp:DropDownList>
                        <asp:DropDownList ID="DropDownListTeste" runat="server" Visible="false"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        Referência da imagem
                        <asp:TextBox ID="TextBoxRefImg" MaxLength="80" Width="180" CssClass="form-control" runat="server" ValidationGroup="grupo1" Visible="false"></asp:TextBox>
                        <asp:FileUpload ID="fileCena" runat="server" AllowMultiple="false" Visible="false" /><br>
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnDelImg" Width="150px" runat="server" Style="margin: 3px;" CssClass="btn btn-danger" Text="Excluir" OnClick="btnDelImg_Click" Visible="True" ValidationGroup="grupo1" OnClientClick="return confirm('Tem certeza que deseja Excluir esta imagem?');" />
                   
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <hr>
    <asp:Panel ID="PanelEdit" ScrollBars="Horizontal" runat="server" Visible="false">
        <div class="row mb-2">
            <div class="col-md-6">
                <p>
                    <asp:CheckBox ID="CheckIniPage" CssClass="form-check-label" runat="server" />
                    <label for="CheckIniPage">Definir Página como Inicial</label>
                </p>
            </div>
            <div class="col-md-12">
                <div class="row mb-2">
                    <div class="col-md-6">
                        Referência (nº Página) *
                        <asp:TextBox ID="txtPagina" MaxLength="80" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPagina" runat="server" ErrorMessage="Referência (nº Página): Campo requerido" Display="None" ForeColor="Red" ValidationGroup="grupo1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-6">
                        Titulo
                        <asp:TextBox ID="txtTitulo" MaxLength="80" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                    </div>
                </div>
                Texto
                    <asp:TextBox ID="TextBoxTexto" Height="120px" CssClass="form-control" runat="server" ValidationGroup="grupo1" TextMode="MultiLine"></asp:TextBox>

            </div>
        </div>
        <hr>
        <div class="row mb-2">
            <div class="col-md-6">
                <asp:ImageButton ID="igmBtnUp" runat="server" ImageUrl="~/imgs/up.png" />
                <asp:DropDownList ID="drop_up_link" CssClass="form-control" runat="server"></asp:DropDownList><br>
                <asp:ImageButton ID="igmBtndw" runat="server" ImageUrl="~/imgs/down.png" />
                <asp:DropDownList ID="drop_down_link" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:ImageButton ID="igmBtnLf" runat="server" ImageUrl="~/imgs/left.png" />
                <asp:DropDownList ID="drop_left_link" CssClass="form-control" runat="server"></asp:DropDownList><br>
                <asp:ImageButton ID="igmBtnRg" runat="server" ImageUrl="~/imgs/right.png" />
                <asp:DropDownList ID="drop_right_link" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        <hr>
        <div class="row mb-3" style="margin-left: 1px">
            <div class="col-md-4">
                Questão 01
                    <asp:TextBox ID="enqt_01_caption" MaxLength="50" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                <asp:DropDownList ID="drop_enqt_01" CssClass="form-control" runat="server"></asp:DropDownList>

                Questão 02
                    <asp:TextBox ID="enqt_02_caption" MaxLength="50" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                <asp:DropDownList ID="drop_enqt_02" CssClass="form-control" runat="server"></asp:DropDownList>

                Questão 03
                    <asp:TextBox ID="enqt_03_caption" MaxLength="50" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                <asp:DropDownList ID="drop_enqt_03" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class='card col-md-4'>
                <b>Utilizar Dado de 6 lados</b><br>
                <div class="row mb-2">
                    <div class="col-md-6">
                        <asp:ImageButton ID="imgD6" runat="server" ImageUrl="~/imgs/d6.png" Width="32" Style="margin-left: 10px" /><br>
                        <p style="margin-left: 10px">
                            Dificuldade:&nbsp;<asp:TextBox runat="server" ID="txtDif" Width="30"></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="rangDif" ControlToValidate="txtDif" Type="Integer" MinimumValue="2" MaximumValue="6" ErrorMessage="Por favor, insira um número entre 2 e 6."></asp:RangeValidator>
                        </p>
                    </div>
                    <div class="col-md-6">
                        Mensagem de Sucesso:<br>
                        <asp:TextBox ID="txtWin" MaxLength="50" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox><br>
                        <asp:DropDownList ID="drop_d6_win" CssClass="form-control" runat="server"></asp:DropDownList>
                        <br>
                        Mensagem de Falha:<br>
                        <asp:TextBox ID="txtLos" MaxLength="50" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox><br>
                        <asp:DropDownList ID="drop_d6_los" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                Questão 04
                    <asp:TextBox ID="enqt_04_caption" MaxLength="50" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                <asp:DropDownList ID="drop_enqt_04" CssClass="form-control" runat="server"></asp:DropDownList>

                Questão 05
                    <asp:TextBox ID="enqt_05_caption" MaxLength="50" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
                <asp:DropDownList ID="drop_enqt_05" CssClass="form-control" runat="server"></asp:DropDownList>
                <hr>
            </div>
        </div>
        <hr>
        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="grupo1" runat="server" ShowMessageBox="True" ShowSummary="False" />

    </asp:Panel>

    <div class="col-md-2">
        <asp:Button ID="btnSalvar" Width="100%" runat="server" Style="margin: 3px;" CssClass="btn btn-success" Text="Salvar" OnClick="btnSalvar_Click" Visible="False" ValidationGroup="grupo1" />
    </div>
    <div class="col-md-2">
        <asp:Button ID="btnAlterar" Width="100%" runat="server" Style="margin: 3px;" CssClass="btn btn-success" Text="Salvar" OnClick="btnAlterar_Click" Visible="False" ValidationGroup="grupo1" />
    </div>
    <div class="col-md-2">
        <asp:Button ID="BtnExcluir" Width="100%" runat="server" Style="margin: 3px;" CssClass="btn btn-danger" Text="Excluir" OnClick="BtnExcluir_Click" Visible="False" ValidationGroup="grupo1" OnClientClick="return confirm('Tem certeza que deseja Excluir esta página?');" />
    </div>
    <div class="col-md-6">
    </div>

    <asp:Panel ID="PanelGrid" ScrollBars="Vertical" runat="server" Visible="true">
        Nº de Páginas:
        <asp:Label ID="lblNPages" runat="server" Visible="false"></asp:Label>

        <asp:GridView ID="GridView1" runat="server" DataKeyNames="id" Width="100%" AutoGenerateColumns="False" CssClass="table table-dark" AllowPaging="True" PageSize="30" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Nº Página (Referência)">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" Text='<%# Eval("pagina") %>' ForeColor="#CCCCCC"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Título">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Select" Text='<%# Eval("titulo") %>' ForeColor="#CCCCCC"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="40%" HeaderText="Notas">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Select" Text='<%# Eval("nota") %>' ForeColor="#CCCCCC"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="8%" HeaderText="Página Inicial">
                    <ItemTemplate>
                        <center>
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Select" Text='<%# Eval("pageINI") %>' ForeColor="#CCCCCC" BorderStyle="None"></asp:LinkButton>
                        </center>
                    </ItemTemplate>
                    <ItemStyle ForeColor="#333333" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:HiddenField ID="HdfBook" runat="server" />
    </asp:Panel>
</asp:Content>

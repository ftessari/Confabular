<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CadGenero.aspx.cs" Inherits="confabular.CadGenero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Titulo" runat="server">
    Gênero Literário
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Corpo" runat="server">

    <asp:Label ID=lblMensagem runat="server" Text=""></asp:Label>  <!-- Mensagem de confirmação-->
    <div class="row mb-2">
        <div class="col-12">       
                <asp:TextBox ID="txtTipo" CssClass="form-control" runat="server" ValidationGroup="grupo1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTipo" runat="server" ErrorMessage="campo requerido" Display="Dynamic" ForeColor="#FF9900" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
        </div>
        <div class="col-12">
            <%--<asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="Button1" ConfirmText="Confirma a Exclusão?"></asp:ConfirmButtonExtender>--%>
            <asp:Button ID="btnNovo" Width="100%" runat="server" Style="margin: 3px;" OnClick="btnNovo_Click" CssClass="btn btn-primary" Text="Salvar" ValidationGroup="tipo" />
            <asp:Button ID="btnAlterar" runat="server" Style="margin: 3px;" CssClass="btn btn-success" Text="Alterar" OnClick="btnAlterar_Click" Visible="False" ValidationGroup="tipo" />
            <asp:Button ID="BtnExcluir" runat="server" Style="margin: 3px;" CssClass="btn btn-danger" Text="Excluir" OnClick="BtnExcluir_Click" Visible="False" />
        </div>
    </div>

    <asp:Panel ID="Panel1" ScrollBars="Horizontal" runat="server" ForeColor="Black">

        <asp:GridView ID="GridView1" runat="server" DataKeyNames="id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="100%" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="table table-dark" >
            <Columns>
                <asp:TemplateField HeaderText="Tipo">
                    <ItemTemplate>
                        <asp:LinkButton  ID="LinkButton1" runat="server" CommandName="Select" Text='<%# Eval("genero") %>' ForeColor="#CCCCCC" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>

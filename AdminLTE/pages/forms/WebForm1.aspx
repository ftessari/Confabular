<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Agro.AdminLTE.pages.forms.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <!-- Select2 -->
  <link rel="stylesheet" href="../../plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css">
  <!-- Bootstrap4 Duallistbox -->

  <!-- Theme style -->
  <link rel="stylesheet" href="../../dist/css/adminlte.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <input type="text" class="form-control" data-inputmask-alias="datetime" data-inputmask-inputformat="mm/dd/yyyy" data-mask>
          
        <asp:TextBox ID=TextBox1 runat="server"  data-inputmask-alias="datetime" data-inputmask-inputformat="mm/dd/yyyy" data-mask></asp:TextBox>
        </div>
    </form>
  <script src="../../plugins/jquery/jquery.min.js"></script>
<script src="../../plugins/select2/js/select2.full.min.js"></script>
<script src="../../plugins/inputmask/jquery.inputmask.min.js"></script>

<script>
  $(function () {
    $('[data-mask]').inputmask()
  })
  
  // DropzoneJS Demo Code End
</script>
</body>
</html>

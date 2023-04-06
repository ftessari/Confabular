using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
//using JqueryNotification;

/// <summary>
/// Summary description for Class1
/// </summary>
public class ClasseGenerica
{
    /// <summary>
    /// Classe responsavel por nomear a imgem para datatime atual
    /// </summary>
    /// <returns></returns>
    public string renomeiaImg()
    {
        return DateTime.Now.ToString().Replace(@"\", "").Replace(" ", "").Replace(":", "").Replace(@"/", "");
    }

    /// <summary>
    /// Método que calcula a percentual
    /// </summary>
    /// <param name="quantidadeTotal">Informe a quantidade total</param>
    /// <param name="quantidadeCalcular">Informe a quantidade para carcular o percentual</param>
    /// <returns></returns>
    public int CalculaPorcentagem(int quantidadeTotal, int quantidadeCalcular)
    {
        return quantidadeCalcular * 100 / quantidadeTotal;
    }

    public bool ValidaData(string data)
    {
        bool r = true;
        try
        {
            var d = Convert.ToDateTime(data);
        }
        catch (Exception)
        {
            r = false;
        }
        return r;
    }
    public DateTime UltimoDiaMes(int mes, int ano)
    {
        DateTime ultimoDiaDoMes = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
        return ultimoDiaDoMes;
    }

    public string retornaAnoMesDiasExtenso(string dtIniMorador1, string dtFimMorador1, string dtIniMorador2, string dtFimMorador2)
    {

        DateTime d1 = Convert.ToDateTime(dtIniMorador1);
        DateTime d2 = Convert.ToDateTime(dtFimMorador1);
        DateTime d3 = Convert.ToDateTime(dtIniMorador2);
        DateTime d4 = Convert.ToDateTime(dtFimMorador2);

        TimeSpan ts = d2.Subtract(d1);
        TimeSpan ts2 = d4.Subtract(d3);

        double totalDias = ts.TotalDays + ts2.TotalDays;

        //Calcula o ano-------------------
        double ano2 = Math.Floor(totalDias / 365.25);

        int ano = Convert.ToInt32(ano2);
        //--------------------------------

        //calcular Mes--------------------
        double mesesResto = Convert.ToDouble(totalDias % 365.25);


        double meses = mesesResto / 30;

        //int ddd = Convert.ToInt32(meses.ToString().Substring(0, meses.ToString().IndexOf(",")));

        int ddd = Convert.ToInt32(meses);


        //Calcula Dias--------------------
        double diasResto = Convert.ToDouble(totalDias % 365.25);

        int dias = Convert.ToInt32(Math.Floor(Math.Ceiling(mesesResto) % 30.41));

        if (ddd == 12 && dias > 25)
        {
            ddd = 11;
        }

        if (ddd == 12 && dias < 5)
        {
            ddd = 0;
        }

        string strAno = "";
        string strMes = "";
        string strDia = "";


        if (ano == 0)
        {
            strAno = "";
        }
        else if (ano == 1)
        {
            strAno = "1 ano";
        }
        else
        {
            strAno = ano + " anos";
        }

        if (ddd == 0)
        {
            strMes = "";
        }
        else if (ddd == 1)
        {
            strMes = " 1 mês";
        }
        else
        {
            strMes = " " + ddd + " meses";
        }

        if (dias == 0)
        {
            strDia = "";
        }
        else if (dias == 1)
        {
            strDia = " 1 dia";
        }
        else
        {
            strDia = " " + dias + " dias";
        }

        return strAno + strMes + strDia;
    }


    public string retornaMesExtenso(int NumeroMes)
    {
        var mes = "";
        switch (NumeroMes)
        {
            case 1:
                mes = "janeiro";
                break;
            case 2:
                mes = "fevereiro";
                break;
            case 3:
                mes = "março";
                break;
            case 4:
                mes = "abril";
                break;
            case 5:
                mes = "maio";
                break;
            case 6:
                mes = "junho";
                break;
            case 7:
                mes = "julho";
                break;
            case 8:
                mes = "agosto";
                break;
            case 9:
                mes = "setembro";
                break;
            case 10:
                mes = "outubro";
                break;
            case 11:
                mes = "novembro";
                break;
            case 12:
                mes = "dezembro";
                break;
        }

        return mes;
    }

    public string AntiSqlInjectionTelefone(TextBox txt)
    {
        return txt.Text.Replace("'", "").Replace(";", "");
    }
    public string AntiSqlInjection(string txt)
    {
        return txt.Replace("'", "").Replace("<div", "").Replace("<script", "").Replace(";", "");
    }

    /// <summary>
    /// Formata a Data para o formato brasleiro -> dd/mm/aaaa hh:mm:ss
    /// </summary>
    /// <param name="dataHora"></param>
    /// <returns></returns>
    public string FormataDataHoraPtbr(DateTime dataHora)
    {
        string dia = dataHora.Day.ToString();
        string mes = dataHora.Month.ToString();
        string ano = dataHora.Year.ToString();
        string hora = dataHora.Hour.ToString();
        string minuto = dataHora.Minute.ToString();
        string segundo = dataHora.Second.ToString();
        return string.Format(
            "{0}/{1}/{2} {3}:{4}:{5}",
            dataHora2Caracteres(dia),
            dataHora2Caracteres(mes),
            ano,
            dataHora2Caracteres(hora),
            dataHora2Caracteres(minuto),
            dataHora2Caracteres(segundo)
            );
    }

    /// <summary>
    /// Caso o textbox contenha o caractere ubderline ( _ ) ele retorna um valor vazio
    /// </summary>
    /// <param name="txt">Nome do textbox a ser verificado</param>
    /// <returns></returns>
    public string verificaCamposMaskComUnderline(TextBox txt)
    {
        string r = "";
        if (!txt.Text.Contains("_"))
        {
            r = txt.Text;
        }
        return r;
    }
    public string FormataDataSql(string Data)
    {
        DateTime d = Convert.ToDateTime(Data);
        string dia = d.Day.ToString();
        string mes = d.Month.ToString();
        string ano = d.Year.ToString();
        string d2 = string.Format(
        "{0}-{1}-{2}",
        ano,
        dataHora2Caracteres(mes),
        dataHora2Caracteres(dia)
        );
        return d2;
    }
    public string FormataDataHoraSql(string dataHora)
    {
        DateTime d = Convert.ToDateTime(dataHora);

        string dia = d.Day.ToString();
        string mes = d.Month.ToString();
        string ano = d.Year.ToString();
        string hora = d.Hour.ToString();
        string minuto = d.Minute.ToString();
        string segundo = d.Second.ToString();
        string d2 = string.Format(
            "{0}-{1}-{2} {3}:{4}:{5}",
            ano,
            dataHora2Caracteres(mes),
            dataHora2Caracteres(dia),
            dataHora2Caracteres(hora),
            dataHora2Caracteres(minuto),
            dataHora2Caracteres(segundo)
            );
        return d2;
    }
    private string dataHora2Caracteres(string dh)
    {
        string retorno = dh;
        if (dh.Length == 1)
        {
            retorno = string.Format("0{0}", dh);
        }

        return retorno;
    }

    public string formataMoedaSql(TextBox txt)
    {
        return txt.Text.Replace(".", "").Replace(",", ".");
    }

    public string formataMoedaSql(string txt)
    {
        return txt.Replace(".", "").Replace(",", ".");
    }

    /// <summary>
    /// Metodo para encriptar strings
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public string Criptografar(string texto)
    {
        string encryptoConnectionString = "";
        Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(texto);
        encryptoConnectionString = Convert.ToBase64String(b);
        return encryptoConnectionString;
    }

    public void insereDropSelecioneItem(DropDownList drop)
    {
        drop.Items.Insert(0, "-- Selecione um item --");
    }

    /// <summary>
    /// Método para descryptografar a string
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public string Descriptografar(string texto)
    {
        string descryptoConnectionString = "";
        Byte[] b = Convert.FromBase64String(texto);
        descryptoConnectionString = System.Text.Encoding.ASCII.GetString(b);
        return descryptoConnectionString;
    }


    string CONST_CHAVE = "MinhaChaveMestraCIJC";

    public string CripRijndael(string dados)
    {
        string chave = CONST_CHAVE;
        byte[] b = Encoding.UTF8.GetBytes(dados);
        byte[] pw = Encoding.UTF8.GetBytes(chave);

        RijndaelManaged rm = new RijndaelManaged();
        PasswordDeriveBytes pdb = new PasswordDeriveBytes(chave, new MD5CryptoServiceProvider().ComputeHash(pw));
        rm.Key = pdb.GetBytes(32);
        rm.IV = pdb.GetBytes(16);
        rm.BlockSize = 128;
        rm.Padding = PaddingMode.PKCS7;

        MemoryStream ms = new MemoryStream();

        CryptoStream cryptStream = new CryptoStream(ms, rm.CreateEncryptor(rm.Key, rm.IV), CryptoStreamMode.Write);
        cryptStream.Write(b, 0, b.Length);
        cryptStream.FlushFinalBlock();


        return System.Convert.ToBase64String(ms.ToArray());

    }

    public string DescripRijndael(string sDados)
    {
        string chave = CONST_CHAVE;
        sDados = sDados.Replace(" ", "+");
        byte[] dados = System.Convert.FromBase64String(sDados);
        byte[] pw = Encoding.UTF8.GetBytes(chave);

        RijndaelManaged rm = new RijndaelManaged();
        PasswordDeriveBytes pdb = new PasswordDeriveBytes(chave, new MD5CryptoServiceProvider().ComputeHash(pw));
        rm.Key = pdb.GetBytes(32);
        rm.IV = pdb.GetBytes(16);
        rm.BlockSize = 128;
        rm.Padding = PaddingMode.PKCS7;

        MemoryStream ms = new MemoryStream(dados, 0, dados.Length);

        CryptoStream cryptStream = new CryptoStream(ms, rm.CreateDecryptor(rm.Key, rm.IV), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(cryptStream);

        return sr.ReadToEnd();

    }

    /// <summary>
    /// Incrementa as casas decimais até o número indicado.
    /// </summary>
    /// <param name="texto">Valor a ser incrementado</param>
    /// <param name="qtdCasas">Quantidade de casas decimais</param>
    /// <returns>Valor alterado</returns>
    public string CasasDecimais(string texto, int qtdCasas)
    {
        var retorno = string.Empty;
        var deposDoPonto = string.Empty;

        var temPonto = false;
        foreach (var item in texto.Where(item => item.Equals(',')))
        {
            temPonto = true;
        }

        if (temPonto)
        {
            texto = texto.Replace(',', '.');
            deposDoPonto = texto.Substring(texto.IndexOf('.') + 1);

            //fixa o tamanho das casas decimais
            if (deposDoPonto.Length > qtdCasas)
            {
                deposDoPonto = deposDoPonto.Substring(0, qtdCasas);
            }
            else if (deposDoPonto.Length < qtdCasas)
            {
                while (deposDoPonto.Length < qtdCasas)
                {
                    deposDoPonto += "0";
                }
            }

            retorno += texto.Substring(0, texto.IndexOf('.'));
        }
        else
        {
            retorno = texto;
            for (var i = 0; i < qtdCasas; i++)
            {
                deposDoPonto += "0";
            }
        }

        return retorno + "." + deposDoPonto;
    }

    /// <summary>
    /// Obtem a quantidade de caracteres (tamanho) de uma string
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public string ContaCaracteresString(string texto)
    {
        string s = texto;
        int tam = s.Length;

        return tam.ToString();
    }

    /// <summary>
    /// Retornar a idade da pessoa com com base na data de nascimento
    /// </summary>
    /// <param name="DataNascimento">Informe aqui a data de nascimento o individuo</param>
    /// <returns></returns>
    public int CalculaIdade(DateTime DataNascimento)
    {
        int anos = DateTime.Now.Year - DataNascimento.Year;
        if (DateTime.Now.Month < DataNascimento.Month || (DateTime.Now.Month == DataNascimento.Month && DateTime.Now.Day < DataNascimento.Day))
            anos--;
        return anos;
    }

    /// <summary>
    /// Substitui todos os espaços em uma string pelo caractere de sublinhado(underline).
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public string SubstituiEspacosPorUnderline(string texto)
    {
        var frase = texto;

        // substitui os espaços por underline
        frase = frase.Replace(" ", "_");

        return frase;
    }

    /// <summary>
    /// Conta as palavras de uma string usando o espaço como separador
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public string ContaPalavrasDeString(string texto)
    {
        var frase = texto;

        // remove os espaços em excesso
        while (frase.IndexOf("  ") >= 0)
            frase = frase.Replace("  ", " ");

        // conta as palavras
        int cont = frase.Split(' ').Length;

        return cont.ToString();
    }

    /// <summary>
    /// Método para gerar número aleatórios de 10 dígitos.
    /// </summary>
    /// <returns></returns>
    private Hashtable random(int numeros)
    {
        var ht = new Hashtable();
        string rand;

        var rnd = new Random(DateTime.Now.Millisecond);
        int numero = 0;

        ht[0] = rand = rnd.Next(0, numeros).ToString();

        while (ht.Count < numeros)
        {
            if (!ht.ContainsValue(rand))
            {
                numero++;
                ht[numero] = rand;
            }
            else
            {
                rand = rnd.Next(0, numeros).ToString();
            }
        }
        return ht;

    }


    /// <summary>
    /// Método responsável por limpar todas as textbox da página, quando acionado.
    /// </summary>
    /// <param name="pai">basta passar como parametro "THIS" para pegar todas as textbox da página</param>
    public void LimpaFormulario(Control pai)
    {
        foreach (Control ctl in pai.Controls)
            if (ctl is TextBox)
                ((TextBox)ctl).Text = string.Empty;
            else if (ctl.Controls.Count > 0)
                LimpaFormulario(ctl);
    }

    public void AbilitaControles(Control pai)
    {
        foreach (Control ctl in pai.Controls)
        {
            if (ctl is TextBox)
            {
                ((TextBox)ctl).Enabled = true;
            }

            if (ctl is DropDownList)
            {
                ((DropDownList)ctl).Enabled = true;
            }

            if (ctl is RadioButtonList)
            {
                ((RadioButtonList)ctl).Enabled = true;
            }

            if (ctl is Button)
            {
                ((Button)ctl).Enabled = true;
            }
        }
    }

    public void DesabilitaControles(Control pai)
    {
        foreach (Control ctl in pai.Controls)
        {
            if (ctl is TextBox)
            {
                ((TextBox)ctl).Enabled = false;
            }

            if (ctl is DropDownList)
            {
                ((DropDownList)ctl).Enabled = false;
            }

            if (ctl is RadioButtonList)
            {
                ((RadioButtonList)ctl).Enabled = false;
            }

            if (ctl is Button)
            {
                ((Button)ctl).Enabled = false;
            }
        }
    }

    public bool ValidaCpf(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        cpf = cpf.Trim();
        cpf = cpf.Replace("_", "").Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        // Caso coloque todos os numeros iguais
        switch (cpf)
        {
            case "11111111111":
                return false;
            case "00000000000":
                return false;
            case "22222222222":
                return false;
            case "33333333333":
                return false;
            case "44444444444":
                return false;
            case "55555555555":
                return false;
            case "66666666666":
                return false;
            case "77777777777":
                return false;
            case "88888888888":
                return false;
            case "99999999999":
                return false;
        }

        tempCpf = cpf.Substring(0, 9);
        soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCpf = tempCpf + digito;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto;

        return cpf.EndsWith(digito);
    }

    public bool ValidaCnpj(string cnpj)
    {
        string CNPJ = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

        if (CNPJ == "00000000000000")
        {
            return false;
        }

        int[] resultado = new int[2] { 0, 0 };
        bool[] CNPJOk = new bool[2] { false, false };
        int[] soma = new int[2] { 0, 0 };
        int[] digitos = new int[14];

        int nrDig;
        string ftmt = "6543298765432";

        try
        {
            for (nrDig = 0; nrDig < 14; nrDig++)
            {
                digitos[nrDig] = int.Parse(
                    CNPJ.Substring(nrDig, 1));
                if (nrDig <= 11)
                    soma[0] += (digitos[nrDig] *
                      int.Parse(ftmt.Substring(
                      nrDig + 1, 1)));
                if (nrDig <= 12)
                    soma[1] += (digitos[nrDig] *
                      int.Parse(ftmt.Substring(
                      nrDig, 1)));
            }

            for (nrDig = 0; nrDig < 2; nrDig++)
            {
                resultado[nrDig] = (soma[nrDig] % 11);
                if ((resultado[nrDig] == 0) || (
                     resultado[nrDig] == 1))
                    CNPJOk[nrDig] = (
                    digitos[12 + nrDig] == 0);
                else
                    CNPJOk[nrDig] = (
                    digitos[12 + nrDig] == (
                    11 - resultado[nrDig]));
            }
            return (CNPJOk[0] && CNPJOk[1]);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Retorna a formatação para as LblMensagens, como, cadastro com sucesso, Editado com sucesso etc... 
    /// </summary>
    /// <param name="tipo">
    /// 1 - Cadastrado com sucessso
    /// 2 - Editado com sucesso
    /// 3 - Excluido com sucesso
    /// </param>
    /// <returns></returns>
    public string MensagensInformativas(int tipo)
    {
        var retorno = "";
        switch (tipo)
        {
            case 1://Cadastrado com sucessso
                retorno += @"<div style='border: 1px solid #B8DAFF; text-align:center; background-color: #CCE5FF; padding:15px; font-family:Verdana; 
                            font-size:12px; color: #004085; font-weight:bold; -moz-border-radius:7px;-webkit-border-radius:7px;border-radius:7px;'>
                            Registro cadastrado com sucesso!</div>";
                break;
            case 2://Editado com sucesso
                retorno += @"<div style='border: 1px solid #C3E6CB; text-align:center; background-color: #D4EDDA; padding:15px; font-family:Verdana;
                            font-size:12px; color: #155724; font-weight:bold; -moz-border-radius:7px;-webkit-border-radius:7px;
                            border-radius:7px;'>Registro alterado com sucesso!</div>";
                break;
            case 3://Excluido com sucesso
                retorno += "<div style='border: 1px solid #F5C6CB; text-align:center; background-color: #F8D7DA; padding:15px; font-family:Verdana;" +
                            "font-size:12px; color: #721C24; font-weight:bold; -moz-border-radius:7px;-webkit-border-radius:7px;border-radius:7px;'>" +
                            "Registro excluído com sucesso!</div>";
                break;
        }

        return retorno;
    }


    private string caminhoImagem(Page p)
    {
        string g = p.Request.Url.AbsoluteUri;
        if (g.Contains("localhost"))
        {
            g = g.Remove(g.LastIndexOf(Convert.ToChar("/"))) + "/adm/img/";
        }
        else
        {
            g = g.Replace(p.Request.Path, "") + "/adm/img/";
        }
        return g;
    }

    public string MensagensErro(string erro)
    {
        return string.Format(string.Format("<div style='border: 1px solid #F5C6CB; text-align:center; background-color: #F8D7DA; padding:15px; font-family:Verdana;" +
                            "font-size:12px; color: #721C24; font-weight:bold; -moz-border-radius:7px;-webkit-border-radius:7px;border-radius:7px;'>" +
                            "{0}</div>", erro));
    }

    public string MensagensOldMan(string msg)
    {
        return string.Format(string.Format("<div style=\"background-image:url("+ @"'imgs\\OldManInfo.gif'" + "); background-size: cover; width: 150px; height: 150px; position: relative; \"><p style='position: absolute; top: 6%; left: 13%; transform: translate(-5%, -5%); color: black; font-size: 14px; font-weight: bold;'>" + msg + "</p></div>"));
    }

    public string MensagensInfoGeral(string textoPersonalizado)
    {
        return "<div style='border: 1px solid #3d85c6; text-align:center; background-color: #9fc5e8; padding:15px; font-family:Verdana;" +
                            "font-size:12px; color: #0b5394; font-weight:bold; -moz-border-radius:7px;-webkit-border-radius:7px;border-radius:7px;'>" +
                            textoPersonalizado + "</div>";
        //return string.Format("<script type=\"text/javascript\">alert('" + textoPersonalizado + "')</script>");
    }
    /// <summary>
    /// Mensgaens ccom cores personalidadas
    /// </summary>
    /// <param name="textoPersonalizado">Seu texto</param>
    /// <param name="tipo">1 azul, 2 verde, 3 vermelho</param>
    /// <returns></returns>
    public string MsgInfoPersonal(string textoPersonalizado, int tipo)
    {
        string cor = "";
        string corBorda = "";
        string corFonte = "";
        if (tipo == 1)
        {
            cor = "#CCE5FF";
            corBorda = "#B8DAFF";
            corFonte = "#004085";
        }
        else if (tipo == 2)
        {
            cor = "#D4EDDA";
            corBorda = "#C3E6CB";
            corFonte = "#155724";
        }
        else
        {
            cor = "#F8D7DA";
            corBorda = "#F5C6CB";
            corFonte = "#721C24";
        }
        return string.Format(string.Format("<div style='border: 1px solid " + corBorda
                    + "; color:" + corFonte + ";background-color: " + cor
                    + "; padding: 10px; text-align:center' role='alert'>{0}</div>", textoPersonalizado));
    }

    public string Extenso_Metragem(decimal metragem)
    {
        var r = Extenso_Valor(metragem);
        r = r.Replace("Reais", "").Replace("Centavos", "Metros Quadrados").Replace("Centavo", "Metros Quadrados").Replace("Um Real", "Um");
        if (!r.Contains("Metros Quadrados"))
        {
            r = r + " Metros Quadrados";
        }
        if (r == "Um Metros Quadrados")
        {
            r = "Um Metro Quadrado";
        }
        return r;
    }

    public string Extenso_Valor(decimal pdbl_Valor)
    {
        string strValorExtenso = ""; //Variável que irá armazenar o valor por extenso do número informado
        string strNumero = "";       //Irá armazenar o número para exibir por extenso 
        string strCentena = "";
        string strDezena = "";
        string strDezCentavo = "";

        decimal dblCentavos = 0;
        decimal dblValorInteiro = 0;
        int intContador = 0;
        bool bln_Bilhao = false;
        bool bln_Milhao = false;
        bool bln_Mil = false;
        bool bln_Unidade = false;

        //Verificar se foi informado um dado indevido 
        if (pdbl_Valor == 0 || pdbl_Valor <= 0)
        {
            //throw new Exception("Valor não suportado pela Função. Verificar se há valor negativo ou nada foi informado");
        }
        if (pdbl_Valor > (decimal)9999999999.99)
        {
            throw new Exception("Valor não suportado pela Função. Verificar se o Valor está acima de 9999999999.99");
        }
        else //Entrada padrão do método
        {
            //Gerar Extenso Centavos 
            pdbl_Valor = (Decimal.Round(pdbl_Valor, 2));
            dblCentavos = pdbl_Valor - (Int64)pdbl_Valor;

            //Gerar Extenso parte Inteira
            dblValorInteiro = (Int64)pdbl_Valor;
            if (dblValorInteiro > 0)
            {
                if (dblValorInteiro > 999)
                {
                    bln_Mil = true;
                }
                if (dblValorInteiro > 999999)
                {
                    bln_Milhao = true;
                    bln_Mil = false;
                }
                if (dblValorInteiro > 999999999)
                {
                    bln_Mil = false;
                    bln_Milhao = false;
                    bln_Bilhao = true;
                }

                for (int i = (dblValorInteiro.ToString().Trim().Length) - 1; i >= 0; i--)
                {
                    // strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) + 1, 1);
                    strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 1);
                    switch (i)
                    {            /*******/
                        case 9:  /*Bilhão*
                                 /*******/
                            {
                                strValorExtenso = fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " Bilhões e" : " Bilhão e");
                                bln_Bilhao = true;
                                break;
                            }
                        case 8: /********/
                        case 5: //Centena*
                        case 2: /********/
                            {
                                if (int.Parse(strNumero) > 0)
                                {
                                    strCentena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 3);

                                    if (int.Parse(strCentena) > 100 && int.Parse(strCentena) < 200)
                                    {
                                        strValorExtenso = strValorExtenso + " Cento e ";
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + " " + fcn_Numero_Centena(strNumero);
                                    }
                                    if (intContador == 8)
                                    {
                                        bln_Milhao = true;
                                    }
                                    else if (intContador == 5)
                                    {
                                        bln_Mil = true;
                                    }
                                }
                                break;
                            }
                        case 7: /*****************/
                        case 4: //Dezena de Milhão*
                        case 1: /*****************/
                            {
                                if (int.Parse(strNumero) > 0)
                                {
                                    strDezena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 2);//

                                    if (int.Parse(strDezena) > 10 && int.Parse(strDezena) < 20)
                                    {
                                        strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ")
                                        + fcn_Numero_Dezena0(Right(strDezena, 1));//corrigido

                                        bln_Unidade = true;
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ")
                                        + fcn_Numero_Dezena1(Left(strDezena, 1));//corrigido 

                                        bln_Unidade = false;
                                    }
                                    if (intContador == 7)
                                    {
                                        bln_Milhao = true;
                                    }
                                    else if (intContador == 4)
                                    {
                                        bln_Mil = true;
                                    }
                                }
                                break;
                            }
                        case 6: /******************/
                        case 3: //Unidade de Milhão* 
                        case 0: /******************/
                            {
                                if (int.Parse(strNumero) > 0 && !bln_Unidade)
                                {
                                    if ((Right(strValorExtenso, 5).Trim()) == "entos"
                                    || (Right(strValorExtenso, 3).Trim()) == "nte"
                                    || (Right(strValorExtenso, 3).Trim()) == "nta")
                                    {
                                        strValorExtenso = strValorExtenso + " e ";
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + " ";
                                    }
                                    strValorExtenso = strValorExtenso + fcn_Numero_Unidade(strNumero);
                                }
                                if (i == 6)
                                {
                                    if (bln_Milhao || int.Parse(strNumero) > 0)
                                    {
                                        strValorExtenso = strValorExtenso + ((int.Parse(strNumero) == 1) && !bln_Unidade ? " Milhão" : " Milhões");
                                        strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 1000000) ? " " : " e");
                                        bln_Milhao = true;
                                    }
                                }
                                if (i == 3)
                                {
                                    if (bln_Mil || int.Parse(strNumero) > 0)
                                    {
                                        strValorExtenso = strValorExtenso + " Mil";
                                        strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 1000) ? " " : " e");
                                        bln_Mil = true;
                                    }
                                }
                                if (i == 0)
                                {
                                    if ((bln_Bilhao && !bln_Milhao && !bln_Mil
                                    && Right((dblValorInteiro.ToString().Trim()), 3) == "0")
                                    || (!bln_Bilhao && bln_Milhao && !bln_Mil
                                    && Right((dblValorInteiro.ToString().Trim()), 3) == "0"))
                                    {
                                        strValorExtenso = strValorExtenso + " e ";
                                    }
                                    strValorExtenso = strValorExtenso + ((Int64.Parse(dblValorInteiro.ToString())) > 1 ? " Reais" : " Real");
                                }
                                bln_Unidade = false;
                                break;
                            }
                    }
                }//
            }
            if (dblCentavos > 0)
            {

                if (dblCentavos > 0 && dblCentavos < 0.1M)
                {
                    strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                    strValorExtenso = strValorExtenso + ((dblCentavos > 0) ? " e " : " ")
                    + fcn_Numero_Unidade(strNumero) + ((dblCentavos > 0.01M) ? " Centavos" : " Centavo");
                }
                else if (dblCentavos > 0.1M && dblCentavos < 0.2M)
                {
                    strNumero = Right(((Decimal.Round(dblCentavos, 2) - (decimal)0.1).ToString().Trim()), 1);
                    strValorExtenso = strValorExtenso + ((dblCentavos > 0) ? " " : " e ")
                    + fcn_Numero_Dezena0(strNumero) + " Centavos ";
                }
                else
                {
                    strNumero = Right(dblCentavos.ToString().Trim(), 2);
                    strDezCentavo = Mid(dblCentavos.ToString().Trim(), 2, 1);

                    if (strNumero.Contains(","))
                    {
                        strNumero = strNumero.Replace(",", "") + "0";
                    }

                    strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 0) ? " e " : " ");
                    strValorExtenso = strValorExtenso + fcn_Numero_Dezena1(Left(strDezCentavo, 1));

                    if ((dblCentavos.ToString().Trim().Length) > 2)
                    {
                        strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                        if (int.Parse(strNumero) > 0)
                        {
                            if (dblValorInteiro <= 0)
                            {
                                if (Mid(strValorExtenso.Trim(), strValorExtenso.Trim().Length - 2, 1) == "e")
                                {
                                    strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                }
                                else
                                {
                                    strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                }
                            }
                            else
                            {
                                strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                            }
                        }
                    }
                    strValorExtenso = strValorExtenso + " Centavos ";
                }
            }
            if (dblValorInteiro < 1) strValorExtenso = Mid(strValorExtenso.Trim(), 2, strValorExtenso.Trim().Length - 2);
        }

        return strValorExtenso.Trim();
    }

    private string fcn_Numero_Dezena0(string pstrDezena0)
    {
        //Vetor que irá conter o número por extenso 
        ArrayList array_Dezena0 = new ArrayList();

        array_Dezena0.Add("Onze");
        array_Dezena0.Add("Doze");
        array_Dezena0.Add("Treze");
        array_Dezena0.Add("Quatorze");
        array_Dezena0.Add("Quinze");
        array_Dezena0.Add("Dezesseis");
        array_Dezena0.Add("Dezessete");
        array_Dezena0.Add("Dezoito");
        array_Dezena0.Add("Dezenove");

        return array_Dezena0[((int.Parse(pstrDezena0)) - 1)].ToString();
    }
    private string fcn_Numero_Dezena1(string pstrDezena1)
    {
        //Vetor que irá conter o número por extenso
        ArrayList array_Dezena1 = new ArrayList();

        array_Dezena1.Add("Dez");
        array_Dezena1.Add("Vinte");
        array_Dezena1.Add("Trinta");
        array_Dezena1.Add("Quarenta");
        array_Dezena1.Add("Cinquenta");
        array_Dezena1.Add("Sessenta");
        array_Dezena1.Add("Setenta");
        array_Dezena1.Add("Oitenta");
        array_Dezena1.Add("Noventa");

        return array_Dezena1[Int16.Parse(pstrDezena1) - 1].ToString();
    }

    private string fcn_Numero_Centena(string pstrCentena)
    {
        //Vetor que irá conter o número por extenso
        ArrayList array_Centena = new ArrayList();

        array_Centena.Add("Cem");
        array_Centena.Add("Duzentos");
        array_Centena.Add("Trezentos");
        array_Centena.Add("Quatrocentos");
        array_Centena.Add("Quinhentos");
        array_Centena.Add("Seiscentos");
        array_Centena.Add("Setecentos");
        array_Centena.Add("Oitocentos");
        array_Centena.Add("Novecentos");

        return array_Centena[((int.Parse(pstrCentena)) - 1)].ToString();
    }
    private string fcn_Numero_Unidade(string pstrUnidade)
    {
        //Vetor que irá conter o número por extenso
        ArrayList array_Unidade = new ArrayList();

        array_Unidade.Add("Um");
        array_Unidade.Add("Dois");
        array_Unidade.Add("Três");
        array_Unidade.Add("Quatro");
        array_Unidade.Add("Cinco");
        array_Unidade.Add("Seis");
        array_Unidade.Add("Sete");
        array_Unidade.Add("Oito");
        array_Unidade.Add("Nove");

        return array_Unidade[(int.Parse(pstrUnidade) - 1)].ToString();
    }

    //Começa aqui os Métodos de Compatibilazação com VB 6 .........Left() Right() Mid()
    public static string Left(string param, int length)
    {
        //we start at 0 since we want to get the characters starting from the 
        //left and with the specified lenght and assign it to a variable
        if (param == "")
            return "";
        string result = param.Substring(0, length);
        //return the result of the operation 
        return result;
    }
    public static string Right(string param, int length)
    {
        //start at the index based on the lenght of the sting minus
        //the specified lenght and assign it a variable 
        if (param == "")
            return "";
        string result = param.Substring(param.Length - length, length);
        //return the result of the operation
        return result;
    }

    public static string Mid(string param, int startIndex, int length)
    {
        //start at the specified index in the string ang get N number of
        //characters depending on the lenght and assign it to a variable 
        string result = "";
        try
        {
            result = param.Substring(startIndex, length);
            //return the result of the operation
        }
        catch (Exception)
        {
            result = "";
        }

        return result;
    }

    public static string Mid(string param, int startIndex)
    {
        //start at the specified index and return all characters after it
        //and assign it to a variable
        string result = param.Substring(startIndex);
        //return the result of the operation 
        return result;
    }

    /// <summary>
    /// Retorna a URL Base do sistema. Ex: http://localhost:22222
    /// </summary>
    /// <param name="pagina">informar somente: this</param>
    /// <returns></returns>
    public string ObterURLBase(Page pagina)
    {
        return pagina.Request.Url.Scheme + "://" + pagina.Request.Url.Authority + pagina.Request.ApplicationPath.TrimEnd('/');
    }

    public string GeraCdUsuario(int CdUsuario)
    {
        int ret;
        ret = (((CdUsuario * DateTime.Now.Year) + DateTime.Now.Day) * DateTime.Now.Hour) * DateTime.Now.Month;
        return new string(ret.ToString().Reverse().ToArray());
    }
    public string LerCdUsuario(int CdUsuario)
    {
        int ret;
        ret = (((CdUsuario / DateTime.Now.Year) - DateTime.Now.Day) / DateTime.Now.Hour) / DateTime.Now.Month;
        return new string(ret.ToString().Reverse().ToArray());
    }
    public string HashSha1(string input)
    {
        using (SHA1Managed sha1 = new SHA1Managed())
        {
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                // can be "x2" if you want lowercase
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}

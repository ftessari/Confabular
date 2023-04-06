using System;
using System.Linq;

/// <summary>
/// Summary description for Estrutura
/// </summary>
public static class Encapsulamentos
{
    static ClasseGenerica gen = new ClasseGenerica();
    private static int numPesquisa;

    private static int _numPasso;
    private static string _numPrograma;
    private static string _nomePrograma;
    private static string _cdUsuario;



    public static int NumPesquisa
    {
        get { return Encapsulamentos.numPesquisa; }
        set { Encapsulamentos.numPesquisa = value; }
    }
    private static int cdCategoriaLancamentos;

    private static int unidade;

    public static int CdCategoriaLancamentos
    {
        get { return cdCategoriaLancamentos; }
        set { cdCategoriaLancamentos = value; }
    }

    public static int Unidade
    {
        get { return unidade; }
        set { unidade = value; }
    }

    /// <summary>
    /// Código do formulário para requisição dos dados para edição no formulário
    /// </summary>
    public static string CdFormulario { get; set; }

    public static int NumPasso
    {
        get { return _numPasso; }
        set { _numPasso = value; }
    }

    public static string NumPrograma
    {
        get { return _numPrograma; }
        set { _numPrograma = value; }
    }

    public static string NomePrograma
    {
        get { return _nomePrograma; }
        set { _nomePrograma = value; }
    }

    public static string CdUsuario
    {
        get { return _cdUsuario; }
        set { _cdUsuario = value; }
    }

    public static string StringCriptografar(string textoCriptografar)
    {
        return gen.Criptografar(textoCriptografar);
    }

    public static string StringDescriptografr(string textoCriptografar)
    {
        return gen.Descriptografar(textoCriptografar);
    }

    public static  string CaminhoAdmImgThumb()
    {
        return "../Img/Thumb/";
    }

    public static  string CaminhoAdmImgNormal()
    {
        return "../Img/ImgGaleriaNormal/";
    }

    public static  string CaminhoHomeImgThumb()
    {
        return "Img/Thumb/";
    }

    public static  string CaminhoHomeImgNormal()
    {
        return "img/ImgGaleriaNormal/";
    }
}
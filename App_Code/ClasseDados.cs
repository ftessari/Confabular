using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClasseDados
/// </summary>
public class ClasseDados
{
    #region variáveis de acesso a dados

    private string sql;
    public SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
    private SqlDataReader dr;
    private DataSet ds;
    private SqlCommand cmd;
    private SqlDataAdapter da;

    #endregion

    # region Outras variáveis

    private string retornoString;
    private SqlDataReader retornoDataReader;
    private bool retornoBool;
    ClasseGenerica gen = new ClasseGenerica();

    # endregion

    public SqlConnection RetornaConexao()
    {
        return Conn;
    }

    public void populaGrid(string strSQL, GridView Grid)
    {
        Grid.DataSource = PopulaComDataSetPersonalizado(strSQL);
        Grid.DataBind();
    }

    public bool RegistroDuplicado(string nomeCampo, string tabela, string condicao)
    {
        bool r = false;
        try
        {
            sql = String.Format("SELECT {0} FROM {1} WHERE {2}", nomeCampo, tabela, condicao);
            cmd = new SqlCommand(sql, Conn);
            Conn.Open();
            dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
            if (dr != null)
            {
                if (dr.HasRows)
                {
                    r = true;
                }
            }
            Conn.Close();
        }
        catch (Exception)
        {
            retornoString = "erro";
        }
        dr.Close();
        dr.Dispose();

        return r;
    }

    public void populaGrid(string strSQL, DataList Dtl)
    {
        Dtl.DataSource = PopulaComDataSetPersonalizado(strSQL);
        Dtl.DataBind();
    }

    /// <summary>
    /// Popula um DropDownList já inserindo no index 0 -> Selecione um item
    /// </summary>
    /// <param name="strSql">Informe a Sql sendo que o primeiro campo é o Autonumeração eo segundo o O que vc deseja mostrar no TextField</param>
    /// <param name="drop">Nome da DropDown - Não informe nenuma propriedade.</param>
    public void PopulaDrop(string strSql, DropDownList drop)
    {
        var d = PopulaComDataSetPersonalizado(strSql);
        drop.DataSource = d;
        drop.DataValueField = d.Tables[0].Columns[0].ColumnName;
        drop.DataTextField = d.Tables[0].Columns[1].ColumnName;
        drop.DataBind();
        drop.Items.Insert(0, "-- Não informar --");
    }

    public void PopulaDropRef(string strSql, DropDownList drop)
    {
        var d = PopulaComDataSetPersonalizado(strSql);
        drop.DataSource = d;
        drop.DataValueField = d.Tables[0].Columns[0].ColumnName; // id
        drop.DataTextField = d.Tables[0].Columns[1].ColumnName;        
        drop.DataBind();
        drop.Items.Insert(0, "-- Não informar --");
    }

    public void PopulaDropMult(string strSql, DropDownList drop, bool Ultimo)
    {       
        cmd = new SqlCommand();
        cmd.Connection = Conn;
        cmd.CommandText = strSql;
        if (!Ultimo)
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
        } 
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        drop.DataSource = ds;
        drop.DataValueField = ds.Tables[0].Columns[0].ColumnName;
        drop.DataTextField = ds.Tables[0].Columns[1].ColumnName;
        drop.DataBind();
        drop.Items.Insert(0, "-- Não informar --");

        if (Ultimo)
        {
            Conn.Close();
        }
    }

    public void PopulaDrop(string strSql, DropDownList drop, Boolean SemTextoIndex0)
    {
        var d = PopulaComDataSetPersonalizado(strSql);
        drop.DataSource = d;
        drop.DataValueField = d.Tables[0].Columns[0].ColumnName;
        drop.DataTextField = d.Tables[0].Columns[1].ColumnName;
        drop.DataBind();
        drop.Items.Insert(0, "");
    }
    /// <summary>
    /// para o caso dos dados retornar somente uma linha e uma coluna
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="drop"></param>
    public void PopulaDropUmCampo(string strSql, DropDownList drop)
    {
        var d = PopulaComDataSetPersonalizado(strSql);
        drop.DataSource = d;
        drop.DataValueField = d.Tables[0].Columns[0].ColumnName;
        drop.DataTextField = d.Tables[0].Columns[0].ColumnName;
        drop.DataBind();
        drop.Items.Insert(0, "-- Todos --");
    }

    public void PopulaCkeckBox(string strSql, CheckBoxList ck)
    {
        var d = PopulaComDataSetPersonalizado(strSql);
        ck.DataSource = d;
        ck.DataValueField = d.Tables[0].Columns[0].ColumnName;
        ck.DataTextField = d.Tables[0].Columns[1].ColumnName;
        ck.DataBind();
    }

    public void PopulaRadioButtonList(string strSql, RadioButtonList rad)
    {
        var d = PopulaComDataSetPersonalizado(strSql);
        rad.DataSource = d;
        rad.DataValueField = d.Tables[0].Columns[0].ColumnName;
        rad.DataTextField = d.Tables[0].Columns[1].ColumnName;
        rad.DataBind();
    }
    /// <summary>
    /// Popula um DropDownList já inserindo no index 0 -> Selecione um item
    /// </summary>
    /// <param name="strSql">Informe a Sql sendo que o primeiro campo é o Autonumeração eo segundo o O que vc deseja mostrar no TextField</param>
    /// <param name="drop">Nome da DropDown - Não informe nenuma propriedade.</param>
    /// <param name="msgIndexZero">Mensagem para a index zero do dropdown</param>
    public void PopulaDrop(string strSql, DropDownList drop, string msgIndexZero)
    {
        var d = PopulaComDataSetPersonalizado(strSql);
        drop.DataSource = d;
        drop.DataValueField = d.Tables[0].Columns[0].ColumnName;
        drop.DataTextField = d.Tables[0].Columns[1].ColumnName;
        drop.DataBind();
        drop.Items.Insert(0, msgIndexZero);
    }
    /// <summary>
    /// Método que verifica se já existe um usuário cadastrado com o mesmo e-mail no qual outro usuário pretende se cadastrar.
    /// </summary>
    /// <param name="valorVerificar">Valor ou texto a ser verificado</param>
    /// <param name="nomeConexao">Nome da conexão no web.config</param>
    /// <param name="nomeCampo">Campo a verificar</param>
    /// <param name="tabela">Nome da tabela a verifivar</param>
    /// <param name="condicaoSql">Informar a condição para a pesquisa, ex: codigo = 1</param>
    /// <returns>True para já existe e FALSE quando não existir</returns>
    public bool TestaDuplicidade(string valorVerificar, string nomeConexao, string nomeCampo, string tabela, string condicaoSql)
    {
        int codigoRetornado;

        Conn.Open();
        var cmdText = string.Format("select {0} from {1} where {2}", nomeCampo, tabela, condicaoSql);
        var command = new SqlCommand(cmdText, Conn);
        
            command.CommandType = System.Data.CommandType.Text;
            var adapter = new SqlDataAdapter(command);
            var ds = new System.Data.DataSet();
            adapter.Fill(ds, tabela);


            // Pega o Valor CODIGO do dataSet e atribui a variáveis
            try
            {
                codigoRetornado = Convert.ToInt32(ds.Tables[tabela].Rows[0][nomeCampo].ToString());
            }
            catch
            {
                codigoRetornado = 0;
            }

            // Faz os testes

            if (codigoRetornado > 0)
            {
                retornoBool = true;
            }

        return retornoBool;
    }

    /// <summary>
    /// Essa classe retorna somente um campo de uma determinada linha ou contagem por uma condição personalizada
    /// </summary>
    /// <param name="nomeCampo">Informe o nome do campo para retorno</param>
    /// <param name="tabela">Nome da tabela</param>
    /// <param name="condicao">Informe a condição para o retorno ex: 'cd_cliente = 17'</param>
    /// <param name="contagem">Vc deseja uma contagem de linhas? Se sim informe 'True' se não 'False'</param>
    /// <returns></returns>
    public string RetornaCampoLinha(string nomeCampo, string tabela, string condicao, bool contagem)
    {
        try
        {
            sql = "SELECT ";
            if (contagem)
            {
                sql += string.Format("COUNT({0})", nomeCampo);
            }
            else
            {
                sql += nomeCampo;
            }
            sql += string.Format(" FROM {0} WHERE {1}", tabela, condicao);

            cmd = new SqlCommand(sql, Conn);
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }

            dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
            dr.Read();
            if (contagem)
            {
                retornoString = dr[0].ToString();
            }
            else
            {
                retornoString = dr[nomeCampo.Replace("[", "").Replace("]", "")].ToString();
            }

            dr.Close();
            Conn.Close();
        }
        catch (Exception)
        {
            retornoString = "erro";
        }
        finally
        {
            dr.Close();
            Conn.Close();
        }
        return retornoString;
    }

    public string SomaColuna(string nomeCampo, string tabela, string condicao)
    {
        try
        {
            sql = String.Format("SELECT sum({0}) as {0} FROM {1} WHERE {2}", nomeCampo, tabela, condicao);
            cmd = new SqlCommand(sql, Conn);
            Conn.Open();
            dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
            dr.Read();
            retornoString = dr[nomeCampo.Replace("[", "").Replace("]", "")].ToString();
            dr.Close();
            Conn.Close();
        }
        catch (Exception erro)
        {
            retornoString = erro.Message;
        }
        finally
        {
            dr.Close();
            Conn.Close();
        }
        return retornoString;
    }
    public string ContaLinhas(string nomeCampo, string tabela, string condicao)
    {
        try
        {
            sql = String.Format("SELECT count({0}) as {0} FROM {1} WHERE {2}", nomeCampo, tabela, condicao);
            cmd = new SqlCommand(sql, Conn);
            Conn.Open();
            dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
            dr.Read();
            retornoString = dr[nomeCampo.Replace("[", "").Replace("]", "")].ToString();
            dr.Close();
            dr.Dispose();
            Conn.Close();
        }
        catch (Exception)
        {
            retornoString = "erro";
        }

        return retornoString;
    }

    public string DeletaRegistro(string tabela, string nomeCampo, int codigoRegistro)
    {
        try
        {
            sql = String.Format("DELETE FROM {0} WHERE {1} = {2}", tabela, nomeCampo, codigoRegistro);
            cmd = new SqlCommand(sql, Conn);
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();
            retornoString = gen.MensagensInformativas(3);
        }
        catch (Exception erro)
        {
            retornoString = erro.Message;
        }
        finally
        {
            Conn.Close();
        }
        return retornoString;
    }

    public SqlDataReader PopulaComDataReader(string campos, string tabela)
    {
        try
        {
            sql = String.Format("SELECT {0} FROM {1}", campos, tabela);
            cmd = new SqlCommand(sql, Conn);
            Conn.Open();
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            retornoDataReader = dr;
            dr.Close();
            Conn.Close();
        }
        catch (Exception erro)
        {
            retornoString = erro.Message;
        }
        finally
        {
            Conn.Close();
        }

        return retornoDataReader;
    }

    public DataSet ConsultaAleatoria(int qtdeItensRetorno, string campos, string tabela, string campoAutoNumTabela)
    {
        if (Conn.State == ConnectionState.Open)
        {
            Conn.Close();
        }
        try
        {
            sql = string.Format("select top {0} {1} from {2} ORDER BY RND(INT(NOW*[{3}])-NOW*[{3}])", qtdeItensRetorno, campos, tabela, campoAutoNumTabela);
            cmd = new SqlCommand();
            cmd.Connection = Conn;
            cmd.CommandText = sql;
            Conn.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Conn.Close();
        }

        return ds;
    }
    /// <summary>
    /// Classe genérica para obter dados do banco
    /// </summary>
    /// <param name="campos">informe o nome dos campos separado por virgula</param>
    /// <param name="tabela">Informe o nome da tabela, lembrando q, vc pode colocar uma condição ex: 'Nome_Tabela where Cd_Condicao = 8'.</param>
    /// <returns></returns>
    public DataSet PopulaComDataSet(string campos, string tabela)
    {
        if (Conn.State == ConnectionState.Open)
        {
            Conn.Close();
        }
        try
        {
            sql = String.Format("SELECT {0} FROM {1}", campos, tabela);
            cmd = new SqlCommand();
            cmd.Connection = Conn;
            cmd.CommandText = sql;
            Conn.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Conn.Close();
        }

        return ds;
    }

    public DataRow retornaLinha(string sql)
    {
        return PopulaComDataSetPersonalizado(sql).Tables[0].Rows[0];
    }

    public DataSet PopulaComDataSetPersonalizado(string sql)
    {
        if (Conn.State == ConnectionState.Open)
        {
            Conn.Close();
        }
        try
        {
            sql = String.Format(sql);
            cmd = new SqlCommand();
            cmd.Connection = Conn;
            cmd.CommandText = sql;
            Conn.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
        }
        catch(Exception erro)
        {
            var err = erro;
        }
        finally
        {
            Conn.Close();
        }

        return ds;
    }
    public void CapturaErro(string formulario, string erroResumido, string erroDetalhado)
    {
        sql = @"INSERT INTO ERROS(ERRO_RESUMIDO, ERRO_DETALHADO, FORMULARIO)
                VALUES(@ERRO_RESUMIDO, @ERRO_DETALHADO, @FORMULARIO)";
        cmd = new SqlCommand(sql, Conn);
        cmd.Parameters.AddWithValue("@ERRO_RESUMIDO", erroResumido);
        cmd.Parameters.AddWithValue("@ERRO_DETALHADO", erroDetalhado);
        cmd.Parameters.AddWithValue("@FORMULARIO", formulario);
        Conn.Open();
        cmd.ExecuteNonQuery();
        Conn.Close();
    }

    /// <summary>
    /// Insert/Update personalizado
    /// </summary>
    /// <param name="sqlInsertUpdate">Sua SQL de inserção/atualização</param>
    /// <returns></returns>
    public string InsertUpdatePersonalizado(string sqlInsertUpdate)
    {
        string r;
        try
        {
            cmd = new SqlCommand(sqlInsertUpdate, Conn);

            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            cmd.ExecuteNonQuery();
            Conn.Close();

            r = gen.MensagensInformativas(1);
        }
        catch (Exception erro)
        {
            r = gen.MensagensErro(erro.Message);
        }
        return r;
    }

    /// <summary>
    /// Metodo utilizado para a execução de comandos com Stored Procedures do SQL Server
    /// </summary>
    /// <param name="NmProcedure">Nome da procedure com seu respectivo parametro, ex: sp_teste 1, 2</param>
    /// <param name="MensagemInformativa">Mensagem personalizada para a execução caso tudo tenha ocorrido sem problemas</param>
    /// <returns></returns>
    public string instrucaoStoredProcedure(string NmProcedure, string MensagemInformativa)
    {
        string r;
        try
        {
            cmd = new SqlCommand(NmProcedure, Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();

            r = gen.MensagensInformativas(1).Replace("Registro Cadastrado com sucesso!", MensagemInformativa);
        }
        catch (Exception erro)
        {
            r = gen.MensagensErro(erro.Message);
        }
        return r;
    }
    /// <summary>
    /// Classe responsável para inserir dados no banco
    /// </summary>
    /// <param name="table">Nome da Tabela</param>
    /// <param name="parameterMap"></param>
    /// <returns></returns>
    private string CreateInsertSql(string table,
                                          IDictionary<string, string> parameterMap)
    {
        var keys = parameterMap.Keys.ToList();
        // ToList() LINQ extension method used because order is NOT
        // guaranteed with every implementation of IDictionary<TKey, TValue>

        var sql = new StringBuilder("SET DATEFORMAT DMY INSERT INTO ").Append(table).Append("(");

        for (var i = 0; i < keys.Count; i++)
        {
            sql.Append(keys[i]);
            if (i < keys.Count - 1)
                sql.Append(", ");
        }

        sql.Append(") VALUES(");

        for (var i = 0; i < keys.Count; i++)
        {
            sql.Append('@').Append(keys[i]);
            if (i < keys.Count - 1)
                sql.Append(", ");
        }

        return sql.Append(")").ToString();
    }

    public string SqlInsert(string table, Page pagina, IDictionary<string, string> parameterMap)
    {
        string r = string.Empty;

        try
        {
            var connection = Conn;
            if (Conn.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var command = connection.CreateCommand();

            command.Connection = connection;
            command.CommandText = CreateInsertSql(table, parameterMap);
            foreach (var pair in parameterMap)
                command.Parameters.AddWithValue(pair.Key, pair.Value.Trim());
            command.ExecuteNonQuery();
            Conn.Close();
            r = gen.MensagensInformativas(1);

        }
        catch (Exception erro)
        {
            r = gen.MensagensErro(erro.Message);
        }

        return r;
    }


    private string CreateUpdateSql(string table, string condicao,
                                          IDictionary<string, string> parameterMap)
    {
        var keys = parameterMap.Keys.ToList();

        var sql = new StringBuilder("set dateformat dmy UPDATE ").Append(table).Append(" SET ");

        for (var i = 0; i < keys.Count; i++)
        {
            sql.Append(keys[i]);
            sql.Append(" = ");
            sql.Append('@').Append(keys[i]);
            sql.Append(", ");
        }

        sql = sql.Remove(sql.ToString().LastIndexOf(","), 1);
        return sql.Append(" WHERE " + condicao).ToString();
    }

    /// <summary>
    /// /Classe de responsável pela atualização dos dados
    /// </summary>
    /// <param name="table">Nome da tabela</param>
    /// <param name="condicao">Condição SQL</param>
    /// <param name="parameterMap">Informar os parâmetros seguidos dos valores, ex: {"Parâmtro", TextBox1.Text}</param>
    /// <returns></returns>
    public string SqlUpdate(string table, string condicao, IDictionary<string, string> parameterMap)
    {
        string r = string.Empty;

        try
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            var command = Conn.CreateCommand();
            command.Connection = Conn;
            command.CommandText = CreateUpdateSql(table, condicao, parameterMap);
            foreach (var pair in parameterMap)
                command.Parameters.AddWithValue(pair.Key, pair.Value.Trim());
            command.ExecuteNonQuery();
            Conn.Close();
            r = gen.MensagensInformativas(2);
        }
        catch (Exception erro)
        {
            r = gen.MensagensErro(erro.Message);
        }

        return r;
    }
}
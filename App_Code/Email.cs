using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;


/// <summary>
/// Classe para envio e recebimento de emails via SMTP e POP3
/// </summary>
public static class E_Mail
{
    static ClasseGenerica gen = new ClasseGenerica();
    /// <summary>
    /// Envia um email para um ou varios destinatarios
    /// </summary>
    /// <param name="Assunto">Assunto do e-mail</param>
    /// <param name="Texto">Text para o corpo do e-mail</param>
    /// <param name="IsBodyHtml">Se o texto enviado é no formato HTML ou não</param>
    /// <param name="Destinatario">Lista de destinatários</param>
    /// <param name="Remetente">Remetente do email (endereço de e-mail do remetente)</param>
    /// <param name="Senha">Senha do remetente informado</param>
    /// <param name="ServidorEnvio">Servidor SMTP para envio do email referente ao remetente informado</param>
    public static void EnviaEmail(string Assunto, string Texto, bool IsBodyHtml, List<string> Destinatario,
        string Remetente, string Senha, string ServidorEnvio)
    {
        if (Destinatario.Count > 0)
        {
            SmtpClient smtpServidor = new SmtpClient()
            {
                Host = ServidorEnvio,
                Port = 25, //Porta padrão sem autenticação
                Credentials = new NetworkCredential(Remetente, Senha)
            };

            MailAddress emailEmitente = new MailAddress(Remetente, Remetente, System.Text.Encoding.UTF8);
            MailAddress emailDestinatario = new MailAddress(Destinatario[0]);

            MailMessage msg = new MailMessage(emailEmitente, emailDestinatario);
            msg.Subject = Assunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = Texto;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = IsBodyHtml;
            msg.Priority = MailPriority.Normal;

            try
            {
                smtpServidor.Send(msg);
                msg.To.Clear();
            }
            catch (SmtpException Ex)
            {
                if (Ex.StatusCode == SmtpStatusCode.MustIssueStartTlsFirst)
                {
                    smtpServidor.Port = 587; // Porta padrão com autenticação
                    smtpServidor.EnableSsl = true; // Utiliza conexão segura

                    try
                    {
                        smtpServidor.Send(msg);
                        msg.To.Clear();
                    }
                    catch (Exception Exx)
                    {
                        throw Exx;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                msg.Dispose();
                Destinatario.Clear();
            }
        }
    }

    /// <summary>
    /// Envia um email para um ou varios destinatarios
    /// </summary>
    /// <param name="Assunto">Assunto do e-mail</param>
    /// <param name="Texto">Text para o corpo do e-mail</param>
    /// <param name="IsBodyHtml">Se o texto enviado é no formato HTML ou não</param>
    /// <param name="Destinatario">Apenas um destinatários</param>
    /// <param name="Remetente">Remetente do email (endereço de e-mail do remetente)</param>
    /// <param name="Senha">Senha do remetente informado</param>
    /// <param name="ServidorEnvio">Servidor SMTP para envio do email referente ao remetente informado</param>
    public static string EnviaEmailUmDestino(string Assunto, string Texto, bool IsBodyHtml, string Destinatario,
        string Remetente, string Senha, string ServidorEnvio)
    {
        string retorno = "";

        SmtpClient smtpServidor = new SmtpClient()
        {
            Host = ServidorEnvio,
            Port = 25, //Porta padrão sem autenticação
            Credentials = new NetworkCredential(Remetente, Senha)
        };

        MailAddress emailEmitente = new MailAddress(Remetente, Remetente, System.Text.Encoding.UTF8);
        MailAddress emailDestinatario = new MailAddress(Destinatario);

        MailMessage msg = new MailMessage(emailEmitente, emailDestinatario);
        msg.Subject = Assunto;
        msg.SubjectEncoding = System.Text.Encoding.UTF8;
        msg.Body = Texto;
        msg.BodyEncoding = System.Text.Encoding.UTF8;
        msg.IsBodyHtml = IsBodyHtml;
        msg.Priority = MailPriority.Normal;

        
        try
        {
            smtpServidor.Send(msg);
            msg.To.Clear();
            retorno = gen.MensagensInfoGeral("Email enviado com sucesso!");
        }
        catch (SmtpException Ex)
        {
            if (Ex.StatusCode == SmtpStatusCode.MustIssueStartTlsFirst)
            {
                smtpServidor.Port = 587; // Porta padrão com autenticação
                smtpServidor.EnableSsl = true; // Utiliza conexão segura

                try
                {
                    smtpServidor.Send(msg);
                    msg.To.Clear();
                }
                catch (Exception Exx)
                {
                    retorno = Exx.Message;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            msg.Dispose();
        }

        return retorno;
    }

    public static int BoolToInt(this bool Boolean)
    {
        if (Boolean)
            return 1;
        else
            return 0;
    }

    public static string EnvioEmailsLocaweb(string NmRemetente, string Destinatario, string TituloMsg, string Conteudo)
    {
        string retorno = string.Empty;

        //Define os dados do e-mail
        string nomeRemetente = NmRemetente;
        string emailRemetente = "site@grupomarcovel.com.br";
        string senha = "ESA28ZT17";

        //Host da porta SMTP
        string SMTP = "smtp.grupomarcovel.com.br";

        string emailDestinatario = Destinatario;
        //string emailComCopia        = "email@comcopia.com.br";
        //string emailComCopiaOculta  = "email@comcopiaoculta.com.br";

        string assuntoMensagem = TituloMsg;
        string conteudoMensagem = Conteudo;

        //Cria objeto com dados do e-mail.
        MailMessage objEmail = new MailMessage();

        //Define o Campo From e ReplyTo do e-mail.
        objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + emailRemetente + ">");

        //Define os destinatários do e-mail.
        objEmail.To.Add(emailDestinatario);

        //Enviar cópia para.
        //objEmail.CC.Add(emailComCopia);

        //Enviar cópia oculta para.
        //objEmail.Bcc.Add(emailComCopiaOculta);

        //Define a prioridade do e-mail.
        objEmail.Priority = System.Net.Mail.MailPriority.Normal;

        //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
        objEmail.IsBodyHtml = true;

        //Define título do e-mail.
        objEmail.Subject = assuntoMensagem;

        //Define o corpo do e-mail.
        objEmail.Body = conteudoMensagem;


        //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");


        // Caso queira enviar um arquivo anexo
        //Caminho do arquivo a ser enviado como anexo
        //string arquivo = Server.MapPath("arquivo.jpg");

        // Ou especifique o caminho manualmente
        //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

        // Cria o anexo para o e-mail
        //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

        // Anexa o arquivo a mensagem
        //objEmail.Attachments.Add(anexo);

        //Cria objeto com os dados do SMTP
        System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

        //Alocamos o endereço do host para enviar os e-mails  
        objSmtp.Credentials = new System.Net.NetworkCredential(emailRemetente, senha);
        objSmtp.Host = SMTP;
        objSmtp.Port = 587;
        //Caso utilize conta de email do exchange da locaweb deve habilitar o SSL
        //objEmail.EnableSsl = true;

        //Enviamos o e-mail através do método .send()
        try
        {
            objSmtp.Send(objEmail);
            retorno = "E-mail enviado com sucesso !";
        }
        catch (Exception ex)
        {
            retorno = "Ocorreram problemas no envio do e-mail. Erro = " + ex.Message;
        }
        finally
        {
            //excluímos o objeto de e-mail da memória
            objEmail.Dispose();
            //anexo.Dispose();
        }
        return retorno;
    }

    public static string EnviaEmailLocaweb(string NomeRemetente, string EmailRemetente, string Senha, string Smtp, string EmailDestinatario, string AssuntoMensagem, string ConteudoMensagem)
    {
        string retorno;

        string nomeRemetente = NomeRemetente;
        string emailRemetente = EmailRemetente;
        string senha = Senha;
        string smtp = Smtp;
        string emailDestinatario = EmailDestinatario;

        string assuntoMensagem = AssuntoMensagem;
        string conteudoMensagem = ConteudoMensagem;

        MailMessage objEmail = new MailMessage();

        //Define o Campo From e ReplyTo do e-mail.
        objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + emailRemetente + ">");

        //Define os destinatários do e-mail.
        objEmail.To.Add(emailDestinatario);

        //Enviar cópia para.
        //objEmail.CC.Add(emailComCopia);

        //Enviar cópia oculta para.
        //objEmail.Bcc.Add(emailComCopiaOculta);

        //Define a prioridade do e-mail.
        objEmail.Priority = System.Net.Mail.MailPriority.Normal;

        //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
        objEmail.IsBodyHtml = true;

        //Define título do e-mail.
        objEmail.Subject = assuntoMensagem;

        //Define o corpo do e-mail.
        objEmail.Body = conteudoMensagem;


        //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");


        // Caso queira enviar um arquivo anexo
        //Caminho do arquivo a ser enviado como anexo
        //string arquivo = Server.MapPath("arquivo.jpg");

        // Ou especifique o caminho manualmente
        //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

        // Cria o anexo para o e-mail
        //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

        // Anexa o arquivo a mensagem
        //objEmail.Attachments.Add(anexo);

        //Cria objeto com os dados do SMTP
        SmtpClient objSmtp = new SmtpClient();

        //Alocamos o endereço do host para enviar os e-mails  
        objSmtp.Credentials = new System.Net.NetworkCredential(emailRemetente, senha);
        objSmtp.Host = smtp;
        objSmtp.Port = 587;
        //Caso utilize conta de email do exchange da locaweb deve habilitar o SSL
        //objEmail.EnableSsl = true;

        //Enviamos o e-mail através do método .send()
        try
        {
            objSmtp.Send(objEmail);
            retorno = "E-mail enviado com sucesso!";
        }
        catch (Exception ex)
        {
            retorno = "Ocorreram problemas no envio do e-mail. Erro = " + ex.Message;
        }
        finally
        {
            //excluímos o objeto de e-mail da memória
            objEmail.Dispose();
            //anexo.Dispose();
        }
        return retorno;
    }

}


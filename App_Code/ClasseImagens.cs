using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Imagens
/// </summary>
public class ClasseImagens
{
    ClasseDados dados = new ClasseDados();

    public string YoutubeThumbnail(string pEmbed)
    {
        string emdeb = pEmbed;

        emdeb = emdeb.Substring(emdeb.LastIndexOf("embed/") + 6);
        emdeb = emdeb.Substring(0, 11);

        return emdeb;
    }


    /// <summary>
    /// Classe responsavel por realizar a o upload da imagem e redimenciona-la, caso não ocorra erros retorna TRUE, senão FALSE
    /// </summary>
    /// <param name="controleFileUpload">Informe o controle file upload como neste exemplo: FileUpload1.PostedFile.FileName</param>
    /// <param name="requestThis">informe somente a palavra reservada this</param>
    /// <param name="alias">Caminho para o salvamento da imagem</param>
    /// <param name="orientacaoImg">Informar P (paisagem) ou R (retrato)</param>
    /// <returns></returns>
    public string resizeImg(FileUpload controleFileUpload, Page requestThis, string alias, string orientacaoImg)
    {
        string retorno = "";

        var uploadedFile = controleFileUpload.PostedFile.FileName;
        var extractPos = uploadedFile.ToString().LastIndexOf("\\") + 1;


        //to retrieve only Filename from the complete path
        var uploadedFileName = uploadedFile.ToString().Substring(extractPos, uploadedFile.ToString().Length - extractPos);

        // Save uploaded file to server at the in the Pics folder
        controleFileUpload.PostedFile.SaveAs(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, alias, uploadedFileName));

        try
        {

            //Read in the image filename whose thumbnail has to be created
            var imageUrl = uploadedFileName;

            //You may even specify a standard thumbnail size

            int imageWidth;
            int imageHeight;

            if (orientacaoImg == "P")
            {
                imageWidth = 200;
                imageHeight = 149;
            }
            else
            {
                imageWidth = 149;
                imageHeight = 200;
            }

            if (imageUrl.IndexOf("/") >= 0 || imageUrl.IndexOf("\\") >= 0)
            {
                requestThis.Response.End();
            }

            imageUrl = string.Format("{0}/{1}", alias, imageUrl);

            var fullSizeImg = System.Drawing.Image.FromFile(requestThis.Request.PhysicalApplicationPath + imageUrl);

            var dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

            var thumbNailImg = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight, dummyCallBack, IntPtr.Zero);

            //We need to create a unique filename for each generated image
            var myDate = DateTime.Now;

            var myString = string.Format("{0}.jpg", myDate.ToString("ddMMyyhhmmss"));

            //Save the thumbnail in Png format. You may change it to a diff format with the ImageFormat property
            thumbNailImg.Save(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, string.Format("{0}//Thumb//", alias), myString), ImageFormat.Jpeg);

            thumbNailImg.Dispose();

/*
            var ms = new MemoryStream((controleFileUpload).FileBytes);
            var map = System.Drawing.Image.FromStream(ms) as Bitmap;
            System.Drawing.Image thumbnail = new Bitmap(560, 420);
            var graphic = Graphics.FromImage(thumbnail);
            if (map != null) graphic.DrawImage(map, 0, 0, 560, 420);
            thumbnail.Save(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, string.Format("{0}//ImgGaleriaNormal//", alias), myString), ImageFormat.Jpeg);
            thumbnail.Dispose();
*/
            retorno = myString;
        }
        catch (Exception erro)
        {
            string form = requestThis.Request.Url.AbsoluteUri;
            form = form.Substring(form.LastIndexOf("/") + 1);
            dados.CapturaErro(form, erro.Message, erro.ToString());

        }
        finally
        {
            if (dados.Conn.State == System.Data.ConnectionState.Open)
            {
                dados.Conn.Close();
            }
        }

        return retorno;
    }

    //public string renomeiaArquivo(string caminhoImg, string extencao)
    //{
    //    var r =
    //        System.IO.File.Move(caminhoImg + "." + extencao, DateTime.Now.ToString("ddMMyyhhmmss") + "." + extencao);
    //    return r.ToString();
    //}

    /// <summary>
    /// Classe responsavel por realizar a o upload da imagem e redimenciona-la, caso não ocorra erros retorna TRUE, senão FALSE
    /// </summary>
    /// <param name="controleFileUpload">Informe o controle file upload como neste exemplo: FileUpload1.PostedFile.FileName</param>
    /// <param name="requestThis">informe somente a palavra reservada this</param>
    /// <param name="alias">Caminho para o salvamento da imagem</param>
    /// <param name="orientacaoImg">Informar P (paisagem) ou R (retrato)</param>
    /// <param name="larguraImgNormal"></param>
    /// <param name="alturaImgNormal"></param>
    /// <returns></returns>
    public string resizeImgPersonalizado(FileUpload controleFileUpload, Page requestThis, string alias, string orientacaoImg, int largura, int altura)
    {
        string retorno = "";

        var uploadedFile = controleFileUpload.PostedFile.FileName;
        var extractPos = uploadedFile.LastIndexOf("\\") + 1;


        //to retrieve only Filename from the complete path
        var uploadedFileName = uploadedFile.Substring(extractPos, uploadedFile.Length - extractPos);

        // Save uploaded file to server at the in the Pics folder C:\dir\confabular\confabular\imgs\pages
        string caminho = string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, alias, uploadedFileName);
        controleFileUpload.PostedFile.SaveAs(caminho);

        try
        {
            //Read in the image filename whose thumbnail has to be created
            var imageUrl = uploadedFileName;

            //You may even specify a standard thumbnail size

            int imageWidth = largura;
            int imageHeight = altura;

            if (orientacaoImg == "P") // Paisagem
            {
                if (largura > 1080)
                {
                    imageWidth = 1080;
                }
                if (altura > 750)
                {
                    imageHeight = 750;
                }                
            }
            else if (orientacaoImg == "A") // Avatar
            {
                if (largura > 200)
                {
                    imageWidth = 200;
                }
                if (altura > 200)
                {
                    imageHeight = 200;
                }
            }
            else if (orientacaoImg == "B") // Book = Capa de livro
            { 
                if (largura > 200)
                {
                    imageWidth = 200;
                }
                if (altura > 250)
                {
                    imageHeight = 250;
                }
            }
            else // R Retrato 
            {
                if (largura > 750)
                {
                    imageWidth = 750;
                }
                if (altura > 1080)
                {
                    imageHeight = 1080;
                }
            }

            if (imageUrl.IndexOf("/") >= 0 || imageUrl.IndexOf("\\") >= 0)
            {
                requestThis.Response.End();
            }

            imageUrl = string.Format("{0}/{1}", alias, imageUrl);

            var fullSizeImg = System.Drawing.Image.FromFile(requestThis.Request.PhysicalApplicationPath + imageUrl);

            var dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

            var thumbNailImg = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight, dummyCallBack, IntPtr.Zero);

            //We need to create a unique filename for each generated image
            var myDate = DateTime.Now;

            var myString = string.Format("{0}.jpg", myDate.ToString("ddMMyyhhmmss"));

            //Save the thumbnail in Png format. You may change it to a diff format with the ImageFormat property
            thumbNailImg.Save(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, string.Format("{0}//Thumb//", alias), myString), ImageFormat.Jpeg);

            thumbNailImg.Dispose();

/*
            var ms = new MemoryStream((controleFileUpload).FileBytes);
            var map = System.Drawing.Image.FromStream(ms) as Bitmap;
            System.Drawing.Image thumbnail = new Bitmap(largura, altura);
            var graphic = Graphics.FromImage(thumbnail);
            if (map != null) graphic.DrawImage(map, 0, 0, largura, altura);
            thumbnail.Save(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, string.Format("{0}//ImgGaleriaNormal//", alias), myString), ImageFormat.Jpeg);
*/
            retorno = myString;
        }
        catch (Exception erro)
        {
            string form = requestThis.Request.Url.AbsoluteUri;
            form = form.Substring(form.LastIndexOf("/") + 1);
            dados.CapturaErro(form, erro.Message, erro.ToString());

        }
        finally
        {
            if (dados.Conn.State == System.Data.ConnectionState.Open)
            {
                dados.Conn.Close();
                
            }
        }
        
        return retorno;
    }


    public string resizeImgPersonalizado(FileUpload controleFileUpload, Page requestThis, string alias, string orientacaoImg, int larguraImgThumb, int alturaImgThumb, int larguraImgNormal, int alturaImgNormal)
    {
        string retorno = "";

        var uploadedFile = controleFileUpload.PostedFile.FileName;
        var extractPos = uploadedFile.LastIndexOf("\\") + 1;


        //to retrieve only Filename from the complete path
        var uploadedFileName = uploadedFile.Substring(extractPos, uploadedFile.Length - extractPos);

        // Save uploaded file to server at the in the Pics folder
        controleFileUpload.PostedFile.SaveAs(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, alias, uploadedFileName));

        try
        {
            //Read in the image filename whose thumbnail has to be created
            var imageUrl = uploadedFileName;

            //You may even specify a standard thumbnail size

            int imageWidth;
            int imageHeight;

            if (orientacaoImg == "P")
            {
                imageWidth = larguraImgThumb;
                imageHeight = alturaImgThumb;
            }
            else
            {
                imageWidth = alturaImgThumb;
                imageHeight = larguraImgThumb;
            }

            if (imageUrl.IndexOf("/") >= 0 || imageUrl.IndexOf("\\") >= 0)
            {
                requestThis.Response.End();
            }

            imageUrl = string.Format("{0}/{1}", alias, imageUrl);

            var fullSizeImg = System.Drawing.Image.FromFile(requestThis.Request.PhysicalApplicationPath + imageUrl);

            var dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

            var thumbNailImg = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight, dummyCallBack, IntPtr.Zero);

            //We need to create a unique filename for each generated image
            var myDate = DateTime.Now;

            var myString = string.Format("{0}.jpg", myDate.ToString("ddMMyyhhmmss"));

            //Save the thumbnail in Png format. You may change it to a diff format with the ImageFormat property
            thumbNailImg.Save(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, string.Format("{0}//Thumb//", alias), myString), ImageFormat.Jpeg);

            thumbNailImg.Dispose();

            /*
            var ms = new MemoryStream((controleFileUpload).FileBytes);
            var map = System.Drawing.Image.FromStream(ms) as Bitmap;
            System.Drawing.Image thumbnail = new Bitmap(larguraImgNormal, alturaImgNormal);
            var graphic = Graphics.FromImage(thumbnail);
            if (map != null) graphic.DrawImage(map, 0, 0, larguraImgNormal, alturaImgNormal);
            thumbnail.Save(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, string.Format("{0}//ImgGaleriaNormal//", alias), myString), ImageFormat.Jpeg);
            */
            retorno = myString;
        }
        catch (Exception erro)
        {
            string form = requestThis.Request.Url.AbsoluteUri;
            form = form.Substring(form.LastIndexOf("/") + 1);
            dados.CapturaErro(form, erro.Message, erro.ToString());

        }
        finally
        {
            if (dados.Conn.State == System.Data.ConnectionState.Open)
            {
                dados.Conn.Close();
            }
        }

        return retorno;
    }

    /// <summary>
    /// Classe responsavel por realizar a o upload da imagem e redimenciona-la, caso não ocorra erros retorna TRUE, senão FALSE
    /// </summary>
    /// <param name="controleFileUpload">Informe o controle file upload como neste exemplo: FileUpload1.PostedFile.FileName</param>
    /// <param name="requestThis">informe somente a palavra reservada this</param>
    /// <param name="alias">Caminho para o salvamento da imagem</param>
    /// <param name="orientacaoImg">Informar P (paisagem) ou R (retrato)</param>
    /// <param name="largura"></param>
    /// <param name="altura"></param>
    /// <returns></returns>
    public string resizeImgPersonalizado2(FileUpload controleFileUpload, Page requestThis, string alias, string orientacaoImg, int largura, int altura)
    {
        string retorno = "";

        var uploadedFile = controleFileUpload.PostedFile.FileName;
        var extractPos = uploadedFile.LastIndexOf("\\") + 1;


        //to retrieve only Filename from the complete path
        var uploadedFileName = uploadedFile.Substring(extractPos, uploadedFile.Length - extractPos);

        // Save uploaded file to server at the in the Pics folder
        controleFileUpload.PostedFile.SaveAs(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, alias, uploadedFileName));

        try
        {


            //We need to create a unique filename for each generated image
            var myDate = DateTime.Now;

            var myString = string.Format("{0}.png", myDate.AddSeconds(Convert.ToDouble(15)).ToString("ddMMyyhhmmss"));


            var ms = new MemoryStream((controleFileUpload).FileBytes);
            var map = System.Drawing.Image.FromStream(ms) as Bitmap;
            System.Drawing.Image thumbnail = new Bitmap(largura, altura);
            var graphic = Graphics.FromImage(thumbnail);
            if (map != null) graphic.DrawImage(map, 0, 0, largura, altura);
            thumbnail.Save(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, string.Format("{0}//Thumb//", alias), myString), ImageFormat.Png);

            retorno = myString;
        }
        catch (Exception erro)
        {
            string form = requestThis.Request.Url.AbsoluteUri;
            form = form.Substring(form.LastIndexOf("/") + 1);
            dados.CapturaErro(form, erro.Message, erro.ToString());

        }
        finally
        {
            if (dados.Conn.State == System.Data.ConnectionState.Open)
            {
                dados.Conn.Close();
            }
        }

        return retorno;
    }

    /// <summary>
    /// Classe responsavel por realizar a o upload da imagem e re dedimenciona-la
    /// </summary>
    /// <param name="controleFileUpload">Informe o controle file upload como neste exemplo: FileUpload1.PostedFile.FileName</param>
    /// <param name="requestThis">informe somente a palavra reservada this</param>
    /// <param name="alias">Caminho para o salvamento da imagem</param>
    /// <param name="orientacaoImg">Informar P (paisagem 200x149), A (Avatar 200x200) ou R (retrato 149x200)</param>
    /// <param name="nmImagem">Informe o nome da imagem com extensão</param>
    /// <returns></returns>
    public bool resizeImgMiniatura(FileUpload controleFileUpload, Page requestThis, string alias, string orientacaoImg, string nmImagem)
    {
        bool retorno = true;

        try
        {
            var uploadedFile = controleFileUpload.PostedFile.FileName;

            // Save uploaded file to server at the in the Pics folder
            controleFileUpload.PostedFile.SaveAs(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, string.Format("{0}//", alias), nmImagem));

            //Read in the image filename whose thumbnail has to be created
            var imageUrl = nmImagem;

            //You may even specify a standard thumbnail size

            int imageWidth;
            int imageHeight;

            if (orientacaoImg == "P") // paisagem
            {
                imageWidth = 200;
                imageHeight = 149;
            }   
            else // Retrato
            {
                imageWidth = 149;
                imageHeight = 200;
            }


            if (imageUrl.IndexOf("/") >= 0 || imageUrl.IndexOf("\\") >= 0)
            {
                requestThis.Response.End();
            }

            imageUrl = string.Format("{0}/{1}", alias, imageUrl);

            var fullSizeImg = System.Drawing.Image.FromFile(requestThis.Request.PhysicalApplicationPath + imageUrl);

            var dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

            var thumbNailImg = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight, dummyCallBack, IntPtr.Zero);

            //Save the thumbnail in Png format. You may change it to a diff format with the ImageFormat property
            thumbNailImg.Save(string.Format("{0}{1}\\{2}", requestThis.Request.PhysicalApplicationPath, string.Format("{0}//Thumb//", alias), nmImagem), ImageFormat.Png);

            thumbNailImg.Dispose();
        }
        catch
        {
            retorno = false;
        }

        return retorno;
    }

    private bool ThumbnailCallback()
    {
        return false;
    }
}
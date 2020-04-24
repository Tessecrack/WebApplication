using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
namespace WebApplication2
{
    public partial class Default : System.Web.UI.Page
    {
        string guid = Guid.NewGuid().ToString();

        public Person Person;

        public MyPage TitlePage;
        public MyPage MainPage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Person = new Person();
                TitlePage = new MyPage();
                MainPage = new MyPage();
            }
        }
        protected void ButtonAccept_Click(object sender, EventArgs e) // ПРИНЯТЬ
        {
            UserInputMainInformation();
            UserInputSocialNetwork();
            ViewState["person"] = Person;
        }
        protected void ButtonUpload_Click(object sender, EventArgs e) // Загрузить изображения титульной страницы
        {
            UploadImage(FileUploadBackgroundTitlePageImage, "title", "background");
            UploadImage(FileUploadFormTitlePageImage, "title", "form");
            ViewState["titlePageImages"] = TitlePage;
        }
        protected void ButtonUploadMainPage_Click(object sender, EventArgs e) // Загрузить изображения основной страницы
        {
            UploadImage(FileUploadBackgroundMainPageImage, "main", "background");
            UploadImage(FileUploadFormMainPageImage, "main", "form");
            ViewState["mainPageImages"] = MainPage;
        }
        protected void ButtonCreateTitlePageImage_Click(object sender, EventArgs e) // Создание титульной страницы
        {
            var title = HelpToCreateImage("titlePageImages", ListOfColorForTitleForm, ListOfPositionForTitleForm);
            if (title == null) return;
            var path = Server.MapPath("Images\\");
            var imageName = "TitlePage" + guid + ".jpg";
            title.Save(path + imageName);
            ResultTitlePage.ImageUrl = "Images\\" + imageName;
            ViewState["BookTitlePage"] = title;
            ViewState["pathBookTitlePage"] = imageName;
        }
        protected void ButtonCreateMainPage_Click(object sender, EventArgs e) // Создание основной страницы
        {
            var main = HelpToCreateImage("mainPageImages", DropDownListForMainFormColor, DropDownListForPositionMainForm);
            if (main == null) return;
            var path = Server.MapPath("Images\\");
            var imageName = "MainPage" + guid + ".jpg";
            main.Save(path + imageName);
            ResultMainPage.ImageUrl = "Images\\" + imageName;
            ViewState["BookMainPage"] = main;
            ViewState["pathBookMainPage"] = imageName;
        }
        protected void ButtonDownloadTitleImage_Click(object sender, EventArgs e) // Скачать изображение титульной страницы
        {
            if (!(ViewState["pathBookTitlePage"] is string pathTitle)) return;
            DownloadImage(pathTitle, "BookTitle.jpg");
        }

        protected void ButtonDownloadMainImage_Click(object sender, EventArgs e) // Скачать изображение основной страницы
        {
            if (!(ViewState["pathBookMainPage"] is string pathMain)) return;
            DownloadImage(pathMain, "BookMain.jpg");
        }
        protected void ButtonCreateBook_Click(object sender, EventArgs e) // создание ежедневника
        {
            if (!(ViewState["BookTitlePage"] is Bitmap title)) return;
            if (!(ViewState["BookMainPage"] is Bitmap main)) return;
            if (title == null || main == null) return;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Book.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var sizePage = GetSizePage();
            Document document = new Document();
            PdfWriter.GetInstance(document, Response.OutputStream);

            document.Open();
            iTextSharp.text.Image pictureTitle = iTextSharp.text.Image.GetInstance(title, System.Drawing.Imaging.ImageFormat.Jpeg);
            iTextSharp.text.Image pictureMain = iTextSharp.text.Image.GetInstance(main, System.Drawing.Imaging.ImageFormat.Jpeg);

            pictureTitle.SetAbsolutePosition(0, 0);
            pictureTitle.ScaleAbsolute(sizePage.Width, sizePage.Height);
            document.Add(pictureTitle);

            document.NewPage();
            pictureMain.SetAbsolutePosition(0, 0);
            pictureMain.ScaleAbsolute(sizePage.Width, sizePage.Height);
            document.Add(pictureMain);

            Response.Write(document);
            document.Close();
            Response.End();
        }
        /*-------------------------------------------------------------------------*/
        private Bitmap HelpToCreateImage(string nameViewState, DropDownList listColor, DropDownList listPosition)
        {
            var obj = ViewState[nameViewState];
            if (!(obj is MyPage images)) return null;
            if (nameViewState == "titlePageImages")
            {
                if (ViewState["TitleBackgroundImage"] is Bitmap back && images.BackgroundImage == null)
                    images.BackgroundImage = new Bitmap(back);
                if (ViewState["TitleFormImage"] is Bitmap form && images.FormImage == null)
                    images.FormImage = new Bitmap(form);
                if (ViewState["extensionTitle"] is string extension && images.ExtensionFormImage == null)
                    images.ExtensionFormImage = extension;
            }
            if (nameViewState == "mainPageImages")
            {
                if (ViewState["MainBackgroundImage"] is Bitmap back && images.BackgroundImage == null)
                    images.BackgroundImage = new Bitmap(back);
                if (ViewState["MainFormImage"] is Bitmap form && images.FormImage == null)
                    images.FormImage = new Bitmap(form);
                if (ViewState["extensionMain"] is string extension && images.ExtensionFormImage == null)
                    images.ExtensionFormImage = extension;
            }
            Bitmap resultImage;
            int coordinateX = images.BackgroundImage.Width / 2, coordinateY = images.BackgroundImage.Height / 2;
            if (images.ExtensionFormImage == ".png")
            {
                CalculatePositionFormPNG(images.BackgroundImage, images.FormImage, ref coordinateX, ref coordinateY, listPosition);
                resultImage = images.OverlayForPNG(coordinateX, coordinateY);
            }
            else
            {
                var binary = images.BinarizationImageFormJPG();
                var colorImage = images.ColorizationImage(binary, ColorChoice(listColor));
                CalculatePositionForm(images.BackgroundImage, images.FormImage, ref coordinateX, ref coordinateY, listPosition);
                resultImage = images.OverlayForJPG(colorImage, coordinateX, coordinateY);
            }
            return resultImage;
        }
        private void UploadImage(FileUpload fileUpload, string pageName, string typeImage)
        {
            if (fileUpload.HasFile)
            {
                string extension = System.IO.Path.GetExtension(fileUpload.FileName);
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    var imageName = typeImage + pageName + guid + fileUpload.FileName;
                    var path = Server.MapPath("Images\\");
                    fileUpload.SaveAs(Server.MapPath("Images\\") + imageName);
                    if (pageName.ToLower() == "title")
                    {
                        if (typeImage.ToLower() == "background")
                        {
                            TitlePage.BackgroundImage = new Bitmap(path + imageName);
                            ImageTitlePage.ImageUrl = "Images\\" + imageName;
                            ViewState["TitleBackgroundImage"] = new Bitmap(TitlePage.BackgroundImage);
                            return;
                        }
                        if (typeImage.ToLower() == "form")
                        {
                            TitlePage.FormImage = new Bitmap(path + imageName);
                            FormTitlePage.ImageUrl = "Images\\" + imageName;
                            TitlePage.ExtensionFormImage = extension;

                            ViewState["TitleFormImage"] = new Bitmap(TitlePage.FormImage);
                            ViewState["extensionTitle"] = extension;
                            return;
                        }
                    }
                    if (pageName.ToLower() == "main")
                    {
                        if (typeImage.ToLower() == "background")
                        {
                            MainPage.BackgroundImage = new Bitmap(path + imageName);
                            ImageMainPage.ImageUrl = "Images\\" + imageName;

                            ViewState["MainBackgroundImage"] = new Bitmap(MainPage.BackgroundImage);
                            return;
                        }
                        if (typeImage.ToLower() == "form")
                        {
                            MainPage.FormImage = new Bitmap(path + imageName);
                            FormMainPage.ImageUrl = "Images\\" + imageName;
                            MainPage.ExtensionFormImage = extension;

                            ViewState["MainFormImage"] = new Bitmap(MainPage.FormImage);
                            ViewState["extensionMain"] = extension;
                            return;
                        }
                    }
                }
            }
        }
        private void DownloadImage(string path, string pageName)
        {
            Response.ContentType = "image/jpeg";
            Response.AppendHeader("Content-Disposition", "attachment; filename= " + pageName);
            Response.TransmitFile(Server.MapPath(@"~\Images\" + path));
            Response.End();
        }
        private void UserInputMainInformation()
        {
            if (TextBoxName.Text != "")
            {
                var parseLine = TextBoxName.Text.Split(' ');
                if (parseLine.Length == 3)
                {
                    Person.LastName = parseLine[0];
                    Person.FirstName = parseLine[1];
                    Person.MiddleName = parseLine[2];
                }
            }
            if (TextBoxBirthDay.Text != "")
                Person.DateOfBirth = TextBoxBirthDay.Text;
        }
        private void UserInputSocialNetwork()
        {
            Person.SocialNetworks = new List<SocialNetwork>();
            if (TextBoxInstagram.Text != "") Person.SocialNetworks.Add(new SocialNetwork("Instagram", TextBoxInstagram.Text));
            if (TextBoxVkontakte.Text != "") Person.SocialNetworks.Add(new SocialNetwork("Vkontakte", TextBoxVkontakte.Text));
            if (TextBoxTelegram.Text != "") Person.SocialNetworks.Add(new SocialNetwork("Telegram", TextBoxTelegram.Text));
            if (TextBoxTwitter.Text != "") Person.SocialNetworks.Add(new SocialNetwork("Twitter", TextBoxTwitter.Text));
        }
        private iTextSharp.text.Rectangle GetSizePage()
        {
            switch (ListOfFormatPage.SelectedIndex)
            {
                case 0: return iTextSharp.text.PageSize.A0;
                case 1: return iTextSharp.text.PageSize.A1;
                case 2: return iTextSharp.text.PageSize.A2;
                case 3: return iTextSharp.text.PageSize.A3;
                case 4: return iTextSharp.text.PageSize.A4;
                case 5: return iTextSharp.text.PageSize.A5;
            }
            return iTextSharp.text.PageSize.A4;
        }
        private Color ColorChoice(DropDownList dropDownList)
        {
            switch (dropDownList.SelectedIndex)
            {
                case 0: return Color.Black;
                case 1: return Color.White;
                case 2: return Color.Red;
                case 3: return Color.Green;
                case 4: return Color.Blue;
                case 5: return Color.Yellow;
                case 6: return Color.BlueViolet;
            }
            throw new Exception();
        }
        private void CalculatePositionFormPNG(Bitmap back, Bitmap form, ref int coordinateX, ref int coordinateY, DropDownList dropDownList)
        {
            switch (dropDownList.SelectedIndex)
            {
                case 0: coordinateX = 1; coordinateY = 1; break;
                case 1: coordinateX = back.Width / 2 - form.Width / 2; coordinateY = 1; break;
                case 2: coordinateX = back.Width - form.Width - 1; coordinateY = 1; break;
                case 3: coordinateX = 1; coordinateY = back.Height / 2 - form.Height / 2; break;
                case 4: coordinateX = back.Width / 2 - form.Width / 2; coordinateY = back.Height / 2 - form.Height / 2; break;
                case 5: coordinateX = back.Width - form.Width - 1; coordinateY = back.Height / 2 - form.Height / 2; break;
                case 6: coordinateX = 1; coordinateY = back.Height - form.Height; break;
                case 7: coordinateX = back.Width / 2 - form.Width / 2; coordinateY = back.Height - form.Height - 1; break;
                case 8: coordinateX = back.Width - form.Width; coordinateY = back.Height - form.Height; break;
            }
        }
        private void CalculatePositionForm(Bitmap back, Bitmap form, ref int coordinateX, ref int coordinateY, DropDownList dropDownList)
        {
            switch (dropDownList.SelectedIndex)
            {
                case 0: coordinateX = form.Width / 2; coordinateY = form.Height / 2; break;
                case 1: coordinateX = back.Width / 2; coordinateY = form.Height / 2; break;
                case 2: coordinateX = back.Width - (form.Width / 2) - 1; coordinateY = form.Height / 2; break;
                case 3: coordinateX = form.Width / 2; coordinateY = back.Height / 2; break;
                case 4: coordinateX = back.Width / 2; coordinateY = back.Height / 2; break;
                case 5: coordinateX = back.Width - (form.Width / 2) - 1; coordinateY = back.Height / 2; break;
                case 6: coordinateX = form.Width / 2; coordinateY = back.Height - (form.Height / 2) - 1; break;
                case 7: coordinateX = back.Width / 2; coordinateY = back.Height - (form.Height / 2) - 1; break;
                case 8: coordinateX = back.Width - (form.Width / 2) - 1; coordinateY = back.Height - (form.Height / 2) - 1; break;
            }
        }
        protected void FileUploadTitlePageImage_Load(object sender, EventArgs e) { }
        protected void FileUploadMainPageImage_Load(object sender, EventArgs e) { }
        protected void FileUploadFormMainPageImage_Load(object sender, EventArgs e) { }
    }
} 
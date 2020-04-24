<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .text {
            font-style: normal;
            font-family: Verdana;
            color: indigo;
        }

        .textbox {
            padding: 2px;
            margin-bottom: 1%;
            font-size: 15px;
            width: 180px;
            border-width: 1px;
            border-color: #f700f4;
            background-color: #ffffff;
            color: #640e61;
            border-style: hidden;
            border-radius: 50px;
            box-shadow: 5px 0px 5px rgba(66,66,66,.75);
            text-shadow: 0px 0px 15px rgba(235,27,208,.75);
        }

        .button {
            display: inline-block;
            color: white;
            text-decoration: none;
            padding: 10px;
            outline: none;
            border-width: 2px 0;
            border-style: solid none;
            border-color: darkmagenta #000 darkmagenta;
            border-radius: 6px;
            background: linear-gradient(180deg, darkviolet, darkviolet);
            transition: 0.2s;
        }

            .button:hover {
                background: linear-gradient(180deg, blueviolet, darkmagenta);
            }

            .button:active {
                background: linear-gradient(180deg, darkmagenta,blueviolet);
            }

        html, body {
            height: 100%;
            background: linear-gradient(45deg, lightcoral, blueviolet) no-repeat;
            background-attachment: fixed;
        }

        .header {
            height: 80px;
            width: 97%;
            padding-top: 5px;
            padding-left: 5px;
            position: fixed;
            top: 0;
            background: linear-gradient(to bottom left,darkorchid, darkmagenta);
            border-width: 5px;
            border-color: violet;
            border-style: double;
            font-size: 60px;
        }

        .main {
            padding-top: 100px;
            height: 1px;
        }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <div class="header">
            <div style="float: left; margin-left: 10%">
                <asp:Label ID="NameOfSite" Text="RoomDay" runat="server" Font-Names="Kunstler Script" ForeColor="#FF66FF"></asp:Label>
            </div>
            <div style="float: right; margin-right: 10%">
                <asp:Button ID="ButtonMainMenu" Text="Главное меню" runat="server" CssClass="button"></asp:Button>
                <asp:Button ID="ButtonInfoProgram" Text="О программе" runat="server" CssClass="button"></asp:Button>
                <asp:Button ID="LinkDeveloperButton" Text="Связаться" runat="server" CssClass="button"></asp:Button>
                <asp:Button ID="HelpButton" Text="Помощь" runat="server" CssClass="button"></asp:Button>
            </div>
        </div>
        <div class="main">
            <div style="margin-left: 20%; margin-right: 20%; border: solid; border-color: darkmagenta; border-width: 1px">
                <h1>
                    <asp:Label ID="LabelInformationFirstBlock" Text="Заполните информацию о себе" runat="server" CssClass="text"></asp:Label>
                </h1>
                <br />
                <div style="text-align:center; font-size: x-large;" class="text">
                    Основное<br />
                </div>
                <div style="text-align: center">
                    <asp:TextBox ID="TextBoxName" Text="Ф.И.О" runat="server" CssClass="textbox"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxBirthDay" Text="Дата рождения" runat="server" CssClass="textbox"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxEmail" Text="Email" runat="server" CssClass="textbox"></asp:TextBox><br />
                    <br />
                </div>
                <div style="text-align:center">
                    <asp:Label ID="LabelSocialNetworkInfo" Text="Социальные сети " runat="server" Font-Size="X-Large" CssClass="text"></asp:Label>
                    <br />
                </div>
                <div style="text-align: center">
                    <asp:TextBox ID="TextBoxInstagram" Text="Instagram" runat="server" CssClass="textbox"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxVkontakte" Text="Вконтакте" runat="server" CssClass="textbox"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxTelegram" Text="Телеграм" runat="server" CssClass="textbox"></asp:TextBox><br />
                    <asp:TextBox ID="TextBoxTwitter" Text="Twitter" runat="server" CssClass="textbox"></asp:TextBox><br />
                </div>
                <div style="text-align: center; margin-top: 15%;">
                    <asp:Button ID="ButtonAccept" Text="Принять" runat="server" CssClass="button" OnClick="ButtonAccept_Click"></asp:Button>
                </div>
            </div>
            <div style="margin-left: 10%; margin-right: 10%; margin-top: 2%; margin-bottom: 2%; border: solid; border-color: darkmagenta; border-width: 1px" class="text">
                <h1>Изображение титульной страницы</h1>
                <div style="text-align: center;">
                    <div style="float: left; margin-left: 10%; text-align: center">
                        <asp:Label ID="Label2" Text="Выберите фоновое изображение" runat="server" Font-Size="Large" CssClass="text"></asp:Label>
                        <br />
                        <asp:FileUpload ID="FileUploadBackgroundTitlePageImage" runat="server" OnLoad="FileUploadTitlePageImage_Load"></asp:FileUpload>
                    </div>
                    <div style="float: left; margin-left: 3%; text-align: center">
                        <asp:Label ID="Label4" Text="Выберите форму заполнения" runat="server" Font-Size="Large"></asp:Label>
                        <br />
                        <asp:FileUpload ID="FileUploadFormTitlePageImage" runat="server" OnLoad="FileUploadTitlePageImage_Load"></asp:FileUpload>
                    </div>
                    <div style="float: left; margin-left: 2%">
                        <asp:Button ID="ButtonUpload" Text="Загрузить изображения" runat="server" CssClass="button" OnClick="ButtonUpload_Click"></asp:Button>
                    </div>
                </div>
                <br />
                <div style="margin-left: 3%; margin-top: 5%; text-align: center; margin-bottom: 1%">
                    <asp:Image ID="ImageTitlePage" Width="250px" Height="250px" runat="server" ImageAlign="Middle"></asp:Image>
                    <asp:Image ID="FormTitlePage" Width="250px" Height="250px" runat="server" ImageAlign="Middle" />
                    <asp:Image ID="ResultTitlePage" Width="250px" Height="250px" runat="server" ImageAlign="Middle" />
                </div>
                <div style="text-align: center; margin-bottom: 2%;">
                    <asp:Button ID="ButtonCreateTitlePageImage" Text="Создать титульную страницу" runat="server" CssClass="button" OnClick="ButtonCreateTitlePageImage_Click" />
                    <asp:Label ID="LabelPosition" Text="Позиция:" runat="server" Font-Size="Large"></asp:Label>
                    <asp:DropDownList ID="ListOfPositionForTitleForm" runat="server">
                        <asp:ListItem>Верх-слева</asp:ListItem>
                        <asp:ListItem>Верх-центр</asp:ListItem>
                        <asp:ListItem>Верх-справа</asp:ListItem>
                        <asp:ListItem>Центр-слева</asp:ListItem>
                        <asp:ListItem>Центр-центр</asp:ListItem>
                        <asp:ListItem>Центр-справа</asp:ListItem>
                        <asp:ListItem>Низ-слева</asp:ListItem>
                        <asp:ListItem>Низ-центр</asp:ListItem>
                        <asp:ListItem>Низ-справа</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label5" Text="Цвет:" runat="server" Font-Size="Large"></asp:Label>
                    <asp:DropDownList ID="ListOfColorForTitleForm" runat="server">
                        <asp:ListItem>Черный</asp:ListItem>
                        <asp:ListItem>Белый</asp:ListItem>
                        <asp:ListItem>Красный</asp:ListItem>
                        <asp:ListItem>Зеленый</asp:ListItem>
                        <asp:ListItem>Синий</asp:ListItem>
                        <asp:ListItem>Желтый</asp:ListItem>
                        <asp:ListItem>Фиолетовый</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="ButtonDownloadTitleImage" Text="Скачать изображение" runat="server" CssClass="button" OnClick="ButtonDownloadTitleImage_Click" />
                </div>
            </div>
            <div style="margin-left: 10%; margin-right: 10%; margin-top: 2%; margin-bottom: 2%; border: solid; border-color: darkmagenta; border-width: 1px" class="text">
                <h1>Изображение основной страницы</h1>
                <div style="text-align: center;">
                    <div style="float: left; margin-left: 10%; text-align: center">
                        <asp:Label ID="Label3" Text="Выберите фоновое изображение" runat="server" Font-Size="Large"></asp:Label>
                        <br />
                        <asp:FileUpload ID="FileUploadBackgroundMainPageImage" runat="server" OnLoad="FileUploadMainPageImage_Load"></asp:FileUpload>
                    </div>
                    <div style="float: left; margin-left: 3%; text-align: center">
                        <asp:Label ID="Label6" Text="Выберите форму заполнения" runat="server" Font-Size="Large"></asp:Label>
                        <br />
                        <asp:FileUpload ID="FileUploadFormMainPageImage" runat="server" OnLoad="FileUploadFormMainPageImage_Load"></asp:FileUpload>
                    </div>
                    <div style="float: left; margin-left: 2%">
                        <asp:Button ID="ButtonUploadMain" Text="Загрузить изображения" runat="server" CssClass="button" OnClick="ButtonUploadMainPage_Click"></asp:Button>
                    </div>
                </div>
                <br />
                <div style="margin-left: 3%; margin-top: 5%; text-align: center; margin-bottom: 1%">
                    <asp:Image ID="ImageMainPage" Width="250px" Height="250px" runat="server" ImageAlign="Middle"></asp:Image>
                    <asp:Image ID="FormMainPage" Width="250px" Height="250px" runat="server" ImageAlign="Middle" />
                    <asp:Image ID="ResultMainPage" Width="250px" Height="250px" runat="server" ImageAlign="Middle" />
                </div>
                <div style="text-align: center; margin-bottom: 2%;">
                    <asp:Button ID="ButtonCreateMainPage" Text="Создать основную страницу" runat="server" CssClass="button" OnClick="ButtonCreateMainPage_Click" />
                    <asp:Label ID="Label7" Text="Позиция:" runat="server" Font-Size="Large"></asp:Label>
                    <asp:DropDownList ID="DropDownListForPositionMainForm" runat="server">
                        <asp:ListItem>Верх-слева</asp:ListItem>
                        <asp:ListItem>Верх-центр</asp:ListItem>
                        <asp:ListItem>Верх-справа</asp:ListItem>
                        <asp:ListItem>Центр-слева</asp:ListItem>
                        <asp:ListItem>Центр-центр</asp:ListItem>
                        <asp:ListItem>Центр-справа</asp:ListItem>
                        <asp:ListItem>Низ-слева</asp:ListItem>
                        <asp:ListItem>Низ-центр</asp:ListItem>
                        <asp:ListItem>Низ-справа</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label8" Text="Цвет:" runat="server" Font-Size="Large"></asp:Label>
                    <asp:DropDownList ID="DropDownListForMainFormColor" runat="server">
                        <asp:ListItem>Черный</asp:ListItem>
                        <asp:ListItem>Белый</asp:ListItem>
                        <asp:ListItem>Красный</asp:ListItem>
                        <asp:ListItem>Зеленый</asp:ListItem>
                        <asp:ListItem>Синий</asp:ListItem>
                        <asp:ListItem>Желтый</asp:ListItem>
                        <asp:ListItem>Фиолетовый</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="ButtonDownloadMainImage" Text="Скачать изображение" runat="server" CssClass="button" OnClick="ButtonDownloadMainImage_Click" />
                </div>
            </div>
            <div style="margin-left: 30%; margin-right: 30%; margin-top: 1%; margin-bottom: 5%; border: solid; border-color: darkmagenta; border-width: 1px" class="text">
                <h2>Создание ежедневника</h2>
                <div style="text-align: center">
                    <asp:Button ID="ButtonCreateBook" Text="Скачать книгу" runat="server" CssClass="button" OnClick="ButtonCreateBook_Click" />
                    Формат страниц
                    <asp:DropDownList ID="ListOfFormatPage" runat="server">
                        <asp:ListItem>A0</asp:ListItem>
                        <asp:ListItem>A1</asp:ListItem>
                        <asp:ListItem>A2</asp:ListItem>
                        <asp:ListItem>A3</asp:ListItem>
                        <asp:ListItem>A4</asp:ListItem>
                        <asp:ListItem>A5</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

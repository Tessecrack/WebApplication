using System;
using System.Drawing;

[Serializable]
public class MyPage
{
    private Color colorForm;
    private int limit = 150;

    public string ExtensionFormImage;
    public Bitmap BackgroundImage;
    public Bitmap FormImage;
    public MyPage()
    {
        colorForm = Color.Black;
    }
    public Bitmap OverlayForPNG(int coordinateX, int coordinateY)
    {
        if (FormImage == null) return BackgroundImage;
        var width = FormImage.Width;
        var height = FormImage.Height;

        var overlayImage = new Bitmap(BackgroundImage);
        using (var graphic = Graphics.FromImage(overlayImage))
        {
            graphic.DrawImage(FormImage, new Rectangle(coordinateX, coordinateY, width, height));
        }
        return overlayImage;
    }
    public Bitmap OverlayForJPG(Bitmap form, int coordinateX, int coordinateY)
    {
        if (FormImage == null) return BackgroundImage;
        var beginX = coordinateX - form.Width / 2;
        var beginY = coordinateY - form.Height / 2;
        var endX = coordinateX + form.Width / 2;
        var endY = coordinateY + form.Height / 2;
        CheckSizesBorderImage(BackgroundImage.Width, BackgroundImage.Height, form.Width, form.Height, ref beginX, ref beginY, ref endX, ref endY);

        var image = new Bitmap(BackgroundImage);
        int sideX = 0, sideY = 0;
        for (int y = beginY; y < endY; y++, sideY++)
        {
            for (int x = beginX; x < endX; x++, sideX++)
            {
                var pixelForm = form.GetPixel(sideX, sideY);
                var pixelBack = BackgroundImage.GetPixel(x, y);
                if (pixelForm.R == colorForm.R || pixelForm.G == colorForm.G || pixelForm.B == colorForm.B)
                    image.SetPixel(x, y, pixelForm);
                else
                    image.SetPixel(x, y, pixelBack);
            }
            sideX = 0;
        }
        return image;
    }

    private void CheckSizesBorderImage(int backgroundWidth, int backgroundHeight, int formWidth, int formHeight,
        ref int beginX, ref int beginY, ref int endX, ref int endY)
    {
        if (beginX < 0) { beginX = 0; endX = formWidth; }
        if (beginY < 0) { beginY = 0; endY = formHeight; }
        if (endX >= backgroundWidth) { beginX = backgroundWidth - formWidth; endX = backgroundWidth; }
        if (endY >= backgroundHeight) { beginY = backgroundHeight - formHeight; endY = backgroundHeight; }
    }

    public Bitmap BinarizationImageFormJPG()
    {
        var width = FormImage.Width;
        var height = FormImage.Height;
        var form = new Bitmap(FormImage);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var color = FormImage.GetPixel(x, y);
                var average = (color.R + color.G + color.B) / 3;
                var newColor = average > limit ? Color.FromArgb(250, 250, 250) : Color.FromArgb(0, 0, 0);
                form.SetPixel(x, y, newColor);
            }
        }
        return form;
    }
    public Bitmap ColorizationImage(Bitmap binary, Color color)
    {
        colorForm = color;
        var form = new Bitmap(binary);
        for (var y = 0; y < binary.Height; y++)
        {
            for (var x = 0; x < binary.Width; x++)
            {
                var pixel = form.GetPixel(x, y);
                if (pixel.R < limit) form.SetPixel(x, y, color);
                else form.SetPixel(x, y, pixel);
            }
        }
        return form;
    }
}
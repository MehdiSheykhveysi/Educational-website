namespace Site.Web.Infrastructures.Interfaces
{
    public interface IImageResizer
    {
        void Resizing(string FilePathResizing, string SavePathAfterResize, int newWidth);
    }
}
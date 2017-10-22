using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public struct FileFieldNames
    {
        public const string FileName = "Dosya Adı";
        public const string Type = "Dosya Tipi";
    }

    public static class FileTypes
    {
        public static string GetFileType(string extension)
        {
            switch (extension)
            {
                case "jpg":
                    return "JPEG Resim";
                case "png":
                    return "PNG Resim";
                case "gif":
                    return "GIF Resim";
                case "doc":
                    return "MS Word Dokümanı";
                case "docx":
                    return "Word Dokümanı";
                case "xls":
                    return "MX Excel Dokümanı";
                case "xlsx":
                    return "Excel Dokümanı";
                case "pdf":
                    return "PDF Dokümanı";
                case "zip":
                    return "ZIP Arşivi";
                case "rar":
                    return "RAR Arşivi";
                case "mp4":
                    return "MP4 Video";
                case "3gp":
                    return "3GP Video";
                case "wma":
                    return "WMA Video";
                case "flv":
                    return "FLV Video";
                default:
                    return "Bilinmeyen Tip";
            }
        }

        public static string GetFileIcon(string extension)
        {
            switch (extension)
            {
                case "jpg":
                case "png":
                case "gif":
                    return "file-image-o";
                case "doc":
                case "docx":
                    return "file-word-o";
                case "xls":
                case "xlsx":
                    return "file-excel-o";
                case "pdf":
                    return "file-pdf-o";
                case "zip":
                case "rar":
                    return "file-archive-o";
                case "mp4":
                case "3gp":
                case "wma":
                case "flv":
                    return "file-video-o";
                default:
                    return "file-o";
            }
        }
    }

    public class FileListModel
    {
        [Display(Name = FileFieldNames.FileName)]
        public string FileName { get; set; }
        [Display(Name = FileFieldNames.Type)]
        public string Type { get; set; }
        public string Icon { get; set; }

        public static FileListModel Import(string fileName, string ext)
        {
            ext = ext.Replace(".", "");
            return new FileListModel
            {
                FileName = fileName,
                Type = FileTypes.GetFileType(ext),
                Icon = FileTypes.GetFileIcon(ext)
            };
        }
    }
}
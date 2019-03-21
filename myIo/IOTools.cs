using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


public class IOTools
{

    public static List<FileInfo> DirInfo(string MyFiles)
    {
        DirEx(MyFiles);

        DirectoryInfo MyDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(MyFiles));

        List<FileInfo> MyFileList = new List<FileInfo>();

        foreach (var Files in MyDir.GetFiles())
        {
            MyFileList.Add(Files);
        }

        return MyFileList;
    }


    public static string FileUplader(string MyPath, HttpPostedFileBase MyFile, string[] FileEx)
    {
        string Msg = "";

        string MyFileName = Path.GetFileName(MyFile.FileName);
        string MyFileEx = Path.GetExtension(MyFile.FileName.ToLower());

        DirEx(MyPath);

        if (FileEx.Contains(MyFileEx))
        {
            Guid FileId = Guid.NewGuid();
            MyFile.SaveAs(HttpContext.Current.Server.MapPath(MyPath) + "/" + FileId.ToString() + "_" + MyFileName);

            Msg = "Ok";
        }
        else
        {
            Msg = "Forkert filformat";
        }

        return Msg;
    }

    public static string DelFile(string MyFilenameAndPath)
    {
        string Msg = "";

        File.Delete(HttpContext.Current.Server.MapPath(MyFilenameAndPath));
        Msg = "Filen blev slettet";

        return Msg;    
    }

    public static void DirEx(string MyPath)
    {
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(MyPath)))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(MyPath));
        }
    }
}

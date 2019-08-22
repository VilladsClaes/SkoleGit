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

        /*         
         De tre argumenter kan være: 
            MyViewModel.Msg = IOTools.FileUplader("~/Uploads", MyFile, Ex);
            MyViewModel.MyFileInfo = IOTools.DirInfo("~/Uploads");
            string[] Ex = { ".jpg", ".png", ".gif" };             
         */


        string Msg = "";

        string MyFileName = Path.GetFileName(MyFile.FileName);
        //Tving filformaterne til at være skrevet med små bogstaver
        string MyFileEx = Path.GetExtension(MyFile.FileName.ToLower());

        DirEx(MyPath);

        //Hvis et af de tilladte fil-formater er at finde i filen, så 
        if (FileEx.Contains(MyFileEx))
        {
            Guid FileId = Guid.NewGuid(); //Navngiver filerne med et unikt navn for at undgå at billederne hedder det samme.
            MyFile.SaveAs(HttpContext.Current.Server.MapPath(MyPath) + "/" + FileId.ToString() + "_" + MyFileName);

            Msg = "Ok";
        }
        //Hvis filformatet ikke er i FileEx (det array som står i Fil-controlleren fx string[] Ex = { ".jpg", ".png", ".gif" }
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

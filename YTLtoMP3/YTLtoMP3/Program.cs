using MediaToolkit.Model;
using MediaToolkit;
using VideoLibrary;
using System.Diagnostics;

internal class Program
{
    delegate void SaveMP3File(string directory, string webUrl, string outputName);

    private static void Main()
    {
        MainProgramExecution();
    }


    static void MainProgramExecution()
    {
        string? VideoURL = null;
        string? VideoSaveDirectory = null;
        string? OutputFileName = null;

        SaveMP3File saveMP3 = new(SaveMP3); //Using delegate for no apparent reason.

        ReadInputForURL(ref VideoURL);

        ReadInputForSaveDirectory(ref VideoSaveDirectory);

        ReadInputForFileName(ref OutputFileName);

        if (VideoSaveDirectory != null && VideoURL != null && OutputFileName != null)
            saveMP3(VideoSaveDirectory, VideoURL, OutputFileName);
        else
        {
            Console.WriteLine("\n A parameter was missing, restarting application. \n");
            MainProgramExecution();
        }
    }


    static void ReadInputForFileName(ref string? FileName)
    {
        Console.WriteLine("\n What would you like this file to be named? \n");

        while (FileName == null)
        {
            FileName = Console.ReadLine(); //Get the output file name.
        }
    }


    static void ReadInputForURL(ref string? URL)
    {
        Console.WriteLine("\n Enter the URL of the youtube video you'd like to download to MP4 format *Right Click to Paste* \n");
        while (URL == null || URL.Contains("youtube") == false) //While the URL is empty, or the URL provided isn't a youtube link.
        {
            URL = Console.ReadLine();
        }
    }


    static void ReadInputForSaveDirectory(ref string? SaveDirectory)
    {

        Console.WriteLine("\n Enter the directory on your computer you'd like to save to *e.g C:\\DirectoryName*\n");

        while (SaveDirectory == null || SaveDirectory.Contains(":") == false)
        {
            SaveDirectory = Console.ReadLine(); //Waits for the input of the user to specify which directory they'd like to put the file in.

            if (Directory.Exists(SaveDirectory) == false && SaveDirectory != null) //If the folder doesn't already exist.
            {
                Directory.CreateDirectory(SaveDirectory); //Create the directory.
                Console.WriteLine($"\n Created New Directory - {SaveDirectory} \n ");
            }
        }
    }


    static void SaveMP3(string SaveToFolder, string VideoURL, string MP3Name)
    {
        string source = SaveToFolder;
        var vid = YouTube.Default.GetVideo(VideoURL);
        string? videopath = Path.Combine(source, vid.FullName);

        Console.WriteLine("\n Downloading Video... *This may a bit* \n ");

        File.WriteAllBytes(videopath, vid.GetBytes());

        Console.WriteLine("\n Video Downloaded. \n");

        var inputFile = new MediaFile { Filename = Path.Combine(source, vid.FullName) };
        var outputFile = new MediaFile { Filename = Path.Combine(source, $"{MP3Name}.mp3") };

        Console.WriteLine("\n Complete, File Created At - " + SaveToFolder + "/" + MP3Name + ".mp3 \n");
    }
}
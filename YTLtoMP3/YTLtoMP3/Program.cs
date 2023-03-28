// See https://aka.ms/new-console-template for more information
using MediaToolkit.Model;
using MediaToolkit;
using VideoLibrary;

static void SaveMP3(string SaveToFolder, string VideoURL, string MP3Name)
{
    string source = SaveToFolder;
    var youtube = YouTube.Default;
    var vid = youtube.GetVideo(VideoURL);
    Console.WriteLine("Retrieving Video");
    string videopath = Path.Combine(source, vid.FullName);
    Console.WriteLine("Downloading Video... *This may a minute or two..*");
    File.WriteAllBytes(videopath, vid.GetBytes());
    Console.WriteLine("Video Downloaded.");

    var inputFile = new MediaFile { Filename = Path.Combine(source, vid.FullName) };
    Console.WriteLine("Finalizing.");
    var outputFile = new MediaFile { Filename = Path.Combine(source, $"{MP3Name}.mp3") };

    Console.WriteLine("Complete.");


}

Console.WriteLine("Enter the URL of the youtube video you'd like to download to MP4 format *Right Click to Paste* \n");
string url = "";
string SaveToFolder = "";

while(url == "" || url.Contains("youtube") == false)
{
    url = Console.ReadLine();
    
}

Console.WriteLine("Enter the directory on your computer you'd like to save to *e.g C:\\SaveTest*\n");
while(SaveToFolder == "")
{
    SaveToFolder = Console.ReadLine();
    Console.WriteLine("\n Downloading video to " + SaveToFolder + " ...");
}

if(SaveToFolder != null && url != null)
SaveMP3(SaveToFolder, url, "file");





using Syroot.Windows.IO;
using YoutubeDLSharp;
using JsonFormatterPlus;
using System.Text.RegularExpressions;

var url = "https://www.youtube.com/watch?v=_p6CMcJwtgk";
var url2 = "https://www.youtube.com/watch?v=bsm271h5gIc";
var url3 = "https://www.youtube.com/watch?v=JdLCEwEFCMU";
var url4 = "https://www.youtube.com/playlist?list=PLaoCst0DYyP1E7ZtqPGU26RYG7cd5eAr2";

var ytdl = new YoutubeDL
{
    YoutubeDLPath = "yt-dlp.exe",
    FFmpegPath = "ffmpeg.exe",
    OutputFolder = KnownFolders.Downloads.Path
};

/* 
 * Relevant attributes for downloading a youtube video:
 * 
 * 1. User inputs their link in edit field
 * 2. Click button to extract information about said link
 *  . (Verify it is a valid link)
 *  . regex http(?:s?):\/\/(?:www\.)?youtu(?:be\.com\/watch\?v=|\.be\/)([\w\-\_]*)(&(amp;)?‌​[\w\?‌​=]*)?
 *  . (Distinguish between video and playlist)
 * 3. Present the information in a concise manner
 *  . (Title) 
 *  . (Thumbnail 480x360) How do I display thumbnail?
 *  . (Duration)
 *  . (Available Formats)
 * 4. Allow the user to choose in which format to download the video
 * 5. Download the file into the default Downloads folder
 *  . (Display progress, display in a list-like manner)
 *  . (Or set it manually)
 * 6. Use persistance to store operation data and progress
 */

Console.WriteLine(File.Exists(ytdl.FFmpegPath));
Console.WriteLine(File.Exists(ytdl.FFmpegPath));

//JsonFormatter.Format(JsonConvert.SerializeObject( object )));
var res = isYoutubeLink(url) ? await ytdl.RunVideoDataFetch(url) : null;
var res2 = isYoutubeLink(url) ? await ytdl.RunVideoDataFetch(url2) : null;
var res3 = isYoutubeLink(url) ? await ytdl.RunVideoDataFetch(url3) : null;
var res4 = isYoutubeLink(url) ? await ytdl.RunVideoDataFetch(url4) : null;

//debug(res);
//debug(res2);
//debug(res3);

debug(res4);

static bool isYoutubeLink(string link)
{
    return new Regex(@"^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube(-nocookie)?\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$", 
        RegexOptions.IgnoreCase | RegexOptions.Multiline).IsMatch(link);
}

static void debug(RunResult<YoutubeDLSharp.Metadata.VideoData>? res)
{
    if (res is null)
    {
        Console.WriteLine("Link is not valid.");
        return;
    }

    Console.WriteLine("===========================================");
    foreach (var item in res.Data.Formats)
        Console.WriteLine(item);

    foreach (var item in res.Data.Thumbnails)
        Console.WriteLine($"{item.Preference} {item.Resolution}");
}

// await ytdl.RunAudioDownload("https://www.youtube.com/watch?v=OozUwqWbXzc", AudioConversionFormat.Mp3);
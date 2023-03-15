using Syroot.Windows.IO;
using YoutubeDLSharp;
using JsonFormatterPlus;
using System.Text.RegularExpressions;

var ytdl = new YoutubeDL
{
    YoutubeDLPath = "yt-dlp.exe",
    FFmpegPath = "ffmpeg.exe",
    OutputFolder = KnownFolders.Downloads.Path
};

// FFmpeg and YoutubeDL executables must be present in our resources
if (!File.Exists(ytdl.FFmpegPath))
{
    Console.WriteLine("Downloading ffmpeg.exe to folder...");
    await YoutubeDL.DownloadFFmpegBinary();
}

if (!File.Exists(ytdl.YoutubeDLPath))
{
    Console.WriteLine("Downloading yt-dlp.exe to folder...");
    await YoutubeDL.DownloadYtDlpBinary();
}

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

// Normal videos
var url = "https://www.youtube.com/watch?v=_p6CMcJwtgk";
var url2 = "https://www.youtube.com/watch?v=bCQb-m0RaG0";

// Very old video
var url3 = "https://www.youtube.com/watch?v=JdLCEwEFCMU";

// Private playlist
var url4 = "https://www.youtube.com/playlist?list=PLaoCst0DYyP1E7ZtqPGU26RYG7cd5eAr2";

// Public playlist
var url5 = "https://www.youtube.com/playlist?list=PLzSjbEiFKZ_w9zWXjVSTLi5FUlcPHwQCc";

// Livestream
var url6 = "https://www.youtube.com/watch?v=jfKfPfyJRdk";

// Shorts
var url7 = "https://www.youtube.com/shorts/eGIE2tNcuWQ";

var video1 = isYoutubeLink(url) ? await ytdl.RunVideoDataFetch(url) : null;
var video2 = isYoutubeLink(url2) ? await ytdl.RunVideoDataFetch(url2) : null;
var old_video = isYoutubeLink(url3) ? await ytdl.RunVideoDataFetch(url3) : null;
var prv_playlist = isYoutubeLink(url4) ? await ytdl.RunVideoDataFetch(url4) : null;
var pub_playlist = isYoutubeLink(url5) ? await ytdl.RunVideoDataFetch(url5) : null;
var livestream = isYoutubeLink(url6) ? await ytdl.RunVideoDataFetch(url6) : null;
var ytshort = isYoutubeLink(url7) ? await ytdl.RunVideoDataFetch(url7) : null;

debug(video1);
debug(video2);
debug(old_video);
debug(prv_playlist);
debug(pub_playlist);
debug(livestream);
debug(ytshort);

static bool isYoutubeLink(string link)
{
    return new Regex(@"^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube(-nocookie)?\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$", 
        RegexOptions.IgnoreCase | RegexOptions.Multiline).IsMatch(link);
}

static void debug(RunResult<YoutubeDLSharp.Metadata.VideoData>? res)
{
    Console.WriteLine("===========================================");

    if (res is null || !res.Success)
    {
        Console.WriteLine("Link is not valid.");
        return;
    }

    if (res.Data.IsLive ?? false)
    {
        Console.WriteLine("Link is a livestream...");
        return;
    }

    Console.WriteLine(res.Data.Title);
    Console.WriteLine(res.Data.ResultType);
}

// await ytdl.RunAudioDownload("https://www.youtube.com/watch?v=OozUwqWbXzc", AudioConversionFormat.Mp3);
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syroot.Windows.IO;
using YoutubeDLSharp;

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

namespace YAY2MP3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}

using ASM.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Music : Page
    {
        private ObservableCollection<Song> _ListSong;
        internal ObservableCollection<Song> ListSong { get => _ListSong; set => _ListSong = value; }

        public Music()
        {
            this.ListSong = new ObservableCollection<Song>();
            this.ListSong.Add(new Song()
            {
                name = "Nam Ay",
                singer = "Duc Phuc",
                photo_album = "https://avatar-nct.nixcdn.com/song/2017/12/09/1/f/7/3/1512816233582_640.jpg",
                mp3link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui955/NamAy-DucPhuc-5305026.mp3"
            });

            this.ListSong.Add(new Song()
            {
                name = "Phia Sau Mot Co Gai",
                singer = "Soobin Hoang Son",
                photo_album = "https://i.ytimg.com/vi/j__Q13iAxNk/maxresdefault.jpg",
                mp3link = "https://vnno-zn-5-tf-mp3-s1-zmp3.zadn.vn/a32e305cab1842461b09/4870257449214198184?authen=exp=1539911644~acl=/a32e305cab1842461b09/*~hmac=21acf7f284b39039a56d4606cc3023b6&filename=Phia-Sau-Mot-Co-Gai-Soobin-Hoang-Son.mp3"
            });

            this.ListSong.Add(new Song()
            {
                name = "Tung Ngay Em Mo Ve Anh",
                singer = "Soobin Hoang Son ft Mlee",
                photo_album = "https://i.ytimg.com/vi/ITDsa_n_IGo/maxresdefault.jpg",
                mp3link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui926/TungNgayEmMoVeAnh-MleeSoobinHoangSon-4523836.mp3"
            });

            this.ListSong.Add(new Song()
            {
                name = "Lang (Mua Dong Di Qua)",
                singer = "JSOL",
                photo_album = "https://i.ytimg.com/vi/nlXKe-YnX0o/maxresdefault.jpg",
                mp3link = "https://vnno-zn-5-tf-mp3-s1-zmp3.zadn.vn/e290183bc37f2a21736e/6928327941221174386?authen=exp=1539911802~acl=/e290183bc37f2a21736e/*~hmac=1f644d13a2e3593e6454b5d77d5e9ac5&filename=Lang-JSOL.mp3"
            });

            this.InitializeComponent();
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            Song tapped_song = panel.Tag as Song;
            this.MediaPLayer.Source = new Uri(tapped_song.mp3link);
            this.NowPlaying.Text = tapped_song.name + " - " + tapped_song.singer;
            this.MediaPLayer.AutoPlay = true;
        }
    }
}

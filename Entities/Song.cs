using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.Entities
{
    class Song
    {
        private string _name;
        private string _singer;
        private string _photo_album;
        private string _mp3link;

        public string name { get => _name; set => _name = value; }
        public string singer { get => _singer; set => _singer = value; }
        public string photo_album { get => _photo_album; set => _photo_album = value; }
        public string mp3link { get => _mp3link; set => _mp3link = value; }
    }
}

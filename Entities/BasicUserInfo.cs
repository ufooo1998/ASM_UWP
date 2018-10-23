using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.Entities
{
    class BasicUserInfo
    {
        private string _token;
        private string _secretToken;
        private string _userId;
        private string _createdTimeMLS;
        private string _expiredTimeMLS;
        private int _status;

        public string token { get => _token; set => _token = value; }
        public string secretToken { get => _secretToken; set => _secretToken = value; }
        public string userId { get => _userId; set => _userId = value; }
        public string createdTimeMLS { get => _createdTimeMLS; set => _createdTimeMLS = value; }
        public string expiredTimeMLS { get => _expiredTimeMLS; set => _expiredTimeMLS = value; }
        public int status { get => _status; set => _status = value; }
    }
}

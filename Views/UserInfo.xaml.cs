using ASM.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http.Headers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserInfo : Page
    {
        public UserInfo()
        {
            UserData();
            this.InitializeComponent();
        }
        public async void UserData()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("CurrentUser.txt");
            string jsonCurrentUser = await FileIO.ReadTextAsync(sampleFile);
            BasicUserInfo currentUser = JsonConvert.DeserializeObject<BasicUserInfo>(jsonCurrentUser);
            
            Debug.WriteLine(currentUser.token);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", currentUser.token);
            var response = httpClient.GetAsync(Sevices.Link.API_GET_INFO);
            var contents = await response.Result.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
            Member memberInfo = JsonConvert.DeserializeObject<Member>(contents);
            Debug.WriteLine(memberInfo.firstName);

        }
    }
}

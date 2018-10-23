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
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Account : Page
    {
        private Member currentMember;
        private LoginMember currentLogin;
        private StorageFile photo;
        private string currentUploadUrl;
        private object contentFrame;

        public Account()
        {
            ShowInfo();
            this.currentLogin = new LoginMember();
            this.currentMember = new Member();
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        
        public async void ShowInfo()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("CurrentUser.txt");
            string jsonCurrentUser = await FileIO.ReadTextAsync(sampleFile);
            BasicUserInfo currentUser = JsonConvert.DeserializeObject<BasicUserInfo>(jsonCurrentUser);
            if (currentUser != null)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", currentUser.token);
                var response = httpClient.GetAsync(Sevices.Link.API_GET_INFO);
                var contents = await response.Result.Content.ReadAsStringAsync();
                Member memberInfo = JsonConvert.DeserializeObject<Member>(contents);
                UserName.Text = memberInfo.firstName + " " + memberInfo.lastName;
                UserEmail.Text = memberInfo.email;
                UserAddress.Text = memberInfo.address;
                UserBirthday.Text = memberInfo.birthday;
                UserPhone.Text = memberInfo.phone;
                Uri avatarUri = new Uri(memberInfo.avatar, UriKind.Absolute);
                BitmapImage bmImage = new BitmapImage(avatarUri);
                UserAvatar.Source = bmImage;
                //UserGender.Text = memberInfo.gender.ToString();
                UserIntroduction.Text = memberInfo.introduction;
            }
        }

        // Get Gender
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            this.currentMember.gender = Int32.Parse(radio.Tag.ToString());
        }
        // Get birthday
        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (sender.Date < DateTime.Now)
            {
                this.currentMember.birthday = sender.Date.Value.ToString("yyyy-MM-dd");
                this.ResponseDate.Visibility = Visibility.Collapsed;
            }
            else
            {
                sender.Date = DateTime.Today;
                this.ResponseDate.Text = "* Invalid birthday";
                this.ResponseDate.Visibility = Visibility.Visible;
            };
        }

        // Reset form
        private void Do_Reset(object sender, RoutedEventArgs e)
        {
            FirstName.Text = string.Empty;
            LastName.Text = string.Empty;
            AvatarUrl.Text = string.Empty;
            Phone.Text = string.Empty;
            Email.Text = string.Empty;
            Password.Password = string.Empty;
            Address.Text = string.Empty;
            Introduction.Text = string.Empty;
        }

        private async void Take_Photo(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (this.photo == null)
            {
                // User cancelled photo capture
                return;
            }
            HttpClient httpClient = new HttpClient();
            currentUploadUrl = await httpClient.GetStringAsync(Sevices.Link.API_GET_URL);
            Debug.WriteLine("Upload url: " + currentUploadUrl);
            HttpUploadFile(currentUploadUrl, "myFile", "image/png");
        }

        private async void Choose_Photo(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Users expect to have a filtered view of their folders 
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            // Open the picker for the user to pick a file
            this.photo = await openPicker.PickSingleFileAsync();
            HttpClient httpClient = new HttpClient();
            currentUploadUrl = await httpClient.GetStringAsync(Sevices.Link.API_GET_URL);
            Debug.WriteLine("Upload url: " + currentUploadUrl);
            HttpUploadFile(currentUploadUrl, "myFile", "image/png");
        }

        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                string imageUrl = reader2.ReadToEnd();
                Avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                AvatarUrl.Text = imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }
        // Register
        private async void Do_Register(object sender, RoutedEventArgs e)
        {
            int x = 0;
            if (Regex.IsMatch(FirstName.Text, "[a-zA-Z]")) {
                currentMember.firstName = this.FirstName.Text;
                firstName.Visibility = Visibility.Collapsed;
                x += 1;
            } else {
                firstName.Text = "* Invalid name";
                firstName.Visibility = Visibility.Visible;
            }

            if (Regex.IsMatch(LastName.Text, "[a-zA-Z]"))
            {
                currentMember.lastName = this.LastName.Text;
                lastName.Visibility = Visibility.Collapsed;
                x += 1;
            }
            else
            {
                lastName.Text = "* Invalid name";
                lastName.Visibility = Visibility.Visible;
            }

            if (AvatarUrl.Text != "")
            {
                this.currentMember.avatar = this.AvatarUrl.Text;
                this.avatar.Visibility = Visibility.Collapsed;
                x += 1;
            }
            else
            {
                avatar.Text = "* Invalid Url";
                avatar.Visibility = Visibility.Visible;

            };


            if (Regex.IsMatch(Phone.Text, "[0-9]") && (Phone.Text.Length == 10))
            {
                currentMember.phone = this.Phone.Text;
                phone.Visibility = Visibility.Collapsed;
                x += 1;
            }
            else
            {
                phone.Visibility = Visibility.Visible;
                phone.Text = "* Invalid Phone Number";
            };

            if (Regex.IsMatch(Email.Text,"[@]") && Regex.IsMatch(Email.Text, "[.]"))
            {
                this.currentMember.email = this.Email.Text;
                this.email.Visibility = Visibility.Collapsed;
                x += 1;
            }
            else
            {
                this.email.Text = "* Invalid email";
                this.email.Visibility = Visibility.Visible;
            };


            if (Password.Password != "")
            {
                this.currentMember.password = this.Password.Password;
                this.password.Visibility = Visibility.Collapsed;
                x += 1;
            }
            else
            {
                password.Text = "* Invalid password";
                password.Visibility = Visibility.Visible;

            };

            if (Address.Text != "")
            {
                currentMember.address = this.Address.Text; ;
                address.Visibility = Visibility.Collapsed;
                x += 1;
            }
            else
            {
                address.Text = "* Address is required";
                address.Visibility = Visibility.Visible;

            };

            this.currentMember.introduction = Introduction.Text;

            if (x == 7) {
                string jsonMember = JsonConvert.SerializeObject(this.currentMember);
                Debug.WriteLine(jsonMember);
                HttpClient httpClient = new HttpClient();
                var content = new StringContent(jsonMember, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(Sevices.Link.API_REGISTER, content);
                var contents = await response.Result.Content.ReadAsStringAsync();
                Debug.WriteLine(contents);
                Debug.WriteLine(response.Result.StatusCode);
                if (response.Result.StatusCode != HttpStatusCode.Created)
                {
                    ErrorResponse responseRegister = JsonConvert.DeserializeObject<ErrorResponse>(contents);
                    if (responseRegister.error.Count > 0)
                    {
                        foreach (var key in responseRegister.error.Keys)
                        {
                            if (this.FindName(key) is TextBlock textBlock)
                            {
                                email.Text = "*" + responseRegister.error["email"];
                                email.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
                else {
                    RegisterNontification.Text = "Register Susscess!";
                }
            }
            else {
                RegisterNontification.Text = "Register Failed!";
            }
        }
        //Login
        private async void Do_Login(object sender, RoutedEventArgs e)
        {
            if (LoginEmail.Text != "" && LoginPassword.Password != "")
            {
                ResponseLogin.Visibility = Visibility.Collapsed;
                string jsonLoginString = JsonConvert.SerializeObject(this.currentLogin);
                Debug.WriteLine(jsonLoginString);
                HttpClient httpClient = new HttpClient();
                var content = new StringContent(jsonLoginString, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(Sevices.Link.API_LOGIN, content);
                var contents = await response.Result.Content.ReadAsStringAsync();
                Debug.WriteLine(contents);
                if (response.Result.StatusCode != HttpStatusCode.Created)
                {
                    ErrorResponse responseRegister = JsonConvert.DeserializeObject<ErrorResponse>(contents);
                    if (responseRegister.error.Count > 0)
                    {
                        ResponseLogin.Text = "* " + responseRegister.message;
                        ResponseLogin.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile sampleFile = await storageFolder.CreateFileAsync("CurrentUser.txt",CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(sampleFile, contents);
                    this.UserInfo.Visibility = Visibility.Visible;
                    this.LoginPanel.Visibility = Visibility.Collapsed;
                    this.RegisterPanel.Visibility = Visibility.Collapsed;
                }
            }
            else {
                this.ResponseLogin.Text = "* Invalid email or password";
                ResponseLogin.Visibility = Visibility.Visible;
            }
        }
        private void Do_Reset_Login(object sender, RoutedEventArgs e)
        {
            this.LoginEmail.Text = string.Empty;
            this.LoginPassword.Password = string.Empty;
        }
    }
}

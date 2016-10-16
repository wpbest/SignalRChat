using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SignalRChat
{
    public partial class LoginPage : ContentPage
    {
        private int? _selectedColor;

        public LoginPage()
        {
            InitializeComponent();
        }

        public void LoginBtnClicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            if (string.IsNullOrEmpty(name))
            {
                DisplayAlert("Enpty name", "Enter your name to start chatting", "Ok");
                return;
            }
            if (!_selectedColor.HasValue)
            {
                DisplayAlert("Choose your color", "Select the color of your messages", "Ok");
                return;
            }

            var chatPage = new ChatPage(name, _selectedColor.Value);
            Application.Current.MainPage = chatPage;
        }

        public void ColorBtnClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            _selectedColor = int.Parse(btn.CommandParameter.ToString());
            btn.BorderWidth = 2;
            btn.BorderColor = Color.Accent;
        }
    }
}

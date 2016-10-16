using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRChat
{
	public class ChatViewModel : BaseViewModel
	{
		private HubConnection _connection;
		private IHubProxy _proxy;

		private const string hubUrl = "http://wpbestchat.azurewebsites.net"; //Change this URL to yours

		private readonly string _name;
		private readonly int _color;

		private string _message;
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		private ObservableCollection<MessageModel> _messages;
		public ObservableCollection<MessageModel> Messages
		{
			get { return _messages; }
			set { SetProperty(ref _messages, value); }
		}

		private ICommand _sendMessageCommand;
		public ICommand SendMessageCommand => _sendMessageCommand ??
											  (_sendMessageCommand = new Command(async () => await SendMessage(), () => true));

		public ChatViewModel(string name, int color)
		{
			_name = name;
			_color = color;
			_messages = new ObservableCollection<MessageModel>();
			InitConnection();
		}

		private async Task SendMessage()
		{
			if (!string.IsNullOrEmpty(Message))
			{
				await _proxy.Invoke("Send", new object[] { _name, _color, Message });
				Message = string.Empty;
			}
		}

		private async void InitConnection()
		{
			_connection = new HubConnection(hubUrl);
			_proxy = _connection.CreateHubProxy("ChatHub");

			_proxy.On<string, int, string>("broadcastMessage", (username, selectedColor, message) =>
			{
				var color = string.Empty;
				switch (selectedColor)
				{
					case 0:
						color = "Aqua";
						break;
					case 1:
						color = "#A2FFB0";
						break;
					default:
						color = "#FFD9C4";
						break;
				}
				var receivedMessage = new MessageModel
				{
					Name = username,
					Message = message,
					Color = color
				};

				if (username.Equals(_name, StringComparison.Ordinal))
					receivedMessage.Pad = new Thickness(60, 5, 0, 5);
				else
					receivedMessage.Pad = new Thickness(0, 5, 60, 5);

                Device.BeginInvokeOnMainThread(() =>
                {
                    Messages.Add(receivedMessage);
                });
			});

			try
			{
				await _connection.Start();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}


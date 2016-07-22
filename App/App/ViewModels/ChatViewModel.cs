using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using App.Models;
using Xamarin.Forms;

namespace App.ViewModels
{
	public class ChatViewModel : INotifyPropertyChanged
	{
		private string _name;
		private string _message;
		private ChatHub _hub;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				OnPropertyChanged();
			}
		}

		public ICommand SubmitLoginCommand { get; }
		public ICommand SubmitLogoutCommand { get; }
		public ICommand SubmitMessageCommand { get; }


		public ObservableCollection<string> MessageList { get; } = new ObservableCollection<string>();

		public ChatViewModel()
		{
			_hub = new ChatHub("http://localhost:5000/");
			SubmitLoginCommand = new Command(() =>
			  {
				  _hub.Login(Name);
			  });

			SubmitLogoutCommand = new Command(() =>
			  {
				  _hub.Logout(Name);
			  });

			SubmitMessageCommand = new Command(() =>
			  {
				  _hub.Send(Name, Message);
				  Message = "";
			  });

			_hub.Recieved += (sender, args) => { MessageList.Add(args.DataList.FirstOrDefault()?.ToString()); };
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
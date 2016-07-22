using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace App.Models
{
	public class ChatHub:IDisposable
	{
		private readonly HubConnection _connection;
		private readonly IHubProxy _proxy;
		private bool _disposed;

		public ChatHub(string url)
		{
			_connection=new HubConnection(url);

			_proxy = _connection.CreateHubProxy("chat");

			_proxy.On("Recieve", (string message) => OnRecieved(message));

			_connection.Start();
		}

		public event EventHandler<RecievedEventArgs> Recieved;

		public void Login(string name) => _proxy.Invoke("Login", name);

		public void Send(string name,string message) => _proxy.Invoke("Send", name,message);

		public void Logout(string name) => _proxy.Invoke("Logout", name);

		protected void OnRecieved(string message) => Recieved?.Invoke(this,new RecievedEventArgs(message));

		public void Dispose()
		{
			if(_disposed)return;

			_connection.Dispose();
		}
	}

	public class RecievedEventArgs : EventArgs
	{
		public List<object> DataList { get; }

		public RecievedEventArgs(object data1)
		{
			DataList=new List<object>()
			{
				{data1},
			};
		}
	}
}
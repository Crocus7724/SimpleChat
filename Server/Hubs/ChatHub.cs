using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Server.Hubs
{
	[HubName("chat")]
	public class ChatHub:Hub
	{
		public void Send(string name,string message)
		{
			Clients.All.Recieve($"{name} : {message}");
		}

		public void Login(string name)
		{
			Clients.All.Recieve($"{name} がログインしました。");
		}

		public void Logout(string name)
		{
			Clients.All.Recieve($"{name}がログアウトしました。");
		}
	}
}
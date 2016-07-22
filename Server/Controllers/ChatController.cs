using Microsoft.AspNet.Mvc;

namespace Server.Controllers
{
	public class ChatController:Controller
	{
		public IActionResult Index() => View();
	}
}
using Microsoft.AspNetCore.Mvc;
using Milestone.Models;

namespace Milestone.Controllers
{
    public class MinesweeperController : Controller
    {
        static List<ButtonModel> buttons = new List<ButtonModel>();
        const int GRID_SIZE = 25;

        public IActionResult Index()
        {
            buttons = new List<ButtonModel>();

            if (buttons.Count < GRID_SIZE)
            {
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    buttons.Add(new ButtonModel { Id = i, ButtonState = 0 });
                }
            }
            return View("Index", buttons);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bn = int.Parse(buttonNumber);
            buttons.ElementAt(bn).ButtonState = (buttons.ElementAt(bn).ButtonState + 1) % 5;
            return View("Index", buttons);
        }
    }
}

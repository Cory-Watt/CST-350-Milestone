using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System.Drawing;

namespace Milestone.Controllers
{
    public class MinesweeperController : Controller
    {
        static List<ButtonModel> buttons = new List<ButtonModel>();
        const int GRID_SIZE = 25;
        const int BOMB_SIZE = 5;
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
            setupLiveNeighbors();
            return View("Index", buttons);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bn = int.Parse(buttonNumber);
            buttons.ElementAt(bn).ButtonState = (buttons.ElementAt(bn).ButtonState + 1) % 5;
            return View("Index", buttons);
        }

        //THis method will set up the board with random live members (w/ bombs)
        public void setupLiveNeighbors()
        {
            //Calculate number of bombs and convert to int
            int count = 0;
            //Set random live cells
            while (count < BOMB_SIZE)
            {
                //Get Random Postion
                Random random = new Random();
                int randNum = random.Next(0, 25);
                //Link cell to new refernce
                //Check if postion is not already live
                if (buttons.ElementAt(randNum).Live == false)
                {
                    // live property to true
                   buttons.ElementAt(randNum).Live = true;
                    count++;
                }
                else
                {
                    continue;
                }
            }
        }

    }
}

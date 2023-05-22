using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System.Drawing;
using System.Security.Policy;

namespace Milestone.Controllers
{
    public class MinesweeperController : Controller
    {
        static List<ButtonModel> buttons = new List<ButtonModel>();
        const int GRID_SIZE = 25;
        const int BOMB_SIZE = 5;
        const int ROW_SIZE = 5;
        public ButtonModel[,] grid { get; set; }
        public IActionResult Index()
        {
            //buttons = new List<ButtonModel>();
            grid = new ButtonModel[5, 5];
            //Will loop thorugh the row
            int id = 0;
            for (int r = 0; r < 5; r++)
            {
                //will loop through the coulmns of the above row
                for (int c = 0; c < 5; c++)
                {
                    // create new cell object and add in every postinon in the grid
                    ButtonModel button = new ButtonModel(id,0);
                    grid[r, c] = button;
                    id++;
                }
            }
            setupLiveNeighbors();
            calculateLiveNeigbors();
            buttons = grid.Cast<ButtonModel>().ToList();
            return View("Index", buttons);
        }

        public IActionResult HandleButtonClick(int buttonNumber)
        {
                    buttons.ElementAt(buttonNumber).ButtonState++;
                
            
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
                int randRow = random.Next(0, 5);
                int randCol = random.Next(0, 5);
                //Link cell to new refernce
                ButtonModel button = grid[randRow, randCol];
                //Check if postion is not already live
                if (button.Live == false)
                {
                    // live property to true
                    button.Live = true;
                    count++;
                }
                else
                {
                    continue;
                }
            }
        }
        //Will calcualte the live neighbors for cells on the grid
        public void calculateLiveNeigbors()
        {
            int arrSize = ROW_SIZE - 1;

            //loop through the grid
            for (int r = 0; r < ROW_SIZE; r++)
            {
                for (int c = 0; c < ROW_SIZE; c++)
                {
                    int liveNeighbors = 0;
                    ButtonModel button = grid[r, c];
                    //Check If cell has down neighbor
                    if (r != arrSize)
                    {
                        ButtonModel down = grid[r + 1, c];
                        if (down.Live)
                        {
                            liveNeighbors++;
                        }
                    }
                    //check if cell has up neighbor
                    if (r != 0)
                    {
                        ButtonModel up = grid[r - 1, c];
                        if (up.Live)
                        {
                            liveNeighbors++;
                        }
                    }
                    //check if cell has right neighbor
                    if (c != ROW_SIZE - 1)
                    {
                        ButtonModel right = grid[r, c + 1];
                        if (right.Live)
                        {
                            liveNeighbors++;
                        }
                    }
                    //check if cell has left neighbor
                    if (c != 0)
                    {
                        ButtonModel left = grid[r, c - 1];
                        if (left.Live)
                        {

                            liveNeighbors++;
                        }
                    }
                    //check if cell has topleft neighbor
                    if (r != 0 && c != 0)
                    {
                        ButtonModel topleft = grid[r - 1, c - 1];
                        if (topleft.Live)
                        {
                            liveNeighbors++;
                        }
                    }
                    //check if cell has bottom right neighbor
                    if (r != ROW_SIZE - 1 && c != ROW_SIZE - 1)
                    {
                        ButtonModel bottomright = grid[r + 1, c + 1];
                        if (bottomright.Live)
                        {
                            liveNeighbors++;
                        }
                    }
                    //check if cell has top right neighbor
                    if (r != 0 && c != ROW_SIZE - 1)
                    {
                        ButtonModel topRight = grid[r - 1, c + 1];
                        if (topRight.Live)
                        {
                            liveNeighbors++;
                        }
                    }
                    //check if cell has bottom left neighbor
                    if (r != ROW_SIZE - 1 && c != 0)
                    {
                        ButtonModel bottomleft = grid[r + 1, c - 1];
                        if (bottomleft.Live)
                        {
                            liveNeighbors++;
                        }
                    }
                    //if cell is live and has 8 neighbors, set to 9 live
                    if (button.Live && liveNeighbors == 8)
                    {
                        button.Neighbors = 9;
                    }
                    else
                    {
                        //set neighbor property
                        button.Neighbors = liveNeighbors;
                    }
                }
            }
        }

    }
}

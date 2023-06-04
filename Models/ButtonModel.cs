namespace Milestone.Models
{
    public class ButtonModel
    {
        public int Id { get; set; }
        public int ButtonState { get; set; }
        public bool Live { get; set; }
        int Row { get; set; }
        int Column { get; set; }
        public bool Visted { get; set; }
        public int Neighbors { get; set; }

        public ButtonModel() { }

        public ButtonModel(int id, int buttonState)
        {
            //Setting default values
            Id = id;
            Row = -1;
            Column = -1;
            Visted = false;
            Live = false;
            Neighbors = 0;
            ButtonState = buttonState;
        }
    }
}

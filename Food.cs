using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Simulator
{
    class Food
    {
        //This class is for generating food. The food is generated when the user clicked on the panel, and the amount of resources a good has is dictated here.
        //Food is also stored within an array.
        private int xCoord;
        private int yCoord;
        private int foodResource;

        public Food()
        {

        }

        public Food(int x, int y)
        {
            X = x;
            Y = y;
            foodResource = 30;
        }

        public int X
        {
            set
            {
                xCoord = value;
            }

            get { return xCoord; }
        }

        public int Y
        {
            set
            {
                yCoord = value;
            }

            get { return yCoord; }
        }

        public int FoodResource
        {
            set { foodResource = value; }
            get { return foodResource; }
        }
    }
}

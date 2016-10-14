using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Simulator
{
    class Nest
    {
        //This class is for generating the nests. Nest are generated with a position based on where the user clicked, and a number for assigning them to ants once an ant finds a nest.
        private int xCoord;
        private int yCoord;
        private int nestNumber;

        public Nest()
        {

        }

        public Nest(int x, int y)
        {
            X = x;
            Y = y;
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

        public int NestNumber
        {
            set
            {
                nestNumber = value;
            }

            get { return nestNumber; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Simulator
{
    class Ant
    {
        /*This class is for generating ants. The class includes all properties for the ants, so their location, targets, and booleans based on their context.
         * all ants are generated with this class and are stored within arrays when generated
         */
        private int xCoord;
        private int yCoord;
        private bool isTargetingFood;
        private bool isTargetingNest;
        private bool isCarryingFood;
        private int foodTargetX;
        private int foodTargetY;
        private int nestTargetX;
        private int nestTargetY;
        private bool isKnowsWhereFood;
        private bool isKnowsWhereNest;
        private int foodTarget;
        private int nestTarget;
        private int movementNo;
        
        //creates 'ghost' ant with no properties
        public Ant()
        {
            xCoord = 0;
            yCoord = 0; 
        }

        //creates ant based on coordinates
        public Ant(int x, int y, int rnd)
        {
            X = x;
            Y = y;
            IsTargetingFood = true;
            IsTargetingNest = false;
            IsCarryingFood = false;
            IsKnowsWhereFood = false;
            IsKnowsWhereNest = false;
            foodTargetX = 0;
            foodTargetY = 0;
            nestTargetX = 0;
            nestTargetY = 0;
            MovementNo = rnd;
        }

        public int X
        {
            set { xCoord = value; }

            get
            {
                return xCoord;
            }
        }       

        public int Y
        {
            set { yCoord = value; }

            get
            {
                return yCoord;
            }
        }


        public bool IsTargetingFood
        {
            set { isTargetingFood = value; }

            get
            {
                return isTargetingFood;
            }
        }

        public bool IsTargetingNest
        {
            set { isTargetingNest = value; }

            get
            {
                return isTargetingNest;
            }
        }

        public bool IsCarryingFood
        {
            set { isCarryingFood = value; }

            get
            {
                return isCarryingFood;
            }
        }

        public bool IsKnowsWhereFood
        {
            set { isKnowsWhereFood = value; }

            get
            {
                return isKnowsWhereFood;
            }
        }

        public bool IsKnowsWhereNest
        {
            set { isKnowsWhereNest = value; }

            get
            {
                return isKnowsWhereNest;
            }
        }

        public int FoodTarget
        {
            set { foodTarget = value; }

            get
            {
                return foodTarget;
            }
        }

        public int NestTarget
        {
            set { nestTarget = value; }

            get
            {
                return nestTarget;
            }
        }

        public int MovementNo
        {
            set { movementNo = value; }

            get
            {
                return movementNo;
            }
        }

        public int FoodX
        {
            set { foodTargetX = value; }
            get { return foodTargetX; }
        }

        public int FoodY
        {
            set { foodTargetY = value; }
            get { return foodTargetY; }
        }
        public int NestX
        {
            set { nestTargetX = value; }
            get { return nestTargetX; }
        }

        public int NestY
        {
            set { nestTargetY = value; }
            get { return nestTargetY; }
        }
    }
}

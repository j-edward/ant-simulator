using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ant_Simulator
{
    public partial class mainForm : Form
    {
        //Main class for all interaction and generation of ants/food/nests. All properties of the droppables & ants are generated and store by their respective classes.
        private Ant tempAnt;
        private Ant checkAnt;
        private Food tempFood;
        private Nest tempNest;
        private Ant[] antArray;
        private Ant[] antEnemyArray;
        private Food[] foodArray;
        private Nest[] nestArray;
        private Nest[] nestEnemyArray;
        private int worldWidth;
        private int worldHeight;
        private int movementPixels;
        private int antSize;
        private int foodSize;
        private int nestSize;
        private int numberOfAnts;
        private int numberOfEnemyAnts;
        private int targetRadiusFood;
        private int targetRadiusNest;
        private int transferRadius;
        private int transferRadiusAnt;
        private int foodCount;
        private int foodLimit;
        private int nestCount;
        private int nestEnemyCount;
        private int nestLimit;
        private int randomNumber;
        private int xCoordinate;
        private int yCoordinate;
        private int movementCount;
        private Bitmap worldImage;
        Random rnd = new Random();

        public mainForm()
        {
            InitializeComponent();

            numberOfAnts = 400;
            numberOfEnemyAnts = 200;
            worldWidth = 720;
            worldHeight = 650;
            movementPixels = 1;
            antSize = 3;
            foodSize = 8;
            nestSize = 10;
            targetRadiusFood = 50;
            targetRadiusNest = 50;
            transferRadiusAnt = 11;
            transferRadius = 2;
            foodLimit = 4;
            nestLimit = 2;
            foodCount = 0;
            nestCount = 0;
            nestEnemyCount = 0;
            movementCount = 0;
            randomNumber = rnd.Next(1, 9);
            checkAnt = new Ant();

            //bitmap image for drawing to (double buffering)
            worldImage = new Bitmap(worldWidth, worldHeight);

            antArray = new Ant[numberOfAnts];
            antEnemyArray = new Ant[numberOfAnts];

            foodArray = new Food[foodLimit];

            nestArray = new Nest[nestLimit];
            nestEnemyArray = new Nest[nestLimit];

            //sets combo box default selection to team A
            antTeamSelectionComboBox.SelectedIndex = 0;
            //force the combo box to be in a dropdown-style, thus stopping the user from entering their own value and potentially causing an error
            antTeamSelectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            //generate all ants (both friendly, and enemy)
            CreateAnts();

            //move the ants
            MoveAnts();

            //start the timer
            simpleTimer.Start();
        }


        private void CreateAnts()
        {
            //generates new ants in random locations, based on limit of ants
            for (int i = 0; i < numberOfAnts; i++)
            {
                //create random coordinates for the friendly ant
                xCoordinate = rnd.Next(1, worldImage.Width);
                yCoordinate = rnd.Next(1, worldImage.Height);

                //generates number to indicate which direction the ant should move in
                randomNumber = rnd.Next(1, 9);

                //creates new ant in a random location
                tempAnt = new Ant(xCoordinate, yCoordinate, randomNumber);

                //assigns each ant to the ENEMY ant array
                antArray[i] = tempAnt;
            }

            //generates new ENEMY ants in random locations, based on limit of ants
            for (int i = 0; i < numberOfEnemyAnts; i++)
            {
                //create random coordinates for the ENEMY ant
                xCoordinate = rnd.Next(1, worldImage.Width);
                yCoordinate = rnd.Next(1, worldImage.Height);

                //generates number to indicate which direction the ant should move in
                randomNumber = rnd.Next(1, 9);

                //creates new ENEMY ant in a random location
                tempAnt = new Ant(xCoordinate, yCoordinate, randomNumber);

                //assigns each ant to the ENEMY ant array
                antEnemyArray[i] = tempAnt;
            }
        }

        private void CreateFood(int x, int y)
        {
            //validation on whether food limit has been hit
            if (foodCount < foodLimit)
            {
                //creates new food based on user mouse coordinates when mouse is clicked
                tempFood = new Food(x, y);

                //assigns each food created to the food array
                foodArray[foodCount] = tempFood;
                foodCount++;
            }
            else
            {
                //simple message to warn user when the limit of food has been hit
                MessageBox.Show("Food limit exceeded!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CreateNest(int x, int y)
        {
            //depending on what selection on the 'antTeamSelectionComboBox', a nest is created for that faction
            if (Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "A")
            {
                if (nestCount < nestLimit)
                {
                    //creates new nest for the friendly ants
                    tempNest = new Nest(x, y);

                    //store nest
                    nestArray[nestCount] = tempNest;
                    nestCount++;
                }
                else
                {
                    //simple message to warn user when the limit of nest has been hit
                    MessageBox.Show("Nest limit exceeded!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            //based on team B being selected, enemy nest is created
            if (Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "B")
            {
                if (nestEnemyCount < nestLimit)
                {
                    //creates new nest to be stored in the enemy nest array
                    tempNest = new Nest(x, y);

                    //store nest
                    nestEnemyArray[nestEnemyCount] = tempNest;
                    nestEnemyCount++;
                }
                else
                {
                    //simple message to warn user when the limit of nest has been hit
                    MessageBox.Show("Nest limit exceeded!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void MoveAnts()
        {
            //if food has been placed, check the food still exists (has more than 0 resources), if it does not, the food should be deleted
            if (foodCount > 0)
            {
                CheckFoodExists();
            }

            //check each ant
            for (int i = 0; i < numberOfAnts; i++)
            {
                tempAnt = antArray[i];
                //if ant is targeting food but is not carrying food, and also has coordinates for the food, it should move to food
                if (tempAnt.IsTargetingFood && tempAnt.IsCarryingFood == false && tempAnt.IsKnowsWhereFood && foodCount > 0)
                {
                    MoveToFood(tempAnt);
                }

                //if the ant is targeting the nest & carrying food, while also knowing where the nest is, it should move to the nest
                else if (tempAnt.IsTargetingNest && tempAnt.IsCarryingFood && tempAnt.IsKnowsWhereNest)
                {
                    MoveToNest(tempAnt);
                }

                //in all other cases the ant should move randomly until it knows where either nest or food is located
                else
                {
                    MoveRandom(tempAnt);
                }

                //check the ants location incase of a change of behaviour eg. it finds a nest
                if (foodCount > 0)
                {
                    CheckAntTargetingFood(tempAnt);
                }
                if (nestCount > 0)
                {
                    CheckAntTargetingNest(tempAnt, nestArray, nestCount);
                }

                //passing ant to check against other ants near it, and whether it hears any information from a nearby ant
                AntCommunication(tempAnt, antArray);
            }

            //checking each enemy ant
            for (int i = 0; i < numberOfEnemyAnts; i++)
            {
                tempAnt = antEnemyArray[i];

                //if enemy ant is targeting food, knows where the food is, but isn't carrying any food - move to the targeted food
                //but only if food has been placed
                //'move to food' in this example slight differs from the normal ant above, where the food the enemy-
                //-ant is targeting is actually other friendly ants that are carrying food
                if (tempAnt.IsTargetingFood && tempAnt.IsCarryingFood == false && tempAnt.IsKnowsWhereFood && foodCount > 0)
                {
                    MoveToFood(tempAnt);
                }

                //if the enemy ant is targeting the nest and knows where the nest is, while still carrying food,
                //the ant should then move to the nest accordingly
                else if (tempAnt.IsTargetingNest && tempAnt.IsCarryingFood && tempAnt.IsKnowsWhereNest)
                {
                    MoveToNest(tempAnt);
                }
                //if neither of the above statements are true, the ant should move randomly 
                else
                {
                    MoveRandom(tempAnt);
                }

                //if an enemy nest has been placed, check whether the enemy ant should be targeting the nest
                //eg. if the nest is in target radius or even if the nest has been deleted and the ant should not be targeting it any longer
                if (nestEnemyCount > 0)
                {
                    CheckAntTargetingNest(tempAnt, nestEnemyArray, nestEnemyCount);
                }

                //check if any enemy anys are near, and can transfer infomation
                //if if an ant is near another friendly ant that is carrying food, so it can steal the food piece
                EnemyAntCommunication(tempAnt);
            }

        }

        //function used to move ants randomly on panel
        private void MoveRandom(Ant tempAnt)
        {
            //this delays the ant having a new number generated, so it employs a more 'momentum'-based movement behaviour, rather than 'wiggle' like an excited atom
            if (movementCount % 5 == 0)
            {
                tempAnt.MovementNo = rnd.Next(1, 9);
            }

            //switch-case to determine which direction the ant should move based on it's movement number
            //each number is assigned to a direction, starting from 1 - up (or north), working clockwise until 8 - diagonally (north west)
            //doing this ensures random movement in the panel
            switch (tempAnt.MovementNo)
            {
                case 1:
                    //up (north)
                    tempAnt.Y = tempAnt.Y - movementPixels;
                    break;
                case 2:
                    //diagonally-up-right (north east)
                    tempAnt.Y = tempAnt.Y - movementPixels;
                    tempAnt.X = tempAnt.X + movementPixels;
                    break;
                case 3:
                    //right (east)
                    tempAnt.X = tempAnt.X + movementPixels;
                    break;
                case 4:
                    //diagonally-down-right (south east)
                    tempAnt.Y = tempAnt.Y + movementPixels;
                    tempAnt.X = tempAnt.X + movementPixels;
                    break;
                case 5:
                    //down (south)
                    tempAnt.Y = tempAnt.Y + movementPixels;
                    break;
                case 6:
                    //diagonally-down-left (south west)
                    tempAnt.Y = tempAnt.Y + movementPixels;
                    tempAnt.X = tempAnt.X - movementPixels;
                    break;
                case 7:
                    //left (west)
                    tempAnt.X = tempAnt.X - movementPixels;
                    break;
                case 8:
                    //diagonally-up-left (north west)
                    tempAnt.Y = tempAnt.Y - movementPixels;
                    tempAnt.X = tempAnt.X - movementPixels;
                    break;
            }

            //ensures the world cycles round, so if ant goes off the screen it returns on the opposite side of the panel
            if (tempAnt.X >= worldImage.Width)
            {
                tempAnt.X = 0;
            }

            if (tempAnt.X < 0)
            {
                tempAnt.X = worldImage.Width;
            }

            if (tempAnt.Y >= worldImage.Height)
            {
                tempAnt.Y = 0;
            }

            if (tempAnt.Y < 0)
            {
                tempAnt.Y = worldImage.Height;
            }
        }

        //once an ant knows where the food is, it should move to the food
        private void MoveToFood(Ant tempAnt)
        {
            //if x coordinate of ant is greater than the food it's targeting, decrease the x value so it's closer
            if (tempAnt.X > tempAnt.FoodX)
            {
                tempAnt.X = tempAnt.X - movementPixels;
            }
            //if x coordinate of ant is smaller than the food it's targeting, increase the x value so it's closer
            else if (tempAnt.X < tempAnt.FoodX)
            {
                tempAnt.X += movementPixels;
            }

            //if y coordinate of ant is greater than the food it's targeting, decrease the y value so it's closer
            if (tempAnt.Y > tempAnt.FoodY)
            {
                tempAnt.Y = tempAnt.Y - movementPixels;
            }
            //if y coordinate of ant is smaller than the food it's targeting, decrease the y value so it's closer
            else if (tempAnt.Y < tempAnt.FoodY)
            {
                tempAnt.Y += movementPixels;
            }
        }

        //once the ant is carrying food and knows where a nest is, it should move to the nest
        private void MoveToNest(Ant tempAnt)
        {
            //if x coordinate of ant is smaller than the nest it's targeting, decrease the x value so it's closer
            if (tempAnt.X > tempAnt.NestX)
            {
                tempAnt.X = tempAnt.X - movementPixels;
            }
            //if x coordinate of ant is smaller than the nest it's targeting, decrease the x value so it's closer
            else if (tempAnt.X < tempAnt.NestX)
            {
                tempAnt.X += movementPixels;
            }

            //if y coordinate of ant is smaller than the nest it's targeting, decrease the y value so it's closer
            if (tempAnt.Y > tempAnt.NestY)
            {
                tempAnt.Y = tempAnt.Y - movementPixels;
            }
            //if y coordinate of ant is smaller than the nest it's targeting, decrease the y value so it's closer
            else if (tempAnt.Y < tempAnt.NestY)
            {
                tempAnt.Y += movementPixels;
            }
        }

        //function draws the ants on a bitmap, that is then drawn to the panel
        private void DisplayAll()
        {
            using (Graphics worldImageGraphics = Graphics.FromImage(worldImage))
            using (Brush antBrush = new SolidBrush(Color.Black))
            using (Brush antEnemyBrush = new SolidBrush(Color.Red))
            using (Brush foodBrush = new SolidBrush(Color.Gold))
            using (Brush nestBrush = new SolidBrush(Color.Gray))
            using (Brush highlighterNestAnt = new SolidBrush(Color.Green))
            using (Brush highlighterFoodAnt = new SolidBrush(Color.Blue))
            using (Pen foodOutlinePen = new Pen(Color.Gray))
            using (Pen nestOutlinePen = new Pen(Color.Black))
            using (Pen NestEnemyOutlinePen = new Pen(Color.Red))
            {
                worldImageGraphics.Clear(Color.LightGray);

                //draws ants on image
                for (int i = 0; i < numberOfAnts; i++)
                {
                    //find each ant in the ant array
                    tempAnt = antArray[i];

                    //find the xCoordinates of each ant, used for drawing them on the bitmap
                    xCoordinate = tempAnt.X;
                    yCoordinate = tempAnt.Y;

                    //if the highlight ants targeting nest is checked, and team A is selected, shows all ants on team A that know where the nest is (highlighted green)
                    if (highlightNestAntCheckBox.Checked && tempAnt.IsKnowsWhereNest && Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "A")
                    {
                        worldImageGraphics.FillRectangle(highlighterNestAnt, xCoordinate, yCoordinate, antSize, antSize);
                    }
                    else
                    {
                        //draws ants as generic black (unhighlighted; ignorant of all droppables)
                        worldImageGraphics.FillRectangle(antBrush, xCoordinate, yCoordinate, antSize + 1, antSize);
                    }

                    //show transfer radius of ants, thus how close they need to be to send infomation (only shows ants on selected team)
                    if (showAntCommsCheckBox.Checked && Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "A")
                    {
                        worldImageGraphics.DrawEllipse(foodOutlinePen, xCoordinate - (transferRadiusAnt / 2), yCoordinate - (transferRadiusAnt / 2), transferRadiusAnt, transferRadiusAnt);
                    }

                    //if highlight ants targeting food is checked, highlight ants blue on the A team; showing which ants know where the food is
                    if (highlightFoodAntCheckBox.Checked && tempAnt.IsKnowsWhereFood && Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "A")
                    {
                        worldImageGraphics.FillRectangle(highlighterFoodAnt, xCoordinate, yCoordinate, antSize + 1, antSize);
                    }

                    //if the ant is carrying food, it should have a small marker on it (gold) to indicate it's carrying a piece of food
                    if (tempAnt.IsCarryingFood)
                        worldImageGraphics.FillRectangle(foodBrush, xCoordinate, yCoordinate, 2, 2);
                }

                //draws enemy ants on image
                for (int i = 0; i < numberOfEnemyAnts; i++)
                {
                    //find each ant in the enemy ant array
                    tempAnt = antEnemyArray[i];

                    //find the xCoordinates of each ant, used for drawing them on the bitmap
                    xCoordinate = tempAnt.X;
                    yCoordinate = tempAnt.Y;

                    //if the highlight ants targeting nest is checked, and team B is selected, shows all ants on team B that know where the nest is (highlighted green)
                    if (highlightNestAntCheckBox.Checked && tempAnt.IsKnowsWhereNest && Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "B")
                    {
                        worldImageGraphics.FillRectangle(highlighterNestAnt, xCoordinate, yCoordinate, antSize, antSize);
                    }
                    else
                    {
                        //draws ants as generic black (unhighlighted; ignorant of all droppables)
                        worldImageGraphics.FillRectangle(antBrush, xCoordinate, yCoordinate, antSize + 1, antSize);
                    }

                    //if show enemy ant check box is checked, all the enemy ants are then changed to RED to indicate accordingly
                    if (highlightEnemyAntCheckBox.Checked)
                    {
                        worldImageGraphics.FillRectangle(antEnemyBrush, xCoordinate, yCoordinate, antSize + 1, antSize);
                    }

                    //show transfer radius of ants, thus how close they need to be to send infomation (only shows ants on selected team)
                    if (showAntCommsCheckBox.Checked && Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "B")
                    {
                        worldImageGraphics.DrawEllipse(foodOutlinePen, xCoordinate - (transferRadiusAnt / 2), yCoordinate - (transferRadiusAnt / 2), transferRadiusAnt, transferRadiusAnt);
                    }

                    //if highlight ants targeting food is checked, highlight ants blue on the B team; showing which ants know the location of where an ant is carrying food
                    if (highlightFoodAntCheckBox.Checked && tempAnt.IsKnowsWhereFood && Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "B")
                    {
                        worldImageGraphics.FillRectangle(highlighterFoodAnt, xCoordinate, yCoordinate, antSize + 1, antSize);
                    }

                    //if the ant is carrying food, it should have a small marker on it (gold) to indicate it's carrying a piece of food
                    if (tempAnt.IsCarryingFood)
                    {
                        worldImageGraphics.FillRectangle(foodBrush, xCoordinate, yCoordinate, 2, 2);
                    }
                }

                //draws food on image
                if (foodCount > 0)
                {
                    for (int i = 0; i < foodCount; i++)
                    {
                        //find each food in the food array
                        tempFood = foodArray[i];
                        if (tempFood.FoodResource > 0)
                        {
                            //get x and y coordinates of the food to later be drawn on the bitmap
                            xCoordinate = tempFood.X;
                            yCoordinate = tempFood.Y;

                            //if foodRangeCheckBox is checked, show the radius of targeting of the food (how close an ant needs to be to find the food)
                            if (showFoodRangeCheckBox.Checked)
                            {
                                //draw the radius
                                worldImageGraphics.DrawEllipse(foodOutlinePen, xCoordinate - (targetRadiusFood / 2), yCoordinate - (targetRadiusFood / 2), targetRadiusFood, targetRadiusFood);
                            }

                            //draw the food on the bitmap
                            worldImageGraphics.FillEllipse(foodBrush, xCoordinate, yCoordinate, foodSize, foodSize);
                            worldImageGraphics.DrawEllipse(foodOutlinePen, xCoordinate, yCoordinate, foodSize, foodSize);
                        }
                    }
                }

                //draws nest(s) on image
                if (nestCount > 0)
                {
                    for (int i = 0; i < nestCount; i++)
                    {
                        //find each nest in the friendly ant nest array
                        tempNest = nestArray[i];

                        //get the coordinates of the nest(s) to later draw the nest(s) on the bitmap
                        xCoordinate = tempNest.X;
                        yCoordinate = tempNest.Y;

                        //if the nest range check box is checked, and the team A is selected, the radius of that team's nests is shown
                        if (showNestRangeCheckBox.Checked && Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "A")
                        {
                            //draw the radius
                            worldImageGraphics.DrawEllipse(nestOutlinePen, xCoordinate - (targetRadiusNest / 2), yCoordinate - (targetRadiusNest / 2), targetRadiusNest, targetRadiusNest);
                        }

                        //draw the nest, outline is black to indicate friendly nest
                        worldImageGraphics.FillEllipse(nestBrush, xCoordinate, yCoordinate, nestSize, nestSize);
                        worldImageGraphics.DrawEllipse(nestOutlinePen, xCoordinate, yCoordinate, nestSize, nestSize);
                    }
                }

                //draws enemy nest(s) on image
                if (nestEnemyCount > 0)
                {
                    for (int i = 0; i < nestEnemyCount; i++)
                    {
                        //find each nest coordinate for the enemy ants
                        tempNest = nestEnemyArray[i];

                        //store coordinates of the nest(s) so later is drawn on the bitmap
                        xCoordinate = tempNest.X;
                        yCoordinate = tempNest.Y;

                        //if nest range is checked, the nest radius should be shown on to indicate how far an ant needs to be to find it
                        if (showNestRangeCheckBox.Checked && Convert.ToString(antTeamSelectionComboBox.SelectedItem) == "B")
                        {
                            //draw the radius
                            worldImageGraphics.DrawEllipse(nestOutlinePen, xCoordinate - (targetRadiusNest / 2), yCoordinate - (targetRadiusNest / 2), targetRadiusNest, targetRadiusNest);
                        }

                        //draw the nest. Highlighted red to indicated enemy nest
                        worldImageGraphics.FillEllipse(nestBrush, xCoordinate, yCoordinate, nestSize, nestSize);
                        worldImageGraphics.DrawEllipse(NestEnemyOutlinePen, xCoordinate, yCoordinate, nestSize, nestSize);
                    }
                }

                //draws bitmap image to mainPanel
                using (Graphics panelGraphics = mainPanel.CreateGraphics())
                {
                    panelGraphics.DrawImage(worldImage, 0, 0, mainPanel.Width, mainPanel.Height);
                }

            }
        }

        //check if the food still has resources, if not ant should stop targeting the food
        private void CheckFoodExists()
        {
            for (int i = 0; i < numberOfAnts; i++)
            {
                //check each food the ant's are targeting exists, if the food doesn't have an resources left, the ant should forget where it is
                tempAnt = antArray[i];

                if (foodArray[tempAnt.FoodTarget].FoodResource < 1)
                {
                    tempAnt.IsKnowsWhereFood = false;
                }
            }
        }

        //if food exists, check whether ant is near food.
        //if it is, ant knows the location of the food
        //then if it is within the transfer radius, transfer food to ant
        //if the food the ant is targeting runs out of resource - the ant forgets where the food was
        private void CheckAntTargetingFood(Ant tempAnt)
        {
            for (int i = 0; i < foodCount; i++)
            {
                tempFood = foodArray[i];

                //check if ant is in target radius of food, if it is, it then knows the location of the food
                //records food no to later check whether the food still has resources if it comes back
                if (IsNearFood(tempAnt, tempFood) && tempFood.FoodResource > 0 && tempAnt.IsKnowsWhereFood == false)
                {
                    //the ant finds the food and stores it's coordinates
                    tempAnt.FoodX = tempFood.X;
                    tempAnt.FoodY = tempFood.Y;
                    tempAnt.IsKnowsWhereFood = true;

                    //and corresponding target number
                    tempAnt.FoodTarget = i;
                }
                else if (tempFood.FoodResource < 1)
                {
                    tempAnt.IsKnowsWhereFood = false;
                }

                //check if ant can transfer, then transfering food to ant if food has any resource. If no food left, ant should forget where the food was
                if (Math.Abs(tempAnt.X - tempAnt.FoodX) <= transferRadius && Math.Abs(tempAnt.Y - tempAnt.FoodY) <= transferRadius && tempAnt.IsTargetingFood)
                {
                    //when the ant takes a piece of food, remove one piece from the food
                    foodArray[tempAnt.FoodTarget].FoodResource--;

                    //ant is now carrying food, no longer looking for food & is now looking for the nest (or moving to it if it knows where it is)
                    tempAnt.IsCarryingFood = true;
                    tempAnt.IsTargetingFood = false;
                    tempAnt.IsTargetingNest = true;

                }
            }
        }

        //if nest(s) has been placed, check whether the ant is near any nest
        //if near, ant knows where the nest is
        //if carrying food and targeting the nest, while also in transfer radius of the nest - the ant transfers food to nest
        private void CheckAntTargetingNest(Ant tempAnt, Nest[] nestArray, int nestCount)
        {
            for (int i = 0; i < nestCount; i++)
            {
                //check if ant is in target radius of a nest, if it is, it then knows the location of the nest
                tempNest = nestArray[i];
                if (IsNearNest(tempAnt, tempNest))
                {
                    //ant stores the nest coordinates
                    tempAnt.NestX = tempNest.X;
                    tempAnt.NestY = tempNest.Y;
                    tempAnt.IsKnowsWhereNest = true;

                    //and corresponding nest target number
                    tempAnt.NestTarget = i;
                }
            }

            //if ant is carrying food & targeting nest & within transfer radius, deposit food and return to targeting food
            if (Math.Abs(tempAnt.X - tempAnt.NestX) <= transferRadius && Math.Abs(tempAnt.Y - tempAnt.NestY) <= transferRadius && tempAnt.IsCarryingFood && tempAnt.IsTargetingNest)
            {
                tempAnt.IsCarryingFood = false;
                tempAnt.IsTargetingNest = false;
                tempAnt.IsTargetingFood = true;
            }
        }

        //function is passed an ant to check if any are nearby, and if any can transfer infomation to it like nearby food or nests
        private void AntCommunication(Ant someAnt, Ant[] antArray)
        {
            if (foodCount > 0)
            {
                for (int i = 0; i < numberOfAnts; i++)
                {
                    //read in and store each ant
                    checkAnt = antArray[i];

                    //compare information of both ants
                    AntCommunicateFood(someAnt, checkAnt);
                    AntCommunicateNest(someAnt, checkAnt);
                }
            }
        }

        //checks if an ant knows the location of the NEST, if it does it transfers the location to 'tempAnt'
        private void AntCommunicateNest(Ant tempAnt, Ant checkAnt)
        {
            if (IsNearAnt(tempAnt, checkAnt) && checkAnt.IsKnowsWhereNest)
            {
                //ant gets the coordinates of the nest if the checkAnt knows where it is
                tempAnt.NestX = checkAnt.NestX;
                tempAnt.NestY = checkAnt.NestY;
                tempAnt.IsKnowsWhereNest = checkAnt.IsKnowsWhereNest;
            }
        }

        //checks if an ant knows the location of the FOOD, if it does it transfers the location to 'tempAnt'
        private void AntCommunicateFood(Ant tempAnt, Ant checkAnt)
        {
            if (IsNearAnt(tempAnt, checkAnt) && checkAnt.IsKnowsWhereFood)
            {
                //ant gets the coordinates of the food if the checkAnt knows where it is
                tempAnt.FoodX = checkAnt.FoodX;
                tempAnt.FoodY = checkAnt.FoodY;
                tempAnt.IsKnowsWhereFood = checkAnt.IsKnowsWhereFood;
            }
        }

        //checks enemy ants if within transfer radius of one another, then transfers nest infomation if one ant knows the location
        //then if an ant bumps (is in the transfer radius) of a friendly ant, transfer food piece to enemy ant & ant goes back to nest
        private void EnemyAntCommunication(Ant tempAnt)
        {
            for (int i = 0; i < numberOfEnemyAnts; i++)
            {
                //stores each instance of the enemy ants to be checked with tempAnt
                checkAnt = antEnemyArray[i];

                //checks whether the checkAnt knows where the nest is, then relays that infomation
                AntCommunicateNest(tempAnt, checkAnt);
            }

            for (int i = 0; i < numberOfEnemyAnts; i++)
            {
                //stores each instance of the enemy ants to be checked with tempAnt
                checkAnt = antArray[i];

                //if near each other and checkAnt is carrying food, transfer where the food is
                if (IsNearAnt(checkAnt, tempAnt) && checkAnt.IsCarryingFood)
                {
                    //enemy ant is now targeting nest and carrying food, thus moving to the nest to store food
                    tempAnt.IsTargetingNest = true;
                    tempAnt.IsCarryingFood = true;
                }
            }
        }

        //check if ant is in target radius of food
        private bool IsNearFood(Ant tempAnt, Food tempFood)
        {
            if ((Math.Abs(tempAnt.X - tempFood.X) <= targetRadiusFood) && (Math.Abs(tempAnt.Y - tempFood.Y) <= targetRadiusFood))
            {
                return true;
            }
            return false;
        }

        //check if ant is in target radius of nest
        private bool IsNearNest(Ant tempAnt, Nest tempNest)
        {
            if ((Math.Abs(tempAnt.X - tempNest.X) <= targetRadiusNest) && (Math.Abs(tempAnt.Y - tempNest.Y) <= targetRadiusNest))
            {
                return true;
            }
            return false;
        }

        //check if ant is near another ant
        private bool IsNearAnt(Ant someAnt, Ant tempAnt)
        {
            if ((Math.Abs(tempAnt.X - someAnt.X) <= transferRadiusAnt) && (Math.Abs(tempAnt.Y - someAnt.Y) <= transferRadiusAnt))
            {
                return true;
            }
            return false;
        }

        //used to clear ants memory of food(s)
        //by erasing their stored food target
        private void ClearAntFoodTargets(Ant[] antArray)
        {
            for (int i = 0; i < antArray.Length; i++)
            {
                //store each instance of ants to be cleared
                tempAnt = antArray[i];

                //stores the food that the ant is targeting
                tempFood = foodArray[tempAnt.FoodTarget];

                //if the ant is near that targeted food-
                if (IsNearFood(tempAnt, tempFood))
                {
                    //forget that the nest exists
                    tempAnt.IsKnowsWhereFood = false;
                }
            }
        }

        //used to clear ants memory of nest(s)
        //by erasing their stored nest target
        private void ClearAntNestTargets(Ant[] antArray, Nest[] nestArray)
        {
            for (int i = 0; i < antArray.Length; i++)
            {
                //store each instance of ants to be cleared
                tempAnt = antArray[i];
                if (tempAnt != null)
                {
                    //stores the nest that the ant is targeting
                    tempNest = nestArray[tempAnt.NestTarget];

                    //if the ant is near that targeted nest -
                    if (IsNearNest(tempAnt, tempNest))
                    {
                        //forget that the nest exists
                        tempAnt.IsKnowsWhereNest = false;
                    }
                }
            }
        }

        //timer tick that triggers the next cycle of the system
        private void simpleTimer_Tick(object sender, EventArgs e)
        {
            //display all entities (ants/enemy ants/nests/food) to the panel
            DisplayAll();

            //move the ants, based on their context (eg. to nest if they know where it is & are carrying food)
            MoveAnts();

            movementCount += 1;
        }

        //when the main panel is clicked by the user, the coordinates are read and-
        //-a droppable object is created depending on which button is checked.
        private void mainPanel_MouseClick(object sender, MouseEventArgs e)
        {

            xCoordinate = e.X;
            yCoordinate = e.Y;

            //if the food radio button is checked, create new food based on mouse location
            if (foodRadioButton.Checked)
            {
                CreateFood(xCoordinate, yCoordinate);
            }

            //if the nest radio button is checked, create new nest based on mouse location
            if (nestRadioButton.Checked)
            {
                CreateNest(xCoordinate, yCoordinate);
            }
        }

        //when button is clicked, all droppable objects are deleted
        //ants still know the position, as if the objects have been 'picked up'
        //using 'set a
        private void clearDroppablesButton_Click(object sender, EventArgs e)
        {
            //if food has been placed-
            if (foodCount > 0)
            {
                //clear food
                ClearAntFoodTargets(antArray);
            }

            //if nest(s) have been placed-
            if (nestCount > 0)
            {
                //clear nest(s)
                ClearAntNestTargets(antArray, nestArray);
            }

            //if enemy nests have been placed-
            if (nestEnemyCount > 0)
            {
                //clear nest(s)
                ClearAntNestTargets(antEnemyArray, nestEnemyArray);
            }

            //set all food in the food array to null, thus clearing the array
            for (int i = 0; i < foodArray.Length; i++)
            {
                foodArray[i] = null;
                foodCount = 0;
            }

            //set all nests in the nest array to null, thus clearing the array
            for (int i = 0; i < nestArray.Length; i++)
            {
                nestArray[i] = null;
                nestCount = 0;
            }

            //set all enemy nests in the enemy nest array to null, thus clearing the array
            for (int i = 0; i < nestEnemyArray.Length; i++)
            {
                nestEnemyArray[i] = null;
                nestEnemyCount = 0;
            }
        }

    }
}

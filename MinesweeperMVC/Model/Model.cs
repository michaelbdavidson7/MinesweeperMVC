using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
   
    public enum Difficulty { EASY, MEDIUM, HARD }

    interface IMinesweeper
    {
        Difficulty GameDifficulty { get; }

        int MineCount { get; set; }

        int RemainingMineCount { get; }

        Boolean GameOver { get; set; }

        int GridWidth { get; set; }

        int GridHeight { get; set; }

        void CreateGrid(Difficulty d);

        void LayMine(Point firstOpenedPoint);

        Boolean OpenCell(Point openClickedPoint);

        Boolean FlagCell(Point flagClickedPoint);

        Cell GetCell(Point clickedCell);

        void NewGame(Difficulty d);
    }

    public class Cell
    {
        public Boolean IsFlagged;
        public Boolean IsOpen;
        public int NeighborBombCount = 0;

        public Boolean IsBomb()
        {
            return NeighborBombCount == 9;
        }

        public Boolean IsZero()
        {
            return NeighborBombCount == 0;
        }
    }

    public class Model : IMinesweeper
    {

        int lengthOfXAxis = 9;
        int lengthOfYAxis = 9;
        public int[,] minesweeperGrid = new int[9, 9];
        public int totalSpacesWithoutBombs ;
       public int bomblessSpacesCounter;
        public int currentButtonSurroundingBombs;
        Point firstOpenedPoint = new Point(0, 0);
        public bool firstClickedPoint = false;
        private int _lengthOfXAxis;
        private int _lengthOfYAxis;
        private int _mineCount = 10;
        private int _remainingMineCount = 10;
        private Difficulty _difficulty;

        public void runGame()
        {
             
            Difficulty difficulty =  Difficulty.EASY;

            if (difficulty == Difficulty.EASY)
            {
                GridWidth = 9;
                GridHeight = 9;
                MineCount = 10;
            }
            if (difficulty == Difficulty.HARD)
            {
                GridWidth = 12;
                GridHeight = 12;
                MineCount = 30;
            }
            if (difficulty == Difficulty.HARD)
            {
                GridWidth = 16;
                GridHeight = 16;
                MineCount = 100;
            }

            NewGame(difficulty);
        }

        public void NewGame(Difficulty d)
        {
            _difficulty = d;
            CreateGrid(d);
        }

        public void CreateGrid(Difficulty d)
        {
            //if (firstClickedPoint == false)
            //{
                LayMine(firstOpenedPoint);
                AssignSurroundingBombNumbers();
            //}
           
        }

        public void LayMine(Point firstOpenedPoint)
        {
            Random RNG = new Random();
            int num1, num2;
            for (int i = 0; i <= _mineCount; )
            {
                num1 = RNG.Next(0, 8);
                num2 = RNG.Next(0, 8);
                if (minesweeperGrid[num1, num2] != 9)
                {
                    minesweeperGrid[num1, num2] = 9;
                    i++;
                }
            }
           

        }

        public void LayMineForTesting(Point firstOpenedPoint)
        {
            for (int i = 0; i < lengthOfXAxis; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    minesweeperGrid[i, j] = 9;   
                }
            }
            firstClickedPoint = true;

        }


        private int AssignSurroundingBombNumbers()
        {
            for (int i = 0; i < lengthOfXAxis; i++)
            {
                for (int j = 0; j < lengthOfYAxis; j++)
                {
                    if (minesweeperGrid[i, j] != 9)
                    {
                        minesweeperGrid[i, j] = FindSurroundingBombs(i, j);
                    }
                }
            }
            return 0;
        }

        public Cell GetCell(Point clickedCell)
        {
            throw new NotImplementedException();
        }

        public bool OpenCell(Point openClickedPoint)
        {
            if (CheckIfBomb(openClickedPoint))
            {
                IsGameOver();
                return true;
            }
            else
            {
                currentButtonSurroundingBombs = minesweeperGrid[openClickedPoint.X, openClickedPoint.Y];
                //set the button to the number
                //if its a zero, open up all adjacent zeroes

                bomblessSpacesCounter++;
                if (IsLastButton())
                {
                   Console.WriteLine("YOU WON! PARTY!!");

                }
                return false;
            }
        }

        public void CheckSurroundingCellsForZeroes(int x, int y)
        {
            Point tempPoint = new Point(0, 0);
            Point[] zeroPoints = new Point[81];
            int quickZeroPointsCounter = 0;
            for (int i = x-1; i < x+1; i++)
            {
                for (int j = y-1; j < y+1; j++)
                {
                    tempPoint.X = i;
                    tempPoint.Y = j;
                    if (InBorderCheck(i, j))
                    {
                        if (CheckIfZero(tempPoint))
                        {
                            zeroPoints[quickZeroPointsCounter] = tempPoint;
                        }
                    }
                }
            }
            foreach (Point p in zeroPoints)
            {
                OpenCell(p);
            }


        }

        bool CheckIfZero(Point p)
        {
            if (minesweeperGrid[p.X, p.Y] == 0)
            {
                return true;
            }
            return false;
        }

        public bool FlagCell(Point flagClickedPoint)
        {
            throw new NotImplementedException();
        }


        public bool CheckIfBomb(Point buttonPoint)
        {
            //the bombs were set to 9, other buttons were not
            if (minesweeperGrid[buttonPoint.X, buttonPoint.Y] == 9)
            {
                return true;
            }
            return false;
        }

        public bool IsLastButton()
        {
            if (bomblessSpacesCounter == totalSpacesWithoutBombs)
            {
                return true;
            }
            return false;
        }

        public void IsGameOver()
        {
            //RevealAllSpots();
            Console.WriteLine("Game Over!");
            //report score
            //close
        }

        

        public int FindSurroundingBombs(int x, int y)
        {
            Point tempPoint = new Point(0,0);
            int surroundingBombs = 0;
            for (int i = x - 1; i <=  x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    
                    if (InBorderCheck(i, j))
                    {
                        tempPoint.X = i;
                        tempPoint.Y = j;
                        if (CheckIfBomb(tempPoint))
                        {
                            surroundingBombs++;
                        }
                    }
                }
            }
            return surroundingBombs;
        }

        public bool InBorderCheck(int X, int Y)
        {
            //if point is within border, return true
            if (X < lengthOfXAxis && X >= 0)
            {
                if (Y < lengthOfYAxis && Y >= 0)
                {
                    return true;
                }
            }
            return false;
        }


        //backlog
        //right click to flag
        //TESTS


        public Difficulty GameDifficulty
        {
            get { return this._difficulty; }
        }

        public int MineCount
        {
            get
            {
                return this._mineCount;
            }
            set
            {
                this._mineCount = value;
            }
        }

        public int RemainingMineCount
        {
            get { return this._remainingMineCount; }
        }

        public bool GameOver
        {
            
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int GridWidth
        {
            get
            {
                return this._lengthOfXAxis;
            }
            set
            {
                this._lengthOfXAxis = value;
            }
        }

        public int GridHeight
        {
            get
            {
                
                return this._lengthOfYAxis;
            }
            set
            {
                
                this._lengthOfYAxis = value;
            }
        }






        public static void Main()
        {
            //Model model = new Model();
            //Difficulty difficulty = Difficulty.EASY;

            //if (difficulty == Difficulty.EASY)
            //{
            //    model.GridWidth = 9;
            //    model.GridHeight = 9;
            //    model.MineCount = 10;
            //}
            //if (difficulty == Difficulty.HARD)
            //{
            //    model.GridWidth = 12;
            //    model.GridHeight = 12;
            //    model.MineCount = 30;
            //}
            //if (difficulty == Difficulty.HARD)
            //{
            //    model.GridWidth = 16;
            //    model.GridHeight = 16;
            //    model.MineCount = 100;
            //}

            //model.NewGame(difficulty);
        }


    }



}






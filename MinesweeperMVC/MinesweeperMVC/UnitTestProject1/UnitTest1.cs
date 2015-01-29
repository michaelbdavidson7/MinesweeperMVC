using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinesweeperMVC;
using Model;

namespace UnitTestProject1
{
   


    [TestClass]
    public class UnitTest1
    {


        Model.Model model = new Model.Model();
        
        Point p = new Point(3, 3);

        [TestMethod]
        public void TestMethod1()
        {
            
            Console.WriteLine("hi");
        }

        [TestMethod]
        public void Test_Is_Last_Button()
        {
            model.runGame();
            
            model.bomblessSpacesCounter = 10;
            model.totalSpacesWithoutBombs = 71;

            Assert.IsFalse(model.IsLastButton());

            model.bomblessSpacesCounter = 71;
            model.totalSpacesWithoutBombs = 71;

            Assert.IsTrue(model.IsLastButton());
        }


        [TestMethod]
        public void Test_Check_If_Bomb()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Assert.IsFalse(model.CheckIfBomb(new Point(i, j)));
                }
            }
            Assert.IsFalse(model.CheckIfBomb(new Point(3,3)));
            Assert.IsFalse(model.CheckIfBomb(new Point(0, 0)));
        }
       
        [TestMethod]
        public void Test_Open_Cell()
        {
            Assert.IsFalse(model.OpenCell(p));
            Assert.IsFalse(model.OpenCell(new Point(0,0)));
            Assert.IsFalse(model.OpenCell(new Point(4,4)));
        }
    
        [TestMethod]
        public void Test_LayMines()
        {
            model.runGame();
            int numMines = 0;
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    Console.WriteLine(model.minesweeperGrid[i, j]);
                    if (model.minesweeperGrid[i, j] == 9)
                    {
                        
                        numMines++;
                    }
                }
            }
            Assert.IsTrue(numMines > 3);
        }

    [TestMethod]
        public void Test_FindSurroundingBombs()
        {
            model.runGame();
            model.FindSurroundingBombs(1,1);
            Console.WriteLine(model.currentButtonSurroundingBombs);
            Assert.IsFalse((model.currentButtonSurroundingBombs == 2));

            Assert.IsTrue(model.FindSurroundingBombs(1, 1) == 2);
        }



    }
}

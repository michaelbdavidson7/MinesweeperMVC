using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using System.Drawing;

namespace Controller
{
    public class Controller
    {
        Model.Model model = new Model.Model();
        Button b;
        Point p;

        static void Main(string[] args)
        {

            
            
            
        }

        public void runGame()
        {
            
            model.runGame();
        }

        //public void buttonPushed(Object sender, EventArgs e)
        //{
        //    this.Button = new Button;
        //    model.OpenCell();
        //    if(e.Button == )
        //}

        public void buttonTagSender(Object sender, EventArgs e)
        {

            b = (Button)sender;
            p = (Point)b.Tag;
            if (model.OpenCell(p))
            {
                //gameover
               
                b.BackColor = Color.Red;
                MessageBox.Show("Game Over", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                b.Text = "" + model.currentButtonSurroundingBombs;
                b.BackColor = Color.Beige;
            }
            if (model.currentButtonSurroundingBombs == 0)
            {
                model.CheckSurroundingCellsForZeroes(p.X, p.Y);
            }
        }

    }
}

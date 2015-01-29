using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperMVC
{
    public partial class View : Form
    {



        Controller.Controller con = new Controller.Controller();

        public View()
        {
            InitializeComponent();

            con.runGame();

            Point[,] buttonPoints = new Point[9, 9];
            Point p;
            int buttonXCoord;
            int buttonYCoord;

            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    buttonXCoord = 33 + (i * 25);
                    buttonYCoord = 92 + (j * 25);
                    this.button1 = new Button
                    {

                        Location = new System.Drawing.Point(buttonXCoord, buttonYCoord),
                        Name = "button" + (i * j),
                        Size = new System.Drawing.Size(25, 25),
                        TabIndex = 0,
                        Text = "",
                        UseVisualStyleBackColor = true,

                    };

                    p = new Point(i, j);
                    buttonPoints[i, j] = p;
                    this.button1.Tag = p;

                    //this.button1.Click += new System.EventHandler(con.buttonPushed);
                    this.button1.Click += new System.EventHandler(con.buttonTagSender);
                    this.Controls.Add(this.button1);
                }
            }


            this.SuspendLayout();


        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            con.runGame();
        }

        private void button_Click(object sender, EventArgs e)
        {


        }

    }
}

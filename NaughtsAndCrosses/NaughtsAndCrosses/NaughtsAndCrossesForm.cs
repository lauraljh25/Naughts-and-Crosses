using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NaughtsAndCrosses
{

    public partial class NC : Form
    {
        Boolean p1Turn = true;
        Boolean AI = false;

        List<Label> labelAI = new List<Label>();


        string txtPlayer1 = "X";
        string txtPlayer2 = "O";

        public String checkwin()
        {
            if (row().Equals("X") || column().Equals("X") || diagonal().Equals("X"))
            {
                return "X";
            }
            else if (row().Equals("O") || column().Equals("O") || diagonal().Equals("O"))
            {
                return "O";
            }
            else
            {
                return "NoWin";
            }
        }

        public string row()
        {
            List<Label> row1 = new List<Label>();
            row1.Add(lbl);
            row1.Add(lbl7);
            row1.Add(lbl4);

            List<Label> row2 = new List<Label>();
            row2.Add(lbl9);
            row2.Add(lbl6);
            row2.Add(lbl3);

            List<Label> row3 = new List<Label>();
            row3.Add(lbl8);
            row3.Add(lbl5);
            row3.Add(lbl2);

            if ((row1.All(item => item.Text == "X")) || (row2.All(item => item.Text == "X")) || (row3.All(item => item.Text == "X")))
            {
                return "X"; 
            }
            else if ((row1.All(item => item.Text == "O")) || (row2.All(item => item.Text == "O")) || (row3.All(item => item.Text == "O")))
            {
                return "O";
            }
            else
            {
                return "";
            }
        }

        public string column()
        {
            List<Label> column1 = new List<Label>();
            column1.Add(lbl);
            column1.Add(lbl9);
            column1.Add(lbl8);

            List<Label> column2 = new List<Label>();
            column2.Add(lbl7);
            column2.Add(lbl6);
            column2.Add(lbl5);

            List<Label> column3 = new List<Label>();
            column3.Add(lbl4);
            column3.Add(lbl3);
            column3.Add(lbl2);

            if ((column1.All(item => item.Text == "X")) || (column2.All(item => item.Text == "X")) || (column3.All(item => item.Text == "X")))
            {
                return "X";
            }
            else if ((column1.All(item => item.Text == "O")) || (column2.All(item => item.Text == "O")) || (column3.All(item => item.Text == "O")))
            {
                return "O";
            }
            else
            {
                return "";
            }
        }

        public string diagonal()
        {
            List<Label> diagonal1 = new List<Label>();
            diagonal1.Add(lbl);
            diagonal1.Add(lbl6);
            diagonal1.Add(lbl2);

            List<Label> diagonal2 = new List<Label>();
            diagonal2.Add(lbl8);
            diagonal2.Add(lbl6);
            diagonal2.Add(lbl4);

            if ((diagonal1.All(item => item.Text == "X")) || (diagonal2.All(item => item.Text == "X")))
            {
                return "X";
            }
            else if ((diagonal1.All(item => item.Text == "O")) || (diagonal2.All(item => item.Text == "O")))
            {
                return "O";
            }
            else
            {
                return "";
            }
        }

        public void endGame()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel.Text.Equals(""))
                {
                    iconLabel.Visible = false;
                }
            }

            //message box 
            string message = "";
            if (!checkwin().Equals("NoWin"))
            {
                message = "Congratulations! " + checkwin() + " wins! Do you want to have another game?";
            }
            else if (checkDraw())
            {
                message = "Congratulations! its a draw! Do you want to have another game?";
            }

            DialogResult dialogResult = MessageBox.Show(message, "End Game", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                player2Btn.Visible = true;
                player1Btn.Visible = true;

                tableLayoutPanel1.Visible = false;


                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    Label iconLabel = control as Label;

                    iconLabel.Visible = false;
                    iconLabel.Text = "";
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            } 
        }

        public bool checkDraw()
        {
            Boolean draw = true;
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel.Text.Equals(""))
                {
                    draw = false;
                }
            }
            if (draw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void startGame()
        {
            player2Btn.Visible = false;
            player1Btn.Visible = false;

            tableLayoutPanel1.Visible = true;


            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                iconLabel.Visible = true;
                iconLabel.Text = "";
            }
        }

        public void AIturn()
        {
            Random r1 = new Random();
            if (labelAI.Any())
            {
                int selectedLbl = r1.Next(labelAI.Count);
                labelAI[selectedLbl].Text = txtPlayer2;
                labelAI.RemoveAt(selectedLbl);
            }
        }

        public NC()
        {
            InitializeComponent();
        }


        private void label_click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                if (AI == false)
                {
                    if (clickedLabel.Text.Equals(""))
                    {
                        if (p1Turn)
                        {
                            clickedLabel.Text = txtPlayer1;
                            p1Turn = false;
                            if (!checkwin().Equals("NoWin"))
                            {
                                endGame();
                            }
                            else if (checkDraw())
                            {
                                endGame();
                            }
                        }
                        else
                        {
                            clickedLabel.Text = txtPlayer2;
                            p1Turn = true;
                            if (!checkwin().Equals("NoWin"))
                            {
                                endGame();
                            }
                            else if (checkDraw())
                            {
                                endGame();
                            }
                        }
                    }
                }
                else
                {
                    Boolean end = false;

                    if (clickedLabel.Text.Equals(""))
                    {
                            clickedLabel.Text = txtPlayer1;
                            labelAI.Remove(clickedLabel);
                            if (!checkwin().Equals("NoWin"))
                            {
                                endGame();
                                end = true;
                            }
                            else if (checkDraw())
                            {
                                endGame();
                                end = true;
                            }

                        if (!end)
                        {
                            AIturn();
                            if (!checkwin().Equals("NoWin"))
                            {
                                endGame();
                            }
                            else if (checkDraw())
                            {
                                endGame();
                            }
                        }
                    }
                }
            }
        }

        private void player2Btn_Click(object sender, EventArgs e)
        {
            startGame();
            AI = false;
        }

        private void player1Btn_Click(object sender, EventArgs e)
        {
            startGame();
            AI = true;

            foreach (Label label in tableLayoutPanel1.Controls)
            {
                labelAI.Add(label);
            }
        }
    }
}

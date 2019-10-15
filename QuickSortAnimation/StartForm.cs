using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickSortAnimation
{
    public partial class StartForm : Form
    {

        public int elementsAmount;
        public int tickTime;

        // main method
        public StartForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // if run button clicked
        private void button1_Click(object sender, EventArgs e)
        {
            // getting data from text boxes
            TextBox elementsAmountTextBox = (TextBox)textBox1;
            string elementsAmountUncoded = elementsAmountTextBox.Text;

            TextBox tickTimeTextBox = (TextBox)textBox2;
            string tickTimeUncoded = tickTimeTextBox.Text;


            try
            {
                // convert data to int
                elementsAmount = int.Parse(elementsAmountUncoded);
                tickTime = int.Parse(tickTimeUncoded);

                // if all correct
                if ((elementsAmount >= 10) && (elementsAmount <= 100) && (tickTime >= 100) && (tickTime <= 10000))
                {
                    // launch animation form
                    AnimationForm animationForm = new AnimationForm(elementsAmount, tickTime);
                    
                    // show animation form and close start form
                    this.Hide();
                    animationForm.Closed += (s, args) => this.Close();
                    animationForm.Show();
                }

                // if data out of borders
                else
                    MessageBox.Show("Введенные данные не соответствуют заданным границам!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (FormatException)
            {
                // if data typed incorrectly
                MessageBox.Show("Неправильный формат ввода!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

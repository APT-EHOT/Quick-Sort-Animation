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
        public StartForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBox elementsAmountTextBox = (TextBox)textBox1;
            string elementsAmountUncoded = elementsAmountTextBox.Text;


            TextBox tickTimeTextBox = (TextBox)textBox2;
            string tickTimeUncoded = tickTimeTextBox.Text;


            try
            {
                int elementsAmount = int.Parse(elementsAmountUncoded);
                int tickTime = int.Parse(tickTimeUncoded);

                if ((elementsAmount >= 10) && (elementsAmount <= 100) && (tickTime >= 100) && (tickTime <= 10000))
                    MessageBox.Show("ПРАВИЛЬНО", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else
                    MessageBox.Show("Введенные данные не соответствуют заданным границам!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильный формат ввода!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

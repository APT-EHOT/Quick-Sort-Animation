using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickSortAnimation
{
    public partial class AnimationForm : Form
    {

        // variables from startForm
        public int elementsAmountTransit;
        public int tickTimeTransit;

        // animation params
        static int _pixelSize;
        static int _width;
        static int _height;
        static int _arraySize;
        static int _textSize;

        int _deltaTime; // animation frames refreshing time 

        bool wasSwapped = false;

        private int _leftBorder = 0;
        private int _rightBorder = 0;
        private int _leftPointer = 0;
        private int _rightPointer = 0;
        private int _pivotPointer = 0;

        int[] numbers; // array to sort

        public Bitmap bitmap;
        public Graphics graphics;
        private PictureBox WindowPictureBox { get; }
        public System.Windows.Forms.Timer TickTimer { get; }

        static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        // sets animation params
        void SetInitData(int elementsAmount, int tickTime)
        {
            // animation elements scaling parameter
            double factor = (double)elementsAmount / 10.0;

            // setting animation drawing params
            _pixelSize = (int)(1.0 / factor * 80.0);
            _width = (int)(factor * 10);
            _height = ((int)(factor * 10)) + 1;
            _arraySize = elementsAmount;
            _textSize = (int)(1.0 / factor * 32.0);

            _deltaTime = tickTime;
        }

        // make and shuffles array to sort
        public void GetShuffledArray()
        {
            Random random = new Random();
            int randomSwap = 10000 + random.Next(1000000);

            for (int i = 0; i < _arraySize; i++)
                numbers[i] = i + 1;

            for (int i = 0; i < randomSwap; i++)
            {
                int randomPoint = random.Next(_arraySize - 1);
                Swap(ref numbers[randomPoint], ref numbers[randomPoint + 1]);
            }
        }

        // draws all window
        public void DrawWindow()
        {
            if (wasSwapped)
                graphics.Clear(Color.DarkRed);
            else
                graphics.Clear(Color.Black);

            for (int i = 0; i < _width; i++)
            {
                Brush mainBrush;
                Brush pointerBrush;

                // borders of qSort iteration
                if ((i == _leftBorder) || (i == _rightBorder))
                {
                    mainBrush = Brushes.LightGray;
                    pointerBrush = Brushes.Gray;
                }

                // pointers of qSort iteration
                else if ((i == _leftPointer) || (i == _rightPointer))
                {
                    mainBrush = Brushes.DarkRed;
                    pointerBrush = Brushes.Red;
                }

                // pivot element
                else if (i == _pivotPointer)
                {
                    mainBrush = Brushes.DarkBlue;
                    pointerBrush = Brushes.Blue;
                }

                // other elements
                else
                    mainBrush = pointerBrush = Brushes.NavajoWhite;

                for (int j = _height - numbers[i]; j < _height; j++)
                {
                    // draws top element of column with value of the element
                    if (j == _height - numbers[i])
                    {
                        graphics.FillRectangle(pointerBrush, i * _pixelSize, j * _pixelSize, _pixelSize, _pixelSize);
                        graphics.DrawRectangle(Pens.Black, i * _pixelSize, j * _pixelSize, _pixelSize, _pixelSize);
                        int integerToPrint = numbers[i];
                        RectangleF rectangleF = new RectangleF(i * _pixelSize, j * _pixelSize, _pixelSize, _pixelSize);
                        graphics.DrawString(integerToPrint.ToString(), new Font("Tahoma", _textSize), Brushes.Black, rectangleF);
                    }

                    // draws other elements of column
                    else
                    {
                        graphics.FillRectangle(mainBrush, i * _pixelSize, j * _pixelSize, _pixelSize, _pixelSize);
                        graphics.DrawRectangle(Pens.Black, i * _pixelSize, j * _pixelSize, _pixelSize, _pixelSize);
                    }
                }
            }
            WindowPictureBox.Image = bitmap;
        }

        // quick sort algo
        public void QuickSort(int leftBorder, int rightBorder)
        {

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                int leftPointer = leftBorder;
                int rightPointer = rightBorder;
                int pivotElement = numbers[(leftPointer + rightPointer) / 2];
                int pivotPointer = (leftPointer + rightPointer) / 2;

                SetPointersGlobal(leftBorder, rightBorder, leftPointer, rightPointer, pivotPointer); Thread.Sleep(_deltaTime);

                do
                {

                    while (numbers[leftPointer] < pivotElement)
                    {
                        leftPointer++; SetPointersGlobal(leftBorder, rightBorder, leftPointer, rightPointer, pivotPointer); Thread.Sleep(_deltaTime);
                    }


                    while (numbers[rightPointer] > pivotElement)
                    {
                        rightPointer--; SetPointersGlobal(leftBorder, rightBorder, leftPointer, rightPointer, pivotPointer); Thread.Sleep(_deltaTime);
                    }


                    if (leftPointer <= rightPointer)
                    {
                        Swap(ref numbers[leftPointer],
                                 ref numbers[rightPointer]);
                        wasSwapped = true;
                        SetPointersGlobal(leftBorder, rightBorder, leftPointer, rightPointer, pivotPointer); Thread.Sleep(_deltaTime);
                        wasSwapped = false;

                        leftPointer++; rightPointer--; SetPointersGlobal(leftBorder, rightBorder, leftPointer, rightPointer, pivotPointer); Thread.Sleep(_deltaTime);
                    }

                } while (leftPointer < rightPointer);

                if (rightPointer > leftBorder)
                    QuickSort(leftBorder, rightPointer);

                if (rightBorder > leftPointer)
                    QuickSort(leftPointer, rightBorder);

            }).Start();

        }

        // gets pointers from QSort and sets it globally
        public void SetPointersGlobal(int leftBorder, int rightBorder, int leftPointer, int rightPointer, int pivotPointer)
        {
            _leftBorder = leftBorder;
            _rightBorder = rightBorder;
            _leftPointer = leftPointer;
            _rightPointer = rightPointer;
            _pivotPointer = pivotPointer;
        }

        // reprint window every tick
        private void TickTimer_Tick(object sender, EventArgs e)
        {
            DrawWindow();
        }


        // main animation method
        public AnimationForm(int elementsAmount, int tickTime)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            elementsAmountTransit = elementsAmount;
            tickTimeTransit = tickTime;
            SetInitData(elementsAmountTransit, tickTimeTransit);

            bitmap = new Bitmap(_pixelSize * _width + 20, _pixelSize * _height + 50);
            graphics = Graphics.FromImage(bitmap);
            WindowPictureBox = new PictureBox { Dock = DockStyle.Fill, Parent = this };
            Size = bitmap.Size;

            numbers = new int[_arraySize];
            GetShuffledArray(); // shuffling 'numbers' array

            DrawWindow();

            QuickSort(0, _arraySize - 1);

            // refreshing timer
            TickTimer = new System.Windows.Forms.Timer { Interval = _deltaTime, Enabled = true };
            TickTimer.Tick += TickTimer_Tick;
        }
    }
}
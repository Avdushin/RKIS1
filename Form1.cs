using System;
using System.Linq;
using System.Windows.Forms;

namespace Calc
{
    public partial class Form1 : Form
    {
        private TextBox textBox;
        private string currentInput = "";

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
            KeyPress += Form1_KeyPress;
            this.Text = "Калькулятор Авдюшин. Д. А";
        }

        private void InitializeUI()
        {
            textBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(220, 30),
                TextAlign = HorizontalAlignment.Right
        };

            Controls.Add(textBox);

            string[] buttonLabels = { "1", "2", "3", "BIN", "4", "5", "6", "OCT", "7", "8", "9", "HEX", "-", "0", "+", "/", "*", ".", "^", "√", "=", "C" };
            int buttonWidth = 50;
            int buttonHeight = 40;
            int currentLeft = 10;
            int currentTop = 50;

            int rows = 4;
            int cols = 3;

            foreach (string label in buttonLabels)
            {
                Button button = new Button
                {
                    Size = new System.Drawing.Size(buttonWidth, buttonHeight),
                    Location = new System.Drawing.Point(currentLeft, currentTop),
                    Text = label
                };

                button.Click += Button_Click;

                Controls.Add(button);

                currentLeft += buttonWidth;

                if (currentLeft > (buttonWidth * cols) + 10)
                {
                    currentLeft = 10;
                    currentTop += buttonHeight;
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] allowedChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '*', '/', '.', '=' };

            if (allowedChars.Contains(e.KeyChar))
            {
                Button_Click(sender, e);
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                e.KeyChar = '=';
                Button_Click(sender, e);
            }
            else
            {
                e.Handled = true;
            }
        }


        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            if (buttonText == "C")
            {
                currentInput = "";
                textBox.Text = "";
            }
            else if (buttonText == "=")
            {
                try
                {
                    double result = EvaluateExpression(currentInput);
                    textBox.Text = result.ToString();
                    currentInput = result.ToString();
                }
                catch (Exception ex)
                {
                    textBox.Text = "Error";
                    currentInput = "";
                }
            }
            else if (buttonText == "√")
            {
                double number = double.Parse(currentInput);
                double result = Math.Sqrt(number);
                textBox.Text = result.ToString();
                currentInput = result.ToString();
            }
            else if (buttonText == "^")
            {
                currentInput += "^";
                textBox.Text = currentInput;
            }
            else if (buttonText == "BIN" || buttonText == "OCT" || buttonText == "HEX")
            {
                try
                {
                    int number = int.Parse(currentInput);
                    switch (buttonText)
                    {
                        case "BIN":
                            currentInput = Convert.ToString(number, 2);
                            break;
                        case "OCT":
                            currentInput = Convert.ToString(number, 8);
                            break;
                        case "HEX":
                            currentInput = Convert.ToString(number, 16).ToUpper();
                            break;
                    }
                    textBox.Text = currentInput;
                }
                catch (Exception ex)
                {
                    textBox.Text = "Error";
                    currentInput = "";
                }
            }
            else
            {
                currentInput += buttonText;
                textBox.Text = currentInput;
            }
        }

        private double EvaluateExpression(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            return Convert.ToDouble(table.Compute(expression, string.Empty));
        }
    }
}

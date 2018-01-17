using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq.Expressions;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        int[,] sudoku;

        public Form1()
        {
            InitializeComponent();

            sudoku = new int[9, 9];
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Title = "Browse Text Files";
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.DefaultExt = "txt";
                openFileDialog1.Filter = "Text files (*.txt)|*.txt";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.ReadOnlyChecked = true;
                openFileDialog1.ShowReadOnly = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                SudokuValidator sudo = new SudokuValidator();
                sudoku = sudo.LoadFile(txtFile.Text);

                bool isValid = sudo.Validate(sudoku);

                MessageBox.Show(string.Format("Sudoku is {0}!", isValid ? "Valid" : "InValid"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Message -{0} \n\n Stack Trace \n{1}", ex.Message, ex.StackTrace));
            }
        }
    }
}

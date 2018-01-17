using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sudoku
{
    public class SudokuValidator
    {
        public int[,] LoadFile(string path)
        {
            int[,] sudoku = new int[9, 9];

            try
            {
                var fileLines = File.ReadLines(path)
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(l => l.ToList());

                int i = 0, j = 0;

                foreach (var numArray in fileLines)
                {
                    numArray.ForEach(num =>
                    {
                        sudoku[i, j] = int.Parse(num.ToString());
                        j++;
                    });
                    i++;
                    j = 0;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                //do some logging
                throw ex;
            }
            catch (FormatException ex)
            {
                //do some logging
                throw ex;
            }
            catch (Exception ex)
            {
                //do some logging
                throw ex;
            }
            return sudoku;
        }


        public bool Validate(int[,] sudoku)
        {
            int innerSquareLength = 3;
            int rowLength = 9;
            int colLength = 9;

            try
            {
                if (sudoku.GetLength(0) != rowLength
                     || sudoku.GetLength(1) != colLength)
                    return false;

                if (!CheckInRange(sudoku))
                    return false;

                for (int row = 0; row < rowLength; ++row)
                {
                    if (!CheckDuplicate(sudoku, row))
                        return false;
                }

                // Check that the subsquares contain no duplicate values
                for (int baseRow = 0; baseRow < rowLength; baseRow += innerSquareLength)
                {
                    for (int baseCol = 0; baseCol < colLength; baseCol += innerSquareLength)
                    {
                        if (!checkSquare(sudoku, baseRow, baseCol, innerSquareLength))
                            return false;
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                //do some logging
                throw ex;
            }
            catch (Exception ex)
            {
                //do some logging
                throw ex;
            }
            return true;
        }


        private bool CheckInRange(int[,] sudoku)
        {
            int min = 1;
            int max = 9;
            try
            {
                for (int row = 0; row < sudoku.GetLength(0); ++row)
                {
                    for (int col = 0; col < sudoku.GetLength(1); ++col)
                    {
                        if (sudoku[row, col] < min || sudoku[row, col] > max)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        private bool CheckDuplicate(int[,] sudoku, int fixedIndex)
        {
            int colSize = 9, rowSize = 9;
            var colFound = new bool[colSize];
            var rowFound = new bool[rowSize];
            int colIndex, rowIndex;

            try
            {
                for (int incr = 0; incr < colSize; ++incr)
                {
                    colIndex = sudoku[fixedIndex, incr] - 1;
                    rowIndex = sudoku[incr, fixedIndex] - 1;

                    if (colFound[colIndex] || rowFound[rowIndex])
                    {
                        return false;
                    }
                    colFound[colIndex] = !colFound[colIndex] ? true : false;
                    rowFound[rowIndex] = !rowFound[rowIndex] ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }



        private bool checkSquare(int[,] sudoku, int baseRow, int baseCol, int innerSquareLength)
        {
            try
            {

                var found = new bool[9];
                for (int row = baseRow; row < (baseRow + innerSquareLength); ++row)
                {
                    for (int col = baseCol; col < (baseCol + innerSquareLength); ++col)
                    {
                        int index = sudoku[row, col] - 1;
                        if (!found[index])
                            found[index] = true;
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {   
                throw ex;
            }
            return true;
        }
    }
}

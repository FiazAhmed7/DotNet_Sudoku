using System;
using NUnit.Framework;
using Sudoku;

namespace Sudoku.Tests
{
    [TestFixture]
    public class SudokuValidatorTest
    {
        SudokuValidator sudo;

        [SetUp]
        public void SetUp()
        {
            //arrange
            sudo = new SudokuValidator();
        }

        [TearDown]
        public void TearDown()
        {
            sudo = null;
        }


        [Test]
        public void LoadFile_ValidFile_CreatesSudokuArray()
        {
            //act
            var arr = sudo.LoadFile(@"C:\Fiaz\DotNet\input_sudoku.txt");

            //assert
            Assert.That(arr, Is.All.Not.Null);
            Assert.That(arr, Is.All.GreaterThan(0).And.LessThan(10));
            Assert.That(arr, Is.All.InstanceOf<Int32>());
            Assert.That(arr.GetLongLength(0), Is.EqualTo(9));
            Assert.That(arr.GetLongLength(1), Is.EqualTo(9));
        }


        [Test]
        public void LoadFile_ValidFile_SudokuArrayHasCorrectValues()
        {
            //arrange
            int[,] expected = new int[,]
            {
                {5,3,4,6,7,8,9,1,2},                {6,7,2,1,9,5,3,4,8},
                {1,9,8,3,4,2,5,6,7},                {8,5,9,7,6,1,4,2,3},                {4,2,6,8,5,3,7,9,1},                {7,1,3,9,2,4,8,5,6},                {9,6,1,5,3,7,2,8,4},                {2,8,7,4,1,9,6,3,5},                {3,4,5,2,8,6,1,7,9}
            };

            //act
            var arr = sudo.LoadFile(@"C:\Fiaz\DotNet\input_sudoku.txt");

            //assert
            Assert.That(arr, Is.EquivalentTo(expected));
        }


        [Test]
        public void LoadFile_InValidFile_ExceptionThrown()
        {
            //assert
            Assert.That(() => sudo.LoadFile(@"C:\Fiaz\DotNet\some_invalid.txt"), 
                        Throws.Exception);
        }


        [Test]
        public void LoadFile_ExtraValues_IndexOutOfBoundExceptionThrown()
        {

            //assert
            Assert.That(() => sudo.LoadFile(@"C:\Fiaz\DotNet\input_sudoku_extraColumn.txt"),
                Throws.TypeOf<IndexOutOfRangeException>());
        }

            


        [Test]
        public void Validate_CorrectInput_ReturnsValid()
        {
            //arrange
            int[,] init = new int[,]
            {
                {5,3,4,6,7,8,9,1,2},                {6,7,2,1,9,5,3,4,8},
                {1,9,8,3,4,2,5,6,7},                {8,5,9,7,6,1,4,2,3},                {4,2,6,8,5,3,7,9,1},                {7,1,3,9,2,4,8,5,6},                {9,6,1,5,3,7,2,8,4},                {2,8,7,4,1,9,6,3,5},                {3,4,5,2,8,6,1,7,9}
            };

            //act
            bool result = sudo.Validate(init);

            //assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Validate_InCorrectInput_ReturnsInValid()
        {
            //arrange
            int[,] init = new int[,]
            {
                {5,3,4,6,7,8,9,1,9}, //chnging init[0,8] from 2 to 9                {6,7,2,1,9,5,3,4,8},
                {1,9,8,3,4,2,5,6,7},                {8,5,9,7,6,1,4,2,3},                {4,2,6,8,5,3,7,9,1},                {7,1,3,9,2,4,8,5,6},                {9,6,1,5,3,7,2,8,4},                {2,8,7,4,1,9,6,3,5},                {3,4,5,2,8,6,1,7,9}
            };

            //act
            bool result = sudo.Validate(init);

            //assert
            Assert.That(result, Is.False);
        }


        [Test]
        public void Validate_NegativeNumbers_ReturnsInValid()
        {
            //arrange
            int[,] init = new int[,]
            {
                {5,3,4,6,7,8,9,1,-2}, //chnging init[0,8] from 2 to -2                {6,7,2,1,9,5,3,4,8},
                {1,9,8,3,4,2,5,6,7},                {8,5,9,7,6,1,4,2,3},                {4,2,6,8,5,3,7,9,1},                {7,1,3,9,2,4,8,5,6},                {9,6,1,5,3,7,2,8,4},                {2,8,7,4,1,9,6,3,5},                {3,4,5,2,8,6,1,7,9}
            };

            //act
            bool result = sudo.Validate(init);

            //assert
            Assert.That(result, Is.False);
        }


        [Test]
        public void Validate_ExtraValues_ReturnsInValid()
        {
            //arrange
            int[,] init = new int[,]
            {
                {5,3,4,6,7,8,9,1,2, 6}, //adding another column of values                {6,7,2,1,9,5,3,4,8, 6},
                {1,9,8,3,4,2,5,6,7, 1},                {8,5,9,7,6,1,4,2,3, 2},                {4,2,6,8,5,3,7,9,1, 3},                {7,1,3,9,2,4,8,5,6, 4},                {9,6,1,5,3,7,2,8,4, 5},                {2,8,7,4,1,9,6,3,5, 6},                {3,4,5,2,8,6,1,7,9, 7}
            };

            //act
            bool result = sudo.Validate(init);

            //assert
            Assert.That(result, Is.False);
        }


        [Test]
        public void Validate_LessValues_ReturnsInValid()
        {
            //arrange
            int[,] init = new int[,]
            {
                {5,3,4,6,7,8,9,1}, //adding another column of values                {6,7,2,1,9,5,3,4},
                {1,9,8,3,4,2,5,6},                {8,5,9,7,6,1,4,2},                {4,2,6,8,5,3,7,3},                {7,1,3,9,2,4,8,5},                {9,6,1,5,3,7,2,8},                {2,8,7,4,1,9,6,3},                {3,4,5,2,8,6,1,7}
            };

            //act
            bool result = sudo.Validate(init);

            //assert
            Assert.That(result, Is.False);
        }

    }
}

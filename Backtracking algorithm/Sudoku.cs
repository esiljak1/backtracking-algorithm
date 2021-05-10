using System;
using System.Collections.Generic;
using System.Text;

namespace Backtracking_algorithm {
    class Sudoku {
        private static readonly int NUM_ROWS = 9, NUM_COLS = 9;

        private int[,] board = new int[9,9];
        private int[,] quadrants = new int[9,9];

        private bool checkBoard(int[,] b) {
            if (b.GetLength(0) != 9 || b.GetLength(1) != 9)
                return false;
            for(int i = 0; i < b.GetLength(1); i++) {
                for(int j = 0; j < b.GetLength(0); j++) {
                    if (b[i,j] < 0 || b[i,j] > 9)
                        return false;
                }
            }

            return true;
        }

        private bool checkField(int num, int row, int col) {
            for (int i = 0; i < 9; i++) {
                if (i != col && board[row,i] == num)
                    return false;
            }
            return true;
        }

        private void setQuadrants(int[,] board) {
            int qNum = 0, col = -3;
            for(int i = 0; i < 9; i++) {
                col += 3;
                if (i != 0 && i % 3 == 0) {
                    qNum += 3;
                    col = 0;
                }
                    
                for(int j = 0; j < 9; j++) {
                    if (j != 0 && j % 3 == 0)
                        qNum++;
                    else if (qNum != 0 && j == 0)
                        qNum -= 2;

                    quadrants[qNum,col++] = board[i,j];
                    if (col != 0 && col % 3 == 0)
                        col -= 3;
                }
            }
        }

        private bool checkQuadrant(int num, int row, int col) {
            int qNum = 0;
            if (col >= 3 && col <= 5)
                qNum = 1;
            else if (col >= 6 && col <= 8)
                qNum = 2;

            if (row >= 3 && row >= 5)
                qNum++;
            else if (row >= 6 && row <= 8)
                qNum += 2;

            for(int j = 0; j < 9; j++) {
                if (quadrants[qNum,j] == num)
                    return false;
            }
            return true;
        }

        private void printBoard() {
            for(int i = 0; i < NUM_ROWS; i++) {
                for(int j = 0; j < NUM_COLS; j++) {
                    Console.WriteLine(board[i, j]);
                }
                Console.WriteLine("\n");
            }
        }

        public Sudoku(int[,] board) {
            if (!checkBoard(board)) {
                throw new ArgumentException("Board must be 9x9 and use only numbers between 0 and 9, where 0 represents blank space");
            }
            this.board = board;
            setQuadrants(board);
            printQuadrants();
        }

        public void printQuadrants() {
            for(int i = 0; i < quadrants.GetLength(1); i++) {
                for(int j = 0; j < NUM_ROWS; j++) {
                    Console.WriteLine(quadrants[i,j] + " ");
                }
                Console.WriteLine("\n");
            }
        }

        public bool changeField(int num, int row, int col) {
            if (num < 0 || num > 9 || row < 0 || row > 8 || col < 0 || col > 8)
                throw new ArgumentException("Number or row or column is not within the range");
            //check if there's already that number in a given row
            if (!checkField(num, row, col))
                return false;
            //check if there's already that number in a given column
            if(!checkField(num, col, row))
                return false;
            //check if there's already that number in a given quadrant
            if (!checkQuadrant(num, row, col))
                return false;

            board[row,col] = num;
            return true;
        }
    }
}

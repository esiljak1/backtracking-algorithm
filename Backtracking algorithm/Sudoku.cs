using System;
using System.Collections.Generic;
using System.Text;

namespace Backtracking_algorithm {
    class Sudoku {
        public static readonly int NUM_ROWS = 9, NUM_COLS = 9;

        private List<List<int>> initialBoard = new List<List<int>>();
        private List<List<int>> quadrants = new List<List<int>>();

        private List<List<int>> playingBoard = new List<List<int>>();

        private bool checkBoard(List<List<int>> b) {
            if (b.Count != 9)
                return false;
            for (int i = 0; i < b.Count; i++) {
                if (b[i].Count != 9)
                    return false;
                for (int j = 0; j < b[i].Count; j++) {
                    if (b[i][j] < 0 || b[i][j] > 9)
                        return false;
                }
            }

            return true;
        }

        private bool checkColumn(int num, int col) {
            for (int i = 0; i < 9; i++) {
                if (playingBoard[i][col] == num)
                    return false;
            }
            return true;
        }

        private void setQuadrants(List<List<int>> board) {
            quadrants = board.ConvertAll(list => new List<int>(list));
            int qNum = 0, col = -3;
            for (int i = 0; i < 9; i++) {
                col += 3;
                if (i != 0 && i % 3 == 0) {
                    qNum += 3;
                    col = 0;
                }

                for (int j = 0; j < 9; j++) {
                    if (j != 0 && j % 3 == 0)
                        qNum++;
                    else if (qNum != 0 && j == 0)
                        qNum -= 2;

                    quadrants[qNum][col++] = board[i][j];
                    if (col != 0 && col % 3 == 0)
                        col -= 3;
                }
            }
        }

        private int getQuadrant(int row, int col) {
            int qNum = 0;
            if (col >= 3 && col <= 5)
                qNum = 1;
            else if (col >= 6 && col <= 8)
                qNum = 2;

            if (row >= 3 && row >= 5)
                qNum++;
            else if (row >= 6 && row <= 8)
                qNum += 2;

            return qNum;
        }
        private bool checkQuadrant(int num, int row, int col) {
            int qNum = getQuadrant(row, col);

            for (int j = 0; j < 9; j++) {
                if (quadrants[qNum][j] == num)
                    return false;
            }
            return true;
        }

        private void addToQuadrant(int num, int row, int col) {
            int qNum = getQuadrant(row, col);
            for(int j = 0; j < quadrants[qNum].Count; j++) {
                if(quadrants[qNum][j] == playingBoard[row][col]) {
                    quadrants[qNum][j] = num;
                    return;
                }
            }
        }

        public void printInitialBoard() {
            for (int i = 0; i < NUM_ROWS; i++) {
                for (int j = 0; j < NUM_COLS; j++) {
                    Console.Write(initialBoard[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void printPlayingBoard() {
            for (int i = 0; i < NUM_ROWS; i++) {
                for (int j = 0; j < NUM_COLS; j++) {
                    Console.Write(playingBoard[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        public int get(int row, int col){
            return playingBoard[row][col];  
        }

        public Sudoku(List<List<int>> board) {
            if (!checkBoard(board)) {
                throw new ArgumentException("Board must be 9x9 and use only numbers between 0 and 9, where 0 represents blank space");
            }

            initialBoard = board.ConvertAll(list => new List<int>(list));
            playingBoard = board.ConvertAll(list => new List<int>(list));

            setQuadrants(board);
        }

        private void printQuadrants() {
            for(int i = 0; i < quadrants.Count; i++) {
                for(int j = 0; j < NUM_ROWS; j++) {
                    Console.WriteLine(quadrants[i][j] + " ");
                }
                Console.WriteLine("\n");
            }
        }

        public bool changeField(int num, int row, int col) {
            if (num < 0 || num > 9 || row < 0 || row > 8 || col < 0 || col > 8)
                throw new ArgumentException("Number or row or column is not within the range");
            //check if cell can be changed
            if (initialBoard[row][col] != 0)
                throw new ArithmeticException("You cannot change this cell");
            if(num == 0) {
                playingBoard[row][col] = num;
                return true;
            }

            //check if there's already that number in a given row
            if (playingBoard[row].Contains(num))
                return false;
            //check if there's already that number in a given column
            if(!checkColumn(num, col))
                return false;
            //check if there's already that number in a given quadrant
            if (!checkQuadrant(num, row, col))
                return false;

            addToQuadrant(num, row, col);
            playingBoard[row][col] = num;
            return true;
        }
    }
}

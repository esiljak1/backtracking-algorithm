using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backtracking_algorithm {
    class Sudoku {
        public static readonly int NUM_ROWS = 9, NUM_COLS = 9;

        private List<List<int>> initialBoard = new List<List<int>>();
        private List<List<int>> quadrants = new List<List<int>>();

        private List<List<int>> playingBoard = new List<List<int>>();
        private bool isSolved = false;

        public bool IsSolved { get => isSolved;}

        private static bool checkRowsAndCols(List<List<int>> board, int row, int col) {
            int num = board[row][col];
            for(int j = 0; j < NUM_COLS; j++) {
                if (j != col && board[row][j] == num)
                    return false;
                if (j != row && board[j][col] == num)
                    return false;
            }
            return true;
        }

        private bool checkBoardState(List<List<int>> b) {
            for(int i = 0; i < NUM_ROWS; i++) {
                for(int j = 0; j < NUM_COLS; j++) {
                    if(b[i][j] != 0) {
                        if (!checkRowsAndCols(b, i, j))
                            return false;
                    }
                }
            }
            return true;
        }

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
            quadrantValidation();
        }

        private int getQuadrant(int row, int col) {
            int qNum = 0;
            if (col >= 3 && col <= 5)
                qNum = 1;
            else if (col >= 6 && col <= 8)
                qNum = 2;

            if (row >= 3 && row <= 5)
                qNum += 3;
            else if (row >= 6 && row <= 8)
                qNum += 6;

            return qNum;
        }

        private bool checkQuadrant(int num, int qNum) {
            if (num == 0)
                return true;
            return quadrants[qNum].Where(x => x == num).ToList().Count <= 1;
        }
        private void quadrantValidation() {
            int qNum = 0;
            foreach(var l in quadrants) {
                foreach(int num in l) {
                    if(!checkQuadrant(num, qNum)) {
                        quadrants.Clear();
                        throw new ArgumentException("No number can repeat itself in the same quadrant (except for 0)");
                    }
                        
                }
                qNum++;
            }
        }
        private bool checkQuadrant(int num, int row, int col) {
            if (num == 0)
                return true;
            int qNum = getQuadrant(row, col);

            return !quadrants[qNum].Any(x => x == num);
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

        private void checkIfSolved() {
            foreach(var l in playingBoard) {
                if (l.Contains(0)) {
                    isSolved = false;
                    return;
                }
            }
            isSolved = true;
        }

        private void print(List<List<int>> b) {
            for (int i = 0; i < NUM_ROWS; i++) {
                for (int j = 0; j < NUM_COLS; j++) {
                    Console.Write(b[i][j] + " ");
                    if (j % 3 == 2 && j != NUM_COLS - 1)
                        Console.Write("| ");
                }
                if (i % 3 == 2 && i != NUM_ROWS - 1) {
                    Console.WriteLine();
                    Console.Write("----------------------");
                }
                Console.WriteLine();
            }
        }

        public void printInitialBoard() {
            print(initialBoard);
        }

        public void printPlayingBoard() {
            print(playingBoard);
        }

        public int get(int row, int col){
            return playingBoard[row][col];  
        }

        public Sudoku(List<List<int>> board) {
            if (!checkBoard(board)) {
                throw new ArgumentException("Board must be 9x9 and use only numbers between 0 and 9, where 0 represents blank space");
            }

            if (!checkBoardState(board)) {
                throw new ArgumentException("No number can be repeated in the same row and/or column except for 0");
            }

            setQuadrants(board);

            initialBoard = board.ConvertAll(list => new List<int>(list));
            playingBoard = board.ConvertAll(list => new List<int>(list));
        }

        public bool changeField(int num, int row, int col) {
            if (num < 0 || num > 9 || row < 0 || row > 8 || col < 0 || col > 8)
                throw new ArgumentException("Number or row or column is not within the range");
            //check if cell can be changed
            if (initialBoard[row][col] != 0)
                throw new ArithmeticException("You cannot change this cell");
            if(num == 0) {
                addToQuadrant(num, row, col);
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
            checkIfSolved();
            return true;
        }
    }
}

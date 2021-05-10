using System;
using System.Collections.Generic;
using System.Text;

namespace Backtracking_algorithm {
    class Sudoku {
        private int[][] board = new int[9][];

        private bool checkBoard(int[][] b) {
            if (b.Length != 9)
                return false;
            for(int i = 0; i < b.Length; i++) {
                if (b[i].Length != 9)
                    return false;
                for(int j = 0; j < b[i].Length; j++) {
                    if (b[i][j] < 0 || b[i][j] > 9)
                        return false;
                }
            }

            return true;
        }

        public Sudoku(int[][] board) {
            if (!checkBoard(board)) {
                throw new ArgumentException("Board must be 9x9 and use only numbers between 0 and 9, where 0 represents blank space");
            }
            this.board = board;
        }

        public bool changeField(int num, int row, int col) {
            if (num < 0 || num > 9 || row < 0 || row > 8 || col < 0 || col > 8)
                throw new ArgumentException("Number or row or column is not within the range");
            for(int i = 0; i < board.Length; i++) {
                if (i != col && board[row][i] == num)
                    return false;
            }
            for(int i = 0; i < board[row].Length; i++) {
                if (i != row && board[i][col] == num)
                    return false;
            }
            board[row][col] = num;
            return true;
        }
    }
}

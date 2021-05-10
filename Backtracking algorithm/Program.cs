﻿using System;

namespace Backtracking_algorithm {
    class Program {
        static void Main(string[] args) {
            int[,] board = new int[,] {
                {3, 0, 6, 5, 0, 8, 4, 0, 0 },
                {5, 2, 0, 0, 0, 0, 0, 0, 0 },
                {0, 8, 7, 0, 0, 0, 0, 3, 1 },
                {0, 0, 3, 0, 1, 0, 0, 8, 0 },
                {9, 0, 0, 8, 6, 3, 0, 0, 5 },
                {0, 5, 0, 0, 9, 0, 6, 0, 0 },
                {1, 3, 0, 0, 0, 0, 2, 5, 0 },
                {0, 0, 0, 0, 0, 0, 0, 7, 4 },
                {0, 0, 5, 2, 0, 6, 3, 0, 0 }
            };
            Sudoku sudoku = new Sudoku(board);
        }
    }
}

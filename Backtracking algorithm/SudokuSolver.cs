using System;
using System.Collections.Generic;
using System.Text;

namespace Backtracking_algorithm {
    class SudokuSolver {
        public static void solve(Sudoku sudoku) {

            for(int i = 0; i < Sudoku.NUM_ROWS; i++) {
                for(int j = 0; j < Sudoku.NUM_COLS; j++) {
                    if(sudoku.get(i, j) == 0) {
                        if (!checkSolution(sudoku, i, j))
                            return;
                    }
                }
            }

            Console.WriteLine("Solved!");
        }

        private static bool checkSolution(Sudoku sudoku, int row, int col) {
            for(int num = 1; num <= 9; num++) {
                if (!sudoku.IsSolved && sudoku.changeField(num, row, col))
                    solve(sudoku);
            }
            if(!sudoku.IsSolved)
                sudoku.changeField(0, row, col);

            return false;
        }
    }
}

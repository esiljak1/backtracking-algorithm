using System;
using System.Collections.Generic;

namespace Backtracking_algorithm {
    class Program {
        static void Main(string[] args) {
            /*List<List<int>> board = new List<List<int>> {
                new List<int>{3, 0, 6, 5, 0, 8, 4, 0, 0 },
                new List<int>{5, 6, 0, 0, 0, 0, 0, 0, 0 },
                new List<int>{0, 8, 7, 0, 0, 0, 0, 3, 1 },
                new List<int>{0, 0, 3, 0, 1, 0, 0, 8, 0 },
                new List<int>{9, 0, 0, 8, 6, 3, 0, 0, 5 },
                new List<int>{0, 5, 0, 0, 9, 0, 6, 0, 0 },
                new List<int>{1, 3, 0, 0, 0, 0, 2, 5, 0 },
                new List<int>{0, 0, 0, 0, 0, 0, 0, 7, 4 },
                new List<int>{0, 0, 5, 2, 0, 6, 3, 0, 0 }
            };
            Sudoku sudoku = new Sudoku(board);
            SudokuSolver.solve(sudoku);

            sudoku.printPlayingBoard();*/

            List<List<int>> board = new List<List<int>>();
            Console.WriteLine("Please enter the numbers for initial sudoku board (0 for empty cell): \n");
            for (int i = 0; i < 9; i++) {
                board.Add(new List<int>());
                Console.WriteLine("Row " + i + ":");
                for (int j = 0; j < 9; j++) {
                    int num = int.Parse(Console.ReadLine());
                    board[i].Add(num);
                }
                Console.WriteLine();
            }
            try {
                Sudoku sudoku = new Sudoku(board);
                SudokuSolver.solve(sudoku);

                if (sudoku.IsSolved) {
                    Console.WriteLine();
                    sudoku.printPlayingBoard();
                }
            }catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}

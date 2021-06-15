using System;

namespace DifferentTicTacToe
{
    public class ComputerAI
    {

        // $G$ NTT-007 (-10) There's no need to re-instantiate the Random instance each time it is used.
        public static void BestMove(GameBoard i_TestBoard, GameCell i_NextMove)
        {
            int sizeOfBoard = i_TestBoard.Length;

            if (sizeOfBoard != 3)
            {
                Random randomValuesFoeCells = new Random();
                int row = randomValuesFoeCells.Next(0, i_TestBoard.Length);
                int col = randomValuesFoeCells.Next(0, i_TestBoard.Length);
                while (!i_TestBoard.Board[row, col].IsCellEmpty())
                {
                    row = randomValuesFoeCells.Next(0, i_TestBoard.Length);
                    col = randomValuesFoeCells.Next(0, i_TestBoard.Length);
                }

                i_NextMove.Row = row;
                i_NextMove.Col = col;
                i_NextMove.CellContent = CellSignsWrapper.CellSigns.SignedCellX;
            }
            else
            {
                int bestScoreOfAlgorithm = int.MaxValue;
                bestScoreOfAlgorithm *= -1; // minus infinity (-inf)
                for (int i = 0; i < sizeOfBoard; i++)
                {
                    for (int j = 0; j < sizeOfBoard; j++)
                    {
                        if (i_TestBoard.Board[i, j].IsCellEmpty())
                        {
                            i_TestBoard.Board[i, j].CellContent = CellSignsWrapper.CellSigns.SignedCellX;
                            GameCell currentCell = i_TestBoard.Board[i, j];
                            i_TestBoard.AmountOfFullCells++;
                            int currentMoveScore = MiniMaxAlgorithm(i_TestBoard, false, currentCell);
                            i_TestBoard.Board[i, j].CellContent = CellSignsWrapper.CellSigns.EmptySignedCell; // Undo the move I tried
                            i_TestBoard.AmountOfFullCells--;
                            if (currentMoveScore > bestScoreOfAlgorithm)
                            {
                                bestScoreOfAlgorithm = currentMoveScore;
                                i_NextMove.Col = currentCell.Col;
                                i_NextMove.Row = currentCell.Row;
                                i_NextMove.CellContent = CellSignsWrapper.CellSigns.SignedCellX;
                            }
                        }
                    }
                }
            }
        }

        public static int MiniMaxAlgorithm(GameBoard i_TestBoard, bool i_IsMaximazing, GameCell i_LastFilledCell)
        {
            int finiteStateScore = finitStateMiniMax(i_TestBoard, i_LastFilledCell);
            if (finiteStateScore == 1 || finiteStateScore == -1 || finiteStateScore == 0)
            {
                return finiteStateScore;
            }
            else
            {
                int size = i_TestBoard.Length;
                int currentMoveScore;
                int bestScoreOfAlgorithm = int.MaxValue;
                if (i_IsMaximazing)
                {
                    bestScoreOfAlgorithm *= -1; // minus infinity (-inf)
                }

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i_TestBoard.Board[i, j].IsCellEmpty())
                        {
                            i_TestBoard.AmountOfFullCells++;
                            if (i_IsMaximazing == true)
                            {
                                i_TestBoard.Board[i, j].CellContent = CellSignsWrapper.CellSigns.SignedCellX;
                                GameCell currentCell = i_TestBoard.Board[i, j];
                                currentMoveScore = MiniMaxAlgorithm(i_TestBoard, false, currentCell);
                                bestScoreOfAlgorithm = Math.Max(currentMoveScore, bestScoreOfAlgorithm);
                            }
                            else
                            {
                                i_TestBoard.Board[i, j].CellContent = CellSignsWrapper.CellSigns.SignedCellO;
                                GameCell currentCell = i_TestBoard.Board[i, j];
                                currentMoveScore = MiniMaxAlgorithm(i_TestBoard, true, currentCell);
                                bestScoreOfAlgorithm = Math.Min(currentMoveScore, bestScoreOfAlgorithm);
                            }

                            i_TestBoard.AmountOfFullCells--;
                            i_TestBoard.Board[i, j].CellContent = CellSignsWrapper.CellSigns.EmptySignedCell; // Undo the move I tried
                        }
                    }
                }

                return bestScoreOfAlgorithm;
            }
        }

        private static int finitStateMiniMax(GameBoard i_TestBoard, GameCell i_LastFilledCell)
        {
            bool result = i_TestBoard.CheckForFullLine(i_LastFilledCell);
            int score = int.MaxValue;
            score *= -1;

            if (result == true)
            {
                if (i_LastFilledCell.CellContent == CellSignsWrapper.CellSigns.SignedCellO)
                {
                    score = 1; // meaning O lost (computer won)
                }
                else
                {
                    score = -1; // meaning X lost, human won
                }
            }
            else
            {
                result = i_TestBoard.CheckIfBoardFull();
                if (result == true)
                {
                    score = 0;
                }
            }

            return score;
        }
    }
}
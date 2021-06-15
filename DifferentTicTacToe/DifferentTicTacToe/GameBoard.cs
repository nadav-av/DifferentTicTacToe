using System;

namespace DifferentTicTacToe
{
    public class GameBoard
    {
        private readonly int r_BoardLength;
        private readonly GameCell[,] r_GameBoard;
        private int m_AmountOfFullCells;

        public GameBoard(int i_BoardLength)
        {
            m_AmountOfFullCells = 0;
            r_BoardLength = i_BoardLength;
            r_GameBoard = new GameCell[i_BoardLength, i_BoardLength];

            for (int i = 0; i < r_BoardLength; i++)
            {
                for (int j = 0; j < r_BoardLength; j++)
                {
                    r_GameBoard[i, j] = new GameCell(CellSignsWrapper.CellSigns.EmptySignedCell, i, j);
                }
            }
        }

        public int Length
        {
            get
            {
                return r_BoardLength;
            }
        }

        public GameCell[,] Board
        {
            get
            {
                return r_GameBoard;
            }
        }

        public int AmountOfFullCells
        {
            get
            {
                return m_AmountOfFullCells;
            }

            set
            {
                m_AmountOfFullCells = value;
            }
        }

        public GameCell GetCellFromBoard(int i_Row, int i_Col)
        {
            return r_GameBoard[i_Row, i_Col];
        }

        public void InitializeBoard()
        {
            AmountOfFullCells = 0;
            for (int i = 0; i < r_BoardLength; i++)
            {
                for (int j = 0; j < r_BoardLength; j++)
                {
                    r_GameBoard[i, j].CellContent = CellSignsWrapper.CellSigns.EmptySignedCell;
                    r_GameBoard[i, j].Col = j;
                    r_GameBoard[i, j].Row = i;
                }
            }
        }

        public GameBoard CloneBoard()
        {
            GameBoard cloned = new GameBoard(r_BoardLength);
            for (int i = 0; i < r_BoardLength; i++)
            {
                for (int j = 0; j < r_BoardLength; j++)
                {
                    if (this.Board[i, j].CellContent != CellSignsWrapper.CellSigns.EmptySignedCell)
                    {
                        cloned.MarkCell(i, j, this.Board[i, j].CellContent);
                    }
                }
            }

            return cloned;
        }

        public void MarkCell(int i_Row, int i_Col, CellSignsWrapper.CellSigns i_SignToFill)
        {
            r_GameBoard[i_Row, i_Col].CellContent = i_SignToFill;
            m_AmountOfFullCells++;
        }

        public bool InputCellStringParse(string i_InputString, ref int io_RowToFill, ref int io_ColToFill, ref bool io_IsQuit)
        {
            bool parseSuccess = false;
            int legalSize = this.Length;
            
            if (i_InputString.Length == 1)
            {
                if (i_InputString[0] == Constants.Q)
                {
                    parseSuccess = true;
                    io_RowToFill = -1;
                    io_ColToFill = -1;
                    io_IsQuit = true;
                }
            }
            else if (i_InputString.Length == 3 && i_InputString[1] == ',')
            {
                io_RowToFill = (int)char.GetNumericValue(i_InputString[0]);
                io_ColToFill = (int)char.GetNumericValue(i_InputString[2]);
                if (io_RowToFill > 0 && io_RowToFill <= legalSize && io_ColToFill > 0 && io_ColToFill <= legalSize)
                {
                    parseSuccess = true;
                }
            }

            return parseSuccess;
        }

        public bool CheckIfBoardFull()
        {
            bool isFull = false;
            int poweredLength = this.Length * this.Length;

            if (poweredLength == m_AmountOfFullCells)
            {
                isFull = true;
            }

            return isFull;
        }

        public bool CheckForFullLine(GameCell i_cell)
        {
            bool thereIsFullLine = false;
            int row = i_cell.Row;
            int col = i_cell.Col;
            CellSignsWrapper.CellSigns sign = i_cell.CellContent;

            if (checkForFullCol(col, sign))
            {
                thereIsFullLine = true;
            }
            else
            {
                if (checkForFullRow(row, sign))
                {
                    thereIsFullLine = true;
                }
                else
                {
                    if (checkForFullPriorDiagonal(sign))
                    {
                        {
                            thereIsFullLine = true;
                        }
                    }
                    else
                    {
                        if (checkForFullSeconderyDiagonal(sign))
                        {
                            {
                                thereIsFullLine = true;
                            }
                        }
                    }
                } 
            }

            return thereIsFullLine;
        }

        private bool checkForFullRow(int i_LastSignRow, CellSignsWrapper.CellSigns i_LastSign)
        {
            bool isSameSign = true;
            for (int i = 0; i < r_BoardLength; i++)
            {
                if (r_GameBoard[i_LastSignRow, i].CellContent != i_LastSign)
                {
                    isSameSign = false;
                }
            }

            return isSameSign;
        }

        private bool checkForFullCol(int i_LastSignCol, CellSignsWrapper.CellSigns i_LastSign)
        {
            bool isSameSign = true;
            for (int i = 0; i < r_BoardLength; i++)
            {
                if (r_GameBoard[i, i_LastSignCol].CellContent != i_LastSign)
                {
                    isSameSign = false;
                }
            }

            return isSameSign;
        }

        private bool checkForFullPriorDiagonal(CellSignsWrapper.CellSigns i_LastSign)
        {
            bool isSameSign = true;
            int currentRowInBoard = 0;
            int currentColInBoard = 0;

            while (currentRowInBoard != r_BoardLength)
            {
                if (r_GameBoard[currentRowInBoard, currentColInBoard].CellContent != i_LastSign)
                {
                    isSameSign = false;
                }

                currentRowInBoard++;
                currentColInBoard++;
            }

            return isSameSign;
        }

        private bool checkForFullSeconderyDiagonal(CellSignsWrapper.CellSigns i_LastSign)
        {
            bool isSameSign = true;
            int currentRowInBoard = 0;
            int currentColInBoard = r_BoardLength - 1;

            while (currentColInBoard >= 0)
            {
                if (r_GameBoard[currentRowInBoard, currentColInBoard].CellContent != i_LastSign)
                {
                    isSameSign = false;
                }

                currentRowInBoard++;
                currentColInBoard--;
            }

            return isSameSign;
        }
    }
}

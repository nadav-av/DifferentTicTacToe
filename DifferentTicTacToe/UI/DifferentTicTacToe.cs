using System;
using System.Text;
using DifferentTicTacToe;

namespace UI
{
    internal class DifferentTicTacToeUI
    {
        // $G$ CSS-999 (-3) this member should be readonly
        private Player m_Player1;
        private Player m_Player2;
        private GameBoard m_GameBoard;

        public DifferentTicTacToeUI(int i_BoardLength, bool i_TwoPlayersMode, CellSignsWrapper.CellSigns i_Player1Sign)
        {
            m_Player1 = new Player(i_Player1Sign, true);
            CellSignsWrapper.CellSigns player2Sign = CellSignsWrapper.CellSigns.SignedCellX;
            m_Player2 = new Player(player2Sign, i_TwoPlayersMode);
            m_GameBoard = new GameBoard(i_BoardLength);
        }

        public static DifferentTicTacToeUI GetGameInformation()
        {
            HeaderPrinter.PrintHeadLine();
            CellSignsWrapper.CellSigns player1ChosenSign = CellSignsWrapper.CellSigns.SignedCellO;
            bool isAgainstHuman = false;
            int boardLength = 0;
            getOtherPlayerType(ref isAgainstHuman);
            getBoardSizeFromUser(ref boardLength);
            DifferentTicTacToeUI game = new DifferentTicTacToeUI(boardLength, isAgainstHuman, player1ChosenSign);
            clearScreen();
            return game;
        }

        public void PlayDifferentTicTacToe()
        {
            bool gameOn = true;
            GameCell filledCell = new GameCell();
            Player currentPlayer = m_Player1;
            Player rivalPlayer = m_Player2;

            while (gameOn)
            {
                PrintGameBoard();
                Console.WriteLine("Player: {0}", (char)currentPlayer.Sign);
                bool retired = playATurn(currentPlayer, filledCell);
                clearScreen();
                if (retired)
                {
                    PrintGameBoard();
                    Console.WriteLine("Player: {0} won!", (char)rivalPlayer.Sign);
                    rivalPlayer.Points++;
                    this.printPointsStatus();
                    this.playAnotherRound(ref gameOn);
                }
                else if (currentPlayer.CheckIfPlayerLost(m_GameBoard, filledCell))
                {
                    PrintGameBoard();
                    Console.WriteLine("Player: {0} won!", (char)rivalPlayer.Sign);
                    rivalPlayer.Points++;
                    this.printPointsStatus();
                    this.playAnotherRound(ref gameOn);
                }
                else if (checkForADraw())
                {
                    PrintGameBoard();
                    Console.WriteLine("DRAW");
                    this.printPointsStatus();
                    this.playAnotherRound(ref gameOn);
                }

                changeCurrentPlayer(ref currentPlayer, ref rivalPlayer);
            }

            HeaderPrinter.PrintGoodbye();
        }

        public void PrintGameBoard()
        {
            int gameBoardLength = m_GameBoard.Length;
            StringBuilder gameBoard = new StringBuilder();
            for (int i = 0; i < gameBoardLength; i++)
            {
                gameBoard.AppendFormat("   {0}", i + 1);
            }

            gameBoard.AppendLine();

            for (int i = 0; i < gameBoardLength; i++)
            {
                gameBoard.AppendFormat("{0}|", i + 1);

                for (int j = 0; j < gameBoardLength; j++)
                {
                    gameBoard.Append((char)m_GameBoard.Board[i, j].CellContent);
                    gameBoard.Append("  |");
                }

                gameBoard.AppendLine();
                gameBoard.Append(" =");

                for (int j = 0; j < 4 * gameBoardLength; j++)
                {
                    gameBoard.Append("=");
                }

                gameBoard.AppendLine();
            }
            Console.WriteLine(gameBoard);
        }

        private static void clearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            HeaderPrinter.PrintHeadLine();
        }

        // $G$ CSS-999 (-3) You should have used constants\enum here.
        private static void getOtherPlayerType(ref bool io_IsAgainstHuman)
        {
            bool stringValidity = false;
            Console.Write("Do you want to play against the computer? (y/n): ");
            string inputString = Console.ReadLine();
            while (stringValidity == false)
            {
                if (inputString.Length != 1)
                {
                    stringValidity = false;
                }
                else if (inputString[0] == 'n')
                {
                    stringValidity = true;
                    io_IsAgainstHuman = true;
                }
                else if (inputString[0] == 'y')
                {
                    stringValidity = true;
                    io_IsAgainstHuman = false;
                }

                if (stringValidity == false)
                {
                    Console.Write(Environment.NewLine);
                    Console.Write("Invalid input. Do you want to play against the computer ? (y / n) : ");
                    inputString = Console.ReadLine();
                }
            }
        }

        // $G$ CSS-999 (-3) You should have used constants\enum here.
        private static void getBoardSizeFromUser(ref int io_BoardSize)
        {
            string inputString;
            bool stringValidity = false;
            Console.Write("Please choose your board size (enter 3-9 will represent 3X3 till 9X9): ");
            inputString = Console.ReadLine();
            while (stringValidity == false)
            {
                if (inputString.Length != 1)
                {
                    stringValidity = false;
                }

                else if (inputString[0] <= '9' && inputString[0] >= '3')
                {
                    stringValidity = true;
                    io_BoardSize = (int)char.GetNumericValue(inputString[0]);
                }

                if (stringValidity == false)
                {
                    Console.Write(Environment.NewLine);
                    Console.Write("Invalid input. Please choose your board size (enter 3-9 will represent 3X3 till 9X9) ");
                    inputString = Console.ReadLine();
                }
            }
        }

        private void printPointsStatus()
        {
            string statusOutput = string.Format(
@"
The points Status is:
Player: {0} have: {1} Points
Player: {2} have: {3} Points",
            (char)m_Player1.Sign,
            m_Player1.Points,
            (char)m_Player2.Sign,
            m_Player2.Points);
            Console.WriteLine(statusOutput);
        }

        // $G$ CSS-999 (-3) You should have used constants\enum here.
        private void playAnotherRound(ref bool io_GameOn)
        {
            bool stringValidity = false;
            Console.Write("Do you want to play another round with same settings? (y/n): ");
            string inputString = Console.ReadLine();
            while (stringValidity == false)
            {
                if (inputString.Length != 1)
                {
                    stringValidity = false;
                }
                else if (inputString[0] == 'n')
                {
                    stringValidity = true;
                    io_GameOn = false;
                }
                else if (inputString[0] == 'y')
                {
                    stringValidity = true;
                    io_GameOn = true;
                    m_GameBoard.InitializeBoard();
                    clearScreen();
                }

                if (stringValidity == false)
                {
                    Console.Write(Environment.NewLine);
                    Console.Write("Invalid input. Do you want to play another round with same settings? (y/n): ");
                    inputString = Console.ReadLine();
                }
            }
        }

        private void changeCurrentPlayer(ref Player io_CurrentPlayer, ref Player io_RivalPlayer)
        {
            Player tempPlayer = io_CurrentPlayer;
            io_CurrentPlayer = io_RivalPlayer;
            io_RivalPlayer = tempPlayer;
        }

        private bool checkForADraw()
        {
            return m_GameBoard.CheckIfBoardFull();
        }

        private void getCellToFill(ref int io_RowToFill, ref int io_ColToFill, ref bool io_IsQuit)
        {
            Console.WriteLine("Please Enter a cell to fill (for example 2,3). You can also quit with Q");
            string cellInput = Console.ReadLine();
            bool isValidInput = false;

            while (isValidInput == false)
            {
                isValidInput = m_GameBoard.InputCellStringParse(cellInput, ref io_RowToFill, ref io_ColToFill, ref io_IsQuit);
                if (isValidInput == false)
                {
                    Console.WriteLine("The values you entered is incorrect. Please Enter a cell to fill (for example 2,3)");
                    cellInput = Console.ReadLine();
                    isValidInput = m_GameBoard.InputCellStringParse(cellInput, ref io_RowToFill, ref io_ColToFill, ref io_IsQuit);
                }
            }
        }


        // $G$ DSN-002 (-20) No separation between the logical part of the game and the UI.
        private bool playATurn(Player i_Player, GameCell i_FilledCell)
        {
            bool retired = false;
            int row = 0;
            int col = 0;

            if (i_Player.IsHuman)
            {
                getCellToFill(ref row, ref col, ref retired);
                while (retired == false && !m_GameBoard.Board[row - 1, col - 1].IsCellEmpty())
                {
                    Console.Write("The cell you chose is full. ");
                    getCellToFill(ref row, ref col, ref retired);
                }

                row--;
                col--;
            }
            else
            {
                ComputerAI.BestMove(m_GameBoard, i_FilledCell);
                row = i_FilledCell.Row;
                col = i_FilledCell.Col;
            }

            if (row >= 0)
            {
                i_FilledCell.Col = col;
                i_FilledCell.Row = row;
                i_FilledCell.CellContent = i_Player.Sign;
                m_GameBoard.MarkCell(row, col, i_Player.Sign); // minus one cause matrix starts with 0
            }
            else
            { 
                retired = true;    
            }

            return retired;
        }
    }
}

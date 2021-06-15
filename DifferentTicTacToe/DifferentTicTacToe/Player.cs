using System;

namespace DifferentTicTacToe
{
    public class Player
    {
        private readonly CellSignsWrapper.CellSigns r_PlayerBoardSign;
        private bool m_IsHumanPlayer;
        private int m_PlayerVictoryPoints;

        public Player(CellSignsWrapper.CellSigns i_PlayerSign, bool i_IsHumanPlayer)
        {
            r_PlayerBoardSign = i_PlayerSign; 
            m_IsHumanPlayer = i_IsHumanPlayer;
            m_PlayerVictoryPoints = 0;
        }

        public bool IsHuman
        {
            get
            {
                return m_IsHumanPlayer;
            }

            set
            {
                m_IsHumanPlayer = value;
            }
        }

        public CellSignsWrapper.CellSigns Sign
        {
            get
            {
                return r_PlayerBoardSign;
            }
        }

        public int Points
        {
            get
            {
                return m_PlayerVictoryPoints;
            }

            set
            {
                m_PlayerVictoryPoints = value;
            }
        }

        public bool CheckIfPlayerLost(GameBoard i_GameBoard, GameCell i_LastFilledCell)
        {
            bool didPlayerLost = i_GameBoard.CheckForFullLine(i_LastFilledCell);
            return didPlayerLost;
        }
    }
}

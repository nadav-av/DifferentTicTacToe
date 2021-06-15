using System;

namespace DifferentTicTacToe
{
    public class GameCell
    {
        private int m_CellRow;
        private int m_CellCol;
        private CellSignsWrapper.CellSigns m_CellContent;

        public GameCell() : this(CellSignsWrapper.CellSigns.EmptySignedCell, 0, 0)
        {
        }
  
        public GameCell(CellSignsWrapper.CellSigns i_InitialSign, int i_Row, int i_Col)
        {
            m_CellContent = i_InitialSign;
            m_CellCol = i_Col;
            m_CellRow = i_Row;
        }

        public CellSignsWrapper.CellSigns CellContent
        {
            get
            {
                return m_CellContent;
            }

            set
            {
                m_CellContent = value;
            }
        }

        public int Row
        {
            get
            {
                return m_CellRow;
            }

            set
            {
                m_CellRow = value;
            }
        }

        public int Col
        {
            get
            {
                return m_CellCol;
            }

            set
            {
                m_CellCol = value;
            }
        }

        public bool IsCellEmpty()
        {
            return m_CellContent == CellSignsWrapper.CellSigns.EmptySignedCell;
        }
    }
}
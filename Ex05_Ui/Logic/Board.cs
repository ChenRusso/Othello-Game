using System.Collections.Generic;

namespace Ex05_Ui
{
    public class Board
    {
        private const int k_DividionForMatrix = 2;
        private readonly eMark[,] m_BoardOthello;
        private int m_BoardSize;
        private int m_EmptyPlacesOnBoard;

        public Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_BoardOthello = new eMark[m_BoardSize, m_BoardSize];
            InitBoard();
        }

        public eMark[,] BoardProperty
        {
            get
            {
                return m_BoardOthello;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        public int CountEmptyPlaces()
        {
            m_EmptyPlacesOnBoard = 0;

            for (int iIndexRow = 1; iIndexRow < m_BoardSize; iIndexRow++)
            {
                for (int jIndexCol = 1; jIndexCol < m_BoardSize; jIndexCol++)
                {
                    if (m_BoardOthello[iIndexRow, jIndexCol] == eMark.Blank)
                    {
                        m_EmptyPlacesOnBoard++;
                    }
                }
            }

            return m_EmptyPlacesOnBoard;
        }

        public void InitBoard()
        {
            for (int iIndexRow = 0; iIndexRow < m_BoardSize; iIndexRow++)
            {
                for (int jIndexCol = 0; jIndexCol < m_BoardSize; jIndexCol++)
                {
                    m_BoardOthello[iIndexRow, jIndexCol] = eMark.Blank;
                }
            }

            m_BoardOthello[(m_BoardSize / k_DividionForMatrix) - 1, (m_BoardSize / k_DividionForMatrix) - 1] = eMark.O;
            m_BoardOthello[(m_BoardSize / k_DividionForMatrix) - 1, m_BoardSize / k_DividionForMatrix] = eMark.X;
            m_BoardOthello[m_BoardSize / k_DividionForMatrix, (m_BoardSize / k_DividionForMatrix) - 1] = eMark.X;
            m_BoardOthello[m_BoardSize / k_DividionForMatrix, m_BoardSize / k_DividionForMatrix] = eMark.O;
        }

        public void PutMarkOnBoard(eMark i_CurrentEMark, int i_SelectedRow, int i_SelectedCol)
        {
            m_BoardOthello[i_SelectedRow, i_SelectedCol] = i_CurrentEMark;
        }

        public bool IsLegalStep(int i_SelectedRow, int i_SelectedCol, Player i_CurrentPlayer)
        {
            bool isLegalStep = false;

            if (m_BoardOthello[i_SelectedRow, i_SelectedCol] == eMark.Blank)
            {
                for (int iIndexRow = -1; iIndexRow <= 1; iIndexRow++)
                {
                    for (int jIndexCol = -1; jIndexCol <= 1; jIndexCol++)
                    {
                        if (!((iIndexRow == 0) && (jIndexCol == 0)) && neighborsCheck(i_CurrentPlayer, i_SelectedRow, i_SelectedCol, iIndexRow, jIndexCol))
                        {
                            isLegalStep = true;
                        }
                    }
                }
            }

            return isLegalStep;
        }

        private bool neighborsCheck(Player i_CurrentPlayer, int i_RowUserInput, int i_ColUserInput, int i_RowShifting, int i_ColShifting)
        {
            bool isPossibleToPlay = true;

            int rowToCheck = i_RowUserInput + i_RowShifting;
            int colToCheck = i_ColUserInput + i_ColShifting;

            while (rowToCheck >= 0 && rowToCheck <= m_BoardSize - 1 && colToCheck >= 0 &&
                   colToCheck <= m_BoardSize - 1 && m_BoardOthello[rowToCheck, colToCheck] == i_CurrentPlayer.GetOppositeMark())
            {
                rowToCheck += i_RowShifting;
                colToCheck += i_ColShifting;
            }

            if (rowToCheck < 0 || rowToCheck > m_BoardSize - 1 || colToCheck < 0 ||
                colToCheck > m_BoardSize - 1 || (rowToCheck - i_RowShifting == i_RowUserInput && colToCheck - i_ColShifting == i_ColUserInput) || m_BoardOthello[rowToCheck, colToCheck] != i_CurrentPlayer.PlayerMark)
            {
                isPossibleToPlay = false;
            }

            return isPossibleToPlay;
        }

        public void ChangeCurrentBoard(Player i_Player, int i_SelectedRow, int i_SelectedCol)
        {
            for (int iIndexForRow = -1; iIndexForRow <= 1; iIndexForRow++)
            {
                for (int jIndexForCol = -1; jIndexForCol <= 1; jIndexForCol++)
                {
                    if (neighborsCheck(i_Player, i_SelectedRow, i_SelectedCol, iIndexForRow, jIndexForCol))
                    {
                        m_BoardOthello[i_SelectedRow, i_SelectedCol] = i_Player.PlayerMark;
                        int rowToChange = i_SelectedRow + iIndexForRow;
                        int colToChange = i_SelectedCol + jIndexForCol;

                        while (m_BoardOthello[rowToChange, colToChange] != i_Player.PlayerMark)
                        {
                            m_BoardOthello[rowToChange, colToChange] = i_Player.PlayerMark;
                            rowToChange += iIndexForRow;
                            colToChange += jIndexForCol;
                        }
                    }
                }
            }
        }

        public void UpdatePlayersScore(ref Player io_FirstPlayer, ref Player io_SecondPlayer)
        {
            io_FirstPlayer.PlayerScore = 0;
            io_SecondPlayer.PlayerScore = 0;

            for (int iIndexForRow = 0; iIndexForRow < m_BoardSize; iIndexForRow++)
            {
                for (int jIndexForCol = 0; jIndexForCol < m_BoardSize; jIndexForCol++)
                {
                    if (m_BoardOthello[iIndexForRow, jIndexForCol] == io_FirstPlayer.PlayerMark)
                    {
                        io_FirstPlayer.PlayerScore += 1;
                    }

                    if (m_BoardOthello[iIndexForRow, jIndexForCol] == io_SecondPlayer.PlayerMark)
                    {
                        io_SecondPlayer.PlayerScore += 1;
                    }
                }
            }
        }

        public bool IsStepsLeft(Player i_CurrentPlayerToCheck, ref List<Coordinates> io_LegalStepForComputer)
        {
            bool isValid = false;
            io_LegalStepForComputer.Clear();
            for (int iIndexRow = 0; iIndexRow < BoardSize; iIndexRow++)
            {
                for (int jIndexCol = 0; jIndexCol < BoardSize; jIndexCol++)
                {
                    if (IsLegalStep(iIndexRow, jIndexCol, i_CurrentPlayerToCheck))
                    {
                        isValid = true;
                        Coordinates coordinates = new Coordinates();

                        coordinates.ColumnCoordinateToPlay = jIndexCol;
                        coordinates.RowCoordinateToPlay = iIndexRow;
                        io_LegalStepForComputer.Add(coordinates);
                    }
                }
            }

            return isValid;
        }
    }
}
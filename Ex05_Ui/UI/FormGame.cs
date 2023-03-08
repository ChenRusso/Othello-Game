using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05_Ui
{
    public partial class FormGame : Form
    {
        private readonly GameManager r_GameManger;
        private readonly OthelloPictureBox[,] r_OthelloBoard;
        private readonly bool r_IsComputer;
        private List<Coordinates> m_CurrentPlayerMoves;
        private int m_LeftOffset = 20;
        private int m_TopOffset = 20;
        private int m_CurrentIndex;

        public FormGame(int i_BoardSize, bool i_IsComputer, GameManager i_GameManager)
        {
            InitializeComponent();
            r_OthelloBoard = new OthelloPictureBox[i_BoardSize, i_BoardSize];
            initializeOthelloBoard(i_BoardSize);
            r_GameManger = i_GameManager;
            m_CurrentPlayerMoves = new List<Coordinates>();
            r_IsComputer = i_IsComputer;
            startNewGame(i_IsComputer);
        }

        private void initializeOthelloBoard(int i_BoardSize)
        {
            int pictureBoxSize = 40;
            int widthOffset = 50;
            int heightOffset = 70;
            Width = (i_BoardSize * pictureBoxSize) + widthOffset;
            Height = (i_BoardSize * pictureBoxSize) + heightOffset;

            for (int rowNumber = 0; rowNumber < i_BoardSize; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < i_BoardSize; columnNumber++)
                {
                    r_OthelloBoard[rowNumber, columnNumber] = createAndReturnNewPictureBox();
                    r_OthelloBoard[rowNumber, columnNumber].RowNumber = rowNumber;
                    r_OthelloBoard[rowNumber, columnNumber].ColumnNumber = columnNumber;
                    Controls.Add(r_OthelloBoard[rowNumber, columnNumber]);
                    m_LeftOffset += pictureBoxSize;
                }

                m_TopOffset += pictureBoxSize;
                m_LeftOffset = 20;
            }
        }

        private OthelloPictureBox createAndReturnNewPictureBox()
        {
            int pictureBoxSize = 40;
            int padding = 3;

            OthelloPictureBox othelloPictureBox = new OthelloPictureBox();
            othelloPictureBox.Size = new Size(pictureBoxSize, pictureBoxSize);
            othelloPictureBox.Padding = new Padding(padding);
            othelloPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            othelloPictureBox.Left = m_LeftOffset;
            othelloPictureBox.Top = m_TopOffset;
            othelloPictureBox.BorderStyle = BorderStyle.Fixed3D;

            return othelloPictureBox;
        }

        private void updateBoard()
        {
            Image redCoinImage = ResourcePhotos.CoinRed;
            Image yellowCoinImage = ResourcePhotos.CoinYellow;
            clearGameBoard();

            int boardSize = r_OthelloBoard.GetLength(0);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (r_GameManger.BoardOthello.BoardProperty[i, j] == eMark.X)
                    {
                        r_OthelloBoard[i, j].Image = redCoinImage;
                    }
                    else if (r_GameManger.BoardOthello.BoardProperty[i, j] == eMark.O)
                    {
                        r_OthelloBoard[i, j].Image = yellowCoinImage;
                    }
                    else
                    {
                        r_OthelloBoard[i, j].BackColor = Color.Empty;
                    }

                    r_OthelloBoard[i, j].Enabled = true;
                }
            }
        }

        private void startNewGame(bool i_IsComputer)
        {
            Player[] playersArray = new Player[2];

            playersArray[0] = r_GameManger.CreateFirstPlayer();
            playersArray[1] = r_GameManger.CreateSecondPlayer(i_IsComputer);

            updateBoard();
            playGame();
        }

        private void coloringLegalSteps(Player[] i_PlayersArray)
        {
            r_GameManger.BoardOthello.IsStepsLeft(i_PlayersArray[m_CurrentIndex], ref m_CurrentPlayerMoves);
            resetClicksOnBoard();
            updateBoard();

            foreach (Coordinates currentCoordinatesToCheck in m_CurrentPlayerMoves)
            {
                int rowCoordinateToPlay = currentCoordinatesToCheck.RowCoordinateToPlay;
                int columnCoordinateToPlay = currentCoordinatesToCheck.ColumnCoordinateToPlay;

                r_OthelloBoard[rowCoordinateToPlay, columnCoordinateToPlay].Enabled = true;
                r_OthelloBoard[rowCoordinateToPlay, columnCoordinateToPlay].BackColor = Color.LimeGreen;
                r_OthelloBoard[rowCoordinateToPlay, columnCoordinateToPlay].Click += buttonPictureBox_Click;
            }
        }

        private void buttonPictureBox_Click(object sender, EventArgs e)
        {
            OthelloPictureBox theSender = sender as OthelloPictureBox;

            r_GameManger.playTurn(r_GameManger.BoardOthello, ref m_CurrentIndex, theSender.RowNumber, theSender.ColumnNumber);
            playGame();
        }

        private void resetClicksOnBoard()
        {
            int sizeOfBoard = r_OthelloBoard.GetLength(0);

            for (int i = 0; i < sizeOfBoard; i++)
            {
                for (int j = 0; j < sizeOfBoard; j++)
                {
                    r_OthelloBoard[i, j].Click -= buttonPictureBox_Click;
                }
            }
        }

        private void clearGameBoard()
        {
            int sizeOfBoard = r_OthelloBoard.GetLength(0);

            for (int iRow = 0; iRow < sizeOfBoard; iRow++)
            {
                for (int jCol = 0; jCol < sizeOfBoard; jCol++)
                {
                    r_OthelloBoard[iRow, jCol].Enabled = false;
                    r_OthelloBoard[iRow, jCol].BackColor = Color.Gainsboro;
                    r_OthelloBoard[iRow, jCol].Image = null;
                }
            }
        }

        private void playGame()
        {
            coloringLegalSteps(r_GameManger.PlayersArray);

            Text = $"Othello - {r_GameManger.PlayersArray[m_CurrentIndex].PlayerName}'s turn";

            if (r_GameManger.BoardOthello.IsStepsLeft(r_GameManger.PlayersArray[0], ref m_CurrentPlayerMoves) || r_GameManger.BoardOthello.IsStepsLeft(r_GameManger.PlayersArray[1], ref m_CurrentPlayerMoves))
            {
                if (showNoValidSteps(r_GameManger.PlayersArray))
                {
                    if (r_GameManger.PlayersArray[m_CurrentIndex].PlayerName == "Computer")
                    {
                        computerTurn();
                    }
                }
                else
                {
                    coloringLegalSteps(r_GameManger.PlayersArray);
                }
            }
            else
            {
                string message = r_GameManger.WinnersStatus();
                DialogResult dialogResult = MessageBox.Show($"{message}", "Othello", MessageBoxButtons.YesNo);
                gameStatus(dialogResult);
            }
        }

        private void computerTurn()
        {
            updateBoard();
            Coordinates coordinates = r_GameManger.PlayersArray[m_CurrentIndex].PlayComputerTurn(m_CurrentPlayerMoves);
            r_GameManger.playTurn(r_GameManger.BoardOthello, ref m_CurrentIndex, coordinates.RowCoordinateToPlay, coordinates.ColumnCoordinateToPlay);
            playGame();
        }

        private void gameStatus(DialogResult i_DialogMessage)
        {
            switch (i_DialogMessage)
            {
                case DialogResult.Yes:
                    r_GameManger.BoardOthello.InitBoard();
                    m_CurrentIndex = 0;
                    startNewGame(r_IsComputer);
                    break;
                case DialogResult.No:
                    Close();
                    break;
            }
        }

        private bool showNoValidSteps(Player[] i_PlayersArray)
        {
            bool hasValidMove = true;

            if (!r_GameManger.BoardOthello.IsStepsLeft(i_PlayersArray[m_CurrentIndex], ref m_CurrentPlayerMoves))
            {
                MessageBox.Show($"No more valid moves for {i_PlayersArray[m_CurrentIndex].PlayerName}! Switch turns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                r_GameManger.SwitchTurns(ref m_CurrentIndex);

                Text = $"Othello - {i_PlayersArray[m_CurrentIndex].PlayerName}'s turn";

                if (i_PlayersArray[m_CurrentIndex].PlayerName == "Computer")
                {
                    updateBoard();
                    r_GameManger.BoardOthello.IsStepsLeft(i_PlayersArray[m_CurrentIndex], ref m_CurrentPlayerMoves);
                    Coordinates coordinates = i_PlayersArray[m_CurrentIndex].PlayComputerTurn(m_CurrentPlayerMoves);
                    r_GameManger.playTurn(r_GameManger.BoardOthello, ref m_CurrentIndex, coordinates.RowCoordinateToPlay, coordinates.ColumnCoordinateToPlay);
                    playGame();
                }

                hasValidMove = false;
            }

            return hasValidMove;
        }
    }
}

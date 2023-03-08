using System;
using System.Windows.Forms;

namespace Ex05_Ui
{
    public partial class FormLogin : Form
    {
        private const int k_MaxBoardSize = 12;
        private const int k_IncreaseBoardSize = 2;
        private int m_UserBoardSize = 6;
        private bool m_AgainstComputer;
        private bool m_StartGame;

        public FormLogin()
        {
            InitializeComponent();
        }

        public bool IsGameStart
        {
            get
            {
                return m_StartGame;
            }
        }

        public bool IsComputer
        {
            get
            {
                return m_AgainstComputer;
            }
        }

        public int UserBoardSize
        {
            get
            {
                return m_UserBoardSize;
            }
        }

        private void buttonIncreaseBoardSize_Click(object sender, EventArgs e)
        {
            Button increaseBoardSizeClick = sender as Button;

            if (m_UserBoardSize == k_MaxBoardSize)
            {
                m_UserBoardSize = 6;
            }
            else
            {
                m_UserBoardSize += k_IncreaseBoardSize;
            }

            increaseBoardSizeClick.Text = $"Board size: {m_UserBoardSize}x{m_UserBoardSize} (click to increase)";
        }

        private void buttonHumanPlayer_Click(object sender, EventArgs e)
        {
            m_StartGame = true;
            Close();
        }

        private void buttonComputerPlayer_Click(object sender, EventArgs e)
        {
            m_AgainstComputer = true;
            m_StartGame = true;
            Close();
        }
    }
}

using System.Windows.Forms;

namespace Ex05_Ui
{
    public class OthelloPictureBox : PictureBox
    {
        private int m_RowNumber;
        private int m_ColumnNumber;

        public int RowNumber
        {
            get
            {
                return m_RowNumber;
            }

            set
            {
                m_RowNumber = value;
            }
        }

        public int ColumnNumber
        {
            get
            {
                return m_ColumnNumber;
            }

            set
            {
                m_ColumnNumber = value;
            }
        }
    }
}

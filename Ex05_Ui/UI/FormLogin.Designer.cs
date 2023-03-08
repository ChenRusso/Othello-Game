
namespace Ex05_Ui
{
    partial class FormLogin
    {
            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
            this.m_ButtonIncreaseBoardSize = new System.Windows.Forms.Button();
            this.m_ButtonHumanPlayer = new System.Windows.Forms.Button();
            this.m_ButtonComputerPlayer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_ButtonIncreaseBoardSize
            // 
            this.m_ButtonIncreaseBoardSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_ButtonIncreaseBoardSize.Location = new System.Drawing.Point(68, 31);
            this.m_ButtonIncreaseBoardSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_ButtonIncreaseBoardSize.Name = "m_ButtonIncreaseBoardSize";
            this.m_ButtonIncreaseBoardSize.Size = new System.Drawing.Size(380, 49);
            this.m_ButtonIncreaseBoardSize.TabIndex = 0;
            this.m_ButtonIncreaseBoardSize.Text = "Board size:6x6 (click to increase)";
            this.m_ButtonIncreaseBoardSize.UseVisualStyleBackColor = true;
            this.m_ButtonIncreaseBoardSize.Click += new System.EventHandler(this.buttonIncreaseBoardSize_Click);
            // 
            // m_ButtonHumanPlayer
            // 
            this.m_ButtonHumanPlayer.Location = new System.Drawing.Point(258, 130);
            this.m_ButtonHumanPlayer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_ButtonHumanPlayer.Name = "m_ButtonHumanPlayer";
            this.m_ButtonHumanPlayer.Size = new System.Drawing.Size(190, 60);
            this.m_ButtonHumanPlayer.TabIndex = 1;
            this.m_ButtonHumanPlayer.Text = "Play against your friend";
            this.m_ButtonHumanPlayer.UseVisualStyleBackColor = true;
            this.m_ButtonHumanPlayer.Click += new System.EventHandler(this.buttonHumanPlayer_Click);
            // 
            // m_ButtonComputerPlayer
            // 
            this.m_ButtonComputerPlayer.Location = new System.Drawing.Point(68, 130);
            this.m_ButtonComputerPlayer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_ButtonComputerPlayer.Name = "m_ButtonComputerPlayer";
            this.m_ButtonComputerPlayer.Size = new System.Drawing.Size(172, 60);
            this.m_ButtonComputerPlayer.TabIndex = 2;
            this.m_ButtonComputerPlayer.Text = "Play against the computer";
            this.m_ButtonComputerPlayer.UseVisualStyleBackColor = true;
            this.m_ButtonComputerPlayer.Click += new System.EventHandler(this.buttonComputerPlayer_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 254);
            this.Controls.Add(this.m_ButtonComputerPlayer);
            this.Controls.Add(this.m_ButtonHumanPlayer);
            this.Controls.Add(this.m_ButtonIncreaseBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.Button m_ButtonIncreaseBoardSize;
            private System.Windows.Forms.Button m_ButtonHumanPlayer;
            private System.Windows.Forms.Button m_ButtonComputerPlayer;
        }

    }
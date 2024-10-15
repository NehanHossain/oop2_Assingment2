//Author: Nehan Hossain
//Date created: october 10, 2024
//Date last modified : october 14, 2024
// Project: assignment 2
// Description:This file contains all the C# code for oop2_Assignment2

//inports
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

//name space
namespace oop2_Assignment2
{
    public partial class MainWindow : Window
    {
        // varible declarations
        private string currentPlayer = "";
        private string playerXName = "";
        private string playerOName = "";
        private int playerXScore = 0;
        private int playerOScore = 0;
        private string[,] board = new string[3, 3];

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Checks if there is a winner on the game board.
        /// </summary>
        /// <param name="boardGame">The current state of the game board.</param>
        /// <returns>True if there is a winner, otherwise false.</returns>
        private bool CheckWinner(string[,] boardGame)
        {
            //checks if there is a winner
            for (int index = 0; index < 3; index++)
            {
                if ((boardGame[index, 0] == boardGame[index, 1] && boardGame[index, 1] == boardGame[index, 2] && !string.IsNullOrEmpty(boardGame[index, 0])) ||
                    (boardGame[0, index] == boardGame[1, index] && boardGame[1, index] == boardGame[2, index] && !string.IsNullOrEmpty(boardGame[0, index])))
                {
                    return true; 
                }
            }

            // otherwise return
            return (boardGame[0, 0] == boardGame[1, 1] && boardGame[1, 1] == boardGame[2, 2] && !string.IsNullOrEmpty(boardGame[0, 0])) ||
                   (boardGame[0, 2] == boardGame[1, 1] && boardGame[1, 1] == boardGame[2, 0] && !string.IsNullOrEmpty(boardGame[0, 2]));
        }

        /// <summary>
        /// Updates the game board with the current player's move.
        /// </summary>
        /// <param name="row">The row index of the move.</param>
        /// <param name="col">The column index of the move.</param>
        /// <param name="button">The button representing the move.</param>
        private void UpdateBoard(int row, int col, Button button)
        {

            board[row, col] = currentPlayer;
            button.Content = currentPlayer;
            button.IsEnabled = false;

            // if there is an winner
            if (CheckWinner(board))
            {
                UpdateScore();
                MessageBox.Show($"Player {currentPlayer} wins!");
                currentPlayer = RandomChoice() == playerXName ? "X" : "O";
                playerXName = xPlayer.Text;
                playerOName = oPlayer.Text;
                if (currentPlayer == "X")
                {
                    currentPlayerOutput.Text = playerXName;
                }
                else
                {
                    currentPlayerOutput.Text = playerOName;
                }
                ClearBoard();
            }
            //if there is a full bord
            else if (IsBoardFull())
            {
                MessageBox.Show("It's a draw!", "Game Over");
                currentPlayer = RandomChoice() == playerXName ? "X" : "O";
                playerXName = xPlayer.Text;
                playerOName = oPlayer.Text;
                if (currentPlayer == "X")
                {
                    currentPlayerOutput.Text = playerXName;
                }
                else
                {
                    currentPlayerOutput.Text = playerOName;
                }
                ClearBoard();
            }
            //otherwise
            else
            {
                currentPlayer = currentPlayer == "X" ? "O" : "X";
                if (currentPlayer == "X")
                {
                    currentPlayerOutput.Text = playerXName;
                }
                else
                {
                    currentPlayerOutput.Text = playerOName;
                }

            }
        }

        /// <summary>
        /// Checks if the game board is full.
        /// </summary>
        /// <returns>True if the board is full, otherwise false.</returns>
        private bool IsBoardFull()
        {
            return board.Cast<string>().All(cell => !string.IsNullOrEmpty(cell));
        }

        /// <summary>
        /// Updates the score of the current player.
        /// </summary>
        private void UpdateScore()
        {
            if (currentPlayer == "X")
            {
                playerXScore++;
                xScore.Text = playerXScore.ToString();
            }
            else
            {
                playerOScore++;
                oScore.Text = playerOScore.ToString();
            }
        }

        /// <summary>
        /// Clears the game board and resets all buttons.
        /// </summary>
        private void ClearBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int coloum = 0; coloum < 3; coloum++)
                {
                    board[row, coloum] = string.Empty;
                    rowOneBox1.Content = "";
                }
            }

            rowOneBox1.Content = "";
            rowZeroBox2.Content = "";
            rowOneBox3.Content = "";
            rowTwoBox1.Content = "";
            rowTwoBox2.Content = "";
            rowTwoBox3.Content = "";
            rowThreeBox1.Content = "";
            rowThreeBox2.Content = "";
            rowThreeBox3.Content = "";

            
            rowOneBox1.IsEnabled = true;
            rowZeroBox2.IsEnabled = true;
            rowOneBox3.IsEnabled = true;
            rowTwoBox1.IsEnabled = true;
            rowTwoBox2.IsEnabled = true;
            rowTwoBox3.IsEnabled = true;
            rowThreeBox1.IsEnabled = true;
            rowThreeBox2.IsEnabled = true;
            rowThreeBox3.IsEnabled = true;

        }

        /// <summary>
        /// Gets the button corresponding to the specified row and column.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <returns>The button at the specified row and column.</returns>
        private Button GetButton(int row, int col)
        {
            return (Button)this.FindName($"row{row}{col}"); 
        }

        /// <summary>
        /// Handles the click event for the first button in the first row.
        /// </summary>
        private void rowOneBox1_Click(object sender, RoutedEventArgs e) => UpdateBoard(0, 0, (Button)sender);

        /// <summary>
        /// Handles the click event for the second button in the first row.
        /// </summary>
        private void rowZeroBox2_Click(object sender, RoutedEventArgs e) => UpdateBoard(0, 1, (Button)sender);

        /// <summary>
        /// Handles the click event for the third button in the first row.
        /// </summary>
        private void rowOneBox3_Click(object sender, RoutedEventArgs e) => UpdateBoard(0, 2, (Button)sender);

        /// <summary>
        /// Handles the click event for the first button in the second row.
        /// </summary>
        private void rowTwoBox1_Click(object sender, RoutedEventArgs e) => UpdateBoard(1, 0, (Button)sender);

        /// <summary>
        /// Handles the click event for the second button in the second row.
        /// </summary>
        private void rowTwoBox2_Click(object sender, RoutedEventArgs e) => UpdateBoard(1, 1, (Button)sender);

        /// <summary>
        /// Handles the click event for the third button in the second row.
        /// </summary>
        private void rowTwoBox3_Click(object sender, RoutedEventArgs e) => UpdateBoard(1, 2, (Button)sender);

        /// <summary>
        /// Handles the click event for the first button in the third row.
        /// </summary>
        private void rowThreeBox1_Click(object sender, RoutedEventArgs e) => UpdateBoard(2, 0, (Button)sender);

        /// <summary>
        /// Handles the click event for the second button in the third row.
        /// </summary>
        private void rowThreeBox2_Click(object sender, RoutedEventArgs e) => UpdateBoard(2, 1, (Button)sender);

        /// <summary>
        /// Handles the click event for the third button in the third row.
        /// </summary>
        private void rowThreeBox3_Click(object sender, RoutedEventArgs e) => UpdateBoard(2, 2, (Button)sender);

        /// <summary>
        /// Handles the click event for the enter button.
        /// Initializes player names and checks for valid input.
        /// Enables the game board for play and randomly selects the starting player.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void enter_Click(object sender, RoutedEventArgs e)
        {
            playerXName = xPlayer.Text;
            playerOName = oPlayer.Text;

            if (string.IsNullOrWhiteSpace(playerXName) || string.IsNullOrWhiteSpace(playerOName))
            {
                MessageBox.Show("Please enter names for both players.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            rowOneBox1.IsEnabled = true;
            rowZeroBox2.IsEnabled = true;
            rowOneBox3.IsEnabled = true;
            rowTwoBox1.IsEnabled = true;
            rowTwoBox2.IsEnabled = true;
            rowTwoBox3.IsEnabled = true;
            rowThreeBox1.IsEnabled = true;
            rowThreeBox2.IsEnabled = true;
            rowThreeBox3.IsEnabled = true;

            currentPlayer = RandomChoice() == playerXName ? "X" : "O";
            playerXName = xPlayer.Text;
            playerOName = oPlayer.Text;
            if (currentPlayer == "X")
            {
                currentPlayerOutput.Text = playerXName;
            }
            else
            {
                currentPlayerOutput.Text = playerOName;
            }

            enter.IsEnabled = false;
        }

        /// <summary>
        /// Randomly selects a starting player.
        /// </summary>
        /// <returns>The name of the randomly chosen player.</returns>
        private string RandomChoice()
        {
            Random random = new Random();
            return random.Next(2) == 0 ? playerXName : playerOName;
        }

        /// <summary>
        /// restarts the entire program
        /// </summary>
        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            ClearBoard();

            rowOneBox1.IsEnabled = false;
            rowZeroBox2.IsEnabled = false;
            rowOneBox3.IsEnabled = false;
            rowTwoBox1.IsEnabled = false;
            rowTwoBox2.IsEnabled = false;
            rowTwoBox3.IsEnabled = false;
            rowThreeBox1.IsEnabled = false;
            rowThreeBox2.IsEnabled = false;
            rowThreeBox3.IsEnabled = false;

            enter.IsEnabled = true;

            playerXScore = 0;
            playerOScore = 0;
            xPlayer.Clear();
            oPlayer.Clear();
            oScore.Clear();
            xScore.Clear();
            currentPlayerOutput.Clear();
        }

        /// <summary>
        /// exzits the program
        /// </summary>
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}

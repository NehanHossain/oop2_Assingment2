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


using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace oop2_Assignment2
{
    public partial class MainWindow : Window
    {
        private string currentPlayer = "X";
        private string playerXName = "";
        private string playerOName = "";
        private int playerXScore = 0;
        private int playerOScore = 0;
        private string[,] board = new string[3, 3];

        public MainWindow()
        {
            InitializeComponent();
        }

        private bool CheckWinner(string[,] boardGame)
        {
            // Check rows, columns, and diagonals for a winner
            for (int index = 0; index < 3; index++)
            {
                if ((boardGame[index, 0] == boardGame[index, 1] && boardGame[index, 1] == boardGame[index, 2] && !string.IsNullOrEmpty(boardGame[index, 0])) ||
                    (boardGame[0, index] == boardGame[1, index] && boardGame[1, index] == boardGame[2, index] && !string.IsNullOrEmpty(boardGame[0, index])))
                {
                    return true; // A winner has been found
                }
            }

            // Check diagonals
            return (boardGame[0, 0] == boardGame[1, 1] && boardGame[1, 1] == boardGame[2, 2] && !string.IsNullOrEmpty(boardGame[0, 0])) ||
                   (boardGame[0, 2] == boardGame[1, 1] && boardGame[1, 1] == boardGame[2, 0] && !string.IsNullOrEmpty(boardGame[0, 2]));
        }

        private void UpdateBoard(int row, int col, Button button)
        {
            board[row, col] = currentPlayer;
            button.Content = currentPlayer;
            button.IsEnabled = false;

            if (CheckWinner(board))
            {
                MessageBox.Show($"Player {currentPlayer} wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateScore();
                ResetGame();
            }
            else if (IsBoardFull())
            {
                MessageBox.Show("It's a draw!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetGame();
            }
            else
            {
                currentPlayer = currentPlayer == "X" ? "O" : "X";
                currentPlayerOutput.Text = $"Current Player: {currentPlayer}";
            }
        }

        private bool IsBoardFull()
        {
            return board.Cast<string>().All(cell => !string.IsNullOrEmpty(cell));
        }

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

        private void ResetGame()
        {
            ClearBoard();
            currentPlayer = RandomChoice() == playerXName ? "X" : "O";
            currentPlayerOutput.Text = $"Current Player: {currentPlayer}";
            enter.IsEnabled = true; // Allow name input again
        }

        private void ClearBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = string.Empty;

                    var button = GetButton(i, j);
                    if (button != null)
                    {
                        button.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show($"Button at position ({i}, {j}) is null!");
                    }
                }
            }
        }

        private Button GetButton(int row, int col)
        {
            return (Button)this.FindName($"row{row}{col}"); // Match naming in XAML
        }

        private void rowOneBox1_Click(object sender, RoutedEventArgs e) => UpdateBoard(0, 0, (Button)sender);
        private void rowZeroBox2_Click(object sender, RoutedEventArgs e) => UpdateBoard(0, 1, (Button)sender);
        private void rowOneBox3_Click(object sender, RoutedEventArgs e) => UpdateBoard(0, 2, (Button)sender);
        private void rowTwoBox1_Click(object sender, RoutedEventArgs e) => UpdateBoard(1, 0, (Button)sender);
        private void rowTwoBox2_Click(object sender, RoutedEventArgs e) => UpdateBoard(1, 1, (Button)sender);
        private void rowTwoBox3_Click(object sender, RoutedEventArgs e) => UpdateBoard(1, 2, (Button)sender);
        private void rowThreeBox1_Click(object sender, RoutedEventArgs e) => UpdateBoard(2, 0, (Button)sender);
        private void rowThreeBox2_Click(object sender, RoutedEventArgs e) => UpdateBoard(2, 1, (Button)sender);
        private void rowThreeBox3_Click(object sender, RoutedEventArgs e) => UpdateBoard(2, 2, (Button)sender);

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            playerXName = xPlayer.Text;
            playerOName = oPlayer.Text;
            if (string.IsNullOrWhiteSpace(playerXName) || string.IsNullOrWhiteSpace(playerOName))
            {
                MessageBox.Show("Please enter names for both players.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            currentPlayer = RandomChoice() == playerXName ? "X" : "O";
            currentPlayerOutput.Text = $"Current Player: {currentPlayer}";
            enter.IsEnabled = false; // Disable enter button after names are set
        }

        private string RandomChoice()
        {
            Random random = new Random();
            return random.Next(2) == 0 ? playerXName : playerOName;
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void currentPlayer_TextChanged(object sender, TextChangedEventArgs e)
        {
            // You can handle text changed event here if necessary
        }
    }
}

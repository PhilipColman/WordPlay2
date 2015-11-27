/**
 * Word Play is basic two player game that removes the letters from a word
 * where the other play is the guess the original word.
 * Copyright (C) 2014  Philip
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Word_Play_2
{
    public partial class MainWindow : Window
    {
        public static string p1Name, p2Name, players;
        public static int p1Score, p2Scroe;
        string word, gameWord;
        int score;
        bool turn;
        Visibility visible = Visibility.Visible;
        Visibility hidden = Visibility.Hidden;
        private string[] words = System.IO.File.ReadAllLines("Words.txt");
        public MainWindow()
        {
            InitializeComponent();
            welcome window = new welcome();
            Hide();
            window.ShowDialog();
            turn = true;
            setup(players);
            Show();
        }
        void updateScore(string players)
        {
            if (players == "one")
            {
                Score.Text = string.Format("Your score: {0}", p1Score.ToString());
            }
            else if (players == "two")
            {
                Score.Text = string.Format("{0}'s Score: {2}\t{1}'s Score: {3}", p1Name, p2Name, p1Score.ToString(), p2Scroe.ToString());
            }
        }
        void setup(string players)
        {
            hide();
            word = "";
            guess.Text = null;
            if (players == "one")
            {
                updateScore("one");
                display.Visibility = visible;
                guess.Visibility = visible;
                Random r = new Random();
                word = words[r.Next(0, words.Length)].ToLower().Trim();
                score = 10;
                gameWord = word.Replace('a', '_').Replace('e', '_').Replace('i', '_').Replace('o', '_').Replace('u', '_');
                display.Text = string.Format("Complet the word\n{0}", gameWord);
                guess.Focus();
            }
            else if (players == "two")
            {
                updateScore("two");
                display.Text = string.Format("Enter a word for {0} to guess", turn ? p1Name : p2Name);
                display.Visibility = visible;
                newWord.Visibility = visible;
                newWord.Focus();
            }
            else
            {
                Close();
            }
        }
        void hide()
        {
            guess.Visibility = hidden;
            Guess.Visibility = hidden;
            newWord.Visibility = hidden;
            display.Visibility = hidden;
            setWord.Visibility = hidden;
        }
        void getWord()
        {
            if (setWord.Visibility == visible)
            {
                word = newWord.Text.ToLower().Trim();
                gameWord = word.Replace('a', '_').Replace('e', '_').Replace('i', '_').Replace('o', '_').Replace('u', '_');
                display.Text = string.Format("Complet the word\n{0}", gameWord);
                newWord.Visibility = hidden;
                setWord.Visibility = hidden;
                newWord.Text = "";
                guess.Visibility = visible;
                score = 10;
                guess.Focus();
            }
        }
        void next()
        {
            turn = players == "two" ? !turn : true;
            if (turn)
            {
                var m = MessageBox.Show("Do want to play again?", "New round?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (m == MessageBoxResult.No)
                {
                    if (players == "two")
                        MessageBox.Show(string.Format("Final Scores:\n{0}: {2}\n{1}: {3}", p1Name, p2Name, p1Score.ToString(), p2Scroe.ToString()), "Score", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    Close();
                }
            }
            setup(players);
            Guess.Visibility = hidden;
        }
        void checkWord()
        {
            if (Guess.Visibility == visible)
            {
                if (guess.Text.ToLower().Trim() == word)
                {
                    p1Score += turn ? score : 0;
                    p2Scroe += turn ? 0 : score;
                    next();
                }
                else
                {
                    score--;
                    guess.Text = null;
                    Guess.Visibility = hidden;
                    if (score > 0)
                        MessageBox.Show("Wrong try again", "Wrong!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    else if (score == 0)
                    {
                        MessageBox.Show(string.Format("You have lost {0}", players == "one" ? "\nThe word was: " + word : ""), "Wrong!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        next();
                    }
                }

            }
        }
        private void newWord_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool b = false;
            if (newWord.Text.Length >= 3)
            {
                foreach (string w in words)
                {
                    if (newWord.Text.ToLower().Trim() == w.ToLower().Trim())
                    {
                        b = true;
                        break;
                    }
                }
            }
            if (b)
            {
                setWord.Visibility = visible;
            }
            else
            {
                setWord.Visibility = hidden;
            }
        }
        private void Guess_Click(object sender, RoutedEventArgs e)
        {
            checkWord();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit", "Exit?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (guess.Text.Length == word.Length)
            {
                Guess.Visibility = visible;
            }
            else
            {
                Guess.Visibility = hidden;
            }
        }
        private void setWord_Click(object sender, RoutedEventArgs e)
        {
            getWord();
        }
        private void newWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                getWord();
            }
        }
        private void guess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                checkWord();
            }
        }
    }
}
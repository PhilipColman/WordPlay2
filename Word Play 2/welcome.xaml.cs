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

namespace Word_Play_2
{
    public partial class welcome : Window
    {
        Visibility visible = Visibility.Visible;
        Visibility hidden = Visibility.Hidden;
        public welcome()
        {
            InitializeComponent();
            sButton.Visibility = hidden;
            p1Name.Visibility = hidden;
            Player_One_Name.Visibility = hidden;
            Player_Two_Name.Visibility = hidden;
            p2Name.Visibility = hidden;
            mButton.Visibility = hidden;
            cbox.Items.Add("One");
            cbox.Items.Add("Two");
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbox.SelectedItem.ToString() == "One")
            {
                p1Name.Visibility = hidden;
                Player_One_Name.Visibility = hidden;
                Player_Two_Name.Visibility = hidden;
                p2Name.Visibility = hidden;
                mButton.Visibility = hidden;
                sButton.Visibility = visible;
            }
            else if (cbox.SelectedItem.ToString() == "Two")
            {
                sButton.Visibility = hidden;
                p1Name.Visibility = visible;
                Player_One_Name.Visibility = visible;
                Player_Two_Name.Visibility = visible;
                p2Name.Visibility = visible;
                mButton.Visibility = hidden;
                p1Name.Text = "";
                p2Name.Text = "";
            }
            else
            {
                sButton.Visibility = hidden;
                p1Name.Visibility = hidden;
                Player_One_Name.Visibility = hidden;
                Player_Two_Name.Visibility = hidden;
                p2Name.Visibility = hidden;
                mButton.Visibility = hidden;
            }
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            mButton.Visibility = p1Name.Text != "" ? (p2Name.Text != "" ? visible : hidden) : hidden;
        }
        private void sButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.players = "one";
            MainWindow.p1Name = "Player";
            MainWindow.p2Name = null;
            MainWindow.p1Score = 0;
            this.Close();
        }
        private void mButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.players = "two";
            MainWindow.p1Name = p1Name.Text;
            MainWindow.p2Name = p2Name.Text;
            MainWindow.p1Score = 0;
            MainWindow.p2Scroe = 0;
            this.Close();
        }
    }
}
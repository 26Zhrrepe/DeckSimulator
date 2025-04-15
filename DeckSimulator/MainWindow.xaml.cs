using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DeckSimulator
{
    public partial class MainWindow : Window
    {
        private List<string> deck = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddCard_Click(object sender, RoutedEventArgs e)
        {
            var cardName = CardNameTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(cardName))
            {
                deck.Add(cardName);
                DeckListBox.Items.Add(cardName);
                CardNameTextBox.Clear();
            }
        }

        private void RunSimulation_Click(object sender, RoutedEventArgs e)
        {
            var random = new Random();
            var hand = new List<string>();
            var tempDeck = new List<string>(deck);

            for (int i = 0; i < 5 && tempDeck.Count > 0; i++)
            {
                int index = random.Next(tempDeck.Count);
                hand.Add(tempDeck[index]);
                tempDeck.RemoveAt(index);
            }

            bool hasSnowpios = HasSnowpiosCheckBox.IsChecked == true;
            bool conditionMet = !hasSnowpios || hand.Contains("スノーピオス");

            ResultTextBox.Text = $"手札: {string.Join(", ", hand)}\n条件達成: {(conditionMet ? "はい" : "いいえ")}";
        }
    }
}

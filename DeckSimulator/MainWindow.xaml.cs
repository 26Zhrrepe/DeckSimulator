using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using System.IO;

namespace DeckSimulator
{
    public partial class MainWindow : Window
    {
        private List<Card> allCards;

        public MainWindow()
        {
            InitializeComponent();
            // 外部ファイル読み込み
            try
            {
                CardDatabase.Load("cards.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show("カード情報の読み込みに失敗しました:\n" + ex.Message);
            }
        }


        private Deck deck = new();
        private void RefreshDeckListView()
        {
            DeckListView.ItemsSource = null;
            DeckListView.ItemsSource = deck.Cards;
        }

        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {
            string cardName = CardNameTextBox.Text.Trim();
            string countText = CardCountTextBox.Text.Trim();

            if (string.IsNullOrEmpty(cardName))
            {
                MessageBox.Show("カード名を入力してください。");
                return;
            }

            Card card = CardDatabase.FindCardByName(cardName);
            if (card == null)
            {
                MessageBox.Show("指定されたカードが見つかりません。");
                return;
            }

            int count = 3; // デフォルトは3枚
            if (!string.IsNullOrEmpty(countText))
            {
                if (!int.TryParse(countText, out count) || count < 1)
                {
                    MessageBox.Show("枚数は1以上の整数で入力してください。");
                    return;
                }
            }

            // デッキに追加（既存カードなら加算）
            deck.AddCard(card, count);
            RefreshDeckListView();

            CardNameTextBox.Text = "";
            CardCountTextBox.Text = "";
        }
        private void RemoveCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeckListView.SelectedItem is Card selectedCard)
            {
                deck.RemoveCard(selectedCard);
                RefreshDeckListView();
            }
        }


    }
}

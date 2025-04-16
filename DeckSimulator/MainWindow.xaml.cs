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

        private void AddCardByName(string name)
        {
            var card = allCards.FirstOrDefault(c => c.Name == name);
            if (card != null)
            {
                var newCard = new Card
                {
                    Name = card.Name,
                    Count = 1,
                    CardType = card.CardType,
                    IsTuner = card.IsTuner,
                    Attribute = card.Attribute,
                    Race = card.Race,
                    Level = card.Level,
                    Categories = new List<string>(card.Categories)
                };

                deck.AddCard(newCard);
                RefreshDeckListView();
            }
            else
            {
                MessageBox.Show("カードが見つかりません");
            }
        }
        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {
            string name = CardNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(name)) return;

            if (!int.TryParse(CardCountTextBox.Text.Trim(), out int count)) return;
            if (count <= 0) return;

            var baseCard = CardDatabase.FindByName(name);
            if (baseCard == null)
            {
                MessageBox.Show("カード情報が見つかりません。JSONファイルを確認してください。");
                return;
            }

            // デッキに入れる用のカード（Countだけ別途設定）
            var cardToAdd = new Card
            {
                Name = baseCard.Name,
                CardType = baseCard.CardType,
                Attribute = baseCard.Attribute,
                Race = baseCard.Race,
                Level = baseCard.Level,
                Categories = baseCard.Categories,
                Count = count
            };

            deck.AddCard(cardToAdd);
            RefreshDeckListView();

            CardNameTextBox.Text = "";
            CardCountTextBox.Text = "";
        }


    }
}

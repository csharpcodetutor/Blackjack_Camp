using Blackjack.Reources; //This is a resource assembly which holds the methods to display card graphics
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            //We will be writing the code for the Blackjack game logic here
        }

        #region Methods
        public static void DisplayTable(Player dealer, Player player, bool flip = false)
        {
            Console.Clear();

            Console.CursorLeft = 4;

            if (flip)
                Console.WriteLine($"DEALER ({dealer.Score()}) {ShowHand(dealer)}");
            else
                Console.WriteLine($"DEALER (XX) XX {dealer.hand.CardsInHand[1]}");

            Console.CursorTop = 1;
            Console.CursorLeft = 32;

            if (flip)
                DisplayCardGraphic(dealer, true);
            else
                DisplayCardGraphic(dealer);


            Console.CursorLeft = 4;
            Console.CursorTop += 10;

            Console.WriteLine($"{player.Name}  ({player.Score()}) {ShowHand(player)}");

            Console.CursorLeft = 32;

            if (flip)
                DisplayCardGraphic(player, true);
            else
                DisplayCardGraphic(player);
        }

        public static string ShowHand(Player player)
        {
            string hand = "";
            foreach (Card card in player.hand.CardsInHand)
            {
                hand += $"{card} ";
            }

            return hand;
        }


        public static void DisplayCardGraphic(Player player, bool flip = false)
        {
            int top = Console.CursorTop;
            Console.CursorLeft = 32;
            foreach (Card card in player.hand.CardsInHand)
            {
                if (flip)
                    card.Hidden = false;

                card.PrintCard();

                Console.CursorTop = top;
                Console.CursorLeft += 4;
            }

            Console.CursorTop += 1;
        }
        #endregion
    }

    #region Classes
    //Classes for the game  Players Cards and Deck
    public class Deck
    {
        List<Card> cards = new List<Card>();
        public List<Card> Cards
        {
            get
            {
                if (cards == null || cards.Count == 0)
                    LoadDeck();

                return cards;
            }
        }

        public void Init()
        {
            LoadDeck();
        }

        private void LoadDeck()
        {
            int i = 1;
            while (i < 14)
            {
                cards.Add(new Card { FaceName = CardSuite.Hearts, FaceValue = i, Hidden = true });
                cards.Add(new Card { FaceName = CardSuite.Diamonds, FaceValue = i, Hidden = true });
                cards.Add(new Card { FaceName = CardSuite.Clubs, FaceValue = i, Hidden = true });
                cards.Add(new Card { FaceName = CardSuite.Spades, FaceValue = i, Hidden = true });
                i++;
            }
        }

        public void TripleShuffle()
        {
            SingleShuffle();
            SingleShuffle();
            SingleShuffle();
        }

        private void SingleShuffle()
        {
            // Create the card shuffling code
        }

        public Card DealCard(bool hide = true)
        {
            var card = cards[0];
            cards.Remove(card);

            card.Hidden = hide;

            return card;
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public Hand hand = new Hand();

        public int Score()
        {
            int total = 0;
            foreach (Card card in hand.CardsInHand)
                total += card.FaceValue;

            return total;
        }
    }

    public class Hand
    {
        public List<Card> CardsInHand = new List<Card>();
    }
    #endregion



}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;


public class Card
{
    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

    public Suit suit;
    public Rank rank;
    public Sprite sprite;

    public Card(Suit s, Rank r, Sprite sprite)
    {
        this.suit = s;
        this.rank = r;
        this.sprite = sprite; 
    }

    public int GetValue()
    {
        if (rank >= Rank.Jack) return 0; 
        return (int)rank;

    }

    public string GetCardName()
    {
        return $"{rank} of {suit}";
    }
}


public class Gameplay : MonoBehaviour
{
    private List<Card> deck = new List<Card>();
    private List<Card> playerHand = new List<Card>();
    private List<Card> dealerHand = new List<Card>();

    [Header("UI References")]
    public Button butDeal;
    public Image[] imgHands = new Image[4];
    public TextMeshProUGUI txtResults;

    [Header("Card Sprites")]
    public Sprite[] cardSprites = new Sprite[52];
    public Sprite cardBackSprite;

    void CreateDeck()
    {
        deck.Clear();
        for (int suit = 0; suit < 4; suit++)
        {
            for (int rank = 1; rank <= 13; rank++)
            {
                int spriteIndex = suit * 13 + (rank - 1);
                deck.Add(new Card((Card.Suit)suit, (Card.Rank)rank, cardSprites[spriteIndex]));
            }
        }
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    void DealCard()
    {
        playerHand.Clear();
        dealerHand.Clear();

        if (deck.Count > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                playerHand.Add(deck[0]);
                imgHands[i].sprite = playerHand[i].sprite;
                deck.RemoveAt(0);
            }
            for (int i = 0; i < 2; i++)
            {
                dealerHand.Add(deck[0]);
                imgHands[i + 2].sprite = dealerHand[i].sprite;
                deck.RemoveAt(0);
            }
        
        }
        //Debug.Log(playerHand[0].GetCardName() + " " + playerHand[0].GetValue());
        //Debug.Log(dealerHand[0].GetCardName() + " " + dealerHand[0].GetValue());

        int playervalue = playerHand[0].GetValue() + playerHand[1].GetValue();
        int dealervalue = dealerHand[0].GetValue() + dealerHand[1].GetValue();

        playervalue = playervalue % 10;
        dealervalue = dealervalue % 10;

        if (playervalue > dealervalue) txtResults.text = "Player Win!";
        else if (playervalue < dealervalue) txtResults.text = "Dealer Win!";
        else txtResults.text = "Draw!";

        deck.Clear();
        CreateDeck();
        ShuffleDeck();
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateDeck();
        ShuffleDeck();
        butDeal.onClick.AddListener(DealCard);

        /*Debug.Log(playerHand[0].GetCardName() + " " + playerHand[0].GetValue());
        Debug.Log(dealerHand[0].GetCardName() + " " + dealerHand[0].GetValue());*/

        /*Debug.Log(deck[0].GetCardName() + " " + deck[0].GetValue());
        Debug.Log(deck[22].GetCardName() + " " + deck[22].GetValue());
        Debug.Log(deck[51].GetCardName() + " " + deck[51].GetValue());*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

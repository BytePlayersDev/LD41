using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    #region Variables

    [HideInInspector]public Card Card;
    public CardActionEnum.Action Action;
    public int index;

    private GameManager gm;
    public DeckList DeckList;

    #endregion
        
    void Start ()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Card = ChooseCard();

        while (!gm.IsCardValid(Card, index))
            Card = ChooseCard();

        UpdateCardAtributes();
    }

    #region Custom Functions

    // OnClicked de una Carta
    public void OnClickedCard()
    {
        gm.CheckAction(Card.Action, this);
    }

    // Selecciona una carta en base a las probabilidades establecidas por cada una
    public Card ChooseCard()
    {
        float total = 0;
        if (DeckList != null)
        {
            foreach (Card c in DeckList.Decks)
            {
                total += c.probability;
            }
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < DeckList.Decks.Count; i++)
        {
            if (randomPoint < DeckList.Decks[i].probability)
                return DeckList.Decks[i];
            else
                randomPoint -= DeckList.Decks[i].probability;
        }

        return DeckList.Decks[DeckList.Decks.Count - 1];
    }

    // Elige una carta y actualiza los botones
    public void ReRollCard()
    {
        Card choosedCard = ChooseCard();

        while(!gm.IsCardValid(choosedCard, index))
            choosedCard = ChooseCard();

        Card = choosedCard;
        UpdateCardAtributes();
    }

    // Actualiza los parametros de la carta
    void UpdateCardAtributes()
    {
        //Cambiar animaciones
        Animator animator = GetComponent<UnityEngine.Animator>();
        if (animator != null)
        {
            animator.Play("Empty");
            animator.SetInteger("Action", (int) Card.Action);
        }
        
        Action = Card.Action;
    }

    #endregion
}

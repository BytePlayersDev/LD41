using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActivated : MonoBehaviour {

    public GameManager Game;
    public CardDisplay CardDisplay;
    public DeckList DeckList;

    public void ActiveCard()
    {
        //TODO :  Deberia pasarle tambien mi boton para que me reactive y me crea
        Game.CardAction(CardDisplay.Card.Action);
        // Desactivo la carta???  
    }

    public void ReRollCard()
    {
        if (DeckList != null)
        {
            int random = Random.Range(0, (DeckList.Decks.Count - 1));
            CardDisplay.UpdateCard(DeckList.Decks[random]);
        }
        //activar carta
    }

}

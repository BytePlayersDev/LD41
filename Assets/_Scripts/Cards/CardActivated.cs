using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActivated : MonoBehaviour {

    public GameManager Game;
    public CardDisplay CardDisplay;
    public DeckList DeckList;

    public void ActiveCard()
    {
        //Desactivar buttons
        UnityEngine.UI.Button []Button = gameObject.transform.parent.GetComponentsInChildren<UnityEngine.UI.Button>();

        for (int i = 0; i < Button.Length; i++) {

          
                Button[i].interactable = false;
            
        }


        //TODO :  Deberia pasarle tambien mi boton para que me reactive y me crea
        Game.CheckAction(CardDisplay.Card.Action, this);
    }
    public Card ChooseCard() {
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
    public void ReRollCard()
    {

        Card choosedCard = ChooseCard();
        //Activar button
        CardDisplay.UpdateCard(choosedCard);

        UnityEngine.UI.Button[] Button = gameObject.transform.parent.GetComponentsInChildren<UnityEngine.UI.Button>();


        for (int i = 0; i < Button.Length; i++)
        {
            Button[i].interactable = true ;
        }

    }

}

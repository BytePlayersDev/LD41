using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActivated : MonoBehaviour {

    public GameManager Game;
    public CardDisplay CardDisplay;
    public DeckList DeckList;

    public void ActiveCard()
    {
        //Desactivar button
        UnityEngine.UI.Button Button = GetComponent<UnityEngine.UI.Button>();
        if(Button != null)
        {
            Button.interactable = false;
        }

        //TODO :  Deberia pasarle tambien mi boton para que me reactive y me crea
        Game.CheckAction(CardDisplay.Card.Action, this);
    }

    public void ReRollCard()
    {
        if (DeckList != null)
        {
            int random = Random.Range(0, (DeckList.Decks.Count));
            CardDisplay.UpdateCard(DeckList.Decks[random]);
        }
        //Activar button

        UnityEngine.UI.Button Button = GetComponent<UnityEngine.UI.Button>();
        if (Button != null)
        {
            Button.interactable = true;
        }
    }

}

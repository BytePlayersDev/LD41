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

    public void ReRollCard()
    {
        if (DeckList != null)
        {
            int random = Random.Range(0, (DeckList.Decks.Count));
            CardDisplay.UpdateCard(DeckList.Decks[random]);
        }
        //Activar button

        UnityEngine.UI.Button[] Button = gameObject.transform.parent.GetComponentsInChildren<UnityEngine.UI.Button>();


        for (int i = 0; i < Button.Length; i++)
        {


            Button[i].interactable = true ;

        }

    }

}

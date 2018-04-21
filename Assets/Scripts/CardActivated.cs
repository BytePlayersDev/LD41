using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActivated : MonoBehaviour {

    public GameObject Player;
    public CardDisplay card;

    public void ActiveCard()
    {
        switch (card.Card.Action)
        {
            case CardActionEnum.Action.MoveRight:
               {
                    //llamar a al controllador de acciones del jugador
               }
           break;
            case CardActionEnum.Action.MoveLeft:
                {
                    //llamar a al controllador de acciones del jugador
                }
                break;
            case CardActionEnum.Action.MoveUp:
                {
                    //llamar a al controllador de acciones del jugador
                }
                break;
            case CardActionEnum.Action.Attack:
                {
                    //llamar a al controllador de acciones del jugador
                }
                break;
            case CardActionEnum.Action.Defend:
                {
                    //llamar a al controllador de acciones del jugador
                }
                break;
            default:
                break;
        }
    }

}

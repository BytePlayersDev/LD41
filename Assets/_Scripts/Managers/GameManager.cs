using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Variables

    [SerializeField]
    private GameObject player;
    public int maxEqualCards;
    public GameObject[] cardButtons;

    private CardDisplay[] cardDArray;
    private PlayerController pc;

    #endregion

    #region Functions

    // Use this for initialization
    void Start ()
    {
        pc = player.GetComponent<PlayerController>();
	}
	
    public void CheckAction(CardActionEnum.Action action, CardDisplay cardActivated)
    {
        SwitchButtons(false);

        switch (action)
        {
            case CardActionEnum.Action.MoveRight:
                {
                    pc.MoveRight();
                }
                break;
            case CardActionEnum.Action.MoveLeft:
                {
                    pc.MoveLeft();
                }
                break;
            case CardActionEnum.Action.Jump:
                {
                    pc.Jump();
                }
                break;
            case CardActionEnum.Action.Attack:
                {
                    pc.Attack();
                }
                break;
            case CardActionEnum.Action.Defend:
                {
                    pc.Defend();
                }
                break;
            default:
                break;
        }
        
        StartCoroutine(ReRollCard(cardActivated));        
    }

    // Activamos/Desactivamos todos los botones
    public void SwitchButtons(bool state)
    {        
        for (int i = 0; i < cardButtons.Length; i++)
        {
            cardButtons[i].GetComponent<Button>().interactable = state;
        }
    }

    // Muestra la pantalla de fin de juego
    public void Death()
    {
        GetComponent<UIManager>().ShowGameOver();
    }
    
    // Ver cartas y si hay más del limite, hacer ReRoll
    public bool IsCardValid(Card newCard, int index)
    {
        int cardCount = 1;        

        for (int i = 0; i<cardButtons.Length; ++i)
        {
            if (i != index) // No miramos nuestra posición
            {
                Card auxC = cardButtons[i].GetComponent<CardDisplay>().Card;
                if (auxC == newCard)
                    ++cardCount;
            }
        }

        return cardCount <= maxEqualCards;
    }

    #endregion

    #region Coroutines

    public IEnumerator ReRollCard(CardDisplay cardDisplay)
    {
        while (pc.GetIsMoving() || pc.GetIsAttacking())
        {
            yield return new WaitForSeconds(0.5f);
        }

        cardDisplay.ReRollCard();
    }
    
    #endregion
}

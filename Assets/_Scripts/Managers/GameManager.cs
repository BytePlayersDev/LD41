using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Variables

    [SerializeField]
    private GameObject player;
    public int maxEqualCards;
    public float secondsCardDelay;
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

        float timecard = cardActivated.Card.timer;
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
            case CardActionEnum.Action.Enigma:
                {

                    //Generar movimiento random
                    int randomnumber = Random.Range(0,6);
                    timecard = GetComponent<DeckList>().Decks[randomnumber].timer;
                    switch (randomnumber){
                        case 0:
                            {
                                pc.Attack();
                            }
                        break;
                        case 1:
                            {
                                pc.Defend();
                            }
                            break;
                        case 2:
                            {
                                pc.MoveLeft();
                            }
                            break;
                        case 3:
                            {
                                pc.MoveRight();
                            }
                            break;
                        case 4:
                            {
                                pc.Jump();
                            }
                            break;
                        case 5:
                            {
                                pc.Dance();
                            }
                            break;
                    }
                }
                break;
            default:
                break;
        }
        StartCoroutine(SwitchButtonsTimer(timecard));
        StartCoroutine(ReRollCard(cardActivated, timecard));        
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
        pc.aSource.PlayOneShot(pc.deathSound);

    }

    // Ver cartas y si hay más del limite, hacer ReRoll
    public bool IsCardValid(Card newCard, int index)
    {
        int cardCount = 1;        

        for (int i = 0; i<cardButtons.Length; ++i)
        {
            if (i != index) // No miramos nuestra posición
                if (cardButtons[i].GetComponent<CardDisplay>().Card == newCard)
                    ++cardCount;
        }

        return cardCount <= maxEqualCards;
    }

    #endregion

    #region Coroutines

    public IEnumerator ReRollCard(CardDisplay cardDisplay, float time)
    {
        //while (pc.GetIsMoving() || pc.GetIsAttacking() || pc.GetIsJumping())
        //{
        yield return new WaitForSeconds(time);
        //}

        cardDisplay.ReRollCard();
    }

    IEnumerator SwitchButtonsTimer(float time)
    {
        SwitchButtons(false);
        yield return new WaitForSeconds(time);
        SwitchButtons(true);
    }

    #endregion
}

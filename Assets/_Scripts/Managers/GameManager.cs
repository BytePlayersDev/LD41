using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Variables

    [SerializeField]
    private GameObject player;
    private PlayerController pc;
    public GameObject[] cardButtons;

    #endregion

    #region Functions

    // Use this for initialization
    void Start ()
    {
        pc = player.GetComponent<PlayerController>();
        
        
	}
	
    public void CheckAction(CardActionEnum.Action action, CardActivated cardActivated)
    {
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


        StartCoroutine(ReRollCard(cardActivated));//"ReRollCard", cardActivated);        
    }

    public void Death()
    {
        Debug.Log("Player is dead.");
        GetComponent<UIManager>().ShowGameOver();
    }
    public bool CheckIfEqual(CardActivated _cardActivated) {
        int cont = 0;
        for (int i = 0; i < cardButtons.Length; i++)
        {
            if (cardButtons[i].GetComponent<CardDisplay>().name.Equals(_cardActivated.GetComponent<CardDisplay>().name)) {
                cont++;
            }
            Debug.Log(cont + " -- " + cardButtons[i].GetComponent<CardDisplay>().name + " ---- " + _cardActivated.GetComponent<CardDisplay>().name);
            if (cont >= 2)
            {
                Debug.Log("ES VERDAD TUUU LOKOOOOOOO");
                return true;
            }

        }
        Debug.Log("----------------------------------------------------------------------------------------------------");
        return false;
    }
    #endregion

    #region Coroutines

    public IEnumerator ReRollCard(CardActivated cardActivated)
    {
        //while (CheckIfEqual(cardActivated) == true)
        //{
        //    Debug.Log("Hay varias Iguales REROOOOOOOLLING IN THE RIVER");
        //    cardActivated.ReRollCard();
        //}
        while (pc.GetIsMoving() || pc.GetIsAttacking())
        {
            yield return new WaitForSeconds(0.5f);
        }
        //if(!CheckIfEqual(cardActivated))
        cardActivated.ReRollCard();

    }

    public IEnumerator FirstReRollCard(CardActivated cardActivated)
    {
        while (CheckIfEqual(cardActivated) == true)
        {
            Debug.Log("Hay varias Iguales REROOOOOOOLLING IN THE RIVER");
            cardActivated.ReRollCard();
        }
        if (CheckIfEqual(cardActivated) == false)
            cardActivated.ReRollCard();
        yield return new WaitForSeconds(0);

    }

    #endregion

}

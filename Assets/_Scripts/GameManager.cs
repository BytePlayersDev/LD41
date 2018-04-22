using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Variables

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject scoreController;

    private PlayerController pc;

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

        StartCoroutine("ReRollCard", cardActivated);        
    }

    public void Death()
    {
        Debug.Log("Player is dead.");
    }

    #endregion

    #region Coroutines

    IEnumerator ReRollCard(CardActivated cardActivated)
    {
        while (!pc.GetIsMoving())
        {
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        cardActivated.ReRollCard();
    }

    #endregion

}

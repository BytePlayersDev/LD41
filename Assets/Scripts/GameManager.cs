using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject scoreController;

    private PlayerController pc;

    // Use this for initialization
    void Start ()
    {
        pc = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerState = PlayerController.playerState;

public class UnityAnimation : BaseAnimation
{
    [SerializeField] Animator _anim;
    public override void changeAnim(playerState currentState)
    {
        for (int i=0; i<=(int)playerState.DEATH; i++){
            playerState tmp = (playerState)i;
            if(tmp != currentState)
                _anim.SetBool(tmp.ToString(), false);
            else
                _anim.SetBool(tmp.ToString(), true);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

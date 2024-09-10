using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aperture.StateMachine;

public class FSMExemple : MonoBehaviour
{
    public enum ExampleEnum
    {
        STATE_ONE,
        STATE_TWO,
        STATE_THREE
    }

    public StateMachine<ExampleEnum> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<ExampleEnum>();
        stateMachine.Init();
        stateMachine.RegisterStates(ExampleEnum.STATE_ONE, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.STATE_TWO, new StateBase());
    }

}

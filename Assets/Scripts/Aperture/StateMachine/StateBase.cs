using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Aperture.StateMachine
{
    public class StateBase : MonoBehaviour
    {
        public virtual void OnStateEnter(params object[] objs)
        {

            Debug.Log("OnStateEnter");

        }

        public virtual void OnStateStay(object o = null)
        {

            Debug.Log("OnStateStay");

        }

        public virtual void OnStateExit(object o = null)
        {

            Debug.Log("OnStateExit");

        }

    }
}

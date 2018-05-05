using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected Transform player;

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                player = other.transform;
                StartCoroutine(StartInteraction());
            }
        }
    }

    protected abstract bool ReasonInteraction();
    protected abstract void InteractionAction();
    protected abstract void InteractionStart();
    protected abstract void InteractionEnd();

    private IEnumerator StartInteraction()
    {
        InteractionStart();

        while (ReasonInteraction())
        {
            InteractionAction();
            yield return null;
        }

        InteractionEnd();
    }
}

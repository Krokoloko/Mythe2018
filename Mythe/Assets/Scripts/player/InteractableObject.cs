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
            Debug.Log("im touching");
            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                player = other.transform;
                StartCoroutine(StartInteraction());
            }
        }
    }

    protected abstract bool ReasonInteraction();
    protected abstract void InteractionAction();

    private IEnumerator StartInteraction()
    {
        while (ReasonInteraction())
        {
            InteractionAction();
            yield return null;
        }
    }
}

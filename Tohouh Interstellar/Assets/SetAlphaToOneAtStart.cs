using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAlphaToOneAtStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDestory : MonoBehaviour
{
    [Range(0, 0.5f)]
    public float destoryTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destoryTime);
    }

}

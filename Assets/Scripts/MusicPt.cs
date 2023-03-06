using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPt : MonoBehaviour
{
    public InputController.musicPtType type;
    private void Start()
    {
        InputController.instance.addMusicPt(gameObject,type);
        //print("add" + transform.name);
    }
}

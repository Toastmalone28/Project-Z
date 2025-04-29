using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameHandler : MonoBehaviour
{
    public TMP_Text TMPName;
    public Canvas canvas;
    public Camera cam;

    private void Awake()
    {
        if(cam == null)
            cam = GameManager.instance.spectatorCamera;

        canvas.worldCamera = cam;
    }

    private void Update()
    {
        canvas.transform.LookAt(cam.transform.position);
        canvas.transform.Rotate(0, 180, 0);
    }
}

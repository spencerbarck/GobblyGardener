using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenGrass : MonoBehaviour
{
    public Camera mainCamera;
    
    void Start()
    {
        float height = mainCamera.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
        gameObject.transform.localScale = new Vector3(width,height,1);
    }
}

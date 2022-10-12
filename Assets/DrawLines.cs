using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    public static DrawLines Instance;
    public LineRenderer _lineRenderer;
    public Vector2 _startLinePosition;
    public bool _isDrawingLine;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        HideLine();
    }
    void Update()
    {
        if(_isDrawingLine)
        {
            //if(Input.GetMouseButtonDown(0))
            //{
            //    _startLineTransform = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //}
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(0, _startLinePosition);
            _lineRenderer.SetPosition(1,new Vector2(mousePos.x,mousePos.y));
        }
    }

    public void HideLine()
    {
        _isDrawingLine = false;
        _lineRenderer.SetPosition(0,new Vector2(0,0));
        _lineRenderer.SetPosition(1,new Vector2(0,0));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private GameObject _LinePrefab;
    [SerializeField]private Transform _LineSpawnPoint;
    private bool _IsDrawing = false;
    private Line _activeLine;
    // Start is called before the first frame update
    public void  SetDraw(bool isDrawing,Color color)
    {
        if (_IsDrawing == isDrawing) return;

        _IsDrawing = isDrawing;
        if (_IsDrawing)
            StartDraw(color);
        else
            StopDraw();
    }

    // Update is called once per frame
    void Update()
    {

        if (_IsDrawing)
        {
            _activeLine.UpdateLine(transform.position);
        }
    }
    private void StartDraw(Color color)
    {
        GameObject newLine = Instantiate(
            _LinePrefab,
            _LineSpawnPoint.position,
            Quaternion.identity
        );

        _activeLine = newLine.GetComponent<Line>();

        var lr = newLine.GetComponent<LineRenderer>();
        lr.endColor =color;
        lr.startColor = new Color(0, 0, 0, 0);

    }
    private void StopDraw()
    {
        _activeLine = null;
    }
}

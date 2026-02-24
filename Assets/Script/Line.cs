using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour
{
    public LineRenderer lineRender;
    List<Vector2> points;
    float alpha = 1;

    public void UpdateLine(Vector2 pos)
    {
        if(points == null)
        {
            points = new List<Vector2>();
            SetPoint(pos);
            return;
        }    
        if(Vector2.Distance(points.Last(),pos)>.1f)
        {
            SetPoint(pos);
        }    


    }
    private void SetPoint(Vector2 point)
    {
        points.Add(point);
        lineRender.positionCount = points.Count;
        lineRender.SetPosition(points.Count - 1, point);
    }
    private void Start()
    {
        //Destroy(this.gameObject, 100);
        StartCoroutine(UpdateAlpha());
    }
    IEnumerator UpdateAlpha()
    {
        while (alpha >= 0)
        {
            alpha -= 0.01f;
            yield return new WaitForSeconds(1);
            lineRender.endColor = new Color(lineRender.endColor.r, lineRender.endColor.g, lineRender.endColor.b, alpha);
        }
            Destroy(this.gameObject); 
    }
}

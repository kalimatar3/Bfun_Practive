using DuckGame.Ultilities;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Splines;
public static class ToolsHelper  
{
    public static void AddSplineToSpline(Spline sourceSpline, Spline targetSpline)
    {
        foreach (var knot in sourceSpline)
        {
            targetSpline.Add(knot);
        }
    }

}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SplineContainerLengthCaculator : MyBehaviour
{
    public SplineContainer splineContainer;
    public float LENGTH;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.splineContainer = GetComponent<SplineContainer>();
    }
    protected override void Awake()
    {
        base.Awake();
        splineContainer.Spline.changed += this.CalculateLength;
    }
    protected void OnDestroy()
    {
        splineContainer.Spline.changed -= this.CalculateLength;
    }
    public void CalculateLength()
    {
        if (splineContainer.Splines.Count > 1)
        {
            for (int i = 1; i < splineContainer.Splines.Count; i++)
            {
                ToolsHelper.AddSplineToSpline(splineContainer.Splines[i], splineContainer.Spline);
            }
            for (int i = 1; i < splineContainer.Splines.Count; i++)
            {
                splineContainer.RemoveSplineAt(i);
            }

        }
        LENGTH = splineContainer.CalculateLength();
    }
}
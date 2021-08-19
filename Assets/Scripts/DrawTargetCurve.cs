using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTargetCurve : MonoBehaviour
{
    public LineRenderer LineRenderer;

    private int _numPoints = 10;
    private Vector3[] positions = new Vector3[10];

    public Vector3[] DrawLinearCurve(Vector3 currentpoint, Vector3 nextPoint)
    {
        for (int i = 1; i < _numPoints + 1; i++)
        {
            float t = i / (float)_numPoints;
            positions[i - 1] = CalculatedLinearBezierPoint(t, currentpoint, nextPoint);
        }

        return positions;
    }

    private Vector3 CalculatedLinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }
}

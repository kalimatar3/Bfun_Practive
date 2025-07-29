using System;
using System.Collections.Generic;
using UnityEngine;
public static class MyCollisionFormula  
{
    public static Vector2 FindClosestLineinPoly(PolygonCollider2D polygon,Vector2 BasePoint) {
        float mindistance = Mathf.Infinity;
        Vector2[] posarray = polygon.GetPath(0);
        Vector2 closestline = Vector2.zero;
        for(int i = 0 ; i < posarray.Length; i++) {
            Vector2 p1 = posarray[i] + polygon.offset + (Vector2)polygon.GetComponent<Transform>().position;
            Vector2 p2 = posarray[(i + 1)% posarray.Length] + polygon.offset + (Vector2)polygon.GetComponent<Transform>().position;
            Vector2 line = p1 - p2;
            float distance = DistanceFromPointToLine(BasePoint,p1,p2);
            if(distance < mindistance) {
                mindistance = distance;
                closestline = line;
            }            
        }
        if(closestline == Vector2.zero) Debug.Log("zero");
        return closestline;
    }
    public static float DistanceFromPointToLine(Vector2 A, Vector2 B, Vector2 C)
    {
        // Vector BC
        Vector2 BC = C - B;

        // Vector từ B đến A
        Vector2 BA = A - B;

        // Tính khoảng cách bằng công thức
        float distance = Mathf.Abs(BC.y * BA.x - BC.x * BA.y) / BC.magnitude;
        return distance;
    }
    public static List<Vector3> RemoveDuplicates(List<Vector3> input, float threshold = 0.001f)
    {
        List<Vector3> result = new List<Vector3>();

        foreach (var v in input)
        {
            bool isDuplicate = false;

            foreach (var r in result)
            {
                if (Vector3.Distance(v, r) < threshold)
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (!isDuplicate)
                result.Add(v);
        }
        return result;
    }
}
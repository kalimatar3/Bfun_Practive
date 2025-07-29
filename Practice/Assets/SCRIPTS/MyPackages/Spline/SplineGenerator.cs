using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
public class SplineGenerator : MonoBehaviour
{
    [SerializeField] protected MeshFilter meshFilter;
    [SerializeField] protected SplineContainer splinecontainer;
    protected List<Vector3> vetexes;
    public void GenerarteSpline() {
        vetexes = meshFilter.sharedMesh.vertices.ToList();
        vetexes =  MyCollisionFormula.RemoveDuplicates(vetexes);
        splinecontainer.Spline.Clear();
        foreach(var ele in vetexes) {
            Vector3 worldpoint = meshFilter.transform.TransformPoint(ele);

            GameObject  gameObject =  new GameObject("Point");
            gameObject.transform.position = worldpoint;
            //splinecontainer.Spline.Add(worldpoint);
        }

    }
}

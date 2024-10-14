using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class TargetPath : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public GameObject pointPrefab;
    public int resolution = 10;

    private void Start ()
    {
        if (points.Count == 0)
        {
            points.Add(Vector3.zero);
            points.Add(Vector3.right * 5f);
        }
    }

    public void InstantiatePrefabsAtPoints ()
    {
        foreach (var point in points)
        {
            Instantiate(pointPrefab, point, Quaternion.identity);
        }
    }

    public Vector3 GetPointAt (int index)
    {
        if (index >= 0 && index < points.Count)
        {
            return points[index];
        }
        return Vector3.zero;
    }

    public List<Vector3> GetCatmullRomCurve ()
    {
        List<Vector3> curvePoints = new List<Vector3>();

        for (int i = 0; i < points.Count - 1; i++)
        {
            var p0 = i == 0 ? points[i] : points[i - 1];
            var p1 = points[i];
            var p2 = points[i + 1];
            var p3 = (i + 2 < points.Count) ? points[i + 2] : points[i + 1];

            for (int t = 0; t <= resolution; t++)
            {
                var tNorm = t / (float) resolution;
                curvePoints.Add(CatmullRom(p0, p1, p2, p3, tNorm));
            }
        }

        return curvePoints;
    }

    private Vector3 CatmullRom (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var t2 = t * t;
        var t3 = t2 * t;

        var result = 0.5f * ((2f * p1) +
                                (-p0 + p2) * t +
                                (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
                                (-p0 + 3f * p1 - 3f * p2 + p3) * t3);

        return result;
    }
}
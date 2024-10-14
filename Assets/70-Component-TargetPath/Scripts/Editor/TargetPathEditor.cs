using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TargetPath))]
public class TargetPathEditor : Editor
{
    private void OnSceneGUI ()
    {
        var targetPath = (TargetPath) target;

        Handles.color = Color.white;
        targetPath.pointPrefab = (GameObject) EditorGUILayout.ObjectField("Prefab", targetPath.pointPrefab, typeof(GameObject), false);

        var curvePoints = targetPath.GetCatmullRomCurve();

        for (var i = 0; i < curvePoints.Count - 1; i++)
        {
            Handles.DrawLine(curvePoints[i], curvePoints[i + 1]);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(targetPath);
        }
    }

    public override void OnInspectorGUI ()
    {
        var targetPath = (TargetPath) target;

        DrawDefaultInspector();

        if (GUILayout.Button("ADD POINT"))
        {
            var newPoint = Vector3.zero;
            if (targetPath.points.Count > 0)
            {
                newPoint = targetPath.points[targetPath.points.Count - 1] + Vector3.forward;
            }
            targetPath.points.Add(newPoint);
        }

        if (GUILayout.Button("INSTANCE"))
        {
            if (targetPath.pointPrefab != null)
            {
                targetPath.InstantiatePrefabsAtPoints();
            }
        }
    }
}

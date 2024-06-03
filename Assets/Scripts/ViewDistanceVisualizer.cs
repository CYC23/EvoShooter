
using UnityEngine;

public class ViewDistanceVisualizer : MonoBehaviour {
    public float radius;
    public void Start() {
        if (TryGetComponent<Spitter>(out var spitter)) {
            radius = spitter.ViewDistance;
        }
        else if (TryGetComponent<Tank>(out var tank)) {
            radius = tank.ViewDistance;
        }
        else if (TryGetComponent<Charger>(out var charger)) {
            radius = charger.ViewDistance;
        }
        else if (TryGetComponent<Charger>(out var agent))
        {
            radius = agent.ViewDistance;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        DrawGizmoCircle(transform.position, radius);
    }
    void DrawGizmoCircle(Vector3 center, float radius) {
        float theta = 0;
        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);
        Vector3 pos = center + new Vector3(x, y, 0);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f) {
            x = radius * Mathf.Cos(theta);
            y = radius * Mathf.Sin(theta);
            newPos = center + new Vector3(x, y, 0);
            Gizmos.DrawLine(lastPos, newPos);
            lastPos = newPos;
        }
        // To close the circle
        Gizmos.DrawLine(lastPos, pos);
    }
}

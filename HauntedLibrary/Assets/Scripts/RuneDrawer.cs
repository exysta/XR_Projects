using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class RuneDrawer : MonoBehaviour
{
    public InputActionReference triggerAction;
    public LineRenderer linePrefab;
    public float minDistance = 0.01f;
    public string targetTag = "RuneZone";

    private LineRenderer currentLine;
    private List<Vector3> points = new List<Vector3>();
    private bool isDrawing = false;

    void Update()
    {
        // Check if the trigger is pressed and inside the RuneZone
        if (triggerAction.action.enabled)
        {
            if (triggerAction.action.enabled && IsInRuneZone())
            {
                if (!isDrawing) StartDrawing();
                UpdateDrawing();
            }
            else if (isDrawing)
            {
                StopDrawing();
                CheckRunePattern();
            }
        }
    }

    bool IsInRuneZone()
    {
        return Physics.Raycast(controller.transform.position, controller.transform.forward, out RaycastHit hit, 1f)
               && hit.collider.CompareTag(targetTag);
    }

    void StartDrawing()
    {
        currentLine = Instantiate(linePrefab, controller.transform.position, Quaternion.identity);
        points.Clear();
        points.Add(controller.transform.position);
        isDrawing = true;
    }

    void UpdateDrawing()
    {
        Vector3 currentPos = controller.transform.position;
        if (Vector3.Distance(points[points.Count - 1], currentPos) > minDistance)
        {
            points.Add(currentPos);
            currentLine.positionCount = points.Count;
            currentLine.SetPositions(points.ToArray());
        }
    }

    void StopDrawing()
    {
        isDrawing = false;
        Destroy(currentLine.gameObject, 1f); // Optional: Fade out after 1 second
    }

    void CheckRunePattern()
    {
        // Simplified recognition logic
        if (IsCircle()) ActivateRune("Circle");
        else if (IsTriangle()) ActivateRune("Triangle");
        else if (IsX()) ActivateRune("X");
    }

    bool IsCircle()
    {
        // Check if the start/end points are close and the path is roughly circular
        float distanceThreshold = 0.1f;
        if (points.Count < 20) return false; // Need enough points
        return Vector3.Distance(points[0], points[points.Count - 1]) < distanceThreshold;
    }

    bool IsTriangle()
    {
        // Check for 3 distinct "corners" in the path
        int directionChanges = 0;
        Vector3 prevDirection = (points[1] - points[0]).normalized;
        for (int i = 2; i < points.Count; i++)
        {
            Vector3 currentDirection = (points[i] - points[i - 1]).normalized;
            if (Vector3.Angle(prevDirection, currentDirection) > 45f) directionChanges++;
            prevDirection = currentDirection;
        }
        return directionChanges >= 3;
    }

    bool IsX()
    {
        // Check for two crossing lines
        if (points.Count < 10) return false;
        Vector3 firstHalfAvg = AverageDirection(0, points.Count / 2);
        Vector3 secondHalfAvg = AverageDirection(points.Count / 2, points.Count);
        return Vector3.Angle(firstHalfAvg, secondHalfAvg) > 60f;
    }

    Vector3 AverageDirection(int start, int end)
    {
        Vector3 sum = Vector3.zero;
        for (int i = start; i < end; i++)
            sum += (points[i] - points[i - 1]).normalized;
        return sum.normalized;
    }

    void ActivateRune(string runeType)
    {
        Debug.Log($"Rune activated: {runeType}");
        // Add your door-opening/puzzle logic here
    }
}
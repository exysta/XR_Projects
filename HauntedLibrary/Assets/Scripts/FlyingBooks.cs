using UnityEngine;
using System.Collections.Generic;

public class BooksOrbit : MonoBehaviour
{
    [Header("Orbit Settings")]
    [Tooltip("The target to orbit around (e.g., the character). If left empty, it uses this GameObject.")]
    public Transform target;
    [Tooltip("Prefab of the book GameObject.")]
    public GameObject bookPrefab;
    [Tooltip("Number of books to orbit.")]
    public int numberOfBooks = 5;
    [Tooltip("Distance from the target.")]
    public float radius = 5f;
    [Tooltip("Rotation speed in degrees per second.")]
    public float speed = 30f;
    [Tooltip("Vertical offset for the books.")]
    public float heightOffset = 0f;

    [Header("Book Rotation")]
    [Tooltip("Should the books face the center?")]
    public bool faceCenter = true;
    [Tooltip("If true, only rotate around the Y-axis (keeps books upright).")]
    public bool rotateYAxisOnly = true;
    [Tooltip("Additional rotation offset (in Euler angles) applied after facing the center.")]
    public Vector3 bookRotationOffset = Vector3.zero;

    // Internal list to keep track of the instantiated books.
    private List<GameObject> books = new List<GameObject>();
    // Global angle offset that increments over time.
    private float currentAngle = 0f;

    void Start()
    {
        // If no target is specified, use the current GameObject.
        if (target == null)
        {
            target = transform;
        }

        // Instantiate the books in a circle around the target.
        for (int i = 0; i < numberOfBooks; i++)
        {
            // Evenly spaced angles around the circle.
            float angle = i * Mathf.PI * 2f / numberOfBooks;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            offset.y = heightOffset;
            // Instantiate the book as a child of this GameObject.
            GameObject book = Instantiate(bookPrefab, target.position + offset, Quaternion.identity, transform);
            books.Add(book);
        }
    }

    void Update()
    {
        // Update the global angle offset (convert speed from degrees to radians).
        currentAngle += speed * Time.deltaTime;
        float angleOffset = currentAngle * Mathf.Deg2Rad;

        // Update each book's position and rotation.
        for (int i = 0; i < books.Count; i++)
        {
            // Compute the new angle for this book.
            float angle = i * Mathf.PI * 2f / numberOfBooks + angleOffset;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            offset.y = heightOffset;
            // Set the book's position relative to the target.
            books[i].transform.position = target.position + offset;

            // If set to face the center, adjust its rotation.
            if (faceCenter)
            {
                // Determine the direction from the book to the target.
                Vector3 direction = target.position - books[i].transform.position;
                if (rotateYAxisOnly)
                {
                    // Zero out vertical difference so the book stays upright.
                    direction.y = 0;
                    if (direction != Vector3.zero)
                    {
                        books[i].transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                    }
                }
                else
                {
                    if (direction != Vector3.zero)
                    {
                        books[i].transform.rotation = Quaternion.LookRotation(direction);
                    }
                }
                // Apply any additional rotation offset.
                books[i].transform.rotation *= Quaternion.Euler(bookRotationOffset);
            }
        }
    }
}

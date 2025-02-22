using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBooks : MonoBehaviour
{
    public Transform character; // Assign your character in the inspector
    public GameObject bookPrefab; // Assign a book prefab in the inspector
    public int numberOfBooks = 5;
    public float radius = 3f;
    public float speed = 50f;
    private List<GameObject> books = new List<GameObject>();
    private List<float> angles = new List<float>();

    void Start()
    {
        // Spawn books around the character
        for (int i = 0; i < numberOfBooks; i++)
        {
            float angle = i * (360f / numberOfBooks);
            Vector3 position = character.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, 1f, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
            GameObject book = Instantiate(bookPrefab, position, Quaternion.identity);
            books.Add(book);
            angles.Add(angle);
        }
    }

    void Update()
    {
        // Rotate books around the character
        for (int i = 0; i < books.Count; i++)
        {
            angles[i] += speed * Time.deltaTime;
            Vector3 newPos = character.position + new Vector3(Mathf.Cos(angles[i] * Mathf.Deg2Rad) * radius, 1f, Mathf.Sin(angles[i] * Mathf.Deg2Rad) * radius);
            books[i].transform.position = newPos;
            books[i].transform.LookAt(character.position + Vector3.up * 1f); // Make books face the character
        }
    }
}

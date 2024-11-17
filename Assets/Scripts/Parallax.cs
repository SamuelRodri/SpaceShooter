using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private float widthImage;
    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        float module = (movementSpeed * Time.time) % widthImage;

        transform.position = initialPos + module * movementDirection;
    }
}

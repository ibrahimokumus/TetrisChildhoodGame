using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     SpawnerManager spawnerManager;
     BoardManager boardManager;
     private ShapeManager currentShape;
    void Start()
    {
        spawnerManager = GameObject.FindObjectOfType<SpawnerManager>();
        boardManager = GameObject.FindObjectOfType<BoardManager>();

        if (spawnerManager)
        {
            if (currentShape==null)
            {
                currentShape = spawnerManager.createShape();
            }
        }
    }

    private void Update()
    {
        if (!boardManager || !spawnerManager)
        {
            return;
        }

        if (currentShape)
        {
            currentShape.downMovement();
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private BoxCollider2D gridArea;
    [SerializeField] private SnakeMovement snakemovement;
    private List<Vector3> allGrid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        Instance = this;

        gridArea = this.GetComponent<BoxCollider2D>();
        allGrid = new List<Vector3>();
        
        var maxX = Mathf.Round(gridArea.bounds.max.x);
        var maxY = Mathf.Round(gridArea.bounds.max.y);

        for (int i = (int)(-maxX / 2); i <= (int)(maxX / 2); i++)
        {
            for (int j = (int)(-maxY / 2); j <= (int)(maxY / 2); j++)
            {
                allGrid.Add(new Vector3(i, j, 0));
            }
        }
    }

    void Start()
    {
        GenerateFood();
    }

    private void GenerateFood()
    {
        GameObject food = Instantiate(foodPrefab);
    }

    public List<Vector3> GetAvaiableGrids()
    {
        var snakePositions = snakemovement.GetSegments();
        return allGrid.Except(snakePositions).ToList();
    }

}

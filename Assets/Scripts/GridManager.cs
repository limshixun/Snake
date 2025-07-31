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

    [SerializeField] private int snakeThreshold;
    private List<Vector3> allGrid;
    private List<GameObject> _foods;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        Instance = this;

        gridArea = this.GetComponent<BoxCollider2D>();
        allGrid = new List<Vector3>();
        _foods = new List<GameObject>();

        var maxX = Mathf.Round(gridArea.bounds.max.x);
        var maxY = Mathf.Round(gridArea.bounds.max.y);

        for (int i = (int)(-maxX / 2); i <= (int)(maxX / 2); i++)
        {
            for (int j = (int)(-maxY / 2); j <= (int)(maxY / 2); j++)
            {
                allGrid.Add(new Vector3(i, j, 0));
            }
        }
        snakeThreshold = allGrid.Count / 4;
    }

    void Start()
    {
        ResetGrid();
    }

    public List<Vector3> GetAvaiableGrids()
    {
        var snakePositions = snakemovement.GetSegments();
        var aGrid = allGrid.Except(snakePositions).ToList();
        if (aGrid.Count == 0)
        {
            GameManager.Instance.gameOver(true);
        }
        return aGrid;
    }

    void GenerateFoods(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject food = Instantiate(foodPrefab);
            _foods.Add(food);
        }
    }

    public void CheckDestroyFood(GameObject food)
    {
        if (snakemovement.GetSegments().Count > snakeThreshold)
        {
            if (_foods.Remove(food))
            {
                Destroy(food);
                snakeThreshold *= 2;
            }
        }
    }

    public void ResetGrid()
    {
        for (int i = 0; i < _foods.Count; i++)
        {
            Destroy(_foods[i].gameObject);
        }
        _foods.Clear();
        GenerateFoods(4);
    }
}

using UnityEngine;

public class Food : MonoBehaviour
{
    // [SerializeField] private BoxCollider2D gridArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        RandomizePosition();

    }

    void RandomizePosition()
    {
        var availableGrid = GridManager.Instance.GetAvaiableGrids();
        int randIdx = Random.Range(0,availableGrid.Count);

        this.transform.position = availableGrid[randIdx];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RandomizePosition();    
        }
    }
}

using UnityEngine;

public class Food : MonoBehaviour
{

    [SerializeField] private BoxCollider2D gridArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizePosition();
    }

    void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.y));
        float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

        this.transform.position = new Vector3(x, y, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RandomizePosition();    
        }
    }
}

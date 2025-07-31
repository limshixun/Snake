using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private SpriteRenderer sr;

    void Start()
    {
        StartCoroutine(RandomizePosition());
    }

    private IEnumerator RandomizePosition()
    {
        GridManager.Instance.CheckDestroyFood(gameObject);
        float randTime = Random.Range(0f, 10f);
        ToggleFood(false);
        yield return new WaitForSeconds(randTime);
        ToggleFood(true);
        var availableGrid = GridManager.Instance.GetAvaiableGrids();
        int randIdx = Random.Range(0, availableGrid.Count);
        this.transform.position = availableGrid[randIdx];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(RandomizePosition());
        }
    }

    private void ToggleFood(bool on)
    {
        bc.enabled = on;
        sr.enabled = on;
    }
}

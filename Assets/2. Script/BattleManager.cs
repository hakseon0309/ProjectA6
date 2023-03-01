using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    public float spawnInterval = 1f;
    public float cardSpeed = 5f;
    public float yOffset = 2f;
    public float xOffset = -2f;

    void Start()
    {
        StartCoroutine(SpawnCards());
    }

    IEnumerator SpawnCards()
    {
        yield return new WaitForSeconds(1f);

        Vector2 startPos = new Vector2(12f, 0f);

        for (int i = 0; i < cardPrefabs.Length; i++)
        {
            GameObject card = Instantiate(cardPrefabs[i], startPos, Quaternion.identity);
            card.transform.SetParent(transform);
            card.transform.position = new Vector3(startPos.x + i * xOffset, startPos.y, 0f);

            Rigidbody2D rb = card.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-cardSpeed, 0f);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaceAndScore : MonoBehaviour
{
    [SerializeField] private Collider spawnArea = null;
    [SerializeField] private Collider thisCollider = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    private int score = 0;

    private void Start() { Relocate(); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Relocate();
            score++;
            scoreText.text = "Score: " + score;
        }
    }

    private void Relocate()
    {
        Vector3 newPos = new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
        );

        var overlaps = Physics.OverlapSphere(
            newPos,
            MaxComponent(thisCollider.bounds.extents) + 0.25f,
            ~0,
            QueryTriggerInteraction.Ignore
        );

        while (overlaps.Length > 0)
        {
            newPos.y = overlaps[0].bounds.max.y + thisCollider.bounds.extents.y + 0.3f;
            overlaps = Physics.OverlapSphere(
                newPos,
                MaxComponent(thisCollider.bounds.extents) + 0.25f,
                ~0,
                QueryTriggerInteraction.Ignore
            );
        }

        transform.position = newPos;
        Debug.Log(newPos);
    }

    private float MaxComponent(Vector3 vector) { return Mathf.Max(vector.x, vector.y, vector.z); }
}

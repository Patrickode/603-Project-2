using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaseTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [Tooltip("The distance this object moves per second towards target.")]
    [SerializeField] private float speed;

    private Camera cachedCam;

    private void Update()
    {
        if (target)
        {
            transform.LookAt(target.transform, Vector3.up);
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.transform.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            if (!cachedCam) { cachedCam = Camera.main; }
            Vector3 targetPos = cachedCam.transform.position + cachedCam.transform.forward * 1.5f;

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(
                    transform.position != targetPos
                        ? targetPos - transform.position
                        : cachedCam.transform.position - transform.position
                    ),
                Mathf.Rad2Deg * (speed * Time.deltaTime)
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(target))
        {
            Destroy(target);
            StartCoroutine(DelayedRestart(2.5f));
        }
    }

    private IEnumerator DelayedRestart(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

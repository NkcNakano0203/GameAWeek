using UnityEngine;

public class AllDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => Destroy(other.gameObject);
}
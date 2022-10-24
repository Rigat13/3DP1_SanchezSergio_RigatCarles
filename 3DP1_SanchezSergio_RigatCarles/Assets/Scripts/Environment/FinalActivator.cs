using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalActivator : MonoBehaviour
{
    [SerializeField] List<EnemyAI> enemiesToActivate;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            foreach (EnemyAI enemy in enemiesToActivate)
                enemy.enabled = true;
        }
    }
}

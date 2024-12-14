using Unity.VisualScripting;
using UnityEngine;

public class TriggerAttackRange: MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();  
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.InAttackRange = true;
            enemy.Attacking = true;

            enemy.FlipTowardPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.InAttackRange = false;
            enemy.Attacking = false;
        }
    }
}

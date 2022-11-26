using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            if (GameManager.IsShip)
            {
                try
                {
                    GameManager.OnPlayerTransformation.Invoke();
                }
                catch { }
                return;
            }
            Obstacles obstacle = collision.gameObject.GetComponent<Obstacles>();
            
            Debug.Log(collision.gameObject.name);
            if (!obstacle.KillWithoutCatching)
            {
                if (obstacle.CatchBack)
                    player.OnCatchedBack?.Invoke();
                else
                    player.OnCatchedFront?.Invoke();
            }
            player.OnDead?.Invoke();

        }
        else if (collision.gameObject.CompareTag("ObstaclesSide")&&player!=null)
        {
            if (!player.IsStunned)
            {
                Debug.Log(collision.gameObject.name);
                player.OnStunned?.Invoke();
            }

        }

    }
}

using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private enum State
    {
        Roaming
    }

    private State state;
    private EnemyRoaming enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyRoaming>();
        state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = enemyPathfinding.GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(Random.Range(1f,3f));
        }
    }
    

}

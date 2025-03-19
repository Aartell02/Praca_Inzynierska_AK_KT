using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public Transform player;

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10); // Camera follows the player but 6 to the right
    }
}

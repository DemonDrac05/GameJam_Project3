using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
}

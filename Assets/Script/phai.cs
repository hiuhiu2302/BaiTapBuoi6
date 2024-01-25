using UnityEngine;
using UnityEngine.UI;

public class CharacterMovementButton : MonoBehaviour
{
    public string characterTag = "Player";
    public float moveSpeed = 5f;

    private GameObject character;
   

    private void Start()
    {
        // Lấy tham chiếu đến đối tượng nhân vật dựa trên tag
        character = GameObject.FindGameObjectWithTag(characterTag);
    }

    public void OnMoveButtonClicked()
    {
        // Thực hiện hành động di chuyển khi button được nhấn
        if (character != null)
        {
            // Di chuyển nhân vật theo hướng nào đó
            Debug.Log("phai");
            transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        }
    }
}
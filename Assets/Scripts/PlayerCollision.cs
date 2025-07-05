using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManage GameManage;
    private AudioManager audioManager;
    private void Awake()
    {
        GameManage=FindAnyObjectByType<GameManage>();
        audioManager=FindAnyObjectByType<AudioManager>();
    }
    private  void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            audioManager.PlayCoinSound();
            GameManage.AddScore(1);
            
        }
        else if(collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            GameManage.GameWin();
        }
    }
}

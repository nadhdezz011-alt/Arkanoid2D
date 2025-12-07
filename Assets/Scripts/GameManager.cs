using UnityEngine;

public class GameManager : MonoBehaviour
{
   public int hearts = 3;
    public void LoseHeart()
    {
        hearts--;
        if (hearts <= 0)
        {
            // Game Over logic here
            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("Hearts left: " + hearts);
        }
    }
}

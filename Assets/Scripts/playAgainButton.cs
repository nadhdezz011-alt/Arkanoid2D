using UnityEngine;
using UnityEngine.SceneManagement;

public class playAgainButton : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}

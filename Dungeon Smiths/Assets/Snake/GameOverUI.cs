using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update

	public void Restart(){
		SceneManager.LoadScene("Game");
	}
}

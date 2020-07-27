using TapsellSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelClickHandler : MonoBehaviour
{

	private readonly string TAPSELL_KEY = "kilkhmaqckffopkpfnacjkobgrgnidkphkcbtmbcdhiokqetigljpnnrbfbnpnhmeikjbq";

	void Start () {
		Tapsell.Initialize (TAPSELL_KEY);
	}

	public void LoadLevel(int level){
		SceneManager.LoadScene (level);
	}
}

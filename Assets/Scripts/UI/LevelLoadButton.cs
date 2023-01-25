using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LevelLoadButton : MonoBehaviour
{
	[SerializeField]
	private bool reloadCurrent;
	[SerializeField,Scene,HideIf("reloadCurrent")]
	private string levelName;
	private Button button;
	[SerializeField]
	private bool addBackAction;
	void Awake()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(() => LoadLevel(levelName));
	}
	void LoadLevel(string levelName)
	{
		if(reloadCurrent)
			levelName = SceneManager.GetActiveScene().name;

		Preloader.Instance.LoadLevel(levelName,addBackAction);
	}
	void OnDestroy()
	{
		button.onClick.RemoveAllListeners();
	}
}

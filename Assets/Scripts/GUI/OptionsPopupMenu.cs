using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsPopupMenu : Menu
{
	protected override void Init()
	{
		AddItem("Exit Map");

		ButtonStyle.contentOffset = new Vector2(4, 0);
	}

	protected override void OnButtonPress(string item)
	{
		switch (item)
		{
		case "Exit Map":SceneManager.LoadScene("MainMenu");break;
		}
		
		Hide();
	}
}
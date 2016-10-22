using UnityEngine;

public class StartMenuGUI : MonoBehaviour {

	private const float fadeDuration = 0.3F;

	[SerializeField]
	private GameObject mainPanelParent;
	[SerializeField]
	private GameObject playPanelParent;
	[SerializeField]
	private GameObject optionPanelParent;
	[SerializeField]
	private GameObject scorePanelParent;

	// Can't use args in Unity built-in method call system
	public void DisplayMainPanel() {
		DisplayPanel(mainPanelParent);
	}
	
	public void HideMainPanel() {
		HidePanel(mainPanelParent);
	}

	public void DisplayPlayPanel() {
		DisplayPanel(playPanelParent);
	}

	public void HidePlayPanel() {
		HidePanel(playPanelParent);
	}

	public void DisplayOptionPanel() {}

	public void HideOptionPanel() {}

	public void DisplayScorePanel() {}

	public void HideScorePanel() {}

	private void DisplayPanel(GameObject panelParent) {
		UnityEngine.UI.Graphic[] panelButtons = panelParent.GetComponentsInChildren<UnityEngine.UI.Graphic>();
		for (int i = 0; i < panelButtons.Length; i++) {
			panelButtons[i].CrossFadeAlpha(1.0F, fadeDuration, false);
		}
	}

	private void HidePanel(GameObject panelParent) {
		UnityEngine.UI.Graphic[] panelButtons = panelParent.GetComponentsInChildren<UnityEngine.UI.Graphic>();
		for (int i = 0; i < panelButtons.Length; i++) {
			panelButtons[i].CrossFadeAlpha(0.0F, fadeDuration, false);
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalController : MonoBehaviour {

	private GameObject controlPanel;
	void Start() {
		controlPanel = GameObject.Find ("Control Screen");
		controlPanel.SetActive (false);
	}
	public void OpenControls() {
		controlPanel.SetActive (true);
	}

	public void CloseControls() {
		controlPanel.SetActive (false);
	}

}

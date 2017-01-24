using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[Header("J1")]
	public GameObject HeartLife_J1;
	public GameObject ManaJauge_J1;
	public GameObject Victoire_J1;

	[Header("J2")]
	public GameObject HeartLife_J2;
	public GameObject ManaJauge_J2;
	public GameObject Victoire_J2;

	void Awake()
	{
		Instance = this;
	}
}

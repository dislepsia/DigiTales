//using UnityEngine;
#if UNITY_EDITOR

using UnityEditor;

public class ResetearPlayerPrefabs : EditorWindow
{
	[MenuItem("Window/Delete PlayerPrefs (All)")]
	static void DeleteAllPlayerPrefs()
	{
		UnityEngine.PlayerPrefs.DeleteAll();
	}
}
#endif
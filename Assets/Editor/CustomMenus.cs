using UnityEngine;
using UnityEditor;

public static class CustomMenus
{
	[MenuItem("GameObject/Snap to Grid")]
	static void SnapToGrid()
	{
		Transform[] selectedTransforms = Selection.GetTransforms(SelectionMode.TopLevel);
		
		foreach (Transform t in selectedTransforms)
		{
			//Tell the undo system that this object is about to be modified
			Undo.RecordObject(t, "Snap to Zero");
			
			Vector3 pos = t.localPosition;
			pos.x = Mathf.Round(pos.x);
			pos.y = Mathf.Round(pos.y);
			pos.z = Mathf.Round(pos.z);
			t.localPosition = pos;
		}
	}
}
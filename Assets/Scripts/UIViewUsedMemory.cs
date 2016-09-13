using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class UIViewUsedMemory : MonoBehaviour {

	[SerializeField] Text memoryText;

#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern int getUsedMemorySize(); 
#endif

	void Update () {
		float m = 0;
#if UNITY_EDITOR
		m = ( System.GC.GetTotalMemory(false) + Profiler.usedHeapSize ) / 1024f;
#elif UNITY_IOS
		m = getUsedMemorySize() / 1024f;
#elif UNITY_ANDROID
		using ( AndroidJavaClass plugin = new AndroidJavaClass("com.veniegames.memorychecker.Main")){
			m = plugin.CallStatic<long>("getUsedMemorySize");
		}
#endif
		memoryText.text = m + "KB  " +  (m/1024f) + "MB";
	}
}

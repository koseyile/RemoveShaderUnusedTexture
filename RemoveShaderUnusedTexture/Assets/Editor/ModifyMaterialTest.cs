using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ModifyMaterialTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[MenuItem ("ChangeMaterial/RemoveAllMaterialUnusedTexture")]
	static void RemoveAllMaterialUnusedTexture(){

		string[] guids = AssetDatabase.FindAssets ("t:material");
		foreach (var g in guids) {
			Debug.Log (AssetDatabase.GUIDToAssetPath(g));
			Material m = AssetDatabase.LoadAssetAtPath<Material> (AssetDatabase.GUIDToAssetPath(g));
			SetTextureToNull (m);
		}

		Debug.Log ("Remove unused textures finished");
	}

	static void SetTextureToNull(Material m){

		if (m == null) {
			//Debug.Log ("nnnn");
		}

		string shaderPath = AssetDatabase.GetAssetPath (m.shader.GetInstanceID ());
		Debug.Log (shaderPath);

		List<string> textureNameList = new List<string>();
		getUnusedTextureName (shaderPath, textureNameList);

		foreach (var i in textureNameList) {
			//Debug.Log (i);
			m.SetTexture (i, null);
		}

		//m.mainTexture = null;
		//m.SetTexture ("_MainTex", null);
		AssetDatabase.SaveAssets ();
		//AssetDatabase.Refresh ();
	}

	static void getUnusedTextureName(string shaderPath, List<string> textureNameList)
	{
		StreamReader sr = new StreamReader (shaderPath);

		while (!sr.EndOfStream) {
			string line = sr.ReadLine ();
			//Debug.Log (line);

			line = line.Trim ();

			if (line.StartsWith ("//")) {
				//Debug.Log (line);
				continue;
			}

			if (line.StartsWith ("sampler2D")) {
				//Debug.Log (line);
				int index = line.IndexOf ("sampler2D")+"sampler2D".Length;
				string texVariable = line.Substring (index);
				texVariable = texVariable.Trim ();
				char[] trimChar = {';'};
				texVariable = texVariable.TrimEnd (trimChar);
				textureNameList.Add (texVariable);
				//Debug.Log (texVariable);
				continue;
			}

			List<string> removeStringList = new List<string> ();
			foreach(var tn in textureNameList){
				if (line.Contains (tn)) {
					//Debug.Log ("check "+ line);
					removeStringList.Add(tn);
				}
			}

			foreach (var rs in removeStringList) {
				//Debug.Log ("Remove " + rs);
				textureNameList.Remove(rs);
			}
		

		}


		//textureNameList.Add ("_MainTex");
	}
}

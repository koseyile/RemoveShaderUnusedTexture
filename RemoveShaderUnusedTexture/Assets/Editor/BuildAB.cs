using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildAB : MonoBehaviour {

	[MenuItem("Build Asset Bundles/Normal")]
	static void BuildABsNone()
	{
		//Create a folder to put the Asset Bundle in.
		// This puts the bundles in your custom folder (this case it's "MyAssetBuilds") within the Assets folder.
		//Build AssetBundles with no special options
		BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXIntel64);
	}
}

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml;
using LitJson;

/// <summary>
/// Model user interface xml editor.
/// </summary>
public class ModelUIXmlEditor : EditorWindow
{
	private GameObject		m_goPhoto;
	private RenderTexture	m_Texture;
	private Camera 			m_Camera;
	private List<string>	m_dModel = new List<string>();
	private List<string>	m_dNames = new List<string> ();
	private List<int>		m_dValue = new List<int>();
	private int 			m_nSelected = 0;
	private int 			m_nLastSelectd = 0;
	private GameObject 		m_goModel;
	private float 			m_fLastTime;

	private Dictionary<string, int> 		
		SModlePopup = new Dictionary<string, int>();

	public enum World {
		Position, Scale, Angle, Select	
	}

	static string AssetSavePath = "Assets/Resources/ShapeAssetObject";

	/// <summary>
	/// Creates the xml editor.
	/// </summary>
	[MenuItem("Window/Custom/ModelUIXmlEditor")]
	public static void CreateXmlEditor()
	{
		ModelUIXmlEditor window = (ModelUIXmlEditor)EditorWindow.GetWindowWithRect((typeof(ModelUIXmlEditor)), new Rect(0, 0, 512, 800), true);
		if (window) 
		{
			window.Show();
			window.Focus();
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ModelUIXmlEditor"/> class.
	/// </summary>
	public 	ModelUIXmlEditor()
	{
		//EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
	}

	/// <summary>
	/// Init this instance.
	/// </summary>
	private void 	Init()
	{
		GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Resources/UI/UIRenderTexture/UI_MODEL.prefab");
		if (!prefab)
			throw new System.NullReferenceException ();

		m_goPhoto = GameObject.Instantiate (prefab);
		if (m_goPhoto) {
			m_Camera = m_goPhoto.GetComponentInChildren<Camera> ();
			if (!m_Camera)
				throw new System.NullReferenceException ();

			m_Texture = m_Camera.targetTexture;
		}		
	}

	/// <summary>
	/// Updates the file list.
	/// </summary>
	public void 	UpdateFileList()
	{
		string[] aryInputPath = {
			"Resources/Hero", "Resources/Monster"
		};

		m_dModel.Clear ();

		int iStart = 0;

		foreach (string path in aryInputPath) {
			List<string> aryList = EditorHelper.SearchFile (SearchFileType.prefab, 
				string.Format ("{0}/{1}", Application.dataPath, path));
			
			foreach (string name in aryList) {
				m_dNames.Add (EditorHelper.GetFileName (name));
				m_dValue.Add (iStart++);
			}

			m_dModel.AddRange (aryList);
		}
	}

	/// <summary>
	/// Resets the modle.
	/// </summary>
	/// <param name="index">Index.</param>
	private void 	ResetModle(string szAssetPath)
	{
		GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> (szAssetPath);
		if (!prefab)
			throw new System.NullReferenceException ();

		if (m_goPhoto) {
			Transform parent = m_goPhoto.transform.Find ("MOUNT");
			if (parent) {
				for (int idx = 0; idx < parent.childCount; idx++) {
					GameObject.DestroyImmediate (parent.GetChild (idx).gameObject);
				}

				m_goModel = GameObject.Instantiate (prefab);
				if (m_goModel) {
					m_goModel.transform.SetParent (parent, false);

					Transform[] aryChild = m_goModel.GetComponentsInChildren<Transform> ();
					foreach (Transform child in aryChild) {
						child.gameObject.layer = LayerMask.NameToLayer ("UIModel");
					}
						
					m_goModel.transform.localPosition = new Vector3 (0, 0, -3.0f);
				}
			}
		}
	}

	/// <summary>
	/// Focus this instance.
	/// </summary>
	public void 	Focus()
	{
		UpdateFileList ();
	}
		
	/// <summary>
	/// Raises the enable event.
	/// </summary>
	public void 	OnEnable()
	{
		Init ();
	}
		
	/// <summary>
	/// Raises the GU event.
	/// </summary>
	public void 	OnGUI()
	{
		GUILayout.BeginVertical ();
		m_nSelected = EditorGUILayout.IntPopup(World.Select.ToString(), m_nSelected, m_dNames.ToArray(), m_dValue.ToArray());
		if (m_nLastSelectd != m_nSelected) {
			string szAssetPath = m_dModel [m_nSelected];
			if (!string.IsNullOrEmpty (szAssetPath)) {
				ResetModle (szAssetPath);
			}

			m_nLastSelectd = m_nSelected;
		}

		GUILayout.Label(string.Format ("{0}x{0}",
			position.width, position.width));

		GUILayout.Box(m_Texture, GUILayout.Width(position.width), GUILayout.Height(position.width));
		if (m_goModel) {
			m_goModel.transform.localPosition 	= EditorGUILayout.Vector3Field (World.Position.ToString(), 	m_goModel.transform.localPosition);
			m_goModel.transform.eulerAngles		= EditorGUILayout.Vector3Field (World.Angle.ToString(), 	m_goModel.transform.eulerAngles);
			m_goModel.transform.localScale		= EditorGUILayout.Vector3Field (World.Scale.ToString(), 	m_goModel.transform.localScale);

			if (GUILayout.Button ("Build")) {
				ShapeAssetObject asset = ScriptableObject.CreateInstance<ShapeAssetObject> ();
				if (asset) {
					asset.LocalPosition = m_goModel.transform.localPosition;
					asset.LocalAngle 	= m_goModel.transform.eulerAngles;
					asset.LocalScale 	= m_goModel.transform.localScale;

					AssetDatabase.CreateAsset(asset, string.Format("{0}/{1}.asset", AssetSavePath, m_dNames[m_nSelected]));
					AssetDatabase.SaveAssets ();
					AssetDatabase.Refresh ();
				}
			}
		}


		GUILayout.EndVertical();
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	public void 	Update()
	{
		float fElapsed = Time.realtimeSinceStartup - m_fLastTime;
		if (fElapsed > 0.33f) {
			m_fLastTime = Time.realtimeSinceStartup;

			if (m_goModel) {
				Animator animator = m_goModel.GetComponent<Animator> ();
				if (animator) {
					animator.Update (fElapsed);
				}
			}

			Repaint ();
		}
	}

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	public void 	OnDestroy()
	{
		EditorSceneManager.OpenScene("Assets/Scene/App.unity");
	}
}
using SLua;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[CustomLuaClass]
public class LuaBehaviourScript : MonoBehaviour
{
	protected LuaFunction m_luaAwake 		= null;
	protected LuaFunction m_luaStart 		= null;
	protected LuaFunction m_luaUpdate 		= null;
	protected LuaFunction m_luaLateUpdate 	= null;
	protected LuaFunction m_luaFixedUpdate 	= null;
	protected LuaFunction m_luaOnDestroy 	= null;
	protected LuaFunction m_luaEnable		= null;
	protected LuaFunction m_luaDisable 		= null;
	protected LuaFunction m_luaAttack 		= null;
	protected LuaFunction m_luaSound 		= null;
	protected LuaFunction m_luaEffect 		= null;
	protected LuaFunction m_luaEvent 		= null;
	protected LuaFunction m_luaForwadStart 	= null;
	protected LuaFunction m_luaForwadEnd 	= null;
	protected LuaFunction m_luaAnimatorMove = null;

	/// <summary>
	/// The m d lua func.
	/// </summary>
	protected Dictionary<string, LuaFunction> 
		m_dLuaFunc = new Dictionary<string, LuaFunction>();

	/// <summary>
	/// Gets the lua module.
	/// </summary>
	/// <value>The lua module.</value>
	public LuaTable 		LuaModule
	{ get; private set; }

	/// <summary>
	/// Gets the lua component.
	/// </summary>
	/// <returns>The lua component.</returns>
	/// <param name="go">Go.</param>
	public static LuaTable 	GetLuaBehaviourScript(GameObject go)
	{
		var luaCom = go.GetComponent<LuaBehaviourScript> ();
		if (null == luaCom) {
			return null;
		}

		return luaCom.LuaModule;
	}

	/// <summary>
	/// Adds the lua component.
	/// </summary>
	/// <returns>The lua component.</returns>
	/// <param name="go">Go.</param>
	/// <param name="luaTbl">Lua tbl.</param>
	public static LuaTable 	AddLuaBehaviourScript(GameObject go, LuaTable luaTbl)
	{
		var luaComp = go.AddComponent<LuaBehaviourScript>();
		luaComp.Initilize(luaTbl);

		return luaComp.LuaModule;
	}

	/// <summary>
	/// Initilize the specified luaTbl.
	/// </summary>
	/// <param name="luaTbl">Lua tbl.</param>
	private void 			Initilize(LuaTable luaTbl)
	{
		object chunk = luaTbl;
		if (chunk != null && chunk is LuaTable) {
			LuaModule = (LuaTable)chunk;

			LuaModule ["this"] 			= this;
			LuaModule ["transform"] 	= transform;
			LuaModule ["gameObject"] 	= gameObject;

			// lua call unity behaviour function
			m_luaAwake 			= LuaModule ["Awake"] as LuaFunction;
			m_luaStart 			= LuaModule ["Start"] as LuaFunction;
			m_luaUpdate 		= LuaModule ["Update"] as LuaFunction;
			m_luaLateUpdate 	= LuaModule ["LateUpdate"] as LuaFunction;
			m_luaFixedUpdate	= LuaModule ["FixedUpdate"] as LuaFunction;
			m_luaOnDestroy 		= LuaModule ["OnDestroy"] as LuaFunction;
			m_luaEnable 		= LuaModule ["OnEnable"] as LuaFunction;
			m_luaDisable 		= LuaModule ["OnDisable"] as LuaFunction;
			m_luaAttack 		= LuaModule ["OnAnimationAttack"] as LuaFunction;
			m_luaForwadStart 	= LuaModule ["OnAnimationForwardStart"] as LuaFunction;
			m_luaForwadEnd 		= LuaModule ["OnAnimationForwardEnd"] as LuaFunction;
			m_luaSound 			= LuaModule ["OnAnimationSound"] as LuaFunction;
			m_luaEffect 		= LuaModule ["OnAnimationEffect"] as LuaFunction;
			m_luaEvent 			= LuaModule ["OnAnimationEvent"] as LuaFunction;
			m_luaAnimatorMove	= LuaModule ["OnAnimatorMove"] as LuaFunction;
		}

		if (null == LuaModule) {
			Debug.LogError ("LuaComponent.Initilize: initilize with nil lua table");
		} else {
			if (m_luaAwake != null)
				m_luaAwake.call (LuaModule);
		}
	}

	/// <summary>
	/// Gets the lua function.
	/// </summary>
	/// <returns>The lua function.</returns>
	/// <param name="funcName">Func name.</param>
	private LuaFunction 	GetLuaFunction(string funcName)
	{
		if (LuaModule == null)
			return null;

		LuaFunction func = null;
		m_dLuaFunc.TryGetValue (funcName, out func);
		if (func == null) {
			func = LuaModule [funcName] as LuaFunction;
			m_dLuaFunc.Add (funcName, func);
		}

		return func;
	}

	[DoNotToLua]
	public object 			CallLuaFunction(string funcName)
	{
		LuaFunction func = GetLuaFunction (funcName);

		if (func != null)
			return func.call (LuaModule);

		if (func == null)
			Debug.LogError ("do not find lua function:" + funcName);

		return null;
	}

	[DoNotToLua]
	public object 			CallLuaFunction(string funcName, object obj)
	{
		LuaFunction func = GetLuaFunction (funcName);

		if (func != null)
			return func.call (LuaModule, obj);

		return null;
	}

	[DoNotToLua]
	public object 			GetProperty(string propertyName)
	{
		if (LuaModule == null)
			return null;

		return LuaModule [propertyName];
	}

	[DoNotToLua]
	public void 			SetProperty(string propertyName, object value)
	{
		if (LuaModule != null)
			LuaModule [propertyName] = value;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	IEnumerator Start()
	{
		if (m_luaStart != null) {
			object ret = m_luaStart.call (LuaModule);
			if (ret is IEnumerator)
				yield return StartCoroutine ((IEnumerator)ret);
			else
				yield return ret;
		}
	}

	// MonoBehaviour callback
	void Update()
	{
		if( m_luaUpdate != null )
			m_luaUpdate.call(LuaModule);
	}

	// MonoBehaviour callback
	void LateUpdate()
	{
		if( m_luaLateUpdate != null )
			m_luaLateUpdate.call(LuaModule);
	}

	// MonoBehaviour callback
	void FixedUpdate()
	{
		if( m_luaFixedUpdate != null )
			m_luaFixedUpdate.call(LuaModule);
	}

	// MonoBehaviour callback
	void OnDestroy()
	{
		if( m_luaOnDestroy != null )
			m_luaOnDestroy.call(LuaModule);
		
		m_dLuaFunc.Clear();

		if( LuaModule != null )
			LuaModule.Dispose();
	}

	void OnEnable()
	{
		if (m_luaEnable != null)
			m_luaEnable.call (LuaModule);
	}

	void OnDisable()
	{
		if (m_luaDisable != null)
			m_luaDisable.call (LuaModule);	
	}

	void OnAnimatorMove()
	{
		if (m_luaAnimatorMove != null)
			m_luaAnimatorMove.call (LuaModule);
	}

	void OnAnimationEvent(AnimationEvent evt)
	{
		#if UNITY_EDITOR
		Debug.Log(string.Format("OnAnimationEvent func={0} int={1} float={2} string={3}", 
			evt.functionName, evt.intParameter, evt.floatParameter, evt.stringParameter));
		#endif

		if (m_luaEvent != null)
			m_luaEvent.call (LuaModule, evt);		
	}

	void OnAnimationAttack(AnimationEvent evt)
	{
		#if UNITY_EDITOR
		Debug.Log(string.Format("OnAnimationAttack func={0} int={1} float={2} string={3}", 
			evt.functionName, evt.intParameter, evt.floatParameter, evt.stringParameter));
		#endif

		if (m_luaAttack != null)
			m_luaAttack.call (LuaModule, evt);
	}

	void OnAnimationForwardStart(AnimationEvent evt)
	{
		#if UNITY_EDITOR
		Debug.Log(string.Format("OnAnimationForwardStart func={0} int={1} float={2} string={3}", 
			evt.functionName, evt.intParameter, evt.floatParameter, evt.stringParameter));
		#endif

		if (m_luaForwadStart != null)
			m_luaForwadStart.call (LuaModule, evt);		
	}

	void OnAnimationForwardEnd(AnimationEvent evt)
	{
		#if UNITY_EDITOR
		Debug.Log(string.Format("OnAnimationForwardEnd func={0} int={1} float={2} string={3}", 
			evt.functionName, evt.intParameter, evt.floatParameter, evt.stringParameter));
		#endif

		if (m_luaForwadEnd != null)
			m_luaForwadEnd.call (LuaModule, evt);		
	}

	void OnAnimationSound(AnimationEvent evt)
	{
		#if UNITY_EDITOR
		Debug.Log(string.Format("OnAnimationSound func={0} int={1} float={2} string={3}", 
			evt.functionName, evt.intParameter, evt.floatParameter, evt.stringParameter));
		#endif

		if (m_luaSound != null)
			m_luaSound.call (LuaModule, evt);		
	}

	void OnAnimationEffect(AnimationEvent evt)
	{
		#if UNITY_EDITOR
		Debug.Log(string.Format("OnAnimationEffect func={0} int={1} float={2} string={3}", 
			evt.functionName, evt.intParameter, evt.floatParameter, evt.stringParameter));
		#endif

		if (m_luaEffect != null)
			m_luaEffect.call (LuaModule, evt);			
	}
}
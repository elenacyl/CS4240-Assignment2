using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;
using Valve.VR;

public class InteractiveCylinder : MonoBehaviour
{

	[SerializeField] private Material m_NormalMaterial;
	[SerializeField] private Material m_OverMaterial;
	[SerializeField] private Material m_ClickedMaterial;
	[SerializeField] private Material m_DoubleClickedMaterial;
	[SerializeField] private VRInteractiveItem m_InteractiveItem;
	[SerializeField] public Renderer m_Renderer;
    [SerializeField] public Transform m_camera;

    private AudioSource source;
	
    void Start() {
        source = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        m_Renderer.material = m_NormalMaterial;
    }

    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
    }

    private void OnDisable()
    {
    	m_InteractiveItem.OnOver -= HandleOver;
    	m_InteractiveItem.OnOut -= HandleOut;
    	m_InteractiveItem.OnClick -= HandleClick;
    	m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
    }

    // Handle the Over event
    private void HandleOver()
    {
    	Debug.Log("Show over state");
    	m_Renderer.material = m_OverMaterial;
    }

    // Handle the Out event
    private void HandleOut()
    {
    	Debug.Log("Show out state");
    	m_Renderer.material = m_NormalMaterial;
    }

    // Handle Click event
    public void HandleClick() {
    	Debug.Log("Show click state");
    	// m_Renderer.material = m_ClickedMaterial;
        source.Play();
        m_camera.position = this.transform.position;
    }

    // Handle the DoubleClick event
    private void HandleDoubleClick()
    {
    	Debug.Log("Show double click");
    	m_Renderer.material = m_DoubleClickedMaterial;
    }
}

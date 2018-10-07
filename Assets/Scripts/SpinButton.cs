using UnityEngine;
using UnityEngine.UI;

public class SpinButton : MonoBehaviour
{

	public GameObject Arrow;
	public Vector3 ArrowScaleTo;
	private bool _isShowingArrow;
	private bool _wheelSpinStart;
	private Vector3 _origScale;
	private Button _spinButton;
	private WheelController _wheelController;
	


	private void Awake()
	{
		_origScale = Arrow.transform.localScale;
		_wheelController = FindObjectOfType<WheelController>();
		_spinButton = GetComponent<Button>();
	}

	public void SetButtonState(bool isEnable)
	{
		if (isEnable)
		{
			_wheelSpinStart = false;
			ShowArrow();
			Debug.LogError(_spinButton + " : "+_wheelController);
			_spinButton.onClick.AddListener(_wheelController.Randomizer.OnRespinClick);
		}
		else
		{
			_wheelSpinStart = true;
			HideArrow();
			_spinButton.onClick.RemoveAllListeners();
		}
	}
	
	
	public void HideArrow()
	{
		_wheelSpinStart = true;
		if (Arrow != null)
			Arrow.SetActive(false);
	}

	public void ShowArrow()
	{
		_wheelSpinStart = false;
		if (Arrow != null)
			Arrow.SetActive(true);
	}
	
	void Update()
	{
		if (_wheelSpinStart) return;
		if (Arrow.transform.localScale == ArrowScaleTo)
			LeanTween.scale(Arrow, _origScale, 0.5f);
		else if (Arrow.transform.localScale == _origScale)
			LeanTween.scale(Arrow, ArrowScaleTo, 0.5f);
	}
	
	
}

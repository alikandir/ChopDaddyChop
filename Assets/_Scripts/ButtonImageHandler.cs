using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageHandler : MonoBehaviour
{
    private Dictionary<BattlePatternElement, Image> _PatternToImage= new Dictionary<BattlePatternElement, Image>();
    [SerializeField] private Image _XSlashImage;
    [SerializeField] private Image _YSlashImage;
    [SerializeField] private Image _ADefendImage;
    RectTransform rectTransform;
    Vector3 offSet= new Vector3(-10,0,0);
    private void Start() {
        rectTransform=GetComponent<RectTransform>();
        _PatternToImage.Add(BattlePatternElement.XSlash,_XSlashImage);
        _PatternToImage.Add(BattlePatternElement.YSlash,_YSlashImage);
        _PatternToImage.Add(BattlePatternElement.ADefend,_ADefendImage);

    }
    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            rectTransform.position += offSet;
        }
        
        
    }

    public bool CheckInTimingWindow()
    {
        if (rectTransform.localPosition.x < -530  && rectTransform.localPosition.x > -700)
        {
            return true;
        }
        else return false;
    }
}

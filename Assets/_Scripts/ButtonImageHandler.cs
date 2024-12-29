using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageHandler : MonoBehaviour
{
    private Dictionary<BattlePatternElement, GameObject> _PatternToImage= new Dictionary<BattlePatternElement, GameObject>();
    [SerializeField] private GameObject _XSlashImage;
    [SerializeField] private GameObject _YSlashImage;
    [SerializeField] private GameObject _ADefendImage;
    //Fail and Success images
    [SerializeField] private Sprite _XSlashFailedImage;
    [SerializeField] private Sprite _YSlashFailedImage;
    [SerializeField] private Sprite _ADefendFailedImage;
    [SerializeField] private Sprite _XSlashSuccessImage;
    [SerializeField] private Sprite _YSlashSuccessImage;
    [SerializeField] private Sprite _ADefendSuccessImage;
    //Set Spawn and End points on scene
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform checkingPoint;
    public float SecPerBeat{get;set;}
    Vector3 offSet= new Vector3(-10,0,0);
    private Player _player;
    private Animator _playerAnim;
    
    private GameObject _currentButtonToCheck;
    private void Start() {
        
        _PatternToImage.Add(BattlePatternElement.XSlash,_XSlashImage);
        _PatternToImage.Add(BattlePatternElement.YSlash,_YSlashImage);
        _PatternToImage.Add(BattlePatternElement.ADefend,_ADefendImage);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerAnim = _player.gameObject.GetComponent<Animator>();

    }
    public void SpawnButton(BattlePatternElement pattern)
    {
       
        GameObject button = Instantiate(_PatternToImage[pattern], spawnPoint.position, Quaternion.identity,spawnPoint.transform);
        StartCoroutine(MoveButton(button.gameObject));
    }
    
    private IEnumerator MoveButton(GameObject button)
    {
        
        Vector3 startPosition = spawnPoint.position;
        Vector3 endPosition = endPoint.position;
        Vector3 checkingPosition = checkingPoint.position;

        // Calculate constant speed
        float speed = Vector3.Distance(startPosition, checkingPosition) / SecPerBeat; 
        RectTransform buttonRect = button.GetComponent<RectTransform>();
        while (button != null) // Continue moving until the button is destroyed
        {
            if (_currentButtonToCheck == null) _currentButtonToCheck = button;
            
            
            // Move the button at a constant speed
            button.transform.position += (endPosition - startPosition).normalized * speed * Time.deltaTime;

            if (buttonRect.anchoredPosition.x<-875f && button.GetComponent<ButtonImage>().IsBeingChecked){
                OnButtonFailed(button.GetComponent<ButtonImage>().GetBattlePatternElement().ToString());
                button.GetComponent<ButtonImage>().IsBeingChecked=false;
            }
            // Check if it has passed the end point
            if (Vector3.Distance(button.transform.position, endPosition) < 10f)
            {
                Destroy(button);
                yield break;
            }
            yield return null;
        }
    }
    public bool IsInCheckArea(){
        
        if (_currentButtonToCheck == null) {
            return false;}
        return _currentButtonToCheck.GetComponent<RectTransform>().anchoredPosition.x < -610f && _currentButtonToCheck.GetComponent<RectTransform>().anchoredPosition.x>-875f ; //I manually tested these positions on the scene view.
    }
    public bool CheckButtonToActionName(string actionName)
    {
        if (_currentButtonToCheck == null) return false;
        switch (actionName)
        {
            case "X-Slash":
                return _currentButtonToCheck.GetComponent<ButtonImage>().GetBattlePatternElement() == BattlePatternElement.XSlash;
            case "Y-Slash":
                return _currentButtonToCheck.GetComponent<ButtonImage>().GetBattlePatternElement() == BattlePatternElement.YSlash;
            case "A-Defend":
                return _currentButtonToCheck.GetComponent<ButtonImage>().GetBattlePatternElement() == BattlePatternElement.ADefend;
            default:
                return false;
        }
    }
    public void OnButtonFailed(string actionName){
        
        if (_currentButtonToCheck == null) return;
        if (!_currentButtonToCheck.GetComponent<ButtonImage>().IsBeingChecked) return;
        _currentButtonToCheck.GetComponent<ButtonImage>().IsBeingChecked=false;
        
        switch (_currentButtonToCheck.GetComponent<ButtonImage>().GetBattlePatternElement().ToString())
        {
            case "XSlash":
                _currentButtonToCheck.GetComponent<Image>().sprite = _XSlashFailedImage;
                BattleEncounterManager.instance.ResetCombo();
                break;
            case "YSlash":
                _currentButtonToCheck.GetComponent<Image>().sprite = _YSlashFailedImage;
                BattleEncounterManager.instance.ResetCombo();
                break;
            case "ADefend":
                _currentButtonToCheck.GetComponent<Image>().sprite = _ADefendFailedImage;
                BattleEncounterManager.instance.OnFailToDefend();
                break;
            default:
                break;
        }
    }
    public void OnButtonSuccess(string actionName, EnemyBase currentEnemy){
        if (_currentButtonToCheck == null) return;
        if (!_currentButtonToCheck.GetComponent<ButtonImage>().IsBeingChecked) return;
        _currentButtonToCheck.GetComponent<ButtonImage>().IsBeingChecked=false;
        switch (_currentButtonToCheck.GetComponent<ButtonImage>().GetBattlePatternElement().ToString())
        {
            case "XSlash":
                _currentButtonToCheck.GetComponent<Image>().sprite = _XSlashSuccessImage;
                currentEnemy.TakeDamage(_player.GetPlayerDamage()*BattleEncounterManager.instance.GetComboDamageMultiplier());
                currentEnemy.GetComponent<Animator>().SetTrigger("Hurt");
                _playerAnim.SetTrigger("LightAttack");

                break;
            case "YSlash":
                _currentButtonToCheck.GetComponent<Image>().sprite = _YSlashSuccessImage;
                currentEnemy.TakeDamage(_player.GetPlayerDamage()*1.5f*BattleEncounterManager.instance.GetComboDamageMultiplier());
                currentEnemy.GetComponent<Animator>().SetTrigger("Hurt");
                _playerAnim.SetTrigger("HeavyAttack");
                break;
            case "ADefend":
                _currentButtonToCheck.GetComponent<Image>().sprite = _ADefendSuccessImage;
                currentEnemy.GetComponent<Animator>().SetTrigger("Attack");
                _playerAnim.SetTrigger("Defend");
                break;
            default:
                break;
        }
    }
}

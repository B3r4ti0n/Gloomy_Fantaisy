using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControler : MonoBehaviour
{
    [SerializeField]
    Text _textToChange;

    [SerializeField]
    Text _textToChange2;

    [SerializeField]
    List<string> _wordList;

    int _currentWordIndex = 0;

    [SerializeField]
    Button leftButton;

    [SerializeField]
    Button rightButton;

    [SerializeField]
	Transform _objectToRotate;

    [SerializeField]
    float _rotationAmount = 120f;

    [SerializeField]
    float _animationDuration = 0.5f;

    private bool _isRotating = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //Onclick rotate name and model 3d
        leftButton.onClick.AddListener(UpdateTextLeft);

        rightButton.onClick.AddListener(UpdateTextRight);
    }

    void UpdateTextLeft()
    { 
        if (!_isRotating)
        {
            if (_currentWordIndex <= 0)
                {
                    _currentWordIndex = _wordList.Count;
                }
            _currentWordIndex--;
            _textToChange.text = _wordList[_currentWordIndex];
            _textToChange2.text = _wordList[_currentWordIndex];
            StartCoroutine(RotateAndAnimate("left"));
        }
    }
    void UpdateTextRight()
    {
       
        if (!_isRotating)
        {
            if (_currentWordIndex >= _wordList.Count - 1)
                {
                    _currentWordIndex = -1;
                }
            _currentWordIndex++;
            _textToChange.text = _wordList[_currentWordIndex];
            _textToChange2.text = _wordList[_currentWordIndex];
            StartCoroutine(RotateAndAnimate("right"));
        }
    }
    IEnumerator RotateAndAnimate(string btnClicked )
    {
        _isRotating = true;
        float endRotation;

        // Rotation in axe y
        float startRotation = _objectToRotate.rotation.eulerAngles.y;
        //If left button, left rotate left else rotate right
        if (btnClicked == "left")
        {
            endRotation = startRotation - _rotationAmount;
        }else
        {
            endRotation = startRotation + _rotationAmount;   
        }
        float t = 0f;
        while (t < 1f)
        {
            //t --> time animation
            t += Time.deltaTime / _animationDuration;
            //start --> end in time
            float currentRotation = Mathf.Lerp(startRotation, endRotation, t);
            //start rotate
            _objectToRotate.rotation = Quaternion.Euler(0f, currentRotation, 0f);
            yield return null;
        }
            _isRotating = false;
        
    }
}

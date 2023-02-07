using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame
{
    public class ThumbsUpButton : MonoBehaviour
    {
        [SerializeField]
        private Button thumbsUpButton;
        [SerializeField]
        private Image thumbsUpButtonImage;
        [SerializeField]
        private Button thumbsDownButton;
        [SerializeField]
        private Image thumbsDownButtonImage;
        private bool isClicked;
        [SerializeField]
        private Text text;
        [SerializeField]
        private Sprite[] sprites;

        // Start is called before the first frame update
        void Start()
        {
            thumbsUpButton.onClick.AddListener(ButtonClick);
            thumbsDownButton.onClick.AddListener(ThumbsDownButtonClick);
        }

        private void ButtonClick()
        {
            if (string.Equals(text.text, "0"))
            {
                text.text = "1";
                thumbsUpButtonImage.sprite = sprites[1];
                thumbsDownButtonImage.sprite = sprites[2];
                isClicked = false;
            }
            else
            {
                text.text = "0";
                thumbsUpButtonImage.sprite = sprites[0];
            }
        }

        private void ThumbsDownButtonClick()
        {
            if (isClicked)
            {
                thumbsDownButtonImage.sprite = sprites[2];
            }
            else
            {
                thumbsDownButtonImage.sprite = sprites[3];
                thumbsUpButtonImage.sprite = sprites[0];
                text.text = "0";
            }

            isClicked = !isClicked;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame
{
    public class ThumbsUpButton : MonoBehaviour
    {
        [SerializeField]
        private Image thumbsUpButtonImage;
        [SerializeField]
        private Image thumbsDownButtonImage;
        [SerializeField]
        private Text likesNumberDisplay;
        [SerializeField]
        private Sprite[] sprites;

        private bool isClicked;

        public void ClickThumbsUp()
        {
            if (string.Equals(likesNumberDisplay.text, "0"))
            {
                likesNumberDisplay.text = "1";
                thumbsUpButtonImage.sprite = sprites[1];
                thumbsDownButtonImage.sprite = sprites[2];
                isClicked = false;
            }
            else
            {
                likesNumberDisplay.text = "0";
                thumbsUpButtonImage.sprite = sprites[0];
            }
        }

        public void ClickThumbsDown()
        {
            if (isClicked)
            {
                thumbsDownButtonImage.sprite = sprites[2];
            }
            else
            {
                thumbsDownButtonImage.sprite = sprites[3];
                thumbsUpButtonImage.sprite = sprites[0];
                likesNumberDisplay.text = "0";
            }

            isClicked = !isClicked;
        }
    }
}
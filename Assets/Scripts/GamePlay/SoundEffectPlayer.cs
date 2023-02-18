using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SnakeGame
{
    public class SoundEffectPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer masterMixer;
        [SerializeField]
        private Slider audioSlider;
        [SerializeField]
        private Button muteButton;
        [SerializeField]
        private Image muteIcon;
        [SerializeField]
        private Sprite[] icons;

        private bool isMuted;
        private float lastVolume;
        private float lastSliderValue;
        private bool isButtonPressed;

        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip[] audioClips;

        void Start()
        {
            masterMixer.SetFloat("Volume", -15);

            GameFlowEvents.SubscriberCollected.AddListener(PlayCollectingSound);
            GameFlowEvents.GameOver.AddListener(PlayExplodingSound);

            audioSlider.onValueChanged.AddListener(ChangeVolume);
            muteButton.onClick.AddListener(ToggleMuteButton);
        }

        private void ToggleMuteButton()
        {
            isButtonPressed = true;
            if (isMuted)
            {
                muteIcon.sprite = icons[0];
                masterMixer.SetFloat("Volume", lastVolume);
                audioSlider.value = lastSliderValue;
            }
            else
            {
                muteIcon.sprite = icons[1];
                masterMixer.GetFloat("Volume", out lastVolume);
                masterMixer.SetFloat("Volume", -80);
                lastSliderValue = audioSlider.value;
                audioSlider.value = -80;
            }

            isMuted = !isMuted;
            isButtonPressed = false;
        }

        private void ChangeVolume(float volume)
        {
            if (isMuted)
            {
                muteIcon.sprite = icons[0];

                if (!isButtonPressed)
                {
                    isMuted = false;
                }
            }

            if (!isButtonPressed)
            {
                masterMixer.SetFloat("Volume", volume);
            }
        }

        private void PlayCollectingSound()
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }

        private void PlayExplodingSound()
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
    }
}
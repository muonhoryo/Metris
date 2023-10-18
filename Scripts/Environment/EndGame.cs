using GameJam_Temple.Characters.COP;
using GameJam_Temple.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam_Temple.Level
{
    public sealed class EndGame : MonoBehaviour
    {
        [SerializeField]
        private Control.CharacterController Controller;
        [SerializeField]
        private GameObject FinalMessage;
        [SerializeField]
        private Text PlayingTimeText;
        [SerializeField]
        private Text DyingCountText;
        [SerializeField]
        private Image EndingSprite;
        [SerializeField]
        private Timer timer;
        [SerializeField]
        private DeathCounter Counter;

        [SerializeField]
        private Sprite[] Endings;

        [SerializeField]
        private int SecondEndingDeathCount;
        [SerializeField]
        private int ThirdEndingDeathCount;
        [SerializeField]
        private string DeathCountMessageText;

        [SerializeField]
        private float DelayTime;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == Registry.Maincharacter.gameObject)
            {
                StartCoroutine(Script());
            }
        }
        IEnumerator Script()
        {
            (Registry.Maincharacter as IGroundMovingCharacter).StopMoving();
            Destroy(Controller);
            Destroy(MainCameraBehaviour.Instance_);
            FinalMessage.SetActive(true);
            timer.Stop();
            PlayingTimeText.text = timer.TimerText;
            DyingCountText.text = DeathCountMessageText+Counter.deathCount;
            if (Counter.deathCount >= ThirdEndingDeathCount)
            {
                EndingSprite.sprite = Endings[2];
            }
            else if (Counter.deathCount >= SecondEndingDeathCount)
            {
                EndingSprite.sprite = Endings[1];
            }
            else
            {
                EndingSprite.sprite = Endings[0];
            }
            yield return new WaitForSeconds(DelayTime);
            (Registry.Maincharacter as IGroundMovingCharacter).SetDirection(1);
            (Registry.Maincharacter as IGroundMovingCharacter).StartMoving();
            Destroy(this);
        }
    }
}

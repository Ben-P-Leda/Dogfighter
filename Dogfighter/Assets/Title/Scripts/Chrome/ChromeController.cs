using UnityEngine;
using Shared.Scripts.Gui;

namespace Title.Scripts.Chrome
{
    public class ChromeController : GuiManager
    {
        private ChromeText[] _texts;

        public Camera SceneCamera;

        protected override void Awake()
        {
            base.Awake();

            _texts = new ChromeText[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                _texts[i] = transform.GetChild(i).GetComponent<ChromeText>();
            }
        }

        private void Start()
        {
            for (int i = 0; i < _texts.Length; i++)
            {
                _texts[i].SetUpDisplay();
            }
        }
    }
}

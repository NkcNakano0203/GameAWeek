using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace TitleUI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField]
        Button startButton;
        [SerializeField]
        Button quitButton;

        private Subject<Unit> startButtonSubject = new Subject<Unit>();
        public IObservable<Unit> StartButton => startButtonSubject;

        private Subject<Unit> quitButtonSubject = new Subject<Unit>();
        public IObservable<Unit> QuitButton => quitButtonSubject;

        void Start()
        {
            startButton.OnClickAsObservable()
                       .Subscribe(_ => { startButtonSubject.OnNext(Unit.Default); })
                       .AddTo(gameObject);
            quitButton.OnClickAsObservable()
                      .Subscribe(_ => { quitButtonSubject.OnNext(Unit.Default); })
                      .AddTo(gameObject);
        }
    }
}
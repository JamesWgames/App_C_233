using System;
using DG.Tweening;
using Prototype.AudioCore;
using Slots.Game.Values;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

namespace DailyRewarded
{
    public class DailyRewardedPanel : Panel
    {
        public static bool IsReachedPrizeToday
        {
            get => PlayerPrefs.GetInt(CurrentDayCode, 0) == 1;
            set => PlayerPrefs.SetInt(CurrentDayCode, value ? 1 : 0);
        }

        private static string CurrentDayCode =>
            $"Day_{DateTime.Now.Year}_{DateTime.Now.DayOfYear}";

        [SerializeField] private CanvasGroup _spinButtonGroup = null;
        [SerializeField] private CanvasGroup _prizeGroup = null;
        
        [Space] 
        
        [SerializeField] private Button _spinButton = null;
        [SerializeField] private Button _okButton= null;
        
        [Space] 

        [SerializeField] private Text _prizeText = null;

        [Space]
        
        [SerializeField] private PrizeSector[] _sectors = new PrizeSector[0];

        [Space]

        [SerializeField] private int _minSpinCount = 2;
        [SerializeField] private int _maxSpinCount = 3;

        [Space]

        [SerializeField] private float _spinTime = 1f;
        [SerializeField] private AnimationCurve _spinCurve = null;

        [Space]

        [SerializeField] private Transform _rotationTarget = null;

        private void OnEnable()
        {
            _spinButtonGroup.alpha = 1f;
            _spinButtonGroup.gameObject.SetActive(true);
            
            _prizeGroup.alpha = 0f;
            _prizeGroup.gameObject.SetActive(false);

            _spinButton.interactable = true;
            _okButton.interactable = false;

            _prizeText.text = "0";
            
            _rotationTarget.localEulerAngles = Vector3.zero;
        }

        public void Spin()
        {
            _spinButton.interactable = false;

            int spinCount = Random.Range(_minSpinCount, _maxSpinCount);
            int sectorIndex = Random.Range(0, _sectors.Length);

            float sectorCenter = (_sectors[sectorIndex].startAngle + _sectors[sectorIndex].endAngle) / 2f;
            float sectorSize = Mathf.Abs(_sectors[sectorIndex].endAngle - _sectors[sectorIndex].startAngle);

            float sectorRandomSize = sectorSize / 4f;
            
            float spinValue = (spinCount * 360f) + sectorCenter + Random.Range(-sectorRandomSize, sectorRandomSize);

            float startSpinValue = _rotationTarget.localEulerAngles.z;

            AudioController.PlaySound("spin");

            DOVirtual.Float(startSpinValue, spinValue, _spinTime, (value) =>
                {
                    _rotationTarget.localEulerAngles = new Vector3(0f, 0f, startSpinValue - value);
                })
                .SetEase(_spinCurve)
                .OnComplete(() =>
                {
                    float angle = _rotationTarget.localEulerAngles.z;

                    angle += 360f;

                    angle %= 360;

                    for (int i = 0; i < _sectors.Length; i++)
                    {
                        PrizeSector sector = _sectors[i];

                        if (angle > sector.startAngle && angle < sector.endAngle)
                        {
                            _spinButtonGroup.DOFade(0f, 0.25f)
                                .OnComplete(() =>
                                {
                                    if (sector.amount == 0)
                                    {
                                        AudioController.PlaySound("zero");
                                    }
                                    else
                                    {
                                        AudioController.PlaySound("coins");
                                    }

                                    Wallet.AddMoney(sector.amount);

                                    _spinButton.gameObject.SetActive(false);
                                    
                                    _prizeGroup.gameObject.SetActive(true);
                                    _prizeGroup.DOFade(1f, 0.25f);

                                    _prizeText.DOCounter(0, sector.amount, 0.5f, false);

                                    _okButton.interactable = true;

                                    IsReachedPrizeToday = true;

                                });

                            return;
                        }
                    }
                });
        }

        private void OnDrawGizmos()
        {
            float startRotation = _rotationTarget.localEulerAngles.z;

            Vector3 startPosition = _rotationTarget.position;

            startPosition.z -= 0.1f;

            for (int i = 0; i < _sectors.Length; i++)
            {
                Mesh mesh = new Mesh();

                Vector3[] vertices = new Vector3[]
                {
                    startPosition,
                    startPosition + (Quaternion.Euler(0, 0, startRotation - _sectors[i].startAngle) * transform.up * 5f),
                    startPosition + (Quaternion.Euler(0, 0, startRotation - _sectors[i].endAngle) * transform.up * 5f)
                };

                int[] triangles = new int[]
                {
                    0, 1, 2
                };

                mesh.vertices = vertices;
                mesh.triangles = triangles;

                mesh.RecalculateNormals();

                Gizmos.color = i % 2 == 0 ? new Color(1, 0, 0, 0.5f) : new Color(0, 0, 1, 0.5f);

                Gizmos.DrawMesh(mesh);

                Gizmos.color = i % 2 == 0 ? new Color(1, 0, 0, 1f) : new Color(0, 0, 1, 1f);

                Gizmos.DrawWireMesh(mesh);
            }
        }
    }
}
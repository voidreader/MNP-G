namespace G2USample
{
    using System;
    using Google2u;
    using UnityEngine;
    using System.Collections;

    public class Hud : MonoBehaviour
    {

        private GameObject _Database;

        public static string Language = "en";
        public static BaseAIScript CurAiScript = null;

        private SampleLocalization _Localization;
        private Rect _Message = new Rect(0.1f, 0.1f, 0.8f, 0.1f);
        private Rect _MessageRect = new Rect();
        public Rect SpawnButton = new Rect(0.01f, 0.01f, 0.1f, 0.1f);
        private Rect _SpawnButtonRect = new Rect();
        public Rect EnglishButton = new Rect(0.3f, 0.01f, 0.1f, 0.1f);
        private Rect _EnglishButtonRect = new Rect();
        public Rect SpanishButton = new Rect(0.4f, 0.01f, 0.1f, 0.1f);
        private Rect _SpanishButtonRect = new Rect();
        public Rect FrenchButton = new Rect(0.5f, 0.01f, 0.1f, 0.1f);
        private Rect _FrenchButtonRect = new Rect();
        public Rect StatsRect = new Rect(0.1f, 0.7f, 0.5f, 0.3f);
        private Rect _StatsRect = new Rect();

        public delegate void OnSpawnClicked();

        public event OnSpawnClicked OnSpawn;

        // Use this for initialization
        private void Start()
        {
            _Database = GameObject.Find("Google2uDatabase");
            _Localization = SampleLocalization.Instance;
        }

        private void Update()
        {
            var w = Screen.width;
            var h = Screen.height;

            _MessageRect.height = h*_Message.height;
            _MessageRect.width = w*_Message.width;
            _MessageRect.x = w*_Message.x;
            _MessageRect.y = h*_Message.y;

            _SpawnButtonRect.height = h*SpawnButton.height;
            _SpawnButtonRect.width = w*SpawnButton.width;
            _SpawnButtonRect.x = w*SpawnButton.x;
            _SpawnButtonRect.y = h*SpawnButton.y;

            _EnglishButtonRect.height = h*EnglishButton.height;
            _EnglishButtonRect.width = w*EnglishButton.width;
            _EnglishButtonRect.x = w*EnglishButton.x;
            _EnglishButtonRect.y = h*EnglishButton.y;

            _SpanishButtonRect.height = h*SpanishButton.height;
            _SpanishButtonRect.width = w*SpanishButton.width;
            _SpanishButtonRect.x = w*SpanishButton.x;
            _SpanishButtonRect.y = h*SpanishButton.y;

            _FrenchButtonRect.height = h*FrenchButton.height;
            _FrenchButtonRect.width = w*FrenchButton.width;
            _FrenchButtonRect.x = w*FrenchButton.x;
            _FrenchButtonRect.y = h*FrenchButton.y;

            _StatsRect.height = h*StatsRect.height;
            _StatsRect.width = w*StatsRect.width;
            _StatsRect.x = w*StatsRect.x;
            _StatsRect.y = h*StatsRect.y;
        }

        private void OnGUI()
        {
            if (_Database == null)
            {
                GUI.Label(_MessageRect, "Google2uDatabase not found in the scene.");
                return;
            }

            if (_Localization == null)
            {
                GUI.Label(_MessageRect, "Localization not found");
                return;
            }

            if (GUI.Button(_SpawnButtonRect,
                _Localization.GetRow(SampleLocalization.rowIds.STR_SPAWN).GetStringData(Language)))
            {
                if (OnSpawn != null)
                    OnSpawn();
            }

            if (GUI.Button(_EnglishButtonRect,
                _Localization.GetRow(SampleLocalization.rowIds.STR_ENGLISH).GetStringData(Language)))
            {
                Language = "en";
            }
            if (GUI.Button(_SpanishButtonRect,
                _Localization.GetRow(SampleLocalization.rowIds.STR_SPANISH).GetStringData(Language)))
            {
                Language = "es";
            }
            if (GUI.Button(_FrenchButtonRect,
                _Localization.GetRow(SampleLocalization.rowIds.STR_FRENCH).GetStringData(Language)))
            {
                Language = "fr";
            }

            if (CurAiScript != null)
            {
                var name = _Localization.GetRow(SampleLocalization.rowIds.STR_NAME).GetStringData(Language) + ": " +
                           CurAiScript.BotName;
                var level = _Localization.GetRow(SampleLocalization.rowIds.STR_LEVEL).GetStringData(Language) + ": " +
                            CurAiScript.BotLevel;
                var hp = _Localization.GetRow(SampleLocalization.rowIds.STR_HEALTH).GetStringData(Language) + ": " +
                         CurAiScript.Health;
                var weapon = _Localization.GetRow(SampleLocalization.rowIds.STR_WEAPON).GetStringData(Language) + ": " +
                             _Localization.GetRow(CurAiScript.WeaponID).GetStringData(Language);

                GUI.Label(_StatsRect, name + Environment.NewLine +
                                      level + Environment.NewLine +
                                      hp + Environment.NewLine +
                                      weapon);
            }

        }
    }
}
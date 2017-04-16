namespace G2USample
{
    using UnityEngine;
    using System.Collections;

    public class AIManager : MonoBehaviour
    {

        public GameObject pMineBot;
        public GameObject pBuzzBot;
        public GameObject[] pSpawnPoints;
        public GameObject pMessageBubble;
        private Google2u.SampleCharacters _Db;
        private Google2u.SampleDialogMap _DialogMap;
        private Google2u.SampleWeapons _Weapons;
        private Hud _hud;

        // Use this for initialization
        private void Start()
        {

            var hudGameObject = GameObject.Find("HudCamera");
            if (hudGameObject != null)
            {
                _hud = hudGameObject.GetComponent<Hud>();
                _hud.OnSpawn += OnSpawn;
            }

            _Db = GetComponent<Google2u.SampleCharacters>();
            _DialogMap = GetComponent<Google2u.SampleDialogMap>();
            _Weapons = GetComponent<Google2u.SampleWeapons>();

        }

        private void OnSpawn()
        {
            if (_Db == null)
                return;

            var numRows = _Db.Rows.Count;

            foreach (var pSpawnPoint in pSpawnPoints)
            {
                var randomRow = Random.Range(0, numRows);
                var randomScript = _Db.Rows[randomRow];
                var dialogMapRow = _DialogMap.GetRow(randomScript._Dialog);
                var weaponRow = _Weapons.GetRow(randomScript._Weapon);
                var spawnScript = pSpawnPoint.GetComponent<SampleSpawnPoint>();
                spawnScript.DoSpawn(randomScript, dialogMapRow, weaponRow, this);
            }
        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}
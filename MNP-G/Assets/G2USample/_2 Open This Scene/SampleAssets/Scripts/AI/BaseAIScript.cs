namespace G2USample
{
    using System;
    using UnityEngine;

    public class BaseAIScript : MonoBehaviour
    {
        public string BotName = "None";
        public int BotLevel = 0;
        public bool CanFly = false;
        public float Health = 1.0f;
        public float Speed = 1.0f;
        public string WeaponID = "None";
        public string[] OnDamaged;
        public string[] OnDeath;

        public SampleSpawnPoint MySpawnPoint;
        private RotateRight _RotateRight;
        private RotateLeft _RotateLeft;

        // Update is called once per frame
        private void Update()
        {
            var hitInfo = new RaycastHit();
            var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (!hit || hitInfo.transform.gameObject != gameObject)
            {
                if (Hud.CurAiScript == this)
                    Hud.CurAiScript = null;
                return;
            }
            Hud.CurAiScript = this;

            if (Input.GetMouseButtonDown(0))
            {
                Health -= 1.0f;
                if (MySpawnPoint.MyAIManager.pMessageBubble != null)
                {
                    var curMessageBubble = (GameObject) Instantiate(MySpawnPoint.MyAIManager.pMessageBubble);

                    var mbScript = curMessageBubble.GetComponent<SampleMessageBubble>();
                    if (mbScript != null)
                    {
                        var db = SampleLocalization.Instance;

                        if (Health > 0)
                        {
                            var dialogCount = OnDamaged.Length;
                            if (dialogCount > 0)
                            {
                                var randomDialog = UnityEngine.Random.Range(0, dialogCount);
                                var curDialogRow = db.GetRow(OnDamaged[randomDialog]);
                                if (curDialogRow != null)
                                    mbScript.MyText = curDialogRow.GetStringData(Hud.Language);
                                curMessageBubble.transform.position = transform.position;
                            }
                        }
                        else
                        {
                            var dialogCount = OnDeath.Length;
                            if (dialogCount > 0)
                            {
                                var randomDialog = UnityEngine.Random.Range(0, dialogCount);
                                var curDialogRow = db.GetRow(OnDeath[randomDialog]);
                                if (curDialogRow != null)
                                    mbScript.MyText = curDialogRow.GetStringData(Hud.Language);
                                curMessageBubble.transform.position = transform.position;
                            }
                        }
                    }

                }
                if (!(Health <= 0.0f))
                    return;

                MySpawnPoint.MyAIObject = null;
                Destroy(gameObject);
            }
        }

        public void EnableAiScript(string in_scriptName)
        {
            _RotateRight = GetComponent<RotateRight>();
            _RotateLeft = GetComponent<RotateLeft>();

            if (in_scriptName.Equals("Right", StringComparison.InvariantCultureIgnoreCase) && _RotateRight != null)
            {
                _RotateRight.enabled = true;
                _RotateRight.Speed = Speed;
            }
            if (in_scriptName.Equals("Left", StringComparison.InvariantCultureIgnoreCase) && _RotateLeft != null)
            {
                _RotateLeft.enabled = true;
                _RotateLeft.Speed = Speed;
            }

        }

    }
}
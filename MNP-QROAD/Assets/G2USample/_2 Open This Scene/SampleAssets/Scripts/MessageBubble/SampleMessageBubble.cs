namespace G2USample
{
    using UnityEngine;

    public class SampleMessageBubble : MonoBehaviour
    {

        public string MyText = string.Empty;
        private TextMesh _MyTextMesh;
        private Vector3 _FallRate;
        private float _TimeElapsed = 0.0f;
        private const float MaxTime = 0.5f;
        // Use this for initialization
        private void Start()
        {
            var myTextScript = GetComponentInChildren<TextMesh>();
            if (myTextScript != null)
                _MyTextMesh = myTextScript;
            _FallRate = new Vector3(0.0f, -3.0f, 0.0f);

            if (_MyTextMesh != null)
                _MyTextMesh.text = MyText;
        }

        // Update is called once per frame
        private void Update()
        {

            _TimeElapsed += Time.deltaTime;

            var curFall = _FallRate;
            curFall.y *= Time.deltaTime;

            transform.position += curFall;

            if (_TimeElapsed >= MaxTime)
                Destroy(gameObject);

        }
    }
}
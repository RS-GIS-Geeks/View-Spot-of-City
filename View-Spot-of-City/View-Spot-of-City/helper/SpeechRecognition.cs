using SpeechLib;

namespace View_Spot_of_City.helper
{
    public class SpeechRecognition
    {
        private static SpeechRecognition _Instance = null;
        private ISpeechRecoGrammar isrg;
        private SpSharedRecoContext ssrContex = null;

        public delegate void StringEvent(string str);
        public StringEvent SetMessage;

        private SpeechRecognition()
        {
            ssrContex = new SpSharedRecoContext();
            isrg = ssrContex.CreateGrammar(1);
            _ISpeechRecoContextEvents_RecognitionEventHandler recHandle =
                new _ISpeechRecoContextEvents_RecognitionEventHandler(ContexRecognition);
            ssrContex.Recognition += recHandle;
        }

        public void BeginRec()
        {
            isrg.DictationSetState(SpeechRuleState.SGDSActive);
        }

        public static SpeechRecognition Instance()
        {
            if (_Instance == null)
                _Instance = new SpeechRecognition();
            return _Instance;
        }

        public void CloseRec()
        {
            isrg.DictationSetState(SpeechRuleState.SGDSInactive);
        }

        private void ContexRecognition(int iIndex, object obj, SpeechRecognitionType type, ISpeechRecoResult result)
        {
            SetMessage?.Invoke(result.PhraseInfo.GetText(0, -1, true));
        }
    }
}

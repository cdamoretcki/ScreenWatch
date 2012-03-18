using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScreenShotReceiver
{
    public class TextAnalysisResult
    {
        private Dictionary<string, Dictionary<string, List<WordConfidence>>> _faults = new Dictionary<string, Dictionary<string, List<WordConfidence>>>();
        
        /// <summary>
        /// add the filterword and the text that contained the filter word
        /// </summary>
        /// <param name="email">email recipient for notification</param>
        /// <param name="fault">word to filter</param>
        /// <param name="word">text containing word to filter</param>
        /// <param name="confidence">confidence of the word</param>
        public void AddFault(string email, string filter, string word, int confidence)
        {
            string upperEmail = email.ToUpper();
            if (!_faults.Keys.Contains(upperEmail))
            {
                var faultsPerEmail = new Dictionary<string, List<WordConfidence>>();
                faultsPerEmail[filter] = new List<WordConfidence>();
                _faults[upperEmail] = faultsPerEmail;
            }
            else
            {
                var faultsPerEmail = _faults[upperEmail];
                if (!faultsPerEmail.Keys.Contains(filter))
                {
                    faultsPerEmail[filter] = new List<WordConfidence>();
                }
            }
            _faults[upperEmail][filter].Add(new WordConfidence(word, confidence));

        }

        /// <summary>
        /// List of all words found through OCR
        /// </summary>
        public IEnumerable<WordConfidence> Text
        {
            get
            {
                foreach(var triggers in _faults.Select(email => email.Value))
                {
                    foreach (var trigger in triggers.Select(trigger => trigger.Value))
                    {
                        foreach (var violation in trigger)
                        {
                            yield return violation;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// List of words that contained filtered words
        /// </summary>
        public Dictionary<string, Dictionary<string, List<WordConfidence>>> FaultedWordsByEmail
        {
            get
            {
                return _faults;
            }
        }
        
        public class WordConfidence
        {
            public string Word { get; set; }
            public int Confidence { get; set; }
            public WordConfidence(string word, int confidence)
            {
                Word = word;
                Confidence = confidence;
            }
        }
    }
}
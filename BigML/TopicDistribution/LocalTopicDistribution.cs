using System.Collections;
using System.Collections.Generic;
using System.Json;
using Iveonik.Stemmers;

namespace BigML
{
    /// A lightweight wrapper around a Topic Model.
    ///
    /// Uses a BigML remote Topic Model to build a local version that can be
    /// used to generate topic distributions for input documents locally.
    ///
    class LocalTopicDistribution
    {
        const int MAXIMUM_TERM_LENGTH = 30;
        const int MIN_UPDATES = 16;
        const int MAX_UPDATES = 512;
        const int SAMPLES_PER_TOPIC = 128;

        static Hashtable CODE_TO_NAME = new Hashtable() {
            {"da", "danish"},
            {"nl", "dutch"},
            {"en", "english"},
            {"fi", "finnish"},
            {"fr", "french"},
            {"de", "german"},
            {"hu", "hungarian"},
            {"it", "italian"},
            {"nn", "norwegian"},
            {"pt", "portuguese"},
            {"ro", "romanian"},
            {"ru", "russian"},
            {"es", "spanish"},
            {"sv", "swedish"},
            {"tr", "turkish"}
        };


        string resourceId;
        IStemmer stemmer = null;
        string seed = null;
        bool caseSensitive = false;
        bool bigrams = false;
        int ntopics = 0;
        object temp;
        float phi = 0.0F;
        int termToIndex = 0;
        JsonObject topicModelObject;
        List<string> topics;


        LocalTopicDistribution(TopicModel tm)
        {
            this.topics = new List<string>();

            if (tm.IsValidResource && tm.StatusMessage.IsFinished())
            {
                string language;

                topicModelObject = tm.Object["topic_model"];
                //this.topics = topicModelObject["topics"];
                language = tm.Object["language"];
                if (CODE_TO_NAME.ContainsKey(language))
                {

                }
            }
        }
    }
}

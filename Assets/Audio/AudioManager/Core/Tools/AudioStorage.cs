using System.Collections.Generic;

namespace Ruinarc
{
    public class AudioStorage 
    {
        private int storageLimit = 200;
        public List<AudioObject> currentAudio;

        #region Constructors

        public AudioStorage()
        {
            currentAudio = new List<AudioObject>();
        }

        public AudioStorage(int storageLimit)
        {
            this.storageLimit = storageLimit;
            currentAudio = new List<AudioObject>();
        }

        #endregion

        public bool HasPlace()
        {
            if(currentAudio.Count >= storageLimit)
            {
                return false;
            }

            return true;
        }
      
    }
}
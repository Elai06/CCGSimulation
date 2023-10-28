using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ImageLoader
    {
        private List<string> _urls = new();

        public ImageLoader()
        {
            _urls.Add("https://api.lorem.space/image/game?w=150&h=220");
            _urls.Add("https://api.lorem.space/image/movie?w=150&h=220");
            _urls.Add("https://api.lorem.space/image/album?w=150&h=150");
            _urls.Add("https://api.lorem.space/image/book?w=150&h=150");
            _urls.Add("https://api.lorem.space/image/face?w=150&h=150");
            _urls.Add("https://api.lorem.space/image/fashion?w=150&h=150");
            _urls.Add("https://api.lorem.space/image/ai?w=150&h=150");
            _urls.Add("https://api.lorem.space/image/dashboard?w=150&h=150");
            _urls.Add("https://api.lorem.space/image/crm?w=150&h=150");
            _urls.Add("https://api.lorem.space/image/finance?w=150&h=150");
            _urls.Add("https://api.lorem.space/image/calendar?w=150&h=150");
            _urls.Add("https://api.lorem.space/image/messenger?w=150&h=150");
        }

        public Texture2D LoadImage()
        {
            var www = new WWW(GetRandomUrl());
            while (!www.isDone)
            {
                if (www.error != null)
                {
                    Debug.LogError("There was an error loading the image: " + www.error);
                    return null;
                }
            }

            return www.texture;
        }

        private string GetRandomUrl()
        {
            var randomIndex = Random.Range(0, _urls.Count);
            return _urls[randomIndex];
        }
    }
}
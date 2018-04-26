using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.dug.UI.animation
{
    [Serializable]
    public class SpriteChunkList
    {
        [SerializeField]
        private string title;

        [SerializeField]
        private List<SpriteChunk> spriteChunkDataList;

        public string Title { get { return title; } }
        public List<SpriteChunk> SpriteChunkDataList { get { return spriteChunkDataList; } }
    }
}
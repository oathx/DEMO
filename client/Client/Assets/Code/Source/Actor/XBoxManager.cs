using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class  XBoxManager
{
    /// <summary>
    /// 
    /// </summary>
    static readonly XBoxManager     instance = new XBoxManager();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static XBoxManager       GetSingleton()
    {
        return instance;
    }

    #region BloodyHero
    public class XBloodyHero
    {
        public XBodyBoxComponent AttackHero
        {
            get
            {
                return _attackHero;
            }
        }

        public List<int> BloodyHeroes
        {
            get
            {
                return _bloodyHeroes;
            }
        }

        public List<XRectBox> OverlapBoxes
        {
            get
            {
                return _boxes;
            }
        }

        private XBodyBoxComponent _attackHero;

        private List<int> _bloodyHeroes;
        private List<XRectBox> _boxes;

        public XBloodyHero(XBodyBoxComponent attack)
        {
            _attackHero = attack;
        }

        public void AddBloodyHero(XBodyBoxComponent receive, XRectBox box)
        {
            if (_bloodyHeroes == null)
                _bloodyHeroes = new List<int>();

            if (_boxes == null)
                _boxes = new List<XRectBox>();

            _bloodyHeroes.Add(receive.cid);
            _boxes.Add(box);
        }

        public void AddBloodyHero(int receiveId, XRectBox box)
        {
            if (_bloodyHeroes == null)
                _bloodyHeroes = new List<int>();

            if (_boxes == null)
                _boxes = new List<XRectBox>();

            _bloodyHeroes.Add(receiveId);
            _boxes.Add(box);
        }

        public void Clear()
        {
            if (_bloodyHeroes != null)
                _bloodyHeroes.Clear();

            if (_boxes != null)
                _boxes.Clear();
        }
    }
    #endregion      //BloodyHero

    /// <summary>
    /// 
    /// </summary>
    private List<XBodyBoxComponent> mHeroes;
    private List<XBodyBoxComponent> mReceiveWarningHeroes;
    private List<XBodyBoxComponent> mLastReceiveWarningHeroes;
    private List<XBodyBoxComponent> mBodyAwayHeroes;
    private List<XBodyBoxComponent> mBodySortedHeroes;

    /// <summary>
    /// 
    /// </summary>
    public XBoxManager()
    {

    }

    private void InitBoxManager()
    {

    }
}


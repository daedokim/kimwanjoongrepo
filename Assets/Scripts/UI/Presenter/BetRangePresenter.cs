using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.dug.UI.view;

namespace com.dug.UI.presenter
{
    public class BetRangePresenter : IPresenter
    {
        private BetRangeView view;

        public BetRangePresenter(BetRangeView view)
        {
            this.view = view;
        }

    }
}

